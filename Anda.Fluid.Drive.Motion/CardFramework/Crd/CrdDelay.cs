using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdDelay : ICrdable
    {
        public ushort DelayTime { get; set; }

        public CrdType Type { get { return CrdType.Delay; } }
    }
}
