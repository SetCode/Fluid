using Anda.Fluid.Domain.FluProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class ChangeSpeedCmd : Command
    {
        private int waitInMills, speed;
        public int WaitInMills
        {
            get { return waitInMills; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public ChangeSpeedCmd(RunnableModule runnableModule,int speed,int waitInMills) : base(runnableModule)
        {
            this.speed = speed;
            this.waitInMills = waitInMills;
        }

        public ChangeSpeedCmd(RunnableModule runnableModule,ChangeSpeedCmdLine changeSpeedCmdLine):this(runnableModule,changeSpeedCmdLine.Speed,changeSpeedCmdLine.WaitInMills)
        {

        }
    }
}
