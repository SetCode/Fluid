using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Utils
{

    public class StringUtil
    {
        ///<summary>
        /// Description	:用于返回指定字符串的指定部分
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public static string GetSubString(string inputString, int startPos, int endPos)
        {
            if (startPos < 0)
            {
                startPos = 0;
            }
            if (endPos > inputString.Length)
            {
                endPos = inputString.Length - 1;
            }
            string str = inputString.Substring(startPos,endPos-startPos);
            return str;
        }
    }
}
