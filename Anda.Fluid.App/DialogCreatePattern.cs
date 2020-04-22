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
using System.Runtime.Serialization.Formatters.Binary;
using CADImportV1._0.Elements;
using CADImportV1._0;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Structure;

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class DialogCreatePattern : FormEx,IMsgSender
    {        
        private string patternName;       
        private Pattern pattern;
        private PatternInfo currPInfo;
        private string currPath;
        public DialogCreatePattern()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

         
            this.txtComponent1.ReadOnly = true;
            this.txtComponent2.ReadOnly = true;
            this.txtMark1X.ReadOnly = true;
            this.txtMark1Y.ReadOnly = true;
            this.txtMark2X.ReadOnly = true;
            this.txtMark2Y.ReadOnly = true;

            this.txtOriginX.ReadOnly = true;
            this.txtOriginY.ReadOnly = true;

            this.initialListView();
        }
        #region 
        private void initialListView()
        {
            lswComponents.View = View.Details;
            lswComponents.GridLines = true;
            this.lswComponents.MultiSelect = true;
            this.lswComponents.FullRowSelect = true;
            this.lswComponents.Columns.Clear();

            lswComponents.Columns.Add("序号", 50, HorizontalAlignment.Center);
        
            lswComponents.Columns.Add("符号", 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add("X坐标", 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add("Y坐标", 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add("角度", 80, HorizontalAlignment.Center);
            lswComponents.Columns.Add("元器件名称", 90, HorizontalAlignment.Center);
            lswComponents.Columns.Add("部件编号", 120, HorizontalAlignment.Center);
            lswComponents.Columns.Add("层", 100, HorizontalAlignment.Center);
            //添加列  
            lswComponents.BeginUpdate();
            lswComponents.EndUpdate();

        }

        private void listViewLoadData(List<CompProperty> comps)
        {
            if (this.currPInfo == null)
                return;
            lswComponents.BeginUpdate();
            this.lswComponents.Items.Clear();
            int count = 0;
            foreach (CompProperty comp in comps)
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                item.SubItems.Add(comp.Desig);
                item.SubItems.Add(comp.Mid.X.ToString("F3"));
                item.SubItems.Add(comp.Mid.Y.ToString("F3"));
                item.SubItems.Add(comp.Rotation.ToString("F3"));
                item.SubItems.Add(comp.Comp);
                item.SubItems.Add(comp.PartNumber);
                item.SubItems.Add(comp.LayOut);
                lswComponents.Items.Add(item);
            }
            lswComponents.EndUpdate();
           
        }
        private CompProperty currentComp;
        private int currentIndex = 0;
        private void lswComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lswComponents.SelectedIndices.Count > 0 && this.lswComponents.SelectedIndices[0] > -1)
            {
                this.currentComp = this.currPInfo.CompsReversed[this.lswComponents.SelectedIndices[0]];
                this.currentIndex = this.lswComponents.SelectedIndices[0];
            }

        }

        private void btnSelectComp1_Click(object sender, EventArgs e)
        {
            StringBuilder compInfo = new StringBuilder();
            if (currentComp != null)
            {
                markComp1 = currentComp;
                compInfo.Append(this.markComp1.Desig).Append("[").Append(this.markComp1.Mid.X.ToString("0.000")).Append(",").Append(this.markComp1.Mid.Y.ToString("0.000")).Append("]");
                this.txtComponent1.Text = compInfo.ToString();
            }
        }

        private void btnSelectComp2_Click(object sender, EventArgs e)
        {
            StringBuilder compInfo = new StringBuilder();
            if (currentComp != null)
            {
                markComp2 = currentComp;
                compInfo.Append(this.markComp2.Desig).Append("[").Append(this.markComp2.Mid.X.ToString("0.000")).Append(",").Append(this.markComp2.Mid.Y.ToString("0.000")).Append("]");
                this.txtComponent2.Text = compInfo.ToString();
            }
        }
      
        #endregion


        #region mark校正
        private PointD patternOrigin;
        private PointD patternOldOrg;//系统 改为机械
        private PointD patternNewOrg;//系统 改为机械

        private CompProperty markComp1;//系统
        private CompProperty markComp2;//系统
        private PointD mark1;//系统 改为机械
        private PointD mark2;//系统 改为机械

        //系统
        private CoordinateTransformer coordinateTransformer = new CoordinateTransformer();
        private bool isCalculated = false;
        private void btnTeachMark1_Click(object sender, EventArgs e)
        {
            PointD p=new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.txtMark1X.Text = p.X.ToString("0.000");
            this.txtMark1Y.Text = p.Y.ToString("0.000");
            //mark1 = p.ToSystem();
            this.mark1 = p;
        }

        private void btnTeachMark2_Click(object sender, EventArgs e)
        {
            PointD p = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.txtMark2X.Text = p.X.ToString("0.000");
            this.txtMark2Y.Text = p.Y.ToString("0.000");
            //this.mark2 = p.ToSystem();
            this.mark2 = p;
        }

        private void btnGto1_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.ManualMovePosXYAndReply(double.Parse(this.txtMark1X.Text), double.Parse(this.txtMark1Y.Text));
        }

        private void btnGto2_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.ManualMovePosXYAndReply(double.Parse(this.txtMark2X.Text), double.Parse(this.txtMark2Y.Text));
        }


        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.patternOrigin = new PointD();
        }
              
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (this.markComp1==null || this.markComp2==null || this.mark1==null || this.mark2==null)
            {
                MessageBox.Show("请先指定两个元器件坐标或拾取两个mark点");
                return;
            }
            this.patternOldOrg = new PointD(0, 0);
            this.patternOldOrg = this.patternOldOrg.ToMachine();
            PointD comp1 = new PointD(markComp1.Mid.X, markComp1.Mid.Y);
            PointD comp2 = new PointD(markComp2.Mid.X, markComp2.Mid.Y);

            this.coordinateTransformer.SetMarkPoint(comp1.ToMachine(), comp2.ToMachine(), this.mark1, this.mark2);
            this.patternNewOrg=this.coordinateTransformer.Transform(this.patternOldOrg);

            this.txtOriginX.Text = this.patternNewOrg.X.ToString("0.000");
            this.txtOriginY.Text = this.patternNewOrg.Y.ToString("0.000");
            this.isCalculated = true;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (this.currentIndex == 0)
            {
                this.currentIndex = 0;
            }
            else
            {
                this.currentIndex--;
            }
            this.currentComp = this.currPInfo.CompsReversed[this.currentIndex];

            PointD transformed = this.coordinateTransformer.Transform(new PointD(this.currentComp.Mid.X, this.currentComp.Mid.Y).ToMachine());
            Machine.Instance.Robot.ManualMovePosXYAndReply(transformed.X, transformed.Y);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.currentIndex == this.currPInfo.CompsReversed.Count - 1)
            {
                this.currentIndex = this.currPInfo.CompsReversed.Count - 1;
            }
            else
            {
                this.currentIndex++;
            }
            this.currentComp = this.currPInfo.CompsReversed[this.currentIndex];
            PointD transformed = this.coordinateTransformer.Transform(new PointD(this.currentComp.Mid.X, this.currentComp.Mid.Y).ToMachine());
            Machine.Instance.Robot.ManualMovePosXYAndReply(transformed.X, transformed.Y);
        }

        #endregion




        private void TrajectoryDialog_Load(object sender, EventArgs e)
        {
            if (FluidProgram.Current == null)
            {
                this.btnLoad.Enabled = false;            
                this.btnOK.Enabled = false;
                this.btnCreate.Enabled = false;
                this.btnImport.Enabled = false;                
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
        /// 创建pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!this.isCalculated)
            {
                MessageBox.Show("请先计算pattern原点");
                return;
            }
            if (FluidProgram.Current == null)
            {
                MessageBox.Show("当前程序为空，请先加载或新建程序");
                this.btnOK.Enabled = false;               
                return;
            }
            else if (FluidProgram.Current != null)
            {
                if (this.currPInfo == null)
                    return;
                if (this.currPInfo.Trajectory.PointsModified.Count <= 0)
                    return;
                //double x=this.patternNewOrg.X - FluidProgram.Current.Workpiece.GetOriginPos().ToSystem().X;
                //double y= this.patternNewOrg.Y - FluidProgram.Current.Workpiece.GetOriginPos().ToSystem().Y;
                PointD org=(this.patternNewOrg - FluidProgram.Current.Workpiece.GetOriginPos()).ToPoint().ToSystem();
                pattern = new Pattern(FluidProgram.Current, this.patternName, org.X, org.Y);

                pattern.CmdLineList.Clear();
                List<CmdLine> cmdLineList = new List<CmdLine>();
                if (this.currPInfo.Trajectory.MarkPt1 == null || this.currPInfo.Trajectory.MarkPt2 == null)
                {
                    //MessageBox.Show("mark1 and mark2 are null,please set mark1 and mark2");
                    MessageBox.Show("mark1点和mark2点没有设置,请设置mark1点和mark2点");
                    return;
                }
                if (this.currPInfo.Trajectory.MarkPt1 != null)
                {
                    CADImportV1._0.Infrastructure.PointD pd1 = this.currPInfo.Trajectory.MarkPt1.Mid;
                    MarkCmdLine mark = new MarkCmdLine(new PointD(pd1.X, pd1.Y));
                    mark.TrackNumber = this.currPInfo.Trajectory.MarkPt1.Desig;
                    cmdLineList.Add(mark);
                }
                if (this.currPInfo.Trajectory.MarkPt2 != null)
                {
                    CADImportV1._0.Infrastructure.PointD pd2 = this.currPInfo.Trajectory.MarkPt2.Mid;  
                     MarkCmdLine mark = new MarkCmdLine(new PointD(pd2.X, pd2.Y));
                    mark.TrackNumber = this.currPInfo.Trajectory.MarkPt2.Desig;
                    cmdLineList.Add(mark);
                }
                foreach (TrajPoint p in this.currPInfo.Trajectory.PointsModified)
                {
                    if (p.Desig.Contains("(Blank)"))
                    {
                        MoveXyCmdLine moveXyCmdLine = new MoveXyCmdLine(p.Mid.X, p.Mid.Y, false);
                        moveXyCmdLine.TrackNumber = p.Desig;
                        cmdLineList.Add(moveXyCmdLine);
                    }
                    else 
                    {
                        DotCmdLine dotCmdLine = new DotCmdLine();
                        dotCmdLine.Position.X = p.Mid.X;
                        dotCmdLine.Position.Y = p.Mid.Y;
                        dotCmdLine.NumShots = p.NumShots;
                        dotCmdLine.TrackNumber = p.Desig;
                        dotCmdLine.Rotation = p.Rotation.ToString("0.00");
                        dotCmdLine.Comp = p.Comp;

                        if (!p.IsWeight)
                        {
                            dotCmdLine.IsAssign = true;
                        }
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
                }
                cmdLineList.Add(new EndCmdLine());
                pattern.InsertCmdLineRange(0, cmdLineList);
                
                //进行校正
                PatternCorrector.Instance.correctPatternRecursive(pattern,this.patternOldOrg,this.patternNewOrg, this.coordinateTransformer);
                MsgCenter.Broadcast(Constants.MSG_ADD_PATTERN, this, pattern);
            }
        }
        /// <summary>
        /// 加载pattern文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "PATTERN|*.pattern";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.currPath = openFileDialog.FileName;
            this.txtFile.Text = this.currPath;            
            if (!this.LoadPatternInfo(this.currPath))
            {
                MessageBox.Show("加载轨迹文件失败");
                return;
            }
            this.listViewLoadData(this.currPInfo.CompsReversed);
        }

        public  bool LoadPatternInfo(string programPath)
        {
            Stream fstream = null;
            try
            {
                string extension = Path.GetExtension(programPath);
                if (extension != ".pattern")
                {
                    MessageBox.Show("请确认打开的是后缀为pattern的文件");
                    return false;
                }
                fstream = new FileStream(programPath, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();
                this.currPInfo = (PatternInfo)binFormat.Deserialize(fstream);
                return true;
               
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            new CADImportForm().ShowDialog();
        }

       
    }
}
