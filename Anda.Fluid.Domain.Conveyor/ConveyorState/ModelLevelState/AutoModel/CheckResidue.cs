using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Infrastructure;
using System.Threading;
using System;
using Anda.Fluid.Domain.Conveyor.Prm;
using System.Threading.Tasks;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    /************************************************************************
     * 本状态为轨道检查状态，对于三站的情况，将会分为三步进行检查。
     * 检查预热站残留板：①工作站和成品站顶升气缸顶起；
     *                   ②检查预热站到位电眼状态，
     *                   ③如果电眼有信号，则反转到信号为下降沿为止，且标记为
     *                     预热站有板；
     *                     如果电眼没有信号，则正转一定时间，期间电眼感应到则
     *                     停止，标记为预热站有板；如果走完这段时间没有感应到，
     *                     则标记为预热站没有板；
     *                     
     * 工作站和成品站的残留板检查和预热站类似。
     * 
     * 完成检查后，要将有板区域的板运动到阻挡气缸处并且顶升起来。（在下一步设
     * 置子站状态的时候，将有板区域的子站状态设置为加热中）
     * 
     * 对于单站的情况，检查步骤是：
     *                   ①先查看三个电眼是否有信号：
     *                     如果有信号，则认为有板，需要人工清理板之后，点击卡
     *                     板解决；
     *                     如果没有信号，则电机反转一定时间，如果在该时间段三
     *                     个电眼有信号，则认为有板，反之则认为无板。
     *                   ②有板时，将会报卡板，需要人工处理后，点击卡板解决。  
     *                   
     * 检查结束后：
     * 对于单站而言，如果卡板会进入到卡板状态，如果不卡板会进入到运行子站状态。
     * 对于三站而言，卡板会自动调整，调整后直接进入运行子站状态。               
     * **********************************************************************/
    public class CheckResidue : IntegralState
    {
        private bool isStuck;
        private bool checkIsDone;
        private bool taskIsBreak;
        public CheckResidue(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {

        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.轨道检查.ToString();
            }
        }

        public override void EnterState()
        {
            this.isStuck = false;
            this.checkIsDone = false;
            this.taskIsBreak = false;


        FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.轨道检查;

            //复位检查结果标志位
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard = false;
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard = false;

            Task.Factory.StartNew(new Action(() =>
            {
                if (ConveyorPrmMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.Singel)
                {
                    this.CheckSingleSite();
                }
                else
                {
                    this.CheckTripleSite();
                }
            }));
       
        }

        public override void ExitState()
        {
            //复位轨道检查结果和任务打断标志位
            this.isStuck = false;
            this.checkIsDone = false;

            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.taskIsBreak = true;
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行结束);
            }
            else if (this.isStuck)
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.卡板);
            }
            else if (this.checkIsDone)
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.轨道复位);
                }
                else
                {
                    this.integralStateMachine.ChangeState(IntegralStateEnum.运行子站);
                }                
            }         
        }
        /// <summary>
        /// 三站式的轨道检查,能自动调整，根据需求，现在不需要
        /// </summary>        
        //private void CheckTripleSite()
        //{
        //    #region 预热站的轨道检查
        //    ConveyorController.Instance.SetWorkingSiteLift(true);
        //    ConveyorController.Instance.SetFinishedSiteLift(true);
        //    ConveyorController.Instance.SetPreSiteLift(false);

        //    //气缸顶升延时
        //    DateTime liftDelayTime1 = DateTime.Now;
        //    while (DateTime.Now - liftDelayTime1 < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).LiftUpDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }

        //    if (ConveyorController.Instance.PreSiteArriveSensor().Is(StsType.High))
        //    {
        //        ConveyorController.Instance.ConveyorBack();
        //        while (ConveyorController.Instance.PreSiteArriveSensor().Is(StsType.High))
        //        {
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = true;
        //    }
        //    else
        //    {
        //        ConveyorController.Instance.ConveyorForward();
        //        DateTime startTime = DateTime.Now;

        //        bool haveBoard = true;
        //        while (!ConveyorController.Instance.PreSiteArriveSensor().Is(StsType.High))
        //        {                  
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            TimeSpan timeSpan = DateTime.Now - startTime;
        //            if (timeSpan >= TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy(0).PreSitePrm.CheckTime))
        //            {                   
        //                haveBoard = false;
        //                break;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        if (haveBoard)
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = true;
        //        }
        //        else
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard = false;
        //        }

        //    }
        //    ConveyorController.Instance.ConveyorStop();
        //    Thread.Sleep(1);
        //    #endregion

        //    #region 工作站的轨道检查
        //    ConveyorController.Instance.SetPreSiteLift(true);
        //    ConveyorController.Instance.SetFinishedSiteLift(true);
        //    ConveyorController.Instance.SetWorkingSiteLift(false);

        //    //气缸顶升延时
        //    DateTime liftDelayTime2 = DateTime.Now;
        //    while (DateTime.Now - liftDelayTime2 < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).LiftUpDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }

        //    if (ConveyorController.Instance.WorkingSiteArriveSensor().Is(StsType.High))
        //    {
        //        ConveyorController.Instance.ConveyorBack();
        //        while (ConveyorController.Instance.WorkingSiteArriveSensor().Is(StsType.High))
        //        {
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard = true;
        //    }
        //    else
        //    {
        //        ConveyorController.Instance.ConveyorForward();
        //        DateTime startTime = DateTime.Now;

        //        bool haveBoard = true;
        //        while (!ConveyorController.Instance.WorkingSiteArriveSensor().Is(StsType.High))
        //        {
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            TimeSpan timeSpan = DateTime.Now - startTime;
        //            if (timeSpan >= TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy(0).WorkingSitePrm.CheckTime))
        //            {
        //                haveBoard = false;
        //                break;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        if (haveBoard)
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard = true;
        //        }
        //        else
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard = false;
        //        }

        //    }
        //    ConveyorController.Instance.ConveyorStop();
        //    Thread.Sleep(1);
        //    #endregion

        //    #region 成品站的轨道检查
        //    ConveyorController.Instance.SetPreSiteLift(true);
        //    ConveyorController.Instance.SetWorkingSiteLift(true);
        //    ConveyorController.Instance.SetFinishedSiteLift(false);

        //    //气缸顶升延时
        //    DateTime liftDelayTime3 = DateTime.Now;
        //    while (DateTime.Now - liftDelayTime3 < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).LiftUpDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }

        //    if (ConveyorController.Instance.FinishedSiteArriveSensor().Is(StsType.High))
        //    {
        //        ConveyorController.Instance.ConveyorBack();
        //        while (ConveyorController.Instance.FinishedSiteArriveSensor().Is(StsType.High))
        //        {
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard = true;
        //    }
        //    else
        //    {
        //        ConveyorController.Instance.ConveyorForward();
        //        DateTime startTime = DateTime.Now;

        //        bool haveBoard = true;
        //        while (!ConveyorController.Instance.FinishedSiteArriveSensor().Is(StsType.High))
        //        {
        //            if (taskIsBreak)
        //            {
        //                return;
        //            }
        //            TimeSpan timeSpan = DateTime.Now - startTime;
        //            if (timeSpan >= TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy(0).FinishedSitePrm.CheckTime))
        //            {
        //                haveBoard = false;
        //                break;
        //            }
        //            Thread.Sleep(1);
        //        }
        //        if(haveBoard)
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard = true;
        //        }
        //        else
        //        {
        //            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard = false;
        //        }

        //    }
        //    ConveyorController.Instance.ConveyorStop();
        //    #endregion

        //    ConveyorController.Instance.SetPreSiteLift(false);
        //    ConveyorController.Instance.SetWorkingSiteLift(false);
        //    ConveyorController.Instance.SetFinishedSiteLift(false);

        //    #region 调整板到位
        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetPreSiteStopper(true);
        //    }
        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetWorkingSiteStopper(true);
        //    }
        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetFinishedSiteStopper(true);
        //    }

        //    DateTime stopperDelayTime = DateTime.Now;
        //    while (DateTime.Now - stopperDelayTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).StopperUpDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }

        //    ConveyorController.Instance.ConveyorForward();

        //    DateTime startArriveTime = DateTime.Now;
        //    while (DateTime.Now - startArriveTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).WorkingSitePrm.BoardArrivedDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }

        //    ConveyorController.Instance.ConveyorStop();

        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetPreSiteLift(true);
        //    }
        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetWorkingSiteLift(true);
        //    }
        //    if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard)
        //    {
        //        ConveyorController.Instance.SetFinishedSiteLift(true);
        //    }

        //    DateTime startLiftTime = DateTime.Now;
        //    while (DateTime.Now - startLiftTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
        //            FindBy(this.integralStateMachine.ConveyorNo).LiftUpDelay))
        //    {
        //        if (this.taskIsBreak)
        //        {
        //            return;
        //        }
        //    }
        //    #endregion

        //    this.checkIsDone = true;
        //}


        /// <summary>
        /// 三站式的轨道检查,需要人工处理
        /// </summary>  
        private void CheckTripleSite()
        {
            if (ConveyorController.Instance.PreSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.PreSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.WorkingSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.FinishedSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.FinishedSiteExitSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
            {
                this.isStuck = true;
            }
            else
            {
                ConveyorController.Instance.ConveyorBack(this.integralStateMachine.ConveyorNo);

                DateTime startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
                    FindBy(this.integralStateMachine.ConveyorNo).CheckTime))
                {
                    if (this.taskIsBreak)
                    {
                        return;
                    }
                    if (ConveyorController.Instance.PreSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                       || ConveyorController.Instance.PreSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                       || ConveyorController.Instance.WorkingSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                       || ConveyorController.Instance.FinishedSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                       || ConveyorController.Instance.FinishedSiteExitSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
                    {
                        ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
                        this.isStuck = true;
                        return;
                    }
                    Thread.Sleep(2);
                }

            }
            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
            this.checkIsDone = true;
        }

        /// <summary>
        /// 单站式的检查逻辑
        /// </summary>
        private void CheckSingleSite()
        {
            if (ConveyorController.Instance.SingleSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.SingleSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                || ConveyorController.Instance.SingleSiteExitSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
            {
                this.isStuck = true;
            }
            else
            {
                ConveyorController.Instance.ConveyorBack(this.integralStateMachine.ConveyorNo);

                DateTime startTime = DateTime.Now;
                while(DateTime.Now-startTime<TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
                    FindBy(this.integralStateMachine.ConveyorNo).CheckTime))
                {
                    if (this.taskIsBreak)
                    {
                        return;
                    }
                    if (ConveyorController.Instance.SingleSiteEnterSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                        || ConveyorController.Instance.SingleSiteArriveSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High)
                        || ConveyorController.Instance.SingleSiteExitSensor(this.integralStateMachine.ConveyorNo).Is(StsType.High))
                    {
                        ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
                        this.isStuck = true;
                        return;
                    }
                    Thread.Sleep(2);
                }

            }
            ConveyorController.Instance.ConveyorAbortStop(this.integralStateMachine.ConveyorNo);
            this.checkIsDone = true;
        }
    }
}
