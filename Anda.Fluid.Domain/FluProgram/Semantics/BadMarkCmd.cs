using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Common;
using static Anda.Fluid.Domain.FluProgram.Grammar.BadMarkCmdLine;
using Anda.Fluid.Drive.Vision.GrayFind;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class BadMarkCmd : SupportDirectiveCmd
    {
        private ModelFindPrm modelFindPrm;
        /// <summary>
        /// Mark点模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm
        {
            get { return modelFindPrm; }
        }

        public GrayCheckPrm GrayCheckPrm { get; private set; }

        private PointD position;
        /// <summary>
        /// mark点位置坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        public BadMarkType FindType { get; set; }

        public bool IsOkSkip { get; set; }

        /// <summary>
        /// 保存BadMark最终的执行结果(是否跳过关联轨迹)
        /// </summary>
        public bool ResultIsSkip { get; set; } = false;

        public BadMarkCmd(RunnableModule runnableModule, BadMarkCmdLine badMarkCmdLine) : base(runnableModule, badMarkCmdLine)
        {
            // 转换成机械坐标
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            position = structure.ToMachine(runnableModule, badMarkCmdLine.Position);
            modelFindPrm = badMarkCmdLine.ModelFindPrm;
            GrayCheckPrm = badMarkCmdLine.GrayCheckPrm;
            this.FindType = badMarkCmdLine.FindType;
            this.IsOkSkip = badMarkCmdLine.IsOkSkip;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new BadMark(this, coordinateCorrector);
        }
    }
}
