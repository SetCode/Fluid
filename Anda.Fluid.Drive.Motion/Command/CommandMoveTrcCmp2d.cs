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

namespace Anda.Fluid.Drive.Motion.Command
{
    public class Cmp2dSts
    {
        public short Status { get; set; }
        public int Count { get; set; }
        public int Space { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveTrcCmp2d : CommandMoveTrc
    {
        private Queue<PointD> cmp2dPtQueue;

        public CommandMoveTrcCmp2d(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, IList<ICrdable> crds, short chn, IList<PointD> cmp2dPoints)
           : base(axisX, axisY, trcPrm, crds)
        {
            if (axisX.Card != axisY.Card)
            {
                throw new Exception("axis x and axis y must be on the same card!");
            }

            this.Chn = chn;
            this.Cmp2dPoints = cmp2dPoints;
            this.Cmp2dSts = new Cmp2dSts();
            this.cmp2dPtQueue = new Queue<PointD>();
        }

        public CommandMoveTrcCmp2d(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, ICrdable crd, short chn, IList<PointD> cmp2dPoints)
          : this(axisX, axisY, trcPrm, new List<ICrdable>() { crd }, chn, cmp2dPoints)
        {

        }

        public short Chn { get; private set; }

        [JsonProperty]
        public IList<PointD> Cmp2dPoints { get; private set; }

        public Cmp2dSts Cmp2dSts { get; private set; }

        protected override void CallEx()
        {
            //this.cmp2dPtQueue.Clear();
            //foreach (var item in this.Cmp2dPoints)
            //{
            //    this.cmp2dPtQueue.Enqueue(item);
            //}

            //add 2d compare data
            //this.AddCmp2dData();
        }

        protected override bool UpdateEx()
        {
            //add crd to space
            //this.AddCmp2dData();

            //cmp run status
            //short status, fifo, space, bufcount;
            //int cmpcount;
            //this.card.Executor.Cmp2dStatus(this.card.CardId, this.Chn,
            //    out status, out cmpcount, out fifo, out space, out bufcount);
            //this.Cmp2dSts.Status = status;
            //this.Cmp2dSts.Count = cmpcount;
            //this.Cmp2dSts.Space = space;
            //if(this.Cmp2dSts.Count == this.Cmp2dPoints.Count)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }

        private void AddCmp2dData()
        {
            short status, fifo, space, bufcount;
            int cmpcount;
            this.card.Executor.Cmp2dStatus(this.card.CardId, this.Chn,
                out status, out cmpcount, out fifo, out space, out bufcount);

            int count = (space < MAX_ONCE) ? space : MAX_ONCE;
            List<int> lstx = new List<int>();
            List<int> lsty = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if(this.cmp2dPtQueue.Count > 0)
                {
                    PointD p = this.cmp2dPtQueue.Dequeue();
                    lstx.Add(this.Axes[0].ConvertPos2Card(p.X));
                    lsty.Add(this.Axes[1].ConvertPos2Card(p.Y));
                }
                else
                {
                    break;
                }
            }
            if(lstx.Count > 0)
            {
                this.card.Executor.Cmp2dData(this.card.CardId, this.Chn, (short)lstx.Count, lstx.ToArray(), lsty.ToArray(), 0);
            }
        }
    }
}
