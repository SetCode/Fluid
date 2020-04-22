using Anda.Fluid.Drive.Conveyor.LeadShine;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineEStopState : IMachineStatable
    {
        private readonly static MachineEStopState instance = new MachineEStopState();
        private MachineEStopState() { }
        public static MachineEStopState Instance => instance;

        public string StateName => "EmergencyStop";

        public void Enter(Machine machine)
        {
            MachineServer.Instance.OnStop?.Invoke();

            if (machine.Setting.MachineSelect != MachineSelection.AD19)
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                {
                    DoType.求板信号.Set(false);
                    DoType.放板信号.Set(false);
                    DoType.运输反转.Set(false);
                    DoType.运输正转.Set(false);
                    DoType.预热开.Set(false);
                    DoType.预热阻挡.Set(false);
                    DoType.预热顶升.Set(false);
                    DoType.预热吹气.Set(false);
                    DoType.工作阻挡.Set(false);
                    //DoType.工作顶升.Set(false);
                    DoType.工作吹气.Set(false);
                    DoType.出板阻挡.Set(false);
                    DoType.出板顶升.Set(false);
                    DoType.出板吹气.Set(false);
                }
                else
                {
                DoType.求板信号.Set(false);
                DoType.放板信号.Set(false);
                DoType.运输反转.Set(false);
                DoType.运输正转.Set(false);
                DoType.预热开.Set(false);
                DoType.预热阻挡.Set(false);
                DoType.预热顶升.Set(false);
                DoType.预热吹气.Set(false);
                DoType.工作阻挡.Set(false);
                DoType.工作顶升.Set(false);
                DoType.工作吹气.Set(false);
                DoType.出板阻挡.Set(false);
                DoType.出板顶升.Set(false);
                DoType.出板吹气.Set(false);
            }
                
            }            
            else
            {
                ConveyorMachine.Instance.ResetAllDo();
            }
        }

        public void Execute(Machine machine)
        {
            bool flag = true;
            Axis[] axes = null;
            //if(Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Setting.DualValveMode != ValveSystem.DualValveMode.跟随)
            //{
            //    axes = Machine.Instance.Robot.AxesXYABZ;
            //}
            //else
            //{
            //    axes = Machine.Instance.Robot.AxesXYZ;
            //}
            //foreach (var item in axes)
            //{
            //    if (item.IsAlarm.Value || !item.IsServoOn.Value)
            //    {
            //        flag = false;
            //    }
            //}

            if (DiType.急停.Sts().Value && flag)
            {
                machine.FSM.ChangeState(MachineEStopResetState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }
    }
}
