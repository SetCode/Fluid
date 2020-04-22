using Anda.Fluid.Domain.FluProgram.Grammar;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class LoopBlockCmd : Command
    {
        private int start;
        public int Start
        {
            get { return start; }
        }

        private int end;
        public int End
        {
            get { return end; }
        }

        private List<DoMultipassCmd> doMultipassCmdList = new List<DoMultipassCmd>();
        public List<DoMultipassCmd> DoMultipassCmdList
        {
            get { return doMultipassCmdList; }
        }

        public LoopBlockCmd(RunnableModule runnableModule, LoopPassCmdLine loopPassCmdLine) : base(runnableModule)
        {
            start = loopPassCmdLine.Start;
            end = loopPassCmdLine.End;
        }
    }
}