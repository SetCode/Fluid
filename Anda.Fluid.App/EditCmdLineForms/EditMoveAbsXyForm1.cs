using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram.Common;
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
    public partial class EditMoveAbsXyForm1 : EditFormBase, IMsgSender
    {
        private bool isCreating;
        private MoveAbsXyCmdLine moveAbsXyCmdLine;
        private MoveAbsXyCmdLine moveAbsXyCmdLineBackUp;

        public EditMoveAbsXyForm1() : this(null)
        {
        }

        public EditMoveAbsXyForm1(MoveAbsXyCmdLine moveAbsXyCmdLine) : base(new PointD(0, 0))
        {
            InitializeComponent();
            this.ReadLanguageResources();
            if (moveAbsXyCmdLine == null)
            {
                isCreating = true;
                this.moveAbsXyCmdLine = new MoveAbsXyCmdLine();
                this.moveAbsXyCmdLine.Position.X = Properties.Settings.Default.absX;
                this.moveAbsXyCmdLine.Position.Y = Properties.Settings.Default.absY;
            }
            else
            {
                isCreating = false;
                this.moveAbsXyCmdLine = moveAbsXyCmdLine;
            }
            switch (this.moveAbsXyCmdLine.MoveType)
            {
                case MoveType.CAMERA:
                    rbCamera.Checked = true;
                    break;
                case MoveType.LASER:
                    rbLaser.Checked = true;
                    break;
                case MoveType.NEEDLE1:
                    rbNeedle1.Checked = true;
                    break;
                case MoveType.NEEDLE2:
                    rbNeedle2.Checked = true;
                    break;
            }
            tbX.Text = this.moveAbsXyCmdLine.Position.X.ToString("0.000");
            tbY.Text = this.moveAbsXyCmdLine.Position.Y.ToString("0.000");
            if(Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                rbNeedle2.Enabled = false;
            }
            if (this.moveAbsXyCmdLine != null)
            {
                this.moveAbsXyCmdLineBackUp = (MoveAbsXyCmdLine)this.moveAbsXyCmdLine.Clone();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            moveAbsXyCmdLine.MoveType = this.getMoveType();
            moveAbsXyCmdLine.Position.X = tbX.Value;
            moveAbsXyCmdLine.Position.Y = tbY.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, moveAbsXyCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, moveAbsXyCmdLine);
            }
            Properties.Settings.Default.absX = moveAbsXyCmdLine.Position.X;
            Properties.Settings.Default.absY = moveAbsXyCmdLine.Position.Y;
            if (!this.isCreating)
            {
                Close();
                if (this.moveAbsXyCmdLine!=null && this.moveAbsXyCmdLineBackUp!=null)
                {
                    CompareObj.CompareField(this.moveAbsXyCmdLine, this.moveAbsXyCmdLineBackUp, null, this.GetType().Name, true);
                }
                
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            tbY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(tbX.Value, tbY.Value);
            Machine.Instance.Robot.ManualMovePosXY(tbX.Value, tbY.Value);
        }

        private MoveType getMoveType()
        {
            if(rbLaser.Checked)
            {
                return MoveType.LASER;
            }
            else if(rbNeedle1.Checked)
            {
                return MoveType.NEEDLE1;
            }
            else if(rbNeedle2.Checked)
            {
                return MoveType.NEEDLE2;
            }
            return MoveType.CAMERA;
        }
    }
}
