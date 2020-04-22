using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class SingleEnter : SingleSiteState
    {
        public SingleEnter(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.起始状态.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.起始状态;
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            //Demo模式
            else if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.Demo)
            {
                if (ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.求板);
                }
            }
            //Pass模式
            else if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.空闲);
            }
            //Auto模式
            else if(FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.Auto)
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).EnterIsSMEMA)
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.空闲);
                }
                else 
                {
                    if (ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                    {
                        this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.求板);
                    }
                }

            }

        }
    }
}
