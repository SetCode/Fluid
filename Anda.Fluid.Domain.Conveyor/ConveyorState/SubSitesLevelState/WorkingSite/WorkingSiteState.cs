using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public abstract class WorkingSiteState : StateBase
    {
        protected WorkingSiteStateMachine workingStateMachine;
        public WorkingSiteState(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
            this.workingStateMachine = workingStateMachine;
        }
    }
    public enum WorkingSiteStateEnum
    {
        起始状态,
        求板,
        板到位,
        加热中,
        准备点胶,
        点胶中,
        点胶完成,
        作业结束
    }
}
