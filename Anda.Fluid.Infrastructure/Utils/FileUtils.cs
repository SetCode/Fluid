using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// 将字符串内容保存到指定文件中（覆盖文件中原来的内容），如果文件不存在，则新建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void SaveText(string filePath, string text)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write,
                    FileShare.None, 4096, FileOptions.None))
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(text);
                }
            }
            catch (Exception e)
            {
                Log.Print(e);
            }
        }

        /// <summary>
        /// 将字符串内容追加到指定文件中，如果文件不存在，则新建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void AppendText(string filePath, string text)
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
            catch (Exception e)
            {
                Log.Print(e);
            }
        }

        public static void AppendTextln(string filePath, string text)
        {
            AppendText(filePath, string.Format($"{text}\r\n"));
        }

        /// <summary>
        /// 格式化文件路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="extension">扩展名</param>
        /// <returns></returns>
        public static string FormatFilePath(string filePath, string extension)
        {
            try
            {
                string dirName = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileName(filePath);
                int index = fileName.LastIndexOf('.');
                if (index > 0)
                {
                    if (fileName.Substring(index, 4) == "." + extension)
                    {
                        fileName = fileName.Substring(0, fileName.Length - 4);
                    }
                }
                return dirName + "\\" + fileName + "." + extension;
            }
            catch
            {
                return filePath + "." + extension;
            }
        }

        public static bool ReadLines(string filePath, out string[] outText)
        {
            outText = null;
            try
            {

                StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("gb2312"));
                string line;
                int nullCount = 0;
                List<string> lines = new List<string>();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Length == 0)
                    {
                        nullCount++;
                    }
                    lines.Add(line);

                }
                outText = lines.ToArray();
                sr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
