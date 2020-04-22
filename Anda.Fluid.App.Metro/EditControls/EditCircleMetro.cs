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
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditCircleMetro : MetroSetUserControl, IMsgSender,ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private CircleCmdLine circleCmdLine;
        private CircleCmdLine circleCmdLineBackUp;
        private bool isCreating;
        private PointD start = new PointD();
        private PointD middle = new PointD();
        private PointD end = new PointD();
        private PointD center = new PointD();
        private bool permitReacting = true;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditCircleMetro()
        {
            InitializeComponent();
        }
        public EditCircleMetro(Pattern pattern) : this(pattern, null)
        {
        }

        public EditCircleMetro(Pattern pattern, CircleCmdLine circleCmdLine)
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            this.circleCmdLine = circleCmdLine;
            isCreating = circleCmdLine == null;
            if (isCreating)
            {
                this.circleCmdLine = new CircleCmdLine();

                this.circleCmdLine.Start.X = Properties.Settings.Default.ArcStartX;
                this.circleCmdLine.Start.Y = Properties.Settings.Default.ArcStartY;
                this.circleCmdLine.Middle.X = Properties.Settings.Default.ArcMidX;
                this.circleCmdLine.Middle.Y = Properties.Settings.Default.ArcMidY;
                this.circleCmdLine.End.X = Properties.Settings.Default.ArcEndX;
                this.circleCmdLine.End.Y = Properties.Settings.Default.ArcEndY;
                this.circleCmdLine.Center.X = Properties.Settings.Default.ArcCenterX;
                this.circleCmdLine.Center.Y = Properties.Settings.Default.ArcCenterY;
                this.circleCmdLine.Degree = MathUtils.CalculateCircleDegree(this.circleCmdLine.Start, this.circleCmdLine.Middle,
                    this.circleCmdLine.End, this.circleCmdLine.Center);
                this.circleCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.ArcLineStyle;
                this.circleCmdLine.IsWeightControl = Properties.Settings.Default.ArcIsWt;
                this.circleCmdLine.Weight = Properties.Settings.Default.ArcWt;
            }
            start.X = this.circleCmdLine.Start.X;
            start.Y = this.circleCmdLine.Start.Y;
            middle.X = this.circleCmdLine.Middle.X;
            middle.Y = this.circleCmdLine.Middle.Y;
            end.X = this.circleCmdLine.End.X;
            end.Y = this.circleCmdLine.End.Y;
            center.X = this.circleCmdLine.Center.X;
            center.Y = this.circleCmdLine.Center.Y;
            //系统坐标->机械坐标
            PointD pS = this.pattern.MachineRel(this.circleCmdLine.Start);
            PointD pM = this.pattern.MachineRel(this.circleCmdLine.Middle);
            PointD pE = this.pattern.MachineRel(this.circleCmdLine.End);
            PointD pC = this.pattern.MachineRel(this.circleCmdLine.Center);
            // 设置UI数据显示
            rbSME.Checked = true; // default
            tbStartX.Text = pS.X.ToString("0.000");
            tbStartY.Text = pS.Y.ToString("0.000");
            tbMiddleX.Text = pM.X.ToString("0.000");
            tbMiddleY.Text = pM.Y.ToString("0.000");
            tbEndX.Text = pE.X.ToString("0.000");
            tbEndY.Text = pE.Y.ToString("0.000");
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                comboBoxLineType.Items.Add("Type " + (i + 1));
            }
            comboBoxLineType.SelectedIndex = (int)this.circleCmdLine.LineStyle;
            comboBoxDegree.SelectedIndex = this.circleCmdLine.Degree == 360 ? 0 : 1;
            cbWeightControl.Checked = this.circleCmdLine.IsWeightControl;
            tbWeight.Text = this.circleCmdLine.Weight.ToString("0.000");
            if (this.circleCmdLine != null)
            {
                circleCmdLineBackUp = (CircleCmdLine)this.circleCmdLine.Clone();
            }
            //this.ReadLanguageResources();
        }

        private void rbSME_CheckedChanged(object sender, System.EventArgs e)
        {
            tbStartX.Enabled = true;
            tbStartY.Enabled = true;
            btnSelectStart.Enabled = true;
            btnStartGoTo.Enabled = true;
            tbMiddleX.Enabled = true;
            tbMiddleY.Enabled = true;
            btnSelectMiddle.Enabled = true;
            btnMiddleGoTo.Enabled = true;
            tbEndX.Enabled = true;
            tbEndY.Enabled = true;
            btnSelectEnd.Enabled = true;
            btnEndGoTo.Enabled = true;
            tbCenterX.Enabled = false;
            tbCenterY.Enabled = false;
            btnSelectCenter.Enabled = false;
            btnCenterGoTo.Enabled = false;
            comboBoxDegree.Enabled = false;
            permitReacting = false;
            tbCenterX.Text = tbCenterX.Value.ToString("0.000");
            tbCenterY.Text = tbCenterY.Value.ToString("0.000");
            permitReacting = true;
        }

        private void rbCenter_CheckedChanged(object sender, System.EventArgs e)
        {
            tbStartX.Enabled = true;
            tbStartY.Enabled = true;
            btnSelectStart.Enabled = true;
            btnStartGoTo.Enabled = true;
            tbMiddleX.Enabled = false;
            tbMiddleY.Enabled = false;
            btnSelectMiddle.Enabled = false;
            btnMiddleGoTo.Enabled = false;
            tbEndX.Enabled = false;
            tbEndY.Enabled = false;
            btnSelectEnd.Enabled = false;
            btnEndGoTo.Enabled = false;
            tbCenterX.Enabled = true;
            tbCenterY.Enabled = true;
            btnSelectCenter.Enabled = true;
            btnCenterGoTo.Enabled = true;
            comboBoxDegree.Enabled = true;
            permitReacting = false;
            tbMiddleX.Text = tbMiddleX.Value.ToString("0.000");
            tbMiddleY.Text = tbMiddleY.Value.ToString("0.000");
            tbEndX.Text = tbEndX.Value.ToString("0.000");
            tbEndY.Text = tbEndY.Value.ToString("0.000");
            permitReacting = true;
        }

        private void tbStartX_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbStartX.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            start = this.pattern.SystemRel(tbStartX.Value, tbStartY.Value);
            if (rbSME.Checked)
            {
                // 重新计算圆心 和 degree
                center = MathUtils.CalculateCircleCenter(start, middle, end);
                double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
                permitReacting = false;
                comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
                //系统坐标->机械坐标
                PointD pC = this.pattern.MachineRel(center);
                tbCenterX.Text = pC.X.ToString("0.000");
                tbCenterY.Text = pC.Y.ToString("0.000");
                permitReacting = true;
            }
            else // center
            {
                // 重新计算 middle end
                PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center,
                    comboBoxDegree.SelectedIndex == 0 ? 360 : -360);
                middle.X = points[0].X;
                middle.Y = points[0].Y;
                end.X = points[1].X;
                end.Y = points[1].Y;
                permitReacting = false;
                //系统坐标->机械坐标
                PointD pM = this.pattern.MachineRel(middle);
                PointD pE = this.pattern.MachineRel(end);
                tbMiddleX.Text = pM.X.ToString("0.000");
                tbMiddleY.Text = pM.Y.ToString("0.000");
                tbEndX.Text = pE.X.ToString("0.000");
                tbEndY.Text = pE.Y.ToString("0.000");
                permitReacting = true;
            }
        }

        private void tbStartY_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbStartY.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            start = this.pattern.SystemRel(tbStartX.Value, tbStartY.Value);
            if (rbSME.Checked)
            {
                // 重新计算圆心 和 degree
                center = MathUtils.CalculateCircleCenter(start, middle, end);
                double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
                permitReacting = false;
                comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
                //系统坐标->机械坐标
                PointD pC = this.pattern.MachineRel(center);
                tbCenterX.Text = pC.X.ToString("0.000");
                tbCenterY.Text = pC.Y.ToString("0.000");
                permitReacting = true;
            }
            else
            {
                // 重新计算 middle end
                PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center,
                    comboBoxDegree.SelectedIndex == 0 ? 360 : -360);
                middle.X = points[0].X;
                middle.Y = points[0].Y;
                end.X = points[1].X;
                end.Y = points[1].Y;
                permitReacting = false;
                //系统坐标->机械坐标
                PointD pM = this.pattern.MachineRel(middle);
                PointD pE = this.pattern.MachineRel(end);
                tbMiddleX.Text = pM.X.ToString("0.000");
                tbMiddleY.Text = pM.Y.ToString("0.000");
                tbEndX.Text = pE.X.ToString("0.000");
                tbEndY.Text = pE.Y.ToString("0.000");
                permitReacting = true;
            }
        }

        private void tbMiddleX_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbMiddleX.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            middle = this.pattern.SystemRel(tbMiddleX.Value, tbMiddleY.Value);
            // 重新计算圆心和 degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
            permitReacting = false;
            comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            permitReacting = true;
        }

        private void tbMiddleY_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbMiddleY.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            middle = this.pattern.SystemRel(tbMiddleX.Value, tbMiddleY.Value);
            // 重新计算圆心和degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
            permitReacting = false;
            comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            permitReacting = true;
        }

        private void tbEndX_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbEndX.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            end = this.pattern.SystemRel(tbEndX.Value, tbEndY.Value);
            // 重新计算圆心和degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
            permitReacting = false;
            comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            permitReacting = true;
        }

        private void tbEndY_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbEndY.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            end = this.pattern.SystemRel(tbEndX.Value, tbEndY.Value);
            // 重新计算圆心和degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            double degree = MathUtils.CalculateCircleDegree(start, middle, end, center);
            permitReacting = false;
            comboBoxDegree.SelectedIndex = degree == 360 ? 0 : 1;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            permitReacting = true;
        }

        private void tbCenterX_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbCenterX.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            center = this.pattern.SystemRel(tbCenterX.Value, tbCenterY.Value);
            // 重新计算 middle end
            PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center,
                comboBoxDegree.SelectedIndex == 0 ? 360 : -360);
            middle.X = points[0].X;
            middle.Y = points[0].Y;
            end.X = points[1].X;
            end.Y = points[1].Y;
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pM = this.pattern.MachineRel(middle);
            PointD pE = this.pattern.MachineRel(end);
            tbMiddleX.Text = pM.X.ToString("0.000");
            tbMiddleY.Text = pM.Y.ToString("0.000");
            tbEndX.Text = pE.X.ToString("0.000");
            tbEndY.Text = pE.Y.ToString("0.000");
            permitReacting = true;
        }

        private void tbCenterY_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbCenterY.IsValid)
            {
                return;
            }
            //机械坐标->系统坐标
            center = this.pattern.SystemRel(tbCenterX.Value, tbCenterY.Value);
            // 重新计算 middle end
            PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center,
                comboBoxDegree.SelectedIndex == 0 ? 360 : -360);
            middle.X = points[0].X;
            middle.Y = points[0].Y;
            end.X = points[1].X;
            end.Y = points[1].Y;
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pM = this.pattern.MachineRel(middle);
            PointD pE = this.pattern.MachineRel(end);
            tbMiddleX.Text = pM.X.ToString("0.000");
            tbMiddleY.Text = pM.Y.ToString("0.000");
            tbEndX.Text = pE.X.ToString("0.000");
            tbEndY.Text = pE.Y.ToString("0.000");
            permitReacting = true;
        }

        private void btnSelectStart_Click(object sender, System.EventArgs e)
        {
            tbStartX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbStartY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectMiddle_Click(object sender, System.EventArgs e)
        {
            tbMiddleX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbMiddleY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectEnd_Click(object sender, System.EventArgs e)
        {
            tbEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectCenter_Click(object sender, System.EventArgs e)
        {
            tbCenterX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbCenterY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnStartGoTo_Click(object sender, System.EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
        }

        private void btnMiddleGoTo_Click(object sender, System.EventArgs e)
        {
            if (!tbMiddleX.IsValid || !tbMiddleY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbMiddleX.Value, origin.Y + tbMiddleY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbMiddleX.Value, origin.Y + tbMiddleY.Value);
        }

        private void btnEndGoTo_Click(object sender, System.EventArgs e)
        {
            if (!tbEndX.IsValid || !tbEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
        }

        private void btnCenterGoTo_Click(object sender, System.EventArgs e)
        {
            if (!tbCenterX.IsValid || !tbCenterY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbCenterX.Value, origin.Y + tbCenterY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbCenterX.Value, origin.Y + tbCenterY.Value);
        }

        private void btnEditLineParams_Click(object sender, System.EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid || !tbMiddleX.IsValid || !tbMiddleY.IsValid
                || !tbEndX.IsValid || !tbEndY.IsValid || !tbCenterX.IsValid || !tbCenterY.IsValid
                || !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid number.");
                MetroSetMessageBox.Show(this, "请输入合理的值");
                return;
            }
            if (tbStartX.Value == tbMiddleX.Value && tbStartY.Value == tbMiddleY.Value)
            {
                //MessageBox.Show("Start point cannot be same with middle point.");
                MetroSetMessageBox.Show(this, "起始点和中间点不可以相同");
                return;
            }
            if (tbEndX.Value == tbMiddleX.Value && tbEndY.Value == tbMiddleY.Value)
            {
                //MessageBox.Show("Middle point cannot be same with end point.");
                MetroSetMessageBox.Show(this, "中间点和结束点不可以相同");
                return;
            }
            if ((tbCenterX.Value == tbStartX.Value && tbCenterY.Value == tbStartY.Value)
                || (tbCenterX.Value == tbMiddleX.Value && tbCenterY.Value == tbMiddleY.Value)
                || (tbCenterX.Value == tbEndX.Value && tbCenterY.Value == tbEndY.Value))
            {
                //MessageBox.Show("Center point cannot be same with SME point.");
                MetroSetMessageBox.Show(this, "中心点SME点不可以相同");
                return;
            }
            circleCmdLine.Start.X = start.X;
            circleCmdLine.Start.Y = start.Y;
            circleCmdLine.Middle.X = center.X - (start.X - center.X);
            circleCmdLine.Middle.Y = center.Y - (start.Y - center.Y);
            circleCmdLine.End.X = start.X;
            circleCmdLine.End.Y = start.Y;
            circleCmdLine.Center.X = center.X;
            circleCmdLine.Center.Y = center.Y;
            circleCmdLine.Degree = comboBoxDegree.SelectedIndex == 0 ? 360 : -360;
            circleCmdLine.LineStyle = (LineStyle)comboBoxLineType.SelectedIndex;
            circleCmdLine.IsWeightControl = cbWeightControl.Checked;
            if (cbWeightControl.Checked)
            {
                circleCmdLine.Weight = tbWeight.Value;
            }
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, circleCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, circleCmdLine);
            }

            Properties.Settings.Default.ArcStartX = circleCmdLine.Start.X;
            Properties.Settings.Default.ArcStartY = circleCmdLine.Start.Y;
            Properties.Settings.Default.ArcMidX = circleCmdLine.Middle.X;
            Properties.Settings.Default.ArcMidY = circleCmdLine.Middle.Y;
            Properties.Settings.Default.ArcEndX = circleCmdLine.End.X;
            Properties.Settings.Default.ArcEndY = circleCmdLine.End.Y;
            Properties.Settings.Default.ArcCenterX = circleCmdLine.Center.X;
            Properties.Settings.Default.ArcCenterY = circleCmdLine.Center.Y;
            Properties.Settings.Default.ArcDegree = circleCmdLine.Degree;
            Properties.Settings.Default.ArcLineStyle = (int)circleCmdLine.LineStyle;
            Properties.Settings.Default.ArcIsWt = circleCmdLine.IsWeightControl;
            Properties.Settings.Default.ArcWt = circleCmdLine.Weight;
            if (this.isCreating)
            {
                return;
            }
            if (this.circleCmdLine != null && this.circleCmdLineBackUp != null)
            {
                CompareObj.CompareMember(this.circleCmdLine, this.circleCmdLineBackUp, null, this.GetType().Name, true);
            }
        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
            this.tbWeight.Enabled = this.cbWeightControl.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnSelectStart);
            buttons.Add(this.btnSelectMiddle);
            buttons.Add(this.btnSelectEnd);
            buttons.Add(this.btnSelectCenter);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
