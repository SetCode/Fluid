using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
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
    public partial class EditMoveXyForm : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private MoveXyCmdLine moveXyCmdLine;
        private MoveXyCmdLine moveXyCmdLineBackUp;
        public EditMoveXyForm()
        {
            InitializeComponent();
        }
        public EditMoveXyForm(Pattern pattern):this(pattern, null)
        {
           
        }
        public EditMoveXyForm(Pattern pattern, MoveXyCmdLine moveXyCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();

            if (moveXyCmdLine == null)
            {
                isCreating = true;
                this.moveXyCmdLine = new MoveXyCmdLine(0, 0);
                this.moveXyCmdLine.Position.X = Properties.Settings.Default.MoveX;
                this.moveXyCmdLine.Position.Y = Properties.Settings.Default.MoveY;
            }
            else
            {
                isCreating = false;
                this.moveXyCmdLine = moveXyCmdLine;
            }
            PointD p =this.pattern.MachineRel(this.moveXyCmdLine.Position);
            this.tbX.Text = p.X.ToString("0.000");
            this.tbY.Text = p.Y.ToString("0.000");
            if (this.moveXyCmdLine != null)
            {
                this.moveXyCmdLineBackUp = (MoveXyCmdLine)this.moveXyCmdLine.Clone();
            }
            this.ReadLanguageResources();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid)
            {
                //MessageBox.Show("Please input a double number for X.");
                MessageBox.Show("请在X方向输入一个小数.");
                return;
            }
            if (!tbY.IsValid)
            {
                MessageBox.Show("请在Y方向输入一个小数.");
                return;
            }
            PointD p=this.pattern.SystemRel(this.tbX.Value, this.tbY.Value);
            this.moveXyCmdLine.Position.X = p.X;
            this.moveXyCmdLine.Position.Y = p.Y;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, moveXyCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, moveXyCmdLine);
            }
            Properties.Settings.Default.MoveX = this.moveXyCmdLine.Position.X;
            Properties.Settings.Default.MoveY = this.moveXyCmdLine.Position.Y;
            if (!this.isCreating)
            {
                Close();
                if (this.moveXyCmdLine != null && this.moveXyCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.moveXyCmdLine, this.moveXyCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.tbX.Text = (Machine.Instance.Robot.PosX - this.origin.X).ToString("0.000");
            this.tbY.Text = (Machine.Instance.Robot.PosY - this.origin.Y).ToString("0.000");

        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!this.tbX.IsValid || !this.tbY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXYAndReply(this.origin.X+this.tbX.Value, this.origin.Y+this.tbY.Value);
        }
    }
}
