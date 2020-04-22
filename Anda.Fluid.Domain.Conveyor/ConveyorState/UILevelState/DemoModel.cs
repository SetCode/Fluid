using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    public class DemoModel : IntegralState
    {
        /*************************************************************************************************
         * 由等待运行模式跳入Demo模式时，会直接进入到Demo模式的入口。
         * 这里是起到一个中继作用，并没有任何实际作用。
         * ***********************************************************************************************/
        public DemoModel(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.Demo模式.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.Auto模式;
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (Machine.Instance.Setting.MachineSelect != MachineSelection.AD19)
            {
                if (this.integralStateMachine.ConveyorNo == 1)
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.初始状态);
                }
                else
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.运行Demo);
                }
            }
            else
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行Demo);
            }
        }
    }
}
