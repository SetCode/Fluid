using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.ActiveItems;
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
    public partial class EditDotForm1 : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private DotCmdLine dotCmdLine;
        private DotCmdLine dotCmdLineBackUp;

        private bool robotIsXYZU;
        private bool robotIsXYZUV;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditDotForm1()
        {
            InitializeComponent();
        }
        public EditDotForm1(Pattern pattern) : this(pattern, null)
        {
        }

        /// <summary>
        /// 编辑点命令
        /// </summary>
        /// <param name="origin">命令所在Pattern的机械坐标</param>
        /// <param name="dotCmdLine">点命令行</param>
        public EditDotForm1(Pattern pattern, DotCmdLine dotCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.robotIsXYZU = Machine.Instance.Robot.AxesStyle == RobotAxesStyle.XYZU;
            this.robotIsXYZUV = Machine.Instance.Robot.AxesStyle == RobotAxesStyle.XYZUV;
            bool showTiltProperty = this.robotIsXYZU || this.robotIsXYZUV;
            this.cbTiltType.Visible = showTiltProperty;
            this.lblTiltType.Visible = showTiltProperty;
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
                this.btnLastCmdLine.Visible = false;
                this.btnNextCmdLine.Visible = false;
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

            if (showTiltProperty)
            {
                this.cbTiltType.Items.Add("不倾斜");
                this.cbTiltType.Items.Add("左倾斜");
                this.cbTiltType.Items.Add("右倾斜");
                if (robotIsXYZUV)
                {
                    this.cbTiltType.Items.Add("前倾斜");
                    this.cbTiltType.Items.Add("后倾斜");
                }
                this.cbTiltType.SelectedIndex = (int)this.dotCmdLine.Tilt;
            }
            this.ReadLanguageResources();

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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid || !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值");
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
            dotCmdLine.Tilt = (TiltType)this.cbTiltType.SelectedIndex;
            if (!this.cbTiltType.Visible)
            {
                dotCmdLine.Tilt = 0;
            }
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
            if (!this.isCreating)
            {
                Close();
            }
            if (this.dotCmdLine!=null && this.dotCmdLineBackUp!=null)
            {
                CompareObj.CompareField(this.dotCmdLine, this.dotCmdLineBackUp, null, this.GetType().Name, true);
                CompareObj.CompareProperty(this.dotCmdLine, this.dotCmdLineBackUp, null, this.GetType().Name, true);
            }
            
        }

        private void btnNextCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchNextSymbol(this.dotCmdLine, this);
        }

        private void btnLastCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchLastSymbol(this.dotCmdLine, this);
        }

        private void ckbShotNums_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.ckbShotNums.Checked;           
            this.cbWeightControl.Checked = !flag;
        }
    }
}
