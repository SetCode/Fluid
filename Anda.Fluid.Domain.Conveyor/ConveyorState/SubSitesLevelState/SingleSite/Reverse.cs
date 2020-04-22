using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class Reverse : SingleSiteState
    {
        private bool isBreak;
        private bool taskIsDone;
        public Reverse(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.反转出板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.反转出板;

            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorController.Instance.SetWorkingSiteStopper(this.singleSiteStateMachine.ConveyorNo, false);

                DateTime stopperDelay = DateTime.Now;
                while (DateTime.Now - stopperDelay < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                    (this.singleSiteStateMachine.ConveyorNo).StopperUpDelay))
                {
                    if (this.isBreak)
                        return;
                }

                ConveyorController.Instance.ConveyorForward(this.singleSiteStateMachine.ConveyorNo);

                while(!ConveyorController.Instance.SingleSiteExitSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    if (this.isBreak)
                        return;
                    Thread.Sleep(10);
                }
                ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);

                Thread.Sleep(200);

                ConveyorController.Instance.ConveyorBack(this.singleSiteStateMachine.ConveyorNo);

                while (!ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    if (this.isBreak)
                        return;
                    Thread.Sleep(10);
                }
                ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
                this.taskIsDone = true;
            }));
        }

        public override void ExitState()
        {
            this.isBreak = false;
            this.taskIsDone = false;
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (this.taskIsDone)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.出板完成);
            }
        }

    }
}
