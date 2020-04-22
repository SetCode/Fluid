using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Newtonsoft.Json;

namespace Anda.Fluid.Drive.Motion.Command
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CommandMotion : ICommandable
    {  
        public CommandMotion(params Axis[] axes)
        {
            this.Axes = axes;
        }

        public CommandMotion(Axis axis)
            : this(new Axis[] { axis })
        {
        }

        [JsonProperty]
        public short[] AxesKeys { get; private set; }

        public Axis[] Axes { get; private set; }

        public CommandState State { get; set; }

        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);

        public Action OnFinished { get; set; }

        public Action OnDone { get; set; }

        public Action OnFailed { get; set; }

        public abstract void Call();

        public abstract bool Guard();

        public abstract void Update();

        public abstract void HandleMsg(CmdMsgType msgType);

        protected short MoveSmoothStop()
        {
            foreach (var item in this.Axes)
            {
                item.Card.Executor.MoveSmoothStop(item.CardId, item.AxisId);
            }
            return 0;
        }

        protected short MoveAbruptStop()
        {
            foreach (var item in this.Axes)
            {
                item.Card.Executor.MoveAbruptStop(item.CardId, item.AxisId);
            }
            return 0;
        }

        public abstract object Clone();
    }
}
