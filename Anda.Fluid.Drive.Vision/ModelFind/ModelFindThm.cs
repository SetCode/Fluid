using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ModelFind
{
    public class ModelFindThm
    {
        [DllImport("NCCMatch.dll", EntryPoint = "CreateModel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateModel(byte[] modelData, int modelWidth, int modelHeight, int searchWindowX, int searchWindowY,
             int searchWindowWidth, int searchWindowHeight, ref int modelId);

        [DllImport("NCCMatch.dll", EntryPoint = "GetRoiImageData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRoiImageData(byte[] imageData, int imageWidth, int imageHeight, int roiX, int roiY, int roiWidth,
            int roiHeight, byte[] roiImageData);

        [DllImport("NCCMatch.dll", EntryPoint = "Match", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Match(byte[] imageData, int imageWidth, int imageHeight, int modelId,
            ref double matchScore, ref double matchPointX, ref double matchPointY);
    }
}
