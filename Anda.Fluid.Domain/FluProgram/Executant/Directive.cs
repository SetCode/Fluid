using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 控制外部参数、硬件的可执行指令
    /// </summary>
    public abstract class Directive : IMsgSender
    {
        public ValveType Valve { get; set; }

        /// <summary>
        /// 当前轨迹倾斜类型
        /// </summary>
        public TiltType Tilt { get; set; } = TiltType.NoTilt;

        //点胶数
        public int shortNum=0;

        /// <summary>
        /// 命令所属的程序
        /// </summary>
        public FluidProgram Program { get; protected set; }

        public RunnableModule RunnableModule { get; protected set; }

        public Command Command { get; set; }
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <returns>执行返回的结果</returns>
        public abstract Result Execute();
    }
}