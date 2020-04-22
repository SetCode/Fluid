using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Domain.FluProgram.Grammar;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class ConveyorBarcodeCmd : SupportDirectiveCmd
    {
        public ConveyorBarcodeCmd(RunnableModule runnableModule, ConveyorBarcodeCmdLine conveyorBarcodeCmdLine) 
            : base(runnableModule, conveyorBarcodeCmdLine)
        {
            
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new ConveyorBarcode(this);
        }
    }
}
