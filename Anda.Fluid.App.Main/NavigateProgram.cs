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
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.UI;
using DrawingPanel.Msg;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class NavigateProgram : UserControl, IMsgSender, IMsgReceiver
    {
        private string status = MsgType.IDLE;
        public NavigateProgram()
        {
            InitializeComponent();
            this.btnMain.Click += BtnMain_Click;
            UserControlEx UserControl = new UserControlEx();
            foreach (Control controls in this.Controls)
            {
                controls.MouseMove += UserControl.ReadDisplayTip;
                controls.MouseLeave += UserControl.DisopTip;
            }
        }

        public NaviBtnAlarms BtnAlarm => this.naviBtnAlarms1;

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if(msgName == MsgType.IDLE)
            {
                this.status = msgName;
                OnIdle();
            }
            else if(msgName == MsgType.RUNNING || msgName == MsgType.BUSY)
            {
                this.status = msgName;
                OnRunning();
            }
            else if(msgName == MsgType.PAUSED)
            {
                this.status = msgName;
                OnPaused();
            }
            else if (msgName == MsgConstants.SWITCH_USER || msgName == MsgConstants.MODIFY_ACCESS || msgName == LngMsg.SWITCH_LNG)
            {
                this.HandleMsg(this.status, this, null);
                this.naviBtnAdvanced1.UpdateUI();
                this.naviBtnConfig1.UpdateUI();
                this.naviBtnTools1.UpdateUI();
            }
        }

        private void BtnMain_Click(object sender, EventArgs e)
        {
            MsgCenter.SendMsg(MsgType.ENTER_MAIN, this, MainForm.Ins, null);

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Workpiece界面);
        }

        private void OnRunning()
        {
            this.naviBtnJog1.Enabled = false;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnLoc1.Enabled = false;
            this.naviBtnTools1.Enabled = false;
            this.naviBtnConfig1.Enabled = false;
            this.naviBtnAdvanced1.Enabled = false;
            this.naviBtnAlarms1.Enabled = true;
            this.naviBtnLogin1.Enabled = false;
            this.btnMain.Enabled = true;
        }

        private void OnPaused()
        {
            this.naviBtnJog1.Enabled = false;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnLoc1.Enabled = false;
            this.naviBtnTools1.Enabled = false;
            this.naviBtnConfig1.Enabled = false;
            this.naviBtnAdvanced1.Enabled = false;
            this.naviBtnAlarms1.Enabled = true;
            this.naviBtnLogin1.Enabled = false;
            this.btnMain.Enabled = true;
        }

        private void OnIdle()
        {
            this.naviBtnJog1.Enabled = true;
            this.naviBtnVision1.Enabled = true;
            this.naviBtnLoc1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnLoc1;
            this.naviBtnTools1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnTools1;
            this.naviBtnConfig1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnConfig1;
            this.naviBtnAdvanced1.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseBtnAdvanced1;
            this.naviBtnAlarms1.Enabled = true;
            this.naviBtnLogin1.Enabled = true;
            this.btnMain.Enabled = true;
        }
    }
}
