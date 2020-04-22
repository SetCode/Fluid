using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.Dialogs;

namespace Anda.Fluid.App.LoadTrajectory
{
    public enum Axis
    {
        X,
        Y
    }
    public enum Axislabel
    {
        N,
        X,
        Y
    }

    public partial class TrajectoryDialog02 : FormEx,IMsgSender
    {        
        private string patternName;       
        private Pattern pattern;
        private PatternInfo currPInfo;
        private string currPath;

        private string pathHelp = "Help\\Trajectory.txt";
        private string[] textHelp;

        public TrajectoryDialog02()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.nudMaxX.Value = 1000;
            this.nudMinX.Value = -1000;
            this.nudMaxY.Value = 1000;
            this.nudMinY.Value = -1000;
            //-324-189
            this.nudOffsetX.Value = 0;
            this.nudOffsetY.Value = 0;
              
            this.trajectoryChart1.Setup(this.setMark);

            axisDic.Add(Axis.X.ToString(), 1);
            axisDic.Add(Axis.Y.ToString(), 2);
            indexLblMap.Add(1, Axislabel.N.ToString());
            indexLblMap.Add(2, Axislabel.N.ToString());
            indexLblMap.Add(-1, Axislabel.N.ToString());
            indexLblMap.Add(-2, Axislabel.N.ToString());

           
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources(this.gpbEdit.Name, this.gpbEdit.Text);
            this.SaveKeyValueToResources(this.lblMinX.Name, this.lblMinX.Text);
            this.SaveKeyValueToResources(this.lblMaxX.Name, this.lblMaxX.Text);
            
            this.SaveKeyValueToResources(this.lblMinY.Name, this.lblMinY.Text);
            this.SaveKeyValueToResources(this.lblMaxY.Name, this.lblMaxY.Text);
            this.SaveKeyValueToResources(this.lblOffsetX.Name, this.lblOffsetX.Text);
            this.SaveKeyValueToResources(this.lblOffsetY.Name, this.lblOffsetY.Text);
           

            this.SaveKeyValueToResources(this.btnGenerate.Name, this.btnGenerate.Text);
            this.SaveKeyValueToResources(this.btnReLoad.Name, this.btnReLoad.Text);
            this.SaveKeyValueToResources(this.btnOffset.Name, this.btnOffset.Text);


            this.SaveKeyValueToResources(this.gpbOptimiz.Name, this.gpbOptimiz.Text);
            this.SaveKeyValueToResources(this.lblmark1.Name, this.lblmark1.Text);
            this.SaveKeyValueToResources(this.ckbMark1.Name, this.ckbMark1.Text);

            this.SaveKeyValueToResources(this.lblmark2.Name, this.lblmark2.Text);
            this.SaveKeyValueToResources(this.ckbMark2.Name, this.ckbMark2.Text);
            this.SaveKeyValueToResources(this.btnOptimize.Name, this.btnOptimize.Text);
            this.SaveKeyValueToResources(this.lblDis.Name, this.lblDis.Text);
            this.SaveKeyValueToResources(this.lblPatternName.Name, this.lblPatternName.Text);
            this.SaveKeyValueToResources(this.btnCreate.Name, this.btnCreate.Text);
            this.SaveKeyValueToResources(this.btnCancel.Name, this.btnCancel.Text);
            this.SaveKeyValueToResources(this.btnOK.Name, this.btnOK.Text);

        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.gpbEdit.Text = this.ReadKeyValueFromResources(this.gpbEdit.Name);
            this.lblMinX.Text = this.ReadKeyValueFromResources(this.lblMinX.Name);
            this.lblMaxX.Text = this.ReadKeyValueFromResources(this.lblMaxX.Name);
            this.lblMinY.Text = this.ReadKeyValueFromResources(this.lblMinY.Name);
            this.lblMaxY.Text = this.ReadKeyValueFromResources(this.lblMaxY.Name);
            this.lblOffsetX.Text = this.ReadKeyValueFromResources(this.lblOffsetX.Name);
            this.lblOffsetY.Text = this.ReadKeyValueFromResources(this.lblOffsetY.Name);
           

