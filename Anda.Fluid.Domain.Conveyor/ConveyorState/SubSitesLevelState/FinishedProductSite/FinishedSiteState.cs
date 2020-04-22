using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public abstract class FinishedSiteState : StateBase
    {
        protected FinishedSiteStateMachine finishedSiteStateMachine;
        public FinishedSiteState(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
            this.finishedSiteStateMachine = finishedSiteStateMachine;
        }
    }
    public enum FinishedSiteStateEnum
    {
        起始状态,
        求板,
        进板中,
        直接出板,
        加热中,
        出板中,
        出板完成,
        卡板,
        作业结束
    }
}
