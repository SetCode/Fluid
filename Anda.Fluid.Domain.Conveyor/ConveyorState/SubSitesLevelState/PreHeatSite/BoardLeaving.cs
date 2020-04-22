using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.Utils;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class BoardLeaving : PreSiteState
    {
        private bool transIsDone;
        private bool taskIsBreak;
        private bool isStuck;
        public BoardLeaving(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.出板中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.出板中;

            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.usingConveyor = true;

            ConveyorTimerMgr.Instance.PreBoardLeavingTimer.ResetAndStart(ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).WorkingSitePrm.EnterStuckTime);

            Task.Factory.StartNew(new Action(() =>
            {                
                ConveyorController.Instance.SetPreSiteStopper(this.preSiteStateMachine.ConveyorNo, false);
                ConveyorController.Instance.SetPreSiteLift(this.preSiteStateMachine.ConveyorNo,false);

                DateTime stopperDelay = DateTime.Now;
                while (this.IsDelaying(stopperDelay, ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).LiftUpDelay, 
                    ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).StopperUpDelay))
                {
                    if (this.taskIsBreak)
                        return;
                    Thread.Sleep(2);
                }

                ConveyorController.Instance.ConveyorForward(this.preSiteStateMachine.ConveyorNo);

                DateTime stuckTime = DateTime.Now;
                while (!ConveyorController.Instance.WorkingSiteArriveSensor(this.preSiteStateMachine.ConveyorNo).Is(StsType.High)) 
                {
                    if (this.taskIsBreak)
                        return;
                    if (!ConveyorTimerMgr.Instance.PreBoardLeavingTimer.IsNormal)
                    {
                        this.isStuck = true;
                    }
                    Thread.Sleep(2);
                }

                DateTime arrivedDelay = DateTime.Now;
                while (DateTime.Now - arrivedDelay < TimeSpan.FromMilliseconds
                     (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).WorkingSitePrm.BoardArrivedDelay))
                {
                    if (this.taskIsBreak)
                        return;
                    Thread.Sleep(2);
                }

                FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.usingConveyor = false;
                ConveyorController.Instance.ConveyorStop(this.preSiteStateMachine.ConveyorNo);
                this.transIsDone = true;
            }));
        }

        public override void ExitState()
        {
            this.transIsDone = false;
            this.taskIsBreak = false;
            this.isStuck = false;
            
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.taskIsBreak = true;
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else if (this.transIsDone)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.出板完成);
            }
            else if (this.isStuck)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.卡板);
            }
        }

        /// <summary>
        /// 判断是否在延时中，是的话返回true
        /// </summary>
        /// <returns></returns>
        private bool IsDelaying(DateTime startTime, double delayTime1,double delayTime2)
        {
            if (DateTime.Now - startTime < TimeSpan.FromMilliseconds(Math.Max(delayTime1, delayTime2))) 
            {
                return true;
            }
            else
                return false;
        }
    }
}
