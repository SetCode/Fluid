using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Prm
{
    public class PreSitePrm : ICloneable
    {
        //不需要人工处理的轨道三站轨道检查，根据需求，现在不需要了
        //public int CheckTime { get; set; } = 20000;

        public int StuckTime { get; set; } = 20000;
        public int BoardArrivedDelay { get; set; } = 1000;
        public long HeatingTime { get; set; } = 0;
        public object Clone()
        {
            return (PreSitePrm)this.MemberwiseClone();
        }

    }
    public class WorkingSitePrm : ICloneable
    {
        //不需要人工处理的轨道三站轨道检查，根据需求，现在不需要了
        //public int CheckTime { get; set; } = 20000;

        public int EnterStuckTime { get; set; } = 20000;
        public int ExitStuckTime { get; set; } = 20000;
        public int BoardArrivedDelay { get; set; } = 1000;
        public long HeatingTime { get; set; } = 0;
        public object Clone()
        {
            return (WorkingSitePrm)this.MemberwiseClone();
        }
    }

    public class FinishedSitePrm : ICloneable
    {
        //不需要人工处理的轨道三站轨道检查，根据需求，现在不需要了
        //public int CheckTime { get; set; } = 20000;

        public int StuckTime { get; set; } = 20000;
        public int BoardArrivedDelay { get; set; } = 1000;
        public long HeatingTime { get; set; } = 0;
        public object Clone()
        {
            return (FinishedSitePrm)this.MemberwiseClone();
        }
    }
}
