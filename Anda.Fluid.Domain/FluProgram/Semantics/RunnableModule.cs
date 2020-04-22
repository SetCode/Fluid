using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    public enum ModuleMode
    {
        /// <summary>
        /// 双阀-主阀生产
        /// </summary>
        MainMode,
        /// <summary>
        /// 双阀-副阀同步
        /// </summary>
        SimulMode,
        /// <summary>
        /// 指定单阀1
        /// </summary>
        AssignMode1,
        /// <summary>
        /// 指定单阀2
        /// </summary>
        AssignMode2,
        /// <summary>
        /// 双阀跟随
        /// </summary>
        DualFallow,
        /// <summary>
        /// BadMark - 跳过生产
        /// </summary>
        SkipMode
    }
    /// <summary>
    /// 由于 CommandsModule 里面的命令在实际执行时，里面的轨迹命令的原点坐标实际是存储在
    /// DoCmdLine, DoMultipassCmdLine 中的，所以在解析成语义时，由于原点坐标不同，实际的DO / DO MULTIPASS 指令
    /// 关联的 pattern 也应该被对应解析成不同的命令模块对象，即 RunnableModule 。
    /// </summary>
    [Serializable]
    public class RunnableModule
    {
        private CommandsModule commandsModule;
        /// <summary>
        /// 对应的语法层的 CommandsModule
        /// </summary>
        public CommandsModule CommandsModule
        {
            get { return commandsModule; }
        }

        [NonSerialized]
        public double MeasuredHt;

        public int BoardNo { get; set; } = 0;

        private PointD origin = new PointD();
        /// <summary>
        /// 原点坐标（主阀坐标）
        /// </summary>
        public PointD Origin
        {
            get { return origin; }
        }

        private ValveType valva;
        /// <summary>
        /// 用于给点胶轨迹的胶阀属性赋值
        /// </summary>
        public ValveType Valve
        {
            get { return valva; }
            set { valva = value; }
        }

        /// <summary>
        /// 每个拼版偏移量
        /// </summary>
        public double ModuleOffsetX { get; set; } = 0;
        public double ModuleOffsetY { get; set; } = 0;

        /// <summary>
        /// 同步拼版的偏移量
        /// </summary>
        public double SimulModuleOffsetX { get; set; } = 0;
        public double SimulModuleOffsetY { get; set; } = 0;

        /// <summary>
        /// 记录当前Module的模式：
        /// MainMode(双阀主阀生产模式)
        /// SimulMode(双阀副阀同步模式)
        /// AssignMode1(单阀1指定模式)
        /// AssignMode2(单阀2指定模式)
        /// </summary>
        public ModuleMode Mode { get; set; } = ModuleMode.AssignMode1;

        /// <summary>
        /// 用于保存自动匹配之前的状态
        /// 仅在语法解析时赋值，请勿在其他位置赋值
        /// </summary>
        public ModuleMode SaveMode { get; set; } = ModuleMode.AssignMode1;

        public bool AdjustBegin = false;

        /// <summary>
        /// 双阀跟随到位标志
        /// </summary>
        public bool HasDualValveFallowed = false;

        /// <summary>
        /// 副阀同步坐标转换器
        /// </summary>
        [NonSerialized]
        private CoordinateTransformer simulTransformer;

        public CoordinateTransformer SimulTransformer
        {
            get { return simulTransformer; }
            set { simulTransformer = value; }
        }

        private List<Command> cmdList = new List<Command>();
        /// <summary>
        /// 语义指令列表
        /// </summary>
        public IReadOnlyList<Command> CmdList
        {
            get { return cmdList; }
        }

        /// <summary>
        /// 添加语义命令
        /// </summary>
        /// <param name="cmd"></param>
        public void AddCommand(Command cmd)
        {
            cmdList.Add(cmd);
        }

        /// <summary>
        /// 获取末尾的语义命令（不包含Mark点）
        /// </summary>
        public Command GetCommandAtTail()
        {
            if (cmdList.Count > 0)
            {
                return cmdList[cmdList.Count - 1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除末尾的语义命令（不包含Mark点，不断开命令与RunnableModule之间的关联）
        /// </summary>
        public void RemoveCommandAtTail()
        {
            if (cmdList.Count > 0)
            {
                cmdList.RemoveAt(cmdList.Count - 1);
            }
        }

        public RunnableModule(CommandsModule commandsModule, PointD origin)
        {
            this.commandsModule = commandsModule;
            //todo reverse
            this.origin.X = origin.X;
            this.origin.Y = origin.Y;
        }

    }
}
