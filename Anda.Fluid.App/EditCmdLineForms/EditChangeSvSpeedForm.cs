using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditChangeSvSpeedForm : FormEx,IMsgSender
    {
        private ChangeSpeedCmdLine changeSpeedCmdLine;
        private ChangeSpeedCmdLine changeSpeedCmdLineBackUp;
        private bool isCreating;
        public EditChangeSvSpeedForm():this(null)
        {

        }
        public EditChangeSvSpeedForm(ChangeSpeedCmdLine changeSpeedCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();

            this.StartPosition = FormStartPosition.CenterParent;

            if (changeSpeedCmdLine == null)
            {
                isCreating = true;
                this.changeSpeedCmdLine = new ChangeSpeedCmdLine();

            }
            else
            {
                this.isCreating = false;
                this.changeSpeedCmdLine = changeSpeedCmdLine;
            }
            tbWaitInMills.Text = this.changeSpeedCmdLine.WaitInMills.ToString();
            tbSpeed.Text = this.changeSpeedCmdLine.Speed.ToString();
            tbSpeed.SelectAll();
            if (this.changeSpeedCmdLine != null)
            {
                this.changeSpeedCmdLineBackUp = (ChangeSpeedCmdLine)this.changeSpeedCmdLine.Clone();
            }
        }

   

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbWaitInMills.IsValid || !tbSpeed.IsValid) 
            {
                //MessageBox.Show("Please input valid value");
                MessageBox.Show("请输入合理的值");
                return;
            }

            changeSpeedCmdLine.Speed = tbSpeed.Value;
            changeSpeedCmdLine.WaitInMills = tbWaitInMills.Value;

            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, changeSpeedCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, changeSpeedCmdLine);
            }

            if (!this.isCreating)
            {
                Close();
                if (this.changeSpeedCmdLine!=null && this.changeSpeedCmdLineBackUp!=null)
                {
                    CompareObj.CompareProperty(this.changeSpeedCmdLine, this.changeSpeedCmdLineBackUp, null, this.GetType().Name, true);
                }
               
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
