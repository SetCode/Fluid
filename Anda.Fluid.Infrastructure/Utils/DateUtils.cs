using System;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class DateUtils
    {
        public static long CurrTimeInMills
        {
            get
            {
                return DateTime.Now.Ticks / 10000;
            }
        }
    }
}
