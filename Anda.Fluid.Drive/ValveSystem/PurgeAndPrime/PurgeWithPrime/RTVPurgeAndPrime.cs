using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime
{
    internal class RTVPurgeAndPrime : IPurgePrimable
    {
        public Result DoPurgeAndPrime(Valve valve)
        {
            Result ret = Result.OK;
            ret = valve.DoPrime();
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = valve.DoPurge();
            return ret;
        }
    }
}
