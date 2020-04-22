using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.DemoModel
{
    public class DemoRun : IntegralState
    {
        private SubSiteStateMachine subSiteStateMachine;
        public DemoRun(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
            
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.运行Demo.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.运行Demo;
            this.subSiteStateMachine = new SingleSiteStateMachine(this.integralStateMachine.ConveyorNo);
            this.subSiteStateMachine.Setup();
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (this.CanExit())
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.等待运行);
            }
            else
            {
                this.subSiteStateMachine.UpdateSate();
            }
        }

        private bool CanExit()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.作业结束
                    && FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
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
