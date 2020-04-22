using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Prm
{
    public class RTVPrm : ICloneable
    {
        /// <summary>
        /// 气缸IO是否启用
        /// </summary>
        public bool IOEnable { get; set; } = false;

        /// <summary>
        /// 气缸电眼无法感应判定时长
        /// </summary>
        public int IOStuckTime { get; set; } = 10;

        /// <summary>
        /// 上轨道出板后轨道转动时长
        /// </summary>
        public int UpConveyorTurnTime { get; set; } = 0;

        /// <summary>
        /// 下轨道出板后轨道转动时长
        /// </summary>
        public int DownConveyorTurnTime { get; set; } = 0;

        public object Clone()
        {
            return (RTVPrm)this.MemberwiseClone();
        }
    }
}
