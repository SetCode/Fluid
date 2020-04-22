using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.SvValveTracker;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveFluider
{
    public class SvValveFluider : IFluider
    {
        public ITrackable GetArcable()
        {
            return new SvValveArc();
        }

        public ITrackable GetDotable()
        {
            return new SvValveDot();
        }

        public ITrackable GetLinable()
        {
            return new SvValveLine();
        }

        public ITrackable GetMultiTracable()
        {
            return new SvValveMultiTraces();
        }

        public ITrackable GetSymbolLinable()
        {
            return new SvValveSymbolLines();
        }
    }
}
