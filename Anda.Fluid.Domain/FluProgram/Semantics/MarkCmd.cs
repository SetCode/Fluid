using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MarkCmd : SupportDirectiveCmd
    {

        private ModelFindPrm modelFindPrm;
        public bool IsFromFile { get; private set; }
        /// <summary>
        /// Mark点模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm
        {
            get { return modelFindPrm; }
        }

        private PointD position;
        /// <summary>
        /// mark点位置坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }
        [NonSerialized]
        private PointD flyPosition;
        /// <summary>
        /// 飞拍触发坐标点位，运行时坐标，不保存
        /// </summary>
        public PointD FlyPosition
        {
            get { return flyPosition; }
            set { flyPosition = value; }
        }
        /// <summary>
        /// 飞拍偏移量
        /// </summary>
        public VectorD FlyOffset { get; set; } = new VectorD();


        public MarkCmd(RunnableModule runnableModule, MarkCmdLine markCmdLine) : base(runnableModule, markCmdLine)
        {
            // 转换成机械坐标
            var structure = runnableModule.CommandsModule.program.ModuleStructure;           
            position = structure.ToMachine(runnableModule, markCmdLine.PosInPattern);
            modelFindPrm = markCmdLine.ModelFindPrm;
            this.IsFromFile = markCmdLine.IsFromFile;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Mark(this,coordinateCorrector);
        }
    }
}