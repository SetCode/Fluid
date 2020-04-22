using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.App.Main.EventBroker;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class NavigateMain : UserControl, IMsgSender, IMsgReceiver
    {
        private string status = MsgType.IDLE;
        public NavigateMain()
        {
            InitializeComponent();

            this.btnEditPgm.Click += BtnEditPgm_Click;
            this.btnExit.Click += BtnExit_Click;
            UserControlEx UserControl = new UserControlEx();
            foreach (Control controls in this.Controls)
            {
                controls.MouseMove += UserControl.ReadDisplayTip;
                controls.MouseLeave += UserControl.DisopTip;
            }
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (msgName == MsgType.IDLE)
            {
                this.status = msgName;
                OnIdle();
            }
            else if (msgName == MsgType.RUNNING)
            {
                this.status = msgName;
                OnRunning();
            }
            else if (msgName == MsgType.PAUSED)
            {
                this.status = msgName;
                OnPaused();
            }
            else if(msgName == MsgType.BUSY)
            {
                this.status = msgName;
                OnRunning();
                this.btnEditPgm.Enabled = false;
            }
            else if(msgName == MsgConstants.SWITCH_USER||msgName == MsgConstants.MODIFY_ACCESS||msgName == LngMsg.SWITCH_LNG)
            {
                this.HandleMsg(this.status, this, null);
                this.naviBtnSetup1.UpdateUI();
                this.naviBtnConfig1.UpdateUI();
                this.naviBtnTools1.UpdateUI();
                this.naviBtnTest1.UpdateUI();
                this.naviBtnCalib1.UpdateUI();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            MsgCenter.SendMsg(MsgType.EXIT, this, MainForm.Ins, null);
        }

        private void BtnEditPgm_Click(object sender, EventArgs e)
        {
            MsgCenter.SendMsg(MsgType.ENTER_EDIT, this, MainForm.Ins, null);
        }

        private void btnLoadPgm_Click(object sender, EventArgs e)
        {
            MainForm.Ins.ProgramCtl.SaveProgramIfChanged();
            LoadFlu.Instance.OpenFile(this);
        }

        private void OnRunning()
        {
            this.btnLoadPgm.Enabled = false;
            this.btnEditPgm.Enabled = false;
            this.naviBtnJog1.Enabled = false;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnTools1.Enabled = false;
            this.naviBtnConfig1.Enabled = false;
            this.naviBtnSetup1.Enabled = false;
            this.naviBtnLogin1.Enabled = false;
            this.naviBtnTest1.Enabled = false;
            this.naviBtnCalib1.Enabled = false;
        }

        private void OnPaused()
        {
            this.btnLoadPgm.Enabled = false;
            this.btnEditPgm.Enabled = false;
            this.naviBtnJog1.Enabled = false;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnTools1.Enabled = false;
            this.naviBtnConfig1.Enabled = false;
            this.naviBtnSetup1.Enabled = false;
            this.naviBtnLogin1.Enabled = false;
            this.naviBtnTest1.Enabled = false;
            this.naviBtnCalib1.Enabled = false;
        }

        private void OnIdle()
        {
            this.btnLoadPgm.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnLoadPgm;
            this.btnEditPgm.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnEditPgm;
            this.naviBtnJog1.Enabled = true;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnTools1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnTools1;
            this.naviBtnConfig1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnConfig1;
            this.naviBtnSetup1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnAdvanced1;
            this.naviBtnLogin1.Enabled = true;
            this.naviBtnTest1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnConfig1;
            this.naviBtnCalib1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnConfig1;
        }
    }
}
