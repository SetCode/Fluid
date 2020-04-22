using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
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
    public partial class EditMoveAbsZForm1 : EditFormBase, IMsgSender
    {
        private bool isCreating;
        private MoveAbsZCmdLine moveAbsZCmdLine;
        private MoveAbsZCmdLine moveAbsZCmdLineBackUp;
        public EditMoveAbsZForm1() : this(null)
        {
        }

        public EditMoveAbsZForm1(MoveAbsZCmdLine moveAbsZCmdLine) : base(new PointD(0, 0))
        {
            InitializeComponent();
            this.ReadLanguageResources();
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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbZ.IsValid)
            {
                //MessageBox.Show("Please input a double number for z.");
                MessageBox.Show("请输入小数.");
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
                Close();
                if (this.moveAbsZCmdLine!=null && this.moveAbsZCmdLineBackUp!=null)
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
    }
}
