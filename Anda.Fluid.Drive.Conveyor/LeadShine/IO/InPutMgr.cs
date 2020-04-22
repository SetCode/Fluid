using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.IO
{
    internal class InPutMgr
    {
        private List<InPut> inPutList;
        private static InPutMgr instance = new InPutMgr();

        private InPutMgr()
        {
            this.inPutList = new List<InPut>();

            for (int i = 1; i < 33; i++)
            {
                DiEnum diName = (DiEnum)i;
                this.Add(new InPut(diName));
            }

            this.AxisYInput = new AxisYLimitInput();
        }

        public static InPutMgr Instance => instance;

        public AxisYLimitInput AxisYInput { get; private set; }

        public int Counts => this.inPutList.Count;

        private void Add(InPut inPut)
        {
            this.inPutList.Add(inPut);
        }

        public InPut FindBy(DiEnum diName)
        {
            return this.inPutList[(int)diName-1];
        }

        public void UpdateSts()
        {
            foreach (var item in this.inPutList)
            {
                item.UpdateSts();
            }

            this.AxisYInput.Upstate();
        }
    }
}
