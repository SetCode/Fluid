using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
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
    public partial class EditMultiLinesForm : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private LineCmdLine lineCmdLine;
        private LineCmdLine lineCmdLineBackUp;
        private List<LineCoordinate> lineCoordinateCache = new List<LineCoordinate>();

        private Line line;       
        private int indexOfLineCoordinate;
        private bool isRunning;
        private double offset;
        private double offsetMax = 5.0;
        private double offsetMin = -5.0;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditMultiLinesForm()
        {
            InitializeComponent();
        }
        public EditMultiLinesForm(Pattern pattern) : this(pattern, null)
        {
        }

        public EditMultiLinesForm(Pattern pattern, LineCmdLine lineCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (lineCmdLine == null)
            {
                isCreating = true;
                this.lineCmdLine = new LineCmdLine();

                this.tbStartX.Text = Properties.Settings.Default.LineStartX.ToString("0.000");
                this.tbStartY.Text = Properties.Settings.Default.LineStartY.ToString("0.000");
                this.tbEndX.Text = Properties.Settings.Default.LineEndX.ToString("0.000");
                this.tbEndY.Text = Properties.Settings.Default.LineEndY.ToString("0.000");
                this.lineCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.LineStyle;
                this.lineCmdLine.IsWeightControl = Properties.Settings.Default.LineIsWt;
                this.lineCmdLine.WholeWeight = Properties.Settings.Default.LineWt;
                this.btnNextCmdLine.Visible = false;
                this.btnLastCmdLine.Visible = false;
            }
            else
            {
                isCreating = false;
                this.lineCmdLine = lineCmdLine;
                lineCoordinateCache.AddRange(this.lineCmdLine.LineCoordinateList);
            }
            //foreach (LineCoordinate coordinate in lineCoordinateCache)
            //{
            //    listBoxPoints.Items.Add(coordinate.ToString());
            //}
            //if (lineCoordinateCache.Count > 0)
            //{
            //    listBoxPoints.SelectedIndex = 0;
            //}
            this.LoadLines2ListBox();
            cbWeightControl.Checked = this.lineCmdLine.IsWeightControl;
            if (this.lineCmdLine != null)
            {
                this.lineCmdLineBackUp = (LineCmdLine)this.lineCmdLine.Clone();
            }
        }

        public EditMultiLinesForm(Pattern pattern, LineCmdLine lineCmdLine, Line line, int indexOfLineCoordinate) 
            :this(pattern,  lineCmdLine)
        {
            this.line = line;
            this.isRunning = true;
            this.indexOfLineCoordinate = indexOfLineCoordinate;
        }
        private void showSelectedLine()
        {
            if (this.indexOfLineCoordinate<0)
            {
                this.indexOfLineCoordinate = 0;
            }
            this.listBoxPoints.SetSelected(this.indexOfLineCoordinate, true);
            this.listBoxPoints.SelectedIndex = this.indexOfLineCoordinate;
            this.listBoxPoints_SelectedIndexChanged(null, null);
        }
    
        private void LoadLines2ListBox()
        {
            listBoxPoints.Items.Clear();
            foreach (var item in this.lineCoordinateCache)
            {
                listBoxPoints.Items.Add(string.Format("{0}:{1},{2}mg,{3}", listBoxPoints.Items.Count, item.LineStyle, item.Weight, item));
            }
            if (lineCoordinateCache.Count > 0)
            {
                listBoxPoints.SelectedIndex = 0;
            }
        }

        private void btnEditLineParams_Click(object sender, System.EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (listBoxPoints.SelectedItem == null)
            {
                return;
            }
            int selectedIndex = listBoxPoints.SelectedIndex;            
            lineCoordinateCache.RemoveAt(selectedIndex);
            this.LoadLines2ListBox();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid || !tbEndX.IsValid || !tbEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            if (tbStartX.Value == tbEndX.Value && tbStartY.Value == tbEndY.Value)
            {
                //MessageBox.Show("Start point cannot be same with end point.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            LineCoordinate coordinate = new LineCoordinate(tbStartX.Value, tbStartY.Value, tbEndX.Value, tbEndY.Value);
            lineCoordinateCache.Add(coordinate);
            listBoxPoints.Items.Add(string.Format("{0}:{1},{2}mg,{3}", listBoxPoints.Items.Count, coordinate.LineStyle, coordinate.Weight, coordinate));
            listBoxPoints.SelectedIndex = listBoxPoints.Items.Count - 1;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            { 
                Line.WaitMsg.Set();
            }
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
                if (lineCoordinateCache.Count <= 0)
                {
                    //MessageBox.Show("Line points is empty.");
                    MessageBox.Show("线轨迹点个数不可以为0.");
                    return;
                }
                lineCmdLine.LineCoordinateList.Clear();
                lineCmdLine.LineCoordinateList.AddRange(lineCoordinateCache);
                lineCmdLine.IsWeightControl = cbWeightControl.Checked;

                if (isCreating)
                {
                    MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, lineCmdLine);
                }
                else
                {
                    MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, lineCmdLine);
                }

                int count = lineCmdLine.LineCoordinateList.Count;
                if (count > 0)
                {
                    LineCoordinate lineCoordinate = lineCmdLine.LineCoordinateList[count - 1];
                    Properties.Settings.Default.LineStartX = lineCoordinate.Start.X;
                    Properties.Settings.Default.LineStartY = lineCoordinate.Start.Y;
                    Properties.Settings.Default.LineEndX = lineCoordinate.End.X;
                    Properties.Settings.Default.LineEndY = lineCoordinate.End.Y;
                    Properties.Settings.Default.LineStyle = (int)lineCmdLine.LineStyle;
                    Properties.Settings.Default.LineIsWt = lineCmdLine.IsWeightControl;
                    Properties.Settings.Default.LineWt = lineCmdLine.WholeWeight;
                }
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
                {
                    Line.WaitMsg.Set();
                }
                if (!this.isCreating)
                {
                    Close();
                    if (this.lineCmdLine != null && this.lineCmdLineBackUp != null)
                    {
                        CompareObj.CompareField(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                        CompareObj.CompareProperty(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                        if (this.lineCmdLine.LineCoordinateList.Count == this.lineCmdLineBackUp.LineCoordinateList.Count)
                        {

                            for (int i = 0; i < this.lineCmdLine.LineCoordinateList.Count; i++)
                            {
                                string pathRoot = this.GetType().Name + "\\lineCmdLine\\" + "LineCoordinateList: " + i.ToString();
                                CompareObj.CompareField(this.lineCmdLine.LineCoordinateList[i], this.lineCmdLineBackUp.LineCoordinateList[i], null, pathRoot, true);
                            }
                        }
                    }

                }
        }

        private void listBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPoints.SelectedIndex < 0)
            {
                return;
            }
            
            LineCoordinate lineCoordinate = lineCoordinateCache[listBoxPoints.SelectedIndex];
            tbStartX.Text = lineCoordinate.Start.X.ToString("0.000");
            tbStartY.Text = lineCoordinate.Start.Y.ToString("0.000");
            tbEndX.Text = lineCoordinate.End.X.ToString("0.000");
            tbEndY.Text = lineCoordinate.End.Y.ToString("0.000");
            this.indexOfLineCoordinate = listBoxPoints.SelectedIndex;
            if (this.pattern.IsReversePattern)
            {
                this.offset = lineCoordinate.LookOffsetRevs;
            }
            else
            {
                this.offset = lineCoordinate.LookOffset;
            }
            this.nudOffset.Value = (decimal)this.offset;
        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnEditWt_Click(object sender, EventArgs e)
        {
            if (new EditLineWeightForm(this.lineCmdLine, this.lineCoordinateCache).ShowDialog() == DialogResult.OK)
            {
                this.LoadLines2ListBox();
            }
        }

        private void btnLineType_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void disableUI()
        {
            this.tbStartX.Enabled = false;
            this.tbStartY.Enabled = false;
            this.btnSelectStart.Enabled = false;

            this.tbEndX.Enabled = false;
            this.tbEndY.Enabled = false;
            this.btnSelectEnd.Enabled = false;
            if (this.isRunning)
            {
                this.listBoxPoints.Enabled = false;
            }                
            this.btnAdd.Enabled = false;
            
            this.btnDelete.Enabled = false;
            this.cbWeightControl.Enabled = false;
            this.btnEditWt.Enabled = false;
            this.btnLineType.Enabled = false;
        }

        private void btnSelectOffset_Click(object sender, EventArgs e)
        {
            double value = 0.0;

            PointD start;
            VectorD vecSta2End;
            VectorD vecAdjust;
            PointD adjustedP = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            if (this.isRunning == true)
            {
                start = this.line.LineCoordinateList[this.indexOfLineCoordinate].Start;
                vecAdjust = (adjustedP - start);
                vecSta2End = this.line.LineCoordinateList[this.indexOfLineCoordinate].End - this.line.LineCoordinateList[this.indexOfLineCoordinate].Start;
            }
            else
            {
                start = origin + this.lineCoordinateCache[this.indexOfLineCoordinate].Start;
                vecAdjust = adjustedP - start;
                PointD end = origin + this.lineCoordinateCache[this.indexOfLineCoordinate].End;
                vecSta2End = end - start;
            }
            value = -(vecSta2End.DotProduct(vecAdjust) / (vecSta2End.Length));
            if (value > this.offsetMax || value < this.offsetMin)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的值超出范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                value = 0.0;
            }
            this.nudOffset.Value = (decimal)value;
            this.offset = value;
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                //保存调整值                
                if (pattern.IsReversePattern)
                {
                    this.lineCoordinateCache[this.indexOfLineCoordinate].LookOffsetRevs = this.offset;
                }
                else
                {
                    this.lineCoordinateCache[this.indexOfLineCoordinate].LookOffset = this.offset;
                }
            }
        }

        private void btnGoToOffset_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            VectorD vecSta2End;
            PointD start;
            PointD end;
            if (this.isRunning)
            {
                start = this.line.LineCoordinateList[this.indexOfLineCoordinate].Start;
                vecSta2End = this.line.LineCoordinateList[this.indexOfLineCoordinate].End - this.line.LineCoordinateList[this.indexOfLineCoordinate].Start;
            }
            else
            {
                start = origin + this.lineCoordinateCache[this.indexOfLineCoordinate].Start;
                end = origin + this.lineCoordinateCache[this.indexOfLineCoordinate].End;
                vecSta2End = end - start;
            }

            if (Math.Abs(this.offset) > 5)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的值超出范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                return;
            }
            VectorD offsetVec = vecSta2End.Normalize() * this.offset;
            
            Machine.Instance.Robot.ManualMovePosXY(offsetVec.X + start.X, offsetVec.Y + start.Y);

        }
        private void nudOffset_ValueChanged(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(this.nudOffset.Value);
            if (value > this.offsetMax || value < this.offsetMin)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的值超出范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                value = 0.0;
            }
            this.offset = value;
        }

        private void EditMultiLinesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Line.WaitMsg.Set();
        }

        private void EditMultiLinesForm_Load(object sender, EventArgs e)
        {
            this.nudOffset.Enabled = false;
            this.btnSelectOffset.Enabled = false;
            this.btnGoToOffset.Enabled = false;
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                this.disableUI();
                this.nudOffset.Enabled = true;
                this.btnSelectOffset.Enabled = true;
                this.btnGoToOffset.Enabled = true;
            }
            this.nudOffset.Value = (decimal)this.offset;
            if(!this.isCreating)
            {
                if (!this.isRunning)
                {
                    this.indexOfLineCoordinate = 0;
                }
                if (this.lineCoordinateCache.Count > 0)
                {
                    this.offset = this.lineCoordinateCache[indexOfLineCoordinate].LookOffset;
                }
                this.showSelectedLine();
            }
            

        }

        private void btnNextCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchNextSymbol(this.lineCmdLine, this);
        }

        private void btnLastCmdLine_Click(object sender, EventArgs e)
        {
            this.btnOk_Click(sender, e);
            SwitchSymbolForm.Instance.SwitchLastSymbol(this.lineCmdLine, this);
        }
    }
}
