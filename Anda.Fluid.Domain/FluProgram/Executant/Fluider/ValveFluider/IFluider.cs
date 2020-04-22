using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveFluider
{
    public interface IFluider
    {
        ITrackable GetDotable();

        ITrackable GetLinable();

        ITrackable GetArcable();

        ITrackable GetSymbolLinable();

        ITrackable GetMultiTracable();
    }
}
