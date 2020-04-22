using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.DeviceType;
using System.Threading;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime
{
    internal class DefaultPurgePrime : IPurgePrimable
    {
        public Result DoPurgeAndPrime(Valve valve)
        {
            if (valve is JtValve)
            {
                JtValve jtValve = (JtValve)valve;
                return this.JtValveLogic(jtValve);
            }
            else
            {
                return this.SvAndGearValveLogic(valve);
            }
        }

        private Result JtValveLogic(JtValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            Result result = Result.OK;

            // 需要重置胶阀状态
            if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
            {
                result = valve.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
                if (!result.IsOk)
                {
                    return result;
                }
            }

            //排胶
            result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            valve.SprayCycleAndWait((short)valve.Prm.PrimeDotsCount);

            //清洗
            PointD purgeCenter = Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(valve.ValveType);
            double r = valve.Prm.PrePurgeRadius;
            double rHalf = r * 0.5;
            double rHalfSqrt3 = r * Math.Sqrt(3) * 0.5;
            PointD[] pts = new PointD[6]
            {
                new PointD(purgeCenter.X + r, purgeCenter.Y),
                new PointD(purgeCenter.X + rHalf, purgeCenter.Y + rHalfSqrt3),
                new PointD(purgeCenter.X - rHalf, purgeCenter.Y + rHalfSqrt3),
                new PointD(purgeCenter.X - r, purgeCenter.Y),
                new PointD(purgeCenter.X - rHalf, purgeCenter.Y - rHalfSqrt3),
                new PointD(purgeCenter.X + rHalf, purgeCenter.Y - rHalfSqrt3)
            };
            result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PurgeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            if (valve.Prm.EnablePrePurgeVaccum)
            {
                DoType.真空清洗.Set(true);
            }
            for (int i = 0; i < valve.Prm.PrePurgeCount; i++)
            {
                foreach (var pt in pts)
                {
                    result = Machine.Instance.Robot.MovePosXYAndReply(pt,valve.Prm.PrePurgerSpeed);
                    if (result == Result.FAILED)
                    {
                        return result;
                    }
                }
            }
            result = Machine.Instance.Robot.MovePosXYAndReply(Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            if (valve.Prm.EnablePrePurgeVaccum)
            {
                DoType.真空清洗.Set(false);
            }

            //排胶
            result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PrimeLoc.ToNeedle(valve.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            valve.SprayCycleAndWait((short)valve.Prm.PrimeDotsCount);

            //擦纸带
            if (valve.Prm.ScrapeEnable)
            {
                result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.ScrapeLoc.ToNeedle(valve.ValveType));
                if (result == Result.FAILED)
                {
                    return result;
                }
                DoType.擦纸带.Set(true);
                //保持一定时间
                Thread.Sleep(TimeSpan.FromSeconds(valve.Prm.ScrapeTime));
                DoType.擦纸带.Set(false);
            }

            return Machine.Instance.Robot.MoveSafeZAndReply();
        }

        private Result SvAndGearValveLogic(Valve valve)
        {
            Result ret = Result.OK;
            ret = valve.DoPrime();
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = valve.DoPurge();
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = valve.DoPrime();

            return ret;
        }
    }
}
