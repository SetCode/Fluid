using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using gts;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class BufFluidItem
    {
        public BufFluidItem(ICrdable crd, List<PointD> points)
        {
            this.Crd = crd;
            this.Points = points;
        }

        public BufFluidItem(ICrdable crd) : this(crd, null)
        {

        }

        public ICrdable Crd { get; }

        public List<PointD> Points { get; }
    }

    public class Cmp2dPrm
    {
        public short Chn { get; set; }
        public short Src { get; set; }
        public short PulseWidth { get; set; }
        public short Maxerr { get; set; }
        public short Threshold { get; set; }
        public PointD Start { get; set; } = new PointD();
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveTrcBufFluid : CommandMotion, ICloneable
    {
        public const int MAX_CRD_ONCE = 100;
        public const int MAX_CMP2D = 512;
        private bool lookAheadEnd = false;
        private Card card = null;
        private Queue<BufFluidItem> bufFluidItemQueue;
        private Queue<PointD> bufCmp2dData;
        private int crdSpace = 0;
        private mc.TCrdData[] trdData;

        public CommandMoveTrcBufFluid(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, Cmp2dPrm cmp2dPrm, InitLook lookAheadPrm, IList<BufFluidItem> bufFluidItems)
           : base(axisX, axisY)
        {
            if (axisX.Card != axisY.Card)
            {
                throw new Exception("axis x and axis y must be on the same card!");
            }
            this.card = axisX.Card;
            this.TrcPrm = trcPrm;
            this.Cmp2dPrm = cmp2dPrm;
            this.LookAheadPrm = lookAheadPrm;
            this.TrcSts = new MoveTrcSts();
            this.bufFluidItemQueue = new Queue<BufFluidItem>();
            this.bufCmp2dData = new Queue<PointD>();
            this.initQueue(bufFluidItems);
        }

        public MoveTrcPrm TrcPrm { get; private set; }

        public Cmp2dPrm Cmp2dPrm { get; private set; }

        public InitLook LookAheadPrm { get; private set; }

        public MoveTrcSts TrcSts { get; private set; }

        public bool EnableINP { get; set; }

        public event Action Starting;

        public event Action Stoping;

        private void initQueue(IList<BufFluidItem> bufFluidItems)
        {
            this.bufFluidItemQueue.Clear();
            this.bufCmp2dData.Clear();
            foreach (var item in bufFluidItems)
            {
                this.bufFluidItemQueue.Enqueue(item);
                if (item.Points != null)
                {
                    foreach (var point in item.Points)
                    {
                        this.bufCmp2dData.Enqueue(point);
                    }
                }
            }
        }

        public override void Call()
        {
            this.State = CommandState.Running;
            this.Axes[0].State.SetBusy(AxisMoveMode.MoveTrc);
            this.Axes[1].State.SetBusy(AxisMoveMode.MoveTrc);
            //init cmp2d
            short rtn = this.initCmp2d();
            //add cmp2d data
            rtn = this.addCmp2dData(0, MAX_CMP2D);
            //init crd
            rtn = this.initCrd();
            //add crds
            rtn = this.addCrdData();
            //start
            this.Starting?.Invoke();
            //start cmp2d
            rtn = this.card.Executor.Cmp2dStart(card.CardId, this.Cmp2dPrm.Chn);
            //start crd
            rtn = this.card.Executor.StartCrd(this.card.CardId, this.TrcPrm.CsId);
        }

        private short initCmp2d()
        {
            try
            {
                //clear 2d compare data
                short rtn = card.Executor.Cmp2dClear(card.CardId, this.Cmp2dPrm.Chn);
                if (rtn != 0) return rtn;
                //set 2d compare mode
                rtn = card.Executor.Cmp2dMode(card.CardId, this.Cmp2dPrm.Chn);
                if (rtn != 0) return rtn;
                //set 2d compare params
                rtn = card.Executor.Cmp2dSetPrm(card.CardId, this.Cmp2dPrm.Chn, this.Axes[0].AxisId, this.Axes[1].AxisId,
                    this.Cmp2dPrm.Src, 0, 0, this.Cmp2dPrm.PulseWidth, this.Cmp2dPrm.Maxerr, this.Cmp2dPrm.Threshold);
                return rtn;
            }
            catch (Exception)
            {
                return -10;
            }
        }

        private short initCrd()
        {
            //初始化插补参数
            short rtn = this.card.Executor.InitCrd(
               this.card.CardId,
               this.TrcPrm.CsId,
               this.TrcPrm.VelMax,
               this.TrcPrm.AccMax,
               this.TrcPrm.EvenTime,
               this.Axes[0].AxisId,
               this.Axes[1].AxisId,
               this.Axes[0].ConvertPos2Card(TrcPrm.OrgX),
               this.Axes[1].ConvertPos2Card(TrcPrm.OrgY));
            if (rtn != 0) return rtn;
            //清空缓存区
            rtn = this.card.Executor.ClrCrdBuf(this.card.CardId, this.TrcPrm.CsId);
            if (rtn != 0) return rtn;
            //初始化前瞻缓存区
            this.trdData = new mc.TCrdData[this.LookAheadPrm.n];
            //rtn = this.card.Executor.InitLookAhead(
            //    this.card.CardId,
            //    this.LookAheadPrm.crd,
            //    this.LookAheadPrm.fifo,
            //    this.LookAheadPrm.Ltime,
            //    this.LookAheadPrm.Lmax,
            //    this.LookAheadPrm.n,
            //    this.trdData);
            return rtn;
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

            //add cmp2d data & crd data
            this.addCmp2dDataAtRunning();
            this.addCrdData();
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
            if (this.bufFluidItemQueue.Count > 0)
            {
                this.State = CommandState.Failed;
                return;
            }

            //done
            this.card.Executor.StopCrd(this.card.CardId, this.TrcPrm.CsId);
            this.card.Executor.Cmp2dStop(card.CardId, this.Cmp2dPrm.Chn);
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
                        this.card.Executor.StopCrd(this.card.CardId, this.TrcPrm.CsId);
                        this.State = CommandState.Pause;
                    }
                    break;
                case CmdMsgType.Resume:
                    if (this.State == CommandState.Pause)
                    {
                        this.card.Executor.StartCrd(this.card.CardId, this.TrcPrm.CsId);
                        this.State = CommandState.Running;
                    }
                    break;
                case CmdMsgType.Stop:
                    this.card.Executor.StopCrd(this.card.CardId, this.TrcPrm.CsId);
                    this.card.Executor.ClrCrdBuf(this.card.CardId, this.TrcPrm.CsId);
                    this.card.Executor.Cmp2dStop(card.CardId, this.Cmp2dPrm.Chn);
                    this.State = CommandState.Failed;
                    break;
            }
        }

        private short addCmp2dDataAtRunning()
        {
            //运行时动态压入2d比较数据
            short status;
            int count;
            short fifo;
            short fifoCount;
            short bufCount;
            short rtn = this.card.Executor.Cmp2dStatus(this.card.CardId, this.Cmp2dPrm.Chn, out status, out count, out fifo, out fifoCount, out bufCount);
            if (rtn != 0) return rtn;
            //判断空闲fifo空间
            if (fifoCount > 0 && this.bufCmp2dData.Count > 0)
            {
                Logger.DEFAULT.Debug(string.Format("addCmp2dData fifo:{0} count{1}", fifo, fifoCount));
                rtn = this.addCmp2dData(fifo, MAX_CMP2D);
            }
            return rtn;
        }

        private short addCmp2dData(short fifo, int fifoCount)
        {
            List<PointD> points = new List<PointD>();
            for (int i = 0; i < fifoCount; i++)
            {
                if (this.bufCmp2dData.Count > 0)
                {
                    points.Add(this.bufCmp2dData.Dequeue());
                }
                else
                {
                    break;
                }
            }
            if(points.Count == 0)
            {
                return 1;
            }
            return this.cmp2dData(points, fifo);
        }

        private short cmp2dData(List<PointD> points, short fifo)
        {
            try
            {
                //转换为相对启动点的相对坐标
                foreach (var item in points)
                {
                    item.X -= this.Cmp2dPrm.Start.X;
                    item.Y -= this.Cmp2dPrm.Start.Y;
                }
                int[] pulsx = new int[points.Count];
                int[] pulsy = new int[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    pulsx[i] = this.Axes[0].ConvertPos2Card(points[i].X);
                    pulsy[i] = this.Axes[1].ConvertPos2Card(points[i].Y);
                }
                return this.card.Executor.Cmp2dData(this.card.CardId, this.Cmp2dPrm.Chn, (short)points.Count, pulsx, pulsy, fifo);
            }
            catch (Exception)
            {
                return -10;
            }
        }

        private short addCrdData()
        {
            //获取插补缓冲区剩余空间
            short rtn = this.card.Executor.GetCrdSpace(this.card.CardId, this.TrcPrm.CsId, out crdSpace);
            if (this.bufFluidItemQueue.Count == 0)
            {
                //if (crdSpace > this.LookAheadPrm.n && !this.lookAheadEnd)
                //{
                //    //没有插补数据了一次性将前瞻缓存区数据塞到插补缓存区
                //    rtn = this.card.Executor.GT_CrdData(this.card.CardId, this.LookAheadPrm.crd, System.IntPtr.Zero, this.LookAheadPrm.fifo);
                //    this.lookAheadEnd = true;
                //}
            }
            else
            {
                //限制一次性压入的插补数量
                int count = (crdSpace < MAX_CRD_ONCE) ? crdSpace : MAX_CRD_ONCE;
                for (int i = 0; i < count; i++)
                {
                    if (this.bufFluidItemQueue.Count > 0)
                    {
                        BufFluidItem bufFluidItem = this.bufFluidItemQueue.Dequeue();
                        rtn = this.addCrd(bufFluidItem.Crd);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return rtn;
        }

        private short addCrd(ICrdable crd)
        {
            short rtn = 0;
            switch (crd.Type)
            {
                case CrdType.ArcXYC:
                    CrdArcXYC crdArcXYC = crd as CrdArcXYC;
                    rtn = this.addCrdArcXYC(crdArcXYC);
                    break;
                case CrdType.ArcXYR:
                    CrdArcXYR crdArcXYR = crd as CrdArcXYR;
                    rtn = this.addCrdArcXYR(crdArcXYR);
                    break;
                case CrdType.BufIO:
                    rtn = this.addCrdBufIO(crd as CrdBufIO);
                    break;
                case CrdType.Delay:
                    rtn = this.addCrdDelay(crd as CrdDelay);
                    break;
                case CrdType.LnXY:
                    CrdLnXY crdLnXY = crd as CrdLnXY;
                    rtn = this.addCrdLnXY(crdLnXY);
                    break;
                case CrdType.XYGear:
                    CrdXYGear crdXYGear = crd as CrdXYGear;
                    rtn = this.addXYGear(crdXYGear);
                    break;
                case CrdType.BufMove:
                    rtn = this.addBufMove(crd as CrdBufMove);
                    break;
            }
            return rtn;
        }

        private short addCrdLnXY(CrdLnXY crd)
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

        private short addCrdBufIO(CrdBufIO crd)
        {
            return this.card.Executor.AddCrdBufIO(this.card.CardId, this.TrcPrm.CsId, crd.Mask, crd.Value);
        }

        private short addCrdDelay(CrdDelay crd)
        {
            return this.card.Executor.AddCrdDelay(this.card.CardId, this.TrcPrm.CsId, crd.DelayTime);
        }

        private short addCrdArcXYC(CrdArcXYC crd)
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

        private short addCrdArcXYR(CrdArcXYR crd)
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

        private short addXYGear(CrdXYGear crd)
        {
            return this.card.Executor.AddCrdGear(
                this.card.CardId,
                this.TrcPrm.CsId,
                crd.GearAxis.AxisId,
                crd.DeltaPulse);
        }

        private short addBufMove(CrdBufMove crd)
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
