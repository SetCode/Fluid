using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    public class PassThroughModel : IntegralState
    {
        public PassThroughModel(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.PassThrough模式.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.PassThrough模式;
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (Machine.Instance.Setting.MachineSelect != MachineSelection.AD19)
            {
                if (this.integralStateMachine.ConveyorNo == 1 && Machine.Instance.Setting.ConveyorSelect != ConveyorSelection.双轨)
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.初始状态);
                }
                else
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.运行PassThrough);
                }
            }
            else
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行PassThrough);
            }
        }
    }
}
