using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using System.Collections.Generic;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using System.Drawing;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Domain.FluProgram.Structure;
using System.Text;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Grammar;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using Anda.Fluid.Infrastructure.Msg;
using System.Threading.Tasks;
using System.Diagnostics;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.Vision.ASV;
using System.Collections;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
	/// <summary>
	/// 线命令
	/// </summary>
	[Serializable]
	public class MultiTraces : Directive, ISprayable
	{        
		public static ManualResetEvent WaitMsg = new ManualResetEvent(false);
        private MultiTracesCmd multiTracesCmd;
        private List<TraceBase> traces = new List<TraceBase>();

        /// <summary>
        /// 所有线段的起始点和终点信息
        /// </summary>
        public List<TraceBase> Traces => this.traces;

        public MultiTracesCmd MultiTraceCmd => this.multiTracesCmd;

        //private LineParam param;
        ///// <summary>
        ///// 线参数
        ///// </summary>
        //public LineParam Param
        //{
        //    get { return param; }
        //}

        private bool isWeightControl = false;
		/// <summary>
		/// 是否开启重量控制
		/// </summary>
		public bool IsWeightControl
		{
			get { return isWeightControl; }
		}

		private double wholeWeight = 0;
		/// <summary>
		/// 如果开启了重量控制，该参数指定重量值，单位：mg
		/// </summary>
		public double WholeWeight
		{
			set
			{
				wholeWeight = value < 0 ? 0 : value;
			}
			get
			{
				return wholeWeight;
			}
		}

        public double OffsetX;

        public double OffsetY;

        [NonSerialized]
		private double curMeasureHeightValue;
        public double CurMeasureHeightValue => this.curMeasureHeightValue;

        public MultiTraces(MultiTracesCmd multiTracesCmd)
            : this(multiTracesCmd, null)
        {

        }

        public MultiTraces(MultiTracesCmd multiTracesCmd, CoordinateCorrector coordinateCorrector)
		{
			this.RunnableModule = multiTracesCmd.RunnableModule;
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

            foreach (var item in multiTracesCmd.Traces)
            {
                TraceBase newTrace = item.Clone() as TraceBase;
                if (newTrace is TraceLine)
                {
                    TraceLine traceLine = newTrace as TraceLine;
                    traceLine.Start = coordinateCorrector.Correct(multiTracesCmd.RunnableModule, traceLine.Start, Executor.Instance.Program.ExecutantOriginOffset);
                    traceLine.End = coordinateCorrector.Correct(multiTracesCmd.RunnableModule, traceLine.End, Executor.Instance.Program.ExecutantOriginOffset);
                }
                else
                {
                    TraceArc traceArc = newTrace as TraceArc;
                    traceArc.Start = coordinateCorrector.Correct(multiTracesCmd.RunnableModule, traceArc.Start, Executor.Instance.Program.ExecutantOriginOffset);
                    traceArc.Mid = coordinateCorrector.Correct(multiTracesCmd.RunnableModule, traceArc.Mid, Executor.Instance.Program.ExecutantOriginOffset);
                    traceArc.End = coordinateCorrector.Correct(multiTracesCmd.RunnableModule, traceArc.End, Executor.Instance.Program.ExecutantOriginOffset);
                }

                //newTrace.Param = multiTracesCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(newLine.LineStyle);
                this.traces.Add(newTrace);
                Log.Dprint(string.Format("{0}: {1}", item.GetType(), item));
			}
			//param = multiTracesCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(multiTracesCmd.LineStyle);
			isWeightControl = multiTracesCmd.IsWeightControl;
			wholeWeight = multiTracesCmd.WholeWeight;
            this.OffsetX = multiTracesCmd.OffsetX;
            this.OffsetY = multiTracesCmd.OffsetY;
			Program = multiTracesCmd.RunnableModule.CommandsModule.Program;
			this.multiTracesCmd = multiTracesCmd;					   
			if (multiTracesCmd.AssociatedMeasureHeightCmd != null)
			{
				curMeasureHeightValue = multiTracesCmd.AssociatedMeasureHeightCmd.RealHtValue;
			}
			else
			{
				curMeasureHeightValue = this.RunnableModule.MeasuredHt;
			}
		}

        public LineParam GetLineStyle(TraceBase trace)
        {
            return this.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam((LineStyle)trace.LineStyle);
        }

		public override Result Execute()
		{
		    if(Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Log.Dprint("begin to execute Line, weight control=" + isWeightControl);
			Result ret = Result.OK;
            switch (Machine.Instance.Valve1.RunMode)
            {
                case ValveRunMode.Wet:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().WetExecute(this);
                    break;
                case ValveRunMode.Dry:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().DryExecute(this);
                    break;
                case ValveRunMode.Look:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().LookExecute(this);
                    break;
                case ValveRunMode.AdjustLine:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().AdjustExecute(this);
                    break;
                case ValveRunMode.InspectDot:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().InspectDotExecute(this);
                    break;
                case ValveRunMode.InspectRect:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().InspectRectExecute(this);
                    break;
                default:

                    break;
            }
            return ret;

		}

        /// <summary>
        /// 称重打胶
        /// </summary>
        public Result Spray(Valve valve)
		{
            Result ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetMultiTracable().PatternWeightExecute(this, valve);
            return ret;
        }

        public void Allocate(int totalShots)
        {
            List<PointD> vertexes = new List<PointD>();
            for (int i = 0; i < this.traces.Count; i++)
            {
                if(i == 0)
                {
                    vertexes.Add(this.traces[i].Start);
                }
                vertexes.Add(this.traces[i].End);
            }
        }
    }
}