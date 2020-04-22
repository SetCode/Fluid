using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Motion.ActiveItems;
using System.Threading;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime
{
    internal class DefaultPrime : IPrimable
    {

        public Result DoPrime(Valve valve)
        {
            if (valve is JtValve)
            {
                JtValve jtValve = (JtValve)valve;
                return this.JtValveDoPrime(jtValve);
            }
            else if (valve is SvValve)
            {
                SvValve svValve = (SvValve)valve;
                return this.SvValveDoPrime(svValve);
            }
            else if (valve is GearValve)
            {
                GearValve gearValve = (GearValve)valve;
                return this.GearValveDpPrime(gearValve);
            }
            else
                return Result.FAILED;
        }

        private Result JtValveDoPrime(JtValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Result result = Result.OK;
            // 手动吐液需要重置胶阀状态
            if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
            {
                result = valve.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
                if (!result.IsOk)
                {
                    return result;
                }
            }
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do prime start");
            //排胶
            result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            valve.SprayCycleAndWait((short)valve.Prm.PrimeDotsCount);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do prime end");
            return Machine.Instance.Robot.MoveSafeZAndReply();
        }

        private Result SvValveDoPrime(SvValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            //排胶
            Result result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }

            valve.Spraying();
            Thread.Sleep(valve.Prm.PrimeTime);
            valve.SprayOff();

            return Machine.Instance.Robot.MoveSafeZAndReply();
        }

        private Result GearValveDpPrime(GearValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            //排胶
            Result result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            valve.Spraying();
            Thread.Sleep(valve.Prm.PrimeTime);
            valve.SprayOff();

            return Machine.Instance.Robot.MoveSafeZAndReply();
        }
    }
}
