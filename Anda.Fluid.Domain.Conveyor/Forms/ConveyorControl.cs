using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Conveyor.ConveyorState;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Drive.Conveyor.LeadShine.IO;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.Conveyor.Forms
{
    public partial class ConveyorControl : UserControlEx
    {
        private bool conveyor2Exit = false;
        
        public ConveyorControl()
        {
            this.InitCtl();
        }
        
        private void InitCtl()
        {
            ConveyorPrmMgr.Instance.Load();

            InitializeComponent();

            this.cbxConveyor.SelectedIndex = 0;

            if (Machine.Instance.Setting.MachineSelect != MachineSelection.RTV)
            {
                this.txtDownConveyorState.Visible = false;
                this.label6.Visible = false;
                this.cbxConveyor.Visible = false;
                this.btnRun.Visible = false;
                this.btnStop.Visible = false;
            }
        }

        public void SetUp()
        {
            if (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.双轨)
            {
                this.conveyor2Exit = true;
            }
            this.Init();

            this.cbxConveyor1Mode.SelectedIndexChanged += new System.EventHandler(this.cbxConveyorMode_SelectedIndexChanged);
            this.cbxConveyor2Mode.SelectedIndexChanged += new System.EventHandler(this.cbxConveyorMode_SelectedIndexChanged);

        }
        private void Init()
        {

            this.cbxConveyor1Mode.SelectedIndex = (int)FlagBitMgr.Instance.FindBy(0).UILevel.SelectedMode;
            
            if (!this.conveyor2Exit)
            {
                this.chkConveyor1.AutoCheck = false;
                this.chkConveyor2.Enabled = false;
                this.cbxConveyor2Mode.Enabled = false;
            }
            else
            {
                this.cbxConveyor2Mode.SelectedIndex = (int)FlagBitMgr.Instance.FindBy(1).UILevel.SelectedMode;
            }

            switch (ConveyorMsgCenter.Instance.ConveyorState)
            {
                case ConveyorControlMsg.轨道1启用:
                    this.chkConveyor1.Checked = true;
                    this.chkConveyor2.Checked = false;
                    break;
                case ConveyorControlMsg.轨道2启用:
                    this.chkConveyor1.Checked = false;
                    this.chkConveyor2.Checked = true;
                    break;
                case ConveyorControlMsg.轨道1和轨道2同时启用:
                    this.chkConveyor1.Checked = true;
                    this.chkConveyor2.Checked = true;
                    break;
                case ConveyorControlMsg.轨道1和轨道2都不启用:
                    this.chkConveyor1.Checked = false;
                    this.chkConveyor2.Checked = false;
                    break;
            }

        }

        private void cbxConveyorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            int conveyorNo;
            if (cbx.Name == this.cbxConveyor1Mode.Name)
            {
                conveyorNo = 0;
            }
            else
            {
                conveyorNo = 1;
            }
            UILevel.RunMode mode = FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode;
            switch (cbx.SelectedIndex)
            {
                case 0:
                    FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode = UILevel.RunMode.Auto;
                    break;
                case 1:
                    FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode = UILevel.RunMode.Demo;
                    break;
                case 2:
                    FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode = UILevel.RunMode.PassThrough;
                    break;
            }
            string msg = string.Format("RunMode change: {0}->{1}", mode.ToString(), FlagBitMgr.Instance.FindBy(conveyorNo).UILevel.SelectedMode.ToString());
            Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
        }
        private void btnConveyor_Click(object sender,EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == this.btnConveyor1Stuck.Name)
            {
                ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1卡板解决);
                this.btnConveyor1Stuck.Enabled = false;
            }

            else if (btn.Name == this.btnConveyor2Stuck.Name)
            {
                ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道2卡板解决);
                this.btnConveyor2Stuck.Enabled = false;
            }

        }
        private void chkConveyor_CheckedChanged(object sender,EventArgs e)
        {
            if (!this.conveyor2Exit)
            {
                if (this.chkConveyor1.Checked)
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1启用);
                }
                else
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1和轨道2都不启用);
                }
            } 
            else
            {
                if (this.chkConveyor1.Checked && this.chkConveyor2.Checked)
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1和轨道2同时启用);
                }
                else if (this.chkConveyor1.Checked && !this.chkConveyor2.Checked)
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1启用);
                }
                else if (!this.chkConveyor1.Checked && this.chkConveyor2.Checked)
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道2启用);
                }
                else if (!this.chkConveyor1.Checked && !this.chkConveyor2.Checked)
                {
                    ConveyorMsgCenter.Instance.ConveyorControl.SendMessage(ConveyorControlMsg.轨道1和轨道2都不启用);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //刷新下层轨道状态
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                this.txtDownConveyorState.Text = ConveyorMgr.Instance.RTVDownConveyor.stateMachine.CurrentSateName;
            }

            this.UpdateIO();
            //刷新轨道1按钮enable状态
            if (FlagBitMgr.Instance.FindBy(0).State.IntegralState == IntegralStateEnum.卡板)
            {
                this.btnConveyor1Stuck.Enabled = true;
            }

            //刷新轨道1显示状态
            if (ConveyorPrmMgr.Instance.FindBy(0).SubsiteMode ==  ConveyorSubsiteMode.Singel)
            {
                this.txtConveyor1IntegralState.Text = FlagBitMgr.Instance.FindBy(0).State.IntegralState.ToString();
                this.txtConveyor1PreSiteState.Text = "";
                this.txtConveyor1FinishedSiteState.Text = "";

                this.txtConveyor1WorkingSiteState.Text = FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.SingleSiteState.ToString();
                if (FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.卡板 )
                {
                    this.txtConveyor1WorkingSiteState.BackColor = Color.Red;
                }
                else
                {
                    this.txtConveyor1WorkingSiteState.BackColor = Color.White;
                }
            }
            else
            {
                this.txtConveyor1IntegralState.Text = FlagBitMgr.Instance.FindBy(0).State.IntegralState.ToString();
                if (FlagBitMgr.Instance.FindBy(0).State.IntegralState == IntegralStateEnum.卡板)
                {
                    this.txtConveyor1IntegralState.BackColor = Color.Red;
                }
                else
                {
                    this.txtConveyor1IntegralState.BackColor = Color.White;
                }

                //预热站状态
                this.txtConveyor1PreSiteState.Text = FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.PreSiteState.ToString();
                if (FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.PreSiteState == PreSiteStateEnum.卡板)
                {
                    this.txtConveyor1PreSiteState.BackColor = Color.Red;
                }
                else
                {
                    this.txtConveyor1PreSiteState.BackColor = Color.White;
                }

                //点胶站状态
                this.txtConveyor1WorkingSiteState.Text = FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.WorkingSiteState.ToString();

                //成品站状态
                this.txtConveyor1FinishedSiteState.Text = FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.FinishedSiteState.ToString();
                if (FlagBitMgr.Instance.FindBy(0).State.SubSitesCurrState.FinishedSiteState == FinishedSiteStateEnum.卡板)
                {
                    this.txtConveyor1FinishedSiteState.BackColor = Color.Red;
                }
                else
                {
                    this.txtConveyor1FinishedSiteState.BackColor = Color.White;
                }
            }
            
            //刷新轨道2
            if (!this.conveyor2Exit)
                return;
            else
            {
                //刷新轨道2按钮enable状态
                if (FlagBitMgr.Instance.FindBy(1).State.IntegralState == IntegralStateEnum.卡板)
                {
                    this.btnConveyor2Stuck.Enabled = true;
                }
                //刷新轨道2显示状态
                if (ConveyorPrmMgr.Instance.FindBy(1).SubsiteMode == ConveyorSubsiteMode.Singel)
                {
                    this.txtConveyor2IntegralState.Text = FlagBitMgr.Instance.FindBy(1).State.IntegralState.ToString();
                    this.txtConveyor2PreSiteState.Text = "";
                    this.txtConveyor2FinishedSiteState.Text = "";

                    this.txtConveyor2WorkingSiteState.Text = FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.SingleSiteState.ToString();
                    if (FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.卡板)
                    {
                        this.txtConveyor2WorkingSiteState.BackColor = Color.Red;
                    }
                    else
                    {
                        this.txtConveyor2WorkingSiteState.BackColor = Color.White;
                    }
                }
                else
                {
                    this.txtConveyor2IntegralState.Text = FlagBitMgr.Instance.FindBy(1).State.IntegralState.ToString();
                    if (FlagBitMgr.Instance.FindBy(1).State.IntegralState == IntegralStateEnum.卡板)
                    {
                        this.txtConveyor2IntegralState.BackColor = Color.Red;
                    }
                    else
                    {
                        this.txtConveyor2IntegralState.BackColor = Color.White;
                    }

                    //预热站状态
                    this.txtConveyor2PreSiteState.Text = FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.PreSiteState.ToString();
                    if (FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.PreSiteState == PreSiteStateEnum.卡板)
                    {
                        this.txtConveyor2PreSiteState.BackColor = Color.Red;
                    }
                    else
                    {
                        this.txtConveyor2PreSiteState.BackColor = Color.White;
                    }

                    //点胶站状态
                    this.txtConveyor2WorkingSiteState.Text = FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.WorkingSiteState.ToString();

                    //成品站状态
                    this.txtConveyor2FinishedSiteState.Text = FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.FinishedSiteState.ToString();
                    if (FlagBitMgr.Instance.FindBy(1).State.SubSitesCurrState.FinishedSiteState == FinishedSiteStateEnum.卡板)
                    {
                        this.txtConveyor2FinishedSiteState.BackColor = Color.Red;
                    }
                    else
                    {
                        this.txtConveyor2FinishedSiteState.BackColor = Color.White;
                    }
                }
            }
            
        }

        /// <summary>
        /// 刷新IO状态
        /// </summary>
        private void UpdateIO()
        {          
            if (Machine.Instance.Setting.MachineSelect != MachineSelection.AD19)
            {
                this.UpdateNotAD19IO();
            }
            else
            {
                this.UpdateAD19IO();
            }
        }

        private void UpdateNotAD19IO()
        {
            //轨道1
            if (DiType.进板检测.Sts().Is(StsType.High))
            {
                this.picConveyor1Enter.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1Enter.BackColor = SystemColors.Control;
            }
            if (DiType.定位1检测.Sts().Is(StsType.High))
            {
                this.picConveyor1PreArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1PreArrived.BackColor = SystemColors.Control;
            }
            if (DiType.定位2检测.Sts().Is(StsType.High))
            {
                this.picConveyor1WorkArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1WorkArrived.BackColor = SystemColors.Control;
            }
            if (DiType.出板检测.Sts().Is(StsType.High))
            {
                this.picConveyor1ProductArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1ProductArrived.BackColor = SystemColors.Control;
            }
            if (DiType.前设备放板信号.Sts().Is(StsType.High))
            {
                this.picConveyor1UpSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1UpSmemaIn.BackColor = SystemColors.Control;
            }
            if (DiType.后设备求板信号.Sts().Is(StsType.High))
            {
                this.picConveyor1DownSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1DownSmemaIn.BackColor = SystemColors.Control;
            }
            if (DoType.求板信号.Sts().Is(StsType.High))
            {
                this.picConveyor1UpSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1UpSmemaOut.BackColor = SystemColors.Control;
            }
            if (DoType.放板信号.Sts().Is(StsType.High))
            {
                this.picConveyor1DownSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1DownSmemaOut.BackColor = SystemColors.Control;
            }

            //轨道2
            if (DiType.轨道2进板检测.Sts().Is(StsType.High))
            {
                this.picConveyor2Enter.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2Enter.BackColor = SystemColors.Control;
            }
            if (DiType.轨道2定位1检测.Sts().Is(StsType.High))
            {
                this.picConveyor2PreArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2PreArrived.BackColor = SystemColors.Control;
            }
            if (DiType.轨道2定位2检测.Sts().Is(StsType.High))
            {
                this.picConveyor2WorkArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2WorkArrived.BackColor = SystemColors.Control;
            }
            if (DiType.轨道2出板检测.Sts().Is(StsType.High))
            {
                this.picConveyor2ProductArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2ProductArrived.BackColor = SystemColors.Control;
            }
            if (DiType.轨道2前设备放板信号.Sts().Is(StsType.High))
            {
                this.picConveyor2UpSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2UpSmemaIn.BackColor = SystemColors.Control;
            }
            if (DiType.轨道2后设备求板信号.Sts().Is(StsType.High))
            {
                this.picConveyor2DownSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2DownSmemaIn.BackColor = SystemColors.Control;
            }
            if (DoType.轨道2求板信号.Sts().Is(StsType.High))
            {
                this.picConveyor2UpSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2UpSmemaOut.BackColor = SystemColors.Control;
            }
            if (DoType.轨道2放板信号.Sts().Is(StsType.High))
            {
                this.picConveyor2DownSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2DownSmemaOut.BackColor = SystemColors.Control;
            }
        }

        private void UpdateAD19IO()
        {
            //轨道1
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1进板感应).Is(StsType.High))
            {
                this.picConveyor1Enter.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1Enter.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1预热站到位).Is(StsType.High))
            {
                this.picConveyor1PreArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1PreArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1工作站到位).Is(StsType.High))
            {
                this.picConveyor1WorkArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1WorkArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1成品站到位).Is(StsType.High))
            {
                this.picConveyor1ProductArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1ProductArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1出板感应).Is(StsType.High))
            {
                this.picConveyor1Exit.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1Exit.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1上游设备有板).Is(StsType.High))
            {
                this.picConveyor1UpSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1UpSmemaIn.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道1下游设备求板).Is(StsType.High))
            {
                this.picConveyor1DownSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1DownSmemaIn.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19OutputSts(DoEnum.轨道1求板).Is(StsType.High))
            {
                this.picConveyor1UpSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1UpSmemaOut.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19OutputSts(DoEnum.轨道1放板).Is(StsType.High))
            {
                this.picConveyor1DownSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor1DownSmemaOut.BackColor = SystemColors.Control;
            }

            //轨道2
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2进板感应).Is(StsType.High))
            {
                this.picConveyor2Enter.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2Enter.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2预热站到位).Is(StsType.High))
            {
                this.picConveyor2PreArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2PreArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2工作站到位).Is(StsType.High))
            {
                this.picConveyor2WorkArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2WorkArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2成品站到位).Is(StsType.High))
            {
                this.picConveyor2ProductArrived.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2ProductArrived.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2出板感应).Is(StsType.High))
            {
                this.picConveyor2Exit.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2Exit.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2上游设备有板).Is(StsType.High))
            {
                this.picConveyor2UpSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2UpSmemaIn.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19InputSts(DiEnum.轨道2下游设备求板).Is(StsType.High))
            {
                this.picConveyor2DownSmemaIn.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2DownSmemaIn.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19OutputSts(DoEnum.轨道2求板).Is(StsType.High))
            {
                this.picConveyor2UpSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2UpSmemaOut.BackColor = SystemColors.Control;
            }
            if (ConveyorController.Instance.GetAD19OutputSts(DoEnum.轨道2放板).Is(StsType.High))
            {
                this.picConveyor2DownSmemaOut.BackColor = Color.Green;
            }
            else
            {
                this.picConveyor2DownSmemaOut.BackColor = SystemColors.Control;
            }
        }

        private void btnChangeConveyor1ToOffline_Click(object sender, EventArgs e)
        {
            FlagBitMgr.Instance.FindBy(0).UILevel.EnterEditForm = true;
        }

        private void btnChangeConveyor2ToOffline_Click(object sender, EventArgs e)
        {
            FlagBitMgr.Instance.FindBy(1).UILevel.EnterEditForm = true;
        }

        private void btnConveyor1ExitOffline_Click(object sender, EventArgs e)
        {
            FlagBitMgr.Instance.FindBy(0).UILevel.ExitEditForm = true;
        }

        private void btnConveyor2ExitOffline_Click(object sender, EventArgs e)
        {
            FlagBitMgr.Instance.FindBy(1).UILevel.ExitEditForm = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (this.cbxConveyor.SelectedIndex == 0)
            {
                AxisType.Axis5.MoveJog(ConveyorPrmMgr.Instance.FindBy(0).Speed);
            }
            else if (this.cbxConveyor.SelectedIndex == 1)
            {
                AxisType.Axis7.MoveJog(ConveyorPrmMgr.Instance.FindBy(1).Speed);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.cbxConveyor.SelectedIndex == 0)
            {
                AxisType.Axis5.MoveStop();
            }
            else if (this.cbxConveyor.SelectedIndex == 1)
            {
                AxisType.Axis7.MoveStop();
            }
        }
    }
}
   