using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Drive.Motion.Command
{
    public struct MovePosPrm
    {
        public double Pos { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public double Dec { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMovePos : CommandMotion, ICloneable
    {
        public CommandMovePos(Axis[] axes, MovePosPrm[] movePosPrms)
            : base(axes)
        {
            this.MovePosPrms = movePosPrms;
            this.PosStss = new bool[axes.Length];
        }
        public CommandMovePos(Axis axis, MovePosPrm movePosPrm)
            : this(new Axis[] { axis }, new MovePosPrm[] { movePosPrm })
        {

        }

        [JsonProperty]
        public MovePosPrm[] MovePosPrms { get; private set; }

        public bool[] PosStss { get; private set; }

        public Action Starting;

        public override void Call()
        {
            this.State = CommandState.Running;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                this.Axes[i].State.SetBusy(AxisMoveMode.MovePos);
                this.Axes[i].Card.Executor.SetMovePos(
                    this.Axes[i].CardId,
                    this.Axes[i].AxisId,
                    this.Axes[i].ConvertPos2Card(MovePosPrms[i].Pos),
                    this.Axes[i].ConvertVel2Card(MovePosPrms[i].Vel),
                    MovePosPrms[i].Acc,
                    MovePosPrms[i].Dec);
                this.Axes[i].Card.Executor.MovePos(this.Axes[i].CardId, this.Axes[i].AxisId);
            }
            this.Starting?.Invoke();
        }

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

            bool flag = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                if (this.Axes[i].IsAlarm.IsRising || this.Axes[i].IsAlarm.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMovePosAlarm);
                    return;
                }
                if (this.Axes[i].IsError.IsRising || this.Axes[i].IsError.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Error);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMovePosAlarm);
                    return;
                }
                if (this.Axes[i].IsPLmt.IsRising || this.Axes[i].IsPLmt.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.PLmt);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.WarnMoveToPLmt);
                    return;
                }
                if (this.Axes[i].IsNLmt.IsRising || this.Axes[i].IsNLmt.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.NLmt);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.WarnMoveToNLmt);
                    return;
                }
                if (this.Axes[i].IsMoving.Value)
                {
                    flag = true;
                    this.PosStss[i] = true;
                }

                //if (!this.Axes[i].IsArrived.Value)
                //{
                //    flag = true;
                //}
            }
            if (flag)
            {
                this.State = CommandState.Running;
                return;
            }
            this.State = CommandState.Succeed;
            foreach (var item in this.Axes)
            {
                item.State.SetIdle();
            }
        }

        public override void HandleMsg(CmdMsgType msgType)
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
                        this.State = CommandState.Pause;
                        this.MoveSmoothStop();
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
                    this.MoveSmoothStop();
                    break;
                default:
                    break;
            }
        }

        public override object Clone()
        {
            return new CommandMovePos(this.Axes, this.MovePosPrms);
        }
    }
}
