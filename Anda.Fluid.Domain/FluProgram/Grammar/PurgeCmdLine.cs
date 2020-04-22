using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    //Edite By Shawn
    [Serializable]
    public class PurgeCmdLine : CmdLine
    {
        public PurgeCmdLine() : base(false)
        {

        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("DoPurge:").ToString();
        }

     
    }
}
