using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using System.Reflection;
using Anda.Fluid.Drive.Sensors;
using DrawingPanel.Msg;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageJobsProduction : MetroSetUserControl, IMsgReceiver
    {
        public PageJobsProduction()
        {
            InitializeComponent();

            //this.canvasControll1.SetControlMode(true);
            //DrawingMsgCenter.Instance.RegisterReceiver(this.canvasControll1);

            this.txtProgramName.ReadOnly = true;
            this.txtBoardCount.ReadOnly = true;
            this.txtPassCount.ReadOnly = true;
            this.txtFailedCount.ReadOnly = true;
            this.txtStartTime.ReadOnly = true;
            this.txtCycleTime.ReadOnly = true;

            this.cmbRunMode.Items.Add(ValveRunMode.Wet);
            this.cmbRunMode.Items.Add(ValveRunMode.Dry);
            this.cmbRunMode.Items.Add(ValveRunMode.Look);
            this.cmbRunMode.SelectedIndexChanged += CbxRunMode_SelectedIndexChanged;
            this.cmbRunMode.SelectedIndex = (int)Machine.Instance.Valve1.RunMode;
            this.cmbRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;

            this.txtSetNum.TextChanged += TxtSetNum_TextChanged; ;
            //this.ReadLanguageResources();
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (msgName == MachineMsg.SETUP_INFO)
            {
                onSystemInfoChanged();
            }
            else if (msgName == MsgDef.IDLE)
            {
                this.txtProgramName.Enabled = true;
                this.txtBoardCount.Enabled = true;
                this.txtPassCount.Enabled = true;
                this.txtFailedCount.Enabled = true;
                this.txtStartTime.Enabled = true;
                this.txtCycleTime.Enabled = true;
                this.cmbRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
                this.txtSetNum.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
            }
            else if (msgName == MsgDef.RUNNING || msgName == MsgDef.BUSY || msgName == MsgDef.PAUSED)
            {
                this.txtProgramName.Enabled = false;
                this.txtBoardCount.Enabled = false;
                this.txtPassCount.Enabled = false;
                this.txtFailedCount.Enabled = false;
                this.txtStartTime.Enabled = false;
                this.txtCycleTime.Enabled = false;
                this.cmbRunMode.Enabled = false;
                this.txtSetNum.Enabled = false;
            }
            else if (msgName == MsgDef.RUNINFO_PROGRAM || msgName == Constants.MSG_NEW_PROGRAM)
            {
                string programName = args[0] as string;
                this.txtProgramName.Text = programName;
            }
            else if (msgName == MsgDef.RUNINFO_START_DATETIME)
            {
                DateTime dateTime = (DateTime)args[0];
                this.txtStartTime.Text = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else if (msgName == MsgDef.RUNINFO_RESULT)
            {
                int boardCount = (int)args[0];
                int failedCount = (int)args[1];
                double cycleTime = (double)args[2];
                this.updateCountInfo(boardCount, failedCount, cycleTime);
            }
            //else if (msgName == LngMsg.SWITCH_LNG)
            //{
            //    this.UpdateUI();
            //}
            else if (msgName == MsgConstants.MODIFY_ACCESS || msgName == MsgConstants.SWITCH_USER)
            {
                this.cmbRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
                this.txtSetNum.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
            }
        }

        private void onSystemInfoChanged()
        {
            string infoStr = string.Format("机型: {0}\n板卡: {1}\n阀组: {2}\n轨道: {3}\n相机: {4}\n激光: {5}\n天平: {6}\n加热: {7}",
                //Assembly.GetExecutingAssembly().GetName().Version,
                Machine.Instance.Setting.MachineSelect,
                Machine.Instance.Setting.CardSelect,
                Machine.Instance.Setting.ValveSelect,
                Machine.Instance.Setting.ConveyorSelect,
                Machine.Instance.Camera.Prm.Vendor,
                SensorMgr.Instance.Laser.Vendor,
                SensorMgr.Instance.Scale.Vendor,
                //SensorMgr.Instance.Proportioners.Channel1,
                //SensorMgr.Instance.Proportioners.Channel2,
                SensorMgr.Instance.Heater.Vendor);
            this.lblSetupInfo.Text = infoStr;
        }

        private void TxtSetNum_TextChanged(object sender)
        {
            try
            {
                Executor.Instance.Cycle = int.Parse(this.txtSetNum.Text);
            }
            catch
            {
                this.txtSetNum.Text = "0";
                Executor.Instance.Cycle = 0;
            }
        }

        private void CbxRunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbRunMode.SelectedIndex < 0)
            {
                return;
            }
            Machine.Instance.Valve1.RunMode = (ValveRunMode)this.cmbRunMode.SelectedIndex;
        }


        private void updateCountInfo(int boardCount, int failedCount, double cycleTime)
        {
            //this.txtBoardCount.Text = boardCount.ToString();
            this.txtFailedCount.Text = failedCount.ToString();
            //this.txtPassCount.Text = (boardCount - failedCount).ToString();
            this.txtPassCount.Text = boardCount.ToString();
            this.txtBoardCount.Text = (boardCount + failedCount).ToString();
            this.txtCycleTime.Text = cycleTime.ToString();
        }

    }
}
