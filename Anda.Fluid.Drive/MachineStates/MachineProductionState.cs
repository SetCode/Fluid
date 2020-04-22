using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public enum ProductionState
    {
        Normal,
        Warn,
        Alarm,
    }

    public sealed class MachineProductionState : IMachineStatable
    {
        private readonly static MachineProductionState instance = new MachineProductionState();
        private MachineProductionState()
        {
            Machine.Instance.FSM.ProductionStateChanged += FSM_ProductionStateChanged;
        }

        public static MachineProductionState Instance => instance;

        public string StateName => "Production";

        public void Enter(Machine machine)
        {
            //this.updateLightTower(Machine.Instance.FSM.CurrProductionState);
        }

        public void Execute(Machine machine)
        {
            if(machine.IsProducting)
            {               
                return;
            }
            if (machine.IsMotionErrStop)
            {
                machine.FSM.ChangeState(MachineAlarmState.Instance);
            }
            else if (!machine.IsAborted)
            {
                machine.FSM.ChangeState(MachineIdleState.Instance);
            }
            else
            {
                machine.FSM.ChangeState(MachineAbortedState.Instance);
            }
        }

        public void Exit(Machine machine)
        {
        }

        private void FSM_ProductionStateChanged(ProductionState obj)
        {
        }
    }
}
