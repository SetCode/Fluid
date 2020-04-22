using Anda.Fluid.Infrastructure.Common;
using System;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Structure;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MeasureHeightCmd : SupportDirectiveCmd,ICloneable
    {
        private PointD position;
        /// <summary>
        /// 测量高度的位置
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 标准高度
        /// </summary>
        public double StandardHt { get; set; } = 0;

        /// <summary>
        /// 上限
        /// </summary>
        public double ToleranceMax { get; set; } = 2;

        /// <summary>
        /// 下限
        /// </summary>
        public double ToleranceMin { get; set; } = -2;
        /// <summary>
        /// 测高时的Z轴高度
        /// </summary>
        public double ZPos { get; set; } = -2;
        /// <summary>
        /// 实际测高值
        /// </summary>
        [NonSerialized]
        private double realHtValue;
        public double RealHtValue
        {
            get { return realHtValue; }
            set { realHtValue = value; }
        }

        public MeasureHeightCmd(RunnableModule runnableModule, MeasureHeightCmdLine measureHeightCmdLine) : base(runnableModule, measureHeightCmdLine)
        {
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            position = structure.ToMachine(runnableModule, measureHeightCmdLine.Position);
            StandardHt = measureHeightCmdLine.StandardHt;
            ToleranceMax = measureHeightCmdLine.ToleranceMax;
            ToleranceMin = measureHeightCmdLine.ToleranceMin;
            ZPos = measureHeightCmdLine.ZPos;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new MeasureHeight(this, coordinateCorrector);
        }

        public object Clone()
        {
            MeasureHeightCmd result = (MeasureHeightCmd)this.MemberwiseClone();
            result.position = (PointD)this.position.Clone();
            return result;
        }
    }
}
