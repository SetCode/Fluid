using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class WorkingHeating : WorkingSiteState
    {
        private bool isBreak;
        private bool taskIsDone;
        public WorkingHeating(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.加热中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.加热中;

            //复位标志位
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard = false;

            if (ConveyorPrmMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).WorkingSitePrm.HeatingTime == 0)
            {
                this.taskIsDone = true;
            }
            else
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    DateTime startTime = DateTime.Now;

                    //如果是工作站和预热站加热时间共享的情况，用以比较的是FlagBitMgr里的NeedHeatingTime。根据需求，现在不做考虑。

                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds
                            (ConveyorPrmMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).WorkingSitePrm.HeatingTime))
                    {
                        if (this.isBreak)
                            return;

                    }
                    this.taskIsDone = true;
                }));
            }
            
        }

        public override void ExitState()
        {
            this.isBreak = false;
            this.taskIsDone = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.作业结束);
            }
            else if (this.taskIsDone)
            {
                this.workingStateMachine.ChangeState(WorkingSiteStateEnum.准备点胶);
            }
        }
    }
}
