using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.Utils;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreAsk : PreSiteState
    {
        private DateTime startTime;
        public PreAsk(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.求板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.求板;

            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.usingConveyor = true;

            ConveyorController.Instance.ConveyorForward(this.preSiteStateMachine.ConveyorNo);
            ConveyorController.Instance.SetPreSiteStopper(this.preSiteStateMachine.ConveyorNo,true);

            ConveyorTimerMgr.Instance.PreAskTimer.ResetAndStart(ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UpStramStuckTime);
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
                if(!ConveyorTimerMgr.Instance.PreAskTimer.IsNormal)
                {
                    this.preSiteStateMachine.ChangeState(PreSiteStateEnum.卡板);
                }
                else if(ConveyorController.Instance.PreSiteEnterSensor(this.preSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    ConveyorController.Instance.AskSignalling(this.preSiteStateMachine.ConveyorNo,false);
                    this.preSiteStateMachine.ChangeState(PreSiteStateEnum.板进入);
                }
            }
            else
            {
                if(ConveyorController.Instance.PreSiteEnterSensor(this.preSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.preSiteStateMachine.ChangeState(PreSiteStateEnum.板进入);
                }
            }
        }
    }
}
