using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.GearValveTracker
{
    public class GearValveSymbolLines : ITrackable
    {
        public Result AdjustExecute(Directive directive)
        {
            return Result.OK;
        }

        public Result DryExecute(Directive directive)
        {
            return Result.OK;
        }

        public Result InspectDotExecute(Directive directive)
        {
            return Result.OK;
        }

        public Result InspectRectExecute(Directive directive)
        {
            return Result.OK;
        }

        public Result LookExecute(Directive directive)
        {
            return Result.OK;
        }

        public Result PatternWeightExecute(Directive directive, Valve valve)
        {
            return Result.OK;
        }

        public Result WetExecute(Directive directive)
        {
            return Result.OK;
        }
    }
}
