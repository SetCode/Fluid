using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.GrayFind
{
    public class GrayCheckThm
    {
        [DllImport("GrayCheck.dll", EntryPoint = "Check", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Check(byte[] modelData, int modelWidth, int modelHeight,
            byte[] checkData, int checkWidth, int checkHeight, int toleracne);
    }
}
