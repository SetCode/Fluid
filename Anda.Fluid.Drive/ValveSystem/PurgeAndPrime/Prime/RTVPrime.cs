using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using System.Threading;
using Anda.Fluid.Drive.ValveSystem.Series;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime
{
    internal class RTVPrime : IPrimable
    {
        public Result DoPrime(Valve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            SvValve svValve = (SvValve)valve;

            //排胶
            Result result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc);
            if (result == Result.FAILED)
            {
                return result;
            }

            valve.Spraying();
            Thread.Sleep(svValve.Prm.PrimeTime);
            valve.SprayOff();

            return Machine.Instance.Robot.MoveSafeZAndReply();
        }
    }
}
