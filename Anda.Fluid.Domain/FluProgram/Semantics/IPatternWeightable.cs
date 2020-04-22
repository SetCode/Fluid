using Anda.Fluid.Domain.FluProgram.Executant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    public interface IPatternWeightable
    {
         Directive ToDirectiveISpray();
    }
}
