using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedEnter : FinishedSiteState
    {
        public FinishedEnter(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.起始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.起始状态;
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
            else if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
                {
                    if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                           && ConveyorController.Instance.DownstreamAskBoard(this.finishedSiteStateMachine.ConveyorNo).Is(Infrastructure.StsType.High))
                    {
                        this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.求板);
                    }
                    else if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                        && FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard)
                    {
                        FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard = false;
                        this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.求板);
                    }

                }
                else if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.求板);
                }
            }
            else
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.求板);
            }           
        }
    }
}
