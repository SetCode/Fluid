using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveTracker
{
    public class JtValveMultiTraces : SymbolLinesTrackBase
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

        /// <summary>
        /// 获取线参数
        /// </summary>
        /// <param name="multiTraces"></param>
        /// <param name="lineStyle"></param>
        /// <returns></returns>
        private LineParam getLineParam(MultiTraces multiTraces, TraceBase trace)
        {
            return multiTraces.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam((LineStyle)trace.LineStyle);
        }

    }
}
