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

namespace Anda.Fluid.App.LoadTrajectory
{

    public partial class TrajectoryDialog : FormEx,IMsgSender
    {        
        private string patternName;       
        private Pattern pattern;
        private PatternInfo currPInfo;
        private string currPath;
        

        public TrajectoryDialog()
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
            ContextMenuStrip lswCompM = new ContextMenuStrip();
            lswCompM.Items.Add("EditComponent").Name = "EditComponent";
            lswCompM.ItemClicked += lswComp_ItemClicked;
            this.lswComponents.ContextMenuStrip = lswCompM;
            this.trajectoryChart1.Setup(this.setMark);
        }
      
        private void initialTrajectoryChart()
        {

        }
        private void lswComp_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text== "EditComponent")
            {
                new DialogComponentLibEdit().Setup(this.currPInfo).ShowDialog();
            }

        }
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                this.currPath = openFileDialog.FileName; 
                this.patternName = Path.GetFileName(currPath);
                this.txtPatternName.Text = this.patternName;
            }
            if (String.IsNullOrEmpty(currPath))
                return;           
            CADImport.Instance.AddFilePath(this.currPath);
            this.lswPathLoad();
            if (this.ckbLoadDefault.Checked)
            {
                this.currPInfo = CADImport.Instance.GetPatternInfo(this.currPath);
                this.currPInfo.GetText();
                this.currPInfo.LoadDefault();
                this.listViewLoadData();
            }            
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.currPInfo.SaveComponent();
        }
        /// <summary>
        /// 点击edit编辑分割行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.currPInfo==null)
                return;
            //弹出对话框
            new ImportEdit(this.listViewLoadData).Setup(this.currPInfo).Show();
            this.childDlgCount += 1;
        }
        private int childDlgCount = 0;
        private void TrajectoryDialog_Load(object sender, EventArgs e)
        {
            //加载元器件lib
            if (!CADImport.Instance.OnLoadASYMTEKLib())
            {
                MessageBox.Show("加载ASYMTEKLib失败","", MessageBoxButtons.OKCancel);
            }
            //当前是否有程序
            if (FluidProgram.Current == null)
            {
                this.txtPatternName.Enabled = false;
                this.lblPatternName.Enabled = false;
            }
            this.initialListView();
            this.lswPathLoad();
        }

        private void initialListView()
        {
            lswComponents.View = View.Details;
            lswComponents.GridLines = true;
            this.lswComponents.MultiSelect = false;
            this.lswComponents.FullRowSelect = true;
            //添加列  
            lswComponents.Columns.Add("Index", 30,HorizontalAlignment.Center);
            lswComponents.Columns.Add("Design", 50, HorizontalAlignment.Center);
            lswComponents.Columns.Add(HeadName.Comp.ToString(), 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(HeadName.X.ToString(), 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(HeadName.Y.ToString(), 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(HeadName.Rot.ToString(), 80, HorizontalAlignment.Center);
            lswComponents.Columns.Add(HeadName.LayOut.ToString(), 50, HorizontalAlignment.Center);
            lswComponents.BeginUpdate();
            lswComponents.EndUpdate();

            this.lswPath.View = View.Details;
            this.lswPath.GridLines = true;
            this.lswPath.MultiSelect = false;
            this.lswPath.FullRowSelect = true;

            this.lswPath.Columns.Add("Path",this.lswPath.Width, HorizontalAlignment.Left);
            this.lswPath.BeginUpdate();
            this.lswPath.EndUpdate();
        }
        /// <summary>
        /// lswComponents中加载数据
        /// </summary>
        private void listViewLoadData()
        {
            PatternInfo patInfor = this.currPInfo;         
            lswComponents.BeginUpdate();
            this.lswComponents.Items.Clear();
            int count = 0;            
            foreach (CompProperty comp in patInfor.CompListStanded)
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                item.SubItems.Add(comp.Desig);
                item.SubItems.Add(comp.Comp);
                item.SubItems.Add(comp.Mid.X.ToString("F3"));
                item.SubItems.Add(comp.Mid.Y.ToString("F3"));
                item.SubItems.Add(comp.Rotation.ToString("F3"));
                item.SubItems.Add(comp.LayOut);
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
                foreach (ColumnHeader ch in this.lswComponents.Columns)
                {
                    ch.Width = -1;
                }
                foreach (ColumnHeader ch in this.lswComponents.Columns)
                {
                    if (ch.Width < 50)
                    {
                        ch.Width = 50;
                    }
                }
            }
        }
        private void adjlswPathWidth()
        {
            foreach (ColumnHeader ch in this.lswPath.Columns)
            {
                ch.Width = -1;
                if (ch.Width <= this.lswPath.Width)
                {
                    ch.Width = this.lswPath.Width;
                }
            }
        }
        private void lswPathLoad()
        {         
            this.lswPath.BeginUpdate();
            this.lswPath.Items.Clear();
            foreach (string path in CADImport.Instance.PathPatternDic.Keys)
            {
                ListViewItem item = new ListViewItem();
                item.Text = path;
                this.lswPath.Items.Add(item);
            }           
            this.lswPath.EndUpdate();
            this.adjlswPathWidth();
        }
     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        { 
            this.Close();
        }
        /// <summary>
        /// 优化最短路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.currPInfo.Trajectory.TrajOptimize(this.currPInfo.Mark1, this.currPInfo.Mark2);            
            this.trajectoryChart1.AddLinePoints(this.currPInfo.Trajectory.PointsOptimized,true);
            if (this.currPInfo.Mark1 != null)
                this.trajectoryChart1.AddPoints(new List<PointD>() { this.currPInfo.Mark1 },true);
            if (this.currPInfo.Mark2 != null)
                this.trajectoryChart1.AddPoints(new List<PointD> { this.currPInfo.Mark2 },false);
            this.trajectoryChart1.DrawChart();
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            this.currPInfo.Trajectory.MinX = (double)this.nudMinX.Value;
            this.currPInfo.Trajectory.MaxX = (double)this.nudMaxX.Value;
            this.currPInfo.Trajectory.MinY = (double)this.nudMinY.Value;
            this.currPInfo.Trajectory.MaxY = (double)this.nudMaxY.Value;
           
            this.currPInfo.Trajectory.PointsSelecte();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.PointList,true);
            this.trajectoryChart1.DrawChart();        
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
        }
        private void btnOffset_Click(object sender, EventArgs e)
        {
            this.currPInfo.Trajectory.Offset = new PointD((double)this.nudOffsetX.Value, (double)this.nudOffsetY.Value);
            this.currPInfo.Trajectory.PointsOffset();
            this.trajectoryChart1.ClearLinePoints();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.PointList,true);
            this.trajectoryChart1.DrawChart();     
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
        }

        private void updateCompList()
        {
            CADImport.Instance.GetCurPatternInfo().CompListStand();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.currPInfo.GenerateTrajectory();
            this.trajectoryChart1.ClearLinePoints();
            this.trajectoryChart1.AddPoints(this.currPInfo.Trajectory.PointList,true);
            this.trajectoryChart1.DrawChart();
            this.lblDistance.Text = this.currPInfo.Trajectory.Distance.ToString("F3");
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
        public void setMark(PointD mark,string markName)
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
        private void showMarks()
        {
            if (this.currPInfo.Mark1 != null)
            {
                this.txtMark1X.Text = this.currPInfo.Mark1.X.ToString("F3");
                this.txtMark1Y.Text = this.currPInfo.Mark1.Y.ToString("F3");
            }
            else
            {
                this.txtMark1X.Text = 0.ToString("F3");
                this.txtMark1Y.Text = 0.ToString("F3");
            }

            if (this.currPInfo.Mark2 != null)
            {
                this.txtMark2X.Text = this.currPInfo.Mark2.X.ToString("F3");
                this.txtMark2Y.Text = this.currPInfo.Mark2.Y.ToString("F3");
            }
            else
            {
                this.txtMark2X.Text = 0.ToString("F3");
                this.txtMark2Y.Text = 0.ToString("F3");
            }
        }

        private void lswPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currPath = String.Empty;
            if (this.lswPath.SelectedIndices.Count > 0)
            {
                if (this.lswPath.SelectedIndices[0] > -1)
                {
                    this.currPath = this.lswPath.SelectedItems[0].Text;
                }
                else
                {
                    return;
                }
            }          
            if (String.IsNullOrEmpty(currPath))
            {
                return;
            }
            this.patternName = Path.GetFileName(currPath);
            this.txtPatternName.Text = this.patternName;
            this.currPInfo = CADImport.Instance.GetPatternInfo(currPath);
         
            this.listViewLoadData();
            //画图
            this.btnGenerate_Click(null, null);
        }

        private void lswPath_MouseEnter(object sender, EventArgs e)
        {
            //this.lswPath.Items[0].Selected = true;
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
                if (this.currPInfo.Mark1 != null)
                {
                    MarkCmdLine mark = new MarkCmdLine(this.currPInfo.Mark1);
                    cmdLineList.Add(mark);
                }
                if (this.currPInfo.Mark2 != null)
                {
                    MarkCmdLine mark = new MarkCmdLine(this.currPInfo.Mark2);
                    cmdLineList.Add(mark);
                }
                foreach (PointD p in this.currPInfo.Trajectory.PointsOptimized)
                {
                    DotCmdLine dotCmdLine = new DotCmdLine();
                    dotCmdLine.Position.X = p.X;
                    dotCmdLine.Position.Y = p.Y;
                    dotCmdLine.DotStyle = DotStyle.TYPE_1;
                    dotCmdLine.IsWeightControl = true;
                    dotCmdLine.Weight = 0.0;
                    cmdLineList.Add(dotCmdLine);
                }
                cmdLineList.Add(new EndCmdLine());
                pattern.InsertCmdLineRange(0, cmdLineList);
                MsgCenter.Broadcast(Constants.MSG_ADD_PATTERN, this, pattern);
            }
        }

        
    }
}
