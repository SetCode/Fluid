using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.GenKey
{
    public class GenKeySN
    {
        [DllImport("Encryption.dll", EntryPoint = "DoDecrypt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DoDecrypt(ref int iyear, ref int iyday, ref int imon, ref int imday, ref int ispan);

        public static bool CheckDate()
        {
            int iyear = -1, iyday = -1, imon = -1, imday = -1, ispan = -1;
            bool b = DoDecrypt(ref iyear, ref iyday, ref imon, ref imday, ref ispan);
            if (!b)
            {
                return false;
            }
            if(iyear < 0 || iyday <0 || imon <0 || imday < 0 || ispan < 0)
            {
                return false;
            }
            iyear += 1900;
            DateTime regday = new DateTime(iyear, imon, imday);
            TimeSpan regspan = TimeSpan.FromDays(ispan * 30);
            DateTime today = DateTime.Today;
            if (today < regday)
            {
                return false;
            }
            if(today - regday > regspan)
            {
                return false;
            }
            return true;
        }
    }
}
