using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Anda.Fluid.Infrastructure.Tasker
{
    public abstract class TaskLongRunning
    {
        private Task worker;
        protected CancellationTokenSource stopToken;

        public bool IsRunning => this.worker != null && !this.worker.IsCompleted;

        public void Start()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.stopToken = new CancellationTokenSource();
            this.worker = Task.Factory.StartNew(
                () => this.DoWork(),
                this.stopToken.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        public void Stop()
        {
            if (!this.IsRunning || this.stopToken.IsCancellationRequested)
            {
                return;
            }

            this.stopToken.Cancel();

            try
            {
                this.worker.Wait();
            }
            catch (AggregateException)
            {
                // in case the task was stopped before it could actually start, it will be canceled.
                if (this.worker.IsFaulted)
                {
                    throw;
                }
            }

            this.worker = null;
        }

        protected abstract void DoWork();
    }
}
