using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Trace;
using System.Text;
using Anda.Fluid.Drive.ValveSystem;
using System.Threading;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 弧命令
    /// </summary>
    [Serializable]
    public class Arc : Directive,ISprayable
    {
        private PointD start;
        /// <summary>
        /// 弧起始点的坐标
        /// </summary>
        public PointD Start
        {
            get { return start; }
        }

        private PointD middle;
        /// <summary>
        /// 弧上位于起始点和末尾点之间的一个坐标
        /// </summary>
        public PointD Middle
        {
            get { return middle; }
        }

        private PointD end;
        /// <summary>
        /// 弧的末尾点的坐标
        /// </summary>
        public PointD End
        {
            get { return end; }
        }

        private PointD center;
        /// <summary>
        /// 弧所在的圆心
        /// </summary>
        public PointD Center
        {
            get { return center; }
        }

        /// <summary>
        /// 半径
        /// </summary>
        protected double r;

        public double R => this.r;

        /// <summary>
        /// 弧的长度
        /// </summary>
        protected double length;

        public double Length => this.length;

        /// <summary>
        /// 弧的度数
        /// </summary>
        public double Degree;

        /// <summary>
        /// 圆弧的方向，0为顺时针，1为逆时针
        /// </summary>
        public short ClockWise
        {
            get
            {
                if (this.Degree < 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        private LineParam param;
        /// <summary>
        /// 线参数
        /// </summary>
        public LineParam Param
        {
            get { return param; }
        }

        private bool isWeightControl = false;
        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl
        {
            get { return isWeightControl; }
        }

        private double weight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        public double Weight
        {
            set
            {
                weight = value < 0 ? 0 : value;
            }
            get
            {
                return weight;
            }
        }
        [NonSerialized]
        private double curMeasureHeightValue = 0;

        public double CurMeasureHeightValue => this.curMeasureHeightValue;


        public Arc(ArcCmd arcCmd, CoordinateCorrector coordinateCorrector)
        {
            //this.Valve = arcCmd.Valve;
            this.RunnableModule = arcCmd.RunnableModule;
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
            start = coordinateCorrector.Correct(arcCmd.RunnableModule, arcCmd.Start, Executor.Instance.Program.ExecutantOriginOffset);
            middle = coordinateCorrector.Correct(arcCmd.RunnableModule, arcCmd.Middle, Executor.Instance.Program.ExecutantOriginOffset);
            end = coordinateCorrector.Correct(arcCmd.RunnableModule, arcCmd.End, Executor.Instance.Program.ExecutantOriginOffset);
            center = coordinateCorrector.Correct(arcCmd.RunnableModule, arcCmd.Center, Executor.Instance.Program.ExecutantOriginOffset);
            Log.Dprint("Arc start " + arcCmd.Start + ", real : " + start);
            Log.Dprint("Arc middle " + arcCmd.Middle + ", real : " + middle);
            Log.Dprint("Arc end " + arcCmd.End + ", real : " + end);
            Log.Dprint("Arc center " + arcCmd.Center + ", real : " + center);
            r = MathUtils.Distance(Center, Start);
            Degree = arcCmd.Degree;
            length = Math.Abs(arcCmd.Degree) / 360f * 2 * Math.PI * r;
            param = arcCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(arcCmd.LineStyle);
            isWeightControl = arcCmd.IsWeightControl;
            weight = arcCmd.Weight;
            Program = arcCmd.RunnableModule.CommandsModule.Program;
            if (arcCmd.AssociatedMeasureheightCmd != null)
            {
                curMeasureHeightValue = arcCmd.AssociatedMeasureheightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
        }

        public override Result Execute()
        {

            Log.Dprint("begin to execute Arc");
            Result ret = Result.OK;
            switch (Machine.Instance.Valve1.RunMode)
            {
                case ValveRunMode.Wet:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().WetExecute(this);
                    break;
                case ValveRunMode.Dry:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().DryExecute(this);
                    break;
                case ValveRunMode.Look:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().LookExecute(this);
                    break;
                case ValveRunMode.AdjustLine:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().AdjustExecute(this);
                    break;
                case ValveRunMode.InspectDot:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().InspectDotExecute(this);
                    break;
                case ValveRunMode.InspectRect:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().InspectRectExecute(this);
                    break;
                default:

                    break;
            }
            return ret;
        }


        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private PointD GetSimulPos(PointD pos)
        {
            PointD simulPos = new PointD();
            ///生成副阀相关参数(起点、插补点位)
            if (this.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - this.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -Math.Abs(simulPos.Y) / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(this.Program.RuntimeSettings.SimulOffsetX, this.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }

        #region pattern weight

        public Arc(ArcCmd arcCmd)
        {
            //this.Valve = arcCmd.Valve;
            this.RunnableModule = arcCmd.RunnableModule;
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
            start =  arcCmd.Start;
            middle = arcCmd.Middle;
            end = arcCmd.End;
            center =  arcCmd.Center;
  
            r = MathUtils.Distance(Center, Start);
            Degree = arcCmd.Degree;
            length = Math.Abs(arcCmd.Degree) / 360f * 2 * Math.PI * r;
            param = arcCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(arcCmd.LineStyle);
            isWeightControl = arcCmd.IsWeightControl;
            weight = arcCmd.Weight;
            Program = arcCmd.RunnableModule.CommandsModule.Program;
            if (arcCmd.AssociatedMeasureheightCmd != null)
            {
                curMeasureHeightValue = arcCmd.AssociatedMeasureheightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
        }
        public Result Spray(Valve valve)
        {
            return FluiderFactory.Instance.CreatFluider(this.Valve).GetArcable().PatternWeightExecute(this, valve);
        }

        #endregion
    }
}