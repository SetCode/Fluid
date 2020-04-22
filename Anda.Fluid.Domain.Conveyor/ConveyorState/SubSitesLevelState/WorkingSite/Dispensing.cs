using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class Dispensing : WorkingSiteState
    {
        public Dispensing(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.点胶中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.点胶中;

            System.Threading.Interlocked.Increment(ref FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts);
        }

        public override void ExitState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.DispenseDone = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.DispenseDone)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.点胶完成);
            }
        }
    }
}
