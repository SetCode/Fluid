using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class DotCmd : SupportDirectiveCmd,IPatternWeightable
    {
        private PointD position;
        /// <summary>
        /// 点坐标，相对于当前所在Pattern的原点
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 所使用的点参数类型
        /// </summary>
        public DotStyle DotStyle = DotStyle.TYPE_1;
        public int NumShots;
        public bool IsAssign = false;
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
        private MeasureHeightCmd associatedMeasureHeightCmd = null;
        public MeasureHeightCmd AssociatedMeasureHeightCmd { get { return this.associatedMeasureHeightCmd; } }
        public DotCmd(RunnableModule runnableModule, DotCmdLine dotCmdLine,MeasureHeightCmd mhCmd) 
            : base(runnableModule, dotCmdLine)
        {
            this.Valve = dotCmdLine.Valve;
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            position = structure.ToMachine(runnableModule, dotCmdLine.Position);
            DotStyle = dotCmdLine.DotStyle;
            IsWeightControl = dotCmdLine.IsWeightControl;
            Weight = dotCmdLine.Weight;
            this.NumShots = dotCmdLine.NumShots;
            this.IsAssign = dotCmdLine.IsAssign;
            this.associatedMeasureHeightCmd = mhCmd;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Dot(this, coordinateCorrector);
        }

        public Directive ToDirectiveISpray()
        {
            return new Dot(this);
        }

    }
}