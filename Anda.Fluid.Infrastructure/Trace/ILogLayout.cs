using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    interface ILogLayout
    {
        string Layout(LoggingEvent e);
    }
}
