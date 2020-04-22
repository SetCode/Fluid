using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public enum LightChn
    {
        Red = 1,
        Green,
        Blue
    }

    public enum LightVendor
    {
        Anda,
        OPT,
        Custom
    }

    public enum LightType
    {
        None,
        Coax,
        Ring,
        Both
    }
}
