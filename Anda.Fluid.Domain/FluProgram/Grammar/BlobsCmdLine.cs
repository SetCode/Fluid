using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.Halcon;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class BlobsCmdLine : CmdLine
    {
        public BlobsTool BlobsTool { get; set; } = new BlobsTool();

        public string SavePath { get; set; } = string.Empty;

        public PointD PosInPattern { get; set; } = new PointD();

        public BlobsCmdLine() : base(true)
        {

        }

        public BlobsCmdLine(PointD posInPattern) : base(true)
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
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("BLOBS: ").Append(MachineRel(PosInPattern)).ToString();
        }

        public override object Clone()
        {
            return this;
        }
    }
}
