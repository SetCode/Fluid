using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditSymbolLinesForm : EditFormBase, IMsgSender
    {
        enum PointType
        {
            Line = 0,
            Arc
        }
        class PointItem : ICloneable
        {
            public PointItem(PointD point)
            {
                this.Point = point;
            }

            public PointItem(PointD point, PointType type)
            {
                this.Point = point;
                this.Type = type;
            }
            public PointD Point { get; set; }
            public PointType Type { get; set; } = PointType.Line;

            /// <summary>
            /// 对应的线或圆弧的索引
            /// Author :Shawn
            /// Date   :2019/12/06
            /// </summary>
            public int TraceIndex;

            /// <summary>
            /// 该点运行时是否需要进行手动示教
            /// </summary>
            public bool ModifyEnable { get; set; } = false;

            public object Clone()
            {
                PointItem result = this.MemberwiseClone() as PointItem;
                result.Point = this.Point.Clone() as PointD;
                return result;
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("{0} {1}", Point, Type.ToString()));
                return builder.ToString();
            }
        }

        class SymbolItem : ICloneable
        {
            public SymbolLine symbolLine = null;
            public SymbolItem(SymbolLine symbolLine)
            {
                this.symbolLine = symbolLine;
                this.Points = symbolLine.symbolPoints;
                this.Type = symbolLine.symbolType;               
            }
            public List<PointD> Points { get; set; }
            public SymbolType Type { get; set; } = SymbolType.Line;
          
            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("{0} {1} {2}", Points[0], Points.Last(), Type.ToString()));
                return builder.ToString();
            }

            public object Clone()
            {
                SymbolItem result = this.MemberwiseClone() as SymbolItem;
                foreach (PointD item in this.Points)
                {
                    result.Points.Add(item.Clone() as PointD);
                }
                return result;
            }
        }
        private Pattern pattern;
        private PointD origin;
        private SymbolLinesCmdLine symbolLinesCmdLine;
        private bool isCreating;
        private List<PointItem> SymbolPoints = new List<PointItem>();
        private List<SymbolLine> SymbolLinesCache = new List<SymbolLine>();
        private MeasureHeightCmdLine measureCmdLine = null;
        private bool isMouseDown = false;


    
        private EditSymbolLinesForm()
        {
            InitializeComponent();
        }


        public EditSymbolLinesForm(Pattern pattern) : this(pattern, null)
        {
        }

        public EditSymbolLinesForm(Pattern pattern, SymbolLinesCmdLine symbolLinesCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                cbxLineType.Items.Add("Type " + (i + 1));
            }
            
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();

            if (symbolLinesCmdLine == null)
            {
                isCreating = true;
                this.symbolLinesCmdLine = new SymbolLinesCmdLine();
                //系统坐标->机械坐标
                PointD p = this.pattern.MachineRel(Properties.Settings.Default.LineEndX, Properties.Settings.Default.LineEndY);
                this.tbPointX.Text = p.X.ToString("0.000");
                this.tbPointY.Text = p.Y.ToString("0.000");
                this.rdoLine.Checked = true;
                this.cbxLineType.SelectedIndex = 0;
                this.heightControl1.SetupFluidProgram(pattern.Program);
            }
            else
            {
                isCreating = false;
                this.symbolLinesCmdLine = symbolLinesCmdLine;
                this.measureCmdLine = symbolLinesCmdLine.BindMHCmdLine.Clone() as MeasureHeightCmdLine;
                foreach (SymbolLine item in symbolLinesCmdLine.Symbols)
                {
                    this.SymbolLinesCache.Add(item.Clone() as SymbolLine);
                }
                this.cbxLineType.SelectedIndex = (int)this.SymbolLinesCache[0].type;
                this.LoadPoints2ListBox();
                //load lines to listboxLines
                this.LoadLines2ListBox();
                this.heightControl1.SetupCmdLine(this.measureCmdLine);
                this.tbMHZPos.Text = this.measureCmdLine.ZPos.ToString("0.000");
            }
            this.tbArcSpeed.Text = this.symbolLinesCmdLine.ArcSpeed.ToString();
            this.listBoxLines.SelectedIndexChanged += ListBoxLines_SelectedIndexChanged;
            this.listBoxPoints.SelectedIndexChanged += ListBoxPoints_SelectedIndexChanged;
            this.cbxLineType.SelectedIndexChanged += CbxLineType_SelectedIndexChanged;
            this.listBoxLines.MouseDown += ListBoxLine_MouseDown;

            this.nudOffsetX.Maximum = 5;
            this.nudOffsetX.Minimum = -5;
            this.nudOffsetX.DecimalPlaces = 3;
            this.nudOffsetX.Increment = (decimal)0.001;
            this.nudOffsetY.Maximum = 5;
            this.nudOffsetY.Minimum = -5;
            this.nudOffsetY.DecimalPlaces = 3;
            this.nudOffsetY.Increment = (decimal)0.001;
            this.nudOffsetX.Value = (decimal)this.symbolLinesCmdLine.OffsetX;
            this.nudOffsetY.Value = (decimal)this.symbolLinesCmdLine.OffsetY;

            this.tbTransitionR.Text = 2.ToString("0.000");
            this.listBoxPoints.MouseDown += ListBoxPoints_MouseDown;
            this.listBoxPoints.MouseDoubleClick += ListBoxPoints_MouseDoubleClick;


            this.heightControl1.canMeasure = false;
            this.heightControl1.canMeasure_way();

            this.ReadLanguageResources();
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources();
            if (this.HasLngResources())
            {
                this.tipPrev.Text = this.ReadKeyValueFromResources(this.tipPrev.Name);
                this.tipNext.Text = this.ReadKeyValueFromResources(this.tipNext.Name);
                this.btnOk.Text = this.ReadKeyValueFromResources(this.btnOk.Name);
                this.lblLineType.Text = this.ReadKeyValueFromResources(this.lblLineType.Name);
                this.btnCancel.Text = this.ReadKeyValueFromResources(this.btnCancel.Name);
            }
        }




        private void ListBoxPoints_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
        }

        private void ListBoxPoints_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBoxPoints.Items.Count == 1 || this.listBoxPoints.SelectedIndex == -1)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                this.cmsPointUpDown.Show(new Point(this.listBoxPoints.PointToScreen(e.Location).X,
                    this.listBoxPoints.PointToScreen(e.Location).Y + 5));
            }
            this.isMouseDown = true;
        }

        private void ListBoxLine_MouseDown(object sender, MouseEventArgs e)
        {
            this.isMouseDown = true;
        }


        private void CbxLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (SymbolLine item in this.SymbolLinesCache)
            {
                item.type = (LineStyle)this.cbxLineType.SelectedIndex;
            }
        }

        private void ListBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isMouseDown)
            {
                return;
            }
            if (this.listBoxPoints.SelectedIndex == -1 || this.listBoxPoints.SelectedIndex > this.listBoxPoints.Items.Count - 1)
            {
                return;
            }
            PointItem curItem = this.listBoxPoints.Items[this.listBoxPoints.SelectedIndex] as PointItem;
            this.tbPointX.Text = curItem.Point.X.ToString();
            this.tbPointY.Text = curItem.Point.Y.ToString();
            if (curItem.Type == PointType.Arc)
            {
                this.rdoArc.Checked = true;
            }
            else
            {
                this.rdoLine.Checked = true;
            }
            if (curItem.ModifyEnable )
            {
                this.chkModifyEnable.Checked = true;
            }
            else
            {
                this.chkModifyEnable.Checked = false;
            }

            this.isMouseDown = false;
            this.listBoxLines.SelectedItems.Clear();
            if (curItem.TraceIndex < this.listBoxLines.Items.Count)
            {
                this.listBoxLines.SelectedIndex = curItem.TraceIndex;
            }

        }

        private void ListBoxLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isMouseDown)
            {
                return;
            }

            if (this.listBoxLines.SelectedIndex == -1 || this.listBoxLines.SelectedIndex > this.listBoxLines.Items.Count - 1)
            {
                return;
            }            
            this.tbMHCount.Text = (this.listBoxLines.Items[this.listBoxLines.SelectedIndex] as SymbolItem).symbolLine.MHCount.ToString();
            this.tbTransitionR.Text = (this.listBoxLines.Items[this.listBoxLines.SelectedIndex] as SymbolItem).symbolLine.transitionR.ToString();
            PointItem curItem;
            this.isMouseDown = false;
            for (int i = 0; i < listBoxPoints.Items.Count; i++)
            {
                curItem = listBoxPoints.Items[i] as PointItem;
                if (curItem.TraceIndex == this.listBoxLines.SelectedIndex)
                {
                    this.listBoxPoints.SelectedIndex = i;
                    break;
                }

            }

        }


        private void btnSelectStart_Click(object sender, EventArgs e)
        {
            tbPointX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbPointY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGotoStart_Click(object sender, EventArgs e)
        {
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
        }
        private void LoadPoints2ListBox()
        {
            listBoxPoints.Items.Clear();
            this.SymbolPoints.Clear();
            int SymbolsCount = this.SymbolLinesCache.Count;
            if (SymbolsCount < 1)
            {
                return;
            }
            PointItem pointItem = new PointItem(this.SymbolLinesCache[0].symbolPoints[0], (PointType)((int)(this.SymbolLinesCache[0].symbolType)));
            pointItem.ModifyEnable = this.SymbolLinesCache[0].needOffset[0];
            this.SymbolPoints.Add(pointItem);
            listBoxPoints.Items.Add(pointItem);
            for (int i = 0; i < SymbolsCount; i++)
            {
                if (this.SymbolLinesCache[i].symbolType == SymbolType.Arc)
                {
                    PointItem arcMiddle = new PointItem(this.SymbolLinesCache[i].symbolPoints[1], PointType.Arc);
                    arcMiddle.ModifyEnable = this.SymbolLinesCache[i].needOffset[1];
                    this.SymbolPoints.Add(arcMiddle);
                    listBoxPoints.Items.Add(arcMiddle);
                    PointItem arcEnd = new PointItem(this.SymbolLinesCache[i].symbolPoints[2], PointType.Arc);
                    arcEnd.ModifyEnable = this.SymbolLinesCache[i].needOffset[2];
                    this.SymbolPoints.Add(arcEnd);
                    listBoxPoints.Items.Add(arcEnd);
                }
                else
                {
                    PointItem lineEnd = new PointItem(this.SymbolLinesCache[i].symbolPoints[1], PointType.Line);
                    lineEnd.ModifyEnable= this.SymbolLinesCache[i].needOffset[1];
                    this.SymbolPoints.Add(lineEnd);
                    listBoxPoints.Items.Add(lineEnd);
                }
            }
            this.SymbolLinesCache.Clear();
            this.btnCreateSymbols_Click(this, null);
            for (int i = 0; i < this.symbolLinesCmdLine.Symbols.Count; i++)
            {
                this.SymbolLinesCache[i].MHCount = this.symbolLinesCmdLine.Symbols[i].MHCount;
                this.SymbolLinesCache[i].transitionR = this.symbolLinesCmdLine.Symbols[i].transitionR;
                this.SymbolLinesCache[i] = this.symbolLinesCmdLine.Symbols[i].Clone() as SymbolLine;
            }
        }

        private void LoadLines2ListBox()
        {
            listBoxLines.Items.Clear();
            foreach (SymbolLine item in this.SymbolLinesCache)
            {
                SymbolItem symbol = new SymbolItem(item);
                listBoxLines.Items.Add(symbol);
            }
        }

        private void RefreshListBoxPoints()
        {
            listBoxPoints.Items.Clear();
            foreach (PointItem item in this.SymbolPoints)
            {
                listBoxPoints.Items.Add(item);
            }
            this.LoadLines2ListBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            PointD p = this.pattern.SystemRel(tbPointX.Value, tbPointY.Value);
            int index = this.listBoxPoints.SelectedIndex;
            if (this.SymbolPoints.Count > 0)
            {
                if (index > -1 && index < this.SymbolPoints.Count - 1 && this.SymbolPoints.Count > 1)
                {
                    VectorD delta1 = p - this.SymbolPoints[index].Point;
                    if (delta1.Length < 0.002)
                    {
                        MessageBox.Show("拾取点位与上一点位重合，请确认坐标");
                        return;
                    }
                    VectorD delta2 = p - this.SymbolPoints[index + 1].Point;
                    if (delta2.Length < 0.002)
                    {
                        MessageBox.Show("拾取点位与下一点位重合，请确认坐标");
                        return;
                    }
                }
                else
                {
                    VectorD delta1 = p - this.SymbolPoints.Last().Point;
                    if (delta1.Length < 0.002)
                    {
                        MessageBox.Show("拾取点位与上一点位重合，请确认坐标");
                        return;
                    }
                }
            }
            PointItem point = new PointItem(p);
            if (rdoArc.Checked)
            {
                point.Type = PointType.Arc;
            }
            else
            {
                point.Type = PointType.Line;
            }
            if (chkModifyEnable.Checked)
            {
                point.ModifyEnable = true;
            }
            else
            {
                point.ModifyEnable = false;
            }

            if (index == this.SymbolPoints.Count - 1 || index == -1)
            {
                this.SymbolPoints.Add(point);
            }
            else
            {
                this.SymbolPoints.Insert(index + 1, point);
            }
           
            this.RefreshListBoxPoints();

            this.listBoxPoints.SelectedIndex = index + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMHCmdLine_Click(object sender, EventArgs e)
        {
            if (this.measureCmdLine == null)
            {
                this.measureCmdLine = new MeasureHeightCmdLine();
            }
            this.measureCmdLine.StandardHt = this.heightControl1.BoardHeight;
            this.measureCmdLine.ToleranceMax = this.heightControl1.MaxTolerance;
            this.measureCmdLine.ToleranceMin = this.heightControl1.MinTolerance;
            tbMHZPos.Text = (Machine.Instance.Robot.PosZ).ToString("0.000");
            this.measureCmdLine.ZPos = this.tbMHZPos.Value;
        }

        //private void btnCreateSymbols_Click(object sender, EventArgs e)
        //{
        //    List<List<PointItem>> temp = new List<List<PointItem>>();
        //    //一段一段圆弧按顺序分开
        //    List<PointItem> curItems = new List<PointItem>();
        //    bool curIsArc = false;
        //    for (int i = 0; i < this.SymbolPoints.Count;i++)
        //    {
        //        if (curItems.Count < 1)
        //        {
        //            if (this.SymbolPoints[i].Type == PointType.Arc)
        //            {
        //                curIsArc = true;
        //            }
        //            curItems.Add(this.SymbolPoints[i]);
        //            continue;
        //        }
        //        if (curIsArc)
        //        {
        //            if (this.SymbolPoints[i].Type != PointType.Arc || curItems.Count > 2)
        //            {
        //                MessageBox.Show(string.Format("第{0}点数据异常，无法生成轨迹",i));
        //                return;
        //            }
        //            curItems.Add(this.SymbolPoints[i]);
        //            if (curItems.Count == 3)
        //            {
        //                curIsArc = false;
        //                List<PointItem> points = new List<PointItem>();
        //                foreach (PointItem item in curItems)
        //                {
        //                    points.Add(item);
        //                }
        //                temp.Add(points);
        //                curItems.Clear();
        //            }
        //        }
        //        else
        //        {
        //            if (this.SymbolPoints[i].Type != PointType.Line)
        //            {
        //                List<PointItem> points = new List<PointItem>();
        //                foreach (PointItem item in curItems)
        //                {
        //                    points.Add(item);
        //                }
        //                temp.Add(points);
        //                curItems.Clear();
        //                curIsArc = true;
        //                curItems.Add(this.SymbolPoints[i]);
        //            }
        //            else
        //            {
        //                curItems.Add(this.SymbolPoints[i]);
        //            }
        //        }
        //    }
        //    if (curItems.Count != 0)
        //    {
        //        if (curItems.Last().Type != curItems[0].Type || (curItems.Last().Type == PointType.Arc && curItems.Count < 3))
        //        {
        //            MessageBox.Show(string.Format("末段数据异常，无法生成轨迹"));
        //            return;
        //        }
        //        else
        //        {
        //            temp.Add(curItems);
        //        }
        //    }

        //    this.SymbolLinesCache.Clear();
        //    //数据分段完成,将分段数据按照类型连接起来
        //    for (int i = 0; i < temp.Count; i++)
        //    {
        //        if (i != 0) //不是首段轨迹，与上一段最后一点相连
        //        {
        //            SymbolLine symbolLine1 = new SymbolLine();
        //            symbolLine1.symbolType = SymbolType.Line;
        //            symbolLine1.symbolPoints.Add(temp[i - 1][temp[i - 1].Count - 1].Point);
        //            symbolLine1.symbolPoints.Add(temp[i][0].Point);
        //            this.SymbolLinesCache.Add(symbolLine1);
        //        }
        //        if (temp[i][0].Type == PointType.Arc)//生成当前集合的圆弧段(每个圆弧点集中只有三个圆弧点)
        //        {
        //            SymbolLine symbolLine2 = new SymbolLine();
        //            symbolLine2.symbolType = SymbolType.Arc;
        //            symbolLine2.symbolPoints.Add(temp[i][0].Point);
        //            symbolLine2.symbolPoints.Add(temp[i][1].Point);
        //            symbolLine2.symbolPoints.Add(temp[i][2].Point);
        //            PointD center = MathUtils.CalculateCircleCenter(temp[i][0].Point, temp[i][1].Point, temp[i][2].Point);
        //            double degree = MathUtils.CalculateCircleDegree(temp[i][0].Point, temp[i][1].Point, temp[i][2].Point, center);
        //            symbolLine2.clockwise = degree > 0 ? 1 : 0;
        //            this.SymbolLinesCache.Add(symbolLine2);
        //        }
        //        else
        //        {
        //            if (temp[i].Count > 1) // 直线集合中可能包含多段
        //            {
        //                for (int j = 1; j < temp[i].Count; j++)
        //                {
        //                    SymbolLine symbolLine3 = new SymbolLine();
        //                    symbolLine3.symbolType = SymbolType.Line;
        //                    symbolLine3.symbolPoints.Add(temp[i][j-1].Point);
        //                    symbolLine3.symbolPoints.Add(temp[i][j].Point);
        //                    this.SymbolLinesCache.Add(symbolLine3);
        //                }
        //            }
        //        }
        //    }
        //    this.LoadLines2ListBox();
        //}

        private void btnCreateSymbols_Click(object sender, EventArgs e)
        {
            List<List<PointItem>> temp = new List<List<PointItem>>();
            //一段一段圆弧按顺序分开
            List<PointItem> curItems = new List<PointItem>();
            //添加首点
            curItems.Add(this.SymbolPoints[0]);
            for (int i = 1; i < this.SymbolPoints.Count; i++)
            {
                if (this.SymbolPoints[i].Type == PointType.Arc)
                {
                    if (temp.Count > 0 && temp.Last()[1].Type == PointType.Arc)//上一段也是圆弧，两段轨迹都是圆弧，没有过渡处理，不允许圆弧-圆弧的轨迹
                    {
                        MessageBox.Show(string.Format("第{0}点数据异常，不支持生成两段连续圆弧", i - 1));
                        this.listBoxPoints.SelectedIndex = i - 2;
                        return;
                    }
                    if (curItems.Count > 2)
                    {
                        MessageBox.Show(string.Format("第{0}点数据异常，无法生成轨迹", i - 1));
                        this.listBoxPoints.SelectedIndex = i - 2;
                        return;
                    }
                    curItems.Add(this.SymbolPoints[i]);
                    if (curItems.Count == 3)
                    {
                        List<PointItem> points = new List<PointItem>();
                        foreach (PointItem item in curItems)
                        {
                            points.Add(item);
                        }
                        temp.Add(points);
                        curItems.Clear();
                        //清空上一组，并将尾点保存
                        curItems.Add(this.SymbolPoints[i]);
                    }
                }
                else
                {
                    if (curItems.Count > 1) // 上一个点是圆弧点才回报错
                    {
                        MessageBox.Show(string.Format("第{0}点数据异常，无法生成轨迹", i));
                        this.listBoxPoints.SelectedIndex = i - 1;
                        return;
                    }
                    curItems.Add(this.SymbolPoints[i]);
                    if (curItems.Count == 2)
                    {
                        List<PointItem> points = new List<PointItem>();
                        foreach (PointItem item in curItems)
                        {
                            points.Add(item);
                        }
                        temp.Add(points);
                        curItems.Clear();
                        //清空上一组，并将尾点保存
                        curItems.Add(this.SymbolPoints[i]);
                    }
                }
            }
            if (curItems.Count != 1)
            {
                DialogResult result = MessageBox.Show(this, "末尾有多余拾取点位，是否忽略，继续生成轨迹", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            this.SymbolLinesCache.Clear();
            //数据分段完成,将分段数据按照类型连接起来
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i][1].Type == PointType.Arc)
                {
                    SymbolLine symbolLine2 = new SymbolLine();
                    symbolLine2.symbolType = SymbolType.Arc;
                    symbolLine2.symbolPoints.Add(temp[i][0].Point);
                    symbolLine2.symbolPoints.Add(temp[i][1].Point);
                    symbolLine2.symbolPoints.Add(temp[i][2].Point);
                    symbolLine2.needOffset.Add(temp[i][0].ModifyEnable);
                    symbolLine2.needOffset.Add(temp[i][1].ModifyEnable);
                    symbolLine2.needOffset.Add(temp[i][2].ModifyEnable);
                    PointD center = MathUtils.CalculateCircleCenter(temp[i][0].Point, temp[i][1].Point, temp[i][2].Point);
                    double degree = MathUtils.CalculateCircleDegree(temp[i][0].Point, temp[i][1].Point, temp[i][2].Point, center);
                    symbolLine2.clockwise = degree > 0 ? 1 : 0;
                    this.SymbolLinesCache.Add(symbolLine2);

                    if (i == 0)
                    {
                        temp[i][0].TraceIndex = i;
                    }
                    temp[i][1].TraceIndex = i;
                    temp[i][2].TraceIndex = i;

                }
                else
                {
                    SymbolLine symbolLine3 = new SymbolLine();
                    symbolLine3.symbolType = SymbolType.Line;
                    symbolLine3.symbolPoints.Add(temp[i][0].Point);
                    symbolLine3.symbolPoints.Add(temp[i][1].Point);
                    symbolLine3.needOffset.Add(temp[i][0].ModifyEnable);
                    symbolLine3.needOffset.Add(temp[i][1].ModifyEnable);
                    this.SymbolLinesCache.Add(symbolLine3);

                    if (i == 0)
                    {
                        temp[i][0].TraceIndex = i;
                    }
                    temp[i][1].TraceIndex = i;
                }
            }
            this.LoadLines2ListBox();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int selectIndex = this.listBoxPoints.SelectedIndex;
            if (selectIndex < 0 || selectIndex >= this.SymbolPoints.Count)
            {
                return;
            }
            PointD pos = new PointD();
            pos.X = tbPointX.Value;
            pos.Y = tbPointY.Value;
            if (selectIndex > 0)
            {
                VectorD delta1 = pos - this.SymbolPoints[selectIndex - 1].Point;
                if (delta1.Length < 0.002)
                {
                    MessageBox.Show("拾取点位与上一点位重合，请确认坐标");
                    return;
                }
            }
            if (selectIndex < this.SymbolPoints.Count - 1)
            {
                VectorD delta2 = pos - this.SymbolPoints[selectIndex + 1].Point;
                if (delta2.Length < 0.002)
                {
                    MessageBox.Show("拾取点位与下一点位重合，请确认坐标");
                    return;
                }
            }

            if (this.chkModifyEnable.Checked)
            {
                this.SymbolPoints[selectIndex].ModifyEnable = true;
            }
            else
            {
                this.SymbolPoints[selectIndex].ModifyEnable = false;
            }

            this.SymbolPoints[selectIndex].Point.X = tbPointX.Value;
            this.SymbolPoints[selectIndex].Point.Y = tbPointY.Value;
            this.RefreshListBoxPoints();
            this.listBoxPoints.SelectedIndex = selectIndex;
        }

        private void btnModifyParam_Click(object sender, EventArgs e)
        {
            if (this.listBoxLines.SelectedIndex == -1 || this.listBoxLines.SelectedIndex > this.listBoxLines.Items.Count - 1)
            {
                return;
            }
            foreach (int i in this.listBoxLines.SelectedIndices)
            {
                this.SymbolLinesCache[i].MHCount = this.tbMHCount.Value;
                this.SymbolLinesCache[i].transitionR = this.tbTransitionR.Value;
            }
            this.LoadLines2ListBox();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.measureCmdLine == null)
            {
                MessageBox.Show("当前轨迹未增加基准测高，请生成测高指令");
                return;
            }
            if (this.SymbolLinesCache.Count < 1)
            {
                MessageBox.Show("未生成轨迹请生成轨迹");
                return;
            }
            if (this.tbArcSpeed.Text == "")
            {
                MessageBox.Show("圆弧速度未设置");
                return;
            }

            this.symbolLinesCmdLine.BindMHCmdLine = this.measureCmdLine;
            this.symbolLinesCmdLine.Symbols = this.SymbolLinesCache;
            this.symbolLinesCmdLine.LineStyle = (LineStyle)cbxLineType.SelectedIndex;
            this.symbolLinesCmdLine.ArcSpeed = this.tbArcSpeed.Value;
            this.symbolLinesCmdLine.OffsetX = (double)this.nudOffsetX.Value;
            this.symbolLinesCmdLine.OffsetY = (double)this.nudOffsetY.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, this.symbolLinesCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.symbolLinesCmdLine);
            }
            this.Close();
        }

        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            int index = this.listBoxPoints.SelectedIndex;
            if (index > this.SymbolPoints.Count - 1 || index == -1)
            {
                return;
            }
            this.SymbolPoints.RemoveAt(index);
            RefreshListBoxPoints();

            this.listBoxPoints.SelectedIndex = index - 1;
        }

        private void btnLineStyleEdit_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void tsiPointUp_Click(object sender, EventArgs e)
        {
            int index = this.listBoxPoints.SelectedIndex;
            if (index == 0)
            {
                return;
            }
            PointItem temp = this.SymbolPoints[index - 1] as PointItem;
            this.SymbolPoints[index - 1] = this.SymbolPoints[index];
            this.SymbolPoints[index] = temp;
            RefreshListBoxPoints();
        }

        private void tsiPointDown_Click(object sender, EventArgs e)
        {
            int index = this.listBoxPoints.SelectedIndex;
            if (index == this.listBoxPoints.Items.Count - 1)
            {
                return;
            }
            PointItem temp = this.SymbolPoints[index + 1] as PointItem;
            this.SymbolPoints[index + 1] = this.SymbolPoints[index];
            this.SymbolPoints[index] = temp;
            RefreshListBoxPoints();
        }

        private void btnGoMHPos_Click(object sender, EventArgs e)
        {
            if (!tbPointX.IsValid || !tbPointY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            PointD position = new PointD(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
            Machine.Instance.Robot.ManualMovePosXY(position.ToLaser());
        }
        private void listBoxPoints_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (this.listBoxPoints.Items.Count != 0)
            {
                Brush backColor;
                Brush textColor = Brushes.Black;
                int r;
                Math.DivRem(e.Index, 2, out r);

                if (r == 0)
                {
                    backColor = Brushes.White;
                }
                else
                {
                    backColor = Brushes.LightGray;
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.Index > -1)
                {

                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                    textColor = SystemBrushes.HighlightText;
                }
                else
                {
                    e.Graphics.FillRectangle(backColor, e.Bounds);
                }

                e.Graphics.DrawString(this.listBoxPoints.Items[e.Index].ToString(), this.listBoxPoints.Font, textColor, e.Bounds);
            }
        }

        private void Prev_Click(object sender, EventArgs e)
        {

            if (this.listBoxPoints.SelectedIndex == 0)
            {
                MessageBox.Show("已经到第一步了！");
                return;
            }
            else if (this.listBoxPoints.SelectedIndex < 0)
            {
                MessageBox.Show("未选中轨迹点！");
                return;
            }
            else
            {
                this.listBoxPoints.SelectedIndex -= 1;
                PointItem curItem = this.listBoxPoints.Items[this.listBoxPoints.SelectedIndex] as PointItem;
                this.tbPointX.Text = curItem.Point.X.ToString();
                this.tbPointY.Text = curItem.Point.Y.ToString();
                if (curItem.Type == PointType.Arc)
                {
                    this.rdoArc.Checked = true;
                }
                else
                {
                    this.rdoLine.Checked = true;
                }

                //MessageBox.Show("X;" + origin.X + tbPointX.Value);
                if (!tbPointX.IsValid || !tbPointY.IsValid)
                {
                    return;
                }
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
            }

        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (this.listBoxPoints.SelectedIndex == this.listBoxPoints.Items.Count - 1)
            {
                MessageBox.Show("已经到最后一步了！");
                return;
            }
            else if (this.listBoxPoints.SelectedIndex > this.listBoxPoints.Items.Count - 1)
            {
                MessageBox.Show("未选中轨迹点！");
                return;
            }
            else
            {

                this.listBoxPoints.SelectedIndex += 1;
                PointItem curItem = this.listBoxPoints.Items[this.listBoxPoints.SelectedIndex] as PointItem;
                this.tbPointX.Text = curItem.Point.X.ToString();
                this.tbPointY.Text = curItem.Point.Y.ToString();
                if (curItem.Type == PointType.Arc)
                {
                    this.rdoArc.Checked = true;
                }
                else
                {
                    this.rdoLine.Checked = true;
                }
                if (!tbPointX.IsValid || !tbPointY.IsValid)
                {
                    return;
                }
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY(origin.X + tbPointX.Value, origin.Y + tbPointY.Value);
            }

        }
        
        
    }
}
