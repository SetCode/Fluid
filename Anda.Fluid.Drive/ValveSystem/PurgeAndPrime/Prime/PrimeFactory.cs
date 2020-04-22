using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime
{
    internal static class PrimeFactory
    {
        public static IPrimable GetIPrimable()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                return new RTVPrime();
            else
                return new DefaultPrime();
        }
    }
}
