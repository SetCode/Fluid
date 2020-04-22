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
    public partial class EditMultipassTimerMetro : MetroSetUserControl, IMsgSender
    {
        private TimerCmdLine timerCmdLine;
        private TimerCmdLine timerCmdLineBackUp;
        private bool isCreating;

        public EditMultipassTimerMetro() : this(null)
        {
        }

        public EditMultipassTimerMetro(TimerCmdLine timerCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();

            if (timerCmdLine == null)
            {
                isCreating = true;
                this.timerCmdLine = new TimerCmdLine();
                this.timerCmdLine.WaitInMills = Properties.Settings.Default.MultipassTimer;
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
                this.timerCmdLineBackUp = (TimerCmdLine)this.timerCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbWaitInMills.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MetroSetMessageBox.Show(this, "请输入正确的参数.");
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

            Properties.Settings.Default.MultipassTimer = timerCmdLine.WaitInMills;
            if (!this.isCreating)
            {
                CompareObj.CompareField(this.timerCmdLine, this.timerCmdLineBackUp, null, this.GetType().Name, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
