using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.Utils;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class BoardEntering : PreSiteState
    {
        private DateTime startTime;
        public BoardEntering(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
            
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.板进入.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.板进入;

            System.Threading.Interlocked.Increment(ref FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts);

            ConveyorTimerMgr.Instance.PreBoardEnteringTimer.ResetAndStart(ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).PreSitePrm.StuckTime);

            Task.Factory.StartNew(new Action(() =>
            {
                //打开温控器
                HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.程序开始运行时);
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.程序开始运行时);
                }
                //写入标准温度
                if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
                {
                    if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                    {
                        Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, Machine.Instance.HeaterController1.HeaterPrm.Standard[1], 1);
                    }
                    Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, Machine.Instance.HeaterController1.HeaterPrm.Standard[0], 0);
                }
                else
                {
                    if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                    {
                        Machine.Instance.HeaterController2.Fire(HeaterMsg.设置标准温度值, Machine.Instance.HeaterController2.HeaterPrm.Standard[0], 0);
                    }
                    Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, Machine.Instance.HeaterController1.HeaterPrm.Standard[0], 0);
                }

            }));
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else if (!ConveyorTimerMgr.Instance.PreBoardEnteringTimer.IsNormal)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.卡板);
            }

            //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
            //else if(FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState== WorkingSiteStateEnum.求板)
            //{
            //    this.CalculateWorkingSiteHeatingTime();
            //    this.preSiteStateMachine.ChangeState(PreSiteStateEnum.出板中);
            //}

            else if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.DispenseAndInsulation)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.出板中);
            }

            else if (!(ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.DispenseAndInsulation)
                 && ConveyorController.Instance.PreSiteArriveSensor(this.preSiteStateMachine.ConveyorNo).Is(StsType.High)) 
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.板到达);
            }
        }

        //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
        //private void CalculateWorkingSiteHeatingTime()
        //{
        //    FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.NeedHeatingTime =
        //            ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).HeatingTime;
        //}

    }
}
