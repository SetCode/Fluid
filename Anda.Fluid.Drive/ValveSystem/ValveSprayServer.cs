using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem
{
    class ValveSprayServer : TaskLoop
    {
        private readonly static ValveSprayServer instance = new ValveSprayServer();
        private ValveSprayServer() { }
        public static ValveSprayServer Instance => instance;

        public AutoResetEvent StartSprayEvent { get; private set; } = new AutoResetEvent(false);

        public TimeSpan SleepSpan;

        public Action SprayAction;

        protected override void Loop()
        {
            StartSprayEvent.WaitOne();
            Thread.Sleep(SleepSpan);
            SprayAction?.Invoke();
        }
    }
}
