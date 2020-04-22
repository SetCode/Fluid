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
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditMoveAbsZMetro : MetroSetUserControl, IMsgSender, ICanSelectButton
    {
        private bool isCreating;
        private MoveAbsZCmdLine moveAbsZCmdLine;
        private MoveAbsZCmdLine moveAbsZCmdLineBackUp;
        public EditMoveAbsZMetro() : this(null)
        {
        }

        public EditMoveAbsZMetro(MoveAbsZCmdLine moveAbsZCmdLine) 
        {
            InitializeComponent();
            //this.ReadLanguageResources();
            if (moveAbsZCmdLine == null)
            {
                isCreating = true;
                this.moveAbsZCmdLine = new MoveAbsZCmdLine();
            }
            else
            {
                isCreating = false;
                this.moveAbsZCmdLine = moveAbsZCmdLine;
                tbZ.Text = this.moveAbsZCmdLine.Z.ToString("0.000");
            }
            if (this.moveAbsZCmdLine != null)
            {
                this.moveAbsZCmdLineBackUp = (MoveAbsZCmdLine)this.moveAbsZCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbZ.IsValid)
            {
                //MessageBox.Show("Please input a double number for z.");
                MetroSetMessageBox.Show(this, "请输入小数.");
                return;
            }
            moveAbsZCmdLine.Z = tbZ.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, moveAbsZCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, moveAbsZCmdLine);
            }
            if (!this.isCreating)
            {
                if (this.moveAbsZCmdLine != null && this.moveAbsZCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.moveAbsZCmdLine, this.moveAbsZCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbZ.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbZ.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.MovePosZ(tbZ.Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnSelect);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
