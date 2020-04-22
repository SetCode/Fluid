using Anda.Fluid.Domain.Conveyor.ConveyorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using System.Threading;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    /*************************************************************************************************
     * 由Auto模式状态进入到Auto入口状态时，会复位所有的气缸、轨道停止运转。
     * 然后直接进入轨道检查状态。
     * ***********************************************************************************************/
    public class AutoEnter : IntegralState
    {
        public AutoEnter(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.Auto起始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.Auto起始状态;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.IsContinueProduct = false;
            ConveyorController.Instance.ResetAllCylinder();

            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);

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
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            this.integralStateMachine.ChangeState(IntegralStateEnum.轨道检查);
        }
    }
}
