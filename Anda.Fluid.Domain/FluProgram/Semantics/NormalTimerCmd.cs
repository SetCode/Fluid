using Anda.Fluid.Domain.FluProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class NormalTimerCmd : Command
    {
        private int waitInMills;
        public int WaitInMills
        {
            get { return waitInMills; }
        }

        public NormalTimerCmd(RunnableModule runnableModule, int waitInMills) : base(runnableModule)
        {
            this.waitInMills = waitInMills;
        }

        public NormalTimerCmd(RunnableModule runnableModule, NormalTimerCmdLine timerCmdLine) 
            : this(runnableModule, timerCmdLine.WaitInMills)
        {
        }
    }
}
