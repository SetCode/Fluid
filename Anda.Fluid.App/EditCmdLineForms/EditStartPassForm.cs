using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditStartPassForm : FormEx, IMsgSender
    {
        private StartPassCmdLine startPassCmdLine;
        private StartPassCmdLine startPassCmdLineBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditStartPassForm()
        {
            InitializeComponent();
        }

        public EditStartPassForm(StartPassCmdLine startPassCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;

            if (startPassCmdLine == null)
            {
                throw new Exception("start pass cmd line is null.");
            }
            this.startPassCmdLine = startPassCmdLine;
            tbIndex.Text = this.startPassCmdLine.Index.ToString();
            tbIndex.SelectAll();
            if (this.startPassCmdLine != null)
            {
                this.startPassCmdLineBackUp = (StartPassCmdLine)this.startPassCmdLine.Clone();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbIndex.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MessageBox.Show("请输入正确的值.");

                return;
            }
            startPassCmdLine.Index = tbIndex.Value;
            MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, startPassCmdLine);
            Close();
            if (this.startPassCmdLine!=null && this.startPassCmdLineBackUp!=null)
            {
                CompareObj.CompareField(this.startPassCmdLine, this.startPassCmdLineBackUp, null, this.GetType().Name, true);
            }
            
        }

    }
}
