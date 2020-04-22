using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
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
    public partial class EditMultiTracesForm : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private MultiTracesCmdLine multiTracesCmdLine;
        //private MultiTracesCmdLine multiTracesCmdLineBackup;
        //private List<TraceBase> tracesCache = new List<TraceBase>();
        private List<TraceBase> tracesCache = new List<TraceBase>();
        private List<PointTrace> pointsCache = new List<PointTrace>();
        private PointD tempStartPoint = new PointD();
        private PointD tempMidPoint = new PointD();
        private PointD tempEndPoint = new PointD();
      
        public EditMultiTracesForm()
        {
            InitializeComponent();
        }

        public EditMultiTracesForm(Pattern pattern) : this(pattern, null)
        {

        }

        public EditMultiTracesForm(Pattern pattern, MultiTracesCmdLine multiTracesCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();

            if (multiTracesCmdLine == null)
            {
                isCreating = true;
                this.multiTracesCmdLine = new MultiTracesCmdLine();
                this.multiTracesCmdLine.LineStyle = (LineStyle)Properties.Settings.Default.LineStyle;
                this.multiTracesCmdLine.IsWeightControl = Properties.Settings.Default.LineIsWt;
                this.multiTracesCmdLine.WholeWeight = Properties.Settings.Default.LineWt;
                //系统坐标->机械坐标
                //PointD p = this.pattern.MachineRel(Properties.Settings.Default.LineEndX, Properties.Settings.Default.LineEndY);
                //this.tbPointX.Text = p.X.ToString("0.000");
                //this.tbPointY.Text = p.Y.ToString("0.000");
                //this.btnLastCmdLine.Visible = false;
                //this.btnNextCmdLine.Visible = false;
            }
            else
            {
                isCreating = false;
                this.multiTracesCmdLine = multiTracesCmdLine;
            }
            foreach (var item in this.multiTracesCmdLine.Traces)
            {
                this.tracesCache.Add(item.Clone() as TraceBase);
            }
            this.updatePointsCache();
            //load points to listboxPoints
            this.LoadPoints2ListBox();
            if (this.pointsCache.Count > 0)
            {
                this.listBoxPoints.SelectedIndex = 0;
            }
            //load lines to listboxLines
            this.LoadLines2ListBox();
            if (this.tracesCache.Count > 0)
            {
                this.listBoxLines.SelectedIndex = 0;
            }
            //cbWeightControl.Checked = this.lineCmdLine.IsWeightControl;
            //if (this.lineCmdLine != null)
            //{
            //    this.lineCmdLineBackUp = (LineCmdLine)this.lineCmdLine.Clone();
            //}
            this.rbLine.Checked = true;
            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                comboBoxLineType.Items.Add("Type " + (i + 1));
            }
            if (this.tracesCache.Count > 0)
            {
                comboBoxLineType.SelectedIndex = this.tracesCache[0].LineStyle;
            }
            else
            {
                comboBoxLineType.SelectedIndex = 0;
            }
            this.tbOffsetX.Text = this.multiTracesCmdLine.OffsetX.ToString("0.000");
            this.tbOffsetY.Text = this.multiTracesCmdLine.OffsetY.ToString("0.000");
        }

        private void updatePointsCache()
        {
            this.pointsCache.Clear();
            for (int i = 0; i < this.tracesCache.Count; i++)
            {
                if (i == 0)
                {
                    this.pointsCache.Add(new PointTrace(this.tracesCache[i].Start, this.tracesCache[i], i, TracePointType.Start));
                }
                if (this.tracesCache[i] is TraceArc)
                {
                    this.pointsCache.Add(new PointTrace((this.tracesCache[i] as TraceArc).Mid, this.tracesCache[i], i, TracePointType.Mid));
                }
                this.pointsCache.Add(new PointTrace(this.tracesCache[i].End, this.tracesCache[i], i, TracePointType.End));
            }
        }

        private void LoadLines2ListBox()
        {
            this.listBoxLines.Items.Clear();
            foreach (var item in this.tracesCache)
            {
                //系统坐标->机械坐标
                if (item is TraceLine)
                {
                    this.listBoxLines.Items.Add(string.Format("LINE: {0} {1} {2}",
                        item.LineStyle + 1,
                        this.pattern.MachineRel(item.Start),
                        this.pattern.MachineRel(item.End)));
                }
                else
                {
                    this.listBoxLines.Items.Add(string.Format("ARC: {0} {1} {2} {3}",
                      item.LineStyle + 1,
                      this.pattern.MachineRel(item.Start),
                      this.pattern.MachineRel((item as TraceArc).Mid),
                      this.pattern.MachineRel(item.End)));
                }
            }
            if (this.tracesCache.Count > 0)
            {
                this.tbStartX.Enabled = false;
                this.tbStartY.Enabled = false;
                this.btnTeachStart.Enabled = false;
                this.btnGotoStart.Enabled = false;
                this.tempStartPoint.X = this.tracesCache[this.tracesCache.Count - 1].End.X;
                this.tempStartPoint.Y = this.tracesCache[this.tracesCache.Count - 1].End.Y;
                this.tbStartX.Text = this.tempStartPoint.X.ToString("0.000");
                this.tbStartY.Text = this.tempStartPoint.Y.ToString("0.000");
            }
        }

        private void LoadPoints2ListBox()
        {
            this.listBoxPoints.Items.Clear();
            for (int i = 0; i < this.pointsCache.Count; i++)
            {
                string traceType = string.Empty;
                if(i == 0)
                {
                    traceType = "START";
                }
                else
                {
                    if (this.pointsCache[i].Trace is TraceLine)
                    {
                        traceType = "LINE";
                    }
                    else
                    {
                        traceType = "ARC";
                    }
                }
                //系统坐标->机械坐标
                listBoxPoints.Items.Add(string.Format("{0}:{1} {2}", traceType, 
                    this.pointsCache[i].PointType.ToString(),
                    this.pattern.MachineRel(this.pointsCache[i].Point)));
            }
        }

        private void listBoxLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxLines.SelectedIndex < 0)
            {
                return;
            }
        }

        private void listBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxPoints.SelectedIndex < 0)
            {
                return;
            }
            if (this.listBoxLines.Items.Count > 0)
            {
                this.listBoxLines.SelectedIndex = this.pointsCache[this.listBoxPoints.SelectedIndex].TraceIndex;
            }
            PointD p = pattern.MachineAbs(this.pointsCache[this.listBoxPoints.SelectedIndex].Point);
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(p);
        }

        private void rbLine_CheckedChanged(object sender, EventArgs e)
        {
            this.tbMidX.Enabled = false;
            this.tbMidY.Enabled = false;
            this.btnTeachMid.Enabled = false;
            this.btnGotoMid.Enabled = false;
        }

        private void rbArc_CheckedChanged(object sender, EventArgs e)
        {
            this.tbMidX.Enabled = true;
            this.tbMidY.Enabled = true;
            this.btnTeachMid.Enabled = true;
            this.btnGotoMid.Enabled = true;
        }

        private void btnTeachStart_Click(object sender, EventArgs e)
        {
            tbStartX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbStartY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
            if (!tbStartX.IsValid || !tbStartY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            //机械坐标->系统坐标
            this.tempStartPoint = pattern.SystemRel(tbStartX.Value, tbStartY.Value);
        }

        private void btnTeachMid_Click(object sender, EventArgs e)
        {
            tbMidX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbMidY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
            if (!tbMidX.IsValid || !tbMidY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            //机械坐标->系统坐标
            this.tempMidPoint = pattern.SystemRel(tbMidX.Value, tbMidY.Value);
        }

        private void btnTeachEnd_Click(object sender, EventArgs e)
        {
            tbEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
            if (!tbEndX.IsValid || !tbEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            //机械坐标->系统坐标
            this.tempEndPoint = pattern.SystemRel(tbEndX.Value, tbEndY.Value);
            if (hasSamePoints())
            {
                MessageBox.Show("存在相同点，请检查.");
                return;
            }
            TraceBase newTrace = null;
            if(this.rbLine.Checked)
            {
                newTrace = new TraceLine(this.tempStartPoint.Clone() as PointD, this.tempEndPoint.Clone() as PointD);
            }
            else
            {
                newTrace = new TraceArc(this.tempStartPoint.Clone() as PointD, this.tempMidPoint.Clone() as PointD, this.tempEndPoint.Clone() as PointD);
            }
            newTrace.LineStyle = this.comboBoxLineType.SelectedIndex;
            this.tracesCache.Add(newTrace);
            this.updatePointsCache();
            this.LoadLines2ListBox();
            this.LoadPoints2ListBox();
            listBoxLines.SelectedIndex = listBoxLines.Items.Count - 1;
            listBoxPoints.SelectedIndex = listBoxPoints.Items.Count - 1;
            // 本段终点为下一段轨迹的起点
            this.tempStartPoint.X = this.tempEndPoint.X;
            this.tempStartPoint.Y = this.tempEndPoint.Y;
            this.tbStartX.Text = this.tempStartPoint.X.ToString("0.000");
            this.tbStartY.Text = this.tempStartPoint.Y.ToString("0.000");
            this.tbMidX.Text = this.tempMidPoint.X.ToString("0.000");
            this.tbMidY.Text = this.tempMidPoint.Y.ToString("0.000");
        }

        private bool hasSamePoints()
        {
            if(this.tempStartPoint != this.tempMidPoint
                && this.tempMidPoint != this.tempEndPoint
                && this.tempEndPoint != this.tempStartPoint)
            {
                return false;
            }
            return false;
        }

        private void btnGotoStart_Click(object sender, EventArgs e)
        {
            if (!tbStartX.IsValid || !tbStartY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbStartX.Value, origin.Y + tbStartY.Value);
        }

        private void btnGotoMid_Click(object sender, EventArgs e)
        {
            if (!tbMidX.IsValid || !tbMidY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbMidX.Value, origin.Y + tbMidY.Value);
        }

        private void btnGotoEnd_Click(object sender, EventArgs e)
        {
            if (!tbEndX.IsValid || !tbEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbEndX.Value, origin.Y + tbEndY.Value);
        }

        private void btnReTeach_Click(object sender, EventArgs e)
        {
            if(this.listBoxPoints.SelectedIndex < 0 || this.listBoxPoints.Items.Count == 0)
            {
                return;
            }

            int index = this.listBoxPoints.SelectedIndex;

            PointTrace selectedPointTrace = this.pointsCache[this.listBoxPoints.SelectedIndex];
            PointD p = pattern.SystemRel(Machine.Instance.Robot.PosX - origin.X, Machine.Instance.Robot.PosY - origin.Y);
            switch(selectedPointTrace.PointType)
            {
                case TracePointType.Start:
                    selectedPointTrace.Trace.Start.X = p.X;
                    selectedPointTrace.Trace.Start.Y = p.Y;
                    break;
                case TracePointType.Mid:
                    (selectedPointTrace.Trace as TraceArc).Mid.X = p.X;
                    (selectedPointTrace.Trace as TraceArc).Mid.Y = p.Y;
                    break;
                case TracePointType.End:
                    selectedPointTrace.Trace.End.X = p.X;
                    selectedPointTrace.Trace.End.Y = p.Y;
                    if (selectedPointTrace.TraceIndex < this.tracesCache.Count - 1)
                    {
                        this.tracesCache[selectedPointTrace.TraceIndex + 1].Start.X = p.X;
                        this.tracesCache[selectedPointTrace.TraceIndex + 1].Start.Y = p.Y;
                    }
                    break;
            }
            this.LoadLines2ListBox();
            this.LoadPoints2ListBox();

            this.SetListSelected(index);
        }

        private void btnEditLineParams_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void comboBoxLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxLines.SelectedIndex < 0)
            {
                return;
            }

            int index = this.listBoxLines.SelectedIndex;

            this.tracesCache[this.listBoxLines.SelectedIndex].LineStyle = this.comboBoxLineType.SelectedIndex;
            this.LoadLines2ListBox();

            this.listBoxLines.SelectedIndex = index;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.tracesCache.Count <= 0)
            {
                //MessageBox.Show("Line points is empty.");
                MessageBox.Show("轨迹数量为0.");
                return;
            }
            this.multiTracesCmdLine.Traces.Clear();
            foreach (var item in this.tracesCache)
            {
                this.multiTracesCmdLine.Traces.Add(item);
            }
            //lineCmdLine.IsWeightControl = cbWeightControl.Checked;
            this.multiTracesCmdLine.OffsetX = tbOffsetX.Value;
            this.multiTracesCmdLine.OffsetY = tbOffsetY.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, multiTracesCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, multiTracesCmdLine);
            }

            //if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
            //{
            //    Line.WaitMsg.Set();
            //}
            this.Close();
            if (!this.isCreating)
            {
                //Close();
                //if (this.lineCmdLine != null && this.lineCmdLineBackUp != null)
                //{
                //    CompareObj.CompareField(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                //    CompareObj.CompareProperty(this.lineCmdLine, this.lineCmdLineBackUp, null, this.GetType().Name, true);
                //    for (int i = 0; i < this.lineCmdLine.LineCoordinateList.Count; i++)
                //    {
                //        string pathRoot = this.GetType().Name + "\\lineCmdLine\\" + "LineCoordinateList: " + i.ToString();
                //        CompareObj.CompareField(this.lineCmdLine.LineCoordinateList[i], this.lineCmdLineBackUp.LineCoordinateList[i], null, pathRoot, true);
                //    }
                //}

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(this.listBoxPoints.SelectedIndex < 0)
            {
                return;
            }
            PointTrace pointTrace = pointsCache[this.listBoxPoints.SelectedIndex];
            TraceBase trace = this.tracesCache[pointTrace.TraceIndex];
            TraceBase traceNext = null;
            TraceBase traceNew = null;
            if (pointTrace.PointType == TracePointType.End)
            {
                // 删除连接点，则同时删除2条相邻轨迹，用一条直线轨迹替代
                if (this.tracesCache.Count - 1 >= pointTrace.TraceIndex + 1)
                {
                    traceNext = this.tracesCache[pointTrace.TraceIndex + 1];
                    traceNew = new TraceLine(trace.Start, traceNext.End);
                    traceNew.LineStyle = trace.LineStyle;
                }
                this.tracesCache.Remove(trace);
                if (traceNext != null)
                {
                    this.tracesCache.Remove(traceNext);
                    this.tracesCache.Insert(pointTrace.TraceIndex, traceNew);
                }
            }
            else if (pointTrace.PointType == TracePointType.Mid)
            {
                traceNew = new TraceLine(trace.Start, trace.End);
                traceNew.LineStyle = trace.LineStyle;
                this.tracesCache.Remove(trace);
                this.tracesCache.Insert(pointTrace.TraceIndex, traceNew);
            }
            this.updatePointsCache();
            this.LoadLines2ListBox();
            this.LoadPoints2ListBox();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.listBoxPoints.SelectedIndex < 0)
            {
                return;
            }
            PointTrace pointTrace = pointsCache[this.listBoxPoints.SelectedIndex];
            if (pointTrace.PointType != TracePointType.End)
            {
                return;
            }

            int index = this.listBoxPoints.SelectedIndex;

            PointTrace pointPrev = pointsCache[this.listBoxPoints.SelectedIndex - 1];
            PointD newPoint = (Machine.Instance.Robot.PosXY - origin).ToPoint();
            TraceBase trace = this.tracesCache[pointTrace.TraceIndex];

            TraceBase tracePrev;
            if (pointTrace.TraceIndex == 0)
            {
                tracePrev = null;
            }
            else
            {
                tracePrev = this.tracesCache[pointTrace.TraceIndex - 1];
            }
           
            if (this.rbLine.Checked)
            {
                trace.Start = newPoint.Clone() as PointD;
                TraceLine line;
                if (tracePrev == null)
                {
                    line = new TraceLine(pointPrev.Point.Clone() as PointD, newPoint.Clone() as PointD);
                    line.LineStyle = trace.LineStyle;
                }
                else
                {
                  line = new TraceLine(tracePrev.End.Clone() as PointD, newPoint.Clone() as PointD);
                  line.LineStyle = trace.LineStyle;
                }
                this.tracesCache.Insert(pointTrace.TraceIndex, line);
            }
            else
            {
                TraceArc arc = new TraceArc(trace.Start, newPoint, trace.End);
                arc.LineStyle = trace.LineStyle;
                this.tracesCache[pointTrace.TraceIndex] = arc;
            }
            this.updatePointsCache();
            this.LoadLines2ListBox();
            this.LoadPoints2ListBox();

            this.listBoxPoints.SelectedIndex = index + 1;
        }

        /// <summary>
        /// 设置pointslistBox某行被选中
        /// </summary>
        /// <param name="index"></param>
        private void SetListSelected(int index)
        {
            this.listBoxPoints.SelectedIndex = index;
        }


        private void prevKeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',' || e.KeyChar == '，')
            {
                if (this.listBoxPoints.SelectedIndex <= 0)
                {
                    return;
                }
                this.listBoxPoints.SelectedIndex -= 1;
                if (this.listBoxLines.Items.Count > 0)
                {
                    this.listBoxLines.SelectedIndex = this.pointsCache[this.listBoxPoints.SelectedIndex].TraceIndex;
                }
                PointD p = pattern.MachineAbs(this.pointsCache[this.listBoxPoints.SelectedIndex].Point);
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY(p);


            }

            else if (e.KeyChar == '。' || e.KeyChar == '.')
            {
                if (this.listBoxPoints.SelectedIndex >= this.listBoxPoints.Items.Count - 1)
                {
                    return;
                }
                this.listBoxPoints.SelectedIndex += 1;
                if (this.listBoxLines.Items.Count > 0)
                {
                    this.listBoxLines.SelectedIndex = this.pointsCache[this.listBoxPoints.SelectedIndex].TraceIndex;
                }
                PointD p = pattern.MachineAbs(this.pointsCache[this.listBoxPoints.SelectedIndex].Point);
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY(p);

            }
        }
    }

    public class PointTrace
    {
        public PointD Point;

        public TraceBase Trace;

        public int TraceIndex;

        public TracePointType PointType;

        public PointTrace(PointD point, TraceBase trace, int traceIndex, TracePointType pointType)
        {
            this.Point = point;
            this.Trace = trace;
            this.TraceIndex = traceIndex;
            this.PointType = pointType;
        }
    }
}
