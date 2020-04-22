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

using System.Threading;
using Anda.Fluid.Infrastructure.Msg;
using System.Threading.Tasks;
using System.Diagnostics;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.Vision.ASV;
using System.Collections;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
	/// <summary>
	/// 线命令
	/// </summary>
	[Serializable]
	public class Line : Directive,ISprayable
	{        
		public static ManualResetEvent WaitMsg = new ManualResetEvent(false);
		private LineCmd lineCmd;
		private List<LineCoordinate> lineCoordinateList = new List<LineCoordinate>();
		private Dictionary<LineCoordinate,Line> lineCoordinateDic = new Dictionary<LineCoordinate, Line>();
		/// <summary>
		/// 所有线段的起始点和终点信息
		/// </summary>
		public List<LineCoordinate> LineCoordinateList
		{
			get { return lineCoordinateList; }
		}

        public LineCmd LineCmd => this.lineCmd;

        /// <summary>
        /// 仅供喷射阀执行时使用
        /// </summary>
        internal List<LineCoordinate> LineCoordinates
        {
            get { return this.lineCoordinateList; }
            set { this.lineCoordinateList = value; }
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
		[NonSerialized]
		private double curMeasureHeightValue;

        public double CurMeasureHeightValue => this.curMeasureHeightValue;
		
		public Line(LineCmd lineCmd, CoordinateCorrector coordinateCorrector)
		{
			//this.Valve = lineCmd.Valve;
			this.RunnableModule = lineCmd.RunnableModule;
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
			PointD start, end;
			foreach (LineCoordinate line in lineCmd.LineCoordinateList)
			{
				start = coordinateCorrector.Correct(lineCmd.RunnableModule, line.Start, Executor.Instance.Program.ExecutantOriginOffset);
				end = coordinateCorrector.Correct(lineCmd.RunnableModule, line.End, Executor.Instance.Program.ExecutantOriginOffset);
				LineCoordinate newLine = new LineCoordinate(start, end);
				newLine.Weight = line.Weight;
				newLine.LineStyle = line.LineStyle;
				newLine.Param = lineCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(newLine.LineStyle);
                newLine.LookOffsetRevs = line.LookOffsetRevs;
                newLine.LookOffset = line.LookOffset;
                newLine.Repetition = line.Repetition;
				lineCoordinateList.Add(newLine);

				lineCoordinateDic.Add(newLine,this);

				Log.Dprint("Line start : " + line.Start + ", real : " + start);
				Log.Dprint("Line end : " + line.End + ", real : " + end);
				
			}
			param = lineCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(lineCmd.LineStyle);
			isWeightControl = lineCmd.IsWeightControl;
			wholeWeight = lineCmd.WholeWeight;
			Program = lineCmd.RunnableModule.CommandsModule.Program;
			this.lineCmd = lineCmd;					   
			if (lineCmd.AssociatedMeasureHeightCmd != null)
			{
				curMeasureHeightValue = lineCmd.AssociatedMeasureHeightCmd.RealHtValue;
			}
			else
			{
				curMeasureHeightValue = this.RunnableModule.MeasuredHt;
			}
            this.Tilt = this.lineCmd.Tilt;
		}

		public override Result Execute()
		{

			Log.Dprint("begin to execute Line, weight control=" + isWeightControl);
			Result ret = Result.OK;

            switch (Machine.Instance.Valve1.RunMode)
            {
                case ValveRunMode.Wet:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().WetExecute(this);
                    break;
                case ValveRunMode.Dry:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().DryExecute(this);
                    break;
                case ValveRunMode.Look:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().LookExecute(this);
                    break;
                case ValveRunMode.AdjustLine:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().AdjustExecute(this);
                    break;
                case ValveRunMode.InspectDot:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().InspectDotExecute(this);
                    break;
                case ValveRunMode.InspectRect:
                    ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().InspectRectExecute(this);
                    break;
                default:

                    break;
            }
            return ret;

		}

        #region pattern weight
        public Line(LineCmd lineCmd)
        {
            //this.Valve = lineCmd.Valve;
            this.RunnableModule = lineCmd.RunnableModule;
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
            PointD start, end;
            foreach (LineCoordinate line in lineCmd.LineCoordinateList)
            {
                start =  line.Start;
                end =  line.End;
                LineCoordinate newLine = new LineCoordinate(start, end);
                newLine.Weight = line.Weight;
                newLine.LineStyle = line.LineStyle;
                newLine.Param = lineCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(newLine.LineStyle);
                newLine.LookOffsetRevs = line.LookOffsetRevs;
                newLine.LookOffset = line.LookOffset;
                newLine.Repetition = line.Repetition;
                lineCoordinateList.Add(newLine);

                lineCoordinateDic.Add(newLine, this);

                Log.Dprint("Line start : " + line.Start + ", real : " + start);
                Log.Dprint("Line end : " + line.End + ", real : " + end);

            }
            //param = lineCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(lineCmd.LineStyle);
            isWeightControl = lineCmd.IsWeightControl;
            wholeWeight = lineCmd.WholeWeight;
            Program = lineCmd.RunnableModule.CommandsModule.Program;
            this.lineCmd = lineCmd;
            if (lineCmd.AssociatedMeasureHeightCmd != null)
            {
                curMeasureHeightValue = lineCmd.AssociatedMeasureHeightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
        }
        /// <summary>
        /// 称重打胶
        /// </summary>
        public Result Spray(Valve valve)
		{
            Result ret = FluiderFactory.Instance.CreatFluider(this.Valve).GetLinable().PatternWeightExecute(this, valve);
            return ret;
        }

		#endregion
	}
}