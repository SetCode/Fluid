using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MultiTracesCmd : SupportDirectiveCmd,IPatternWeightable
    {
        public MultiTracesCmdLine multiTracesCmdLine;

        private List<TraceBase> traces = new List<TraceBase>();
        /// <summary>
        /// 所有轨迹集合
        /// </summary>
        public List<TraceBase> Traces => this.traces;

        /// <summary>
        /// 所使用的线参数类型
        /// </summary>
        public LineStyle LineStyle = LineStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl = false;

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

        /// <summary>
        /// X偏移
        /// </summary>
        public double OffsetX;

        /// <summary>
        /// Y偏移
        /// </summary>
        public double OffsetY;

        [NonSerialized]
        private MeasureHeightCmd associatedMeasureHeightCmd = null;
        public MeasureHeightCmd AssociatedMeasureHeightCmd
        {
            get { return associatedMeasureHeightCmd; }
        }


        public MultiTracesCmd(RunnableModule runnableModule, MultiTracesCmdLine multiTracesCmdLine, MeasureHeightCmd mhCmd) 
            : base(runnableModule, multiTracesCmdLine)
        {
            this.Valve = multiTracesCmdLine.Valve;
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            this.multiTracesCmdLine = multiTracesCmdLine;

            foreach (var trace in this.multiTracesCmdLine.Traces)
            {
                TraceBase newTrace = trace.Clone() as TraceBase;
                if (newTrace is TraceLine)
                {
                    TraceLine traceLine = newTrace as TraceLine;
                    traceLine.Start = structure.ToMachine(runnableModule, traceLine.Start);
                    traceLine.End = structure.ToMachine(runnableModule, traceLine.End);
                }
                else
                {
                    TraceArc traceArc = newTrace as TraceArc;
                    traceArc.Start = structure.ToMachine(runnableModule, traceArc.Start);
                    traceArc.Mid = structure.ToMachine(runnableModule, traceArc.Mid);
                    traceArc.End = structure.ToMachine(runnableModule, traceArc.End);
                }
                this.traces.Add(newTrace);
            }
            
            LineStyle = multiTracesCmdLine.LineStyle;
            IsWeightControl = multiTracesCmdLine.IsWeightControl;
            wholeWeight = multiTracesCmdLine.WholeWeight;
            this.OffsetX = multiTracesCmdLine.OffsetX;
            this.OffsetY = multiTracesCmdLine.OffsetY;
            this.associatedMeasureHeightCmd = mhCmd;
            //this.CheckRepeat();
        }
        

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new MultiTraces(this, coordinateCorrector);
        }
        public Directive ToDirectiveISpray()
        {
            return new MultiTraces(this);
        }

        /// <summary>
        /// 进行多条线段的首尾重合比较，适用于所有线段类型
        /// </summary>
        private void CheckRepeat()
        {
            //获得所有线段的首尾点
            List<PointD> allPoints = new List<PointD>();
            //for (int i = 0; i < this.traces.Count; i++)
            //{
            //    allPoints.Add(lineCoordinateList[i].Start);
            //    allPoints.Add(lineCoordinateList[i].End);
            //}

            ////获得所有线段的首尾点比较结果
            //List<LineRepetition> lineRepetition = MathUtils.CheckLinesPepeat(allPoints);

            ////将结果赋给线段，理论上结果和线段是一一对应的关系
            //for (int i = 0; i < lineCoordinateList.Count; i++)
            //{
            //    lineCoordinateList[i].Repetition = lineRepetition[i];
            //}
        }
    }
}