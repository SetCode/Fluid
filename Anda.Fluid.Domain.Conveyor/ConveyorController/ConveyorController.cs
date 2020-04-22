using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.Utils;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Conveyor.LeadShine;
using Anda.Fluid.Drive.Conveyor.LeadShine.IO;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.SetupIO;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor
{
    /// <summary>
    /// 轨道控制器，进行相关的IO操作
    /// </summary>
    public class ConveyorController
    {       
        private readonly static ConveyorController instance = new ConveyorController();
        private ConveyorController() { }
        public static ConveyorController Instance => instance;

        /// <summary>
        /// 初始化板卡
        /// </summary>
        public void ConveyorInit()
        {
            ConveyorMachine.Instance.Init();
        }

        /// <summary>
        /// 关闭雷塞卡
        /// </summary>
        public void LeadShineClose()
        {
            ConveyorMachine.Instance.Close();
        }


        /// <summary>
        /// 电机正转,传入参数0为Conveyor1,1为Conveyor2
        /// </summary>
        public void ConveyorForward(int conveyorId)
        {
            if (FlagBitMgr.Instance.FindBy(conveyorId).SubSitesLevel.FinishedSite.usingConveyor)
                return;
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    this.MotorForward(conveyorId);
                    break;
                case BoardDirection.RightToLeft:
                    this.MotorBack(conveyorId);
                    break;
                case BoardDirection.LeftToLeft:
                    this.MotorForward(conveyorId);
                    break;
                case BoardDirection.RightToRight:
                    this.MotorBack(conveyorId);
                    break;
            }
        }

        /// <summary>
        /// 电机反转
        /// </summary>
        public void ConveyorBack(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    this.MotorBack(conveyorId);
                    break;
                case BoardDirection.RightToLeft:
                    this.MotorForward(conveyorId);
                    break;
                case BoardDirection.LeftToLeft:
                    this.MotorBack(conveyorId);
                    break;
                case BoardDirection.RightToRight:
                    this.MotorForward(conveyorId);
                    break;
            }
        }

        /// <summary>
        /// 保温站停止轨道
        /// </summary>
        /// <param name="conveyorId"></param>
        public void FinishedSiteStopConveyor(int conveyorId)
        {
            //暂停所有计时器
            ConveyorTimerMgr.Instance.PauseAll();
            //直接停止轨道
            this.MotorStop(conveyorId);
        }

        /// <summary>
        /// 保温站复位轨道
        /// </summary>
        /// <param name="conveyorId"></param>
        public void FinishedSiteResetConveyor(int conveyorId)
        {
            //启动所有计时器
            ConveyorTimerMgr.Instance.StartAll();
            //如果预热站在保温站停止轨道前有在使用轨道，则重启轨道
            if (FlagBitMgr.Instance.FindBy(conveyorId).SubSitesLevel.PreSite.usingConveyor)
            {
                this.ConveyorForward(conveyorId);
            }

        }

        /// <summary>
        /// 会根据其他工站来自动决定是否停止轨道
        /// </summary>
        public void ConveyorStop(int conveyorId)
        {
            if(FlagBitMgr.Instance.FindBy(conveyorId).SubSitesLevel.PreSite.usingConveyor == false
                && FlagBitMgr.Instance.FindBy(conveyorId).SubSitesLevel.FinishedSite.usingConveyor == false)
            {
                this.MotorStop(conveyorId);             
            }            
        }

        /// <summary>
        /// 直接停止轨道
        /// </summary>
        public void ConveyorAbortStop(int conveyorId)
        {
            this.MotorStop(conveyorId);
        }

        /// <summary>
        /// 控制预热站阻挡气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetPreSiteStopper(int conveyorId,bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    this.SetStopper(conveyorId, siteName.PreSite, b);
                    break;
                case BoardDirection.RightToLeft:
                    this.SetStopper(conveyorId, siteName.FinisheSite, b);
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
        }

        /// <summary>
        /// 控制工作站阻挡气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetWorkingSiteStopper( int conveyorId,bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }

                switch (boardDirection)
                {
                    case BoardDirection.LeftToRight:
                        this.SetStopper(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.RightToLeft:
                        this.SetStopper(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.LeftToLeft:
                        this.SetStopper(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.RightToRight:
                        this.SetStopper(conveyorId, siteName.WorkingSite, b);
                        break;
                }
        }

        /// <summary>
        /// 控制成品站阻挡气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetFinishedSiteStopper(int conveyorId, bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }

                switch (boardDirection)
                {
                    case BoardDirection.LeftToRight:
                        this.SetStopper(conveyorId, siteName.FinisheSite, b);
                        break;
                    case BoardDirection.RightToLeft:
                        this.SetStopper(conveyorId, siteName.PreSite, b);
                        break;
                    case BoardDirection.LeftToLeft:
                        break;
                    case BoardDirection.RightToRight:
                        break;
                }

        }

        /// <summary>
        /// 控制预热站顶升气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetPreSiteLift(int conveyorId, bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }

                switch (boardDirection)
                {
                    case BoardDirection.LeftToRight:
                        this.SetLift(conveyorId, siteName.PreSite, b);
                        break;
                    case BoardDirection.RightToLeft:
                        this.SetLift(conveyorId, siteName.FinisheSite, b);
                        break;
                    case BoardDirection.LeftToLeft:
                        break;
                    case BoardDirection.RightToRight:
                        break;
                }

        }

        /// <summary>
        /// 控制工作站顶升气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetWorkingSiteLift(int conveyorId, bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }

                switch (boardDirection)
                {
                    case BoardDirection.LeftToRight:
                        this.SetLift(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.RightToLeft:
                        this.SetLift(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.LeftToLeft:
                        this.SetLift(conveyorId, siteName.WorkingSite, b);
                        break;
                    case BoardDirection.RightToRight:
                        this.SetLift(conveyorId, siteName.WorkingSite, b);
                        break;
                }

        }

        /// <summary>
        /// 控制成品站顶升气缸
        /// </summary>
        /// <param name="b"></param>
        public void SetFinishedSiteLift(int conveyorId, bool b)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }

                switch (boardDirection)
                {
                    case BoardDirection.LeftToRight:
                        this.SetLift(conveyorId, siteName.FinisheSite, b);
                        break;
                    case BoardDirection.RightToLeft:
                        this.SetLift(conveyorId, siteName.PreSite, b);
                        break;
                    case BoardDirection.LeftToLeft:
                        break;
                    case BoardDirection.RightToRight:
                        break;
                }

        }

        /// <summary>
        /// 控制预热站吹气
        /// </summary>
        /// <param name="b"></param>
        public void SetPreSiteBlow( bool b)
        {
            switch (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection)
            {
                case BoardDirection.LeftToRight:
                    break;
                case BoardDirection.RightToLeft:
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
        }

        /// <summary>
        /// 控制工作站吹气
        /// </summary>
        /// <param name="b"></param>
        public void SetWorkingSiteBlow(bool b)
        {
            switch (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection)
            {
                case BoardDirection.LeftToRight:

                    break;
                case BoardDirection.RightToLeft:

                    break;
                case BoardDirection.LeftToLeft:

                    break;
                case BoardDirection.RightToRight:

                    break;
            }
        }

        /// <summary>
        /// 控制成品站吹气
        /// </summary>
        /// <param name="b"></param>
        public void SetFinishedSiteBlow(bool b)
        {
            switch (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection)
            {
                case BoardDirection.LeftToRight:
                    break;
                case BoardDirection.RightToLeft:
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
        }

        /// <summary>
        /// 向上游设备发送求板信号
        /// </summary>
        /// <param name="b"></param>
        public void AskSignalling(int conveyorId, bool b)
        {
            this.SendSignal(conveyorId, streamName.UpStream, b);
        }

        /// <summary>
        /// 向下游设备发送有板信号
        /// </summary>
        /// <param name="b"></param>
        public void InStoreSignalling(int conveyorId, bool b)
        {
            this.SendSignal(conveyorId, streamName.DownStream, b);
        }
       
        /// <summary>
        /// 查看上游设备放板信号状态
        /// </summary>
        /// <returns></returns>
        public Sts UpstreamPutBoard(int conveyorId)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    return this.GetAD19InputSts(DiEnum.轨道1上游设备有板);
                }
                else
                {
                    return this.GetAD19InputSts(DiEnum.轨道2上游设备有板);
                }
            }
            else
            {
                if (conveyorId == 0)
                    return DiType.前设备放板信号.Sts();
                else
                    return DiType.轨道2前设备放板信号.Sts();
            }           
        }

        /// <summary>
        /// 查看下游设备求板信号状态
        /// </summary>
        /// <returns></returns>
        public Sts DownstreamAskBoard(int conveyorId)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    return this.GetAD19InputSts(DiEnum.轨道1下游设备求板);
                }
                else
                {
                    return this.GetAD19InputSts(DiEnum.轨道2下游设备求板);
                }
            }
            else
            {
                if (conveyorId == 0)
                    return DiType.后设备求板信号.Sts();
                else
                    return DiType.轨道2后设备求板信号.Sts();
            }
            
        }

        /// <summary>
        /// 查看预热站进板感应电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts PreSiteEnterSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1进板感应);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2进板感应);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.进板检测.Sts();
                        else
                            sts = DiType.轨道2进板检测.Sts();
                    }
                    
                    break;
                case BoardDirection.RightToLeft:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1出板感应);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2出板感应);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.出板检测.Sts();
                        else
                            sts = DiType.轨道2出板检测.Sts();
                    }                  
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
            return sts;
        }

        /// <summary>
        /// 查看预热站到位感应电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts PreSiteArriveSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1预热站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2预热站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.定位1检测.Sts();
                        else
                            sts = DiType.轨道2定位1检测.Sts();
                    }
                    break;
                case BoardDirection.RightToLeft:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1成品站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2成品站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.定位2检测.Sts();
                        else
                            sts = DiType.轨道2定位2检测.Sts();
                    }                  
                    break;
                case BoardDirection.LeftToLeft:              
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
            return sts;
        }

        /// <summary>
        /// 查看工作站到位感应电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts WorkingSiteArriveSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1工作站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2工作站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.定位2检测.Sts();
                        else
                            sts = DiType.轨道2定位2检测.Sts();
                    }                   
                    break;
                case BoardDirection.RightToLeft:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1工作站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2工作站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.定位1检测.Sts();
                        else
                            sts = DiType.轨道2定位1检测.Sts();
                    }                  
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
            return sts;
        }

        /// <summary>
        /// 查看成品站到位感应电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts FinishedSiteArriveSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1成品站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2成品站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.出板检测.Sts();
                        else
                            sts = DiType.轨道2出板检测.Sts();
                    }                 
                    break;
                case BoardDirection.RightToLeft:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1预热站到位);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2预热站到位);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.进板检测.Sts();
                        else
                            sts = DiType.轨道2进板检测.Sts();
                    }                  
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
            return sts;
        }

        /// <summary>
        /// 查看成品站出板感应电眼到位状态
        /// </summary>
        /// <returns></returns>
        public Sts FinishedSiteExitSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            switch (boardDirection)
            {
                case BoardDirection.LeftToRight:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1出板感应);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2出板感应);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.出板检测.Sts();
                        else
                            sts = DiType.轨道2出板检测.Sts();
                    }                    
                    break;
                case BoardDirection.RightToLeft:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道1进板感应);
                        }
                        else
                        {
                            sts = this.GetAD19InputSts(DiEnum.轨道2进板感应);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            sts = DiType.进板检测.Sts();
                        else
                            sts = DiType.轨道2进板检测.Sts();
                    }
                   
                    break;
                case BoardDirection.LeftToLeft:
                    break;
                case BoardDirection.RightToRight:
                    break;
            }
            return sts;
        }

        /// <summary>
        /// 查看单站进板感应电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts SingleSiteEnterSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            if ( boardDirection == BoardDirection.LeftToRight
                || boardDirection == BoardDirection.LeftToLeft)
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    if (conveyorId == 0)
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道1进板感应);
                    }
                    else
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道2进板感应);
                    }
                }
                else
                {
                    if (conveyorId == 0)
                        sts = DiType.进板检测.Sts();
                    else
                        sts = DiType.轨道2进板检测.Sts();
                }
            }
            else
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    if (conveyorId == 0)
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道1出板感应);
                    }
                    else
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道2出板感应);
                    }
                }
                else
                {
                    if (conveyorId == 0)
                        sts = DiType.出板检测.Sts();
                    else
                        sts = DiType.轨道2出板检测.Sts();
                }
            }
           
            return sts;
        }

        /// <summary>
        /// 查看单站到位电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts SingleSiteArriveSensor(int conveyorId)
        {
            Sts sts = new Sts();
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    sts = this.GetAD19InputSts(DiEnum.轨道1工作站到位);
                }
                else
                {
                    sts = this.GetAD19InputSts(DiEnum.轨道2工作站到位);
                }
            }
            else
            {
                if (conveyorId == 0)
                    if (Machine.Instance.IOSetupable is IOSetupAD16_Gts4_单阀双轨)
                    {
                        if (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection == BoardDirection.RightToLeft)
                        {
                            sts = DiType.轨道1右到左定位2检测.Sts();
                        }
                        else
                        {
                            sts = DiType.定位2检测.Sts();
                        }
                    }
                    else
                    {
                        sts = DiType.定位2检测.Sts();
                    }

                else
                {
                    if (Machine.Instance.IOSetupable is IOSetupAD16_Gts4_单阀双轨)
                    {
                        if (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection == BoardDirection.RightToLeft)
                        {
                            sts = DiType.轨道2右到左定位2检测.Sts();
                        }
                        else
                        {
                            sts = DiType.轨道2定位2检测.Sts();
                        }
                    }
                    else
                    {
                        sts = DiType.轨道2定位2检测.Sts();
                    }
                }
            }

            return sts;
        }

        /// <summary>
        /// 查看单站出板电眼状态
        /// </summary>
        /// <returns></returns>
        public Sts SingleSiteExitSensor(int conveyorId)
        {
            BoardDirection boardDirection;
            if (conveyorId == 0)
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(0).BoardDirection;
            }
            else
            {
                boardDirection = ConveyorPrmMgr.Instance.FindBy(1).BoardDirection;
            }
            Sts sts = new Sts();
            if (boardDirection == BoardDirection.LeftToRight
                || boardDirection == BoardDirection.RightToRight)
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    if (conveyorId == 0)
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道1出板感应);
                    }
                    else
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道2出板感应);
                    }
                }
                else
                {
                    if (conveyorId == 0)
                        sts = DiType.出板检测.Sts();
                    else
                        sts = DiType.轨道2出板检测.Sts();
                }
            }
            else
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    if (conveyorId == 0)
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道1进板感应);
                    }
                    else
                    {
                        sts = this.GetAD19InputSts(DiEnum.轨道2进板感应);
                    }
                }
                else
                {
                    if (conveyorId == 0)
                        sts = DiType.进板检测.Sts();
                    else
                        sts = DiType.轨道2进板检测.Sts();
                }
            }
            return sts;
        }

        /// <summary>
        /// 复位所有的顶升和阻挡气缸
        /// </summary>
        public void ResetAllCylinder()
        {
            this.SetPreSiteStopper(0,false);
            this.SetPreSiteStopper(1, false);
            this.SetPreSiteLift(0,false);
            this.SetPreSiteLift(1, false);
            this.SetWorkingSiteStopper(0,false);
            this.SetWorkingSiteStopper(1,false);
            this.SetWorkingSiteLift(0,false);
            this.SetWorkingSiteLift(1, false);
            this.SetFinishedSiteStopper(0,false);
            this.SetFinishedSiteStopper(1, false);
            this.SetFinishedSiteLift(0,false);
            this.SetFinishedSiteLift(1, false);


            try
            {
                DoType.轨道1右到左工作阻挡.Set(false);
                DoType.轨道2右到左工作阻挡.Set(false);
                DoType.工作阻挡.Set(false);
                DoType.轨道2工作阻挡.Set(false);
            }
            catch (Exception)
            {
            }
        }



        // 此处的操作都是以轨道左进右出为前提

        /// <summary>
        /// 轨道电机正转
        /// </summary>
        private void MotorForward(int conveyorId)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    ConveyorMachine.Instance.Conveyor1JogForwardMove(ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
                }
                else
                {
                    ConveyorMachine.Instance.Conveyor2JogForwardMove(ConveyorPrmMgr.Instance.FindBy(1).Speed, ConveyorPrmMgr.Instance.FindBy(1).AccTime);
                }
            }
            else if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                AxisType.Axis5.MoveJog(ConveyorPrmMgr.Instance.FindBy(0).Speed);
            }
            else
            {
                if (conveyorId == 0)
                    DoType.运输正转.Set(true);
                else
                    DoType.轨道2运输正转.Set(true);
            }
        }

        /// <summary>
        /// 轨道电机反转
        /// </summary>
        /// <param name="conveyorId"></param>
        private void MotorBack(int conveyorId)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    ConveyorMachine.Instance.Conveyor1JogBackMove(ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
                }
                else
                {
                    ConveyorMachine.Instance.Conveyor2JogBackMove(ConveyorPrmMgr.Instance.FindBy(1).Speed, ConveyorPrmMgr.Instance.FindBy(1).AccTime);
                }
            }
            else if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                AxisType.Axis5.MoveJog(-ConveyorPrmMgr.Instance.FindBy(0).Speed);
            }
            else
            {
                if (conveyorId == 0)
                {
                    DoType.运输反转.Set(true);
                    Thread.Sleep(30);
                    DoType.运输正转.Set(true);
                }
                else
                {
                    DoType.轨道2运输反转.Set(true);
                    Thread.Sleep(30);
                    DoType.轨道2运输正转.Set(true);
                }
            }
        }

        /// <summary>
        /// 轨道电机停止
        /// </summary>
        /// <param name="conveyorId"></param>
        private void MotorStop(int conveyorId)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (conveyorId == 0)
                {
                    ConveyorMachine.Instance.Conveyor1Stop();
                }
                else
                {
                    ConveyorMachine.Instance.Conveyor2Stop();
                }
            }
            else if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                AxisType.Axis5.MoveStop();
            }
            else
            {
                if (conveyorId == 0)
                {
                    DoType.运输正转.Set(false);
                    DoType.运输反转.Set(false);
                }
                else
                {
                    DoType.轨道2运输正转.Set(false);
                    DoType.轨道2运输反转.Set(false);
                }    
            }
        }

        /// <summary>
        /// 阻挡气缸输出
        /// </summary>
        /// <param name="conveyorId"></param>
        /// <param name="siteName"></param>
        /// <param name="b"></param>
        private void SetStopper(int conveyorId, siteName siteName, bool b)
        {
            switch (siteName)
            {
                case siteName.PreSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1预热站阻挡, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2预热站阻挡, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            DoType.预热阻挡.Set(b);
                        else
                            DoType.轨道2预热阻挡.Set(b);
                    }
                    break;
                case siteName.WorkingSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1工作站阻挡, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2工作站阻挡, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                        {
                            if (Machine.Instance.IOSetupable is IOSetupAD16_Gts4_单阀双轨)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(0).BoardDirection ==             BoardDirection.RightToLeft)
                                {
                                    DoType.轨道1右到左工作阻挡.Set(b);
                                }
                                else
                                {
                                    DoType.工作阻挡.Set(b);
                                }
                            }
                            else
                            {
                                DoType.工作阻挡.Set(b);
                            }
                        }                           
                        else
                        {
                            if (Machine.Instance.IOSetupable is IOSetupAD16_Gts4_单阀双轨)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(1).BoardDirection == BoardDirection.RightToLeft)
                                {
                                    DoType.轨道2右到左工作阻挡.Set(b);
                                }
                                else
                                {
                                    DoType.轨道2工作阻挡.Set(b);
                                }
                            }
                            else
                            {
                                DoType.轨道2工作阻挡.Set(b);
                            }
                        }
                    }
                    break;
                case siteName.FinisheSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1成品站阻挡, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2成品站阻挡, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            DoType.出板阻挡.Set(b);
                        else
                            DoType.轨道2出板阻挡.Set(b);
                    }
                    break;
            }
        }

        /// <summary>
        /// 顶升气缸输出
        /// </summary>
        /// <param name="conveyorId"></param>
        /// <param name="siteName"></param>
        /// <param name="b"></param>
        private void SetLift(int conveyorId, siteName siteName, bool b)
        {
            switch (siteName)
            {
                case siteName.PreSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1预热站顶升, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2预热站顶升, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            DoType.预热顶升.Set(b);
                        else
                            DoType.轨道2预热顶升.Set(b);
                    }
                    break;
                case siteName.WorkingSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1工作站顶升, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2工作站顶升, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            DoType.工作顶升.Set(b);
                        else
                            DoType.轨道2工作顶升.Set(b);
                    }
                    break;
                case siteName.FinisheSite:
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道1成品站顶升, b);
                        }
                        else
                        {
                            ConveyorMachine.Instance.SetDo(DoEnum.轨道2成品站顶升, b);
                        }
                    }
                    else
                    {
                        if (conveyorId == 0)
                            DoType.出板顶升.Set(b);
                        else
                            DoType.轨道2出板顶升.Set(b);
                    }
                    break;
            }
        }

        /// <summary>
        /// 向外围设备发出信号
        /// </summary>
        /// <param name="conveyorId"></param>
        /// <param name="streamName"></param>
        /// <param name="b"></param>
        private void SendSignal(int conveyorId,streamName streamName,bool b)
        {
            switch (streamName)
            {
                case streamName.UpStream:
                    #region AD19发出信号
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            if (ConveyorPrmMgr.Instance.FindBy(0).SMEMAIsPulse)
                            {
                                Task.Factory.StartNew(new Action(() =>
                                {
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道1求板, true);
                                    Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).PulseTime);
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道1求板, false);
                                }));
                            }
                            else
                            {
                                ConveyorMachine.Instance.SetDo(DoEnum.轨道1求板, true);
                            }
                        }
                        else
                        {
                            if (ConveyorPrmMgr.Instance.FindBy(1).SMEMAIsPulse)
                            {
                                Task.Factory.StartNew(new Action(() =>
                                {
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道2求板, true);
                                    Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(1).PulseTime);
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道2求板, false);
                                }));
                            }
                            else
                            {
                                ConveyorMachine.Instance.SetDo(DoEnum.轨道2求板, true);
                            }
                        }
                    }
                    #endregion
                    #region 其余型号机台发出信号
                    else
                    {
                        if (conveyorId == 0)
                        {
                            if (b)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(0).SMEMAIsPulse)
                                {
                                    Task.Factory.StartNew(new Action(() =>
                                    {
                                        DoType.求板信号.Set(true);
                                        Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).PulseTime);
                                        DoType.求板信号.Set(false);
                                    }));
                                }
                                else
                                {
                                    DoType.求板信号.Set(true);
                                }
                            }
                            else
                            {
                                DoType.求板信号.Set(false);
                            }
                        }
                        else
                        {
                            if (b)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(1).SMEMAIsPulse)
                                {
                                    Task.Factory.StartNew(new Action(() =>
                                    {
                                        DoType.轨道2求板信号.Set(true);
                                        Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(1).PulseTime);
                                        DoType.轨道2求板信号.Set(false);
                                    }));
                                }
                                else
                                {
                                    DoType.轨道2求板信号.Set(true);
                                }
                            }
                            else
                            {
                                DoType.轨道2求板信号.Set(false);
                            }
                        }
                    }
                #endregion
                    break;
                case streamName.DownStream:
                    #region AD19发出信号
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                    {
                        if (conveyorId == 0)
                        {
                            if (ConveyorPrmMgr.Instance.FindBy(0).SMEMAIsPulse)
                            {
                                Task.Factory.StartNew(new Action(() =>
                                {
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道1放板, true);
                                    Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).PulseTime);
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道1放板, false);
                                }));
                            }
                            else
                            {
                                ConveyorMachine.Instance.SetDo(DoEnum.轨道1放板, true);
                            }
                        }
                        else
                        {
                            if (ConveyorPrmMgr.Instance.FindBy(1).SMEMAIsPulse)
                            {
                                Task.Factory.StartNew(new Action(() =>
                                {
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道2放板, true);
                                    Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(1).PulseTime);
                                    ConveyorMachine.Instance.SetDo(DoEnum.轨道2放板, false);
                                }));
                            }
                            else
                            {
                                ConveyorMachine.Instance.SetDo(DoEnum.轨道2放板, true);
                            }
                        }
                    }
                    #endregion
                    #region 其余型号机台发出信号
                    else
                    {
                        if (conveyorId == 0)
                        {
                            if (b)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(0).SMEMAIsPulse)
                                {
                                    Task.Factory.StartNew(new Action(() =>
                                    {
                                        DoType.放板信号.Set(true);
                                        Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).PulseTime);
                                        DoType.放板信号.Set(false);
                                    }));
                                }
                                else
                                {
                                    DoType.放板信号.Set(true);
                                }
                            }
                            else
                            {
                                DoType.放板信号.Set(false);
                            }
                        }
                        else
                        {
                            if (b)
                            {
                                if (ConveyorPrmMgr.Instance.FindBy(1).SMEMAIsPulse)
                                {
                                    Task.Factory.StartNew(new Action(() =>
                                    {
                                        DoType.轨道2放板信号.Set(true);
                                        Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(1).PulseTime);
                                        DoType.轨道2放板信号.Set(false);
                                    }));
                                }
                                else
                                {
                                    DoType.轨道2放板信号.Set(true);
                                }
                            }
                            else
                            {
                                DoType.轨道2放板信号.Set(false);
                            }
                        }
                    }
                    #endregion
                    break;
            }
        }


        internal Sts GetAD19InputSts(DiEnum diName)
        {
            if (ConveyorMachine.Instance.InquireDiSts(diName) == IOSts.High)
            {
                Sts sts = new Sts();
                sts.Update(true);
                sts.Update(true);
                return sts;
            }
            else if (ConveyorMachine.Instance.InquireDiSts(diName) == IOSts.Low)
            {
                Sts sts = new Sts();
                sts.Update(false);
                sts.Update(false);
                return sts;
            }
            else if(ConveyorMachine.Instance.InquireDiSts(diName) == IOSts.IsRising)
            {
                Sts sts = new Sts();
                sts.Update(false);
                sts.Update(true);
                return sts;
            }
            else
            {
                Sts sts = new Sts();
                sts.Update(true);
                sts.Update(false);
                return sts;
            }
        }

        internal Sts GetAD19OutputSts(DoEnum doName)
        {
            if (ConveyorMachine.Instance.GetDoSts(doName) == IOSts.High)
            {
                Sts sts = new Sts();
                sts.Update(true);
                sts.Update(true);
                return sts;
            }
            else 
            {
                Sts sts = new Sts();
                sts.Update(false);
                sts.Update(false);
                return sts;
            }
        }

        private enum siteName
        {
            PreSite,
            WorkingSite,
            FinisheSite,
        }

        private enum streamName
        {
            UpStream,
            DownStream
        }
    }

}
