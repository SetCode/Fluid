using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdLnXYR : ICrdable
    {
        public double EndPosX { get; set; }
        public double EndPosY { get; set; }
        public double EndPosR { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double VelEnd { get; set; }
        public CrdType Type { get { return CrdType.LnXYR; } }
    }
}
