using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Calib
{
    public class CalibMap
    {
        [DllImport("calibrationmap_x86.dll", EntryPoint = "creatNewClbMap", CallingConvention = CallingConvention.Cdecl)]
        public static extern int creatNewClbMap(int lengthnum, int widthnum, double sidewidth);

        [DllImport("calibrationmap_x86.dll", EntryPoint = "addPointData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int addPointData(double xreal, double yreal, double xmach, double ymach, int indX, int indY);

        /// <summary>
        /// 输入：期望位置，输出：机械位置
        /// </summary>
        /// <param name="xreal"></param>
        /// <param name="yreal"></param>
        /// <param name="xmach"></param>
        /// <param name="ymach"></param>
        /// <returns></returns>
        [DllImport("calibrationmap_x86.dll", EntryPoint = "mapToMach", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mapToMach(double xreal, double yreal, ref double xmach, ref double ymach);

        /// <summary>
        /// 输入：机械位置，输出：期望位置
        /// </summary>
        /// <param name="xmach"></param>
        /// <param name="ymach"></param>
        /// <param name="xreal"></param>
        /// <param name="yreal"></param>
        /// <returns></returns>
        [DllImport("calibrationmap_x86.dll", EntryPoint = "mapToReal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mapToReal(double xmach, double ymach, ref double xreal, ref double yreal);

        [DllImport("calibrationmap_x86.dll", EntryPoint = "isCompleted", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool isCompleted();

        [DllImport("circleCapturerx86.dll", EntryPoint = "excuteCaliper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool excuteCaliper(byte[] imageData, int imageWidth, int imageHeight, ref double outX, ref double outY, ref double outR);
    }
}
