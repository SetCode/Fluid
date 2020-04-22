using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
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
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    public class IntegralStuck : IntegralState
    {
        public IntegralStuck(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.卡板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.卡板;
            MessageBox.Show("轨道中可能有未知板，请人工处理。");

            // 单站模式增加生产当前轨道中的产品，避免拿出机台（特别是在线机台）
            if (ConveyorPrmMgr.Instance.FindBy(0).SubsiteMode == ConveyorSubsiteMode.Singel)
            {
                if (MessageBox.Show("是否生产当前轨道中的板？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1启用 || ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道2启用 || ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1和轨道2同时启用)
                    {
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
                            //气缸松开
                            ConveyorController.Instance.SetWorkingSiteStopper(this.integralStateMachine.ConveyorNo, false);
                            ConveyorController.Instance.SetWorkingSiteLift(this.integralStateMachine.ConveyorNo, false);

                            //如果工作站电眼或出板电眼感应有板，则反转一段距离再进入
                            if (ConveyorController.Instance.SingleSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                                    || ConveyorController.Instance.SingleSiteExitSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
                            {
                                ConveyorController.Instance.ConveyorBack(this.integralStateMachine.ConveyorNo);

                                //回转计时开始
                                DateTime startBackTime = DateTime.Now;
                                while (ConveyorController.Instance.SingleSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.Low)
                                        || ConveyorController.Instance.SingleSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.IsFalling))
                                {
                                    TimeSpan timeSpan = DateTime.Now - startBackTime;
                                    if (timeSpan >= TimeSpan.FromSeconds(10))
                                    {
                                        MessageBox.Show("可能发生卡板");
                                        goto end;
                                    }
                                    Thread.Sleep(1);
                                }
                                ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);

                                Thread.Sleep(2);
                            }

                            //如果没有感应到有板，则直接正转进入
                            ConveyorController.Instance.ConveyorForward(this.integralStateMachine.ConveyorNo);
                            ConveyorController.Instance.SetWorkingSiteStopper(this.integralStateMachine.ConveyorNo, true);

                            DateTime startForwardTime = DateTime.Now;
                            while (!ConveyorController.Instance.SingleSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
                            {
                                TimeSpan timeSpan = DateTime.Now - startForwardTime;
                                if (timeSpan >= TimeSpan.FromSeconds(10))
                                {
                                    MessageBox.Show("可能发生卡板");
                                    goto end;
                                }
                                Thread.Sleep(1);
                            }

                            //电眼感应到位延时
                            Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).WorkingSitePrm.BoardArrivedDelay);

                            ConveyorController.Instance.SetWorkingSiteLift(this.integralStateMachine.ConveyorNo, true);
                            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.IsContinueProduct = true;
                            end:
                            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
                        }));
                    }
                }
            }
            //需要按下卡板解决按钮
        }

        public override void ExitState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.StuckIsSolve = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.StuckIsSolve || FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.IsContinueProduct)
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行子站);
            }
            else if(FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行结束);
            }
        }
    }
}
