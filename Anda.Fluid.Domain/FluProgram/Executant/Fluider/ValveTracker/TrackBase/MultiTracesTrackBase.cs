using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase
{
    public abstract class MultiTracesTrackBase : ITrackable
    {
        public abstract Result AdjustExecute(Directive directive);
        public abstract Result DryExecute(Directive directive);
        public abstract Result InspectDotExecute(Directive directive);
        public abstract Result InspectRectExecute(Directive directive);
        public abstract Result LookExecute(Directive directive);
        public abstract Result PatternWeightExecute(Directive directive, Valve valve);
        public abstract Result WetExecute(Directive directive);
    }
}
