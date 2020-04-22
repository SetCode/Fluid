using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.GearValveTracker;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveFluider
{
    public class GearValveFluider : IFluider
    {
        public ITrackable GetArcable()
        {
            return new GearValveArc();
        }

        public ITrackable GetDotable()
        {
            return new GearValveDot();
        }

        public ITrackable GetLinable()
        {
            return new GearValveLine();
        }

        public ITrackable GetMultiTracable()
        {
            return new GearValveMultiTraces();
        }

        public ITrackable GetSymbolLinable()
        {
            return new GearValveSymbolLines();
        }
    }
}
