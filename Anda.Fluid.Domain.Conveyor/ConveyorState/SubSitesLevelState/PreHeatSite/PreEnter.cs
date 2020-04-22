using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Forms;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreEnter : PreSiteState
    {
        public static event Action Conveyor1PutBtnIsVisible, Conveyor2PutBtnIsVisible;
        public PreEnter(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.起始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.起始状态;
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
            else if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).EnterIsSMEMA)
            {
                this.ChangeState(PreSiteStateEnum.空闲);
            }
            else 
            {
                if (ConveyorController.Instance.PreSiteEnterSensor(this.preSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.ChangeState(PreSiteStateEnum.求板);
                }
            }

        }
        private void ChangeState(PreSiteStateEnum state)
        {
            if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.DispenseAndInsulation
                  && FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSite.WorkingSiteStateEnum.求板)
            {
                this.preSiteStateMachine.ChangeState(state);
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode != ConveyorSubsiteMode.DispenseAndInsulation)
            {
                this.preSiteStateMachine.ChangeState(state);
            }
        }
    }
}
