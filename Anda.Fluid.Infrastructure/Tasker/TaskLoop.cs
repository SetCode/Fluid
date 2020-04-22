using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Tasker
{
    public abstract class TaskLoop : TaskLongRunning 
    {
        protected int loopSleepMills = 2;

        protected override void DoWork()
        {
            while (!this.stopToken.IsCancellationRequested)
            {
                this.Loop();
                Thread.Sleep(loopSleepMills);
            }
        }

        protected abstract void Loop();
    }
}
