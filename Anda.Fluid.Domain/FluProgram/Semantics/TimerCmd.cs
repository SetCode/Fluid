using Anda.Fluid.Domain.FluProgram.Grammar;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class TimerCmd : Command
    {
        private long waitInMills;
        public long WaitInMills
        {
            get { return waitInMills; }
        }

        public TimerCmd(RunnableModule runnableModule, long waitInMills) : base(runnableModule)
        {
            this.waitInMills = waitInMills;
        }

        public TimerCmd(RunnableModule runnableModule, TimerCmdLine timerCmdLine) : this(runnableModule, timerCmdLine.WaitInMills)
        {
        }
    }
}