            this.btnGenerate.Text = this.ReadKeyValueFromResources(this.btnGenerate.Name);
            this.btnReLoad.Text = this.ReadKeyValueFromResources(this.btnReLoad.Name);
            this.btnOffset.Text = this.ReadKeyValueFromResources(this.btnOffset.Name);


            this.gpbOptimiz.Text = this.ReadKeyValueFromResources(this.gpbOptimiz.Name);
            this.lblmark1.Text = this.ReadKeyValueFromResources(this.lblmark1.Name);
            this.ckbMark1.Text = this.ReadKeyValueFromResources(this.ckbMark1.Name);

            this.lblmark2.Text = this.ReadKeyValueFromResources(this.lblmark2.Name);
            this.ckbMark2.Text = this.ReadKeyValueFromResources(this.ckbMark2.Name);
            this.btnOptimize.Text = this.ReadKeyValueFromResources(this.btnOptimize.Name);
            this.lblDis.Text = this.ReadKeyValueFromResources(this.lblDis.Name);

            this.lblPatternName.Text = this.ReadKeyValueFromResources(this.lblPatternName.Name);
            this.btnCreate.Text = this.ReadKeyValueFromResources(this.btnCreate.Name);
            this.btnCancel.Text = this.ReadKeyValueFromResources(this.btnCancel.Name);

            this.btnOK.Text = this.ReadKeyValueFromResources(this.btnOK.Name);


        }

        private void TrajectoryDialog_Load(object sender, EventArgs e)
        {
            FileUtils.ReadLines(pathHelp, out textHelp);
            //当前是否有程序
            if (FluidProgram.Current == null)
            {
                this.txtPatternName.Enabled = false;
                this.lblPatternName.Enabled = false;
            }
            this.currPInfo = CADImport.Instance.GetCurPatternInfo();
            if (this.currPInfo == null)
                return;
            this.patternName = this.currPInfo.PatternName;
            if (string.IsNullOrEmpty(this.patternName))
                return;
            
            this.txtPatternName.Text = this.patternName;
            this.initialListView();
            this.listViewLoadData();
        }

