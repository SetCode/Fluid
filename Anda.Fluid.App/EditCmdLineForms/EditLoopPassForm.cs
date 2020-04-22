using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditLoopPassForm : FormEx, IMsgSender
    {
        private bool isCreating;
        private LoopPassCmdLine loopPassCmdLine;
        private LoopPassCmdLine loopPassCmdLineBackUp;

        public EditLoopPassForm() : this(null)
        {
        }

        public EditLoopPassForm(LoopPassCmdLine loopPassCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;

            if (loopPassCmdLine == null)
            {
                isCreating = true;
                this.loopPassCmdLine = new LoopPassCmdLine(1, 3);
            }
            else
            {
                isCreating = false;
                this.loopPassCmdLine = loopPassCmdLine;
            }
            tbStart.Text = this.loopPassCmdLine.Start.ToString();
            tbEnd.Text = this.loopPassCmdLine.End.ToString();
            if (this.loopPassCmdLine != null)
            {
                this.loopPassCmdLineBackUp = (LoopPassCmdLine)this.loopPassCmdLine.Clone();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbStart.IsValid || !tbEnd.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值");
                return;
            }
            if (tbStart.Value > tbEnd.Value)
            {
                //MessageBox.Show("Start value can not be bigger than end value.");
                MessageBox.Show("起始点不可以大于结束点.");
                return;
            }
            loopPassCmdLine.Start = tbStart.Value;
            loopPassCmdLine.End = tbEnd.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, loopPassCmdLine, new NextLoopCmdLine());
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, loopPassCmdLine);
            }
            if (!this.isCreating)
            {
                Close();
                if (this.loopPassCmdLine!=null && this.loopPassCmdLineBackUp!=null)
                {
                    CompareObj.CompareField(this.loopPassCmdLine, this.loopPassCmdLineBackUp, null, this.GetType().Name, true);
                }
                
            }
        }
    }
}
