using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    /****************************************************************************************
     * 整体状态机的初始状态，既是状态机的起点，也是状态机的终点。
     * 在收到终止消息或者作业完成之后，会回到初始状态
     ****************************************************************************************/
    public class Initial : IntegralState
    {
        public Initial(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.初始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.初始状态;

        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (!this.ConveyorIsExist()) 
            {
                integralStateMachine.ChangeState(IntegralStateEnum.初始状态);
            }
            else
            {
                integralStateMachine.ChangeState(IntegralStateEnum.等待运行);
            }
                           
        }

        /// <summary>
        /// 确认轨道是否存在,存在返回true
        /// </summary>
        /// <returns></returns>
        private bool ConveyorIsExist()
        {
            if (this.integralStateMachine.ConveyorNo == 0)
            {
                if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1启用
                    || ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1和轨道2同时启用)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道2启用
                    || ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1和轨道2同时启用)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }    

        }
    }
}
