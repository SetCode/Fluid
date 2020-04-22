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
    public class BoardEntering : SingleSiteState
    {
        private DateTime startTime;
        public BoardEntering(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.进板中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.进板中;

            System.Threading.Interlocked.Increment(ref FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).SubSitesLevel.SingleSite.EnterBoardCounts);

            Task.Factory.StartNew(new Action(() =>
            {
                //打开温控器
                HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.程序开始运行时);
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.程序开始运行时);
                }

                Thread.Sleep(1000);
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

            this.startTime = DateTime.Now;
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (this.IsStuck())
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.卡板);
            }

            else if (ConveyorController.Instance.SingleSiteArriveSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.板到位);
            }
        }


        private bool IsStuck()
        {
            if (DateTime.Now - this.startTime > TimeSpan.FromMilliseconds(
                ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).WorkingSitePrm.EnterStuckTime))
            {
                return true;
            }
            else
                return false;
        }
    }
}
