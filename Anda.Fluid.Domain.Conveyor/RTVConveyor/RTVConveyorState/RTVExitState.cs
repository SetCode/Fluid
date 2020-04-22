using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVExitState : StateBase
    {
        public override string GetName => RTVConveyorStateEnum.作业结束.ToString();

        public RTVExitState(RTVConveyorStateMachine stateMachine):base(stateMachine)
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
            this.stateMachine.ChangeState(RTVConveyorStateEnum.起始状态);
        }
    }
}
