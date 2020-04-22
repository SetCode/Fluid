using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using System.Drawing;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using System.Threading;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 点命令
    /// </summary>
    [Serializable]
    public class Dot : Directive, ISprayable
    {
        private PointD position;
        /// <summary>
        /// 机械坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        private DotParam param;
        /// <summary>
        /// 点参数
        /// </summary>
        public DotParam Param
        {
            get { return param; }
        }

        private int numShots;
        private bool isAssign = false;
        public int NumShots
        {
            get
            {
                if (isAssign)
                {
                    return numShots;
                }
                else
                {
                    return param.NumShots;
                }
                
            }
                   
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
            get
            {
                return weight;
            }
        }
        [NonSerialized]
        private double curMeasureHeightValue;

        public double CurMeasureHeightValue => this.curMeasureHeightValue;
        public Dot(DotCmd dotCmd, CoordinateCorrector coordinateCorrector)
        {
            //this.Valve = dotCmd.Valve;
            this.RunnableModule = dotCmd.RunnableModule;
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
            position = coordinateCorrector.Correct(dotCmd.RunnableModule, dotCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            Log.Dprint("Dot position : " + dotCmd.Position + ", real : " + position);
            param = dotCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetDotParam(dotCmd.DotStyle);
            isWeightControl = dotCmd.IsWeightControl;
            weight = dotCmd.Weight;
            this.isAssign = dotCmd.IsAssign;
            this.numShots = dotCmd.NumShots;
            Program = dotCmd.RunnableModule.CommandsModule.Program;
            if (dotCmd.AssociatedMeasureHeightCmd != null)
            {
                curMeasureHeightValue = dotCmd.AssociatedMeasureHeightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
            this.Tilt = dotCmd.Tilt;
        }

        public override Result Execute()
        {
            Result ret = Result.OK;
            switch (Machine.Instance.Valve1.RunMode)
            {
                case ValveRunMode.Wet:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().WetExecute(this);
                    break;
                case ValveRunMode.Dry:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().DryExecute(this);
                    break;
                case ValveRunMode.Look:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().LookExecute(this);
                    break;
                case ValveRunMode.AdjustLine:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().AdjustExecute(this);
                    break;
                case ValveRunMode.InspectDot:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().InspectDotExecute(this);
                    break;
                case ValveRunMode.InspectRect:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetDotable().InspectRectExecute(this);
                    break;
                default:
                    
                    break;
            }
            return ret;
        }

        #region pattern weight
        public Dot(DotCmd dotCmd)
        {
            //this.Valve = dotCmd.Valve;
            this.RunnableModule = dotCmd.RunnableModule;
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
            position =  dotCmd.Position;            
            param = dotCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetDotParam(dotCmd.DotStyle);
            isWeightControl = dotCmd.IsWeightControl;
            weight = dotCmd.Weight;
            this.isAssign = dotCmd.IsAssign;
            this.numShots = dotCmd.NumShots;
            Program = dotCmd.RunnableModule.CommandsModule.Program;
            if (dotCmd.AssociatedMeasureHeightCmd != null)
            {
                curMeasureHeightValue = dotCmd.AssociatedMeasureHeightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
        }

        public Result Spray(Valve valve)
        {
            Result ret = Result.OK;

            ret = FluiderFactory.Instance.CreatFluider(valve).GetDotable().PatternWeightExecute(this, valve);

            return ret;
        }
        #endregion
    }
}