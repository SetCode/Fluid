using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Data
{
    public sealed class DataServer : TaskLoop
    {
        private readonly static DataServer instance = new DataServer();
        private DataServer() { }
        public static DataServer Instance => instance;

        private ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

        protected override void Loop()
        {
            Action action;
            if(this.queue.TryDequeue(out action))
            {
                action?.Invoke();
            }
        }

        public void Fire(Action action)
        {
            this.queue.Enqueue(action);
        }
    }
}
