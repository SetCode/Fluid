using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class CircleCmd : ArcCmd
    {
        public CircleCmd(RunnableModule runnableModule, CircleCmdLine circleCmdLine,MeasureHeightCmd mhCmd) 
            : base(runnableModule, circleCmdLine,mhCmd)
        {
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return base.ToDirective(coordinateCorrector);
        }
    }
}