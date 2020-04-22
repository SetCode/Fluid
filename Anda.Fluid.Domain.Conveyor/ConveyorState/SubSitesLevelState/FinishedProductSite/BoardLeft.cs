using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class BoardLeft : FinishedSiteState
    {
        private bool taskIsDone;
        private bool dispenseIsDone;
        public BoardLeft(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.出板完成.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.出板完成;

            if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
            {
                ConveyorController.Instance.InStoreSignalling(this.finishedSiteStateMachine.ConveyorNo,false);
            }

            Task.Factory.StartNew(new Action(() =>
            {               
                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.finishedSiteStateMachine.ConveyorNo).BoardLeftDelay))
                {
                    Thread.Sleep(1);
                }

                if ( this.TotalTaskIsDone())
                {
                    this.dispenseIsDone = true;
                }

                this.taskIsDone = true;
               
            }));
            
        }

        public override void ExitState()
        {          
            this.taskIsDone = false ;
            this.dispenseIsDone = false;
        }

        public override void UpdateState()
        {
            if ( this.dispenseIsDone
                || FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate) 
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
            else if(this.taskIsDone)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.起始状态);
            }
        }
        private bool TotalTaskIsDone()
        {
            if (FlagBitMgr.Instance.BoardCounts == 0)
            {
                return false;
            }
            else if ((FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.ExitBoardCounts
                    == FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts)
                    && (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState == PreHeatSite.PreSiteStateEnum.作业结束))
            {
                return true;
            }
            else
                return false;
        }
    }
}
