using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.BaseClass
{
    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class StateBase
    {
        protected StateMachineBase stateMachine;
        public StateBase(StateMachineBase stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public abstract string GetName { get; }
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}
