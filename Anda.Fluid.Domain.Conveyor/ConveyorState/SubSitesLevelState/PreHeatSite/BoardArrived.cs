using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.Utils;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class BoardArrived : PreSiteState
    {
        private bool boardIsFix;
        private bool TaskIsBreak;
        public BoardArrived(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.板到达.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.板到达;

            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorTimerMgr.Instance.PreBoardArrivedTimer.ResetAndStart(ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).PreSitePrm.BoardArrivedDelay);
                while (ConveyorTimerMgr.Instance.PreBoardArrivedTimer.IsNormal)
                {
                    if (this.TaskIsBreak)
                    {
                        return;
                    }
                    Thread.Sleep(2);
                }

                FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).SubSitesLevel.PreSite.usingConveyor = false;
                ConveyorController.Instance.ConveyorStop(this.preSiteStateMachine.ConveyorNo);

                ConveyorController.Instance.SetPreSiteLift(this.preSiteStateMachine.ConveyorNo, true);

                DateTime liftTime = DateTime.Now;
                while (this.IsDelaying(liftTime, ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).LiftUpDelay))
                {
                    if (this.TaskIsBreak)
                    {
                        return;
                    }
                    Thread.Sleep(2);
                }
                // 启用轨道扫码 预热站顶板后扫码
                if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ConveyorScan)
                {
                    int ret = -1;
                    string tempStr = "";
                    for (int i = 0; i < 3; i++)
                    {
                        ret = Machine.Instance.GetCurConveyorBarcodeScanner(this.preSiteStateMachine.ConveyorNo).BarcodeScannable.ReadValue(TimeSpan.FromSeconds(3), out tempStr);
                        if (ret == 0)
                        {
                            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.PreBarcode = tempStr;
                            break;
                        }
                    }
                    // todo 扫不到码停在当前站，上层弹窗处理
                    if (ret !=0)
                    {
                        FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.PreBarcode = "";
                    }
                    else
                    {
                        this.boardIsFix = true;
                    }
                    FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = true;
                }
                else
                {
                    this.boardIsFix = true;
                }

            }));
        }

        public override void ExitState()
        {
            this.boardIsFix = false;
            this.TaskIsBreak = false;
            if (ConveyorPrmMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ConveyorScan)
            {
                FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = false;
            }
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.TaskIsBreak = true;
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
            else if (this.boardIsFix)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.加热中);
            }
        }

        /// <summary>
        /// 判断是否在延时中，是的话返回true
        /// </summary>
        /// <returns></returns>
        private bool IsDelaying(DateTime startTime,double delayTime)
        {
            if (DateTime.Now - startTime < TimeSpan.FromMilliseconds(delayTime))
            {
                return true;
            }
            else
                return false;
        }
    }
}
