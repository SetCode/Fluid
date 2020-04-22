using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    public enum LogCategory
    {         
        CODE = 1,
        ALARM = 2,
        LOADING = 4,
        MANUAL = 8,
        SETTING = 16,
        RUNNING = 32,
        ALL = 63
    }
}
