using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
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
    public partial class EditArcForm1 : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private ArcCmdLine arcCmdLine;
        private ArcCmdLine arcCmdLineBackUp;
        private bool isCreating;
        private PointD start = new PointD();
        private PointD middle = new PointD();
        private PointD end = new PointD();
        private PointD center = new PointD();
        private double degree;
        private bool permitReacting = true;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditArcForm1()
        {
            InitializeComponent();
        }

        public EditArcForm1(Pattern pattern) : this(pattern, null)
        {
        }

        public EditArcForm1(Pattern pattern, ArcCmdLine arcCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            this.arcCmdLine = arcCmdLine;
            isCreating = arcCmdLine == null;
            if (isCreating)
            {
                this.arcCmdLine = new ArcCmdLine();

                this.arcCmdLine.Start.X = Properties.Settings.Default.ArcStartX;
                this.arcCmdLine.Start.Y = Properties.Settings.Default.ArcStartY;
                this.arcCmdLine.Middle.X = Properties.Settings.Default.ArcMidX;
                this.arcCmdLine.Middle.Y = Properties.Settings.Default.ArcMidY;
                this.arcCmdLine.End.X = Properties.Settings.Default.ArcEndX;
                this.arcCmdLine.End.Y = Properties.Settings.Default.ArcEndY;
                this.arcCmdLine.Center.X = Properties.Settings.Default.ArcCenterX;
                this.arcCmdLine.Center.Y = Properties.Settings.Default.ArcCenterY;
                this.arcCmdLine.Degree = Properties.Settings.Default.ArcDegree;
                this.arcCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.ArcLineStyle;
                this.arcCmdLine.IsWeightControl = Properties.Settings.Default.ArcIsWt;
                this.arcCmdLine.Weight = Properties.Settings.Default.ArcWt;
                this.btnLastCmdLine.Visible = false;
                this.btnNextCmdLine.Visible = false;
            }
            start.X = this.arcCmdLine.Start.X;
            start.Y = this.arcCmdLine.Start.Y;
            middle.X = this.arcCmdLine.Middle.X;
            middle.Y = this.arcCmdLine.Middle.Y;
            end.X = this.arcCmdLine.End.X;
            end.Y = this.arcCmdLine.End.Y;
            center.X = this.arcCmdLine.Center.X;
            center.Y = this.arcCmdLine.Center.Y;
            degree = this.arcCmdLine.Degree;
            //系统坐标->机械坐标
            PointD pS = this.pattern.MachineRel(start);
            PointD pM = this.pattern.MachineRel(middle);
            PointD pE = this.pattern.MachineRel(end);
            PointD pC = this.pattern.MachineRel(center);
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
            tbDegree.Text = degree.ToString("0.000");
            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                comboBoxLineType.Items.Add("Type " + (i + 1));
            }
            comboBoxLineType.SelectedIndex = (int)this.arcCmdLine.LineStyle;
            cbWeightControl.Checked = this.arcCmdLine.IsWeightControl;
            tbWeight.Text = this.arcCmdLine.Weight.ToString("0.000");
            if (this.arcCmdLine != null)
            {
                this.arcCmdLineBackUp = (ArcCmdLine)this.arcCmdLine.Clone();
            }
            this.ReadLanguageResources();
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
            tbDegree.Enabled = false;
            permitReacting = false;
            tbCenterX.Text = tbCenterX.Value.ToString("0.000");
            tbCenterY.Text = tbCenterY.Value.ToString("0.000");
            tbDegree.Text = tbDegree.Value.ToString("0.000");
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
            tbDegree.Enabled = true;
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
                degree = MathUtils.CalculateDegree(start, middle, end, center);
                permitReacting = false;
                //系统坐标->机械坐标
                PointD pC = this.pattern.MachineRel(center);
                tbCenterX.Text = pC.X.ToString("0.000");
                tbCenterY.Text = pC.Y.ToString("0.000");
                tbDegree.Text = degree.ToString("0.000");
                permitReacting = true;
            }
            else // center
            {
                // 重新计算 middle end
                PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center, degree);
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
                degree = MathUtils.CalculateDegree(start, middle, end, center);
                permitReacting = false;
                //系统坐标->机械坐标
                PointD pC = this.pattern.MachineRel(center);
                tbCenterX.Text = pC.X.ToString("0.000");
                tbCenterY.Text = pC.Y.ToString("0.000");
                tbDegree.Text = degree.ToString("0.000");
                permitReacting = true;
            }
            else
            {
                // 重新计算 middle end
                PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center, degree);
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
            // 重新计算圆心 和 degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            degree = MathUtils.CalculateDegree(start, middle, end, center);
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            tbDegree.Text = degree.ToString("0.000");
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
            // 重新计算圆心 和 degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            degree = MathUtils.CalculateDegree(start, middle, end, center);
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            tbDegree.Text = degree.ToString("0.000");
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
            // 重新计算圆心 和 degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            degree = MathUtils.CalculateDegree(start, middle, end, center);
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            tbDegree.Text = degree.ToString("0.000");
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
            // 重新计算圆心 和 degree
            center = MathUtils.CalculateCircleCenter(start, middle, end);
            degree = MathUtils.CalculateDegree(start, middle, end, center);
            permitReacting = false;
            //系统坐标->机械坐标
            PointD pC = this.pattern.MachineRel(center);
            tbCenterX.Text = pC.X.ToString("0.000");
            tbCenterY.Text = pC.Y.ToString("0.000");
            tbDegree.Text = degree.ToString("0.000");
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
            PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center, degree);
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
            PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center, degree);
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

        private void tbDegree_TextChanged(object sender, System.EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbDegree.IsValid)
            {
                return;
            }
            degree = tbDegree.Value;
            // 重新计算 middle end
            PointD[] points = MathUtils.CalculateMiddleAndEndPoint(start, center, degree);
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

        private void btnStartGoTo_Click(object sender, EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
        }

        private void btnMiddleGoTo_Click(object sender, EventArgs e)
        {
            if (!tbMiddleX.IsValid || !tbMiddleY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbMiddleX.Value, origin.Y + tbMiddleY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbMiddleX.Value, origin.Y + tbMiddleY.Value);
        }

        private void btnEndGoTo_Click(object sender, EventArgs e)
        {
            if (!tbEndX.IsValid || !tbEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
        }

        private void btnCenterGoTo_Click(object sender, EventArgs e)
        {
            if (!tbCenterX.IsValid || !tbCenterY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbCenterX.Value, origin.Y + tbCenterY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbCenterX.Value, origin.Y + tbCenterY.Value);
        }

        private void btnEditLineParams_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid || !tbMiddleX.IsValid || !tbMiddleY.IsValid
                || !tbEndX.IsValid || !tbEndY.IsValid || !tbCenterX.IsValid || !tbCenterY.IsValid
                || !tbDegree.IsValid || !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid number.");
                MessageBox.Show("请输入正确的值");
                return;
            }
            if (tbStartX.Value == tbMiddleX.Value && tbStartY.Value == tbMiddleY.Value)
            {
                //MessageBox.Show("Start point cannot be same with middle point.");
                MessageBox.Show("起始点和中间点不可以相同");
                return;
            }
            if (tbStartX.Value == tbEndX.Value && tbStartY.Value == tbEndY.Value)
            {
                //MessageBox.Show("Start point cannot be same with end point.");
                MessageBox.Show("起始点和结束点不可以相同");
                return;
            }
            if (tbEndX.Value == tbMiddleX.Value && tbEndY.Value == tbMiddleY.Value)
            {
                //MessageBox.Show("Middle point cannot be same with end point.");
                MessageBox.Show("中间点和结束点不可以相同");
                return;
            }
            if ((tbCenterX.Value == tbStartX.Value && tbCenterY.Value == tbStartY.Value)
                || (tbCenterX.Value == tbMiddleX.Value && tbCenterY.Value == tbMiddleY.Value)
                || (tbCenterX.Value == tbEndX.Value && tbCenterY.Value == tbEndY.Value))
            {
                //MessageBox.Show("Center point cannot be same with SME point.");
                MessageBox.Show("中间点和SME点不可以相同");
                return;
            }
            arcCmdLine.Start.X = start.X;
            arcCmdLine.Start.Y = start.Y;
            arcCmdLine.Middle.X = middle.X;
            arcCmdLine.Middle.Y = middle.Y;
            arcCmdLine.End.X = end.X;
            arcCmdLine.End.Y = end.Y;
            arcCmdLine.Center.X = center.X;
            arcCmdLine.Center.Y = center.Y;
            arcCmdLine.Degree = degree;
            arcCmdLine.LineStyle = (LineStyle)comboBoxLineType.SelectedIndex;
            arcCmdLine.IsWeightControl = cbWeightControl.Checked;
            if (cbWeightControl.Checked)
            {
                arcCmdLine.Weight = tbWeight.Value;
            }
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, arcCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, arcCmdLine);
            }
            //CompareObj.CompareProperty(this.arcCmdLine, this.arcCmdLineBackUp, null, null, true);
            //CompareObj.CompareField(this.arcCmdLine, this.arcCmdLineBackUp, null, null, true);
        
            Properties.Settings.Default.ArcStartX = arcCmdLine.Start.X;
            Properties.Settings.Default.ArcStartY = arcCmdLine.Start.Y;
            Properties.Settings.Default.ArcMidX = arcCmdLine.Middle.X;
            Properties.Settings.Default.ArcMidY = arcCmdLine.Middle.Y;
            Properties.Settings.Default.ArcEndX = arcCmdLine.End.X;
            Properties.Settings.Default.ArcEndY = arcCmdLine.End.Y;
            Properties.Settings.Default.ArcCenterX = arcCmdLine.Center.X;
            Properties.Settings.Default.ArcCenterY = arcCmdLine.Center.Y;
            Properties.Settings.Default.ArcDegree = arcCmdLine.Degree;
            Properties.Settings.Default.ArcLineStyle = (int)arcCmdLine.LineStyle;
            Properties.Settings.Default.ArcIsWt = arcCmdLine.IsWeightControl;
            Properties.Settings.Default.ArcWt = arcCmdLine.Weight;
            if (!this.isCreating)
            {
                Close();
            }
            if (this.arcCmdLine!=null  && this.arcCmdLineBackUp!=null)
            {
                CompareObj.CompareMember(this.arcCmdLine, this.arcCmdLineBackUp, null, this.GetType().Name, true);
            }
            
        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
            this.tbWeight.Enabled = this.cbWeightControl.Checked;
        }

        private void btnNextCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchNextSymbol(this.arcCmdLine,this);
        }

        private void btnLastCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchLastSymbol(this.arcCmdLine,this);
        }
    }
}
