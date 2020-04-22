using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveBufTracker;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveTracker;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveFluider
{
    internal class JtValveFluider : IFluider
    {
        public ITrackable GetArcable()
        {
            if (FluidProgram.Current.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.连续)
            {
                return new JtValveBufArc();
            }
            return new JtValveArc();
        }

        public ITrackable GetDotable()
        {
            if (FluidProgram.Current.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.连续)
            {
                return new JtValveBufDot();
            }
            return new JtValveDot();
        }

        public ITrackable GetLinable()
        {
            if (FluidProgram.Current.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.连续)
            {
                return new JtValveBufLine();
            }
            return new JtValveLine();
        }

        public ITrackable GetMultiTracable()
        {
            return new JtValveMultiTraces();
        }

        public ITrackable GetSymbolLinable()
        {
            return new JtValveSymbolLines();
        }


    }
}
