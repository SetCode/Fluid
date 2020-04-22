using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedHeating : FinishedSiteState
    {
        private bool taskIsDone;
        private bool isBreak;
        public FinishedHeating(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.加热中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.加热中;

            //复位标志位
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard = false;

            if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
            {
                ConveyorController.Instance.InStoreSignalling(this.finishedSiteStateMachine.ConveyorNo,true);
            }

            if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).FinishedSitePrm.HeatingTime == 0)
            {
                this.taskIsDone = true;
            }
            else
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    DateTime startTime = DateTime.Now;
                   
                    if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
                    {
                        ConveyorController.Instance.SetFinishedSiteLift(this.finishedSiteStateMachine.ConveyorNo,false);
                    }
                    else
                    {
                        ConveyorController.Instance.SetFinishedSiteLift(this.finishedSiteStateMachine.ConveyorNo,true);
                    }

                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                        (this.finishedSiteStateMachine.ConveyorNo).FinishedSitePrm.HeatingTime)+
                        TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).LiftUpDelay))
                    {
                        if (this.isBreak)
                            return;
                    }
                    this.taskIsDone = true;
                }));
            }
        }

        public override void ExitState()
        {
            this.taskIsDone = false;
            this.isBreak = false;

        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
            else if (this.taskIsDone && ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA) 
            {
                if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                       && ConveyorController.Instance.DownstreamAskBoard(this.finishedSiteStateMachine.ConveyorNo).Is(Infrastructure.StsType.High))
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.出板中);
                }
                else if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                    && FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard)
                {
                    FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard = false;
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.出板中);
                }

            }
            else if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.出板中);
            }
        }
    }
}
