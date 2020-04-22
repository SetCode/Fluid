using System;
using System.Runtime.InteropServices;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class MemoryUtils
    {
        /// <summary>
        /// 获取对象的地址
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetAddr(object o)
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);
            IntPtr addr = GCHandle.ToIntPtr(h);
            return "0x" + addr.ToString("X");
        }
    }
}
