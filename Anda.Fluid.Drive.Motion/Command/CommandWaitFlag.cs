using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandWaitFlag : ICommandable
    {
        private bool flag = false;

        public CommandState State { get; set; }

        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);

        public Action OnFinished { get; set; }

        public Action OnDone { get; set; }

        public Action OnFailed { get; set; }

        public void Call()
        {
            this.State = CommandState.Running;
            this.flag = false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Guard()
        {
            return true;
        }

        public void HandleMsg(CmdMsgType msgType)
        {
            if (this.State == CommandState.Failed)
            {
                return;
            }

            switch (msgType)
            {
                case CmdMsgType.Pause:
                    break;
                case CmdMsgType.Resume:
                    break;
                case CmdMsgType.Stop:
                    break;
                case CmdMsgType.Flag:
                    this.flag = true;
                    break;
            }
        }

        public void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }

            if(!this.flag)
            {
                this.State = CommandState.Running;
                return;
            }

            this.State = CommandState.Succeed;
        }
    }
}
