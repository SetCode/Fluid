using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.Tasker;

namespace Anda.Fluid.Drive.Motion.Scheduler
{
    public abstract class SchedulerBase<TCommand> : TaskLoop
        where TCommand : ICommandable 
    {
        private DateTime dateTime1 = DateTime.Now;
        private DateTime dateTime2 = DateTime.Now;
        private ConcurrentQueue<Telegram> msgQueue = new ConcurrentQueue<Telegram>();

        public List<IActive> Observers { get; private set; } = new List<IActive>();

        public TimeSpan CycleSpan { get; private set; }

        public void RegisterObserver(IActive device)
        {
            this.Observers.Add(device);
        }

        private void Dispatch(Telegram telegram)
        {
            List<IActive> observers;
            if(telegram.IsNotifyAll)
            {
                observers = this.Observers;
            }
            else
            {
                observers = telegram.Observers;
            }
            foreach (var item in observers)
            {
                item.HandleMsg(telegram.Msg);
            }

        }

        public void NotifyAll(CmdMsgType msg)
        {
            Telegram telegram = new Telegram()
            {
                Msg = msg,
                IsNotifyAll = true
            };
            this.msgQueue.Enqueue(telegram);
        }

        public void Notify(CmdMsgType msg, IActive observer)
        {
            Telegram telegram = new Telegram()
            {
                Msg = msg,
                IsNotifyAll = false,
                Observers = new List<IActive>() { observer }
            };
            this.msgQueue.Enqueue(telegram);
        }

        public void Notify(CmdMsgType msg, List<IActive> observers)
        {
            Telegram telegram = new Telegram()
            {
                Msg = msg,
                IsNotifyAll = false,
                Observers = observers
            };
            this.msgQueue.Enqueue(telegram);
        }

        protected override void Loop()
        {
            while (true)
            {
                Telegram telegram;
                if (this.msgQueue.TryDequeue(out telegram))
                {
                    this.Dispatch(telegram);
                }
                else
                {
                    break;
                }
            }

            this.dateTime1 = DateTime.Now;

            this.UpdateFirst();

            foreach (var item in this.Observers)
            {
                item.Update();
            }

            this.dateTime2 = DateTime.Now;

            this.CycleSpan = this.dateTime2 - this.dateTime1;
        }

        protected abstract void UpdateFirst();
    }
}
