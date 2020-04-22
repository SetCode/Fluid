using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdXYGear : ICrdable
    {
        /// <summary>
        /// 跟随轴号
        /// </summary>
        public Axis GearAxis { get; set; }
        /// <summary>
        /// 相对位移量
        /// </summary>
        public int DeltaPulse { get; set; }
        public CrdType Type { get { return CrdType.XYGear; } }
    }
}
