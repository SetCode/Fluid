using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreHeating : PreSiteState
    {
        private bool taskIsBreak;
        private bool taskIsDone;
        private DateTime startTime;
        public PreHeating(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.加热中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.加热中;

            //复位标志位
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = false;

            if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).PreSitePrm.HeatingTime == 0)
            {
                this.taskIsDone = true;
            }
            else
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    this.startTime = DateTime.Now;
                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                        (this.preSiteStateMachine.ConveyorNo).PreSitePrm.HeatingTime))
                    {
                        if (this.taskIsBreak)
                        {
                            return;
                        }
                        Thread.Sleep(2);
                    }
                    this.taskIsDone = true;
                }));
            }
                    
        }

        public override void ExitState()
        {
            this.taskIsBreak = false;
            this.taskIsDone = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.taskIsBreak = true;
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.求板)
            {
                //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
                //this.CalculateWorkingSiteHeatingTime();    
                //this.preSiteStateMachine.ChangeState(PreSiteStateEnum.出板中);

                if (this.taskIsDone)
                {
                    this.preSiteStateMachine.ChangeState(PreSiteStateEnum.出板中);
                }                     
               
            }
        }

        //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
        //private void CalculateWorkingSiteHeatingTime()
        //{
        //    if (this.heatIsDone)
        //    {
        //        FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.NeedHeatingTime = 0;
        //    }

        //    else
        //    {
        //        TimeSpan timeSpan = DateTime.Now - this.startTime;
        //        FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.WorkingSite.NeedHeatingTime =
        //           ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).HeatingTime - timeSpan.Milliseconds;
        //    }

        //}
    }
}
