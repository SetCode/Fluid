using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVEnterState : StateBase
    {
        public override string GetName => RTVConveyorStateEnum.起始状态.ToString();

        public RTVEnterState(RTVConveyorStateMachine rtvConveyorStateMachine):base(rtvConveyorStateMachine)
        {

        }

        public override void EnterState()
        {
            DoType.下层轨道有板信号.Set(false);
            DoType.下层轨道求板信号.Set(false);
            AxisType.Axis7.MoveStop();
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart) 
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.轨道检查);
            }             
        }
    }
}
