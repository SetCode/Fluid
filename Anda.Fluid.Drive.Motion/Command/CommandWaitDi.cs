using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandWaitDi : ICommandable
    {
        private DateTime dateTimeStart;

        public CommandWaitDi(DI[] dd, bool[] diValues, TimeSpan timeout)
        {
            this.dateTimeStart = DateTime.Now;
            this.DIs = dd;
            this.DIValues = diValues;
            this.Timeout = timeout;
        }

        public CommandWaitDi(DI d, bool diValue, TimeSpan timeout)
            : this(new DI[] { d }, new bool[] { diValue }, timeout)
        {

        }

        public CommandState State { get; set; }

        public DI[] DIs { get; private set; }

        public bool[] DIValues { get; private set; }

        public TimeSpan Timeout { get; private set; }

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
            if (this.State == CommandState.Failed)
            {
                return;
            }

            switch (msgType)
            {
                case CmdMsgType.Pause:
                    if (this.State == CommandState.Running)
                    {
                        this.Timeout = this.Timeout - (DateTime.Now - this.dateTimeStart);
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
            if (this.State != CommandState.Running)
            {
                return;
            }

            if ((DateTime.Now - this.dateTimeStart) > this.Timeout)
            {
                this.State = CommandState.Failed;
                return;
            }

            bool flag = false;
            for (int i = 0; i < this.DIs.Length; i++)
            {
                if(this.DIs[i].Status.Value != this.DIValues[i])
                {
                    flag = true;
                }
            }
            if(flag)
            {
                this.State = CommandState.Running;
                return;
            }

            this.State = CommandState.Succeed;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
