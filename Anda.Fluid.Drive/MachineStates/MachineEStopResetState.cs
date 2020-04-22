using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineEStopResetState : IMachineStatable
    {
        private readonly static MachineEStopResetState instance = new MachineEStopResetState();
        private MachineEStopResetState() { }
        public static MachineEStopResetState Instance => instance;

        public string StateName => "EmergencyStopReset";

        public void Enter(Machine machine)
        {
        }

        public void Execute(Machine machine)
        {
            if(machine.IsInitializing)
            {
                machine.FSM.ChangeState(MachineInitializeState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }
    }
}
