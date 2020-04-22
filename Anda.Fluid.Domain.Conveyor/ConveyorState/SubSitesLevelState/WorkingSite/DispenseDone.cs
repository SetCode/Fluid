using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class DispenseDone : WorkingSiteState
    {
        private bool taskIsDone;
        private bool isBreak;
        private bool dispenseTaskIsDone;
        public DispenseDone(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.点胶完成.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.点胶完成;

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
                //如果是预热站加点胶站，向下游设备发送信号
                if (ConveyorPrmMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
                {
                    ConveyorController.Instance.InStoreSignalling(this.workingStateMachine.ConveyorNo, true);
                }

                ConveyorController.Instance.SetWorkingSiteLift(this.workingStateMachine.ConveyorNo,false);

                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(Math.Max(ConveyorPrmMgr.Instance.FindBy
                    (this.workingStateMachine.ConveyorNo).LiftUpDelay,
                   ConveyorPrmMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).StopperUpDelay)))
                {
                    if (this.isBreak)
                        return;
                }
                this.taskIsDone = true;

                if (this.TotalTaskIsDone()) 
                {
                    this.dispenseTaskIsDone = true;
                } 
            }));
            
        }

        public override void ExitState()
        {
            this.taskIsDone = false; ;
            this.isBreak = false;
            this.dispenseTaskIsDone = false;
        }

        public override void UpdateState()
        {
            if (this.dispenseTaskIsDone
                || FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }

            else if (this.NeedContinueWork()) 
            {
                if (this.taskIsDone)
                {
                    this.workingStateMachine.ChangeState(WorkingSiteStateEnum.起始状态);
                }                
            }
        }
        private bool TotalTaskIsDone()
        {
            if (FlagBitMgr.Instance.BoardCounts == 0)
            {
                return false;
            }
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts
                      == FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts
                      && FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState == PreSiteStateEnum.作业结束)
            {
                return true;
            }
            else
                return false;
        }
        private bool NeedContinueWork()
        {
            if (ConveyorPrmMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
            {
                if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState == FinishedSiteStateEnum.出板完成
                     && (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts
                     - FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.ExitBoardCounts == 0))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState == FinishedSiteStateEnum.加热中
                     && (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts
                     - FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.ExitBoardCounts == 0))
                {
                    return true;
                }
                else
                    return false;
            }

        }
    }
}
