using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Calib
{
    public class CalibBy9d
    {
        [DllImport("Project9dx86.dll", EntryPoint = "CalibrationAlgorithm")]
        public static extern int Calibrate(double[] phyPtSet, int num, double[] imgPtSet);

        [DllImport("Project9dx86.dll", EntryPoint = "ConvertImagePlot2PhysicalPlot")]
        public static extern int ConvertImg2Phy(ref double imgPtX, ref double imgPtY, ref double phyPtX, ref double phyPtY);

        [DllImport("Project9dx86.dll", EntryPoint = "GetAngle")]
        public static extern int GetAngle(ref double angle);

        [DllImport("Project9dx86.dll", EntryPoint = "GetScale")]
        public static extern int GetScale(ref double scale);

        [DllImport("Project9dx86.dll", EntryPoint = "GetOffset")]
        public static extern int GetOffset(ref double x, ref double y);
    }
}
