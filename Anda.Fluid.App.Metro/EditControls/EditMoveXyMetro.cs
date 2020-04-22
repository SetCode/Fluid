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

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditMoveXyMetro : MetroSetUserControl, IMsgSender
    {
        private bool isCreating;
        private MoveXyCmdLine moveXyCmdLine;
        private MoveXyCmdLine moveXyCmdLineBackUp;

        public EditMoveXyMetro() : this(null)
        {
        }

        public EditMoveXyMetro(MoveXyCmdLine moveXyCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();

            if (moveXyCmdLine == null)
            {
                isCreating = true;
                this.moveXyCmdLine = new MoveXyCmdLine(0, 0);
            }
            else
            {
                isCreating = false;
                this.moveXyCmdLine = moveXyCmdLine;
            }
            tbX.Text = this.moveXyCmdLine.Position.X.ToString("0.000");
            tbY.Text = this.moveXyCmdLine.Position.Y.ToString("0.000");
            if (this.moveXyCmdLine != null)
            {
                this.moveXyCmdLineBackUp = (MoveXyCmdLine)this.moveXyCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbX.IsValid)
            {
                //MessageBox.Show("Please input a double number for X.");
                MetroSetMessageBox.Show(this, "请在X方向输入一个小数.");
                return;
            }
            if (!tbY.IsValid)
            {
                MetroSetMessageBox.Show(this, "请在Y方向输入一个小数.");
                return;
            }
            moveXyCmdLine.Position.X = tbX.Value;
            moveXyCmdLine.Position.Y = tbY.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, moveXyCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, moveXyCmdLine);
            }
            if (!this.isCreating)
            {
                if (this.moveXyCmdLine != null && this.moveXyCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.moveXyCmdLine, this.moveXyCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
