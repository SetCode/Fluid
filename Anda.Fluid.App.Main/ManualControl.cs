using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Domain.Conveyor.Forms;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using System.Threading;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Dialogs.GlueManage;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class ManualControl : UserControl, IMsgSender, IMsgReceiver
    {
        private SensorsReadValueForm sensorsForm;

        public ManualControl()
        {
            InitializeComponent();
            UserControlEx UserControl = new UserControlEx();
            foreach (Control controls in this.Controls)
            {
                controls.MouseMove += UserControl.ReadDisplayTip;
                controls.MouseLeave += UserControl.DisopTip;
            }

        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                this.btnDownConveyorStart.Visible = true;
                this.btnDownConveyorEnd.Visible = true;
            }
            else
            {
                this.btnDownConveyorStart.Visible = false;
                this.btnDownConveyorEnd.Visible = false;
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.TSV300)
                {
                    this.ConveyorIsEnable(false);
                }
            }
            if (msgName == MsgType.IDLE)
            {
                this.btnPurge.Enabled = true;
                this.btnPrime.Enabled = true;
                this.btnScale.Enabled = true;
                this.btnHeatIO.Enabled = true;
                this.btnScanner.Enabled = true;
                this.btnLaser.Enabled = true;
                this.btnConveyor.Enabled = true;
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.TSV300)
                {
                    this.ConveyorIsEnable(false);
                }
                else
                {
                    this.ConveyorIsEnable(true);
                }
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                {
                    this.btnDownConveyorStart.Enabled = true;
                    this.btnDownConveyorEnd.Enabled = true;
                }
            }
            else if (msgName == MsgType.RUNNING || msgName == MsgType.PAUSED || msgName == MsgType.BUSY)
            {
                this.btnPurge.Enabled = false ;
                this.btnPrime.Enabled = false;
                this.btnScale.Enabled = false;
                this.btnHeatIO.Enabled = false;
                this.btnScanner.Enabled = false;
                this.btnLaser.Enabled = false;
                this.ConveyorIsEnable(false);
                if (msgName == MsgType.RUNNING || msgName == MsgType.PAUSED)
                {
                    this.btnConveyor.Enabled = true;
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                    {
                        this.btnDownConveyorStart.Enabled = true;
                        this.btnDownConveyorEnd.Enabled = true;
                    }
                }
                else
                {
                    this.btnConveyor.Enabled = false;
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                    {
                        this.btnDownConveyorStart.Enabled = false;
                        this.btnDownConveyorEnd.Enabled = false;
                    }
                }
                
            }
        }

        public void ConveyorIsEnable(bool isEnable)
        {
            this.btnBoardEnter.Enabled = isEnable;
            this.btnBoardExit.Enabled = isEnable;
            this.btnSMEMAEnter.Enabled = isEnable;
            this.btnSMEMAOut.Enabled = isEnable;
        }

        private async void btnPurge_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.DoPurgeAndPrime();
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                    {
                        Machine.Instance.Robot.MovePosABAndReply(new PointD());
                    }
                    Machine.Instance.Valve2.DoPurgeAndPrime();
                }
            });
            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private async void btnPrime_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.DoPrime();
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                    {
                        Machine.Instance.Robot.MovePosABAndReply(new PointD());
                    }
                    Machine.Instance.Valve2.DoPrime();
                }
            });
            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private async void btnScale_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                bool scaleSts = true;
                if (Machine.Instance.Scale.Scalable.CommunicationOK ==ComCommunicationSts.ERROR)
                {
                    scaleSts = false;
                }
                else
                {
                    scaleSts = Machine.Instance.Scale.Scalable.CommunicationTest();
                }
                if (!scaleSts)
                {
                    //string msg = "Scale is disconnect,Please check scale!";
                    string msg = "天平连接失败，请检查!";
                    MessageBox.Show(msg);
                    return;
                }
                Result ret = Result.OK;
                ret = Machine.Instance.Valve1.AutoRunWeighingWithPurge();
                if (!ret.IsOk)
                {
                    return;
                }
             
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    ret = Machine.Instance.Valve2.AutoRunWeighingWithPurge();
                    if (!ret.IsOk)
                    {
                        return;
                    }
                }
            });
            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private void btnHeatIO_Click(object sender, EventArgs e)
        {
            bool b1 = !DoType.胶枪加热1.Sts().Value;
            DoType.胶枪加热1.Set(b1);

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                DoType.胶枪加热2.Set(b1);

            }

            if (b1)
            {
                this.btnHeatIO.BackColor = SystemColors.Control;
            }
            else
            {
                this.btnHeatIO.BackColor = Color.Gray;
            }

        }

        private void btnConveyor_Click(object sender, EventArgs e)
        {            
            FormMgr.Show<ConveyorControlForm>(this);
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            this.ShowSensorReadForm(0);
        }

        private void btnLaser_Click(object sender, EventArgs e)
        {
            this.ShowSensorReadForm(1);
        }

        private void ShowSensorReadForm(int type)
        {
            if (this.sensorsForm == null)
            {
                this.sensorsForm = new SensorsReadValueForm(type);
            }
            else
            {
                if (this.sensorsForm.Visible)
                {
                    return;
                }
                this.sensorsForm = new SensorsReadValueForm(type);
            }
            this.sensorsForm.TopMost = true;
            this.sensorsForm.Show(this);
        }

        private void btnBoardEnter_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) 
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动进板);
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动进板);
            }
        }

        private void btnBoardExit_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动出板);
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动出板);
            }
        }

        private void btnGlueManage_Click(object sender, EventArgs e)
        {
            FormMgr.Show<GlueManageForm>(this);
        }

        private void btnDownConveyorStart_Click(object sender, EventArgs e)
        {
            ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.下层轨道启用);
        }

        private void btnDownConveyorEnd_Click(object sender, EventArgs e)
        {
            ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.下层轨道停用);
        }

        private void btnSMEMAEnter_Click(object sender, EventArgs e)
        {
            int conveyorNo = 0;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                conveyorNo = 1;
            }
            if (conveyorNo == 0)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动SMEMA进板);
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动SMEMA进板);
            }
        }

        private void btnSMEMAOut_Click(object sender, EventArgs e)
        {
            int conveyorNo = 0;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                conveyorNo = 1;
            }
            if (conveyorNo == 0)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动SMEMA出板);
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动SMEMA出板);
            }
        }
    }
}
