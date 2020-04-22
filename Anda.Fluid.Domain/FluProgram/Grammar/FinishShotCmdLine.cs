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
    public class FinishShotCmdLine : DotCmdLine
    {
        public FinishShotCmdLine():base()
        {

        }

        public override object Clone()
        {
            FinishShotCmdLine dotCmdLine = MemberwiseClone() as FinishShotCmdLine;
            dotCmdLine.position = position.Clone() as PointD;
            return dotCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("FINISH SHOT : ").Append((int)DotStyle + 1);
            sb.Append(", ").Append(MachineRel(position));
            return sb.ToString();
        }

      
    }
}
