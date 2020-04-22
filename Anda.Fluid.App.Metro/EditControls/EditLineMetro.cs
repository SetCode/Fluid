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
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditLineMetro : MetroSetUserControl, IMsgSender, ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private bool isRunning;
        private LineCmdLine lineCmdLine;
        private LineCmdLine lineCmdLineBackUp;
        private LineCoordinate lineCoordinate;
        private Line line;
        private double offset;
        private double offsetMax = 5.0;
        private double offsetMin = -5.0;

        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditLineMetro()
        {
            InitializeComponent();
        }

        public EditLineMetro(Pattern pattern) : this(pattern, null)
        {
        }

        public EditLineMetro(Pattern pattern, LineCmdLine lineCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (lineCmdLine == null)
            {
                isCreating = true;
                this.lineCmdLine = new LineCmdLine() { LineMethod = LineMethod.Single };
                this.lineCoordinate = new LineCoordinate(new PointD(), new PointD());
                this.lineCoordinate.Start.X = Properties.Settings.Default.LineStartX;
                this.lineCoordinate.Start.Y = Properties.Settings.Default.LineStartY;
                this.lineCoordinate.End.X = Properties.Settings.Default.LineEndX;
                this.lineCoordinate.End.Y = Properties.Settings.Default.LineEndY;
                this.lineCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.LineStyle;
                this.lineCmdLine.IsWeightControl = Properties.Settings.Default.LineIsWt;
                this.lineCmdLine.WholeWeight = Properties.Settings.Default.LineWt;
            }
            else
            {
                isCreating = false;
                this.lineCmdLine = lineCmdLine;
                this.lineCoordinate = this.lineCmdLine.LineCoordinateList[0];
            }
            PointD startMachine = this.pattern.MachineRel(this.lineCoordinate.Start);
            PointD endMachine = this.pattern.MachineRel(this.lineCoordinate.End);
            this.tbStartX.Text = startMachine.X.ToString("0.000");
            this.tbStartY.Text = startMachine.Y.ToString("0.000");
            this.tbEndX.Text = endMachine.X.ToString("0.000");
            this.tbEndY.Text = endMachine.Y.ToString("0.000");
            this.offset = this.lineCoordinate.LookOffset;
            this.nudOffset.Value = (decimal)this.lineCoordinate.LookOffset;
            this.nudOffset.Enabled = false;
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                this.disableUI();
                this.nudOffset.Enabled = true;
            }

            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                comboBoxLineType.Items.Add("Type " + (i + 1));
            }
            comboBoxLineType.SelectedIndex = (int)this.lineCmdLine.LineStyle;
            cbWeightControl.Checked = this.lineCmdLine.IsWeightControl;
            if (this.lineCmdLine.IsWeightControl)
            {
                tbWeight.Text = this.lineCmdLine.WholeWeight.ToString("0.000");
            }

            this.cbInspectionKey.Items.Add(InspectionKey.Line1);
            this.cbInspectionKey.Items.Add(InspectionKey.Line2);
            this.cbInspectionKey.Items.Add(InspectionKey.Line3);
            this.cbInspectionKey.Items.Add(InspectionKey.Line4);
            this.cbInspectionKey.SelectedIndex = 0;
            if (this.pattern.IsReversePattern)
            {
                this.nudOffset.Value = (decimal)this.lineCoordinate.LookOffsetRevs;
            }
            else
            {
                this.nudOffset.Value = (decimal)this.lineCoordinate.LookOffset;
            }
            if (this.lineCmdLine != null)
            {
                this.lineCmdLineBackUp = (LineCmdLine)this.lineCmdLine.Clone();
            }

        }

        public EditLineMetro(Pattern pattern, LineCmdLine lineCmdLine, Line line)
            : this(pattern, lineCmdLine)
        {
            this.line = line;
            this.isRunning = true;
        }

        private void disableUI()
        {
            this.tbStartX.Enabled = false;
            this.tbStartY.Enabled = false;
            this.tbEndX.Enabled = false;
            this.tbEndY.Enabled = false;
            this.btnSelectStart.Enabled = false;
            this.btnSelectEnd.Enabled = false;
            this.comboBoxLineType.Enabled = false;
            this.btnEditLineParams.Enabled = false;
            this.cbWeightControl.Enabled = false;
            this.tbWeight.Enabled = false;
        }
        private void btnEditLineParams_Click(object sender, System.EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void btnSelectStart_Click(object sender, EventArgs e)
        {
            tbStartX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbStartY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectEnd_Click(object sender, EventArgs e)
        {
            tbEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGotoStart_Click(object sender, EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
        }

        private void btnGoToEnd_Click(object sender, EventArgs e)
        {
            if (!tbEndX.IsValid || !tbEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                //偏移量调整
                Line.WaitMsg.Set();
                //TODO
            }
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (cbWeightControl.Checked && !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid weight values.");
                MetroSetMessageBox.Show(this, "请输入合理的胶重.");
                return;
            }
            if (!tbStartX.IsValid || !tbStartY.IsValid || !tbEndX.IsValid || !tbEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入正确的起始点和结束点值.");
                return;
            }
            if (tbStartX.Value == tbEndX.Value && tbStartY.Value == tbEndY.Value)
            {
                //MessageBox.Show("Start point cannot be same with end point.");
                MetroSetMessageBox.Show(this, "起始点和结束点不可以相同.");
                return;
            }

            PointD startMap = this.pattern.SystemRel(tbStartX.Value, tbStartY.Value);
            PointD endMap = this.pattern.SystemRel(tbEndX.Value, tbEndY.Value);
            this.lineCoordinate.Start.X = startMap.X;
            this.lineCoordinate.Start.Y = startMap.Y;
            this.lineCoordinate.End.X = endMap.X;
            this.lineCoordinate.End.Y = endMap.Y;
            this.lineCoordinate.Weight = tbWeight.Value;
            this.lineCoordinate.LineStyle = (LineStyle)comboBoxLineType.SelectedIndex;

            lineCmdLine.LineCoordinateList.Clear();
            lineCmdLine.LineCoordinateList.Add(this.lineCoordinate);
            lineCmdLine.LineStyle = (LineStyle)comboBoxLineType.SelectedIndex;
            lineCmdLine.IsWeightControl = cbWeightControl.Checked;
            if (lineCmdLine.IsWeightControl)
            {
                lineCmdLine.WholeWeight = tbWeight.Value;
            }

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                //保存调整值               
                if (pattern.IsReversePattern)
                {
                    this.lineCoordinate.LookOffsetRevs = this.offset;
                }
                else
                {
                    this.lineCoordinate.LookOffset = this.offset;
                }
            }

            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, lineCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, lineCmdLine);
            }

            Properties.Settings.Default.LineStartX = this.lineCoordinate.Start.X;
            Properties.Settings.Default.LineStartY = this.lineCoordinate.Start.Y;
            Properties.Settings.Default.LineEndX = this.lineCoordinate.End.X;
            Properties.Settings.Default.LineEndY = this.lineCoordinate.End.Y;
            Properties.Settings.Default.LineStyle = (int)lineCmdLine.LineStyle;
            Properties.Settings.Default.LineIsWt = lineCmdLine.IsWeightControl;
            Properties.Settings.Default.LineWt = lineCmdLine.WholeWeight;

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                Line.WaitMsg.Set();
            }

            if (this.isCreating)
            {
                return;
            }
            if (this.lineCmdLine != null && this.lineCmdLineBackUp != null)
            {
                CompareObj.CompareField(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                CompareObj.CompareProperty(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                for (int i = 0; i < this.lineCmdLine.LineCoordinateList.Count; i++)
                {
                    string pathRoot = this.GetType().Name + "\\lineCmdLine\\" + "LineCoordinateList: " + i.ToString();
                    CompareObj.CompareField(this.lineCmdLine.LineCoordinateList[i], this.lineCmdLineBackUp.LineCoordinateList[i], null, pathRoot, true);
                }
            }

        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
            this.tbWeight.Enabled = this.cbWeightControl.Checked;
        }

        private void btnOffset_Click(object sender, EventArgs e)
        {
            double value = 0.0;
            PointD start;
            VectorD vecSta2End;
            VectorD vecAdjust;
            PointD adjustedP = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            if (this.isRunning == true)
            {
                start = this.line.LineCoordinateList[0].Start;
                vecAdjust = (adjustedP - start);
                vecSta2End = this.line.LineCoordinateList[0].End - this.line.LineCoordinateList[0].Start;
            }
            else
            {
                start = origin + lineCoordinate.Start;
                vecAdjust = adjustedP - start;
                PointD end = origin + lineCoordinate.End;
                vecSta2End = end - start;
            }
            value = -(vecSta2End.DotProduct(vecAdjust) / (vecSta2End.Length));
            if (value > this.offsetMax || value < this.offsetMin)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的参数超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                value = 0.0;
            }
            this.nudOffset.Value = (decimal)value;
            this.offset = value;
        }

        private void nudOffset_ValueChanged(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(this.nudOffset.Value);
            if (value > this.offsetMax || value < this.offsetMin)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的参数超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                value = 0.0;
            }
            this.offset = value;
        }

        private void btnGotoOffset_Click(object sender, EventArgs e)
        {

            Machine.Instance.Robot.MoveSafeZ();
            VectorD vecSta2End;
            PointD start;
            PointD end;
            if (this.isRunning)
            {
                start = this.line.LineCoordinateList[0].Start;
                vecSta2End = this.line.LineCoordinateList[0].End - this.line.LineCoordinateList[0].Start;
            }
            else
            {
                start = origin + lineCoordinate.Start;
                end = origin + lineCoordinate.End;
                vecSta2End = end - start;
            }

            if (Math.Abs(this.offset) > 5)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的参数超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                return;
            }
            VectorD offsetVec = vecSta2End.Normalize() * this.offset;

            //Machine.Instance.Robot.MovePosXY(offsetVec.X + start.X, offsetVec.Y + start.Y);
            Machine.Instance.Robot.ManualMovePosXY(offsetVec.X + start.X, offsetVec.Y + start.Y);
        }

        private void EditSingleLineForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Line.WaitMsg.Set();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbInspectionKey.SelectedIndex < 0)
            {
                return;
            }
            this.lineCmdLine.inspectionKey = (InspectionKey)this.cbInspectionKey.SelectedItem;
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnSelectStart);
            buttons.Add(this.btnSelectEnd);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
