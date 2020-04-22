using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    /// <summary>
    /// 存储一组命令的集合，例如Program、Workpiece、Pattern
    /// </summary>
    [Serializable]
    public abstract class CommandsModule
    {
        protected string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        protected List<CmdLine> cmdLinesReversed = new List<CmdLine>();
        public List<CmdLine> CmdLinesReversed { get { return cmdLinesReversed; } }

        
        public string ReverseName { get { return name + "Sort"; } }

        public bool IsReversePattern = false;

        public bool IsModified = false;
        [NonSerialized]
        private bool lineAdjusted = false;
        public bool LineAdjusted
        {
            get
            {
                return this.lineAdjusted;
            }
            set
            {
                this.lineAdjusted = value;
            }
        }

        internal FluidProgram program;
        /// <summary>
        /// 所属的Program
        /// </summary>
        public FluidProgram Program
        {
            get { return program; }
        }

        private List<CmdLine> cmdLineList = new List<CmdLine>();
       
        /// <summary>
        /// 语法指令列表
        /// </summary>
        public List<CmdLine> CmdLineList
        {
            get { return cmdLineList; }
        }
        /// <summary>
        /// 轨迹逆
        /// </summary>
        //protected List<CmdLine> cmdLinesReversed = new List<CmdLine>();
        //public List<CmdLine> CmdLinesReversed
        //{
        //    get { return cmdLinesReversed; }
        //}
        public bool NeedReverse = false;
        public CommandsModule(FluidProgram program)
        {
            this.program = program;
        }

        public void AddCmdLine(CmdLine cmdLine)
        {
            cmdLineList.Add(cmdLine);
            cmdLine.CommandsModule = this;
        }

        public void InsertCmdLine(int index, CmdLine cmdLine)
        {
            cmdLineList.Insert(index, cmdLine);
            cmdLine.CommandsModule = this;
        }

        public void InsertCmdLineRange(int index, IEnumerable<CmdLine> cmdLineList)
        {
            if (cmdLineList == null)
            {
                return;
            }
            this.cmdLineList.InsertRange(index, cmdLineList);
            foreach (CmdLine cmdLine in cmdLineList)
            {
                cmdLine.CommandsModule = this;
            }
        }

        public void RemoveCmdLineAt(int index)
        {
            cmdLineList[index].CommandsModule = null;
            cmdLineList.RemoveAt(index);
        }

        public int FindCmdLineIndex(CmdLine cmdLine)
        {
            return cmdLineList.FindIndex(item => item.Equals(cmdLine));
        }

        public DoCmdLine GetWorkPieceCmdLine()
        {
            foreach (var item in this.cmdLineList)
            {
                if(item is DoCmdLine)
                {
                    var w = item as DoCmdLine;
                    if(w.PatternName == Workpiece.WORKPIECE_NAME)
                    {
                        return w;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取所有Mark点命令行
        /// </summary>
        /// <returns></returns>
        public List<MarkCmdLine> GetMarkCmdLines()
        {
            List<MarkCmdLine> markCmdLines = new List<MarkCmdLine>();
            int count = 0;
            foreach (CmdLine cmdLine in cmdLineList)
            {
                if (!cmdLine.Enabled)
                {
                    continue;
                }
                if (cmdLine is MarkCmdLine)
                {
                    markCmdLines.Add(cmdLine as MarkCmdLine);
                    count++;
                    // 每个Pattern最多只会有2个Mark点
                    if (count == 2)
                    {
                        break;
                    }
                }
                // 语法约束了Mark点命令行一定在在最前面
                else if (!(cmdLine is CommentCmdLine))
                {
                    break;
                }
            }
            return markCmdLines;
        }

        /// <summary>
        /// 获取所有Mark点命令行(不管有没有启用)
        /// </summary>
        /// <returns></returns>
        public List<MarkCmdLine> GetMarkCmdLines2()
        {
            List<MarkCmdLine> markCmdLines = new List<MarkCmdLine>();
            int count = 0;
            foreach (CmdLine cmdLine in cmdLineList)
            {
                if (!cmdLine.Enabled && !(cmdLine is MarkCmdLine))
                {
                    continue;
                }
                if (cmdLine is MarkCmdLine)
                {
                    markCmdLines.Add(cmdLine as MarkCmdLine);
                    count++;
                    // 每个Pattern最多只会有2个Mark点
                    if (count == 2)
                    {
                        break;
                    }
                }
                // 语法约束了Mark点命令行一定在在最前面
                else if (!(cmdLine is CommentCmdLine))
                {
                    break;
                }
            }
            return markCmdLines;
        }

        /// <summary>
        /// 获取当前pattern的badmark
        /// </summary>
        /// <returns></returns>
        public BadMarkCmdLine GetBadMarkCmdLine()
        {
            foreach (CmdLine cmdLine in cmdLineList)
            {
                if (!cmdLine.Enabled)
                {
                    continue;
                }
                if (cmdLine is BadMarkCmdLine)
                {
                    BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                    return badMarkCmdLine;
                }
                // 语法约束了Mark点命令行一定在在非Mark指令的前面
                else if (!(cmdLine is CommentCmdLine)&&!(cmdLine is MarkCmdLine))
                {
                    break;
                }
            }
            return null;
        }

        public List<NozzleCheckCmdLine> GetNozzleCmdLines()
        {
            List<NozzleCheckCmdLine> nozzleCheckCmdLines = new List<NozzleCheckCmdLine>();
            foreach (CmdLine cmdLine in cmdLineList)
            {
                if (!cmdLine.Enabled)
                {
                    continue;
                }
                if (cmdLine is NozzleCheckCmdLine)
                {
                    NozzleCheckCmdLine nozzleCheckCmdLine = cmdLine as NozzleCheckCmdLine;
                    nozzleCheckCmdLines.Add(nozzleCheckCmdLine);
                }
            }
            return nozzleCheckCmdLines;
        }

        /// <summary>
        /// 程序加载后需要对Mark点初始化//初始化badmark的模板
        /// </summary>
        public void InitMarks()
        {
            foreach (MarkCmdLine markCmdLine in GetMarkCmdLines2())
            {
                Log.Print("Init ModelFindPrm in pattern " + name);
                markCmdLine.ModelFindPrm.Init();
            }
            BadMarkCmdLine badMarkCmdLine = GetBadMarkCmdLine();
            if (badMarkCmdLine!=null)
            {
                badMarkCmdLine.ModelFindPrm.Init();
            }
            foreach (NozzleCheckCmdLine nozzleCheckCmdLine in GetNozzleCmdLines())
            {
                Log.Print("Init nozzle ModelFindPrm in pattern " + name);
                nozzleCheckCmdLine.ModelFindPrm.Init();
            }
        }
    }
}