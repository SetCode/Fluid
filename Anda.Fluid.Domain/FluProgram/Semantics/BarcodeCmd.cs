using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Vision.Barcode;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Domain.FluProgram.Executant;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class BarcodeCmd : SupportDirectiveCmd
    {
        private BarcodePrm barcodePrm;
        public BarcodePrm BarcodePrm => barcodePrm;

        private PointD position;
        public PointD Position => this.position;

        public BarcodeCmd(RunnableModule runnableModule, BarcodeCmdLine barcodeCmdLine) : base(runnableModule, barcodeCmdLine)
        {
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            this.position = structure.ToMachine(runnableModule, barcodeCmdLine.PosInPattern);
            this.barcodePrm = barcodeCmdLine.BarcodePrm;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Barcode(this, coordinateCorrector);
        }
    }
}
