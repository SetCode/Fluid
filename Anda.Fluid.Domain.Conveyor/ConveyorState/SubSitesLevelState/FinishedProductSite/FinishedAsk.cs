using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedAsk : FinishedSiteState
    {
        public FinishedAsk(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.求板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.求板;
            if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
                return;
            ConveyorController.Instance.SetFinishedSiteStopper(this.finishedSiteStateMachine.ConveyorNo,true);
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.点胶完成)
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.直接出板);
                }
                else
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.进板中);
                }
            }
            else if (this.NeedContinueWork())
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.直接出板);
                }
                else
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.进板中);
                }
            }
        }
        private bool NeedContinueWork()
        {
            if ((FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.作业结束)
                && (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts
                == FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts))
            {
                return true;
            }
            else
                return false;
        }
    }
}
