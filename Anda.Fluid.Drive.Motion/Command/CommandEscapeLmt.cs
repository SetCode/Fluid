using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure.Alarming;
using Newtonsoft.Json;
using System.Diagnostics;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.Motion.Command
{
    public enum LmtType
    {
        Positive,
        Negtive
    }

    public struct EscapeLmtPrm
    {
        public LmtType Lmt { get; set; }  
        public double Step { get; set; }  
        public double Vel { get; set; }
        public double Acc { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandEscapeLmt : CommandMotion
    {
        private double[] axesTemp;
        private Sts[] stopStss;

        public CommandEscapeLmt(Axis[] axes, EscapeLmtPrm[] escapeLmtPrm)
            : base(axes)
        {
            this.EscapeLmtPrms = escapeLmtPrm;
            this.axesTemp = new double[axes.Length];
            this.stopStss = new Sts[axes.Length];
        }

        public CommandEscapeLmt(Axis axis, EscapeLmtPrm escapeLmtPrm)
            : this(new Axis[] { axis }, new EscapeLmtPrm[] { escapeLmtPrm })
        {

        }

        [JsonProperty]
        public EscapeLmtPrm[] EscapeLmtPrms { get; private set; }

        public override void Call()
        {
            this.State = CommandState.Running;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                this.Axes[i].State.SetBusy(AxisMoveMode.EscapeLmt);
                this.stopStss[i] = new Sts();
                EscapeLmtPrm escapeLmtPrm = this.EscapeLmtPrms[i];
                double pos = this.Axes[i].Pos + escapeLmtPrm.Step;
                this.Axes[i].Card.Executor.SetMovePos(
                    this.Axes[i].CardId,
                    this.Axes[i].AxisId,
                    this.Axes[i].ConvertPos2Card(pos),
                    this.Axes[i].ConvertVel2Card(escapeLmtPrm.Vel),
                    escapeLmtPrm.Acc,
                    escapeLmtPrm.Acc);
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

            bool flagLmt = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                if (this.Axes[i].IsAlarm.IsRising)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalEscapeLmtAlarm);
                    return;
                }
                if (this.Axes[i].IsError.IsRising)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Error);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalEscapeLmtError);
                    return;
                }

                switch (this.EscapeLmtPrms[i].Lmt)
                {
                    case LmtType.Positive:
                        if (this.Axes[i].IsPLmt.Value)
                        {
                            this.Axes[i].ClrSts();
                            flagLmt = true;
                        }
                        else if (this.Axes[i].IsPLmt.IsFalling)
                        {
                            this.axesTemp[i] = this.Axes[i].Pos;
                        }
                        break;
                    case LmtType.Negtive:
                        if (this.Axes[i].IsNLmt.Value)
                        {
                            this.Axes[i].ClrSts();
                            flagLmt = true;
                        }
                        else if (this.Axes[i].IsNLmt.IsFalling)
                        {
                            this.axesTemp[i] = this.Axes[i].Pos;
                        }
                        break;
                }
            }
            if (flagLmt)
            {
                this.State = CommandState.Running;
                return;
            }

            bool flag = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                switch (this.EscapeLmtPrms[i].Lmt)
                {
                    case LmtType.Positive:
                        if (this.axesTemp[i] - this.Axes[i].Pos > 2)
                        {
                            this.stopStss[i].Update(true);
                            if (this.stopStss[i].IsRising)
                            {
                                this.Axes[i].Card.Executor.MoveSmoothStop(this.Axes[i].CardId, this.Axes[i].AxisId);
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                        break;
                    case LmtType.Negtive:
                        if (this.Axes[i].Pos - this.axesTemp[i] > 2)
                        {
                            this.stopStss[i].Update(true);
                            if (this.stopStss[i].IsRising)
                            {
                                this.Axes[i].Card.Executor.MoveSmoothStop(this.Axes[i].CardId, this.Axes[i].AxisId);
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                        break;
                }

                if(this.Axes[i].IsMoving.Value)
                {
                    flag = true;
                }
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
    }
}
