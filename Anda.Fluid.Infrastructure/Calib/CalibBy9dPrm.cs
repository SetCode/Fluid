using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Calib
{
    public class CalibBy9dPrm
    {
        public List<double> PhyPtSet { get; private set; } = new List<double>();

        public List<double> ImgPtSet { get; private set; } = new List<double>();

        public int Num { get; set; }

        public double Angle { get; set; }

        public double Scale { get; set; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        public CalibBy9dPrm Default()
        {
            this.PhyPtSet.Clear();
            this.ImgPtSet.Clear();
            this.PhyPtSet.AddRange(new double[] { 0, 0, 0, 2, 2, 2, 2, 0, 2, -2, 0, -2, -2, -2, -2, 0, -2, 2 });
            this.ImgPtSet.AddRange(new double[] { 640, 480, 640, 824, 297, 823, 298, 479, 299, 135, 643, 136, 986, 137, 985, 481, 984, 825 });
            this.Num = 9;
            return this;
        }

        public int Update()
        {
            if (this.Num < 3 
                || this.PhyPtSet.Count < this.Num * 2
                || this.ImgPtSet.Count < this.Num * 2)
            {
                return -1;
            }

            int rtn = CalibBy9d.Calibrate(this.PhyPtSet.ToArray(), this.Num, this.ImgPtSet.ToArray());
            if (rtn != 0) return rtn;

            double angle = 0, scale = 1, x = 0, y = 0;
            rtn = CalibBy9d.GetAngle(ref angle);
            if (rtn != 0) return rtn;
            rtn = CalibBy9d.GetScale(ref scale);
            if (rtn != 0) return rtn;
            rtn = CalibBy9d.GetOffset(ref x, ref y);
            if (rtn != 0) return rtn;

            this.Angle = angle;
            this.Scale = scale;
            this.OffsetX = x;
            this.OffsetY = y;

            return 0;
        }
    }
}
