using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public enum CrdType
    {
        ArcXYC,
        ArcXYR,
        BufIO,
        Delay,
        LnXY,
        LnXYR,
        LnXYAB,
        ArcXYABC,
        ArcXYABR,
        XYGear,
        BufMove
    }

    public interface ICrdable
    {
        CrdType Type { get; }
    }
}
