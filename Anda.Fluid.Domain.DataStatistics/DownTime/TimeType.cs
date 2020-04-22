using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public enum WorkState
    {
        Normal,
        Abnormal        
    }   

    public enum TimeType
    {
        All,
        WaitForBoard,
        Spray,
        BreakDown,
        ChangeGlue
    }
}
