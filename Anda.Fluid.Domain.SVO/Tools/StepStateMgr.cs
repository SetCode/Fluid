using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.SVO
{
    internal sealed class StepStateMgr
    {
        private static readonly StepStateMgr instance = new StepStateMgr();
        private StepStateMgr() { }
        public static StepStateMgr Instance => instance;


        private List<StepState> list = new List<StepState>();
        public StepStateMgr Add(StepState stepState)
        {
            this.Remove(stepState.Step);
            this.list.Add(stepState);
            return this;
        }

        public StepStateMgr Remove(int step)
        {
            this.list.RemoveAll(x => x.Step == step);
            return this;
        }

        public StepState FindBy(int step)
        {
            foreach (var item in this.list)
            {
                if(item.Step == step)
                {
                    return item;
                }
            }
            return null;
        }
        public int DoneCount()
        {
            int doneCount=0;
            for (int i = 0; i < this.list.Count; i++)
            {
                if (this.list[i].IsDone)
                {
                    doneCount++;
                }
            }
            return doneCount;
        }

    }
}
