using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class SingleIdle : SingleSiteState
    {
        public SingleIdle(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.空闲.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.空闲;
            ConveyorController.Instance.AskSignalling(this.singleSiteStateMachine.ConveyorNo,true);
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else  
            {
                if (!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                    && ConveyorController.Instance.UpstreamPutBoard(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High)) 
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.求板);
                }
                else if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                    && FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.UpStreamHaveBoard)
                {
                    FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.UpStreamHaveBoard = false;
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.求板);
                }    
                else if(ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).SMEMAIsSingleInteraction
                    && ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.求板);
                }         
            }
        }
    }
}
