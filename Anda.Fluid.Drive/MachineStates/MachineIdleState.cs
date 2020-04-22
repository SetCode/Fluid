using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineIdleState : IMachineStatable
    {
        private readonly static MachineIdleState instance = new MachineIdleState();
        private MachineIdleState() { }
        public static MachineIdleState Instance => instance;

        public string StateName => "Idle";

        public void Enter(Machine machine)
        {
        }

        public void Execute(Machine machine)
        {
            if (machine.IsProducting)
            {
                machine.FSM.ChangeState(MachineProductionState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }
    }
}
