using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandSetDo : ICommandable
    {
        public CommandSetDo(DO[] dd, bool[] dOValues)
        {
            this.DOs = dd;
            this.DOValues = dOValues;
        }

        public CommandSetDo(DO d, bool dOValue)
            : this(new DO[] { d }, new bool[] { dOValue })
        {

        }

        public CommandState State { get; set; }

        public DO[] DOs { get; private set; }

        public bool[] DOValues { get; private set; }

        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);

        public Action OnFinished { get; set; }

        public Action OnDone { get; set; }

        public Action OnFailed { get; set; }

        public void Call()
        {
            for (int i = 0; i < this.DOs.Length; i++)
            {
                this.DOs[i].Set(this.DOValues[i]);
            }
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
       
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
