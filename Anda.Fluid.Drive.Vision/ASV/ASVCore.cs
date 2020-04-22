using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ASV
{
    public class ASVCore
    {
        [DllImport("ASVCore.dll", EntryPoint = "LoadFrom", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LoadFrom(string path);

        [DllImport("ASVCore.dll", EntryPoint = "InitiateAsv_Csharp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitiateAsv(bool useChinese = true);

        [DllImport("ASVCore.dll", EntryPoint = "CloseAsv", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseAsv();

        [DllImport("ASVCore.dll", EntryPoint = "CreatInspection", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreatInspection(int key = -1);

        [DllImport("ASVCore.dll", EntryPoint = "DeleteInspection", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteInspection(int inspectionKey);

        [DllImport("ASVCore.dll", EntryPoint = "ShowEditWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ShowEditWindow(int inspectionKey);

        [DllImport("ASVCore.dll", EntryPoint = "GetAllInspectionKey_Csharp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAllInspectionKey(int[] keys, int count);

        [DllImport("ASVCore.dll", EntryPoint = "SetImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetImage(int inspectionKey, byte[] ImageData, int ImageHeight, int ImageWide, bool isRGB = false);

        [DllImport("ASVCore.dll", EntryPoint = "Excute_Csharp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Excute(int inspectionKey, byte[] ImageData, int ImageHeight, int ImageWide, byte[] result, bool isRGB = false);

        [DllImport("ASVCore.dll", EntryPoint = "GetResultImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetResultImage(int inspectionKey, char[] ImageData, int ImageHeight, int ImageWide);
    }
}
