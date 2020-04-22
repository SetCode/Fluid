using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveBufTracker
{
    public class JtValveBufDot : LineTrackBase
    {
        public override Result AdjustExecute(Directive directive)
        {
            return Result.OK;
        }

        public override Result DryExecute(Directive directive)
        {
            return Result.OK;
        }

        public override Result InspectDotExecute(Directive directive)
        {
            return Result.OK;
        }

        public override Result InspectRectExecute(Directive directive)
        {
            return Result.OK;
        }

        public override Result LookExecute(Directive directive)
        {
            return Result.OK;
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            return Result.OK;
        }
    }
}
