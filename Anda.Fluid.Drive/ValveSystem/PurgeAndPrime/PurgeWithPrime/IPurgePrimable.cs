using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime
{
    internal interface IPurgePrimable
    {
        Result DoPurgeAndPrime(Valve valve);
    }
}
