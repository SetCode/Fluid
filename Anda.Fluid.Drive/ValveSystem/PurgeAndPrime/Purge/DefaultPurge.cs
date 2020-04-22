using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.ValveSystem.Series;
using System.Threading;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge
{
    internal class DefaultPurge : IPurgable
    {
        public Result DoPurge(Valve valve)
        {
            if (valve is JtValve)
            {
                JtValve jtValve = (JtValve)valve;
                return this.JtValveDoPurge(jtValve);
            }
            else if (valve is SvValve)
            {
                SvValve svValve = (SvValve)valve;
                return this.SvValveDoPurge(svValve);
            }
            else if (valve is GearValve)
            {
                GearValve gearValve = (GearValve)valve;
                return this.GearValveDoPurge(gearValve);
            }
            else
                return Result.FAILED;
        }

        private Result JtValveDoPurge(JtValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Result result = Result.OK;
            // 手动清洗需要重置胶阀状态
            if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
            {
                result = valve.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
                if (!result.IsOk)
                {
                    return result;
                }
            }
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do purge start");
            result = Result.OK;
            PointD purgeCentor = Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(valve.ValveType);
            double r = valve.Prm.PrePurgeRadius;
            double rHalf = r * 0.5;
            double rHalfSqrt3 = r * Math.Sqrt(3) * 0.5;
            PointD[] pts = new PointD[]
            {
                new PointD(purgeCentor.X+r,purgeCentor.Y),
                new PointD(purgeCentor.X+rHalf,purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-rHalf, purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-r, purgeCentor.Y),
                new PointD(purgeCentor.X-rHalf,purgeCentor.Y-rHalfSqrt3),
                new PointD(purgeCentor.X+rHalf, purgeCentor.Y-rHalfSqrt3)
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
                foreach (PointD pt in pts)
                {
                    Machine.Instance.Robot.MovePosXYAndReply(pt,valve.Prm.PrePurgerSpeed);
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
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do purge end");
            return Machine.Instance.Robot.MoveSafeZAndReply();
        }

        private Result SvValveDoPurge(SvValve valve)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Result result = Result.OK;
            PointD purgeCentor = Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(valve.ValveType);
            double r = valve.Prm.PrePurgeRadius;
            double rHalf = r * 0.5;
            double rHalfSqrt3 = r * Math.Sqrt(3) * 0.5;
            PointD[] pts = new PointD[]
            {
                new PointD(purgeCentor.X+r,purgeCentor.Y),
                new PointD(purgeCentor.X+rHalf,purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-rHalf, purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-r, purgeCentor.Y),
                new PointD(purgeCentor.X-rHalf,purgeCentor.Y-rHalfSqrt3),
                new PointD(purgeCentor.X+rHalf, purgeCentor.Y-rHalfSqrt3)
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

            Thread.Sleep(TimeSpan.FromSeconds(valve.Prm.PrePurgeDelay));
            valve.Spraying();

            for (int i = 0; i < valve.Prm.PrePurgeCount; i++)
            {
                foreach (PointD pt in pts)
                {
                    Machine.Instance.Robot.MovePosXYAndReply(pt);
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

            valve.SprayOff();
            return Machine.Instance.Robot.MoveSafeZAndReply();
        }

        private Result GearValveDoPurge(GearValve vavle)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Result result = Result.OK;
            PointD purgeCentor = Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(vavle.ValveType);
            double r = vavle.Prm.PrePurgeRadius;
            double rHalf = r * 0.5;
            double rHalfSqrt3 = r * Math.Sqrt(3) * 0.5;
            PointD[] pts = new PointD[]
            {
                new PointD(purgeCentor.X+r,purgeCentor.Y),
                new PointD(purgeCentor.X+rHalf,purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-rHalf, purgeCentor.Y+rHalfSqrt3),
                new PointD(purgeCentor.X-r, purgeCentor.Y),
                new PointD(purgeCentor.X-rHalf,purgeCentor.Y-rHalfSqrt3),
                new PointD(purgeCentor.X+rHalf, purgeCentor.Y-rHalfSqrt3)
            };

            result = Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.PurgeLoc.ToNeedle(vavle.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            if (vavle.Prm.EnablePrePurgeVaccum)
            {
                DoType.真空清洗.Set(true);
            }

            Thread.Sleep(TimeSpan.FromSeconds(vavle.Prm.PrePurgeDelay));
            vavle.Spraying();

            for (int i = 0; i < vavle.Prm.PrePurgeCount; i++)
            {
                foreach (PointD pt in pts)
                {
                    Machine.Instance.Robot.MovePosXYAndReply(pt);
                }
            }
            result = Machine.Instance.Robot.MovePosXYAndReply(Machine.Instance.Robot.CalibPrm.PurgeLoc.ToNeedle(vavle.ValveType));
            if (result == Result.FAILED)
            {
                return result;
            }
            if (vavle.Prm.EnablePrePurgeVaccum)
            {
                DoType.真空清洗.Set(false);
            }

            vavle.SprayOff();
            return Machine.Instance.Robot.MoveSafeZAndReply();
        }
    }
}
