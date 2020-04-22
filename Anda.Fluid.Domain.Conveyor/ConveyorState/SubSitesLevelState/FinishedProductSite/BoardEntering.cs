using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class BoardEntering : FinishedSiteState
    {
        private bool taskIsDone;
        private bool isBreak;
        private bool isStuck;
        public BoardEntering(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.进板中.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.进板中;

            System.Threading.Interlocked.Increment(ref FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.ExitBoardCounts);

            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorController.Instance.SetWorkingSiteStopper(this.finishedSiteStateMachine.ConveyorNo,false);

                DateTime stopperDelay = DateTime.Now;
                while (DateTime.Now - stopperDelay < TimeSpan.FromMilliseconds
                        (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).StopperUpDelay)) 
                {
                    if (this.isBreak)
                        return;
                    Thread.Sleep(1);
                }

                ConveyorController.Instance.ConveyorForward(this.finishedSiteStateMachine.ConveyorNo);
                FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.usingConveyor = true;

                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds
                        (ConveyorPrmMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).WorkingSitePrm.ExitStuckTime))
                {
                    if (this.isBreak)
                        return;
                    if(ConveyorController.Instance.FinishedSiteArriveSensor(this.finishedSiteStateMachine.ConveyorNo).Is(StsType.High))
                    {
                        DateTime delayTime = DateTime.Now;
                        while (DateTime.Now - delayTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                                (this.finishedSiteStateMachine.ConveyorNo).FinishedSitePrm.BoardArrivedDelay))
                        {
                            if (this.isBreak)
                                return;
                        }
                        this.taskIsDone = true;
                        return;
                    }
                }
                this.isStuck = true;
            }));
        }

        public override void ExitState()
        {
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).SubSitesLevel.FinishedSite.usingConveyor = false;
            ConveyorController.Instance.ConveyorStop(this.finishedSiteStateMachine.ConveyorNo);

            this.isBreak = false;
            this.taskIsDone = false;
            this.isStuck = false;                                 
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.isBreak = true;                        
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
            else if (this.isStuck)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.卡板);
            }
            else if (this.taskIsDone)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.加热中);
            }

        }
    }
}
