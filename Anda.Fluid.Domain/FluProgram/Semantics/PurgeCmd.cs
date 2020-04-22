using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    //Edite By Shawn
    [Serializable]
    public class PurgeCmd : SupportDirectiveCmd
    {
        public PurgeCmd(RunnableModule runnableModule, PurgeCmdLine purgeCmdLine) : base(runnableModule, purgeCmdLine)
        {
            this.Valve = purgeCmdLine.Valve;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Purge(this);
        }
    }
}
