using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Anda.Fluid.Drive
{
    public class DbService : TaskLoop
    {
        private readonly static DbService instance = new DbService();
        private DbService() { }
        public static DbService Instance => instance;
        
        private ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();
        public bool Enable = false;
        protected override void Loop()
        {            
            Action action;
            if (queue.TryDequeue(out action))
            {
                if (this.Enable)
                {
                    action?.Invoke();
                }
                
            }
        }

        public void Fire(Action action)
        {
            this.queue.Enqueue(action);
        }
    }
}
