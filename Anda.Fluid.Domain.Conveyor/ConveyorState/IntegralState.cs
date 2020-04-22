using Anda.Fluid.Domain.Conveyor.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState
{
    public abstract class IntegralState:StateBase
    {
        protected IntegralStateMachine integralStateMachine;
        public IntegralState(IntegralStateMachine integralStateMachine):base(integralStateMachine)
        {
            this.integralStateMachine = integralStateMachine;
        }

    }
}
