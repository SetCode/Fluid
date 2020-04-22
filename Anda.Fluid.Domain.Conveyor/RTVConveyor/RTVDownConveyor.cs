using Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor
{
    public class RTVDownConveyor
    {
        public RTVConveyorStateMachine stateMachine;
        public RTVDownConveyor()
        {
            this.stateMachine = new RTVConveyorStateMachine();
        }

        public void InitStates()
        {
            this.stateMachine.Region(RTVConveyorStateEnum.起始状态, new RTVEnterState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.轨道检查, new RTVCheckState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.空闲, new RTVIdleState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.求板, new RTVAskState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.进板中, new RTVBoardEnteringState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.板到位, new RTVBoardArrivedState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.出板中, new RTVBoardLeavingState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.出板完成, new RTVBoardLeftState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.作业结束, new RTVExitState(this.stateMachine));
            this.stateMachine.Region(RTVConveyorStateEnum.卡板, new RTVStuckState(this.stateMachine));

            this.stateMachine.SetDefault(RTVConveyorStateEnum.起始状态);
        }
    }
}
