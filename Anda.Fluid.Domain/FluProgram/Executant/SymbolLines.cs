using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.MathTools;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.ValveSystem.Series;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// Description：银宝山特殊多段线轨迹执行层指令
    /// Author：liyi
    /// Date：2019/09/18
    /// </summary>
    [Serializable]
    public class SymbolLines : Directive
    {
        private SymbolLinesCmd symbolLinesCmd;
        /// <summary>
        /// 校准的原始轨迹数据
        /// </summary>
        private List<SymbolLine> oriSymbols = new List<SymbolLine>();
        /// <summary>
        /// 添加倒角处理后的轨迹数据
        /// </summary>
        private List<SymbolLine> finalSymbols = new List<SymbolLine>();

        private double arcSpeed = 20;

        /// <summary>
        /// 激光尺校准与测量时的激光尺位置的高度差
        /// </summary>
        private double LaserZDelta = 0;

        [NonSerialized]
        private double curMeasureHeightValue;

        public double OffsetX;
        public double OffsetY;

        public SymbolLines(SymbolLinesCmd symbolLinesCmd, CoordinateCorrector coordinateCorrector)
        {
            this.RunnableModule = symbolLinesCmd.RunnableModule;
            this.Program = symbolLinesCmd.RunnableModule.CommandsModule.Program;
            if (this.RunnableModule.Mode == ModuleMode.AssignMode1 || this.RunnableModule.Mode == ModuleMode.MainMode)
            {
                this.Valve = ValveType.Valve1;
            }
            else if (this.RunnableModule.Mode == ModuleMode.DualFallow)
            {
                this.Valve = ValveType.Both;
            }
            else
            {
                this.Valve = ValveType.Valve2;
            }
            this.symbolLinesCmd = symbolLinesCmd;
            this.arcSpeed = this.symbolLinesCmd.ArcSpeed;
            this.oriSymbols = symbolLinesCmd.Symbls;
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look)
            {
                curMeasureHeightValue = Machine.Instance.Robot.CalibPrm.SafeZ;
            }
            else
            {
                foreach (SymbolLine item in symbolLinesCmd.Symbls)
                {
                    if (item.MHCmdList.Count > 0)//至少有个首点
                    {
                        this.LaserZDelta = item.MHCmdList[0].ZPos - Machine.Instance.Robot.CalibPrm.SafeZ;
                        curMeasureHeightValue = item.MHCmdList[0].RealHtValue + this.LaserZDelta;
                        break;
                    }
                }
            }
            // 校准当前多线段所有轨迹
            this.oriSymbols = new List<SymbolLine>();

            foreach (SymbolLine item in symbolLinesCmd.Symbls)
            {
                SymbolLine symbol = item.Clone() as SymbolLine;
                symbol.Correct(this.RunnableModule, coordinateCorrector);
                symbol.AddOffset();
                if (symbol.MHList == null)
                {
                    symbol.MHList = new List<MeasureHeight>();
                }
                symbol.MHList.Clear();
                //校正测高点
                foreach (var mhCmd in item.MHCmdList)
                {
                    symbol.MHList.Add(new MeasureHeight(mhCmd, coordinateCorrector));
                }
                this.oriSymbols.Add(symbol);
            }

            this.OffsetX = symbolLinesCmd.OffsetX;
            this.OffsetY = symbolLinesCmd.OffsetY;
        }
        public override Result Execute()
        {
            Result ret = Result.OK;
            LineParam param = symbolLinesCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(this.oriSymbols[0].type);
            foreach (var item in oriSymbols)
            {
                if (item.symbolType == SymbolType.Arc)
                {
                    PointD center = MathUtils.CalculateCircleCenter(item.symbolPoints[0], item.symbolPoints[1], item.symbolPoints[2]);
                    item.symbolPoints[1] = center;
                }
            }
            // 添加倒角轨迹
            this.finalSymbols = new List<SymbolLine>();
            this.finalSymbols.Add((SymbolLine)oriSymbols[0].Clone());
            for (int i = 0; i < oriSymbols.Count - 1; i++)
            {
                SymbolLine transitionArc = new SymbolLine();
                transitionArc.symbolType = SymbolType.Arc;
                List<PointD> symbol1Points = oriSymbols[i].symbolPoints;
                List<PointD> symbol2Points = oriSymbols[i + 1].symbolPoints;
                if (oriSymbols[i].symbolType == SymbolType.Line && oriSymbols[i + 1].symbolType == SymbolType.Line) // 线-线
                {
                    // 添加过渡圆弧
                    transitionArc.symbolPoints = SymbolLineMathTools.GetLineTransitionArc(symbol1Points[0], symbol1Points[1], symbol2Points[1], oriSymbols[i].transitionR);
                    this.finalSymbols.Last().symbolPoints[1] = transitionArc.symbolPoints[0];
                    oriSymbols[i + 1].symbolPoints[0] = transitionArc.symbolPoints[2];
                }
                else if (oriSymbols[i].symbolType == SymbolType.Line && oriSymbols[i + 1].symbolType == SymbolType.Arc) // 线 - 弧
                {
                    // 添加过渡圆弧
                    transitionArc.symbolPoints = SymbolLineMathTools.GetLineToArcTransitionArcByR(symbol1Points[0], symbol1Points[1], symbol2Points[1], oriSymbols[i].transitionR, oriSymbols[i + 1].clockwise);
                    this.finalSymbols.Last().symbolPoints[1] = transitionArc.symbolPoints[0];
                    oriSymbols[i + 1].symbolPoints[0] = transitionArc.symbolPoints[2];
                }
                else if (oriSymbols[i].symbolType == SymbolType.Arc && oriSymbols[i + 1].symbolType == SymbolType.Line) // 弧 - 线
                {
                    // 添加过渡圆弧
                    int clockwise = 0;
                    if (oriSymbols[i].clockwise == 0)
                    {
                        clockwise = 1;
                    }
                    transitionArc.symbolPoints = SymbolLineMathTools.GetLineToArcTransitionArcByR(symbol2Points[1], symbol2Points[0], symbol1Points[1], oriSymbols[i].transitionR, clockwise);
                    transitionArc.symbolPoints.Reverse();
                    this.finalSymbols.Last().symbolPoints[2] = transitionArc.symbolPoints[0];
                    oriSymbols[i + 1].symbolPoints[0] = transitionArc.symbolPoints[2];
                }//弧到弧不做处理

                // 计算过渡圆弧方向
                transitionArc.clockwise = SymbolLineMathTools.GetArcDirect(transitionArc.symbolPoints[0], transitionArc.symbolPoints[1], transitionArc.symbolPoints[2]);

                this.finalSymbols.Add(transitionArc);
                this.finalSymbols.Add((SymbolLine)oriSymbols[i + 1].Clone());
            }

            // 计算每段轨迹起始角度和结束角度
            foreach (SymbolLine item in this.finalSymbols)
            {
                if (item.symbolType == SymbolType.Line)
                {
                    item.StartAngle = MathUtils.CalculateArc(item.symbolPoints[0], item.symbolPoints[1]);
                    item.EndAngle = item.StartAngle;
                }
                else
                {
                    double tempAngle = item.clockwise == 0 ? 90f : -90f;
                    item.TrackStartAngle = MathUtils.CalculateArc(item.symbolPoints[1], item.symbolPoints[0]);
                    item.TrackEndAngle = MathUtils.CalculateArc(item.symbolPoints[1], item.symbolPoints[2]);
                    item.StartAngle = MathUtils.CalculateArc(item.symbolPoints[1], item.symbolPoints[0]) - tempAngle;
                    item.EndAngle = MathUtils.CalculateArc(item.symbolPoints[1], item.symbolPoints[2]) - tempAngle;
                    item.EndAngle = -1 * MathUtils.CalculateDegree(item.symbolPoints[0], item.symbolPoints[2], item.symbolPoints[1], item.clockwise);
                    item.TrackSweep = -1 * MathUtils.CalculateDegree(item.symbolPoints[0], item.symbolPoints[2], item.symbolPoints[1], item.clockwise);
                }
            }

            double currZ = Machine.Instance.Robot.PosZ;
            double firstZ = 0;
            if (Machine.Instance.Valve1.RunMode != ValveRunMode.Look)
            {
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    firstZ = this.Program.RuntimeSettings.BoardZValue + param.DispenseGap;
                }
                else
                {
                    firstZ = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                }
            }
            else
            {
                firstZ = Machine.Instance.Robot.CalibPrm.SafeZ;
            }


            PointD offsetP = new PointD(this.OffsetX, this.OffsetY);
            // 轨迹动作逻辑
            // 胶阀到位置 （XYU三轴同时到位）
            // 偏移调整
            double z = 0;
            if (Machine.Instance.Valve1.RunMode != Drive.ValveSystem.ValveRunMode.Look)
            {
                //到起点，Z轴到点胶位置
                if (currZ > firstZ)
                {
                    // 移动到加速区间起始位置，这里加针嘴上补偿
                    Log.Dprint("move to position XY : " + this.finalSymbols[0].symbolPoints[0]);
                    ret = Machine.Instance.Robot.MovePosXYRAndReply(this.finalSymbols[0].symbolPoints[0].ToNeedle(Machine.Instance.Valve1.ValveType) + offsetP, this.finalSymbols[0].StartAngle.ToAxisRPos(),
                        this.Program.MotionSettings.VelXY,
                        this.Program.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    // 下降到指定高度

                    if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                    {
                        z = firstZ;
                    }
                    else
                    {
                        z = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                    }

                    Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                    ret = Machine.Instance.Robot.MovePosZAndReply(z, param.DownSpeed, param.DownAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                else
                {
                    // 上升到指定高度

                    if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                    {
                        z = firstZ;
                    }
                    else
                    {
                        z = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                    }
                    Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                    ret = Machine.Instance.Robot.MovePosZAndReply(z, param.DownSpeed, param.DownAccel);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }

                    // 移动到加速区间起始位置,这里加针嘴上补偿
                    Log.Dprint("move to position XY : " + this.finalSymbols[0].symbolPoints[0]);
                    ret = Machine.Instance.Robot.MovePosXYRAndReply(this.finalSymbols[0].symbolPoints[0].ToNeedle(Machine.Instance.Valve1.ValveType) + offsetP, this.finalSymbols[0].StartAngle.ToAxisRPos(),
                        this.Program.MotionSettings.VelXY,
                        this.Program.MotionSettings.AccXY);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }
            else
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                ret = Machine.Instance.Robot.MovePosXYAndReply(this.finalSymbols[0].symbolPoints[0],
                    this.Program.MotionSettings.VelXY,
                    this.Program.MotionSettings.AccXY);
            }
            // XYR轴三轴联动后R轴转点位模式
            Machine.Instance.Robot.AxisR.Card.Executor.SetMovePos(
                Machine.Instance.Robot.AxisR.CardId,
                Machine.Instance.Robot.AxisR.AxisId,
                Machine.Instance.Robot.AxisR.ConvertPos2Card(Machine.Instance.Robot.AxisR.Pos), 5, 5, 5);

            // 使用symbols生成对应数据段对象数组
            List<CrdSymbolLine> finalCrdData = new List<CrdSymbolLine>();
            foreach (SymbolLine item in this.finalSymbols)
            {
                if (item.symbolType == SymbolType.Arc)
                {
                    CrdSymbolLine temp = new CrdSymbolLine();
                    temp.Type = (int)item.symbolType;
                    temp.Points = item.symbolPoints;
                    temp.StartAngle = item.StartAngle;
                    temp.EndAngle = item.EndAngle;
                    temp.TrackStartAngle = item.TrackStartAngle;
                    temp.TrackEndAngle = item.TrackEndAngle;
                    temp.TrackSweep = item.TrackSweep;
                    temp.Clockwise = item.clockwise;
                    if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look)
                    {
                        temp.EndZ = Machine.Instance.Robot.CalibPrm.SafeZ;
                    }
                    else
                    {
                        if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                        {
                            temp.EndZ = firstZ;
                        }
                        else
                        {
                            if (finalCrdData.Count == 0) // 
                            {
                                temp.EndZ = Converter.NeedleBoard2Z(param.DispenseGap, item.MHCmdList.Last().RealHtValue + this.LaserZDelta);
                            }
                            else
                            {
                                temp.EndZ = finalCrdData[finalCrdData.Count - 1].EndZ;
                            }
                        }
                    }
                    finalCrdData.Add(temp);
                }
                else
                {
                    // 根据测高点数拆分长直线
                    // 首条轨迹有可能有2个测高但不需要拆分
                    if ((item.MHCount > 1 && finalCrdData.Count != 0) || (item.MHCount > 2 && finalCrdData.Count == 0))
                    {
                        CrdSymbolLine temp = new CrdSymbolLine();
                        temp.Points.Add(new PointD());
                        temp.Points.Add(new PointD());
                        temp.Type = (int)item.symbolType;
                        temp.Points[0].X = item.symbolPoints[0].X;
                        temp.Points[0].Y = item.symbolPoints[0].Y;
                        //temp.Points[1].X = item.MHCmdList[0].Position.X;
                        //temp.Points[1].Y = item.MHCmdList[0].Position.Y;
                        temp.Points[1].X = item.MHList[0].Position.X;
                        temp.Points[1].Y = item.MHList[0].Position.Y;
                        temp.StartAngle = item.StartAngle;
                        temp.EndAngle = item.EndAngle;
                        temp.TrackStartAngle = item.TrackStartAngle;
                        temp.TrackEndAngle = item.TrackEndAngle;
                        temp.TrackSweep = item.TrackSweep;
                        temp.Clockwise = item.clockwise;
                        if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look)
                        {
                            temp.EndZ = Machine.Instance.Robot.CalibPrm.SafeZ;
                        }
                        else
                        {
                            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                            {
                                temp.EndZ = firstZ;
                            }
                            else
                            {
                                temp.EndZ = Converter.NeedleBoard2Z(param.DispenseGap, item.MHCmdList[0].RealHtValue + this.LaserZDelta);
                            }
                        }
                        finalCrdData.Add(temp);
                        for (int i = 1; i < item.MHCmdList.Count; i++)
                        {
                            CrdSymbolLine temp1 = new CrdSymbolLine();
                            temp1.Points.Add(new PointD());
                            temp1.Points.Add(new PointD());
                            temp1.Type = (int)item.symbolType;
                            //temp1.Points[0].X = item.MHCmdList[i - 1].Position.X;
                            //temp1.Points[0].Y = item.MHCmdList[i - 1].Position.Y;
                            //temp1.Points[1].X = item.MHCmdList[i].Position.X;
                            //temp1.Points[1].Y = item.MHCmdList[i].Position.Y;
                            temp1.Points[0].X = item.MHList[i - 1].Position.X;
                            temp1.Points[0].Y = item.MHList[i - 1].Position.Y;
                            temp1.Points[1].X = item.MHList[i].Position.X;
                            temp1.Points[1].Y = item.MHList[i].Position.Y;
                            temp1.StartAngle = item.StartAngle;
                            temp1.EndAngle = item.EndAngle;
                            temp.TrackStartAngle = item.TrackStartAngle;
                            temp.TrackEndAngle = item.TrackEndAngle;
                            temp.TrackSweep = item.TrackSweep;
                            temp1.Clockwise = item.clockwise;
                            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look)
                            {
                                temp1.EndZ = Machine.Instance.Robot.CalibPrm.SafeZ;
                            }
                            else
                            {
                                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                                {
                                    temp1.EndZ = firstZ;
                                }
                                else
                                {
                                    temp1.EndZ = Converter.NeedleBoard2Z(param.DispenseGap, item.MHCmdList[i].RealHtValue + this.LaserZDelta);
                                }

                            }
                            finalCrdData.Add(temp1);
                        }
                    }
                    else
                    {
                        CrdSymbolLine temp = new CrdSymbolLine();
                        temp.Type = (int)item.symbolType;
                        temp.Points = item.symbolPoints;
                        temp.StartAngle = item.StartAngle;
                        temp.EndAngle = item.EndAngle;
                        temp.TrackStartAngle = item.TrackStartAngle;
                        temp.TrackEndAngle = item.TrackEndAngle;
                        temp.TrackSweep = item.TrackSweep;
                        temp.Clockwise = item.clockwise;
                        if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look)
                        {
                            temp.EndZ = Machine.Instance.Robot.CalibPrm.SafeZ;
                        }
                        else
                        {
                            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                            {
                                temp.EndZ = firstZ;
                            }
                            else if (item.MHCount == 0) //当前直线轨迹没有测高，用前一个测高值
                            {
                                temp.EndZ = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                            }
                            else
                            {
                                temp.EndZ = Converter.NeedleBoard2Z(param.DispenseGap, item.MHCmdList.Last().RealHtValue + this.LaserZDelta);
                            }

                        }
                        finalCrdData.Add(temp);
                    }
                    if (Machine.Instance.Valve1.RunMode != ValveRunMode.Look)
                    {

                    }
                }
            }
            double lastZPos = 0;
            for (int i = 0; i < finalCrdData.Count; i++)
            {
                double temp = finalCrdData[i].EndZ;
                if (i == 0)
                {
                    finalCrdData[i].EndZ = finalCrdData[i].EndZ - firstZ;
                }
                else
                {
                    finalCrdData[i].EndZ = finalCrdData[i].EndZ - lastZPos;
                }
                lastZPos = temp;
            }

            //设置进行点胶需要的各种参数（Edit By 肖旭）
            GearValveFluidSymbolLinesPrm fluidPrm = new GearValveFluidSymbolLinesPrm
            {
                Vel = this.symbolLinesCmd.LineParam.Speed,
                StopSprayDistance = this.symbolLinesCmd.LineParam.ShutOffDistance,
                StartSprayDelay = this.symbolLinesCmd.LineParam.PreMoveDelay,
                EndPosDelay = this.symbolLinesCmd.LineParam.Dwell,
                PressDistance = this.symbolLinesCmd.LineParam.PressDistance,
                PressVel = this.symbolLinesCmd.LineParam.PressSpeed,
                PressAcc = this.symbolLinesCmd.LineParam.PressAccel,
                PressTime = this.symbolLinesCmd.LineParam.PressTime,
                RaiseDistance = this.symbolLinesCmd.LineParam.RaiseDistance,
                RaiseVel = this.symbolLinesCmd.LineParam.RaiseSpeed,
                RaiseAcc = this.symbolLinesCmd.LineParam.RaiseAccel,
                ArcSpeed = this.symbolLinesCmd.ArcSpeed,
                BacktrackEndPos = this.symbolLinesCmd.LineParam.BacktrackEndGap + z,
                BacktrackDistance = this.symbolLinesCmd.LineParam.BacktrackDistance,
                BacktrackGap = this.symbolLinesCmd.LineParam.BacktrackGap,
                BacktrackSpeed = this.symbolLinesCmd.LineParam.BacktrackSpeed,
                BacktrackEndGap = this.symbolLinesCmd.LineParam.BacktrackEndGap,
                BackGap = this.symbolLinesCmd.LineParam.BackGap
            };

            // 将插补数据传递到valve对象中
            ret = Machine.Instance.Valve1.FluidSymbolLines(finalCrdData, fluidPrm, FluidProgram.Current.MotionSettings.WeightAcc, this.OffsetX, this.OffsetY
                );

            return ret;
        }



    }
}
