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
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.App.Settings;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditMoveLocMetro : MetroSetUserControl, IMsgSender
    {
        private bool isCreating;
        private MoveToLocationCmdLine moveToLocationCmdLine;
        private MoveToLocationCmdLine moveToLocationCmdLineBackUp;
        private FluidProgram program;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditMoveLocMetro()
        {
            InitializeComponent();
        }

        public EditMoveLocMetro(FluidProgram program) : this(program, null)
        {
        }

        public EditMoveLocMetro(FluidProgram program, MoveToLocationCmdLine moveToLocationCmdLine)
        {
            InitializeComponent();
            this.program = program;
            if (moveToLocationCmdLine == null)
            {
                isCreating = true;
                this.moveToLocationCmdLine = new MoveToLocationCmdLine();
            }
            else
            {
                isCreating = false;
                this.moveToLocationCmdLine = moveToLocationCmdLine;
            }
            //this.ReadLanguageResources();
            refreshList();
            if (this.moveToLocationCmdLine != null)
            {
                this.moveToLocationCmdLineBackUp = (MoveToLocationCmdLine)this.moveToLocationCmdLine.Clone();
            }
        }

        private void refreshList()
        {
            comboBoxSysPositions.Items.Clear();
            // 加载所有的用户自定义位置
            foreach (var item in this.program.UserPositions)
            {
                comboBoxSysPositions.Items.Add(item);
            }
            // 选择当前 item
            UserPosition up = this.program.UserPositions.Find(x => x.Name == this.moveToLocationCmdLine.PositionName);
            if (up != null)
            {
                comboBoxSysPositions.SelectedItem = up;
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (comboBoxSysPositions.SelectedItem == null)
            {
                //MessageBox.Show("System position is not selected.");
                MetroSetMessageBox.Show(this, "请选择坐标点位.");
                return;
            }
            UserPosition up = comboBoxSysPositions.SelectedItem as UserPosition;
            moveToLocationCmdLine.PositionName = up.Name;
            moveToLocationCmdLine.MoveType = up.MoveType;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, moveToLocationCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, moveToLocationCmdLine);
            }
            if (!this.isCreating)
            {
                if (this.moveToLocationCmdLine != null && this.moveToLocationCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.moveToLocationCmdLine, this.moveToLocationCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            new SystemPositionDefinations1(this.program).ShowDialog();
            refreshList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
