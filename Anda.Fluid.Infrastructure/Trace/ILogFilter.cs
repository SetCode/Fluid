using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    interface ILogFilter
    {
        bool Filt(LoggingEvent e);
    }
}
