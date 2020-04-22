using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge
{
    internal interface IPurgable
    {
        Result DoPurge(Valve valve);
    }
}
