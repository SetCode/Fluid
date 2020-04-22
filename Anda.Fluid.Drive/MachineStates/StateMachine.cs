using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public class StateMachine:IAlarmLightable
    {
        public IMachineStatable CurrState { get; private set; }

        public IMachineStatable PrevState { get; private set; }

        public event Action<IMachineStatable> StateChanged;

        public ProductionState CurrProductionState { get; private set; }

        public ProductionState PrevProductionState { get; private set; }

        public event Action<ProductionState> ProductionStateChanged;

        public ManualResetEvent EnterAbortedState = new ManualResetEvent(false);

        public void Update()
        {
            this.CurrState.Execute(Machine.Instance);
        }

        public void ChangeState(IMachineStatable state)
        {
            if(this.CurrState == state)
            {
                return;
            }
            this.PrevState = this.CurrState;
            this.PrevState?.Exit(Machine.Instance);
            this.CurrState = state;
            this.CurrState?.Enter(Machine.Instance);
            this.StateChanged?.Invoke(this.CurrState);
            Log.Print(string.Format("machine change state: {0} => {1}", this.PrevState, this.CurrState.StateName));
        }

        public void ChangeProductionState(ProductionState state)
        {
            if(this.CurrProductionState == state)
            {
                return;
            }
            this.PrevProductionState = this.CurrProductionState;
            this.CurrProductionState = state;
            Logger.DEFAULT.Info(LogCategory.RUNNING, "", string.Format("change production state: {0}", state));
            this.ProductionStateChanged?.Invoke(this.CurrProductionState);
        }

        public void HandleAlarmEvent(AlarmHandleType alarmType)
        {
            switch (alarmType)
            {
                case AlarmHandleType.ImmediateHandle:
                    this.ChangeProductionState(ProductionState.Alarm);
                    break;
                case AlarmHandleType.DelayHandle:
                    this.ChangeProductionState(ProductionState.Alarm);
                    break;
                case AlarmHandleType.AutoAndImmeDiateHandle:
                    this.EnterAbortedState.WaitOne(10000);
                    MachineAlarmState.Instance.IsImmidiateAlarm = true;
                    this.ChangeState(MachineAlarmState.Instance);
                    break;
                case AlarmHandleType.AutoAndDelayHandle:
                    MachineAlarmState.Instance.IsImmidiateAlarm = false;
                    this.ChangeState(MachineAlarmState.Instance);
                    break;
            }
        }
        public void StopLightTower()
        {
            if (this.CurrState is MachineProductionState)
            {
                this.ChangeProductionState(ProductionState.Normal);
            }
            else if (this.CurrState is MachineAlarmState)
            {
                this.ChangeState(MachineIdleState.Instance);
            }
        }
    }
}
