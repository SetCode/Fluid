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
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.App.Common;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditSvSpeedMetro : MetroSetUserControl, IMsgSender
    {
        private ChangeSpeedCmdLine changeSpeedCmdLine;
        private ChangeSpeedCmdLine changeSpeedCmdLineBackUp;
        private bool isCreating;
        public EditSvSpeedMetro():this(null)
        {

        }
        public EditSvSpeedMetro(ChangeSpeedCmdLine changeSpeedCmdLine)
        {
            InitializeComponent();

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
                MetroSetMessageBox.Show(this, "请输入合理的值");
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
                if (this.changeSpeedCmdLine != null && this.changeSpeedCmdLineBackUp != null)
                {
                    CompareObj.CompareProperty(this.changeSpeedCmdLine, this.changeSpeedCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
