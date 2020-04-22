using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public class MachineAlarmState : IMachineStatable
    {
        private static MachineAlarmState instance = new MachineAlarmState();
        private MachineAlarmState() { }
        public static MachineAlarmState Instance => instance;
        public string StateName => "Alarm";

        public bool IsImmidiateAlarm { get; set; } = false;

        public void Enter(Machine machine)
        {
        }

        public void Execute(Machine machine)
        {
            if (machine.IsProducting)
            {
                machine.FSM.ChangeState(MachineProductionState.Instance);
            }
            if (!machine.IsMotionErrStop && machine.IsAborted)
            {
                machine.FSM.ChangeState(MachineAbortedState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }
    }
}
