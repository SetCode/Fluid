using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class Dispensing : SingleSiteState
    {
        public Dispensing(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.点胶中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.点胶中;
        }

        public override void ExitState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.DispenseDone = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.DispenseDone)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.点胶完成);
            }
        }
    }
}