        private void initialListView()
        {
            lswComponents.View = View.Details;
            lswComponents.GridLines = true;
            this.lswComponents.MultiSelect = false;
            this.lswComponents.FullRowSelect = true;
            this.lswComponents.Columns.Clear();

            lswComponents.Columns.Add("Index", 50, HorizontalAlignment.Center);
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.Design)
                {
                    lswComponents.Columns.Add(HeadName.Design.ToString(), 50, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.Comp)
                {
                    lswComponents.Columns.Add(HeadName.Comp.ToString(), 50, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.X)
                {
                    lswComponents.Columns.Add(HeadName.X.ToString(), 20, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.Y)
                {
                    lswComponents.Columns.Add(HeadName.Y.ToString(), 20, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.Rot)
                {
                    lswComponents.Columns.Add(HeadName.Rot.ToString(), 60, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPInfo.HeadSelected)
            {
                if (head.StandName == HeadName.LayOut)
                {
                    lswComponents.Columns.Add(HeadName.LayOut.ToString(), 80, HorizontalAlignment.Center);
                }
            }
            //添加列  
            lswComponents.BeginUpdate();
            lswComponents.EndUpdate();

         
        }
        /// <summary>
        /// lswComponents中加载数据
        /// </summary>
        private void listViewLoadData()
        {
            this.initialListView();
            PatternInfo patInfor = this.currPInfo;         
            lswComponents.BeginUpdate();
            this.lswComponents.Items.Clear();
            int count = 0;            
            foreach (CompProperty comp in patInfor.CompListStanded)
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                foreach (ColumnHeader column in this.lswComponents.Columns)
                {                    
                    if (column.Text == HeadName.Design.ToString())
                    {
                        item.SubItems.Add(comp.Desig);
                        continue;
                    }
                    if (column.Text == HeadName.Comp.ToString())
                    {
                        item.SubItems.Add(comp.Comp);
                        continue;
                    }
                    if (column.Text == HeadName.X.ToString())
                    {
                        item.SubItems.Add(comp.Mid.X.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Y.ToString())
                    {
                        item.SubItems.Add(comp.Mid.Y.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Rot.ToString())
                    {
                        item.SubItems.Add(comp.Rotation.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.LayOut.ToString())
                    {
                        item.SubItems.Add(comp.LayOut);
                        continue;
                    }
                }                
                lswComponents.Items.Add(item);
            }
            lswComponents.EndUpdate();
            this.adjustColumnWidth();
        }
        /// <summary>
        /// 
        /// </summary>
        private void listViewLoadPoints(List<TrajPoint> trajPoints)
        {            
            lswComponents.BeginUpdate();
            this.lswComponents.Items.Clear();
            int count = 0;
            foreach (TrajPoint comp in trajPoints)
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                foreach (ColumnHeader column in this.lswComponents.Columns)
                {
                    if (column.Text == HeadName.Design.ToString())
                    {
                        item.SubItems.Add(comp.Desig);
                        continue;
                    }
                    if (column.Text == HeadName.Comp.ToString())
                    {
                        item.SubItems.Add(comp.Comp);
                        continue;
                    }
                    if (column.Text == HeadName.X.ToString())
                    {
                        item.SubItems.Add(comp.Mid.X.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Y.ToString())
                    {
                        item.SubItems.Add(comp.Mid.Y.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Rot.ToString())
                    {
                        item.SubItems.Add(comp.Rotation.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.LayOut.ToString())
                    {
                        item.SubItems.Add(comp.LayOut);
                        continue;
                    }
                }
                lswComponents.Items.Add(item);
            }
            lswComponents.EndUpdate();
            this.adjustColumnWidth();
        }
        /// <summary>
        /// 调整lswComponents的宽度
        /// </summary>
        private void adjustColumnWidth()
        {
            if (this.currPInfo.CompListStanded.Count>0)
            {
                int width1 = 0;
                int widthBack = 0;
                foreach (ColumnHeader item in this.lswComponents.Columns)
                {
                    widthBack = item.Width;
                    item.Width = -1;
                    width1 = item.Width;
                    if (width1 < widthBack)
                    {
                        item.Width = widthBack;
                    }
                    else
                    {
                        item.Width = width1;
                    }
                }
                int columnCount = this.lswComponents.Columns.Count-1;
                int widthAvg = (this.lswComponents.Width-this.lswComponents.Columns[0].Width) / columnCount;
                for (int i = 1; i < this.lswComponents.Columns.Count; i++)
                {
                    if (this.lswComponents.Columns[i].Width < widthAvg)
                    {
                        this.lswComponents.Columns[i].Width = widthAvg;
                    }
                }
               

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.pattern == null || this.pattern.CmdLineList.Count <= 0)
            {
                MessageBox.Show("创建pattern失败");
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            else
            {
                this.currPInfo.IsPatternCreated = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }
        /// <summary>
        /// 优化最短路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.currPInfo.Mark1==null || this.currPInfo.Mark2 == null)
            {
                MessageBox.Show("mark1 and mark2 must be set up");
                return;
            }
            this.currPInfo.Trajectory.TrajOptimize(this.currPInfo.Mark1, this.currPInfo.Mark2);            
            this.trajectoryChart1.AddLinePoints(this.currPInfo.Trajectory.PointsOptimized,true);
            if (this.currPInfo.Mark1 != null)
                this.trajectoryChart1.AddPoints(new List<PointD>() { this.currPInfo.Mark1.Mark },true);
            if (this.currPInfo.Mark2 != null)
                this.trajectoryChart1.AddPoints(new List<PointD> { this.currPInfo.Mark2.Mark },false);
            this.trajectoryChart1.DrawChart();
            this.listViewLoadPoints(this.currPInfo.Trajectory.PointsModified);
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReLoad_Click(object sender, EventArgs e)
        {
            this.currPInfo.Trajectory.MinX = (double)this.nudMinX.Value;
            this.currPInfo.Trajectory.MaxX = (double)this.nudMaxX.Value;
            this.currPInfo.Trajectory.MinY = (double)this.nudMinY.Value;
            this.currPInfo.Trajectory.MaxY = (double)this.nudMaxY.Value;
           
            this.currPInfo.Trajectory.PointsSelecte();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.GetPts(),true);
            this.trajectoryChart1.DrawChart();
            this.listViewLoadPoints(this.currPInfo.Trajectory.PointsModified);
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
            this.setMark(null, "Mark1");
            this.setMark(null, "Mark2");
            this.ckbMark1.Checked = false;
            this.ckbMark2.Checked = false;
        }
        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOffset_Click(object sender, EventArgs e)
        {
            this.currPInfo.Trajectory.Offset = new PointD((double)this.nudOffsetX.Value, (double)this.nudOffsetY.Value);
            this.currPInfo.Trajectory.PointsOffset();
            this.trajectoryChart1.ClearLinePoints();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.GetPts(),true);
            this.trajectoryChart1.DrawChart();
            this.listViewLoadPoints(this.currPInfo.Trajectory.PointsModified);
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
            this.setMark(null, "Mark1");
            this.setMark(null, "Mark2");
            this.ckbMark1.Checked = false;
            this.ckbMark2.Checked = false;
        }

       
        /// <summary>
        /// 生成轨迹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.currPInfo.GenerateTrajectory();
            this.trajectoryChart1.ClearLinePoints();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.GetPts(),true);
            this.trajectoryChart1.DrawChart();
            this.listViewLoadPoints(this.currPInfo.Trajectory.PointsModified);
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
            this.setMark(null, "Mark1");
            this.setMark(null, "Mark2");
            this.ckbMark1.Checked = false;
            this.ckbMark2.Checked = false;
        }
        /// <summary>
        /// 编辑元器件库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            new DialogComponentLibEdit().Setup(this.currPInfo).ShowDialog();
        }
        public void setMark(MarkPoint mark,string markName)
        {
            if (markName=="Mark1")
            {
                this.currPInfo.Mark1 = mark;
            }
            else if(markName == "Mark2")
            {
                this.currPInfo.Mark2 = mark;
            }
            this.showMarks();
        }
        private void ckbMark1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.currPInfo.Mark1!=null)
             this.currPInfo.Mark1.IsSpray = this.ckbMark1.Checked;
        }

        private void ckbMark2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.currPInfo.Mark2!=null)
                this.currPInfo.Mark2.IsSpray = this.ckbMark2.Checked;
        }
        private void showMarks()
        {
            if (this.currPInfo.Mark1 != null)
            {
                this.txtMark1X.Text = this.currPInfo.Mark1.Mark.X.ToString("F3");
                this.txtMark1Y.Text = this.currPInfo.Mark1.Mark.Y.ToString("F3");
            }
            else
            {
                this.txtMark1X.Text = 0.ToString("F3");
                this.txtMark1Y.Text = 0.ToString("F3");
            }

            if (this.currPInfo.Mark2 != null)
            {
                this.txtMark2X.Text = this.currPInfo.Mark2.Mark.X.ToString("F3");
                this.txtMark2Y.Text = this.currPInfo.Mark2.Mark.Y.ToString("F3");
            }
            else
            {
                this.txtMark2X.Text = 0.ToString("F3");
                this.txtMark2Y.Text = 0.ToString("F3");
            }
        }
               
        private void btnCreate_Click(object sender, EventArgs e)
        {            
            if (String.IsNullOrEmpty(this.txtPatternName.Text.Trim()))
            {
                if (String.IsNullOrEmpty(this.currPath))
                    return;
                this.patternName = Path.GetFileName(currPath);
            }
            else
            {
                this.patternName = this.txtPatternName.Text.Trim();
            }
            if (FluidProgram.Current != null)
            {
                if (this.currPInfo.Trajectory.PointsOptimized.Count <= 0)
                    return;
                pattern = new Pattern(FluidProgram.Current, this.patternName, FluidProgram.Current.Workpiece.Origin.X, FluidProgram.Current.Workpiece.Origin.Y);

                pattern.CmdLineList.Clear();
                List<CmdLine> cmdLineList = new List<CmdLine>();
                if (this.currPInfo.Mark1 == null || this.currPInfo.Mark2 == null)
                {
                    MessageBox.Show("mark1 and mark2 are null,please set mark1 and mark2");
                    return;
                }
                if (this.currPInfo.Mark1 != null)
                {
                    MarkCmdLine mark = new MarkCmdLine(this.currPInfo.Mark1.Mark);
                    cmdLineList.Add(mark);
                }
                if (this.currPInfo.Mark2 != null)
                {
                    MarkCmdLine mark = new MarkCmdLine(this.currPInfo.Mark2.Mark);
                    cmdLineList.Add(mark);
                }
                foreach (TrajPoint p in this.currPInfo.Trajectory.PointsModified)
                {
                    DotCmdLine dotCmdLine = new DotCmdLine();
                    dotCmdLine.Position.X = p.Mid.X;
                    dotCmdLine.Position.Y = p.Mid.Y;
                    FluidProgram.Current.ProgramSettings.DotParamList[0].NumShots = p.NumShots;
                    dotCmdLine.DotStyle = DotStyle.TYPE_1;                    
                    if (p.Weight <= 0)
                    {
                        dotCmdLine.IsWeightControl = false;
                    }
                    else
                    {
                        dotCmdLine.IsWeightControl = p.IsWeight;
                    }
                    dotCmdLine.Weight = p.Weight;
                    cmdLineList.Add(dotCmdLine);
                }
                cmdLineList.Add(new EndCmdLine());
                pattern.InsertCmdLineRange(0, cmdLineList);
                MsgCenter.Broadcast(Constants.MSG_ADD_PATTERN, this, pattern);
            }
        }

        private void lswComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currPInfo.Trajectory == null || this.currPInfo.Trajectory.PointList.Count<=0)
                return;
            if (this.lswComponents.SelectedIndices.Count>0)
            {
                TrajPoint p = this.currPInfo.Trajectory.PointsModified[this.lswComponents.SelectedIndices[0]];
                if (p.Mid!=null)
                {
                    this.trajectoryChart1.ShowPoint(p.Mid);
                }

            }
        }        

        #region Axis set

        private Dictionary<string, int> axisDic = new Dictionary<string, int>();
        private Dictionary<int, string> indexLblMap = new Dictionary<int, string>();

        private void lblR_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lblR.Text=="X")
            {
                this.lblR.Text = "Y";
                this.modifyLblDic(1, Axislabel.Y.ToString());                
            }
            else if (this.lblR.Text == "Y")
            {
                this.lblR.Text = Axislabel.N.ToString();                
                this.modifyLblDic(1, Axislabel.N.ToString());
            }
            else if (this.lblR.Text == Axislabel.N.ToString())
            {
                this.lblR.Text = "X";                
                this.modifyLblDic(1, Axislabel.X.ToString());
            }
            this.updateLbl();
        }
        
        private void lblL_Click(object sender, EventArgs e)
        {
            if (this.lblL.Text == "X")
            {
                this.lblL.Text = "Y";
                this.modifyLblDic(-1, Axislabel.Y.ToString());
            }
            else if (this.lblL.Text == "Y" )
            {
                this.lblL.Text = Axislabel.N.ToString();
                this.modifyLblDic(-1, Axislabel.N.ToString());
            }
            else if (this.lblL.Text == Axislabel.N.ToString())
            {
                this.lblL.Text = "X";
                this.modifyLblDic(-1, Axislabel.X.ToString());
            }
            this.updateLbl();
        }
       
        private void lblU_Click(object sender, EventArgs e)
        {
            if (this.lblU.Text == "X")
            {
                this.lblU.Text = "Y";
                this.modifyLblDic(2, Axislabel.Y.ToString());
            }
            else if (this.lblU.Text == "Y")
            {
                this.lblU.Text = Axislabel.N.ToString();
                this.modifyLblDic(2, Axislabel.N.ToString());
            }
            else if (this.lblU.Text == Axislabel.N.ToString())
            {
                this.lblU.Text = "X";
                this.modifyLblDic(2, Axislabel.X.ToString());
            }
            this.updateLbl();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {
            if (this.lblD.Text == "X")
            {
                this.lblD.Text = "Y";
                this.modifyLblDic(-2, Axislabel.Y.ToString());
            }
            else if (this.lblD.Text == "Y" )
            {
                this.lblD.Text = Axislabel.N.ToString();
                this.modifyLblDic(-2, Axislabel.N.ToString());
            }
            else if (this.lblD.Text == Axislabel.N.ToString())
            {
                this.lblD.Text = "X";
                this.modifyLblDic(-2, Axislabel.X.ToString());
            }
            this.updateLbl();
        }
        private void btnTransform_Click(object sender, EventArgs e)
        {
            if (!checkDicLbls())
            {
                MessageBox.Show("the coordinate set failly");
                return;
            }
            this.currPInfo.Trajectory.Reverse(this.indexLblMap);
            this.trajectoryChart1.ClearLinePoints();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.GetPts(), true);
            this.trajectoryChart1.DrawChart();
            this.listViewLoadPoints(this.currPInfo.Trajectory.PointsModified);
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
            this.setMark(null, "Mark1");
            this.setMark(null, "Mark2");
            this.ckbMark1.Checked = false;
            this.ckbMark2.Checked = false;
        }
        
        private void modifyLblDic(int key, string value)
        {            
            this.indexLblMap[key] = value;
        }
        private bool checkDicLbls()
        {
            bool isOk = false;
            int xCount = 0, yCount = 0;
            List<int> direcIndex = new List<int>();
            foreach (var item in indexLblMap.Keys)
            {
                if (this.indexLblMap[item] == Axislabel.X.ToString())
                {
                    direcIndex.Add(item);
                    xCount++;
                }
                else if(this.indexLblMap[item] == Axislabel.Y.ToString())
                {
                    direcIndex.Add(item);
                    yCount++;
                }
            }
            if (xCount == 1 && yCount == 1)
            {
                if (direcIndex.Count==2)
                {
                    if (Math.Abs(direcIndex[0]) != Math.Abs(direcIndex[1]))
                    {
                        isOk = true;
                    }
                }                
            }
            return isOk;

        }

        private void updateLbl()
        {
            this.lblR.Text = this.indexLblMap[1];
            this.lblU.Text = this.indexLblMap[2];
            this.lblL.Text = this.indexLblMap[-1];
            this.lblD.Text = this.indexLblMap[-2];
        }

        private void DicAdd(string key,int value)
        {
            if (axisDic.ContainsKey(key))
            {
                this.DicKeyRemove(key);                
            }
            axisDic.Add(key, value);
        }

        private void DicKeyRemove(string key)
        {
            if (axisDic.ContainsKey(key))
            {
                axisDic.Remove(key);
            }
        }
        private void DicValueRemove(int value)
        {
            foreach (var item in this.axisDic.Keys)
            {
                if (this.axisDic[item]==value)
                {
                    axisDic.Remove(item);
                }
            }
            
        }

        private bool checkValue(string key, int value)
        {
            bool isOk = true;
            if (key == Axis.X.ToString())
            {
                if (this.axisDic.ContainsKey(Axis.Y.ToString()))
                {                    
                    if (Math.Abs(this.axisDic[Axis.Y.ToString()])== Math.Abs(value))
                    {
                        isOk = false;
                    }
                }

            }
            else if (key == Axis.Y.ToString())
            {
                if (this.axisDic.ContainsKey(Axis.X.ToString()))
                {
                    if (Math.Abs(this.axisDic[Axis.X.ToString()]) == Math.Abs(value))
                    {
                        isOk = false;
                    }
                }
            }                      
            return isOk;
            
        }








        #endregion

        private HelpForm helpFrm;
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (this.textHelp == null)
            {
                MessageBox.Show("加载帮助文档失败");
                this.textHelp = new string[] { "no message" };
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in textHelp)
            {
                sb.AppendLine(item);
            }

            if (helpFrm == null || helpFrm.IsDisposed)
            {
                helpFrm = new HelpForm().SetUp(sb.ToString(), this);
                helpFrm.Show(this);
            }

        }
    }
}
