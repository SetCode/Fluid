using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class BoardLeft : PreSiteState
    {
        private bool DiespensTaskIsDone;
        public BoardLeft(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.出板完成.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.出板完成;

            if (FlagBitMgr.Instance.BoardCounts == 0)
            {
                return;
            }
            else if (FlagBitMgr.Instance.BoardCounts == FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts)
            {
                this.DiespensTaskIsDone = true;
            }
        }

        public override void ExitState()
        {
            this.DiespensTaskIsDone = false;
        }

        public override void UpdateState()
        {
            if (this.DiespensTaskIsDone
                || FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.板到位)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.起始状态);
            }
        }
    }
}
