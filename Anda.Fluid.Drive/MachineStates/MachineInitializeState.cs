using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineInitializeState : IMachineStatable
    {
        private readonly static MachineInitializeState instance = new MachineInitializeState();
        private MachineInitializeState() { }
        public static MachineInitializeState Instance => instance;

        public string StateName => "Initialize";

        public void Enter(Machine machine)
        {
        }

        public void Execute(Machine machine)
        {
            if(machine.IsMotionInitDone)
            {
                machine.FSM.ChangeState(MachineIdleState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }
    }
}
