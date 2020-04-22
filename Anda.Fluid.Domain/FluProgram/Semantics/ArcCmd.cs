using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class ArcCmd : SupportDirectiveCmd,IPatternWeightable
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
        /// 弧度
        /// </summary>
        public double Degree;

        /// <summary>
        /// 所使用的线参数类型
        /// </summary>
        public LineStyle LineStyle = LineStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl = false;

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
        private MeasureHeightCmd associatedMeasureheightCmd = null;
        public MeasureHeightCmd AssociatedMeasureheightCmd
        {
            get
            {
                return this.associatedMeasureheightCmd;
            }
        }
       
        public ArcCmd(RunnableModule runnableModule, ArcCmdLine arcCmdLine, MeasureHeightCmd mhCmd) : base(runnableModule, arcCmdLine)
        {
            this.Valve = arcCmdLine.Valve;
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            if (!runnableModule.CommandsModule.IsReversePattern)
            {
                start = structure.ToMachine(runnableModule, arcCmdLine.Start);
                middle = structure.ToMachine(runnableModule, arcCmdLine.Middle);
                end = structure.ToMachine(runnableModule, arcCmdLine.End);
                center = structure.ToMachine(runnableModule, arcCmdLine.Center);
                Degree = arcCmdLine.Degree;
            }
            else
            {
                start = structure.ToMachine(runnableModule, arcCmdLine.End);
                middle = structure.ToMachine(runnableModule, arcCmdLine.Middle);
                end = structure.ToMachine(runnableModule, arcCmdLine.Start);
                center = structure.ToMachine(runnableModule, arcCmdLine.Center);
                Degree = -arcCmdLine.Degree;
            }
            
            LineStyle = arcCmdLine.LineStyle;
            IsWeightControl = arcCmdLine.IsWeightControl;
            weight = arcCmdLine.Weight;
            this.associatedMeasureheightCmd = mhCmd;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Arc(this, coordinateCorrector);
        }

        public Directive ToDirectiveISpray()
        {
            return new Arc(this);
        }

    }
}