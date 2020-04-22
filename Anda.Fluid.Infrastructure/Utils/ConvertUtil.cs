using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class ConvertUtil
    {
        /// <summary>
        /// 负数转十六进制字符串
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        public static string NegativeToHexString(int iNumber)
        {
            string strResult = string.Empty;

            if (iNumber < 0)
            {
                iNumber = -iNumber;

                string strNegate = string.Empty;

                char[] binChar = Convert.ToString(iNumber, 2).PadLeft(16, '0').ToArray();

                foreach (char ch in binChar)
                {
                    if (Convert.ToInt32(ch) == 48)
                    {
                        strNegate += "1";
                    }
                    else
                    {
                        strNegate += "0";
                    }
                }

                int iComplement = Convert.ToInt32(strNegate, 2) + 1;

                strResult = Convert.ToString(iComplement, 16).ToUpper();
            }

            return strResult;
        }
        /// <summary>
        /// 十六进制字符串转负数
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static int HexStringToNegative(string strNumber)
        {
            int iNegate = 0;
            int iNumber = Convert.ToInt32(strNumber, 16);

            if (iNumber > 127)
            {
                int iComplement = iNumber - 1;
                string strNegate = string.Empty;

                char[] binChar = Convert.ToString(iComplement, 2).PadLeft(16, '0').ToArray();

                foreach (char ch in binChar)
                {
                    if (Convert.ToInt32(ch) == 48)
                    {
                        strNegate += "1";
                    }
                    else
                    {
                        strNegate += "0";
                    }
                }

                iNegate = -Convert.ToInt32(strNegate, 2);
            }

            return iNegate;
        }

        public static string ASCII2String(byte asciiCode)
        {
            System.Text.ASCIIEncoding coding = new ASCIIEncoding();
            byte[] bytes = new byte[] { asciiCode };
            return coding.GetString(bytes);
        }
    }
}
