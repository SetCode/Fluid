using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Domain.FluProgram.Executant;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class FinishShotCmd : DotCmd
    {
        public FinishShotCmd(RunnableModule runnableModule, FinishShotCmdLine dotCmdLine, MeasureHeightCmd mhCmd) : base(runnableModule, dotCmdLine, mhCmd)
        {
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return base.ToDirective(coordinateCorrector);
        }
    }
}
