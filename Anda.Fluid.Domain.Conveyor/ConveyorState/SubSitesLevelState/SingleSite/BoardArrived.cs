using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
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
    public class BoardArrived : SingleSiteState
    {
        private bool boardIsFix;
        private bool TaskIsBreak;
        private bool liftIsStuck;
        public BoardArrived(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.板到位.ToString();
            }
        }
        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.板到位;
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.IsContinueProduct = false;

            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
            {
                ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
            }
            else
            {
                Task.Factory.StartNew(new Action(() =>
                {

                    DateTime arrivedTime = DateTime.Now;
                    while (this.IsDelaying(arrivedTime,
                        ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).WorkingSitePrm.BoardArrivedDelay))
                    {
                        if (this.TaskIsBreak)
                        {
                            return;
                        }
                        Thread.Sleep(2);
                    }

                    ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);

                    ConveyorController.Instance.SetWorkingSiteLift(this.singleSiteStateMachine.ConveyorNo, true);

                    DateTime liftTime = DateTime.Now;
                    //如果是RTV
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV && ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOEnable) 
                    {
                        while (!DiType.压板下位.Sts().Is(StsType.High))
                        {
                            if (this.TaskIsBreak)
                            {
                                return;
                            }

                            if (DateTime.Now - liftTime > TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOStuckTime)) 
                            {
                                this.liftIsStuck = true;
                                return;
                            }

                            Thread.Sleep(2);
                        }
                    }
                    else
                    {
                        while (this.IsDelaying(liftTime, ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).LiftUpDelay))
                        {
                            if (this.TaskIsBreak)
                            {
                                return;
                            }
                            Thread.Sleep(2);
                        }
                    }


                    //查看胶阀加热是否完成
                    if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
                    {
                        if (!Machine.Instance.HeaterController1.HeaterPrm.IsContinuseHeating
                              && Machine.Instance.HeaterController1.HeaterPrm.CloseHeatingWhenIdle)
                        {
                            while (!Machine.Instance.HeaterController1.HeatingIsFinished)
                            {
                                if (this.TaskIsBreak)
                                {
                                    return;
                                }
                                Thread.Sleep(2);
                            }

                            //清洗
                            if (Machine.Instance.HeaterController1.wasClosed)
                            {
                                Machine.Instance.Valve1.DoPurgeAndPrime();
                                Machine.Instance.HeaterController1.wasClosed = false;
                            }
                            
                        }                                                 
                    }
                    else if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                    {
                        if (!Machine.Instance.HeaterController1.HeaterPrm.IsContinuseHeating
                                 && Machine.Instance.HeaterController1.HeaterPrm.CloseHeatingWhenIdle)
                        {
                            if (Machine.Instance.HeaterController1.HeaterControllable is AiKaThermostat)
                            {
                                while (!Machine.Instance.HeaterController1.HeatingIsFinished)
                                {
                                    if (this.TaskIsBreak)
                                    {
                                        return;
                                    }
                                    Thread.Sleep(2);
                                }
                                //清洗
                                if (Machine.Instance.HeaterController1.wasClosed)
                                {
                                    Machine.Instance.Valve1.DoPurgeAndPrime();
                                    Machine.Instance.Valve2.DoPurgeAndPrime();
                                    Machine.Instance.HeaterController1.wasClosed = false;
                                }
                               
                            }
                            else if (Machine.Instance.HeaterController1.HeaterControllable is ThermostatOmron)
                            {
                                while (!Machine.Instance.HeaterController1.HeatingIsFinished
                                         && !Machine.Instance.HeaterController2.HeatingIsFinished)
                                {
                                    if (this.TaskIsBreak)
                                    {
                                        return;
                                    }
                                    Thread.Sleep(2);
                                }
                                //清洗
                                if (Machine.Instance.HeaterController1.wasClosed)
                                {
                                    Machine.Instance.Valve1.DoPurgeAndPrime();
                                    Machine.Instance.Valve2.DoPurgeAndPrime();
                                    Machine.Instance.HeaterController1.wasClosed = false;
                                }
                            }
                        }                           
                    }

                    this.boardIsFix = true;
                }));
            }
            
        }

        public override void ExitState()
        {
            this.boardIsFix = false;
            this.TaskIsBreak = false;
            this.liftIsStuck = false;
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ModelLevel.Auto.DispenseStart = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.TaskIsBreak = true;
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
            {
                ConveyorController.Instance.InStoreSignalling(this.singleSiteStateMachine.ConveyorNo, true);
                if (!ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).ReceiveSMEMAIsPulse
                            && ConveyorController.Instance.DownstreamAskBoard(this.singleSiteStateMachine.ConveyorNo).Is(Infrastructure.StsType.High))
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板中);
                }

            }
            else
            {
                if (this.liftIsStuck)
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.气缸卡住);
                }
                if (this.boardIsFix)
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.准备点胶);
                }
            }
           
        }

        /// <summary>
        /// 判断是否在延时中，是的话返回true
        /// </summary>
        /// <returns></returns>
        private bool IsDelaying(DateTime startTime, double delayTime)
        {
            if (DateTime.Now - startTime < TimeSpan.FromMilliseconds(delayTime))
            {
                return true;
            }
            else
                return false;
        }
    }
}
