using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedExit : FinishedSiteState
    {
        public FinishedExit(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.作业结束.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.作业结束;
            ConveyorController.Instance.InStoreSignalling(this.finishedSiteStateMachine.ConveyorNo,false);
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            
        }
    }
}
