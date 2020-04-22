using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using System.ComponentModel;

namespace Anda.Fluid.Drive.Motion.Command
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveJog : CommandMotion
    {
        public CommandMoveJog(Axis axis, double vel, double acc)
            : base(axis)
        {
            this.Vel = vel;
            this.Acc = acc;
        }

        public override void Call()
        {
            this.State = CommandState.Running;
            this.Axes[0].State.SetBusy(AxisMoveMode.MoveJog);
            this.Axes[0].Card.Executor.MoveJog(
                this.Axes[0].CardId,
                this.Axes[0].AxisId,
                this.Axes[0].ConvertVel2Card(this.Vel),
                this.Acc);
        }

        [JsonProperty]
        public double Vel { get; private set; }

        [JsonProperty]
        public double Acc { get; private set; }

        public bool JogSts { get; private set; } = false;

        public override bool Guard()
        {
            return true;
        }

        public override void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }

            if (this.Axes[0].IsAlarm.IsRising)
            {
                this.Axes[0].State.SetAlarm(AxisAlarmType.Alarm);
                this.State = CommandState.Failed;
                AlarmServer.Instance.Fire(this.Axes[0], AlarmInfoMotion.FatalMoveJogAlarm);
                return;
            }
            if(this.Axes[0].IsError.IsRising)
            {
                this.Axes[0].State.SetAlarm(AxisAlarmType.Error);
                this.State = CommandState.Failed;
                AlarmServer.Instance.Fire(this.Axes[0], AlarmInfoMotion.FatalMoveJogError);
                return;
            }
            if(this.Axes[0].IsMoving.Value)
            {
                this.JogSts = true;
                this.State = CommandState.Running;
                return;
            }
            this.State = CommandState.Succeed;
            this.Axes[0].State.SetIdle();
        }

        public override void HandleMsg(CmdMsgType msgType)
        {
            if (this.State == CommandState.Failed)
            {
                return;
            }

            switch (msgType)
            {
                case CmdMsgType.Stop:
                    this.State = CommandState.Succeed;
                    this.MoveSmoothStop();
                    break;
                default:
                    break;
            }
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
