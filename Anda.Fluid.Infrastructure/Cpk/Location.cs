using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Cpk
{
    
    public class Location
    {
        private const int LETAOFFSET = 65;
        public int ERowIndex;
        public string EColIndex;

        
        public int NPRowIndex;

        public int NPColIndex;

        public string LocRC;

        public Location(int eRowIndex, string eColIndex)
        {
            this.ERowIndex = eRowIndex;
            this.EColIndex = eColIndex;
        }
        public Location(int npRowIndex, int npColIndex)
        {
            this.NPRowIndex = npRowIndex;
            this.NPColIndex = npColIndex;
        }

        public Location()
        { }
        /// <summary>
        /// 将Excel的行（1开始）列（A开始） 转换为NPOI的行列（从0开始）
        /// </summary>
        public void ExlIndexToNpIndex()
        {
            Sheet.ExlIndexToNpIndex(ERowIndex, EColIndex, out NPRowIndex, out NPColIndex);
        }

        /// <summary>
        ///  将NPOI的行列（从0开始）转换为Excel的行（1开始）列（A开始）
        /// </summary>
        /// <returns></returns>
        public string NpIndexToExlIndex()
        {
            return NpIndexToExlIndex(NPRowIndex, NPColIndex, out ERowIndex, out EColIndex);
        }
        
        //0(colIndex)-A-65(ASCII)
        public  static string NpIndexToExlIndex(int nprowIndex, int npcolIndex,out int erowIndex, out string ecolIndex)
        {

             ecolIndex = IntToAsc2(npcolIndex);

             erowIndex = nprowIndex+1;

            return ecolIndex + erowIndex.ToString();

        }
        public static string GetRC(int nprowIndex, int npcolIndex)
        {
            int erowIndex;
            string ecolIndex;
            return NpIndexToExlIndex(nprowIndex, npcolIndex, out erowIndex, out ecolIndex);
        }

        public  static string IntToAsc2(int index)
        {
            int[] arrayOut;
            Analysis(index, out arrayOut);

            StringBuilder sb = new StringBuilder();
            int arrLength = arrayOut.Length;
            if (arrLength > 1)
            {
                arrayOut[0] = arrayOut[0] - 1;
            }
            for (int i = 0; i < arrayOut.Length; i++)
            {
                sb.Append(IntToAsc(arrayOut[i] + LETAOFFSET));
            }
            return sb.ToString();
        }
        //解析出数组
        private static void Analysis(int input, out int[] array)
        {
            double num;
            int count = 0;
            int x = 1;
            
            while (true)
            {
                int numerator = Convert.ToInt32(Math.Pow(26, x));

                num = Convert.ToDouble(input / numerator);
                if (num < 1)
                {
                    break;
                }
                x++;
                count++;
            }

            int[] res = new int[count + 1];
            int Temp = input;
            for (int i = count; i >= 0; i--)
            {
                res[count - i] = Temp / Convert.ToInt32(Math.Pow(26, i));
                Temp -= res[count - i] * Convert.ToInt32(Math.Pow(26, i));
            }
            array = res;
        }

        public  static string IntToAsc(int index)
        {

            byte[] ascBArr = new byte[1];
            ascBArr[0] = (byte)Convert.ToInt32(index);
            string ascStr = Convert.ToString(System.Text.Encoding.ASCII.GetString(ascBArr));
            return ascStr;
        }


    }
}
