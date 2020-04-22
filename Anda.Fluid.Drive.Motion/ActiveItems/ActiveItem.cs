using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.Scheduler;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public abstract class ActiveItem : IActive
    {
        private ConcurrentQueue<ICommandable> commandQueue = new ConcurrentQueue<ICommandable>();
        private ICommandable currentCommand = null;

        public ActiveItem()
        {

        }

        public bool StopFlag { get; set; } = false;

        public ActiveItemState State { get; set; } = ActiveItemState.Idel;

        public ConcurrentQueue<ICommandable> CommandQueue => this.commandQueue;

        public ICommandable CurrentCommand => this.currentCommand;

        public void HandleMsg(CmdMsgType msgType)
        {
            if(this.currentCommand != null)
            {
                //this.State = ActiveItemState.Busy;
                this.currentCommand.HandleMsg(msgType);
            }
            else
            {
                //this.State = ActiveItemState.Idel;
            }
        }

        public void Update()
        {
            if (this.currentCommand == null)
            {
                this.commandQueue.TryDequeue(out this.currentCommand);
            }

            if(this.currentCommand != null)
            {
                if(this.StopFlag)
                {
                    this.Stop();
                }

                this.currentCommand.Update();

                switch (this.currentCommand.State)
                {
                    case CommandState.Idel:
                        if (this.currentCommand.Guard())
                        {
                            this.currentCommand.Call();
                        }
                        break;
                    case CommandState.Running:
                        break;
                    case CommandState.Pause:
                        break;
                    case CommandState.Succeed:
                        this.currentCommand.AutoEvent.Set();
                        this.currentCommand.OnFinished?.Invoke();
                        this.currentCommand = null;
                        this.State = ActiveItemState.Idel;
                        break;
                    case CommandState.Failed:
                        this.currentCommand.AutoEvent.Set();
                        this.currentCommand.OnFinished?.Invoke();
                        this.currentCommand = null;
                        this.State = ActiveItemState.Idel;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Fire(ICommandable command)
        {
            this.State = ActiveItemState.Busy;
            this.commandQueue.Enqueue(command);
        }

        public void WaitFlag()
        {
            CommandWaitFlag command = new CommandWaitFlag();
            this.Fire(command);
        }

        public void SetFlag()
        {
            SchedulerMotion.Instance.Notify(CmdMsgType.Flag, this);
        }

        public void Pause()
        {
            SchedulerMotion.Instance.Notify(CmdMsgType.Pause, this);
        }

        public void Resume()
        {
            SchedulerMotion.Instance.Notify(CmdMsgType.Resume, this);
        }

        public void Stop()
        {
            SchedulerMotion.Instance.Notify(CmdMsgType.Stop, this);
        }

        public void Delay(TimeSpan delay)
        {
            CommandDelay command = new CommandDelay(delay);
            this.Fire(command);
        }

        public Result DelayAndReply(TimeSpan delay)
        {
            CommandDelay command = new CommandDelay(delay);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public Result WaitCommandReply(ICommandable command)
        {
            command.AutoEvent.WaitOne();
            if (command.State == CommandState.Failed)
            {
                return Result.FAILED;
            }
            return Result.OK;
        }
    }
}
