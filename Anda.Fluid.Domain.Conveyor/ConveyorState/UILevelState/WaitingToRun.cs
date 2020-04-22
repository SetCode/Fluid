
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.UILevelState
{
    /**********************************************************************************************
     * 在无限制的情况下，初始状态会无条件进入等待运行状态。
     * 等待运行状态会根据选择条件进入Offline,Auto,Demo或者PassThrough模式。
     **********************************************************************************************/
    public class WaitingToRun : IntegralState
    {
        public WaitingToRun(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.等待运行.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.等待运行;
            //复位标志位
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate = false;

            //清零各个子站的板计数器
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubSitesLevel.PreSite.EnterBoardCounts = 0;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.DispenseBoardCounts = 0;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.ExitBoardCounts = 0;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubSitesLevel.SingleSite.EnterBoardCounts = 0;
        }

        public override void ExitState()
        {
            //复位标志位
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.EnterEditForm = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.ExitEditForm = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.AutoRun = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.DemoRun = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.PassRun = false;

        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
            {
                integralStateMachine.ChangeState(IntegralStateEnum.初始状态);
            }
            else if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.EnterEditForm)
            {
                integralStateMachine.ChangeState(IntegralStateEnum.Offline模式);
            }
            else if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.AutoRun)
            {
                integralStateMachine.ChangeState(IntegralStateEnum.Auto模式);
            }
            else if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.DemoRun)
            {
                integralStateMachine.ChangeState(IntegralStateEnum.Demo模式);
            }
            else if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.PassRun)
            {
                integralStateMachine.ChangeState(IntegralStateEnum.PassThrough模式);
            }
        }
    }
}
