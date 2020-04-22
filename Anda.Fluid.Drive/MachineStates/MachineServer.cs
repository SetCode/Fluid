using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineServer : TaskLoop,IAlarmSenderable
    {
        private readonly static MachineServer instance = new MachineServer();

        private Stopwatch timer1 = new Stopwatch();
        private MachineServer()
        {
            this.loopSleepMills = 10;
            timer1.Reset();
        }
        public static MachineServer Instance => instance;

        public string Name => "Alarm Signal";

        public object Obj => this;

        public bool OnRun = false;
        public Action OnStop;
        public Action OnPausedOrResume;


        protected override void Loop()
        {
            if(!DiType.急停.Sts().Value)
            {
                Machine.Instance.FSM.ChangeState(MachineEStopState.Instance);
                if(Machine.Instance.Valve1.ValveSeries != ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve1.SprayOff();
                }
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Valve2.ValveSeries != ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve2.SprayOff();
                }
            }
            // 目前只有无轨道机台有启动停止暂停按键（RTV 未处理）
            if (Machine.Instance.IsNoConveyor())
            {
                //按钮按下执行切换状态
                DI di = DIMgr.Instance.FindBy((int)DiType.启动);
                if (di != null)
                {
                    if (this.stsStart == null)
                    {
                        this.stsStart = new Infrastructure.Sts();
                    }
                    this.stsStart.Update(di.Status.Value);
                    if (this.stsStart.IsRising || this.stsStart.Value)
                    {
                        if (timer1.IsRunning && timer1.Elapsed.TotalSeconds > 1)
                        {
                            this.OnRun = true;
                        }
                        else
                        {
                            timer1.Start();
                            this.OnRun = false;
                        }
                    }
                    else
                    {
                        timer1.Reset();
                        this.OnRun = false;
                    }
                }
                di = DIMgr.Instance.FindBy((int)DiType.停止);
                if (di != null)
                {
                    if (this.stsStop == null)
                    {
                        this.stsStop = new Infrastructure.Sts();
                    }
                    this.stsStop.Update(di.Status.Value);
                    if (this.stsStop.IsRising)
                    {
                        this.OnStop?.Invoke();
                    }
                }

                di = DIMgr.Instance.FindBy((int)DiType.暂停);
                if (di != null)
                {
                    if (this.stsPauseOrResume == null)
                    {
                        this.stsPauseOrResume = new Infrastructure.Sts();
                    }
                    this.stsPauseOrResume.Update(di.Status.Value);
                    if (this.stsPauseOrResume.IsRising)
                    {
                        if (Machine.Instance.IsProducting)
                        {
                            this.OnPausedOrResume?.Invoke();
                        }
                    }
                }
            }
            

            //Axis[] axes = null;
            //if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            //{
            //    axes = Machine.Instance.Robot.AxesXYZ;
            //}
            //else
            //{
            //    axes = Machine.Instance.Robot.AxesXYABZ;
            //}
            foreach (var item in Machine.Instance.Robot.AxesAll)
            {
                if(!item.Enabled)
                {
                    continue;
                }
                if(item.IsAlarm.Value || !item.IsServoOn.Value)
                {
                    Machine.Instance.Robot.IsHomeDone = false;
                    Machine.Instance.FSM.ChangeState(MachineEStopState.Instance);
                }
            }

            Machine.Instance.FSM.Update();
            // 根据当前机台状态和报警信息设置三色光和蜂鸣器状态
            this.GetNextLightTowerState();
            Machine.Instance.LightTower.Update();
            Machine.Instance.Laser.Laserable.Update();
            Machine.Instance.Scale.Scalable.Update();
            Machine.Instance.HeaterController1.HeaterControllable.Update();
            Machine.Instance.Proportioner1.Proportional.Update();
            Machine.Instance.Proportioner2.Proportional.Update();

            if (Machine.Instance.IsMotionInitDone)
            {
                this.UpdateDiAlarm();
                this.UpdateAxisStatus();
            }
        }
        /// <summary>
        /// 根据当前机台状态及报警状态综合判断当前三色灯及蜂鸣器的动作
        /// </summary>
        private void GetNextLightTowerState()
        {
            Tuple<int,Tuple<bool,bool>> alarmState;
            if (Machine.Instance.FSM.CurrState is MachineIdleState || Machine.Instance.FSM.CurrState is MachineAbortedState)
            {
                alarmState = AlarmServer.Instance.GetCurrentAlarmLightTowerState(1);
            }
            else
            {
                alarmState = AlarmServer.Instance.GetCurrentAlarmLightTowerState();
            }
            // 生产状态无异常时-绿灯常亮，蜂鸣器不响
            if (Machine.Instance.FSM.CurrState is MachineProductionState) // 生产时
            {
                Machine.Instance.LightTower.Set((LightTowerType)alarmState.Item1, alarmState.Item2.Item1, alarmState.Item2.Item2);
            }
            else if (Machine.Instance.FSM.CurrState is MachineIdleState || Machine.Instance.FSM.CurrState is MachineAbortedState)
            {
                // 机台空闲不运行时
                Machine.Instance.LightTower.Set((LightTowerType)alarmState.Item1, alarmState.Item2.Item1, alarmState.Item2.Item2);
            }
            else if (Machine.Instance.FSM.CurrState is MachineEStopState)
            {
                // 机台急停时 红灯闪烁蜂鸣器间歇鸣叫
                Machine.Instance.LightTower.Set(LightTowerType.Red, true, true);
            }
            else if (Machine.Instance.FSM.CurrState is MachineEStopResetState)
            {
                // 急停恢复时 黄灯常亮
                Machine.Instance.LightTower.Set(LightTowerType.Yellow, false, false);
            }
            else if(Machine.Instance.FSM.CurrState is MachineAlarmState)
            {
                // 机台报警停机时
                Machine.Instance.LightTower.Set((LightTowerType)alarmState.Item1, alarmState.Item2.Item1, alarmState.Item2.Item2);
            }
            else if (Machine.Instance.FSM.CurrState is MachineInitializeState )
            {
                // 机台初始化时 黄灯常亮
                Machine.Instance.LightTower.Set(LightTowerType.Yellow, false, false);
            }
        }

        /// <summary>
        /// 刷报警输入信号
        /// </summary>
        private void UpdateDiAlarm()
        {
            if (DiType.气压检测.Sts().Is(Infrastructure.StsType.High)) 
            {
                AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorAirPressure);
                Machine.Instance.IsAirPressureErrStop = true;
            }
            if (DiType.气压检测.Sts().Is(Infrastructure.StsType.Low))
            {
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorAirPressure);
                if (!Machine.Instance.IsProducting)
                {
                    Machine.Instance.IsAirPressureErrStop = false;
                }
            }

            if (DiType.胶量感应1.Sts().Is(Infrastructure.StsType.High))
            {
                AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve1GlueEmpty);
            }
            if (DiType.胶量感应1.Sts().Is(Infrastructure.StsType.Low))
            {
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve1GlueEmpty);
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                if (DiType.胶量感应2.Sts().Is(Infrastructure.StsType.High))
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve2GlueEmpty);
                }
                if (DiType.胶量感应2.Sts().Is(Infrastructure.StsType.Low))
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve2GlueEmpty);
                }
            }

            if (DiType.门禁.Sts().Is(Infrastructure.StsType.High))
            {
                AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorDoorGuard);
            }
            if (DiType.门禁.Sts().Is(Infrastructure.StsType.Low))
            {
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorDoorGuard);
            }

            //刷温度报警
            //按照艾卡开启的通道进行刷
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[0])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[0])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve1Temp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve1Temp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve1Temp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[1])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[1])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve2Temp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve2Temp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve2Temp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[2])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[2])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor1PreTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1PreTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1PreTemp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[3])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[3])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor1WorkTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1WorkTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1WorkTemp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[4])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[4])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor1FinishTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1FinishTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1FinishTemp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[5])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[5])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor2PreTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2PreTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2PreTemp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[6])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[6])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor2WorkTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2WorkTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2WorkTemp);
                }

                if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[7])
                {
                    if (!HeaterControllerMgr.Instance.FindBy(0).TempOk()[7])
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.Conveyor2FinishTemp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2FinishTemp);
                    }
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2FinishTemp);
                }
            }

            //按欧姆龙的刷
            else if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Omron)
            {
                //无论单双阀，都会刷第一个温控器
                if (DiType.阀1温度报警.Sts().Is( Infrastructure.StsType.High) && HeaterControllerMgr.Instance.FindBy(0).AlarmEnable)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve1Temp);
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve1Temp);
                }
                //双阀时刷第二个温控器
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (DiType.阀2温度报警.Sts().Is(Infrastructure.StsType.High) && HeaterControllerMgr.Instance.FindBy(1).AlarmEnable)
                    {
                        AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorValve2Temp);
                    }
                    else
                    {
                        AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve2Temp);
                    }
                }
            }
            else //设置为disable时,移除所有温度报警。
            {
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve1Temp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorValve2Temp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1PreTemp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1WorkTemp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor1FinishTemp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2PreTemp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2WorkTemp);
                AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.Conveyor2FinishTemp);
            }

            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (DiType.排风检测.Sts().Is(Infrastructure.StsType.High))
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoDI.ErrorExhaust);
                }
                if (DiType.排风检测.Sts().Is(Infrastructure.StsType.Low))
                {
                    AlarmServer.Instance.RemoveAlarm(this, AlarmInfoDI.ErrorExhaust);
                }
            }

        }

        /// <summary>
        /// 刷轴的报警状态
        /// </summary>
        private void UpdateAxisStatus()
        {
            //无论单双阀都会刷XYZ轴的状态
            foreach (var item in Machine.Instance.Robot.AxesXYZ)
            {
                this.HandleAxisAlarm(item);
            }
            //如果是单阀，会消除AB轴的报警
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                if (Machine.Instance.Robot.AxesAB == null)
                {
                    return;
                }
                foreach (var item in Machine.Instance.Robot.AxesAB)
                {
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.FatalIsAlarm);
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.FatalIsError);
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.FatalServoOn);
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.FatalMoveHomeError);
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.WarnMoveToPLmt);
                    AlarmServer.Instance.RemoveAlarm(item, AlarmInfoMotion.WarnMoveToNLmt);
                }
            }
            //如果是双阀,则会刷AB轴的报警
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                if (Machine.Instance.Robot.AxesAB == null)
                {
                    return;
                }
                foreach (var item in Machine.Instance.Robot.AxesAB)
                {
                    this.HandleAxisAlarm(item);
                }
            }
        }

        private void HandleAxisAlarm(Axis axis)
        {
            if (axis.IsAlarm.Value)
            {
                AlarmServer.Instance.Fire(axis, AlarmInfoMotion.FatalIsAlarm);
            }
            else
            {
                AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.FatalIsAlarm);
            }
            if (axis.IsError.Value)
            {
                AlarmServer.Instance.Fire(axis, AlarmInfoMotion.FatalIsError);
            }
            else
            {
                AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.FatalIsError);
            }
            if (axis.IsServoOn.Value)
            {
                AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.FatalServoOn);
            }
            else
            {
                AlarmServer.Instance.Fire(axis, AlarmInfoMotion.FatalServoOn);
            }
            if (axis.IsHomeError.Value)
            {
                AlarmServer.Instance.Fire(axis, AlarmInfoMotion.FatalMoveHomeError);
            }
            else
            {
                AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.FatalMoveHomeError);
            }
            if (axis.State.MoveMode != AxisMoveMode.MoveHome && axis.State.MoveMode != AxisMoveMode.EscapeLmt)
            {
                if (axis.IsPLmt.IsRising || axis.IsPLmt.Value)
                {
                    AlarmServer.Instance.Fire(axis, AlarmInfoMotion.WarnMoveToPLmt);
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.WarnMoveToPLmt);
                }
                if (axis.IsNLmt.IsRising || axis.IsNLmt.Value)
                {
                    AlarmServer.Instance.Fire(axis, AlarmInfoMotion.WarnMoveToNLmt);
                }
                else
                {
                    AlarmServer.Instance.RemoveAlarm(axis, AlarmInfoMotion.WarnMoveToNLmt);
                }
            }
        }
        private Infrastructure.Sts stsStop=new Infrastructure.Sts();
        private Infrastructure.Sts stsStart = new Infrastructure.Sts();
        private Infrastructure.Sts stsPauseOrResume = new Infrastructure.Sts();

        private bool IsRising(DI di)
        {
            Infrastructure.Sts sts = new Infrastructure.Sts();
            sts.Update(di.Status.Value);
            return sts.IsRising;
        }
    }
}
