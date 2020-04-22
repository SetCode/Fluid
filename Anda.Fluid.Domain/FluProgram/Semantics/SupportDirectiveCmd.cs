using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    /// <summary>
    /// 支持生成Directive指令的Command
    /// </summary>
    [Serializable]
    public abstract class SupportDirectiveCmd : Command
    {
        public ValveType Valve { get; set; }

        /// <summary>
        /// 当前轨迹倾斜类型
        /// </summary>
        public TiltType Tilt { get; set; } = TiltType.NoTilt;

        private CmdLine cmdLine;
        /// <summary>
        /// 与当前Command对应的CmdLine
        /// </summary>
        public CmdLine CmdLine
        {
            get { return cmdLine; }
        }
     

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cmdLine">与当前Command对应的CmdLine</param>
        public SupportDirectiveCmd(RunnableModule runnableModule, CmdLine cmdLine) : base(runnableModule)
        {
            this.cmdLine = cmdLine;
            this.Tilt = this.cmdLine.Tilt;
        }

        /// <summary>
        /// 根据当前Command生成对应的Directive指令
        /// </summary>
        /// <returns></returns>
        public abstract Directive ToDirective(CoordinateCorrector coordinateCorrector);
    }
}