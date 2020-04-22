using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker
{
    /// <summary>
    /// 轨迹能力接口，所有阀——轨迹组合都必须实现该接口
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Look模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result LookExecute(Directive directive);

        /// <summary>
        /// Dry模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result DryExecute(Directive directive);

        /// <summary>
        /// Wet模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result WetExecute(Directive directive);

        /// <summary>
        /// Adjust模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result AdjustExecute(Directive directive);

        /// <summary>
        /// InspectDot模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result InspectDotExecute(Directive directive);

        /// <summary>
        /// InspectRect模式下的执行逻辑
        /// </summary>
        /// <param name="directive"></param>
        Result InspectRectExecute(Directive directive);

        Result PatternWeightExecute(Directive directive, Valve valve);


    }
}
