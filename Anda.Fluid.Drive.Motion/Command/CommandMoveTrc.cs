using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class MoveTrcPrm
    {
        public short CsId { get; set; }
        public double VelMax { get; set; }
        public double AccMax { get; set; }
        public short EvenTime { get; set; }
        public double OrgX { get; set; }
        public double OrgY { get; set; }
    }

    public class MoveTrcSts
    {
        public short Run { get; set; }
        public int Space { get; set; }
        public int Segment { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveTrc : CommandMotion, ICloneable
    {
        private Queue<ICrdable> crdQueue;
        protected const int MAX_ONCE = 100;
        private int space = 0;
        protected Card card = null;

        public CommandMoveTrc(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, IList<ICrdable> crds)
           : base(axisX, axisY)
        {
            if (axisX.Card != axisY.Card)
            {
                throw new Exception("axis x and axis y must be on the same card!");
            }
            this.card = axisX.Card;
            this.TrcPrm = trcPrm;
            this.Crds = crds;
            this.TrcSts = new MoveTrcSts();
            this.crdQueue = new Queue<ICrdable>();
        }

        public CommandMoveTrc(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, ICrdable crd)
          : this(axisX, axisY, trcPrm, new List<ICrdable>() { crd })
        {

        }

        public CommandMoveTrc(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, IList<ICrdable> crds, Action starting)
            : this(axisX, axisY, trcPrm, crds)
        {
            this.Starting = starting;
        }

        public CommandMoveTrc(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, ICrdable crd, Action starting)
            :this(axisX, axisY, trcPrm, crd)
        {
            this.Starting = starting;
        }



        [JsonProperty]
        public IList<ICrdable> Crds { get; private set; }

        [JsonProperty]
        public MoveTrcPrm TrcPrm { get; private set; }

        public MoveTrcSts TrcSts { get; private set; }

        public bool EnableINP { get; set; }

        public override void Call()
        {
            this.State = CommandState.Running;
            this.Axes[0].State.SetBusy(AxisMoveMode.MoveTrc);
            this.Axes[1].State.SetBusy(AxisMoveMode.MoveTrc);
            
            this.crdQueue.Clear();
            foreach (var item in this.Crds)
            {
                this.crdQueue.Enqueue(item);
            }

            //init crd
            this.card.Executor.InitCrd(
                this.card.CardId, 
                this.TrcPrm.CsId, 
                this.TrcPrm.VelMax, 
                this.TrcPrm.AccMax,
                this.TrcPrm.EvenTime, 
                this.Axes[0].AxisId, 
                this.Axes[1].AxisId, 
                this.Axes[0].ConvertPos2Card(TrcPrm.OrgX), 
                this.Axes[1].ConvertPos2Card(TrcPrm.OrgY));
            //clear buf
            this.card.Executor.ClrCrdBuf(this.card.CardId, this.TrcPrm.CsId);
            //add crds
            this.AddCrd2Space();

            //call Ex
            this.CallEx();

            this.Starting?.Invoke();

            //start
            this.card.Executor.StartCrd(this.card.CardId, this.TrcPrm.CsId);
        }

        public event Action Starting;

        public event Action Stoping;

        protected virtual void CallEx() { }

        protected virtual bool UpdateEx() => true;

        protected virtual bool GuardTime() => false;

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

            for (int i = 0; i < this.Axes.Length; i++)
            {
                if (this.Axes[i].IsAlarm.IsRising || this.Axes[i].IsAlarm.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMoveTrcAlarm);
                    return;
                }
                if (this.Axes[i].IsError.IsRising || this.Axes[i].IsError.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Error);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMoveTrcError);
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
            }

            //add crd to space
            this.AddCrd2Space();
            //run status
            short run;
            int segment;
            this.card.Executor.GetCrdStatus(this.card.CardId, this.TrcPrm.CsId, out run, out segment);
            this.TrcSts.Run = run;
            this.TrcSts.Segment = segment;

            if (this.TrcSts.Run == 1)
            {
                this.State = CommandState.Running;
                return;
            }

            if (this.EnableINP)
            {
                //判断到位信号
                bool flag = false;
                foreach (var item in this.Axes)
                {
                    if (item.IsArrived.Value)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    this.State = CommandState.Running;
                    return;
                }
            }

            //run == 0, crdQueue.count > 0, failed
            if (this.crdQueue.Count > 0)
            {
                this.State = CommandState.Failed;
                return;
            }

            this.UpdateEx();

            //done
            this.card.Executor.StopCrd(this.card.CardId, this.TrcPrm.CsId);
            this.Stoping?.Invoke();
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
                    }
                    break;
                case CmdMsgType.Resume:
                    if (this.State == CommandState.Pause)
                    {
                        this.State = CommandState.Running;
                    }
                    break;
                case CmdMsgType.Stop:
                    this.State = CommandState.Failed;
                    break;
            }
        }

        private short AddCrd2Space()
        {
            short rtn = 0;
            this.card.Executor.GetCrdSpace(this.card.CardId, this.TrcPrm.CsId, out space);
            int count = (space < MAX_ONCE) ? space : MAX_ONCE;
            for (int i = 0; i < count; i++)
            {
                if (this.crdQueue.Count > 0)
                {
                    ICrdable crd = this.crdQueue.Dequeue();
                    switch (crd.Type)
                    {
                        case CrdType.ArcXYC:
                            CrdArcXYC crdArcXYC = crd as CrdArcXYC;
                            rtn = this.AddCrdArcXYC(crdArcXYC);
                            break;
                        case CrdType.ArcXYR:
                            CrdArcXYR crdArcXYR = crd as CrdArcXYR;
                            rtn = this.AddCrdArcXYR(crdArcXYR);
                            break;
                        case CrdType.BufIO:
                            rtn = this.AddCrdBufIO(crd as CrdBufIO);
                            break;
                        case CrdType.Delay:
                            rtn = this.AddCrdDelay(crd as CrdDelay);
                            break;
                        case CrdType.LnXY:
                            CrdLnXY crdLnXY = crd as CrdLnXY;
                            rtn = this.AddCrdLnXY(crdLnXY);
                            break;
                        case CrdType.XYGear:
                            CrdXYGear crdXYGear = crd as CrdXYGear;
                            rtn = this.AddXYGear(crdXYGear);
                            break;
                        case CrdType.BufMove:
                            rtn = this.AddBufMove(crd as CrdBufMove);
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
            return rtn;
        }

        private short AddCrdLnXY(CrdLnXY crd)
        {
            return this.card.Executor.AddCrdLnXY(
                this.card.CardId,
                this.TrcPrm.CsId,
                this.Axes[0].ConvertPos2Card(crd.EndPosX),
                this.Axes[1].ConvertPos2Card(crd.EndPosY),
                this.Axes[0].ConvertVel2Card(crd.Vel),
                crd.Acc,
                this.Axes[0].ConvertVel2Card(crd.VelEnd));
        }

        private short AddCrdBufIO(CrdBufIO crd)
        {
            return this.card.Executor.AddCrdBufIO(this.card.CardId, this.TrcPrm.CsId, crd.Mask, crd.Value);
        }

        private short AddCrdDelay(CrdDelay crd)
        {
            return this.card.Executor.AddCrdDelay(this.card.CardId, this.TrcPrm.CsId, crd.DelayTime);
        }

        private short AddCrdArcXYC(CrdArcXYC crd)
        {
            short rtn = this.card.Executor.AddCrdArcXYC(
                this.card.CardId,
                this.TrcPrm.CsId,
                this.Axes[0].ConvertPos2Card(crd.EndPosX),
                this.Axes[1].ConvertPos2Card(crd.EndPosY),
                this.Axes[0].ConvertPos2Card(crd.CenterX),
                this.Axes[1].ConvertPos2Card(crd.CenterY),
                crd.Clockwise,
                this.Axes[0].ConvertVel2Card(crd.Vel),
                crd.Acc,
                this.Axes[0].ConvertVel2Card(crd.VelEnd));
            return rtn;
        }

        private short AddCrdArcXYR(CrdArcXYR crd)
        {
            return this.card.Executor.AddCrdArcXYR(
                this.card.CardId, 
                this.TrcPrm.CsId,
                this.Axes[0].ConvertPos2Card(crd.EndPosX),
                this.Axes[1].ConvertPos2Card(crd.EndPosY),
                this.Axes[0].ConvertPos2Card(crd.R), 
                crd.Clockwise,
                this.Axes[0].ConvertVel2Card(crd.Vel),
                crd.Acc,
                this.Axes[0].ConvertVel2Card(crd.VelEnd));
        }

        private short AddXYGear(CrdXYGear crd)
        {
            return this.card.Executor.AddCrdGear(
                this.card.CardId,
                this.TrcPrm.CsId,
                crd.GearAxis.AxisId,
                crd.DeltaPulse);
        }

        private short AddBufMove(CrdBufMove crd)
        {
            return this.card.Executor.AddBufMove(
                this.card.CardId,
                this.TrcPrm.CsId,
                crd.Axis.AxisId,
                crd.Axis.ConvertPos2Card(crd.Pos),
                crd.Axis.ConvertVel2Card(crd.Vel),
                crd.Acc,
                crd.Mode);
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
