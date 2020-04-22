using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anda.Fluid.Drive.Motion.Command
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CommandDelay : ICommandable
    {
        private DateTime dateTimeStart;

        public CommandDelay(TimeSpan delay)
        {
            this.Delay = delay;
            this.dateTimeStart = DateTime.Now;
        }

        public CommandState State { get; set; }

        [JsonProperty]
        public TimeSpan Delay { get; private set; }

        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);

        public Action OnFinished { get; set; }

        public Action OnDone { get; set; }

        public Action OnFailed { get; set; }

        public void Call()
        {
            this.State = CommandState.Running;
            this.dateTimeStart = DateTime.Now;
        }

        public bool Guard()
        {
            return true;
        }

        public void HandleMsg(CmdMsgType msgType)
        {
            switch (msgType)
            {
                case CmdMsgType.Pause:
                    if (this.State == CommandState.Running)
                    {
                        this.Delay = this.Delay - (DateTime.Now - this.dateTimeStart);
                        this.State = CommandState.Pause;
                    }
                    break;
                case CmdMsgType.Resume:
                    if (this.State == CommandState.Pause)
                    {
                        this.Call();
                    }
                    break;
                case CmdMsgType.Stop:
                    this.State = CommandState.Failed;
                    break;
            }
        }

        public void Update()
        {
            if(this.State != CommandState.Running)
            {
                return;
            }

            if((DateTime.Now - this.dateTimeStart) < this.Delay)
            {
                this.State = CommandState.Running;
                return;
            }

            this.State = CommandState.Succeed;
        }

        public object Clone()
        {
            return new CommandDelay(this.Delay);
        }
    }
}
