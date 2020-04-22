using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Anda.Fluid.Domain.FluProgram
{
    [Serializable]
    public class Pattern : CommandsModule
    {
        public ValveType Valve { get; set; } = ValveType.Valve1;

        /// <summary>
        /// 原点产品坐标
        /// </summary>
        public PointD Origin { get; set; } = new PointD();

       
        /// <summary>
        /// 是否存在pass block命令
        /// </summary>
        public bool HasPassBlocks
        {
            get
            {
                foreach (CmdLine cmdLine in CmdLineList)
                {
                    if ((cmdLine is StartPassCmdLine || cmdLine is EndPassCmdLine) && cmdLine.Enabled)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 判断当前pattern中是否有引用自身的指令
        /// </summary>
        public bool IsContainItself
        {
            get
            {
                foreach (CmdLine cmdLine in CmdLineList)
                {
                    if ((cmdLine is DoCmdLine || cmdLine is DoMultiPassCmdLine || cmdLine is StepAndRepeatCmdLine))
                    {
                        if (cmdLine is DoCmdLine)
                        {
                            DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                            if (doCmdLine.PatternName == this.name)
                            {
                                return true;
                            }
                        }
                        else if (cmdLine is DoMultiPassCmdLine)
                        {
                            DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                            if (doMultiPassCmdLine.PatternName == this.name)
                            {
                                return true;
                            }
                        }
                        else if (cmdLine is StepAndRepeatCmdLine)
                        {
                            StepAndRepeatCmdLine stepAndRepeatCmdLine = cmdLine as StepAndRepeatCmdLine;
                            foreach (DoCmdLine item in stepAndRepeatCmdLine.DoCmdLineList)
                            {
                                if (item.PatternName == this.name)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 加载显示该Pattern时是否需要拍摄Mark点校正原点和轨迹坐标
        /// </summary>
        public bool NeedMarkCorrect { get; set; }

        public Pattern(FluidProgram program, string name, double originX, double originY) : base(program)
        {
            this.name = name;
            Origin.X = originX;
            Origin.Y = originY;
            //this.ParentPattern = program.Workpiece;
            //program.Workpiece.AddChild(this);
            AddCmdLine(new EndCmdLine());
        }

        /// <summary>
        /// 获取原点系统坐标
        /// </summary>
        /// <returns></returns>
        public PointD GetOriginSys()
        {
            return Origin + this.program.Workpiece.GetOriginPos().ToSystem();
        }

        /// <summary>
        /// 获取原点机械坐标
        /// </summary>
        /// <returns></returns>
        public virtual PointD GetOriginPos()
        {
            return GetOriginSys().ToMachine();
        }

        /// <summary>
        /// Pattern中的系统相对坐标->机械相对坐标
        /// </summary>
        /// <param name="p">Pattern中的系统相对坐标</param>
        /// <returns></returns>
        public PointD MachineRel(PointD p)
        {
            return ((GetOriginSys() + p).ToMachine() - GetOriginPos()).ToPoint();
        }

        public PointD MachineRel(double x, double y)
        {
            return MachineRel(new PointD(x, y));
        }

        /// <summary>
        /// Pattern中的系统相对坐标->机械绝对坐标
        /// </summary>
        /// <param name="p">Pattern中的系统相对坐标</param>
        /// <returns></returns>
        public PointD MachineAbs(PointD p)
        {
            return (GetOriginSys() + p).ToMachine();
        }

        /// <summary>
        /// Pattern中的机械相对坐标->系统相对坐标
        /// </summary>
        /// <param name="p">Pattern中的机械相对坐标</param>
        /// <returns></returns>
        public PointD SystemRel(PointD p)
        {
            return ((GetOriginPos() + p).ToSystem() - GetOriginSys()).ToPoint();
        }

        public PointD SystemRel(double x, double y)
        {
            return SystemRel(new PointD(x, y));
        }


        /// <summary>
        /// 将命令 逆序
        /// </summary>
        public List<CmdLine> ReverseCmdLines()
        {            
            int count = this.CmdLineList.Count;
            cmdLinesReversed.Clear();
            int preIndex = -1;
            for (int i = 0; i < count; i++)
            {
                CmdLine cmdLine = this.CmdLineList[i];
                if (cmdLine is MarkCmdLine)
                {
                    preIndex = i;
                    cmdLinesReversed.Add(cmdLine);
                }
                else if (cmdLine is BadMarkCmdLine)
                {
                    preIndex = i;
                    cmdLinesReversed.Add(cmdLine);
                }
                else if (cmdLine is MeasureHeightCmdLine)
                {
                    preIndex = i;
                    cmdLinesReversed.Add(cmdLine);
                }

            }
            for (int i = this.CmdLineList.Count - 2; i > preIndex; i--)
            {
                CmdLine cmdLine = this.CmdLineList[i];
                cmdLinesReversed.Add(cmdLine);
            }
            cmdLinesReversed.Add(this.CmdLineList[this.CmdLineList.Count - 1]);

            return null;

        }

        [NonSerialized]
        private Pattern PatternReversed;

        /// <summary>
        /// 产生逆pattern
        /// </summary>
        public Pattern ReversePattern()
        {
            //当修改pattern时，PatternReversd!=null，这样的结果是修改后的pattern轨迹没有逆序
            //添加了IsModified，当pattern修改后IsModified为true
            if (PatternReversed != null && !this.IsModified)
            {
                return PatternReversed;
            }

            Pattern newPattern = new Pattern(FluidProgram.Current, this.ReverseName, this.Origin.X, this.Origin.Y);
            newPattern.IsReversePattern = true;
            int count = this.CmdLineList.Count;
            newPattern.CmdLineList.Clear();
            int preIndex = -1;
            for (int i = 0; i < count; i++)
            {
                CmdLine cmdLine = this.CmdLineList[i];
                if (cmdLine is MarkCmdLine)
                {
                    preIndex = i;
                    newPattern.CmdLineList.Add(cmdLine);
                }
                else if (cmdLine is BadMarkCmdLine)
                {
                    preIndex = i;
                    newPattern.CmdLineList.Add(cmdLine);
                }
                else if (cmdLine is MeasureHeightCmdLine)
                {
                    preIndex = i;
                    newPattern.CmdLineList.Add(cmdLine);
                }
                else if (cmdLine is BarcodeCmdLine)
                {
                    preIndex = i;
                    newPattern.CmdLineList.Add(cmdLine);
                }

            }
            for (int i = this.CmdLineList.Count - 2; i > preIndex; i--)
            {
                CmdLine cmdLine = this.CmdLineList[i];
               
                newPattern.CmdLineList.Add(cmdLine);
            }
            newPattern.CmdLineList.Add(this.CmdLineList[this.CmdLineList.Count - 1]);
            PatternReversed = newPattern;
            return newPattern;

        }

        /// <summary>
        /// 父级Pattern，用于编辑命令时保存Pattern引用关系
        /// </summary>
        [NonSerialized]
        public Pattern ParentPattern;

        /// <summary>
        /// 子Pattern集合，用于编辑Pattern时，校正本级Pattern，同时校正子Pattern
        /// </summary>
        [NonSerialized]
        private List<Pattern> children = new List<Pattern>();
        public List<Pattern> getChildren()
        {
            if(children == null)
            {
                children = new List<Pattern>();
            }
            return children;
        }

        /// <summary>
        /// 添加子Pattern，同类型的子Pattern只添加一个
        /// </summary>
        /// <param name="pattern"></param>
        public void AddChild(Pattern pattern)
        {
            var p = children.Find(x => x.Name == pattern.Name);
            if(p != null)
            {
                return;
            }
            children.Add(pattern);
        }
    }
}