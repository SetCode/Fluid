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
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditPolylineMetro : MetroSetUserControl, IMsgSender,ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private LineCmdLine lineCmdLine;
        private LineCmdLine lineCmdLineBackUp;
        private List<LineCoordinate> lineCoordinateCache = new List<LineCoordinate>();
        private List<PointD> linePointsCache = new List<PointD>();
        private Line line;
        private int indexOfLineCoordinate;
        private bool isRunning;
        private double offset;
        private double offsetMax = 5.0;
        private double offsetMin = -5.0;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditPolylineMetro()
        {
            InitializeComponent();
        }
        public EditPolylineMetro(Pattern pattern) : this(pattern, null)
        {
        }

        public EditPolylineMetro(Pattern pattern, LineCmdLine lineCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();

            if (lineCmdLine == null)
            {
                isCreating = true;
                this.lineCmdLine = new LineCmdLine() { LineMethod = LineMethod.Poly };
                this.lineCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.LineStyle;
                this.lineCmdLine.IsWeightControl = Properties.Settings.Default.LineIsWt;
                this.lineCmdLine.WholeWeight = Properties.Settings.Default.LineWt;
                //系统坐标->机械坐标
                PointD p = this.pattern.MachineRel(Properties.Settings.Default.LineEndX, Properties.Settings.Default.LineEndY);
                this.tbPointX.Text = p.X.ToString("0.000");
                this.tbPointY.Text = p.Y.ToString("0.000");
            }
            else
            {
                isCreating = false;
                this.lineCmdLine = lineCmdLine;
                lineCoordinateCache.AddRange(this.lineCmdLine.LineCoordinateList);
                for (int i = 0; i < lineCoordinateCache.Count; i++)
                {
                    if (i == 0)
                    {
                        this.linePointsCache.Add(lineCoordinateCache[i].Start);
                    }
                    this.linePointsCache.Add(lineCoordinateCache[i].End);
                }
            }

            //load points to listboxPoints
            this.LoadPoints2ListBox();
            //load lines to listboxLines
            this.LoadLines2ListBox();
            if (lineCoordinateCache.Count > 0)
            {
                listBoxLines.SelectedIndex = 0;
            }
            cbWeightControl.Checked = this.lineCmdLine.IsWeightControl;
            if (this.lineCmdLine != null)
            {
                this.lineCmdLineBackUp = (LineCmdLine)this.lineCmdLine.Clone();
            }
        }

        public EditPolylineMetro(Pattern pattern, LineCmdLine lineCmdLine, Line line, int indexOfLineCoordinate) : this(pattern, lineCmdLine)
        {
            this.line = line;
            this.isRunning = true;
            this.indexOfLineCoordinate = indexOfLineCoordinate;

        }
        private void showSelectedPoint()
        {
            if (this.indexOfLineCoordinate < 0)
            {
                this.indexOfLineCoordinate = 0;
            }
            this.listBoxPoints.SetSelected(this.indexOfLineCoordinate, true);
            this.listBoxPoints.SelectedIndex = this.indexOfLineCoordinate;
            this.listBoxPoints_SelectedIndexChanged(null, null);
        }

        private void showSelectedLine()
        {
            this.listBoxLines.SetSelected(this.indexOfLineCoordinate, true);
            this.listBoxLines.SelectedIndex = this.indexOfLineCoordinate;
            this.listBoxLines_SelectedIndexChanged(null, null);
        }

        private void LoadPoints2ListBox()
        {
            listBoxPoints.Items.Clear();
            foreach (var item in this.linePointsCache)
            {
                //系统坐标->机械坐标
                listBoxPoints.Items.Add(string.Format("{0}:{1}", listBoxPoints.Items.Count,
                    this.pattern.MachineRel(item)));
            }
            if (this.selectedIndex > this.listBoxPoints.Items.Count - 1)
            {
                this.selectedIndex = this.listBoxPoints.Items.Count - 1;
            }
            this.listBoxPoints.SelectedIndex = this.selectedIndex;
        }

        private void LoadLines2ListBox()
        {
            listBoxLines.Items.Clear();
            foreach (var item in this.lineCoordinateCache)
            {
                //系统坐标->机械坐标
                listBoxLines.Items.Add(string.Format("{0}:{1},{2}mg,{3}{4}", listBoxLines.Items.Count, item.LineStyle, item.Weight,
                    pattern.MachineRel(item.Start), pattern.MachineRel(item.End)));
            }

        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (listBoxPoints.SelectedItem == null)
            {
                return;
            }
            int selectedIndex = listBoxPoints.SelectedIndex;
            linePointsCache.RemoveAt(selectedIndex);
            this.LoadPoints2ListBox();

            lineCoordinateCache.Clear();
            if (linePointsCache.Count < 2)
            {
                return;
            }
            for (int i = 0; i < linePointsCache.Count - 1; i++)
            {
                lineCoordinateCache.Add(new LineCoordinate(linePointsCache[i], linePointsCache[i + 1]));
            }
            this.LoadLines2ListBox();
        }
        /// <summary>
        /// 修改点坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeachPoint_Click(object sender, EventArgs e)
        {
            if (listBoxPoints.SelectedItem == null)
            {
                return;
            }
            int selectedIndex = listBoxPoints.SelectedIndex;
            //修改            
            tbPointX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbPointY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值");
                return;
            }
            //机械坐标->系统坐标
            PointD newPoint = pattern.SystemRel(tbPointX.Value, tbPointY.Value);
            linePointsCache[selectedIndex] = newPoint;
            this.LoadPoints2ListBox();

            lineCoordinateCache.Clear();
            if (linePointsCache.Count < 2)
            {
                return;
            }
            for (int i = 0; i < linePointsCache.Count - 1; i++)
            {
                lineCoordinateCache.Add(new LineCoordinate(linePointsCache[i], linePointsCache[i + 1]));
            }
            this.LoadLines2ListBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbPointX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbPointY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            //机械坐标->系统坐标
            PointD newPoint = pattern.SystemRel(tbPointX.Value, tbPointY.Value);
            PointD lastPoint = null;
            if (this.linePointsCache.Count > 0)
            {
                lastPoint = this.linePointsCache[this.linePointsCache.Count - 1];
                if (newPoint == lastPoint)
                {
                    //MessageBox.Show("Start point cannot be same with end point.");
                    MessageBox.Show("起始点和终点不可以相同.");
                    return;
                }
                LineCoordinate line = new LineCoordinate(lastPoint, newPoint);
                lineCoordinateCache.Add(line);
                //系统坐标->机械坐标
                listBoxLines.Items.Add(string.Format("{0}:{1},{2}mg,{3}{4}", listBoxLines.Items.Count, line.LineStyle, line.Weight,
                    pattern.MachineRel(line.Start), pattern.MachineRel(line.End)));
            }

            linePointsCache.Add(newPoint);
            //系统坐标->机械坐标
            listBoxPoints.Items.Add(string.Format("{0}:{1}", listBoxPoints.Items.Count,
                pattern.MachineRel(newPoint)));
            listBoxPoints.SelectedIndex = listBoxPoints.Items.Count - 1;
        }

        private void btnGotoPoint_Click(object sender, EventArgs e)
        {
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            {
                Line.WaitMsg.Set();
                //TODO
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (lineCoordinateCache.Count <= 0)
            {
                //MessageBox.Show("Line points is empty.");
                MetroSetMessageBox.Show(this, "线轨迹上点个数为0.");
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
        }
        private int selectedIndex = -1;
        private void listBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPoints.SelectedIndex < 0)
            {
                return;
            }
            this.selectedIndex = listBoxPoints.SelectedIndex;
            //系统坐标->机械坐标
            PointD point = pattern.MachineRel(linePointsCache[listBoxPoints.SelectedIndex]);
            tbPointX.Text = point.X.ToString("0.000");
            tbPointY.Text = point.Y.ToString("0.000");
        }

        private void listBoxLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxLines.SelectedIndex < 0)
            {
                return;
            }
            LineCoordinate lineCoordinate = lineCoordinateCache[this.listBoxLines.SelectedIndex];
            //系统坐标->机械坐标
            PointD start = pattern.MachineRel(lineCoordinate.Start);
            PointD end = pattern.MachineRel(lineCoordinate.End);
            this.tbPointX.Text = start.X.ToString("0.000");
            this.tbPointY.Text = start.Y.ToString("0.000");
            if (this.pattern.IsReversePattern)
            {
                this.offset = lineCoordinate.LookOffsetRevs;
            }
            else
            {
                this.offset = lineCoordinate.LookOffset;
            }
            this.nudOffset.Value = (decimal)this.offset;
            this.indexOfLineCoordinate = listBoxLines.SelectedIndex;

        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEditWeight_Click(object sender, EventArgs e)
        {
            if (new EditLineWeightForm(this.lineCmdLine, this.lineCoordinateCache).ShowDialog() == DialogResult.OK)
            {
                this.LoadLines2ListBox();
            }
        }

        private void btnLineStyle_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void EditPolyLineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Line.WaitMsg.Set();
        }

        private void disableUI()
        {
            this.tbPointX.Enabled = false;
            this.tbPointY.Enabled = false;
            this.btnSelectStart.Enabled = false;
            btnAdd.Enabled = false;

            if (this.isRunning)
            {
                this.listBoxLines.Enabled = false;
            }
            this.cbWeightControl.Enabled = false;
            this.btnEditWeight.Enabled = false;
            this.btnLineStyle.Enabled = false;
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
                MessageBox.Show("设置的值超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
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
                MessageBox.Show("设置的值超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                return;
            }
            VectorD offsetVec = vecSta2End.Normalize() * this.offset;

            //Machine.Instance.Robot.MovePosXY(offsetVec.X + start.X, offsetVec.Y + start.Y);
            Machine.Instance.Robot.ManualMovePosXY(offsetVec.X + start.X, offsetVec.Y + start.Y);
        }

        private void EditPolyLineForm_Load(object sender, EventArgs e)
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
            if (!this.isCreating)
            {
                if (!this.isRunning)
                {
                    this.indexOfLineCoordinate = 0;
                }
                if (this.lineCoordinateCache.Count > 0)
                {
                    this.offset = this.lineCoordinateCache[indexOfLineCoordinate].LookOffset;
                }
                this.showSelectedPoint();
                this.showSelectedLine();
            }

        }

        private void nudOffset_ValueChanged(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(this.nudOffset.Value);
            if (value > this.offsetMax || value < this.offsetMin)
            {
                //MessageBox.Show("The setting offset is out of range[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                MessageBox.Show("设置的值超出了范围：[-5.0 5.0] ", null, MessageBoxButtons.OKCancel);
                value = 0.0;
            }
            this.offset = value;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnAdd);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
