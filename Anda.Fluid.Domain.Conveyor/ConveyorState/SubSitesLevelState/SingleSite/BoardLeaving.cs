using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class BoardLeaving : SingleSiteState
    {
        private bool isBreak;
        private bool taskIsDone;
        private bool isStuck;
        private bool isDoneTime;
        private bool liftIsStuck;
        private DateTime doneTime;
        public BoardLeaving(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.出板中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.出板中;

            if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ExitIsSMEMA
                || FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough) 
            {
                ConveyorController.Instance.InStoreSignalling(this.singleSiteStateMachine.ConveyorNo,true);
            }

            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorController.Instance.SetWorkingSiteStopper(this.singleSiteStateMachine.ConveyorNo,false);

                DateTime stopperDelay = DateTime.Now;
                //如果是RTV
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV && ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOEnable)
                {
                    while (!DiType.待料阻挡下位.Sts().Is(StsType.High))
                    {
                        if (this.isBreak)
                        {
                            return;
                        }

                        if (DateTime.Now - stopperDelay > TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOStuckTime))
                        {
                            this.liftIsStuck = true;
                            return;
                        }

                        Thread.Sleep(2);
                    }
                }
                else
                {
                    while (DateTime.Now - stopperDelay < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                         (this.singleSiteStateMachine.ConveyorNo).StopperUpDelay))
                    {
                        if (this.isBreak)
                            return;
                    }
                }

                this.ConveyorRun();

                DateTime stuckTime = DateTime.Now;
                //SMEMA模式出板
                if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ExitIsSMEMA
                    || FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
                {
                    Sts SingleSiteExitSensor = new Sts();
                    int fallCount = 0;
                    while (DateTime.Now - stuckTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                   (this.singleSiteStateMachine.ConveyorNo).DownStreamStuckTime))
                    {
                        SingleSiteExitSensor.Update(ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Value);
                        if (SingleSiteExitSensor.Is(StsType.IsFalling))
                        {
                            fallCount++;
                            Logger.DEFAULT.Info("fallingCount: " + fallCount + "\r\n");
                        }
                        if (!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                          && ConveyorController.Instance.DownstreamAskBoard(this.singleSiteStateMachine.ConveyorNo).Is(StsType.Low)
                          && (fallCount >= 1))
                        {
                            this.taskIsDone = true;
                            return;
                        }
                        else if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                                 && ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.Low))
                        {
                            this.taskIsDone = true;
                            return;
                        }
                    }
                        
                }
                else
                {
                    while (DateTime.Now - stuckTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.singleSiteStateMachine.ConveyorNo).WorkingSitePrm.ExitStuckTime))
                    {
                        //自动出板
                        if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).AutoExitBoard)
                        {
                            if (ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.IsFalling))
                            {
                                this.taskIsDone = true;
                                return;
                            }
                        }
                        //手动出板
                        else
                        {
                            if (ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                            {
                                ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
                                this.taskIsDone = true;
                                return;
                            }
                        }
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
            this.isDoneTime = false;
            this.liftIsStuck = false;
            ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (this.isStuck)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.卡板);
            }
            else if (this.liftIsStuck)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.气缸卡住);
            }
            else if (this.taskIsDone)
            {               
                if (this.DelayIsDone())
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板完成);
                }

            }
        }
        private void ConveyorRun()
        {
            switch (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).BoardDirection)
            {
                case BoardDirection.LeftToRight:
                    ConveyorController.Instance.ConveyorForward(this.singleSiteStateMachine.ConveyorNo);
                    break;
                case BoardDirection.RightToLeft:
                    ConveyorController.Instance.ConveyorForward(this.singleSiteStateMachine.ConveyorNo);
                    break;
                case BoardDirection.LeftToLeft:
                    ConveyorController.Instance.ConveyorBack(this.singleSiteStateMachine.ConveyorNo);
                    break;
                case BoardDirection.RightToRight:
                    ConveyorController.Instance.ConveyorBack(this.singleSiteStateMachine.ConveyorNo);
                    break;
            }
        }

        private bool DelayIsDone()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                if (!this.isDoneTime)
                {
                    this.isDoneTime = true;
                    this.doneTime = DateTime.Now;
                    return false;
                }
                if (this.isDoneTime)
                {
                    if (DateTime.Now - this.doneTime > TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.UpConveyorTurnTime))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;         
            }
            else
            {
                return true;
            }
        }

    }
}
