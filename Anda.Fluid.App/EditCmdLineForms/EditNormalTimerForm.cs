using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditNormalTimerForm : FormEx, IMsgSender
    {
        private NormalTimerCmdLine timerCmdLine;
        private NormalTimerCmdLine timerCmdLineBackUp;
        private bool isCreating;

        public EditNormalTimerForm() : this(null)
        {
        }

        public EditNormalTimerForm(NormalTimerCmdLine timerCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;

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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbWaitInMills.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MessageBox.Show("请输入正确的值");
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
                Close();
                if (this.timerCmdLine!=null && this.timerCmdLineBackUp!=null)
                {
                    CompareObj.CompareField(this.timerCmdLine, this.timerCmdLineBackUp, null, this.GetType().Name, true);
                }
                
            }
        }

    }
}
