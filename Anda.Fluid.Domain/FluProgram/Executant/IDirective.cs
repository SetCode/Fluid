using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 控制外部参数、硬件的可执行指令
    /// </summary>
    public interface IDirective
    {
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <returns>执行返回的结果</returns>
        Result Execute();
    }
}