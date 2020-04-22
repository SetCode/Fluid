using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.ActiveItems;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandCapture : ICommandable
    {
        public CommandState State { get; set; }

        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);

        public Action OnFinished { get; set; }

        public Action OnDone { get; set; }

        public Action OnFailed { get; set; }

        public void Call()
        {
            this.State = CommandState.Running;
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
            
        }

        public void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }
        }
    }
}
