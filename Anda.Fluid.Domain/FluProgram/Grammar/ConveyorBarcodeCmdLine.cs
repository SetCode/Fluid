using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class ConveyorBarcodeCmdLine : CmdLine  
    {
        public ConveyorBarcodeCmdLine() : base(true)
        {
        }

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // do nothing
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("CONVEYOR BARCODE").ToString();
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
