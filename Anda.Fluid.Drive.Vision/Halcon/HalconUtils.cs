using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace Anda.Fluid.Drive.Vision.Halcon
{
    public static class HalconUtils
    {
        public static HImage ToHImage(this byte[] bytes, int width, int height)
        {
            GCHandle obj = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr intPtr = obj.AddrOfPinnedObject();
            HImage hImage = new HImage();
            hImage.GenImage1("byte", width, height, intPtr);
            return hImage;
        }

        public static byte[] ToBytes(this HImage hImage)
        {
            HTuple tuple, type, width, height;
            tuple = hImage.GetImagePointer1(out type, out width, out height);
            IntPtr[] intPtrs = tuple.ToIPArr();
            byte[] bytes = new byte[width * height];
            Marshal.Copy(intPtrs[0], bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
