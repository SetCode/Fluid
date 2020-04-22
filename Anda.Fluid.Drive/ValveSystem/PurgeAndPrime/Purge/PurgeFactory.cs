using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge
{
    internal static class PurgeFactory
    {
        public static IPurgable GetIPurgable()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                return new RTVPurge();
            else
                return new DefaultPurge();
        }
    }
}
