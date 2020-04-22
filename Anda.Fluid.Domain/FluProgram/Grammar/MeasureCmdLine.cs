using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MeasureCmdLine : CmdLine
    {
        private MeasurePrm measurePrm = new MeasurePrm();
        public MeasurePrm MeasurePrm => measurePrm;
       
        public string SavePath = string.Empty;

        public PointD PosInPattern { get; private set; } = new PointD();

        public MeasureContents MeasureContent = MeasureContents.None;
        //public bool NeedMeasureHeight = false;
        public  List<MeasureHeightCmdLine> MeasureHeightCmdLines = new List<MeasureHeightCmdLine>();

        /// <summary>
        /// 上限
        /// </summary>
        public double ToleranceMax { get; set; } = 10;

        /// <summary>
        /// 下限
        /// </summary>
        public double ToleranceMin { get; set; } =0;

        public MeasureCmdLine() : base(true)
        {
           
        }
        public MeasureCmdLine(PointD posInPattern):base(true)
        {
            this.PosInPattern = posInPattern;
        }

        

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            PointD oldPosMachine = (PosInPattern + patternOldOrigin.ToSystem()).ToMachine();
            PointD newPosMachine = coordinateTransformer.Transform(oldPosMachine);
            VectorD v = newPosMachine.ToSystem() - patternNewOrigin.ToSystem();
            this.PosInPattern.X = v.X;
            this.PosInPattern.Y = v.Y;
            if (this.MeasureContent.HasFlag(MeasureContents.GlueHeight) && this.MeasureHeightCmdLines != null && this.MeasureHeightCmdLines.Count==2)
            {
                foreach (var item in this.MeasureHeightCmdLines)
                {
                    item.Correct(patternOldOrigin, coordinateTransformer, patternNewOrigin);
                }
            }
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
       
            return sb.Append("MEASURE : ").Append(MeasurePrm.GetMeasureTypeEn(this.measurePrm.measureType)).Append(MachineRel(PosInPattern)).ToString();
        }
                

        public override object Clone()
        {
            MeasureCmdLine measureCmdline = MemberwiseClone() as MeasureCmdLine;
            measureCmdline.measurePrm = this.measurePrm.Clone() as MeasurePrm;
            measureCmdline.MeasureHeightCmdLines = new List<MeasureHeightCmdLine>();
            foreach (var item in this.MeasureHeightCmdLines)
            {
                measureCmdline.MeasureHeightCmdLines.Add(item.Clone() as MeasureHeightCmdLine); 
            }
            return measureCmdline;
        }
    }

    [Flags]
    public enum MeasureContents
    {
        None= 0x01,
        LineWidth =0x02,
        GlueHeight=0x04
    }
}
