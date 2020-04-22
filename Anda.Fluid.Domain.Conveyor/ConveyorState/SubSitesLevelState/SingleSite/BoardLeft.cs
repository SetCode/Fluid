using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class BoardLeft : SingleSiteState
    {
        private bool taskIsDone;
        private bool dispenseIsDone;
        public BoardLeft(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.出板完成.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.出板完成;

            if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ExitIsSMEMA
                || FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough) 
            {
                ConveyorController.Instance.InStoreSignalling(this.singleSiteStateMachine.ConveyorNo,false);
            }

            Task.Factory.StartNew(new Action(() =>
            {
                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.singleSiteStateMachine.ConveyorNo).BoardLeftDelay))
                {
                    Thread.Sleep(1);
                }

                if (this.TotalTaskIsDone())
                {
                    this.dispenseIsDone = true;
                }

                this.taskIsDone = true;

            }));

        }

        public override void ExitState()
        {
            this.taskIsDone = false;
            this.dispenseIsDone = false;
        }

        public override void UpdateState()
        {
            if (this.dispenseIsDone
                || FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (this.taskIsDone)
            {
                //手动模式下等待板被取走再切换状态
                if(!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ExitIsSMEMA
                        &&!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).AutoExitBoard)
                {
                    if(ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.Low))
                    {
                        this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.起始状态);
                    }
                }
                else
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.起始状态);
                }
            }
        }
        private bool TotalTaskIsDone()
        {
            if (FlagBitMgr.Instance.BoardCounts == 0)
            {
                return false;
            }
            else if ((FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).SubSitesLevel.SingleSite.EnterBoardCounts
                    == FlagBitMgr.Instance.BoardCounts)) 
            {
                return true;
            }
            else
                return false;
        }
    }
}
