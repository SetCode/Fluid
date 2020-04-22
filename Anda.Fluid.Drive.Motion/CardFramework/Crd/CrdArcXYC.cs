using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdArcXYC : ICrdable
    {
        public double EndPosX { get; set; }
        public double EndPosY { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        /// <summary>
        /// 0：顺时针
        /// 1：逆时针
        /// </summary>
        public short Clockwise { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double VelEnd { get; set; }

        public CrdType Type { get { return CrdType.ArcXYC; } }
    }
}
