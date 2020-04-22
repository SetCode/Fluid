using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime
{
    internal static class PurgeAndPrimeFactory
    {
        public static IPurgePrimable GetIPurgePrimable()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                return new RTVPurgeAndPrime();
            }
            else
                return new DefaultPurgePrime();
        }
    }
}
