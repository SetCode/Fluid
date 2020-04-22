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
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditTimerMetro : MetroSetUserControl, IMsgSender
    {
        private NormalTimerCmdLine timerCmdLine;
        private NormalTimerCmdLine timerCmdLineBackUp;
        private bool isCreating;

        public EditTimerMetro() : this(null)
        {
        }

        public EditTimerMetro(NormalTimerCmdLine timerCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();

            if (timerCmdLine == null)
            {
                isCreating = true;
                this.timerCmdLine = new NormalTimerCmdLine();
                this.timerCmdLine.WaitInMills = Properties.Settings.Default.NormalTimer;
            }
            else
            {
                isCreating = false;
                this.timerCmdLine = timerCmdLine;
            }
            tbWaitInMills.Text = this.timerCmdLine.WaitInMills.ToString();
            tbWaitInMills.SelectAll();
            if (this.timerCmdLine != null)
            {
                this.timerCmdLineBackUp = (NormalTimerCmdLine)this.timerCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbWaitInMills.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MetroSetMessageBox.Show(this, "请输入正确的值");
                return;
            }
            timerCmdLine.WaitInMills = tbWaitInMills.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, timerCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, timerCmdLine);
            }

            Properties.Settings.Default.NormalTimer = timerCmdLine.WaitInMills;
            if (!this.isCreating)
            {
                if (this.timerCmdLine != null && this.timerCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.timerCmdLine, this.timerCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
