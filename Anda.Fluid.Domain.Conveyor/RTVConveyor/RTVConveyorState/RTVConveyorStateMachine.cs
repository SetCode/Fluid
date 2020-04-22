using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVConveyorStateMachine: StateMachineBase
    {

    }
    public enum RTVConveyorStateEnum
    {
        起始状态,
        轨道检查,
        空闲,
        求板,
        进板中,
        板到位,
        出板中,
        出板完成,
        卡板,
        作业结束
    }
}
