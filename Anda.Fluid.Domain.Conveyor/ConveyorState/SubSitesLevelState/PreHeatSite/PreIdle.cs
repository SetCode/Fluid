using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreIdle : PreSiteState
    {
        public PreIdle(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.空闲.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.空闲;
            ConveyorController.Instance.AskSignalling(this.preSiteStateMachine.ConveyorNo,true);
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else
            {
                if (!ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                      && ConveyorController.Instance.UpstreamPutBoard(this.preSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.ChangeState();
                }
                else if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                    && FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.UpStreamHaveBoard)
                {
                    FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.UpStreamHaveBoard = false;
                    this.ChangeState();
                }
            }
        }

        private void ChangeState()
        {
            if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.DispenseAndInsulation
                  && FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSite.WorkingSiteStateEnum.求板)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.求板);
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode != ConveyorSubsiteMode.DispenseAndInsulation)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.求板);
            }
        }
    }
}
