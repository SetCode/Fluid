using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Heater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class BoardArrived : WorkingSiteState
    {
        private bool taskIsDone;
        private bool isBreak;
        public BoardArrived(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.板到位.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.板到位;
            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorController.Instance.SetWorkingSiteLift(this.workingStateMachine.ConveyorNo,true);

                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
                    FindBy(this.workingStateMachine.ConveyorNo).LiftUpDelay))
                {
                    if (this.isBreak)
                        return;                    
                }


                //查看胶阀加热是否完成
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
                {
                    if (!Machine.Instance.HeaterController1.HeaterPrm.IsContinuseHeating
                          && Machine.Instance.HeaterController1.HeaterPrm.CloseHeatingWhenIdle)
                    {
                        while (!Machine.Instance.HeaterController1.HeatingIsFinished)
                        {
                            if (this.isBreak)
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
                                if (this.isBreak)
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
                                if (this.isBreak)
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

                this.taskIsDone = true;
            }));
        }

        public override void ExitState()
        {
            this.taskIsDone = false;
            this.isBreak = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }
            else if (this.taskIsDone)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.加热中);
            }
        }
    }
}
