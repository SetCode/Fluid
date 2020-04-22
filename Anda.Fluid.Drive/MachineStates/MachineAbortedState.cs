using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public sealed class MachineAbortedState : IMachineStatable
    {
        private readonly static MachineAbortedState instance = new MachineAbortedState();
        private MachineAbortedState() { }
        public static MachineAbortedState Instance => instance;

        public string StateName => "Aborted";

        public void Enter(Machine machine)
        {
            machine.FSM.EnterAbortedState.Set();
            //machine.LightTower.Set(LightTowerType.Red, true);
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
            machine.FSM.EnterAbortedState.Reset();
        }
    }
}
