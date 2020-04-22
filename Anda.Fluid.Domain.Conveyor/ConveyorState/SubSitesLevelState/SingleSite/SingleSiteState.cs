using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public abstract class SingleSiteState : StateBase
    {
        protected SingleSiteStateMachine singleSiteStateMachine;
        public SingleSiteState(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
            this.singleSiteStateMachine = stateMachine;
        }
    }
    public enum SingleSiteStateEnum
    {
        起始状态,
        空闲,
        求板,
        进板中,
        板到位,
        准备点胶,
        点胶中,
        点胶完成,
        出板中,
        反转出板,
        出板完成,
        卡板,
        作业结束,
        气缸卡住
    }
}
