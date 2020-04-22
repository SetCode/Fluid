using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.SvValveTracker
{
    public class SvValveArc : ArcTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            //TODO螺杆阀暂时没有该功能
            return Result.OK;
        }

        public override Result DryExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            return this.SvValveWetAndDryLogic(arc);
        }

        public override Result InspectDotExecute(Directive directive)
        {
            //TODO螺杆阀暂时没有该功能
            return Result.OK;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            //TODO螺杆阀暂时没有该功能
            return Result.OK;
        }

        public override Result LookExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            return this.SvValveCVLogic(arc);
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            //TODO螺杆阀暂时没有该功能
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            return this.SvValveWetAndDryLogic(arc);
        }

        private Result SvValveCVLogic(Arc arc)
        {
            Result ret = Result.OK;

            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = Machine.Instance.Robot.MovePosXYAndReply(arc.Start,
                FluidProgram.Current.MotionSettings.VelXY,
                FluidProgram.Current.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }
            Log.Print("Arc start position in LookMode: " + arc.Start.ToString());
            Thread.Sleep(TimeSpan.FromSeconds(arc.Param.PreMoveDelay));

            ret = Machine.Instance.Robot.MoveArcAndReply(arc.End, arc.Center, arc.ClockWise, arc.Param.Speed,
                FluidProgram.Current.MotionSettings.AccXY);

            return ret;
        }

        private Result SvValveWetAndDryLogic(Arc arc)
        {
            Result ret = Result.OK;

            //主阀的相关点
            PointD start = arc.Start.ToNeedle(arc.Valve);
            PointD end = arc.End.ToNeedle(arc.Valve);
            PointD center = arc.Center.ToNeedle(arc.Valve);

            //副阀的起点
            PointD simulStart = this.GetSimulPos(arc,start);

            SvValveFludLineParam arcParam = this.GetSvValveArcParam(arc,start, end, center);

            double currZ = Machine.Instance.Robot.PosZ;
            double targZ = 0;
            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = arc.Program.RuntimeSettings.BoardZValue + arc.Param.DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(arc.Param.DispenseGap, arc.CurMeasureHeightValue);
            }

            ret = this.SvValveMoveToArcStart(arc,start, simulStart, currZ, targZ);
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStartSpary(arc,arcParam, center);
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStopSpary(arc);
            if (!ret.IsOk)
            {
                return ret;
            }

            return ret;
        }

        /// <summary>
        /// 移动到圆弧的起点
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="currZ"></param>
        /// <param name="targZ"></param>
        /// <returns></returns>
        private Result SvValveMoveToArcStart(Arc arc,PointD pos, PointD simulStart, double currZ, double targZ)
        {
            Result ret = Result.OK;

            if (currZ > targZ)
            {
                if (arc.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    // 移动到指定位置
                    Log.Dprint("move to Arc Start position XY : " + pos + simulStart);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulStart,
                        FluidProgram.Current.MotionSettings.VelXYAB,
                        FluidProgram.Current.MotionSettings.AccXYAB, 
                        (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    // 移动到指定位置
                    Log.Dprint("move to Arc Start position XYAV : " + pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                }

                if (!ret.IsOk)
                {
                    return ret;
                }
                double z = 0;
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    z = targZ;
                }
                else
                {
                    z = Converter.NeedleBoard2Z(arc.Param.DispenseGap, arc.CurMeasureHeightValue);
                }
                Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + arc.Param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, arc.Param.DownSpeed, arc.Param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            else
            {
                double z = 0;
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    z = targZ;
                }
                else
                {
                    z = Converter.NeedleBoard2Z(arc.Param.DispenseGap, arc.CurMeasureHeightValue);
                }

                Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + arc.Param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, arc.Param.DownSpeed, arc.Param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }

                if (arc.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    // 移动到指定位置
                    Log.Dprint("move to Arc Start position XY : " + pos + simulStart);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulStart,
                      FluidProgram.Current.MotionSettings.VelXYAB,
                      FluidProgram.Current.MotionSettings.AccXYAB,
                      (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    // 移动到指定位置
                    Log.Dprint("move to Arc Start position XYAV : " + pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                       FluidProgram.Current.MotionSettings.VelXY,
                       FluidProgram.Current.MotionSettings.AccXY);
                }

                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// 开始走圆弧轨迹并出胶
        /// </summary>
        /// <param name="end"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private Result SvValveStartSpary(Arc arc,SvValveFludLineParam arcParam, PointD center)
        {
            Result ret = Result.OK;
            if (arc.RunnableModule.Mode == ModuleMode.MainMode && Machine.Instance.Setting.DualValveMode == DualValveMode.异步)
            {
                ret = Machine.Instance.DualValve.FluidArc(arcParam, center, arc.ClockWise, FluidProgram.Current.MotionSettings.WeightAcc);
            }
            else
            {
                ret = Machine.Instance.Valve1.FluidArc(arcParam, center, arc.ClockWise, FluidProgram.Current.MotionSettings.WeightAcc);
            }
            if (!ret.IsOk)
            {
                return ret;
            }
            return ret;
        }

        private Result SvValveStopSpary(Arc arc)
        {
            Result ret = Result.OK;

            // 抬高一段距离 Retract Distance
            if (arc.Param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + arc.Param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(arc.Param.RetractDistance, arc.Param.RetractSpeed, arc.Param.RetractAccel);
            }

            if (arc.RunnableModule.Mode == ModuleMode.MainMode)
            {
                ret = Machine.Instance.DualValve.SuckBack(arc.Param.SuckBackTime);
            }
            else
            {
                ret = Machine.Instance.Valve1.SuckBack(arc.Param.SuckBackTime);
            }
            if (!ret.IsOk)
            {
                return ret;
            }
            return ret;
        }

        private SvValveFludLineParam GetSvValveArcParam(Arc arc,PointD start, PointD end, PointD center)
        {
            double startPosDelay = arc.Param.PreMoveDelay;
            PointD endPoint = end;
            PointD[] transPoints = new PointD[0];

            //计算以起点为基准的关胶距离
            double shutOffDistance = Math.Abs(arc.Length - arc.Param.ShutOffDistance);
            double shutOffRatio = shutOffDistance / arc.Length;
            double shutOffDegree = arc.Degree * shutOffRatio;
            PointD stopSprayPos = MathUtils.CalculateMiddleAndEndPoint(start, center, shutOffDegree)[1];

            //计算回走时的终点距离
            double backTrackDistance = arc.Length * (1 - arc.Param.BacktrackDistance * 0.01);
            double backTrackRatio = backTrackDistance / arc.Length;
            double backTrackDegree = arc.Degree * backTrackRatio;
            PointD backTrackPos = MathUtils.CalculateMiddleAndEndPoint(start, center, backTrackDegree)[1];

            PointD[] backTransPoints = new PointD[0];

            //计算线速度，分重量线和普通线
            double[] vel = new double[1];
            if (arc.IsWeightControl)
            {
                double lineWeight = arc.Weight;
                double flowVel;
                FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(FluidProgram.Current.RuntimeSettings.SvOrGearValveCurrSpeed, out flowVel);

                double time = lineWeight * flowVel;

                vel[0] = arc.Length / time;
            }
            else
            {
                vel[0] = arc.Param.Speed;
            }

            double backTrackDelay = arc.Param.Dwell;
            double backTrackGap = arc.Param.BacktrackGap;
            double backTrackVel = arc.Param.BacktrackSpeed;

            return new SvValveFludLineParam()
            {
                startPosDelay = startPosDelay,
                endPos = endPoint,
                transPoints = transPoints,
                stopSprayPos = stopSprayPos,
                backTrackPos = backTrackPos,
                backTransPoints = backTransPoints,
                vels = vel,
                backTrackDelay = backTrackDelay,
                backTrackGap = backTrackGap,
                backTrackVel = backTrackVel
            };

        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private PointD GetSimulPos(Arc arc,PointD pos)
        {
            PointD simulPos = new PointD();
            ///生成副阀相关参数(起点、插补点位)
            if (arc.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - arc.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -Math.Abs(simulPos.Y) / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(arc.Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(arc.Program.RuntimeSettings.SimulOffsetX, arc.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }
    }
}
