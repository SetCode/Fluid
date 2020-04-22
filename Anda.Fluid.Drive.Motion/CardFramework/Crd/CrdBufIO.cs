using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    public class CrdBufIO : ICrdable
    {
        public int Mask { get; set; }
        public int Value { get; set; }

        public CrdType Type { get { return CrdType.BufIO; } }
    }
}
