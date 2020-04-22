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
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.Vision.ASV;
using System.Threading;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive.GlueManage;
using Anda.Fluid.Drive.ValveSystem.Series;

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
     Accel Distance: 线的起始点之前需要增加一段距离作为点胶阀运动的加速区间，以保证线段上运动是匀速的。此参数指定了加速区间的长度。
                     用户可以选择"Table"表示由内置公式自动计算加速区间长度，也可以由用户手动编辑指定加速区间长度。
     Decel Distance: 线的末端跟随一段距离作为点胶阀运动的减速区间，以保证线段上运动是匀速的。此参数指定了减速区间的长度。
                     用户可以选择"Table"表示由内置公式自动计算减速区间长度，也可以由用户手动编辑指定减速区间长度。
     点胶后：
     Retract Distance: 在减速区间运动完成后，XY方向上速度为0后，点胶阀需要抬升一段距离，此参数指定点胶阀抬升的相对距离值。
     Retract Speed: 指定点胶阀抬升速度
     Retract Accel: 指定点胶阀抬升加速度
     Control: 只针对普通线
     Control Mode: 控制模式，一共有四种：
     1. Pos-based spacing: 指定两滴胶水之间的间距长度值
     2. Time-based spacing: 相邻两滴胶水喷胶开始时间的最小时间间隔，单位：秒，如果该参数值为s，本次喷胶开始时间距离上次喷胶开始时间为d, 
                            如果d<s，则还需要等待（s-d）秒，否则无需等待。
     3. Total # of dots: 指定一条线上一共有几个点
     4. No dispense: 只运动不喷胶
     Spacing: 如果是Pos-based spacing模式，该参数可编辑，指定了两滴胶水之间的间距值；如果是其他模式，该参数无效，不可编辑。
     Shot Time Interval: 如果是Time-based spacing模式，该参数可编辑，指定了相邻两滴胶水喷胶开始时间的最小时间间隔值；
                         如果是其他模式，该参数无效，不可编辑。
     Total # of dots: 如果是Total # of dots模式，该参数可编辑，指定了点的数量。如果是其他模式，该参数无效，不可编辑。
     */
    public class JtValveLine : LineTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        public override Result DryExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        public override Result InspectDotExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        public override Result LookExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            Line directiveLine = directive as Line;

            // 重量线 //胶量模式
            if (directiveLine.IsWeightControl)
            {
                // 计算线段总长度
                double sum = 0;
                foreach (LineCoordinate line in directiveLine.LineCoordinateList)
                {
                    sum += line.CalculateDistance();
                }
                foreach (LineCoordinate line in directiveLine.LineCoordinateList)
                {
                    double lineDistance = line.CalculateDistance();
                    // 计算点胶数量
                    int shots = (int)((decimal)line.Weight / (decimal)directiveLine.Program.RuntimeSettings.SingleDropWeight);
                    // 计算时间 (需要将微秒单位转换成秒）
                    double totalTime = Machine.Instance.Valve1.SpraySec * shots;
                    // 计算速度
                    double wtSpeed = this.CalculateVel(line, lineDistance, totalTime);
                    //基于时间控制
                    totalTime = lineDistance / wtSpeed;
                    double intervalSec = totalTime / (shots - 1);
                    double dispenseSec = Machine.Instance.Valve1.SpraySec;
                    if (intervalSec < dispenseSec)
                    {
                        intervalSec = dispenseSec;
                    }
                    // 执行
                    this.sprayLine(directiveLine,valve, shots, intervalSec);
                }
            }
            // 普通线//速度模式
            else
            {
                foreach (LineCoordinate line in directiveLine.LineCoordinateList)
                {
                    // 运动速度不能超过设定的最大速度
                    double speed = line.Param.Speed > FluidProgram.Current.MotionSettings.WorkMaxVel ? FluidProgram.Current.MotionSettings.WorkMaxVel : line.Param.Speed;
                    // 计算点胶位置
                    double intervalSec;
                    PointD[] points = calNormalLinePoints(line, speed, out intervalSec);
                    // 执行
                    this.sprayLine(directiveLine,valve, points.Length, intervalSec);
                }
            }
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            Line direcitveLine = directive as Line;
            Result ret = ExecuteLogic(direcitveLine);
            return ret;
        }

        /// <summary>
        /// 手动打线，供四方位角度校准时使用
        /// </summary>
        /// <returns></returns>
        public Result ManualDispense(PointD start, PointD end, double posZ, LineParam lineParam, bool isWeight, double weight)
        {
            Result result = Result.OK;

            PointD accStart = new PointD(), decelEnd = new PointD();
            double speed = 0, intervalSec = 0;
            PointD[] points = null;

            //胶量模式
            if (isWeight)
            {
                this.GetManualWtLineParam(start, end, lineParam, weight, out accStart, out decelEnd, out speed, out points, out intervalSec);
            }
            //速度模式
            else
            {
                this.GetManualNormalLineParam(start, end, lineParam, out accStart, out decelEnd, out speed, out points, out intervalSec);
            }

            //执行打线
            // 移动到加速区间起始位置
            Log.Dprint("move to position XY : " + accStart);
            result = Machine.Instance.Robot.MovePosXYAndReply(accStart,
                     FluidProgram.Current.MotionSettings.VelXY,FluidProgram.Current.MotionSettings.AccXY);
            if (!result.IsOk)
            {
                return result;
            }

            // 到指定高度
            result = Machine.Instance.Robot.MovePosZAndReply(posZ, lineParam.DownSpeed, lineParam.DownAccel);
            if (!result.IsOk)
            {
                return result;
            }

            //开始走直线出胶
            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                JtValve jtValve = Machine.Instance.Valve1 as JtValve;
                jtValve.FluidManualLine(accStart, start, end, decelEnd, speed, points, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
            }

            return result;
        }

        /// <summary>
        /// 除开PatternWeight外所有模式下的执行逻辑
        /// </summary>
        /// <param name="directiveLine"></param>
        /// <returns></returns>
        private  Result ExecuteLogic(Line directiveLine)
        {
            Result ret = Result.OK;
            // 重量线 //胶量模式
            if (directiveLine.IsWeightControl)
            {
                if (directiveLine.LineCmd.lineCmdLine.IsWholeWtMode && directiveLine.LineCmd.lineCmdLine.LineMethod != LineMethod.Single)
                {
                    int totalShots = (int)((decimal)directiveLine.WholeWeight / (decimal)directiveLine.Program.RuntimeSettings.SingleDropWeight);
                    MathTools.AllocateDotCount.Allocate(totalShots, directiveLine.LineCoordinates);
                }
                foreach (LineCoordinate line in directiveLine.LineCoordinateList)
                {
                    PointD accStart = new PointD(), decelEnd = new PointD();
                    double speed = 0, intervalSec = 0;
                    PointD[] points = null;
                    this.CalculateWtLineParam(line, directiveLine, out accStart, out decelEnd, out speed, out points, out intervalSec);
                    ret = this.executeSingleLine(directiveLine, line, accStart, decelEnd, speed, points, intervalSec);
                }
            }
            // 普通线 //速度模式
            else
            {
                foreach (LineCoordinate line in directiveLine.LineCoordinateList)
                {
                    PointD accStart = new PointD(), decelEnd = new PointD();
                    double speed = 0, intervalSec = 0;
                    PointD[] points = null;
                    this.CalculateNormalLineParam(line, directiveLine, out accStart, out decelEnd, out speed, out points, out intervalSec);
                    ret = this.executeSingleLine(directiveLine, line, accStart, decelEnd, speed, points, intervalSec);
                }
            }
            return ret;
        }


        /// <summary>
        /// 执行单条线段命令
        /// </summary>
        /// <param name="line"></param>
        /// <param name="accStart">加速区间距离</param>
        /// <param name="decelEnd">减速区间距离</param>
        /// <param name="speed">线段区间的运动速度</param>
        /// <param name="points">每一滴胶水的点胶位置</param>
        /// <param name="intervalSec">每一滴胶水的打胶时间，单位秒</param>
        /// <returns></returns>
        private Result executeSingleLine(Line directiveLine,LineCoordinate line, PointD accStart, PointD decelEnd, double speed, PointD[] points, double intervalSec)
        {
            double lineOffset = line.Param.Offset;
            // 偏移调整
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Dry || Machine.Instance.Valve1.RunMode == ValveRunMode.Wet
                || Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                if (directiveLine.LineCmd.RunnableModule.CommandsModule.IsReversePattern)
                {
                    lineOffset += line.LookOffsetRevs;
                }
                else
                {
                    lineOffset += line.LookOffset;
                }

            }

            VectorD v = (line.End - line.Start).Normalize() * lineOffset;
            PointD cameraLineStart = line.Start + v;
            PointD cameraLineEnd = line.End + v;
            PointD cameraAccStart = accStart + v;
            PointD cameraDecEnd = decelEnd + v;
            PointD[] cameraPoints = new PointD[points.Length];
            PointD[] cameraPoints2 = new PointD[points.Length];//副阀的点位
            for (int i = 0; i < points.Length; i++)
            {
                cameraPoints[i] = points[i] + v;
            }
            // 点胶起点
            PointD valveAccStart = cameraAccStart.ToNeedle(directiveLine.Valve, directiveLine.Tilt);
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulStart = new PointD(directiveLine.Program.RuntimeSettings.SimulDistence, 0)/*-胶阀原点间距？*/;
            ///生成副阀相关参数(起点、插补点位)
            if (directiveLine.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                for (int i = 0; i < points.Length; i++)
                {
                    cameraPoints2[i] = cameraPoints[i] - directiveLine.RunnableModule.SimulTransformer.Transform(cameraPoints[i]).ToVector() - SimulModuleOffset;
                    //乘以系数得到AB轴的移动位移
                    cameraPoints2[i].X = -Math.Abs(cameraPoints2[i].X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                    cameraPoints2[i].Y = -cameraPoints2[i].Y / Machine.Instance.Robot.CalibPrm.VerticalRatio;
                }
                //副阀在加速段直接使用实际点胶起始位置
                simulStart = cameraPoints2[0];
            }
            Result ret = Result.OK;
            ////CV模式
            //if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look)
            //{
            //    newAccStart = Converter.Needle1ToCamera(accStart);
            //    newLineStart = Converter.Needle1ToCamera(line.Start);
            //    newLineEnd = Converter.Needle1ToCamera(line.End);
            //    newDecEnd = Converter.Needle1ToCamera(decelEnd);
            //    for (int i = 0; i < points.Length; i++)
            //    {
            //        newPoints[i] = Converter.Needle1ToCamera(points[i]);
            //    }
            //}
            double targZ = 0;
            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = directiveLine.Program.RuntimeSettings.BoardZValue + line.Param.DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(line.Param.DispenseGap, directiveLine.CurMeasureHeightValue,directiveLine.Tilt);
            }
            double z = targZ;

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.AdjustLine
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectRect)
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }

                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look)
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(cameraLineStart,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                    ret = Machine.Instance.Robot.MovePosXYAndReply(cameraLineEnd,
                        line.Param.Speed,
                         FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                    Log.Print("current position in LookMode: " + cameraAccStart.ToString() + " || the LookOffset:" + line.LookOffset);
                }

                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.AdjustLine)
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(cameraLineStart,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    LineCmdLine lineCmdline = directiveLine.LineCmd.lineCmdLine;
                    if (lineCmdline == null || line == null)
                    {
                        return ret;
                    }

                    Machine.Instance.IsProducting = false;
                    Line.WaitMsg.Reset();
                    switch (lineCmdline.LineMethod)
                    {
                        case LineMethod.Single:
                            MsgCenter.Broadcast(MsgType.MSG_LINEEDITLOOK_SHOW, directiveLine, lineCmdline);
                            break;
                        case LineMethod.Multi:
                        case LineMethod.Poly:
                            MsgCenter.Broadcast(MsgType.MSG_LINEEDITLOOK_SHOW, directiveLine, lineCmdline, directiveLine.LineCoordinates.IndexOf(line));
                            break;
                        default:
                            break;
                    }
                    Line.WaitMsg.WaitOne();
                    Machine.Instance.IsProducting = true;
                }
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectRect)
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(cameraLineStart,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                    LineCmdLine lineCmdline = directiveLine.LineCmd.lineCmdLine;
                    if (lineCmdline == null || line == null)
                    {
                        return ret;
                    }
                    if (lineCmdline.LineMethod != LineMethod.Single)
                    {
                        return ret;
                    }

                    InspectionLine inspectionLine = InspectionMgr.Instance.FindBy((int)lineCmdline.inspectionKey) as InspectionLine;
                    if (inspectionLine != null)
                    {
                        Thread.Sleep(inspectionLine.SettlingTime);
                        double width1, width2;
                        Machine.Instance.CaptureAndInspect(inspectionLine);
                        width1 = inspectionLine.PhyWidth1;
                        width2 = inspectionLine.PhyWidth2;
                        Log.Dprint(inspectionLine.CurrResultStr);
                        string resline = string.Format("{0},{1},{2},{3}", Math.Round(cameraLineStart.X, 3), Math.Round(cameraLineStart.Y, 3), Math.Round(width1, 3), Math.Round(width2, 3));
                        CsvUtil.WriteLine(directiveLine.Program.RuntimeSettings.FilePathInspectRect, resline);
                        Thread.Sleep(inspectionLine.DwellTime);
                    }
                }
                return ret;

            }
            else
            {
                if (Machine.Instance.Valve1.RunMode != Drive.ValveSystem.ValveRunMode.Look)
                {
                    // 倾斜到位
                    Log.Dprint("change tilt status : " + directiveLine.Tilt.ToString());
                    ret = Machine.Instance.Valve1.ChangeValveTiltStatus(directiveLine.Tilt, FluidProgram.Current.MotionSettings.VelU, FluidProgram.Current.MotionSettings.AccU);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                    double currZ = Machine.Instance.Robot.PosZ;
                    //到起点，Z轴到点胶位置
                    if (currZ > targZ)
                    {
                        // 移动到加速区间起始位置
                        Log.Dprint("move to position XY : " + valveAccStart);
                        if (directiveLine.RunnableModule.Mode == ModuleMode.MainMode)
                        {
                            ret = Machine.Instance.Robot.MovePosXYABAndReply(valveAccStart, simulStart, 
                                FluidProgram.Current.MotionSettings.VelXYAB,
                                FluidProgram.Current.MotionSettings.AccXYAB,
                                (int)Machine.Instance.Setting.CardSelect);
                        }
                        else
                        {
                            ret = Machine.Instance.Robot.MovePosXYAndReply(valveAccStart,
                                FluidProgram.Current.MotionSettings.VelXY,
                                FluidProgram.Current.MotionSettings.AccXY);
                        }
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                        Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + line.Param.DispenseGap.ToString("0.000000"));
                        ret = Machine.Instance.Robot.MovePosZAndReply(z, line.Param.DownSpeed, line.Param.DownAccel);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }
                    else
                    {
                        Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + line.Param.DispenseGap.ToString("0.000000"));
                        ret = Machine.Instance.Robot.MovePosZAndReply(z, line.Param.DownSpeed, line.Param.DownAccel);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                        // 移动到加速区间起始位置
                        Log.Dprint("move to position XY : " + valveAccStart);
                        if (directiveLine.RunnableModule.Mode == ModuleMode.MainMode)
                        {
                            ret = Machine.Instance.Robot.MovePosXYABAndReply(valveAccStart, simulStart,
                                FluidProgram.Current.MotionSettings.VelXYAB,
                                FluidProgram.Current.MotionSettings.AccXYAB,
                                (int)Machine.Instance.Setting.CardSelect);
                        }
                        else
                        {
                            ret = Machine.Instance.Robot.MovePosXYAndReply(valveAccStart,
                                FluidProgram.Current.MotionSettings.VelXY,
                                FluidProgram.Current.MotionSettings.AccXY);
                        }
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(cameraLineStart,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                }
            }

            // 移动： 加速区间--线段区间--减速区间
            Log.Dprint("fluid line, accStart=" + cameraAccStart + " start=" + cameraLineStart + ", end=" + cameraLineEnd + ", decelEnd=" + cameraDecEnd + ", spped=" + speed);
            //printPoints(newPoints);
            if (directiveLine.RunnableModule.Mode == ModuleMode.MainMode)
            {
                ret = Machine.Instance.DualValve.FluidLine(cameraAccStart, cameraLineStart, cameraLineEnd, cameraDecEnd, speed, cameraPoints, intervalSec, simulStart, cameraPoints2, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
            }
            else if (directiveLine.RunnableModule.Mode == ModuleMode.DualFallow)
            {
                ret = Machine.Instance.DualValve.FluidLine(cameraAccStart, cameraLineStart, cameraLineEnd, cameraDecEnd, speed, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
            }
            else if (directiveLine.RunnableModule.Mode == ModuleMode.AssignMode2)
            {
                ret = Machine.Instance.Valve2.FluidLine(cameraAccStart, cameraLineStart, cameraLineEnd, cameraDecEnd, speed, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
            }
            else 
            {
                ret = Machine.Instance.Valve1.FluidLine(cameraAccStart, cameraLineStart, cameraLineEnd, cameraDecEnd, speed, cameraPoints, intervalSec, FluidProgram.Current.MotionSettings.WeightAcc);
                GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, points.Length * directiveLine.Program.RuntimeSettings.SingleDropWeight);
            }
            if (!ret.IsOk)
            {
                return ret;
            }

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Dry)
            {
                Log.Dprint("RetractDistance : " + line.Param.RetractDistance);
                // 减速区间运动完成后，x y 轴速度为0， 此时需要抬升一段高度
                if (line.Param.RetractDistance > 0)
                {
                    Log.Dprint("move up RetractDistance : " + line.Param.RetractDistance);
                    ret = Machine.Instance.Robot.MoveIncZAndReply(line.Param.RetractDistance, line.Param.RetractSpeed, line.Param.RetractAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 计算在所有模式下，执行重量线或胶量模式时线命令所需要的参数
        /// </summary>
        private void CalculateWtLineParam(LineCoordinate line,Line directiveLine,out PointD accStartPoint,out PointD decelEndPoint,out double runSpeed,out PointD[] linePoints,out double fluidIntervalSec)
        {
            double accDistance = 0, decelDistance = 0;
            double lineDistance = line.CalculateDistance();
            // 计算点胶数量
            int shots = 0;
            if (directiveLine.LineCmd.lineCmdLine.IsWholeWtMode && directiveLine.LineCmd.lineCmdLine.LineMethod != LineMethod.Single)
            {
                shots = line.Dots;
            }
            else
            {
                if (directiveLine.Program.RuntimeSettings.isHalfAdjust)
                {
                    // 四舍五入
                    shots = (int)Math.Round((decimal)line.Weight / (decimal)directiveLine.Program.RuntimeSettings.SingleDropWeight, MidpointRounding.ToEven);
                }
                else
                {
                    shots = (int)((decimal)line.Weight / (decimal)directiveLine.Program.RuntimeSettings.SingleDropWeight);
                }
            }
            // 计算时间 (需要将微秒单位转换成秒）
            double totalTime = Machine.Instance.Valve1.SpraySec * shots;
            // 计算速度
            double wtSpeed = lineDistance / totalTime;
            if (line.Param.WtCtrlSpeedValueType != LineParam.ValueType.COMPUTE)
            {
                wtSpeed = line.Param.WtCtrlSpeed > wtSpeed ? wtSpeed : line.Param.WtCtrlSpeed;
            }
            // 运动速度不能超过设定的最大速度
            wtSpeed = wtSpeed > FluidProgram.Current.MotionSettings.WeightMaxVel ? FluidProgram.Current.MotionSettings.WeightMaxVel : wtSpeed;
            Log.Dprint(string.Format("lineDistance={0}, weight={1} totalTime={2}, param.WtCtrlSpeedValueType={3}, param.WtCtrlSpeed={4}, wtSpeed={5}",
                lineDistance, line.Weight, totalTime, line.Param.WtCtrlSpeedValueType, line.Param.WtCtrlSpeed, wtSpeed));

            // 计算加速区间距离
            if (line.Param.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                accDistance = MathUtils.CalculateDistance(0, wtSpeed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                Log.Dprint("Acc Distance COMPUTE wtSpeed=" + wtSpeed + ", WeightAcc=" + FluidProgram.Current.MotionSettings.WeightAcc
                    + ", distance=" + accDistance);
            }
            else
            {
                accDistance = line.Param.AccelDistance;
            }
            // 计算减速区间距离
            if (line.Param.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                decelDistance = MathUtils.CalculateDistance(wtSpeed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                Log.Dprint("Decel Distance COMPUTE wtSpeed=" + wtSpeed + ", WeightAcc=" + FluidProgram.Current.MotionSettings.WeightAcc
                    + ", distance=" + decelDistance);
            }
            else
            {
                decelDistance = line.Param.DecelDistance;
            }
            // 计算加速区间起始位置和减速区间结束位置
            PointD accStart = calAccStartPosition(line, accDistance);
            PointD decelEnd = calDecelEndPosition(line, decelDistance);
            // 计算点胶位置
            PointD[] points = calFixedTotalOfDots(line, shots);

            //基于时间控制
            totalTime = lineDistance / wtSpeed;
            double intervalSec = totalTime / (shots - 1);
            double dispenseSec = Machine.Instance.Valve1.SpraySec;
            if (intervalSec < dispenseSec)
            {
                intervalSec = dispenseSec;
                points = calFixedTotalOfDots(line, (int)(totalTime / intervalSec) + 1);
            }

            accStartPoint = accStart;
            decelEndPoint = decelEnd;
            runSpeed = wtSpeed;
            linePoints = points;
            fluidIntervalSec = intervalSec;
        }

        /// <summary>
        /// 计算在所有模式下，执行普通线或速度模式时线命令所需要的参数
        /// </summary>
        private void CalculateNormalLineParam(LineCoordinate line, Line directiveLine, out PointD accStartPoint, out PointD decelEndPoint, out double runSpeed, out PointD[] linePoints, out double fluidIntervalSec)
        {
            double accDistance = 0, decelDistance = 0;
            // 运动速度不能超过设定的最大速度
            double speed = line.Param.Speed > FluidProgram.Current.MotionSettings.WorkMaxVel ? FluidProgram.Current.MotionSettings.WorkMaxVel : line.Param.Speed;
            // 计算加速区间距离
            if (line.Param.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                accDistance = MathUtils.CalculateDistance(0, speed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                Log.Dprint("Acc Distance COMPUTE speed=" + speed + ", WeightAcc=" + FluidProgram.Current.MotionSettings.WeightAcc
                    + ", distance=" + accDistance);
            }
            else
            {
                accDistance = line.Param.AccelDistance;
            }
            // 计算减速区间距离
            if (line.Param.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                decelDistance = MathUtils.CalculateDistance(speed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.Current.MotionSettings.WeightAcc));
                Log.Dprint("Decel Distance COMPUTE speed=" + speed + ", WeightAcc=" + FluidProgram.Current.MotionSettings.WeightAcc
                    + ", distance=" + decelDistance);
            }
            else
            {
                decelDistance = line.Param.DecelDistance;
            }

            // 计算加速区间起始位置和减速区间结束位置
            PointD accStart = calAccStartPosition(line, accDistance);
            PointD decelEnd = calDecelEndPosition(line, decelDistance);
            // 计算点胶位置
            double intervalSec;
            PointD[] points = calNormalLinePoints(line, speed, out intervalSec);

            accStartPoint = accStart;
            decelEndPoint = decelEnd;
            runSpeed = speed;
            linePoints = points;
            fluidIntervalSec = intervalSec;
        }

        /// <summary>
        /// 根据加速区间，计算加速起始位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="accDistance"></param>
        /// <returns></returns>
        private PointD calAccStartPosition(LineCoordinate line, double accDistance)
        {
            VectorD v = (line.Start - line.End).Normalize() * accDistance;
            return line.Start + v;

            //PointD position = new PointD();
            //double lineDistance = MathUtils.Distance(line.Start, line.End);
            //double cos = (line.End.X - line.Start.X) / lineDistance;
            //double sin = (line.End.Y - line.Start.Y) / lineDistance;
            //position.X = line.Start.X - accDistance * cos;
            //position.Y = line.Start.Y - accDistance * sin;
            //return position;
        }

        private PointD calAccStartPosition(PointD start, PointD end, double accDistance)
        {
            VectorD v = (start - end).Normalize() * accDistance;
            return start + v;
        }

        /// <summary>
        /// 根据减速区间，计算减速结束位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="decelDistance"></param>
        /// <returns></returns>
        private PointD calDecelEndPosition(LineCoordinate line, double decelDistance)
        {
            VectorD v = (line.End - line.Start).Normalize() * decelDistance;
            return line.End + v;

            //PointD position = new PointD();
            //double lineDistance = MathUtils.Distance(line.Start, line.End);
            //double cos = (line.End.X - line.Start.X) / lineDistance;
            //double sin = (line.End.Y - line.Start.Y) / lineDistance;
            //position.X = line.End.X + decelDistance * cos;
            //position.Y = line.End.Y + decelDistance * sin;
            //return position;
        }

        private PointD calDecelEndPosition(PointD start, PointD end, double decelDistance)
        {
            VectorD v = (end - start).Normalize() * decelDistance;
            return end + v;
        }

        /// <summary>
        /// 计算普通线段上每个点胶位置
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private PointD[] calNormalLinePoints(LineCoordinate line, double speed, out double intervalSec)
        {
            double totalTime = line.CalculateDistance() / speed;
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
            if (line.Param.ControlMode == LineParam.CtrlMode.POS_BASED_SPACING)
            {
                points = calFixedTotalOfDots(line, (int)(line.CalculateDistance() / line.Param.Spacing) + 1);
                intervalSec = totalTime / (points.Length - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(line, (int)(totalTime / intervalSec) + 1);
                }
            }
            else if (line.Param.ControlMode == LineParam.CtrlMode.TIME_BASED_SPACING)
            {
                intervalSec = line.Param.ShotTimeInterval / 1000.0 < dispenseTime ? dispenseTime : line.Param.ShotTimeInterval / 1000.0;
                points = calFixedTotalOfDots(line, (int)(totalTime / intervalSec) + 1);
            }
            else if (line.Param.ControlMode == LineParam.CtrlMode.TOTAL_OF_DOTS && line.Param.TotalOfDots > 0)
            {
                intervalSec = totalTime / (line.Param.TotalOfDots - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(line, (int)(totalTime / intervalSec) + 1);
                }
                else
                {
                    points = calFixedTotalOfDots(line, line.Param.TotalOfDots);
                }
            }
            else
            {
                points = new PointD[0];
            }
            return points;
        }

        private PointD[] calNormalLinePoints(PointD start, PointD end, LineParam lineParam, double speed, out double intervalSec)
        {
            double lineDistance = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            double totalTime = lineDistance / speed;
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
            if (lineParam.ControlMode == LineParam.CtrlMode.POS_BASED_SPACING)
            {
                points = calFixedTotalOfDots(start, end, (int)(lineDistance / lineParam.Spacing) + 1);
                intervalSec = totalTime / (points.Length - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(start, end, (int)(totalTime / intervalSec) + 1);
                }
            }
            else if (lineParam.ControlMode == LineParam.CtrlMode.TIME_BASED_SPACING)
            {
                intervalSec = lineParam.ShotTimeInterval / 1000.0 < dispenseTime ? dispenseTime : lineParam.ShotTimeInterval / 1000.0;
                points = calFixedTotalOfDots(start, end, (int)(totalTime / intervalSec) + 1);
            }
            else if (lineParam.ControlMode == LineParam.CtrlMode.TOTAL_OF_DOTS && lineParam.TotalOfDots > 0)
            {
                intervalSec = totalTime / (lineParam.TotalOfDots - 1);
                if (intervalSec < dispenseTime)
                {
                    intervalSec = dispenseTime;
                    points = calFixedTotalOfDots(start, end, (int)(totalTime / intervalSec) + 1);
                }
                else
                {
                    points = calFixedTotalOfDots(start, end, lineParam.TotalOfDots);
                }
            }
            else
            {
                points = new PointD[0];
            }
            return points;
        }

        /// <summary>
        /// 对于一条线段上固定点胶数的模式，计算所有点胶位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="totalOfDots"></param>
        /// <returns></returns>
        private PointD[] calFixedTotalOfDots(LineCoordinate line, int totalOfDots)
        {
            PointD[] points = null;
            if (totalOfDots <= 0)
            {
                points = new PointD[0];
                return points;
            }
            else
            {
                points = new PointD[totalOfDots];
                if (totalOfDots == 1)
                {
                    points[0] = new PointD((line.End.X + line.Start.X) / 2, (line.End.Y + line.Start.Y) / 2);
                    return points;
                }
                else
                {
                    double dx = (line.End.X - line.Start.X) / (totalOfDots - 1);
                    double dy = (line.End.Y - line.Start.Y) / (totalOfDots - 1);
                    for (int i = 0; i < totalOfDots; i++)
                    {
                        points[i] = new PointD(line.Start.X + dx * i, line.Start.Y + dy * i);
                    }

                    //根据首尾重复情况去掉首尾点
                    PointD[] dispensPoints;
                    if (line.Repetition.TotalRepeat)
                    {
                        dispensPoints = new PointD[points.Length - 2];
                        for (int i = 0; i < dispensPoints.Length; i++)
                        {
                            dispensPoints[i] = points[i + 1];
                        }
                    }
                    else if (!line.Repetition.TotalRepeat && line.Repetition.StartIsRepeat)
                    {
                        dispensPoints = new PointD[points.Length - 1];
                        for (int i = 0; i < dispensPoints.Length; i++)
                        {
                            dispensPoints[i] = points[i + 1];
                        }
                    }
                    else if (!line.Repetition.TotalRepeat && line.Repetition.EndIsRepeat)
                    {
                        dispensPoints = new PointD[points.Length - 1];
                        for (int i = 0; i < dispensPoints.Length; i++)
                        {
                            dispensPoints[i] = points[i];
                        }
                    }
                    else
                    {
                        dispensPoints = points;
                    }

                    return dispensPoints;
                }

            }

        }

        private PointD[] calFixedTotalOfDots(PointD start, PointD end, int totalOfDots)
        {
            PointD[] points = null;
            if (totalOfDots <= 0)
            {
                points = new PointD[0];
                return points;
            }
            else
            {
                points = new PointD[totalOfDots];
                if (totalOfDots == 1)
                {
                    points[0] = new PointD((end.X + start.X) / 2, (end.Y + start.Y) / 2);
                    return points;
                }
                else
                {
                    double dx = (end.X - start.X) / (totalOfDots - 1);
                    double dy = (end.Y - start.Y) / (totalOfDots - 1);
                    for (int i = 0; i < totalOfDots; i++)
                    {
                        points[i] = new PointD(start.X + dx * i, start.Y + dy * i);
                    }
                    return points;
                }

            }

        }

        private void sprayLine(Line line,Valve valve, int shots, double intervalSec)
        {
            valve.MoveToScaleLoc();
            valve.SprayCycle((short)shots, (short)(intervalSec - valve.Prm.JtValvePrm.OnTime));
            GlueManagerMgr.Instance.UpdateGlueRemainWeight((int)valve.Key, shots * line.Program.RuntimeSettings.SingleDropWeight);
            line.shortNum += shots;
        }

        private double CalculateVel(LineCoordinate line, double lineDistance, double totalTime)
        {
            // 计算速度
            double wtSpeed = lineDistance / totalTime;
            if (line.Param.WtCtrlSpeedValueType != LineParam.ValueType.COMPUTE)
            {
                wtSpeed = line.Param.WtCtrlSpeed > wtSpeed ? wtSpeed : line.Param.WtCtrlSpeed;
            }
            // 运动速度不能超过设定的最大速度
            wtSpeed = wtSpeed > FluidProgram.Current.MotionSettings.WeightMaxVel ? FluidProgram.Current.MotionSettings.WeightMaxVel : wtSpeed;
            Log.Dprint(string.Format("lineDistance={0}, weight={1} totalTime={2}, param.WtCtrlSpeedValueType={3}, param.WtCtrlSpeed={4}, wtSpeed={5}",
                lineDistance, line.Weight, totalTime, line.Param.WtCtrlSpeedValueType, line.Param.WtCtrlSpeed, wtSpeed));
            return wtSpeed;
        }

        /// <summary>
        /// 计算在手动打线模式下，重量线需要的各种参数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="lineParam"></param>
        /// <param name="weight"></param>
        /// <param name="accStartPoint"></param>
        /// <param name="decelEndPoint"></param>
        /// <param name="runSpeed"></param>
        /// <param name="linePoints"></param>
        /// <param name="fluidIntervalSec"></param>
        private void GetManualWtLineParam(PointD start, PointD end, LineParam lineParam, double weight, out PointD accStartPoint, out PointD decelEndPoint, out double runSpeed, out PointD[] linePoints, out double fluidIntervalSec)
        {
            double accDistance = 0, decelDistance = 0;
            double lineDistance = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));

            // 计算点胶数量
            int shots = (int)((decimal)weight / (decimal)FluidProgram.CurrentOrDefault().RuntimeSettings.SingleDropWeight);

            // 计算时间 (需要将微秒单位转换成秒）
            double totalTime = Machine.Instance.Valve1.SpraySec * shots;
            // 计算速度
            double wtSpeed = lineDistance / totalTime;
            if (lineParam.WtCtrlSpeedValueType != LineParam.ValueType.COMPUTE)
            {
                wtSpeed = lineParam.WtCtrlSpeed > wtSpeed ? wtSpeed : lineParam.WtCtrlSpeed;
            }

            // 运动速度不能超过设定的最大速度
            wtSpeed = wtSpeed > FluidProgram.CurrentOrDefault().MotionSettings.WeightMaxVel ? FluidProgram.CurrentOrDefault().MotionSettings.WeightMaxVel : wtSpeed;
            Log.Dprint(string.Format("lineDistance={0}, weight={1} totalTime={2}, param.WtCtrlSpeedValueType={3}, param.WtCtrlSpeed={4}, wtSpeed={5}",
                lineDistance, weight, totalTime, lineParam.WtCtrlSpeedValueType, lineParam.WtCtrlSpeed, wtSpeed));

            // 计算加速区间距离
            if (lineParam.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                accDistance = MathUtils.CalculateDistance(0, wtSpeed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc));
                Log.Dprint("Acc Distance COMPUTE wtSpeed=" + wtSpeed + ", WeightAcc=" + FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc
                    + ", distance=" + accDistance);
            }
            else
            {
                accDistance = lineParam.AccelDistance;
            }

            // 计算减速区间距离
            if (lineParam.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                decelDistance = MathUtils.CalculateDistance(wtSpeed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc));
                Log.Dprint("Decel Distance COMPUTE wtSpeed=" + wtSpeed + ", WeightAcc=" + FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc
                    + ", distance=" + decelDistance);
            }
            else
            {
                decelDistance = lineParam.DecelDistance;
            }
            // 计算加速区间起始位置和减速区间结束位置
            PointD accStart = calAccStartPosition(start, end, accDistance);
            PointD decelEnd = calDecelEndPosition(start, end, decelDistance);
            // 计算点胶位置
            PointD[] points = calFixedTotalOfDots(start, end, shots);

            //基于时间控制
            totalTime = lineDistance / wtSpeed;
            double intervalSec = totalTime / (shots - 1);
            double dispenseSec = Machine.Instance.Valve1.SpraySec;
            if (intervalSec < dispenseSec)
            {
                intervalSec = dispenseSec;
                points = calFixedTotalOfDots(start, end, (int)(totalTime / intervalSec) + 1);
            }

            accStartPoint = accStart;
            decelEndPoint = decelEnd;
            runSpeed = wtSpeed;
            linePoints = points;
            fluidIntervalSec = intervalSec;
        }

        /// <summary>
        /// 计算在手动打线模式下，普通线需要的各种参数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="lineParam"></param>
        /// <param name="accStartPoint"></param>
        /// <param name="decelEndPoint"></param>
        /// <param name="runSpeed"></param>
        /// <param name="linePoints"></param>
        /// <param name="fluidIntervalSec"></param>
        private void GetManualNormalLineParam(PointD start, PointD end, LineParam lineParam, out PointD accStartPoint, out PointD decelEndPoint, out double runSpeed, out PointD[] linePoints, out double fluidIntervalSec)
        {
            double accDistance = 0, decelDistance = 0;
            // 运动速度不能超过设定的最大速度
            double speed = lineParam.Speed > FluidProgram.CurrentOrDefault().MotionSettings.WorkMaxVel ? FluidProgram.CurrentOrDefault().MotionSettings.WorkMaxVel : lineParam.Speed;
            // 计算加速区间距离
            if (lineParam.AccelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                accDistance = MathUtils.CalculateDistance(0, speed, Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc));
                Log.Dprint("Acc Distance COMPUTE speed=" + speed + ", WeightAcc=" + FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc
                    + ", distance=" + accDistance);
            }
            else
            {
                accDistance = lineParam.AccelDistance;
            }
            // 计算减速区间距离
            if (lineParam.DecelDistanceValueType == LineParam.ValueType.COMPUTE)
            {
                decelDistance = MathUtils.CalculateDistance(speed, 0, -Machine.Instance.Robot.AxisX.ConvertAcc2Mm(FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc));
                Log.Dprint("Decel Distance COMPUTE speed=" + speed + ", WeightAcc=" + FluidProgram.CurrentOrDefault().MotionSettings.WeightAcc
                    + ", distance=" + decelDistance);
            }
            else
            {
                decelDistance = lineParam.DecelDistance;
            }

            // 计算加速区间起始位置和减速区间结束位置
            PointD accStart = calAccStartPosition(start, end, accDistance);
            PointD decelEnd = calDecelEndPosition(start, end, decelDistance);
            // 计算点胶位置
            double intervalSec;
            PointD[] points = calNormalLinePoints(start, end, lineParam, speed, out intervalSec);

            accStartPoint = accStart;
            decelEndPoint = decelEnd;
            runSpeed = speed;
            linePoints = points;
            fluidIntervalSec = intervalSec;
        }
    }
}
