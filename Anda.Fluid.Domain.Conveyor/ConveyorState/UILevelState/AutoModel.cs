
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    /*************************************************************************************************
     * 由等待运行模式跳入Auto模式时，会直接进入到Auto模式的入口。
     * 这里是起到一个中继作用，并没有任何实际作用。
     * ***********************************************************************************************/
    public class AutoModel : IntegralState
    {
        public AutoModel(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.Auto模式.ToString();
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
            //if (Machine.Instance.Setting.MachineSelect != MachineSelection.AD19)
            //{
            //    if (this.integralStateMachine.ConveyorNo == 1)
            //    {
            //        this.integralStateMachine.ChangeState(IntegralStateEnum.初始状态);
            //    }
            //    else
            //    {
            //        this.integralStateMachine.ChangeState(IntegralStateEnum.Auto起始状态);
            //    }
            //}
            //else
            //{
            //    this.integralStateMachine.ChangeState(IntegralStateEnum.Auto起始状态);
            //}
            this.integralStateMachine.ChangeState(IntegralStateEnum.Auto起始状态);
        }
    }
}
