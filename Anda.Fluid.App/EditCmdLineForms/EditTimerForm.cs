using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditTimerForm : FormEx, IMsgSender
    {
        private TimerCmdLine timerCmdLine;
        private TimerCmdLine timerCmdLineBackUp;
        private bool isCreating;

        public EditTimerForm() : this(null)
        {
        }

        public EditTimerForm(TimerCmdLine timerCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;

            if (timerCmdLine == null)
            {
                isCreating = true;
                this.timerCmdLine = new TimerCmdLine();
                this.timerCmdLine.WaitInMills = Properties.Settings.Default.Timer;
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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbWaitInMills.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MessageBox.Show("请输入正确的参数.");
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

            Properties.Settings.Default.Timer = timerCmdLine.WaitInMills;
            if (!this.isCreating)
            {
                Close();
                CompareObj.CompareField(this.timerCmdLine, this.timerCmdLineBackUp, null, this.GetType().Name, true);
            }
        }

    }
}
