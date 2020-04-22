using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Calib
{
    public class CalibMapPrm
    {
        public int RowNum { get; set; }
        public int ColNum { get; set; }
        public double Interval { get; set; }
        public List<MapPoint> Items { get; set; } = new List<MapPoint>();
        public List<MapPoint> VerifyItems { get; set; } = new List<MapPoint>();
    }

    public class MapPoint
    {
        public double RealX { get; set; }
        public double RealY { get; set; }
        public double RobotX { get; set; }
        public double RobotY { get; set; }
        public int IndexX { get; set; }
        public int IndexY { get; set; }
        public double R { get; set; }
        public bool Ok { get; set; }

        public override string ToString()
        {
            return string.Format("{0}[{1},{2}]:R{3},Delta({4},{5})",
                Ok, IndexX, IndexY, Math.Round(R, 3), Math.Round(RealX - RobotX, 3), Math.Round(RealY - RobotY, 3));
        }
    }
}
