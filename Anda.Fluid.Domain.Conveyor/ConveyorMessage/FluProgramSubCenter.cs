using Anda.Fluid.Domain.Conveyor.ConveyorState;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Anda.Fluid.Domain.Conveyor.Flag.UILevel;

namespace Anda.Fluid.Domain.Conveyor.ConveyorMessage
{
    public class FluProgramSubCenter
    {
        public void ReciveMessage(object message)
        {
            
        }

        public void SendMessage(FluProgramMsg message)
        {
            ConveyorMsgCenter.Instance.PassMessage(this,message);
        }

        /// <summary>
        /// 告诉轨道总的板数量
        /// </summary>
        /// <param name="BoardCount"></param>
        public void SendBoardCount(int BoardCount)
        {
            FlagBitMgr.Instance.BoardCounts = BoardCount;
        }

        /// <summary>
        /// 告诉轨道该程序对应的轨道宽度
        /// </summary>
        /// <param name="width"></param>
        public void SendConveyorWidth(double width)
        {
            FlagBitMgr.Instance.ConveyorWidth = width;
        }

        /// <summary>
        /// 询问轨道板是否到位
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Board1IsExist"></param>
        /// <param name="Board2IsExist"></param>
        public Tuple<bool, bool> IsBoardIn()
        {
            bool Board1IsExist, Board2IsExist;
            if (ConveyorPrmMgr.Instance.FindBy(0).SubsiteMode == ConveyorSubsiteMode.Singel)
            {
                if (FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.准备点胶)
                {
                    Board1IsExist = true;
                }
                else
                {
                    Board1IsExist = false;
                }
            }
            else
            {
                if(FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.准备点胶)
                {
                    Board1IsExist = true;
                }
                else
                {
                    Board1IsExist = false;
                }
            }

            if (ConveyorPrmMgr.Instance.FindBy(1).SubsiteMode == ConveyorSubsiteMode.Singel)
            {
                if (FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.准备点胶)
                {
                    Board2IsExist = true;
                }
                else
                {
                    Board2IsExist = false;
                }
            }
            else
            {
                if (FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.准备点胶)
                {
                    Board2IsExist = true;
                }
                else
                {
                    Board2IsExist = false;
                }
            }
            return new Tuple<bool, bool>(Board1IsExist, Board2IsExist);
        }

        /// <summary>
        /// 询问轨道是否是Offline模式,输出一个二维bool数组,数组第一个值代表轨道1，第二个值代表轨道2
        /// </summary>
        /// <param name="message"></param>
        /// <param name="conveyor1Mode"></param>
        public Tuple<bool, bool> IsOffline()
        {
            Logger.DEFAULT.Debug("ConveyorMsgCenter.Instance.Program.IsOffline()  IN before");
            bool[] conveyorIsOffline = new bool[2];
            if(FlagBitMgr.Instance.FindBy(0).State.IntegralState == IntegralStateEnum.Offline模式)
            {
                conveyorIsOffline[0] = true;
            }
            else
            {
                conveyorIsOffline[0] = false;
            }
            if (FlagBitMgr.Instance.FindBy(1).State.IntegralState == IntegralStateEnum.Offline模式)
            {
                conveyorIsOffline[1] = true;
            }
            else
            {
                conveyorIsOffline[1] = false;
            }
            Logger.DEFAULT.Debug("ConveyorMsgCenter.Instance.Program.IsOffline()  IN END");
            return new Tuple<bool, bool>(conveyorIsOffline[0], conveyorIsOffline[1]);
        }

        /// <summary>
        /// 获取两个轨道是否搜扫到条码
        /// </summary>
        /// <returns>返回值true为扫码异常，false为扫码正常</returns>
        public Tuple<bool,bool> GetPreSiteBarcodeError()
        {
            bool conveyor1BarcodeErr, conveyor2BarcodeErr;
            conveyor1BarcodeErr = (FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.PreBarcode.Equals("") && FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.PreSiteHaveBoard);
            conveyor2BarcodeErr = (FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.PreBarcode.Equals("") && FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.PreSiteHaveBoard);
            return new Tuple<bool, bool>(conveyor1BarcodeErr, conveyor2BarcodeErr);
        }

        public bool isConveyorScanBarcode()
        {
            if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1启用)
            {
                return ConveyorPrmMgr.Instance.FindBy(0).ConveyorScan;
            }
            else if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道2启用)
            {
                return ConveyorPrmMgr.Instance.FindBy(1).ConveyorScan;
            }
            else if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1和轨道2同时启用)
            {
                return ConveyorPrmMgr.Instance.FindBy(0).ConveyorScan && ConveyorPrmMgr.Instance.FindBy(1).ConveyorScan;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 返回机台轨道工作站治具条码
        /// </summary>
        /// <returns></returns>
        public Tuple<Tuple<bool,string>,Tuple<bool,string>> GetWorkingSiteBarcode()
        {
            string conveyor1BarcodeStr = FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.CurrentBarcode;
            string conveyor2BarcodeStr = FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.CurrentBarcode;
            Tuple<bool, string> Conveyor1Barcode, Conveyor2Barcode;
            if (!conveyor1BarcodeStr.Equals(""))
            {
                Conveyor1Barcode = new Tuple<bool, string>(true, conveyor1BarcodeStr);
            }
            else
            {
                Conveyor1Barcode = new Tuple<bool, string>(false, "");
            }
            if (!conveyor2BarcodeStr.Equals(""))
            {
                Conveyor2Barcode = new Tuple<bool, string>(true, conveyor2BarcodeStr);
            }
            else
            {
                Conveyor2Barcode = new Tuple<bool, string>(false, "");
            }
            return new Tuple<Tuple<bool, string>, Tuple<bool, string>>(Conveyor1Barcode, Conveyor2Barcode);
        }

    }
    public enum FluProgramMsg
    {
        轨道状态机启动,
        轨道状态机停止,

        进入轨道参数界面,
        进入编程界面,
        退出编程界面,

        启动按钮按下,
        停止按钮按下,

        轨道1点胶动作开始,
        轨道1点胶完成,

        轨道2点胶动作开始,
        轨道2点胶完成,

        轨道1手动进板,
        轨道1手动出板,
        轨道1手动SMEMA进板,
        轨道1手动SMEMA出板,
        轨道2手动进板,
        轨道2手动出板,
        轨道2手动SMEMA进板,
        轨道2手动SMEMA出板
    }
}
