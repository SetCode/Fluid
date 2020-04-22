using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class BoardLeaving : FinishedSiteState
    {
        private bool isBreak;
        private bool taskIsDone;
        private bool isStuck;
        public BoardLeaving(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.出板中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.出板中;

            Task.Factory.StartNew(new Action(() =>
            {
                if(ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA
                || (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA
                    && ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).AutoExitBoard))
                {
                    ConveyorController.Instance.SetFinishedSiteStopper(this.finishedSiteStateMachine.ConveyorNo,false);
                }                

                DateTime stopperDelay = DateTime.Now;
                while (DateTime.Now - stopperDelay < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.finishedSiteStateMachine.ConveyorNo).StopperUpDelay))
                {
                    if (this.isBreak)
                        return;
                }

                ConveyorController.Instance.ConveyorForward(this.finishedSiteStateMachine.ConveyorNo);
                FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.usingConveyor = true;
                DateTime stuckTime = DateTime.Now;

                //SMEMA模式出板
                if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA)
                {
                    while (DateTime.Now - stuckTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.finishedSiteStateMachine.ConveyorNo).DownStreamStuckTime))
                    {
                        if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                       && ConveyorController.Instance.DownstreamAskBoard(this.finishedSiteStateMachine.ConveyorNo).Is(StsType.Low))
                        {
                            this.taskIsDone = true;
                            return;
                        }
                        else if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                                  && ConveyorController.Instance.FinishedSiteExitSensor(this.finishedSiteStateMachine.ConveyorNo).Is(StsType.IsFalling))
                        {
                            this.taskIsDone = true;
                            return;
                        }
                    }
                   
                }
                else
                {
                    //自动出板
                    if (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).AutoExitBoard)
                    {
                        while (DateTime.Now - stuckTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                                  (this.finishedSiteStateMachine.ConveyorNo).FinishedSitePrm.StuckTime))
                        {
                            if (ConveyorController.Instance.FinishedSiteExitSensor(this.finishedSiteStateMachine.ConveyorNo).Is(StsType.IsFalling))
                            {
                                this.taskIsDone = true;
                                return;
                            }
                        }
                    }
                    //手动出板
                    else
                    {
                        FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.usingConveyor = false;
                        ConveyorController.Instance.FinishedSiteStopConveyor(this.finishedSiteStateMachine.ConveyorNo);
                        this.taskIsDone = true;

                    }
                }
                
                this.isStuck = true;
            }));
        }

        public override void ExitState()
        {
            this.isBreak = false;
            this.taskIsDone = false;
            this.isStuck = false;

            if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA
                    && !ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).AutoExitBoard)
            {
                ConveyorController.Instance.FinishedSiteResetConveyor(this.finishedSiteStateMachine.ConveyorNo);
            }
            else
            {
                ConveyorController.Instance.ConveyorStop(this.finishedSiteStateMachine.ConveyorNo);
            }
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.usingConveyor = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
            else if (this.isStuck)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.卡板);
            }
            else if (this.taskIsDone)
            {
                if (!ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).ExitIsSMEMA
                    && !ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).AutoExitBoard)
                {
                    if (ConveyorController.Instance.FinishedSiteExitSensor(this.finishedSiteStateMachine.ConveyorNo).Is(StsType.Low))
                    {
                        this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.出板完成);
                    }
                }
                else
                {
                    this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.出板完成);
                }
            }
        }
    }
}
