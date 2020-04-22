using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive.GlueManage;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveTracker
{
     /*点胶前：
     Down Speed: 执行线命令过程中Z轴下降速度
     Down Acceleration: 执行线命令过程中Z轴下降加速度
     点胶中：
     Dispense Gap: 距板高度，点胶时点胶阀距离基板的高度值
     Speed: 执行普通线命令时沿着线轨迹的运动速度。该参数只针对普通线
     Wt-Ctrl Speed: 执行重量线命令时沿着线轨迹的运动速度。
                    用户可以下拉选择"Compute",由内置公式自动计算运动速度，也可以由用户手动指定，
                    但是不能大于内置公式计算出来的值。该参数只针对重量线。
     Accel Distance: 线的起始点之前需要增加一段距离作为点胶阀运动的加速区间，以保证线段上运动是匀速的。此参数指定了加速区间的长度。用户可以选择"Table"表示由内置公式自动计算加速区间长度，也可以由用户手动编辑指定加速区间长度。
     Decel Distance: 线的末端跟随一段距离作为点胶阀运动的减速区间，以保证线段上运动是匀速的。此参数指定了减速区间的长度。用户可以选择"Table"表示由内置公式自动计算减速区间长度，也可以由用户手动编辑指定减速区间长度。
     点胶后：
     Retract Distance: 在减速区间运动完成后，XY方向上速度为0后，点胶阀需要抬升一段距离，此参数指定点胶阀抬升的相对距离值。
     Retract Speed: 指定点胶阀抬升速度
     Retract Accel: 指定点胶阀抬升加速度
     Control: 只针对普通线
     Control Mode: 控制模式，一共有四种：
     1. Pos-based spacing: 指定两滴胶水之间的间距长度值
     2. Time-based spacing: 相邻两滴胶水喷胶开始时间的最小时间间隔，单位：秒，如果该参数值为s，本次喷胶开始时间距离上次喷胶开始时间为d, 如果d<s，则还需要等待（s-d）秒，否则无需等待。
     3. Total # of dots: 指定一条线上一共有几个点
     4. No dispense: 只运动不喷胶
     Spacing: 如果是Pos-based spacing模式，该参数可编辑，指定了两滴胶水之间的间距值；如果是其他模式，该参数无效，不可编辑。
     Shot Time Interval: 如果是Time-based spacing模式，该参数可编辑，指定了相邻两滴胶水喷胶开始时间的最小时间间隔值；如果是其他模式，该参数无效，不可编辑。
     Total # of dots: 如果是Total # of dots模式，该参数可编辑，指定了点的数量。如果是其他模式，该参数无效，不可编辑。
     */
    public class JtValveBufArc : ArcTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        public override Result DryExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        public override Result InspectDotExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        public override Result LookExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            Arc arc = directive as Arc;

            // 重量弧
            if (arc.IsWeightControl)
            {
                // 计算点胶数量
                int shots;
                if (arc.Program.RuntimeSettings.isHalfAdjust)
                {
                    // 四舍五入
                    shots = (int)Math.Round(((decimal)arc.Weight / (decimal)arc.Program.RuntimeSettings.SingleDropWeight),MidpointRounding.ToEven);
                }
                else
                {
                    shots = (int)((decimal)arc.Weight / (decimal)arc.Program.RuntimeSettings.SingleDropWeight);
                }
                // 计算时间 (需要将微秒单位转换成秒）
                double totalTime = Machine.Instance.Valve1.SpraySec * shots;
                // 计算速度
                double wtSpeed = arc.Length / totalTime;
                if (arc.Param.WtCtrlSpeedValueType != LineParam.ValueType.COMPUTE)
                {
                    wtSpeed = arc.Param.WtCtrlSpeed > wtSpeed ? wtSpeed : arc.Param.WtCtrlSpeed;
                }
                // 运动速度不能超过设定的最大速度
                wtSpeed = wtSpeed > FluidProgram.Current.MotionSettings.WeightMaxVel ? FluidProgram.Current.MotionSettings.WeightMaxVel : wtSpeed;
                // 计算点胶位置
                PointD[] points = calFixedTotalOfDots(arc,arc.Start, arc.Center, arc.End, shots);
                //基于时间控制
                totalTime = arc.Length / wtSpeed;
                double intervalSec = totalTime / (shots - 1);
                double dispenseSec = Machine.Instance.Valve1.SpraySec;
                if (intervalSec < dispenseSec)
                {
                    intervalSec = dispenseSec;
                }
                //执行
                this.sprayLine(arc,valve, points.Length, intervalSec);
            }
            // 普通弧
            else
            {
                // 运动速度不能超过设定的最大速度
                double speed = arc.Param.Speed > FluidProgram.Current.MotionSettings.WorkMaxVel ? FluidProgram.Current.MotionSettings.WorkMaxVel : arc.Param.Speed;
                // 计算点胶位置
                double intervalSec;
                PointD[] points = calNormalArcPoints(arc,arc.Start, arc.Center, arc.End, speed, out intervalSec);
                this.sprayLine(arc,valve, points.Length, intervalSec);
            }
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            Arc arc = directive as Arc;
            Result ret = ExecuteLogic(arc);
            return ret;
        }

        /// <summary>
        /// 喷射阀在除开PatternWeight模式外其余所有模式的执行逻辑
        /// </summary>
        /// <returns></returns>
        private Result ExecuteLogic(Arc arc)
        {
            Result ret = Result.OK;
            double accDistance = 0, decelDistance = 0;
            // 重量弧
            if (arc.IsWeightControl)
            {
                // 计算点胶数量
                int shots = (int)((decimal)arc.Weight / (decimal)arc.Program.RuntimeSettings.SingleDropWeight);
                // 计算时间 (需要将微秒单位转换成秒）
                double totalTime = Machine.Instance.Valve1.SpraySec * shots;
                // 计算速度
                double wtSpeed = arc.Length / totalTime;
                if (arc.Param.WtCtrlSpeedValueType != LineParam.ValueType.COMPUTE)
                {
                    wtSpeed = arc.Param.WtCtrlSpeed > wtSpeed ? wtSpeed : arc.Param.WtCtrlSpeed;
                }
                // 运动速度不能超过设定的最大速度
                wtSpeed = wtSpeed > FluidProgram.Current.MotionSettings.WeightMaxVel ? FluidProgram.Current.MotionSettings.WeightMaxVel : wtSpeed;
                // 计算加速区间距离
                if (arc.Param.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
                {
                    accDistance = MathUtils.CalculateDistance(0, wtSpeed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                }
                else
                {
                    accDistance = arc.Param.AccelDistance;
                }
                // 计算减速区间距离
                if (arc.Param.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
                {
                    decelDistance = MathUtils.CalculateDistance(wtSpeed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                }
                else
                {
                    decelDistance = arc.Param.DecelDistance;
                }
                // 计算加速区间起始位置和减速区间结束位置
                PointD accStart = calAccStartPosition(arc,arc.Start, arc.Center, accDistance);
                PointD decelEnd = calDecelEndPosition(arc,arc.End, arc.Center, decelDistance);
                // 计算点胶位置
                PointD[] points = calFixedTotalOfDots(arc,arc.Start, arc.Center, arc.End, shots);

                //基于时间控制
                totalTime = arc.Length / wtSpeed;
                double intervalSec = totalTime / (shots - 1);
                double dispenseSec = Machine.Instance.Valve1.SpraySec;
                if (intervalSec < dispenseSec)
                {
                    intervalSec = dispenseSec;
                    points = calFixedTotalOfDots(arc,arc.Start, arc.Center, arc.End, (int)(totalTime / intervalSec) + 1);
                }

                ret = this.executeArc(arc,accStart, arc.Start, arc.End, decelEnd, arc.Center, (short)(arc.Degree > 0 ? 1 : 0), wtSpeed, points, intervalSec);
            }
            // 普通弧
            else
            {
                // 运动速度不能超过设定的最大速度
                double speed = arc.Param.Speed > FluidProgram.Current.MotionSettings.WorkMaxVel ? FluidProgram.Current.MotionSettings.WorkMaxVel : arc.Param.Speed;
                // 计算加速区间距离
                if (arc.Param.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
                {
                    accDistance = MathUtils.CalculateDistance(0, speed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                }
                else
                {
                    accDistance = arc.Param.AccelDistance;
                }
                // 计算减速区间距离
                if (arc.Param.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
                {
                    decelDistance = MathUtils.CalculateDistance(speed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                }
                else
                {
                    decelDistance = arc.Param.DecelDistance;
                }
                // 计算加速区间起始位置和减速区间结束位置
                PointD accStart = calAccStartPosition(arc,arc.Start, arc.Center, accDistance);
                PointD decelEnd = calDecelEndPosition(arc,arc.End, arc.Center, decelDistance);
                // 计算点胶位置
                double intervalSec;
                PointD[] points = calNormalArcPoints(arc,arc.Start, arc.Center, arc.End, speed, out intervalSec);

                ret = this.executeArc(arc,accStart, arc.Start, arc.End, decelEnd, arc.Center, (short)(arc.Degree > 0 ? 1 : 0), speed, points, intervalSec);
            }

            return ret;
        }

        private Result executeArc(Arc arc,PointD accStart, PointD arcStart, PointD arcEnd, PointD decEnd, PointD center, short clockwise, double vel, PointD[] points, double intervalSec)
        {
            // 偏移调整
            VectorD v = (arcStart - accStart).Normalize() * arc.Param.Offset;
            PointD cameraStart = arcStart + v;
            PointD cameraEnd = arcEnd + v;
            PointD cameraAccStart = accStart + v;
            PointD cameraDecEnd = decEnd + v;
            PointD cameraCenter = center + v;
            PointD[] cameraPoints = new PointD[points.Length];
            PointD[] cameraPoints2 = new PointD[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                cameraPoints[i] = points[i] + v;
            }
            // 打胶起点
            PointD valveAccStart = cameraAccStart.ToNeedle(arc.Valve);
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulStart = new PointD(arc.Program.RuntimeSettings.SimulDistence, 0)/*-胶阀原点间距？*/;
            ///生成副阀相关参数(起点、插补点位)
            if (arc.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标 = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                for (int i = 0; i < points.Length; i++)
                {
                    cameraPoints2[i] = cameraPoints[i] - arc.RunnableModule.SimulTransformer.Transform(cameraPoints[i]).ToVector() - SimulModuleOffset;
                    cameraPoints2[i].X = -Math.Abs(cameraPoints2[i].X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                    cameraPoints2[i].Y = -cameraPoints2[i].Y / Machine.Instance.Robot.CalibPrm.VerticalRatio;
                }
                //副阀在加速段直接使用实际点胶起始位置
                simulStart = cameraPoints2[0];
            }

            Result ret = Result.OK;
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

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }

                // 移动到加速区间起始位置,(视觉模式不用移动AB轴)
                Log.Dprint("move to position XY : " + cameraStart);
                ret = Machine.Instance.Robot.MovePosXYAndReply(cameraStart,
                    FluidProgram.Current.MotionSettings.VelXY,
                    FluidProgram.Current.MotionSettings.AccXY);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            else
            {
                if (currZ > targZ)
                {
                    // 移动到加速区间起始位置
                    if (arc.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Log.Dprint("move to position XYAB : " + valveAccStart + simulStart);
                        ret = Machine.Instance.Robot.MovePosXYABAndReply(valveAccStart, simulStart, 
                            FluidProgram.Current.MotionSettings.VelXYAB,
                            FluidProgram.Current.MotionSettings.AccXYAB,
                            (int)Machine.Instance.Setting.CardSelect);
                    }
                    else
                    {
                        Log.Dprint("move to position XY : " + valveAccStart);
                        ret = Machine.Instance.Robot.BufMoveLnXY(valveAccStart,
                            FluidProgram.Current.MotionSettings.VelXY,
                            FluidProgram.Current.MotionSettings.AccXY);
                    }
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    // 下降到指定高度
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
                    ret = Machine.Instance.Robot.BufMovePosZ(z, arc.Param.DownSpeed, arc.Param.DownAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                else
                {
                    // 上升到指定高度
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
                    ret = Machine.Instance.Robot.BufMovePosZ(z, arc.Param.DownSpeed, arc.Param.DownAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    // 移动到加速区间起始位置
                    if (arc.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Log.Dprint("move to position XYAB : " + valveAccStart + simulStart);
                        ret = Machine.Instance.Robot.MovePosXYABAndReply(valveAccStart, simulStart,
                                FluidProgram.Current.MotionSettings.VelXYAB,
                                FluidProgram.Current.MotionSettings.AccXYAB,
                                (int)Machine.Instance.Setting.CardSelect);
                    }
                    else
                    {
                        Log.Dprint("move to position XY : " + valveAccStart);
                        ret = Machine.Instance.Robot.BufMoveLnXY(valveAccStart,
                                FluidProgram.Current.MotionSettings.VelXY,
                                FluidProgram.Current.MotionSettings.AccXY);
                    }
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }

            // 移动： 加速区间--线段区间--减速区间
            Log.Dprint("Fluid Arc, accStart=" + cameraAccStart + ", start=" + cameraStart + ", end=" + cameraEnd + ", decelEnd=" + cameraDecEnd
                + ", center=" + cameraCenter + ", degrees=" + arc.Degree + ", speed=" + vel);
            printPoints(cameraPoints);
            if (arc.RunnableModule.Mode == ModuleMode.MainMode)
            {
                ret = Machine.Instance.DualValve.FluidArc(cameraAccStart, cameraStart, cameraEnd, cameraDecEnd, cameraCenter, clockwise, vel, cameraPoints, intervalSec, simulStart, cameraPoints2, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
            }
            else if (arc.RunnableModule.Mode == ModuleMode.DualFallow)
            {
                ret = Machine.Instance.DualValve.FluidArc(cameraAccStart, cameraStart, cameraEnd, cameraDecEnd, cameraCenter, clockwise, vel, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
            }
            else if (arc.RunnableModule.Mode == ModuleMode.AssignMode2)
            {
                ret = Machine.Instance.Valve2.FluidArc(cameraAccStart, cameraStart, cameraEnd, cameraDecEnd, cameraCenter, clockwise, vel, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
            }
            else
            {
                ret = Machine.Instance.Valve1.BufFluidArc(cameraAccStart, cameraStart, cameraEnd, cameraDecEnd, cameraCenter, clockwise, vel, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * arc.Program.RuntimeSettings.SingleDropWeight);
            }
            if (!ret.IsOk)
            {
                return ret;
            }

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Dry)
            {
                Log.Dprint("RetractDistance : " + arc.Param.RetractDistance);
                // 减速区间运动完成后，x y 轴速度为0， 此时需要抬升一段高度
                if (arc.Param.RetractDistance > 0)
                {
                    Log.Dprint("move up RetractDistance : " + arc.Param.RetractDistance);
                    ret = Machine.Instance.Robot.BufMoveIncZ(arc.Param.RetractDistance, arc.Param.RetractSpeed, arc.Param.RetractAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }

            return ret;
        }

        private void printPoints(PointD[] points)
        {
            Log.Dprint("points list (" + points.Length + ") :");
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < points.Length; i++)
            {
                sb.Append(points[i]).Append(" ");
                if (i != 0 && i % 5 == 0)
                {
                    Log.Dprint(sb.ToString());
                    sb = new StringBuilder();
                }
            }
            if (sb.Length > 0)
            {
                Log.Dprint(sb.ToString());
            }
        }

        /// <summary>
        /// 计算普通弧上每个点胶位置
        /// </summary>
        /// <returns></returns>
        private PointD[] calNormalArcPoints(Arc arc,PointD s, PointD c, PointD e, double speed, out double intervalSec)
        {
            double totalTime = arc.Length / speed;
            // 计算点胶时间 (需要将微秒单位转换成秒）
            double dispenseTime = Machine.Instance.Valve1.SpraySec;
            intervalSec = dispenseTime;

            PointD[] points = null;
            // Control Mode: 控制模式，一共有四种：
            // 1. Pos-based spacing: 指定两滴胶水之间的间距长度值
            // 2. Time-based spacing: 相邻两滴胶水喷胶开始时间的最小时间间隔，单位：秒，如果该参数值为s，本次喷胶开始时间距离上次喷胶开始时间为d, 
            //                        如果d < s，则还需要等待（s-d）秒，否则无需等待。
            // 3. Total # of dots: 指定一条线上一共有几个点
            // 4. No dispense: 只运动不喷胶
            if (arc.Param.ControlMode == LineParam.CtrlMode.POS_BASED_SPACING)
            {
                points = calFixedTotalOfDots(arc,s, c, e, (int)(arc.Length / arc.Param.Spacing) + 1);
                intervalSec = totalTime / (points.Length - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(arc,s, c, e, (int)(totalTime / intervalSec) + 1);
                }
            }
            else if (arc.Param.ControlMode == LineParam.CtrlMode.TIME_BASED_SPACING)
            {
                intervalSec = arc.Param.ShotTimeInterval / 1000.0 < dispenseTime ? dispenseTime : arc.Param.ShotTimeInterval / 1000.0;
                points = calFixedTotalOfDots(arc,s, c, e, (int)(totalTime / intervalSec) + 1);
            }
            else if (arc.Param.ControlMode == LineParam.CtrlMode.TOTAL_OF_DOTS && arc.Param.TotalOfDots > 0)
            {
                intervalSec = totalTime / (arc.Param.TotalOfDots - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(arc,s, c, e, (int)(totalTime / intervalSec) + 1);
                }
                else
                {
                    points = calFixedTotalOfDots(arc,s, c, e, arc.Param.TotalOfDots);
                }
            }
            else
            {
                points = new PointD[0];
            }
            return points;
        }

        private PointD[] calFixedTotalOfDots(Arc arc,PointD s, PointD c, PointD e, int shots)
        {
            PointD[] points = null;
            if (shots <= 0)
            {
                points = new PointD[0];
            }
            else
            {
                bool isClockwise = arc.Degree < 0;
                double startRadian = MathUtils.GetRadianOnCircle(c, s, arc.R);
                double endRadian = MathUtils.GetRadianOnCircle(c, e, arc.R);
                if (shots == 1)
                {
                    points = new PointD[1];
                    if (isClockwise)
                    {
                        if (startRadian > endRadian)
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, arc.R, (startRadian + endRadian) / 2);
                        }
                        else
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, arc.R, (startRadian + endRadian + 2 * Math.PI) / 2);
                        }
                    }
                    else
                    {
                        if (startRadian >= endRadian)
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, arc.R, (startRadian + endRadian + 2 * Math.PI) / 2);
                        }
                        else
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, arc.R, (startRadian + endRadian) / 2);
                        }
                    }
                }
                else
                {
                    points = new PointD[shots];
                    double start = 0;
                    double gap = 0;

                    int sprayshots = shots - 1;
                    if (arc.Degree == 360 || arc.Degree == -360)
                    {
                        sprayshots = shots;
                    }

                    if (isClockwise)
                    {
                        if (startRadian > endRadian)
                        {
                            start = startRadian;
                            gap = -(start - endRadian) / sprayshots;
                        }
                        else
                        {
                            start = startRadian + 2 * Math.PI;
                            gap = -(start - endRadian) / sprayshots;
                        }
                    }
                    else
                    {
                        if (startRadian >= endRadian)
                        {
                            start = startRadian;
                            gap = (endRadian + 2 * Math.PI - start) / sprayshots;
                        }
                        else
                        {
                            start = startRadian;
                            gap = (endRadian - start) / sprayshots;
                        }
                    }

                    for (int i = 0; i < shots; i++)
                    {
                        points[i] = MathUtils.GetPointOnCircle(c, arc.R, startRadian + gap * i);
                    }
                }
            }
            return points;
        }

        /// <summary>
        /// 根据加速区间，计算加速起始位置
        /// </summary>
        /// <arc.Param name="line"></arc.Param>
        /// <arc.Param name="accDistance"></arc.Param>
        /// <returns></returns>
        private PointD calAccStartPosition(Arc arc,PointD s, PointD c, double accDistance)
        {
            PointD position = new PointD();
            double cos = (s.Y - c.Y) / arc.R;
            double sin = (s.X - c.X) / arc.R;
            if (arc.Degree > 0)
            {
                position.X = s.X + accDistance * cos;
                position.Y = s.Y - accDistance * sin;
            }
            else
            {
                position.X = s.X - accDistance * cos;
                position.Y = s.Y + accDistance * sin;
            }
            return position;
        }

        /// <summary>
        /// 根据减速区间，计算减速结束位置
        /// </summary>
        /// <arc.Param name="line"></arc.Param>
        /// <arc.Param name="decelDistance"></arc.Param>
        /// <returns></returns>
        private PointD calDecelEndPosition(Arc arc,PointD e, PointD c, double decelDistance)
        {
            PointD position = new PointD();
            double cos = (e.Y - c.Y) / arc.R;
            double sin = (e.X - c.X) / arc.R;
            if (arc.Degree > 0)
            {
                position.X = e.X - decelDistance * cos;
                position.Y = e.Y + decelDistance * sin;
            }
            else
            {
                position.X = e.X + decelDistance * cos;
                position.Y = e.Y - decelDistance * sin;
            }
            return position;
        }


        private void sprayLine(Arc arc,Valve valve, int shots, double intervalSec)
        {
            valve = Machine.Instance.Valve1;
            valve.MoveToScaleLoc();
            valve.SprayCycle((short)shots, (short)(intervalSec - valve.Prm.JtValvePrm.OnTime));
            GlueManagerMgr.Instance.UpdateGlueRemainWeight((int)valve.Key, shots * arc.Program.RuntimeSettings.SingleDropWeight);
            arc.shortNum += shots;
        }
    }
}
