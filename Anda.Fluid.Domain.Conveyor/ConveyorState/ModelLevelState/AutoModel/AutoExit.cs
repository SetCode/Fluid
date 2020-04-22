using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    public class AutoExit : IntegralState
    {
        public AutoExit(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.运行结束.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.运行结束;

            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
        }

        public override void ExitState()
        {
           
        }

        public override void UpdateState()
        {
            this.integralStateMachine.ChangeState(IntegralStateEnum.等待运行);
        }
    }
}
