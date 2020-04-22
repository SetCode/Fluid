using Anda.Fluid.Domain.FluProgram.Grammar;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class PassBlockCmd : Command
    {
        private int index;
        public int Index
        {
            get { return index; }
        }

        private List<Command> cmdList = new List<Command>();
        public List<Command> CmdList
        {
            get { return cmdList; }
        }

        public PassBlockCmd(RunnableModule runnableModule, int index) : base(runnableModule)
        {
            this.index = index;
        }

        public PassBlockCmd(RunnableModule runnableModule, StartPassCmdLine startPassCmdLine) : this(runnableModule, startPassCmdLine.Index)
        {
        }
    }
}