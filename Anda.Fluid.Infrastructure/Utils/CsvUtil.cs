using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Utils
{
    public static class CsvUtil
    {
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool WriteLine(string filePath, string line)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, true,Encoding.GetEncoding("gb2312"));
                sw.WriteLine(line);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
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
