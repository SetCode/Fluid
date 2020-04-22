using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MeasureCmd : SupportDirectiveCmd
    {
        private MeasurePrm measurePrm;
        public MeasurePrm MeasurePrm => measurePrm;
        public string SavePath = string.Empty;

        private PointD position;
        public PointD Position => this.position;
        //public bool NeedMeasureHeight = false;
        //0: 集中处理  1：执行时处理
        public List<MeasureHeightCmd> MeasureHeightCmds;
        public MeasureContents MeasureContent = MeasureContents.None;
        /// <summary>
        /// 上限
        /// </summary>
        public double ToleranceMax { get; set; } = 10;
        /// <summary>
        /// 下限
        /// </summary>
        public double ToleranceMin { get; set; } = 0;

        public MeasureCmd(RunnableModule runnableModule, MeasureCmdLine measurecmdLine) : base(runnableModule, measurecmdLine)
        {
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            
            this.position = structure.ToMachine(runnableModule, measurecmdLine.PosInPattern);
            this.measurePrm = measurecmdLine.MeasurePrm;
            this.SavePath = measurecmdLine.SavePath;

            //this.NeedMeasureHeight = measurecmdLine.NeedMeasureHeight;
            this.MeasureContent = measurecmdLine.MeasureContent;
            if (measurecmdLine.MeasureContent.HasFlag(MeasureContents.GlueHeight) && measurecmdLine.MeasureHeightCmdLines != null && measurecmdLine.MeasureHeightCmdLines.Count == 2)
            {
                this.ToleranceMax = measurecmdLine.MeasurePrm.ToleranceMax;
                this.ToleranceMin = measurecmdLine.MeasurePrm.ToleranceMin;
                this.MeasureHeightCmds = new List<MeasureHeightCmd>();
                foreach (MeasureHeightCmdLine item in measurecmdLine.MeasureHeightCmdLines)
                {
                    this.MeasureHeightCmds.Add( new MeasureHeightCmd(runnableModule, item));
                }
            }
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Measure(this, coordinateCorrector);
        }
    }
}
