
using Anda.Fluid.Domain.Conveyor.Flag;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    /*********************************************************************************************
     * Offline模式状态，是Offline模式下的入口和出口。
     * 根据接受到的消息，会进入到手动进板或者手动出板状态。
     * 如果接受到离开编程界面的消息，则会退回到等待运行状态。
     * *******************************************************************************************/
    public class OfflineModel : IntegralState
    {
        public OfflineModel(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.Offline模式.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.Offline模式;
            //电机停止转动
            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
        }

        public override void ExitState()
        {
            //复位标志位
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.EnterEditForm = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.ExitEditForm = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.ExitEditForm)
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.等待运行);
            }
        }
    }
}
