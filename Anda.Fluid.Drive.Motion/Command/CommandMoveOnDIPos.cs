using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandMoveOnDIPos : CommandMotion
    {
        public CommandMoveOnDIPos(Axis[] axes, MovePosPrm[] movePosPrms, DI d, StsType stsType)
            : base(axes)
        {
            this.MovePosPrms = movePosPrms;
            this.DI = d;
            this.StsType = stsType;
        }

        public CommandMoveOnDIPos(Axis axis, MovePosPrm movePosPrm, DI d, StsType stsType)
           : this(new Axis[] { axis }, new MovePosPrm[] { movePosPrm }, d, stsType)
        {

        }

        public MovePosPrm[] MovePosPrms { get; private set; }

        public DI DI { get; private set; }

        public StsType StsType { get; private set; }

        public Action ResultBack;

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
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Guard()
        {
            return true;
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

        public override void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }

            if(this.DI.Status.Is(this.StsType))
            {
                ResultBack?.Invoke();
                this.State = CommandState.Succeed;
                this.MoveSmoothStop();
                foreach (var item in this.Axes)
                {
                    item.State.SetIdle();
                }
                return;
            }

            bool flag = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                if (this.Axes[i].IsAlarm.IsRising)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMovePosAlarm);
                    return;
                }
                if (this.Axes[i].IsError.IsRising)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Error);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMovePosAlarm);
                    return;
                }
                if (this.Axes[i].IsMoving.Value)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                this.State = CommandState.Running;
                return;
            }
            this.State = CommandState.Failed;
            foreach (var item in this.Axes)
            {
                item.State.SetIdle();
            }
        }
    }
}
