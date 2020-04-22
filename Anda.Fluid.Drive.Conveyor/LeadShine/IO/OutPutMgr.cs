using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.IO
{
    internal class OutPutMgr
    {
        private List<OutPut> outPutList;
        private static OutPutMgr instance = new OutPutMgr();

        private OutPutMgr()
        {
            this.outPutList = new List<OutPut>();
            for (int i = 1; i < 23; i++)
            {
                DoEnum doName = (DoEnum)i;
                this.Add(new OutPut(doName));
            }
        }
        public static OutPutMgr Instance => instance;

        public int Counts => this.outPutList.Count;

        internal List<OutPut> OutPutList => this.outPutList;

        private void Add(OutPut outPut)
        {
            this.outPutList.Add(outPut);
        }

        public OutPut FindBy(DoEnum doName)
        {
            return this.outPutList[(int)doName-1];
        }

        public void UpdateSts()
        {
            foreach (var item in this.outPutList)
            {
                item.UpdateSts();
            }
        }
    }
}
