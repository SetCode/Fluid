using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class DispenseReady : WorkingSiteState
    {
        public DispenseReady(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.准备点胶.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.准备点胶;
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.CanDispense = true;
            //为了防止点胶程序错误发送点胶完成信号，在这里复位该信号
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.DispenseDone = false;
        }

        public override void ExitState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.CanDispense = false;
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.DispenseStart = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.DispenseStart)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.点胶中);
            }
        }
    }
}
