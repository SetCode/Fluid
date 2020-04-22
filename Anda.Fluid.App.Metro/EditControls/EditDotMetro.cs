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
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditDotMetro : MetroSetUserControl, IMsgSender,ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private DotCmdLine dotCmdLine;
        private DotCmdLine dotCmdLineBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditDotMetro()
        {
            InitializeComponent();
        }
        public EditDotMetro(Pattern pattern) : this(pattern, null)
        {
        }

        /// <summary>
        /// 编辑点命令
        /// </summary>
        /// <param name="origin">命令所在Pattern的机械坐标</param>
        /// <param name="dotCmdLine">点命令行</param>
        public EditDotMetro(Pattern pattern, DotCmdLine dotCmdLine)
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (dotCmdLine == null)
            {
                isCreating = true;
                this.dotCmdLine = new DotCmdLine();
                this.dotCmdLine.Position.X = Properties.Settings.Default.DotX;
                this.dotCmdLine.Position.Y = Properties.Settings.Default.DotY;
                this.dotCmdLine.DotStyle = (DotStyle)Properties.Settings.Default.DotStyle;
                this.dotCmdLine.IsWeightControl = Properties.Settings.Default.DotIsWt;
                this.dotCmdLine.Weight = Properties.Settings.Default.DotWt;
            }
            else
            {
                isCreating = false;
                this.dotCmdLine = dotCmdLine;
            }
            //系统坐标->机械坐标
            PointD p = this.pattern.MachineRel(this.dotCmdLine.Position);
            tbLocationX.Text = p.X.ToString("0.000");
            tbLocationY.Text = p.Y.ToString("0.000");
            for (int i = 0; i < 10; i++)
            {
                comboBoxDotType.Items.Add("Type " + (i + 1));
            }
            comboBoxDotType.SelectedIndex = (int)this.dotCmdLine.DotStyle;
            cbWeightControl.Checked = this.dotCmdLine.IsWeightControl;
            tbWeight.Text = this.dotCmdLine.Weight.ToString("0.000");
            this.ckbShotNums.Checked = this.dotCmdLine.IsAssign;
            this.tbShots.Text = this.dotCmdLine.NumShots.ToString("0");
            if (this.dotCmdLine != null)
            {
                this.dotCmdLineBackUp = (DotCmdLine)this.dotCmdLine.Clone();
            }
            //this.ReadLanguageResources();
        }

        private void btnEditDotParams_Click(object sender, EventArgs e)
        {
            new EditDotParamsForm(FluidProgram.Current.ProgramSettings.DotParamList).ShowDialog();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbLocationX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbLocationY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, System.EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
            this.tbWeight.Enabled = this.cbWeightControl.Checked;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid || !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入有效数值");
                return;
            }
            //机械坐标->系统坐标
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            dotCmdLine.Position.X = p.X;
            dotCmdLine.Position.Y = p.Y;
            dotCmdLine.DotStyle = (DotStyle)comboBoxDotType.SelectedIndex;
            dotCmdLine.IsWeightControl = cbWeightControl.Checked;
            dotCmdLine.Weight = tbWeight.Value;
            dotCmdLine.IsAssign = this.ckbShotNums.Checked;
            dotCmdLine.NumShots = int.Parse(this.tbShots.Text);
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, dotCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, dotCmdLine);
            }
            Properties.Settings.Default.DotX = dotCmdLine.Position.X;
            Properties.Settings.Default.DotY = dotCmdLine.Position.Y;
            Properties.Settings.Default.DotStyle = (int)dotCmdLine.DotStyle;
            Properties.Settings.Default.DotIsWt = dotCmdLine.IsWeightControl;
            Properties.Settings.Default.DotWt = dotCmdLine.Weight;
            if(this.isCreating)
            {
                return;
            }
            if (this.dotCmdLine != null && this.dotCmdLineBackUp != null)
            {
                CompareObj.CompareField(this.dotCmdLine, this.dotCmdLineBackUp, null, this.GetType().Name, true);
                CompareObj.CompareProperty(this.dotCmdLine, this.dotCmdLineBackUp, null, this.GetType().Name, true);
            }
        }

        private void ckbShotNums_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.ckbShotNums.Checked;
            this.cbWeightControl.Checked = !flag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, this);
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
