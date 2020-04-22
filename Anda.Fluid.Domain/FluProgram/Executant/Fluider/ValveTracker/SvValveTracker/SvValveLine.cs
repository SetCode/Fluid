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

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.SvValveTracker
{
    public class SvValveLine : LineTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            //TODO 该逻辑暂时没有
            return Result.OK;
        }

        public override Result DryExecute(Directive directive)
        {
            Line line = directive as Line;

            return this.SvValveWetAndDryLogic(line);
        }

        public override Result InspectDotExecute(Directive directive)
        {
            //TODO螺杆阀没有此逻辑
            return Result.OK;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            Line line = directive as Line;

            return this.SvValveInspectLogic(line);
        }

        public override Result LookExecute(Directive directive)
        {
            Line line = directive as Line;

            return this.SvValveCVLogic(line);

        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            //TODO螺杆阀没有此逻辑
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            Line line = directive as Line;

            return this.SvValveWetAndDryLogic(line);
        }

        /// <summary>
        /// 计算螺杆阀的所有线命令的点坐标
        /// </summary>
        /// <returns></returns>
        private List<PointD> CaculateSvValvePoints(Line line)
        {
            List<PointD> list = new List<PointD>();
            if (line.LineCmd.lineCmdLine.LineMethod == LineMethod.Poly)
            {
                for (int i = 0; i < line.LineCoordinateList.Count; i++)
                {
                    list.Add(line.LineCoordinateList[i].Start);
                }
                list.Add(line.LineCoordinateList[line.LineCoordinateList.Count - 1].End);
            }
            else
            {
                for (int i = 0; i < line.LineCoordinateList.Count; i++)
                {
                    list.Add(line.LineCoordinateList[i].Start);
                    list.Add(line.LineCoordinateList[i].End);
                }
            }
            return list;
        }

        /// <summary>
        /// 螺杆阀CV模式模式下的运行逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveCVLogic(Line line)
        {
            Result ret = Result.OK;

            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            //List<PointD> points = this.CaculateSvValvePoints(line);
            //for (int i = 0; i < points.Count; i++)
            //{
            //    ret = Machine.Instance.Robot.MovePosXYAndReply(points[i],
            //        FluidProgram.Current.MotionSettings.VelXY,
            //        FluidProgram.Current.MotionSettings.AccXY);
            //    if (!ret.IsOk)
            //    {
            //        return ret;
            //    }
            //    Log.Print("current position in LookMode: " + points[i].ToString());
            //}

            if (line.LineCmd.lineCmdLine.LineMethod == LineMethod.Poly)
            {
                ret = Machine.Instance.Robot.MovePosXYAndReply(line.LineCoordinateList[0].Start,
                    FluidProgram.Current.MotionSettings.VelXY,
                    FluidProgram.Current.MotionSettings.AccXY);
                if (!ret.IsOk) return ret;

                for (int i = 0; i < line.LineCoordinateList.Count; i++)
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(line.LineCoordinateList[i].End,
                         line.Param.Speed,
                         FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk) return ret;
                }
            }
            else
            {
                for (int i = 0; i < line.LineCoordinateList.Count; i++)
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(line.LineCoordinateList[i].Start,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk) return ret;

                    ret = Machine.Instance.Robot.MovePosXYAndReply(line.LineCoordinateList[i].End,
                       line.Param.Speed,
                       FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk) return ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// 螺杆阀Inspect模式下的运行逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveInspectLogic(Line line)
        {
            Result ret = Result.OK;

            List<PointD> points = this.CaculateSvValvePoints(line);

            if (line.LineCmd.lineCmdLine == null || points == null)
            {
                return ret;
            }
            if (line.LineCmd.lineCmdLine.LineMethod != LineMethod.Single)
            {
                return ret;
            }

            for (int i = 0; i < points.Count; i++)
            {
                //为了去到线段的起点，会跳过list的奇数部分（终点）
                if (i % 2 == 1)
                {
                    continue;
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(points[i],
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    LineCmdLine lineCmdline = line.LineCmd.lineCmdLine;
                    InspectionLine inspectionLine = InspectionMgr.Instance.FindBy((int)lineCmdline.inspectionKey) as InspectionLine;
                    if (inspectionLine != null)
                    {
                        Thread.Sleep(inspectionLine.SettlingTime);
                        double width1, width2;
                        Machine.Instance.CaptureAndInspect(inspectionLine);
                        width1 = inspectionLine.PhyWidth1;
                        width2 = inspectionLine.PhyWidth2;
                        Log.Dprint(inspectionLine.CurrResultStr);
                        string resline = string.Format("{0},{1},{2},{3}", Math.Round(points[i].X, 3), Math.Round(points[i].Y, 3), Math.Round(width1, 3), Math.Round(width2, 3));
                        CsvUtil.WriteLine(line.Program.RuntimeSettings.FilePathInspectRect, resline);
                        Thread.Sleep(inspectionLine.DwellTime);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 螺杆阀Wet和Dry模式下的运行逻辑
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private Result SvValveWetAndDryLogic(Line line)
        {
            Result ret = Result.OK;

            List<PointD> points = this.CaculateSvValvePoints(line);

            //主阀坐标点
            List<PointD> primaryPoints = new List<PointD>();
            //副阀坐标点
            List<PointD> simulPoints = new List<PointD>();

            //将相机坐标位置转换为阀的位置
            for (int i = 0; i < points.Count; i++)
            {
                primaryPoints.Add(points[i].ToNeedle(line.Valve));
            }

            //为主阀的坐标点进行偏移调整
            for (int i = 0; i < line.LineCoordinateList.Count; i++)
            {
                VectorD v = (line.LineCoordinateList[i].End - line.LineCoordinateList[i].Start).Normalize() *
                    (line.LineCoordinateList[i].Param.Offset + line.LineCoordinateList[i].LookOffset);
                if (line.LineCmd.lineCmdLine.LineMethod != LineMethod.Poly)
                {
                    primaryPoints[i * 2] += v;
                    primaryPoints[i * 2 + 1] += v;
                }
                else
                {
                    primaryPoints[i] += v;
                    if (i == line.LineCoordinateList.Count - 1)
                    {
                        primaryPoints[(primaryPoints.Count - 1)] += v;
                    }
                }

            }

            //生成副阀的坐标点
            for (int i = 0; i < primaryPoints.Count; i++)
            {
                simulPoints.Add(this.GetSimulPos(line,primaryPoints[i]));
            }

            if (line.LineCmd.lineCmdLine.LineMethod != LineMethod.Poly)
            {
                ret = this.SvValveNotPolyLogic(line,primaryPoints, simulPoints);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            else
            {
                ret = this.SvValvePolyLogic(line,primaryPoints, simulPoints);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private PointD GetSimulPos(Line line,PointD pos)
        {
            PointD simulPos = new PointD();
            ///生成副阀相关参数(起点、插补点位)
            if (line.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - line.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -Math.Abs(simulPos.Y) / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(line.Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(line.Program.RuntimeSettings.SimulOffsetX, line.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }

        /// <summary>
        /// 螺杆阀的非Polyline的打胶逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveNotPolyLogic(Line line,List<PointD> primaryPoints, List<PointD> simulPoints)
        {
            Result ret = Result.OK;

            //如果坐标点总数不是偶数(正常情况下不可能存在)
            if (primaryPoints.Count % 2 != 0)
            {
                return Result.FAILED;
            }

            for (int i = 0; i < (primaryPoints.Count / 2); i++)
            {
                SvValveFludLineParam primaryLineParam = this.GetSvValveParam(line,primaryPoints[0 * 2], primaryPoints[0 * 2 + 1], line.LineCoordinateList[i]);
                SvValveFludLineParam simulLineParam = this.GetSvValveParam(line,simulPoints[0 * 2], simulPoints[0 * 2 + 1], line.LineCoordinateList[i]);

                double currZ = Machine.Instance.Robot.PosZ;
                double targZ = 0;
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    targZ = line.Program.RuntimeSettings.BoardZValue + line.LineCoordinateList[i].Param.DispenseGap;
                }
                else
                {
                    targZ = Converter.NeedleBoard2Z(line.LineCoordinateList[i].Param.DispenseGap, line.CurMeasureHeightValue);
                }

                ret = this.SvValveMoveToLineStart(line,primaryPoints[i * 2], simulPoints[i * 2], currZ, targZ, line.LineCoordinateList[i].Param);
                if (!ret.IsOk)
                {
                    return ret;
                }

                ret = this.SvValveStartSpary(line,primaryLineParam, simulLineParam);
                if (!ret.IsOk)
                {
                    return ret;
                }

                ret = this.SvValveStopSpary(line,line.LineCoordinateList[i].Param);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// 螺杆阀的Poly线的打胶逻辑
        /// </summary>
        /// <param name="primaryPoints"></param>
        /// <param name="simulPoints"></param>
        /// <returns></returns>
        private Result SvValvePolyLogic(Line line,List<PointD> primaryPoints, List<PointD> simulPoints)
        {
            Result ret = Result.OK;

            SvValveFludLineParam primaryLineParam = this.GetSvValveParam(line,primaryPoints, line.LineCoordinateList);
            SvValveFludLineParam simulLineParam = this.GetSvValveParam(line,simulPoints, line.LineCoordinateList);

            double currZ = Machine.Instance.Robot.PosZ;
            double targZ = 0;
            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = line.Program.RuntimeSettings.BoardZValue + line.LineCoordinateList[0].Param.DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(line.LineCoordinateList[0].Param.DispenseGap, line.CurMeasureHeightValue);
            }

            ret = this.SvValveMoveToLineStart(line,primaryPoints[0], simulPoints[0], currZ, targZ, line.LineCoordinateList[0].Param);
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStartSpary(line,primaryLineParam, simulLineParam);
            if (!ret.IsOk)
            {
                return ret;
            }

            ret = this.SvValveStopSpary(line,line.LineCoordinateList[0].Param);
            if (!ret.IsOk)
            {
                return ret;
            }

            return ret;
        }

        /// <summary>
        /// 计算螺杆阀运行非Poly线时所需的各个参数
        /// </summary>
        /// <param name="primaryPoints"></param>
        /// <param name="simulPoints"></param>
        /// <param name="lineParam"></param>
        /// <returns></returns>
        private SvValveFludLineParam GetSvValveParam(Line line,PointD startPos, PointD endPos, LineCoordinate lineCoordinate)
        {
            double startPosDelay = lineCoordinate.Param.PreMoveDelay;
            PointD endPoint = endPos;
            PointD[] transPoints = new PointD[0];//非Poly线为空

            //计算线段的角度
            double angleOfLine = Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X) * 180 / Math.PI;
            //线段总长度
            double sumDistance = Math.Sqrt(Math.Pow(endPos.Y - startPos.Y, 2) + Math.Pow(endPos.X - startPos.X, 2));

            //计算以起点为基准的关胶距离
            double shutOffDistance = Math.Abs(sumDistance - lineCoordinate.Param.ShutOffDistance);
            PointD stopSprayPos = new PointD();
            stopSprayPos.X = Math.Round(startPos.X + shutOffDistance * Math.Cos(angleOfLine * 2 * Math.PI / 360), 3);
            stopSprayPos.Y = Math.Round(startPos.Y + shutOffDistance * Math.Sin(angleOfLine * 2 * Math.PI / 360), 3);

            PointD backTrackPos = new PointD();

            //计算以起点为基准的到回走时的终点距离
            double backTrackDistance = sumDistance * (1 - lineCoordinate.Param.BacktrackDistance * 0.01);
            backTrackPos.X = Math.Round(startPos.X + backTrackDistance * Math.Cos(angleOfLine * 2 * Math.PI / 360), 3);
            backTrackPos.Y = Math.Round(startPos.Y + backTrackDistance * Math.Sin(angleOfLine * 2 * Math.PI / 360), 3);

            PointD[] backTransPoints = new PointD[0];

            //计算线速度，分重量线和普通线
            double[] vel = new double[1];
            if (line.LineCmd.IsWeightControl)
            {
                double lineWeight = lineCoordinate.Weight;
                double flowVel;
                FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(FluidProgram.Current.RuntimeSettings.SvOrGearValveCurrSpeed, out flowVel);

                double time = lineWeight * flowVel;

                vel[0] = sumDistance / time;
            }
            else
            {
                vel[0] = lineCoordinate.Param.Speed;
            }

            double backTrackDelay = lineCoordinate.Param.Dwell;
            double backTrackGap = lineCoordinate.Param.BacktrackGap;
            double backTrackVel = lineCoordinate.Param.BacktrackSpeed;

            return new SvValveFludLineParam()
            {
                startPosDelay = startPosDelay,
                endPos = endPos,
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
        /// 计算螺杆阀运行Poly线时所需要的各个参数
        /// </summary>
        /// <param name="points"></param>
        /// <param name="lineParam"></param>
        /// <returns></returns>
        private SvValveFludLineParam GetSvValveParam(Line line,List<PointD> points, List<LineCoordinate> lineCoordinateList)
        {
            double startPosDelay = lineCoordinateList[0].Param.PreMoveDelay;
            PointD endPoint = points[points.Count - 1];

            PointD[] transPoints = new PointD[points.Count - 1];
            for (int i = 1; i < points.Count; i++)
            {
                transPoints[i - 1] = points[i];
            }

            PointD stopSprayPos = this.GetPointOnPoly(points, lineCoordinateList[0].Param.ShutOffDistance, false).Item1;

            //回走时的终点位置，Poly线不用管这个参数
            PointD backTrackpos = new PointD();

            //将poly线反向
            List<PointD> backPoints = new List<PointD>();
            for (int i = 0; i < points.Count; i++)
            {
                backPoints.Add(points[points.Count - 1 - i]);
            }

            PointD[] backTransPoints = this.GetPointOnPoly(backPoints, lineCoordinateList[0].Param.BacktrackDistance, true).Item2;

            double[] vels = new double[line.LineCoordinateList.Count];

            for (int i = 0; i < line.LineCoordinateList.Count; i++)
            {
                if (line.LineCmd.IsWeightControl)
                {
                    double distance = Math.Sqrt(Math.Pow(line.LineCoordinateList[i].End.Y - line.LineCoordinateList[i].Start.Y,
                                         2) + Math.Pow(line.LineCoordinateList[i].End.X - line.LineCoordinateList[i].Start.X, 2));
                    double lineWeight = line.LineCoordinateList[i].Weight;
                    double flowVel;
                    FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(FluidProgram.Current.RuntimeSettings.SvOrGearValveCurrSpeed, out flowVel);
                    double time = lineWeight * flowVel;
                    vels[i] = distance / time;
                }
                else
                {
                    vels[i] = line.LineCoordinateList[i].Param.Speed;
                }
            }

            double backTrackDelay = line.LineCoordinateList[0].Param.Dwell;
            double backTrackGap = line.LineCoordinateList[0].Param.BacktrackGap;
            double backTrackVel = line.LineCoordinateList[0].Param.BacktrackSpeed;

            return new SvValveFludLineParam()
            {
                startPosDelay = startPosDelay,
                endPos = endPoint,
                transPoints = transPoints,
                stopSprayPos = stopSprayPos,
                backTrackPos = backTrackpos,
                backTransPoints = backTransPoints,
                vels = vels,
                backTrackDelay = backTrackDelay,
                backTrackGap = backTrackGap,
                backTrackVel = backTrackVel
            };

        }

        /// <summary>
        /// 螺杆阀移动到线的起点
        /// </summary>
        /// <returns></returns>
        private Result SvValveMoveToLineStart(Line line,PointD pos, PointD simulPos, double currZ, double targZ, LineParam param)
        {
            Result ret = Result.OK;

            if (currZ > targZ)
            {
                // 移动到指定位置
                Log.Dprint("move to Line Start position XY : " + pos);
                if (line.RunnableModule.Mode == ModuleMode.MainMode)
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
                    z = Converter.NeedleBoard2Z(param.DispenseGap, line.CurMeasureHeightValue);
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
                    z = Converter.NeedleBoard2Z(param.DispenseGap, line.CurMeasureHeightValue);
                }
                Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, param.DownSpeed, param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }

                Log.Dprint("move to Line Start position XY : " + pos);
                if (line.RunnableModule.Mode == ModuleMode.MainMode)
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

        private Result SvValveStartSpary(Line line,SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam)
        {
            Result ret = Result.OK;
            if (line.RunnableModule.Mode == ModuleMode.MainMode && Machine.Instance.Setting.DualValveMode == DualValveMode.异步)
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

        private Result SvValveStopSpary(Line line,LineParam param)
        {
            Result ret = Result.OK;

            // 抬高一段距离 Retract Distance
            if (param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(param.RetractDistance, param.RetractSpeed, param.RetractAccel);
            }

            if (line.RunnableModule.Mode == ModuleMode.MainMode)
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
        /// 获得Poly线上某个固定距离的点坐标,该距离由终点指向起点(也可传入百分比),获取被该点截断的新Poly线
        /// </summary>
        /// <param name="points">包含起点和终点</param>
        /// <param name="distance">回走距离</param>
        /// <returns></returns>
        private Tuple<PointD, PointD[]> GetPointOnPoly(List<PointD> points, double distance, bool isPercent)
        {

            //计算Poly线的总长度和各线段的长度
            double sumDistance = 0;
            double[] singleDistance = new double[points.Count - 1];
            for (int i = 0; i < singleDistance.Length; i++)
            {
                singleDistance[i] = Math.Sqrt(Math.Pow(points[i + 1].Y - points[i].Y, 2) + Math.Pow(points[i + 1].X - points[i].X, 2));
                sumDistance += singleDistance[i];
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

            //确定该距离在第几个线段内(最小为1)
            int lineIndex = 0;
            for (int i = 0; i < singleDistance.Length; i++)
            {
                double currDistance = 0;
                for (int j = 0; j <= i; j++)
                {
                    currDistance += singleDistance[j];
                }

                if (currDistance >= shutOffDistance)
                {
                    lineIndex = i + 1;
                    break;
                }
            }

            //计算线段的角度
            double angleOfLine = Math.Atan2(points[lineIndex].Y - points[lineIndex - 1].Y, points[lineIndex].X - points[lineIndex - 1].X) * 180 / Math.PI;
            //计算该距离的坐标点相对于所处线段上相对于线段起点的距离
            double PreDistance = 0;
            double relativeDistance = 0;
            for (int i = 0; i < lineIndex; i++)
            {
                if (i == 0)
                {
                    PreDistance += 0;
                }
                else
                {
                    PreDistance += singleDistance[i - 1];
                }
            }

            relativeDistance = shutOffDistance - PreDistance;

            //得到结果
            PointD pos = new PointD();
            double cos = Math.Cos(angleOfLine * Math.PI / 180);
            double sin = Math.Sin(angleOfLine * Math.PI / 180);
            pos.X = Math.Round(points[lineIndex - 1].X + relativeDistance * cos, 3);
            pos.Y = Math.Round(points[lineIndex - 1].Y + relativeDistance * sin, 3);

            PointD[] newPolyPoints = new PointD[lineIndex + 1];
            for (int i = 0; i < newPolyPoints.Length; i++)
            {
                if (i == newPolyPoints.Length - 1)
                {
                    newPolyPoints[i] = pos;
                }
                else
                {
                    newPolyPoints[i] = points[i];
                }
            }

            Tuple<PointD, PointD[]> tuple = new Tuple<PointD, PointD[]>(pos, newPolyPoints);
            return tuple;
        }

    }
}
