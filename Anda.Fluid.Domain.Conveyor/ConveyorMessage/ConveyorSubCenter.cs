using Anda.Fluid.Domain.Conveyor.ConveyorAction;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorMessage
{
    public class ConveyorSubCenter
    {
        public  void SendMessage()
        {
            
        }
        public void ReciveMessage(object message)
        {
            if (message.GetType() == typeof(ConveyorControlMsg))
            {
                this.DisposeFromControl((ConveyorControlMsg)message);
            }
            else if (message.GetType() == typeof(FluProgramMsg))
            {
                FluProgramMsg msg = (FluProgramMsg)message;

                if (msg == FluProgramMsg.轨道状态机启动)
                {
                    ConveyorMgr.Instance.Setup();
                }
                else if (msg == FluProgramMsg.轨道状态机停止)
                {
                    ConveyorMgr.Instance.Unload();
                }
                else if (msg == FluProgramMsg.进入轨道参数界面)
                {
                    new ConveyorSettingForm(2).ShowDialog();
                }

                else
                {
                    switch (ConveyorMsgCenter.Instance.ConveyorState)
                    {
                        case ConveyorControlMsg.轨道1启用:
                            this.DisposeFromFluProgram(msg, 0);
                            break;
                        case ConveyorControlMsg.轨道2启用:
                            this.DisposeFromFluProgram(msg, 1);
                            break;
                        case ConveyorControlMsg.轨道1和轨道2同时启用:
                            this.DisposeFromFluProgram(msg, 0);
                            this.DisposeFromFluProgram(msg, 1);
                            break;
                    }
                }

            }
        }
        private void DisposeFromFluProgram(FluProgramMsg message, int conveyorNo)
        {
            switch (message)
            {
                case FluProgramMsg.进入编程界面:
                    FlagBitMgr.Instance.FindBy(0).UILevel.EnterEditForm = true;
                    FlagBitMgr.Instance.FindBy(1).UILevel.EnterEditForm = true;
                    break;
                case FluProgramMsg.退出编程界面:
                    FlagBitMgr.Instance.FindBy(0).UILevel.ExitEditForm = true;
                    FlagBitMgr.Instance.FindBy(1).UILevel.ExitEditForm = true;
                    break;
                case FluProgramMsg.启动按钮按下:
                    if (FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode == UILevel.RunMode.Auto)
                    {
                        FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.AutoRun = true;
                    }
                    if (FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode == UILevel.RunMode.Demo)
                    {
                        FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.DemoRun = true;
                    }
                    if (FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
                    {
                        FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.PassRun = true;
                    }
                    break;
                case FluProgramMsg.停止按钮按下:
                    FlagBitMgr.Instance.FindBy(0).UILevel.Terminate = true;
                    FlagBitMgr.Instance.FindBy(1).UILevel.Terminate = true;
                    break;
                case FluProgramMsg.轨道1点胶动作开始:
                    FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.DispenseStart = true;
                    break;
                case FluProgramMsg.轨道1点胶完成:
                    FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.DispenseDone = true;
                    break;
                case FluProgramMsg.轨道2点胶动作开始:
                    FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.DispenseStart = true;
                    break;
                case FluProgramMsg.轨道2点胶完成:
                    FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.DispenseDone = true;
                    break;
                case FluProgramMsg.轨道1手动进板:
                    new ManualEnterBoard().Execute(0);
                    break;
                case FluProgramMsg.轨道1手动出板:
                    new ManualOutBoard().Execute(0);
                    break;
                case FluProgramMsg.轨道1手动SMEMA进板:
                    new ManualSMEMAEnterBoard().Execute(0);
                    break;
                case FluProgramMsg.轨道1手动SMEMA出板:
                    new ManualSMEMAOutBoard().Execute(0);
                    break;
                case FluProgramMsg.轨道2手动进板:
                    new ManualEnterBoard().Execute(1);
                    break;
                case FluProgramMsg.轨道2手动出板:
                    new ManualOutBoard().Execute(1);
                    break;
                case FluProgramMsg.轨道2手动SMEMA进板:
                    new ManualSMEMAEnterBoard().Execute(1);
                    break;
                case FluProgramMsg.轨道2手动SMEMA出板:
                    new ManualSMEMAOutBoard().Execute(1);
                    break;
            }
        }
        private void DisposeFromControl(ConveyorControlMsg message)
        {
            switch (message)
            {
                case ConveyorControlMsg.轨道1启用:
                    ConveyorMsgCenter.Instance.ConveyorState = ConveyorControlMsg.轨道1启用;
                    break;
                case ConveyorControlMsg.轨道2启用:
                    ConveyorMsgCenter.Instance.ConveyorState = ConveyorControlMsg.轨道2启用;
                    break;
                case ConveyorControlMsg.轨道1和轨道2同时启用:
                    ConveyorMsgCenter.Instance.ConveyorState = ConveyorControlMsg.轨道1和轨道2同时启用;
                    break;
                case ConveyorControlMsg.轨道1和轨道2都不启用:
                    ConveyorMsgCenter.Instance.ConveyorState = ConveyorControlMsg.轨道1和轨道2都不启用;
                    break;
                case ConveyorControlMsg.轨道1卡板解决:
                    FlagBitMgr.Instance.FindBy(0).ModelLevel.Auto.StuckIsSolve = true;
                    break;
                case ConveyorControlMsg.轨道2卡板解决:
                    FlagBitMgr.Instance.FindBy(1).ModelLevel.Auto.StuckIsSolve = true;
                    break;
                case ConveyorControlMsg.下层轨道启用:
                    FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart = true;
                    break;
                case ConveyorControlMsg.下层轨道停用:
                    FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart = false;
                    break;
                case ConveyorControlMsg.轨道1手动SMEMA进板:
                    new ManualSMEMAEnterBoard().Execute(0);
                    break;
                case ConveyorControlMsg.轨道1手动SMEMA出板:
                    new ManualSMEMAOutBoard().Execute(0);
                    break;
                case ConveyorControlMsg.轨道2手动SMEMA进板:
                    new ManualSMEMAEnterBoard().Execute(1);
                    break;
                case ConveyorControlMsg.轨道2手动SMEMA出板:
                    new ManualSMEMAOutBoard().Execute(1);
                    break;
            }
        }
    }

}
