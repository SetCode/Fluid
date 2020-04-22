using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public abstract class PreSiteState : StateBase
    {
        protected PreSiteStateMachine preSiteStateMachine;
        public PreSiteState(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
            this.preSiteStateMachine = preSiteStateMachine;
        }

    }
    public enum PreSiteStateEnum
    {
        起始状态,
        空闲,
        求板,
        板进入,
        板到达,
        加热中,
        出板中,
        出板完成,
        卡板,
        作业结束
    }
}
