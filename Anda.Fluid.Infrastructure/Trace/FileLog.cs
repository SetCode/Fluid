using CommonLibrary.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Infrastructure.Trace
{
    /// <summary>
    /// 日志内容输出到本地
    /// </summary>
    class FileLog : IDisposable
    {
        // 每个日志文件最大为1M
        private const int LOG_FILE_MAX_SIZE = 1024 * 1024;
        // 日志文件最长保留天数
        private const int LOG_FILE_RETAIN_DAYS = 30;
        // 崩溃日志文件最长保留天数
        private const int CRASH_FILE_RETAIN_DAYS = 90;
        // 当持续一段时间没有日志输出时，退出线程
        private const long REMAIN_IDLE_TIME = 5000L;
        // 在一定时间范围内，每次最少凑够多长的字符数才输出到本地
        private const int OUTPUT_MIN_LENGTH = 100 * 1024;
        // 记录上一次清理过期文件的日期
        private string lastCheckDateStr;
        // 日志文件存放目录
        private const string LOG_DIR = "log";
        // 崩溃日志存放目录
        private const string CRASH_DIR = "log/crash";
        // 存放日志内容的缓冲队列
        private BlockingCollection<string> logQueue = new BlockingCollection<string>();
        // 负责将日志内容输出到本地的线程
        private Thread saveToLocalThread;
        // 线程相关的锁
        private object threadlock = new object();
        // 线程是否已经退出
        private bool threadExited = true;
        // 标记是否正在运行
        private volatile bool isRunning = true;

        /// <summary>
        /// 将日志内容输出到本地
        /// </summary>
        /// <param name="msg"></param>
        internal void print(string msg)
        {
            if (isRunning)
            {
                lock (threadlock)
                {
                    logQueue.Add(msg);
                    if (threadExited)
                    {
                        // 启动负责将日志输出到本地的线程
                        saveToLocalThread = new Thread(saveToLocalRunnable);
                        saveToLocalThread.Priority = ThreadPriority.BelowNormal;
                        saveToLocalThread.Start();
                        threadExited = false;
                    }
                }
            }
        }

        /// <summary>
        /// 将崩溃日志内容输出到本地，存放在"./log/crash"目录下，保存在单独的文件中
        /// </summary>
        /// <param name="e"></param>
        internal void printOnCrash(Exception e)
        {
            saveCrashToLocal(e.ToString());
        }

        public void Dispose()
        {
            if (!isRunning)
            {
                return;
            }
            isRunning = false;
            // 等待缓冲区日志信息输出到本地
            while (saveToLocalThread != null && saveToLocalThread.IsAlive)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));
            }
        }

        /// <summary>
        /// 从logQueue中获取日志内容，输出到本地文件
        /// </summary>
        private void saveToLocalRunnable()
        {
            StringBuilder logMsg = new StringBuilder();
            long lastOutputTime = 0L;
            long expiredTime = 0L;
            int remainTime = 0;
            string item = null;
            while (isRunning || logQueue.Count > 0)
            {
                // 当超过一定时间或者超过一定长度时，输出到本地，减少操作本地文件的次数
                remainTime = 500;
                expiredTime = DateUtils.CurrTimeInMills + remainTime;
                while (remainTime > 0 && logMsg.Length < OUTPUT_MIN_LENGTH)
                {                   
                    logQueue.TryTake(out item, remainTime);
                    if (!string.IsNullOrEmpty(item))
                    {
                        logMsg.Append(item);
                    }
                    remainTime = (int)(expiredTime - DateUtils.CurrTimeInMills);
                }
                if (logMsg.Length > 0)
                {
                    saveLogToLocal(logMsg.ToString());
                    logMsg.Clear();
                    lastOutputTime = DateUtils.CurrTimeInMills;
                }
                // 超过一定时间没有输出日志，则结束线程
                if (DateUtils.CurrTimeInMills - lastOutputTime >= REMAIN_IDLE_TIME)
                {
                    lock (threadlock)
                    {
                        if (logQueue.Count == 0)
                        {
                            threadExited = true;
                            break;
                        }
                    }
                }
            }
            Debug.WriteLine("log thread exit.");
        }

        /// <summary>
        /// 将日志内容保存到本地
        /// </summary>
        /// <param name="msg">日志内容</param>
        private void saveLogToLocal(string msg)
        {
            if (!Directory.Exists(LOG_DIR))
            {
                Directory.CreateDirectory(LOG_DIR);
            }
            cleanExpiredLogFiles();
            saveTextToLocal(getLogFilePath(), msg);
        }

        /// <summary>
        /// 清除过期日志文件
        /// </summary>
        private void cleanExpiredLogFiles()
        {
            string[] subdirs = Directory.GetDirectories(LOG_DIR);
            if (subdirs == null || subdirs.Length == 0)
            {
                return;
            }
            string dtStr = DateTime.Now.AddDays(-LOG_FILE_RETAIN_DAYS).ToString("yyyyMMdd");
            // 当天已经检测过，不再检测
            if (dtStr.Equals(lastCheckDateStr))
            {
                return;
            }
            foreach (string subdir in subdirs)
            {
                if (new DirectoryInfo(subdir).Name.CompareTo(dtStr) <= 0)
                {
                    Directory.Delete(subdir, true);
                }
            }
            lastCheckDateStr = dtStr;
        }

        /// <summary>
        /// 获取当前要输出的日志文件全路径
        /// </summary>
        /// <returns></returns>
        private string getLogFilePath()
        {
            string dir = string.Format($"{LOG_DIR}/{DateTime.Now.ToString("yyyyMMdd")}");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            FileInfo fileInfo = null;
            for (int i = 1; ; i++)
            {
                fileInfo = new FileInfo($"{dir}/{i}.txt");
                if (fileInfo.Exists && fileInfo.Length > LOG_FILE_MAX_SIZE)
                {
                    continue;
                }
                return fileInfo.FullName;
            }
        }

        /// <summary>
        /// 将崩溃日志内容保存到本地
        /// </summary>
        /// <param name="msg">崩溃日志内容</param>
        private void saveCrashToLocal(string msg)
        {
            if (!Directory.Exists(CRASH_DIR))
            {
                Directory.CreateDirectory(CRASH_DIR);
            }
            cleanExpiredCrashFiles();
            saveTextToLocal(getCrashFilePath(), msg);
        }

        /// <summary>
        /// 清除过期崩溃日志文件
        /// </summary>
        private void cleanExpiredCrashFiles()
        {
            string[] files = Directory.GetFiles(CRASH_DIR);
            if (files == null || files.Length == 0)
            {
                return;
            }
            string dtStr = DateTime.Now.AddDays(-CRASH_FILE_RETAIN_DAYS).ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";
            foreach (string file in files)
            {
                if (new FileInfo(file).Name.CompareTo(dtStr) <= 0)
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// 获取当前要输出的崩溃日志文件全路径
        /// </summary>
        /// <returns></returns>
        private string getCrashFilePath()
        {
            if (!Directory.Exists(CRASH_DIR))
            {
                Directory.CreateDirectory(CRASH_DIR);
            }
            FileInfo fileInfo = new FileInfo(string.Format($"{CRASH_DIR}/{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.txt"));
            return fileInfo.FullName;
        }

        /// <summary>
        /// 将文本内容追加到指定文件末尾
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        /// <param name="text">文本内容</param>
        private void saveTextToLocal(string filePath, string text)
        {
            try
            {
                FileMode fileMode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;
                using (var stream = new FileStream(filePath, fileMode, FileAccess.Write,
                    FileShare.None, 4096, FileOptions.None))
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        
    }

}
