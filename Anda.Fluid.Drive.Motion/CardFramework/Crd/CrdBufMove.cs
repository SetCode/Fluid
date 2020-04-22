using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdBufMove : ICrdable
    {
        public CrdType Type => CrdType.BufMove;

        public Axis Axis { get; set; }

        public double Pos { get; set; }

        public double Vel { get; set; }

        public double Acc { get; set; }

        /// <summary>
        /// 0：非模态，1：模态
        /// </summary>
        public int Mode { get; set; } = 1;
    }
}
