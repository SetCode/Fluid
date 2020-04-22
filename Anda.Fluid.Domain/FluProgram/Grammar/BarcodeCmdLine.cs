using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.Barcode;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class BarcodeCmdLine : CmdLine
    {
        private BarcodePrm barcodePrm = new BarcodePrm();

        public PointD PosInPattern { get; private set; } 

        public BarcodeCmdLine() : base(true)
        {
            this.PosInPattern = new PointD();
        }

        public BarcodePrm BarcodePrm => barcodePrm;

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
            return sb.Append("BARCODE : ").Append(MachineRel(PosInPattern)).ToString();
        }

        public override object Clone()
        {
            BarcodeCmdLine cmdLine = this.MemberwiseClone() as BarcodeCmdLine;
            cmdLine.barcodePrm = this.barcodePrm.Clone() as BarcodePrm;
            cmdLine.PosInPattern = this.PosInPattern.Clone() as PointD;
            return cmdLine;
        }
    }
}
