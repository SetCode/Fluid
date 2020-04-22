using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class WorkingAsk : WorkingSiteState
    {
        public WorkingAsk(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.求板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.求板;
            ConveyorController.Instance.SetWorkingSiteStopper(this.workingStateMachine.ConveyorNo,true);
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState == PreSiteStateEnum.出板完成)
            {
                // 工作站更新当前条码
                FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.CurrentBarcode = FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.PreBarcode;
                // 清空预热站条码
                FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.PreBarcode = "";
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.板到位);
            }
        }
    }
}
