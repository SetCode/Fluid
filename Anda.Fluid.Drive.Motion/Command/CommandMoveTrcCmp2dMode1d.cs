using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    /// <summary>
    /// 2d比较一维模式插补指令(实际测试结果不好---废弃)
    /// </summary>
    public class CommandMoveTrcCmp2dMode1d :CommandMoveTrc
    {
        private Queue<PointD> cmp2dPtQueue;
        private short curIdleFIFO;
        public CommandMoveTrcCmp2dMode1d(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, IList<ICrdable> crds, short chn, IList<PointD> cmp2dPoints)
           : base(axisX, axisY, trcPrm, crds)
        {
            this.Chn = chn;
            this.Cmp2dPoints = cmp2dPoints;
            this.Cmp2dSts = new Cmp2dSts();
            this.cmp2dPtQueue = new Queue<PointD>();
        }

        public short Chn { get; private set; }

        public IList<PointD> Cmp2dPoints { get; private set; }

        public Cmp2dSts Cmp2dSts { get; private set; }

        protected override void CallEx()
        {
            this.cmp2dPtQueue.Clear();
            foreach (var item in this.Cmp2dPoints)
            {
                this.cmp2dPtQueue.Enqueue(item);
            }
            //add 2d compare data
            this.AddCmp2dData();
        }

        protected override bool UpdateEx()
        {
            //add crd to space
            this.AddCmp2dData();

            //cmp run status
            short status, fifo, space, bufcount;
            int cmpcount;
            this.card.Executor.Cmp2dStatus(this.card.CardId, this.Chn,
                out status, out cmpcount, out fifo, out space, out bufcount);
            this.Cmp2dSts.Status = status;
            this.Cmp2dSts.Count = cmpcount;
            this.Cmp2dSts.Space = space;
            //if (this.Cmp2dSts.Count == this.Cmp2dPoints.Count)
            //{
            //    return true;
            //}
            return false;
        }

        private void AddCmp2dData()
        {
            short status, fifo, space, bufcount;
            int cmpcount;
            this.card.Executor.Cmp2dStatus(this.card.CardId, this.Chn,
                out status, out cmpcount, out fifo, out space, out bufcount);
            Log.Dprint(string.Format("module trigger {0}", cmpcount));
            curIdleFIFO = fifo;

            int count = (space < 400/*MAX_ONCE*/) ? space : 400/*MAX_ONCE*/;
            List<int> lstx = new List<int>();
            List<int> lsty = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if (this.cmp2dPtQueue.Count > 0)
                {
                    PointD p = this.cmp2dPtQueue.Dequeue();
                    lstx.Add(0);
                    lsty.Add(this.Axes[1].ConvertPos2Card(p.Y));
                }
                else
                {
                    break;
                }
            }
            Log.Dprint(string.Format("fifo {0} add {1} points,module trigger {2}", fifo, count, cmpcount));
            if (lstx.Count > 0)
            {
                this.card.Executor.Cmp2dData(this.card.CardId, this.Chn, (short)lstx.Count, lstx.ToArray(), lsty.ToArray(), fifo);
            }
        }
    }
}
