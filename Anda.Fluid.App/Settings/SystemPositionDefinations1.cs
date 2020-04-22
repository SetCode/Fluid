using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Settings
{
    public partial class SystemPositionDefinations1 : EditFormBase, IMsgSender
    {
        private FluidProgram program;
        private bool changed = false;
        private UserPosition selectedUserPosition;
        private UserPosition selectedUserPositionBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private SystemPositionDefinations1()
        {
            InitializeComponent();
        }

        public SystemPositionDefinations1(FluidProgram program)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.program = program;
            this.updateList();
            tbX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            tbY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
            tbZ.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
            if(Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.rbNeedle2.Enabled = false;
            }
            if(listBoxDefs.Items.Count > 0)
            {
                listBoxDefs.SelectedIndex = 0;
            }
        }

        private void updateList()
        {
            listBoxDefs.Items.Clear();
            foreach (var item in this.program.UserPositions)
            {
                listBoxDefs.Items.Add(item);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            tbY.Text = Machine.Instance.Robot.PosY.ToString("0.000");           
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid || !tbZ.IsValid)
            {
                return;
            }
            //Machine.Instance.Robot.MoveSafeZ();
            ////Machine.Instance.Robot.MovePosXY(tbX.Value, tbY.Value);
            //Machine.Instance.Robot.ManualMovePosXY(tbX.Value, tbY.Value);
            //Machine.Instance.Robot.MovePosZAndReply(tbZ.Value);

            PointD pos = new PointD(tbX.Value, tbY.Value);
            PointD positionCamera= new PointD(tbX.Value, tbY.Value);
            MoveType moveType = this.getMoveType();
            
            switch (moveType)
            {
                case MoveType.NEEDLE1:
                    pos = pos.ToNeedle(Drive.ValveSystem.ValveType.Valve1);
                    Log.Dprint("move to position by needle1 : " + pos + ", position by camera:" + positionCamera);
                    break;
                case MoveType.NEEDLE2:
                    pos = pos.ToNeedle(Drive.ValveSystem.ValveType.Valve2);
                    Log.Dprint("move to position by needle2 : " + pos + ", position by camera:" + positionCamera);
                    break;
                case MoveType.LASER:
                    pos = pos.ToLaser();
                    Log.Dprint("move to position by laser : " + pos + ", position by camera:" + positionCamera);
                    break;
                default:
                    Log.Dprint("move to position : " + pos);
                    break;
            }
            Result ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return;
            }
            ret=Machine.Instance.Robot.MovePosXYAndReply(pos, Machine.Instance.Robot.DefaultPrm.ManualVelXY,Machine.Instance.Robot.DefaultPrm.ManulAccXY);
            if (!ret.IsOk)
            {
                return;
            }
       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                //MessageBox.Show("Please input name.");
                MessageBox.Show("请输入点名称.");
                return;
            }
            string name = tbName.Text.Trim();
            if (this.program.UserPositions.Find(x => x.Name == name) != null)
            {
                //MessageBox.Show("Name " + name + " has already existed!");
                MessageBox.Show("名称 " + name + " 已经存在!");
                return;
            }
            if (!tbX.IsValid || !tbY.IsValid|| !tbZ.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的参数.");
                return;
            }
            PointD pos = new PointD(tbX.Value, tbY.Value);
            UserPosition up = new UserPosition(name, pos);
            up.MoveType = this.getMoveType();
            up.PosZ = tbZ.Value;

            this.program.UserPositions.Add(up);
            listBoxDefs.Items.Add(up);
            changed = true;
            string msg = string.Format("Add userPosition {0}:[{1},{2}]", name, pos.X, pos.Y);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid ||!tbZ.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的参数.");
                return;
            }
            if (listBoxDefs.SelectedItem == null)
            {
                return;
            }
            int selectedIndex = listBoxDefs.SelectedIndex;
            this.selectedUserPosition.Position.X = tbX.Value;
            this.selectedUserPosition.Position.Y = tbY.Value;
            this.selectedUserPosition.PosZ = tbZ.Value;
            this.selectedUserPosition.MoveType = this.getMoveType();
            this.updateList();
            listBoxDefs.SelectedIndex = selectedIndex;
            CompareObj.CompareField(this.selectedUserPosition, this.selectedUserPositionBackUp, null, this.GetType().Name, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDefs.SelectedItem == null)
            {
                return;
            }
            UserPosition up = listBoxDefs.SelectedItem as UserPosition;
            this.program.UserPositions.Remove(up);
            listBoxDefs.Items.Remove(up);
            changed = true;
            if(listBoxDefs.Items.Count > 0)
            {
                listBoxDefs.SelectedIndex = 0;
            }
            string msg = string.Format("Delete userPosition {0}:[{1},{2}]", up.Name, up.Position.X, up.Position.Y);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
        }

        private void listBoxDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDefs.SelectedItem == null)
            {
                return;
            }
            this.selectedUserPosition = listBoxDefs.SelectedItem as UserPosition;
            this.selectedUserPositionBackUp = (UserPosition)this.selectedUserPosition.Clone();
            this.tbName.Text = this.selectedUserPosition.Name;
            this.tbX.Text = selectedUserPosition.Position.X.ToString("0.000");
            this.tbY.Text = selectedUserPosition.Position.Y.ToString("0.000");
            this.tbZ.Text = selectedUserPosition.PosZ.ToString("0.000");
            this.checkMoveType(selectedUserPosition.MoveType);
        }

        private void checkMoveType(MoveType moveType)
        {
            switch (moveType)
            {
                case Domain.FluProgram.Common.MoveType.CAMERA:
                    this.rbCamera.Checked = true;
                    break;
                case Domain.FluProgram.Common.MoveType.LASER:
                    this.rbLaser.Checked = true;
                    break;
                case Domain.FluProgram.Common.MoveType.NEEDLE1:
                    this.rbNeedle1.Checked = true;
                    break;
                case Domain.FluProgram.Common.MoveType.NEEDLE2:
                    this.rbNeedle2.Checked = true;
                    break;
            }
        }

        private MoveType getMoveType()
        {
            if (rbLaser.Checked)
            {
                return MoveType.LASER;
            }
            else if (rbNeedle1.Checked)
            {
                return MoveType.NEEDLE1;
            }
            else if (rbNeedle2.Checked)
            {
                return MoveType.NEEDLE2;
            }
            return MoveType.CAMERA;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (changed)
            {
                MsgCenter.Broadcast(Constants.MSG_SYS_POSITIONS_DEFS_CHANGED, this, null);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTeachZ_Click(object sender, EventArgs e)
        {
            tbZ.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
        }

        private void btnGotoZ_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MovePosZAndReply(tbZ.Value);
        }
    }
}
