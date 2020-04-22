using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class DispenseDone : SingleSiteState
    {
        private bool taskIsDone;
        private bool isBreak;
        private bool isStuck;
        public DispenseDone(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.点胶完成.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.点胶完成;

            //温控器控制
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                Machine.Instance.HeaterController1.Opeate(OperateHeaterController.可以启动自动关闭功能);
            }
            else
            {
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    Machine.Instance.HeaterController2.Opeate(OperateHeaterController.可以启动自动关闭功能);
                }
                Machine.Instance.HeaterController1.Opeate(OperateHeaterController.可以启动自动关闭功能);
            }

            Task.Factory.StartNew(new Action(() =>
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).IsWaitOutInExitSensor)
                {
                    // 板子停在出板电眼处等待出板
                    ConveyorController.Instance.SetWorkingSiteStopper(this.singleSiteStateMachine.ConveyorNo, false);
                    ConveyorController.Instance.SetWorkingSiteLift(this.singleSiteStateMachine.ConveyorNo, false);
                    DateTime startTime = DateTime.Now;
                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(Math.Max(ConveyorPrmMgr.Instance.FindBy
                        (this.singleSiteStateMachine.ConveyorNo).LiftUpDelay,
                       ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).StopperUpDelay)))
                    {
                        if (this.isBreak)
                            return;
                    }

                    ConveyorController.Instance.ConveyorForward(this.singleSiteStateMachine.ConveyorNo);

                    startTime = DateTime.Now;
                    while (!ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                    {
                        TimeSpan timeSpan = DateTime.Now - startTime;
                        if (timeSpan >= TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).WorkingSitePrm.ExitStuckTime))
                        {
                            this.isStuck = true;
                            goto end;
                        }
                        Thread.Sleep(1);
                    }
                    end:
                    ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
                    this.taskIsDone = true;
                }
                else
                {
                    ConveyorController.Instance.SetWorkingSiteLift(this.singleSiteStateMachine.ConveyorNo, false);

                    DateTime startTime = DateTime.Now;
                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(Math.Max(ConveyorPrmMgr.Instance.FindBy
                        (this.singleSiteStateMachine.ConveyorNo).LiftUpDelay,
                       ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).StopperUpDelay)))
                    {
                        if (this.isBreak)
                            return;
                    }
                    this.taskIsDone = true;
                    return;
                }
            }));

        }

        public override void ExitState()
        {
            this.taskIsDone = false;
            this.isBreak = false;
            this.isStuck = false;
        }

        public override void UpdateState()
        {
            ConveyorController.Instance.InStoreSignalling(this.singleSiteStateMachine.ConveyorNo, true);

            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if(this.isStuck)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.卡板);
            }
            else if (this.taskIsDone) 
            {
                if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.Demo)
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.反转出板);
                }
                else if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.Auto)
                {
                    if (!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ExitIsSMEMA)
                    {
                        this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板中);
                    }
                    else
                    {
                        if (!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                            && ConveyorController.Instance.DownstreamAskBoard(this.singleSiteStateMachine.ConveyorNo).Is(Infrastructure.StsType.High))
                        {
                            this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板中);
                        }
                        else if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                            && FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard)
                        {
                            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard = false;
                            this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板中);
                        }
                    }
                }
            }
        }
    }
}
