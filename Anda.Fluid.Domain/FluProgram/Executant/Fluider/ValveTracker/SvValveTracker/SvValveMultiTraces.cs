using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive.Vision.ASV;
using System.Threading;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.SvValveTracker
{
    public class SvValveMultiTraces : LineTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            //TODO 该逻辑暂时没有
            return Result.OK;
        }

        public override Result DryExecute(Directive directive)
        {
            return this.SvValveWetAndDryLogic(directive as MultiTraces);
        }

        public override Result InspectDotExecute(Directive directive)
        {
            //TODO螺杆阀没有此逻辑
            return Result.OK;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            return this.SvValveInspectLogic(directive as MultiTraces);
        }

        public override Result LookExecute(Directive directive)
        {
            return this.SvValveCVLogic(directive as MultiTraces);
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            //TODO螺杆阀没有此逻辑
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            return this.SvValveWetAndDryLogic(directive as MultiTraces);
        }

        /// <summary>
        /// 获取线参数
        /// </summary>
        /// <param name="multiTraces"></param>
        /// <param name="lineStyle"></param>
        /// <returns></returns>
        private LineParam getLineParam(MultiTraces multiTraces, TraceBase trace)
        {
            return multiTraces.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam((LineStyle)trace.LineStyle);
        }

        /// <summary>
        /// 螺杆阀CV模式模式下的运行逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveCVLogic(MultiTraces multiTraces)
        {
            Result ret = Result.OK;

            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            if (multiTraces.Traces.Count == 0)
            {
                return ret;
            }
            // 移动到轨迹起点
            ret = Machine.Instance.Robot.MovePosXYAndReply(multiTraces.Traces[0].Start,
                    FluidProgram.Current.MotionSettings.VelXY,
                    FluidProgram.Current.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }
            SvValveFludLineParam primaryLineParam = this.GetSvValveParam(multiTraces, multiTraces.Traces);
            // 连续插补
            double[] vels = new double[multiTraces.Traces.Count];
            for (int i = 0; i < vels.Length; i++)
            {
                vels[i] = this.getLineParam(multiTraces, multiTraces.Traces[i]).Speed;
            }
            List<ICrdable> crdList = Valve.GetCrdsBy(multiTraces.Traces, vels, FluidProgram.Current.MotionSettings.AccXY);
            ret = Machine.Instance.Robot.MoveTrcXYReply(crdList);
            if (!ret.IsOk)
            {
                return ret;
            }
            List<ICrdable> backCrdList = Valve.GetCrdsBy(primaryLineParam.backTransTraces, primaryLineParam.backTrackVel, FluidProgram.Current.MotionSettings.AccXY);
            return Machine.Instance.Robot.MoveTrcXYReply(backCrdList);
        }

        /// <summary>
        /// 螺杆阀Inspect模式下的运行逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveInspectLogic(MultiTraces multiTraces)
        {
            return Result.OK;
        }

        /// <summary>
        /// 螺杆阀Wet和Dry模式下的运行逻辑
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private Result SvValveWetAndDryLogic(MultiTraces multiTraces)
        {
            Result ret = Result.OK;
            //主阀轨迹
            List<TraceBase> primaryTraces = new List<TraceBase>();
            //副阀轨迹
            List<TraceBase> simulTraces = new List<TraceBase>();

            //将相机坐标位置转换为阀的位置
            foreach (var trace in multiTraces.Traces)
            {
                TraceBase newTrace = trace.Clone() as TraceBase;
                newTrace.Start = newTrace.Start.ToNeedle(multiTraces.Valve);
                newTrace.End = newTrace.End.ToNeedle(multiTraces.Valve);
                if (newTrace is TraceArc)
                {
                    TraceArc traceArc = newTrace as TraceArc;
                    traceArc.Mid = traceArc.Mid.ToNeedle(multiTraces.Valve);
                }
                primaryTraces.Add(newTrace);
            }

            //为主阀的坐标点进行偏移调整
            foreach (var trace in primaryTraces)
            {
                trace.TranslateBy(multiTraces.OffsetX, multiTraces.OffsetY);
            }

            //生成副阀的坐标点
            for (int i = 0; i < primaryTraces.Count; i++)
            {
                simulTraces.Add(this.getSimulTraces(multiTraces, primaryTraces[i]));
            }

            ret = this.SvValveTracesLogic(multiTraces, primaryTraces, simulTraces);
            if (!ret.IsOk)
            {
                return ret;
            }

            return ret;
        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private PointD getSimulPos(MultiTraces multiTraces, PointD pos)
        {
            PointD simulPos = new PointD();
            ///生成副阀相关参数(起点、插补点位)
            if (multiTraces.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - multiTraces.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -Math.Abs(simulPos.Y) / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(multiTraces.Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(multiTraces.Program.RuntimeSettings.SimulOffsetX, multiTraces.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }

        /// <summary>
        /// 获取副阀的AB轴的移动轨迹
        /// </summary>
        /// <param name="multiTraces"></param>
        /// <param name="trace"></param>
        /// <returns></returns>
        private TraceBase getSimulTraces(MultiTraces multiTraces, TraceBase trace)
        {
            TraceBase newTrace = trace.Clone() as TraceBase;
            newTrace.Start = this.getSimulPos(multiTraces, newTrace.Start);
            newTrace.End = this.getSimulPos(multiTraces, newTrace.End);
            if(newTrace is TraceArc)
            {
                TraceArc traceArc = newTrace as TraceArc;
                traceArc.Mid = this.getSimulPos(multiTraces, traceArc.Mid);
            }
            return newTrace;
        }

        /// <summary>
        /// 螺杆阀的轨迹线的打胶逻辑
        /// </summary>
        /// <param name="primaryPoints"></param>
        /// <param name="simulPoints"></param>
        /// <returns></returns>
        private Result SvValveTracesLogic(MultiTraces multiTraces, List<TraceBase> primaryTraces, List<TraceBase> simulTraces)
        {
            Result ret = Result.OK;

            SvValveFludLineParam primaryLineParam = this.GetSvValveParam(multiTraces, primaryTraces);
            SvValveFludLineParam simulLineParam = this.GetSvValveParam(multiTraces, simulTraces);

            double currZ = Machine.Instance.Robot.PosZ;
            double targZ = 0;
            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = multiTraces.Program.RuntimeSettings.BoardZValue + this.getLineParam(multiTraces, multiTraces.Traces[0]).DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(this.getLineParam(multiTraces, multiTraces.Traces[0]).DispenseGap, multiTraces.CurMeasureHeightValue);
            }

            ret = this.SvValveMoveToLineStart(multiTraces, primaryTraces[0].Start, simulTraces[0].Start, currZ, targZ, this.getLineParam(multiTraces, multiTraces.Traces[0]));
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStartSpary(multiTraces, primaryLineParam, simulLineParam);
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStopSpary(multiTraces, this.getLineParam(multiTraces, multiTraces.Traces[0]));
            if (!ret.IsOk)
            {
                return ret;
            }

            return ret;
        }

        /// <summary>
        /// 计算螺杆阀运行多段轨迹线时所需要的各个参数
        /// </summary>
        /// <param name="points"></param>
        /// <param name="lineParam"></param>
        /// <returns></returns>
        private SvValveFludLineParam GetSvValveParam(MultiTraces multiTraces, List<TraceBase> traces)
        {
            // 获取提前开胶时间
            double startPosDelay = this.getLineParam(multiTraces, traces[0]).PreMoveDelay;
            // 获取终点
            PointD endPoint = traces[traces.Count - 1].End;
            // 计算关胶点
            PointD stopSprayPos = this.getPointOnTraces(traces, this.getLineParam(multiTraces, traces[0]).ShutOffDistance, false).Item1;

            // 回走时的终点位置，Poly线不用管这个参数
            PointD backTrackpos = new PointD();

            // 轨迹集合反向
            List<TraceBase> reverseTraces = new List<TraceBase>();
            for (int i = 0; i < traces.Count; i++)
            {
                reverseTraces.Add(traces[traces.Count - 1 - i].Reverse());
            }
            // 回走轨迹集合
            List<TraceBase> backTraces = this.getPointOnTraces(reverseTraces, this.getLineParam(multiTraces, traces[0]).BacktrackDistance, true).Item2;

            double[] vels = new double[traces.Count];
            for (int i = 0; i < traces.Count; i++)
            {
                if (multiTraces.IsWeightControl)
                {
                    //double distance = Math.Sqrt(Math.Pow(line.LineCoordinateList[i].End.Y - line.LineCoordinateList[i].Start.Y,
                    //                     2) + Math.Pow(line.LineCoordinateList[i].End.X - line.LineCoordinateList[i].Start.X, 2));
                    //double lineWeight = line.LineCoordinateList[i].Weight;
                    //double flowVel;
                    //FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(FluidProgram.Current.RuntimeSettings.SvOrGearValveCurrSpeed, out flowVel);
                    //double time = lineWeight * flowVel;
                    //vels[i] = distance / time;
                }
                else
                {
                    vels[i] = this.getLineParam(multiTraces, traces[i]).Speed;
                }
            }

            double backTrackDelay = this.getLineParam(multiTraces, traces[0]).Dwell;
            double backTrackGap = this.getLineParam(multiTraces, traces[0]).BacktrackGap;
            double backTrackVel = this.getLineParam(multiTraces, traces[0]).BacktrackSpeed;

            return new SvValveFludLineParam()
            {
                startPosDelay = startPosDelay,
                endPos = endPoint,
                //transPoints = transPoints,
                transTraces = traces,
                stopSprayPos = stopSprayPos,
                backTrackPos = backTrackpos,
                IsTraceMode = true,
                backTransTraces = backTraces,
                vels = vels,
                backTrackDelay = backTrackDelay,
                backTrackGap = backTrackGap,
                backTrackVel = backTrackVel,
            };
        }

        /// <summary>
        /// 螺杆阀移动到线的起点
        /// </summary>
        /// <returns></returns>
        private Result SvValveMoveToLineStart(MultiTraces multiTraces, PointD pos, PointD simulPos, double currZ, double targZ, LineParam param)
        {
            Result ret = Result.OK;

            if (currZ > targZ)
            {
                // 移动到指定位置
                Log.Dprint("move to Line Start position XY : " + pos);
                if (multiTraces.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, 
                        FluidProgram.Current.MotionSettings.VelXYAB,
                        FluidProgram.Current.MotionSettings.AccXYAB,
                        (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
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
                    z = Converter.NeedleBoard2Z(param.DispenseGap, multiTraces.CurMeasureHeightValue);
                }
                Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, param.DownSpeed, param.DownAccel);
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
                    z = Converter.NeedleBoard2Z(param.DispenseGap, multiTraces.CurMeasureHeightValue);
                }
                Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, param.DownSpeed, param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }

                Log.Dprint("move to Line Start position XY : " + pos);
                if (multiTraces.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                       FluidProgram.Current.MotionSettings.VelXYAB,
                       FluidProgram.Current.MotionSettings.AccXYAB,
                       (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
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

        private Result SvValveStartSpary(MultiTraces multiTraces, SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam)
        {
            Result ret = Result.OK;
            if (multiTraces.RunnableModule.Mode == ModuleMode.MainMode && Machine.Instance.Setting.DualValveMode == DualValveMode.异步)
            {
                ret = Machine.Instance.DualValve.FluidLine(primaryLineParam, simulLineParam, FluidProgram.Current.MotionSettings.WeightAcc);
            }
            else
            {
                ret = Machine.Instance.Valve1.FluidLine(primaryLineParam, FluidProgram.Current.MotionSettings.WeightAcc);
            }
            if (!ret.IsOk)
            {
                return ret;
            }
            return ret;
        }

        private Result SvValveStopSpary(MultiTraces multiTraces, LineParam param)
        {
            Result ret = Result.OK;

            // 抬高一段距离 Retract Distance
            if (param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(param.RetractDistance, param.RetractSpeed, param.RetractAccel);
            }

            if (multiTraces.RunnableModule.Mode == ModuleMode.MainMode)
            {
                ret = Machine.Instance.DualValve.SuckBack(param.SuckBackTime);
            }
            else
            {
                ret = Machine.Instance.Valve1.SuckBack(param.SuckBackTime);
            }
            if (!ret.IsOk)
            {
                return ret;
            }
            return ret;
        }

        /// <summary>
        /// 获得轨迹上某个固定距离的点坐标,该距离由终点指向起点(也可传入百分比),获取被该点截断的新轨迹
        /// </summary>
        /// <param name="traces">轨迹集合</param>
        /// <param name="distance">提前关胶距离</param>
        /// <param name="isPercent">是否按百分比</param>
        /// <returns>停止点，关胶距离换算后的轨迹集合</returns>
        private Tuple<PointD, List<TraceBase>> getPointOnTraces(List<TraceBase> traces, double distance, bool isPercent)
        {
            //计算线的总长度和各线段的长度
            double sumDistance = 0;
            foreach (var trace in traces)
            {
                sumDistance += trace.Length;
            }

            //确定该距离相对于起始点的距离
            double shutOffDistance = 0;
            if (isPercent)
            {
                shutOffDistance = sumDistance * (distance * 0.01);
            }
            else
            {
                shutOffDistance = sumDistance - distance;
            }

            if(shutOffDistance < 0)
            {
                shutOffDistance = 1;
            }

            // 确定该距离在第几个线段内
            double currDistance = 0;
            double prevDistance = 0;
            List<TraceBase> newTraces = new List<TraceBase>();
            for (int i = 0; i < traces.Count; i++)
            {
                currDistance += traces[i].Length;
                if (currDistance > shutOffDistance)
                {
                    newTraces.Add(traces[i].NewTraceFromStart(shutOffDistance - prevDistance));
                    break;
                }
                else if (currDistance == shutOffDistance)
                {
                    newTraces.Add(traces[i]);
                    break;
                }
                else
                {
                    newTraces.Add(traces[i]);
                }
                prevDistance = currDistance;
            }

            return new Tuple<PointD, List<TraceBase>>(newTraces[newTraces.Count - 1].End, newTraces);
        }
    }
}
