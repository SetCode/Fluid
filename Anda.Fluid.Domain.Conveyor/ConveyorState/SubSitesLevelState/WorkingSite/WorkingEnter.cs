using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class WorkingEnter : WorkingSiteState
    {
        public WorkingEnter(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.起始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.起始状态;
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            this.workingStateMachine.ChangeState(WorkingSiteStateEnum.求板);
        }
    }
}
