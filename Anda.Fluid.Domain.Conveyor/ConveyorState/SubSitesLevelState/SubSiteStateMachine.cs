using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState
{
    public class SubSiteStateMachine:StateMachineBase
    {
        private int conveyorNo;
        public SubSiteStateMachine(int conveyorNo):base()
        {
            this.conveyorNo = conveyorNo;
        }
        public int ConveyorNo => this.conveyorNo;
        public virtual void Setup()
        {

        }
    }
}
