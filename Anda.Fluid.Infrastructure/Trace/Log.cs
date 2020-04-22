using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Trace
{
    public class Log
    {
        private static readonly FileLog fileLog = new FileLog();

        public static void init()
        {
            setUnhandledExceptionHandler();
        }

        private static void setUnhandledExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += (sender, e) =>
            {
                onCrash(e.Exception);
            };
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                onCrash((Exception)e.ExceptionObject);
            };
        }

        private static void onCrash(Exception e)
        {
            if (e == null)
            {
                return;
            }
            Type t = e.GetType();
            Debug.WriteLine(e.ToString());
            fileLog.printOnCrash(e);
            //MessageBox.Show(e.ToString());
        }

        public static void Print(string msg)
        {
            Print(null, msg);
        }

        public static void Print(string tag, string msg)
        {
            string logMsg = buildString(tag, msg);
            Debug.WriteLine(logMsg);
            fileLog.print(logMsg);
        }

        /// <summary>
        /// 只在Debug模式下输出日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Dprint(string msg)
        {
#if DEBUG
            Print(null, msg);
#endif
        }

        /// <summary>
        /// 只在Debug模式下输出日志
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="msg"></param>
        public static void Dprint(string tag, string msg)
        {
#if DEBUG
            Print(tag, msg);
#endif
        }

        public static void Print(Exception e)
        {
            Print(null, e);
        }

        public static void Print(string tag, Exception e)
        {
            if (e == null)
            {
                return;
            }
            Print(tag, "Exception occured: \r\n" + e.ToString());
        }

        private static string buildString(string tag, string msg)
        {
            string timeStr = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            StringBuilder sb = new StringBuilder();
            sb.Append('[').Append(timeStr);
            if (tag != null)
            {
                sb.Append(" / ").Append(tag);
            }
            sb.Append("] ").Append(msg).Append("\r\n");
            return sb.ToString();
        }

        public static void Dispose()
        {
            fileLog.Dispose();
        }
    }
}
