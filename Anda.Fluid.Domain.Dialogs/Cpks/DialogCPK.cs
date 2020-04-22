using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Cpk;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public partial class DialogCPK : JogFormBase, IAlarmSenderable
    {
        
        private PropertyGrid prog = new PropertyGrid();
        private CpkPrm cpkPrmBackUp = new CpkPrm();
        private CpkType cpkType = CpkType.None;
        private InspectionDot inspection;
        public DialogCPK()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.prog.Parent = this.panelCpk;
            this.prog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prog.Width = 200;

            this.prog.CategoryForeColor = Color.Black;
            this.prog.ToolbarVisible = false;
            this.prog.ExpandAllGridItems();
            this.prog.PropertySort = PropertySort.Categorized;

            this.txtXStartX.Text = Convert.ToString(0.000);
            this.txtXStartY.Text = Convert.ToString(0.000);
            this.txtXEndX.Text = Convert.ToString(0.000);
            this.txtXEndY.Text = Convert.ToString(0.000);
            this.txtPosZ1.Text = Convert.ToString(0.000);

            this.txtYStartX.Text = Convert.ToString(0.000);
            this.txtYStartY.Text = Convert.ToString(0.000);
            this.txtYEndX.Text = Convert.ToString(0.000);
            this.txtYEndY.Text = Convert.ToString(0.000);
            this.txtPosZ2.Text = Convert.ToString(0.000);

            this.txtXYStartX.Text = Convert.ToString(0.000);
            this.txtXYStartY.Text = Convert.ToString(0.000);
            this.txtXYEndX.Text = Convert.ToString(0.000);
            this.txtXYEndY.Text = Convert.ToString(0.000);
            this.txtPosZ3.Text = Convert.ToString(0.000);

            this.txtZStart.Text = Convert.ToString(0.000);
            this.txtZEnd.Text = Convert.ToString(0.000);
            this.txtZPosX.Text = Convert.ToString(0.000);
            this.txtZPosY.Text = Convert.ToString(0.000);

            this.inspection = InspectionMgr.Instance.FindBy(0) as InspectionDot;

        }
        public object Obj => this;

        private bool isRunning = false;
       
        public  DialogCPK Setup()
        {
            if (!CPKMgr.Instance.LoadPrm())
            {
                CPKMgr.Instance.Prm = new CpkPrm();
            }
            CPKMgr.Instance.Initial();
            this.cpkPrmBackUp = (CpkPrm)CPKMgr.Instance.Prm.Clone();
            return this;
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveProportyGridLngText(new CpkPrm());
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private async void button_Click(object sender, EventArgs e)
        {            
            var button = sender as Button;
            this.DisableButton();
            this.isRunning = true;
            await Task.Factory.StartNew(() =>
            {
                try
                {            
                    switch (button.Name)
                    {
                        case "btnValve1":
                            cpkType = CpkType.Valve1;
                            CPKMgr.Instance.ExecuteOne(typeof(Valve1WeightCPK).Name);
                            break;
                        case "btnValve2":
                            cpkType = CpkType.Valve2;
                            CPKMgr.Instance.ExecuteOne(typeof(Valve2WeightCPK).Name);
                            break;
                        case "btnAxisX":
                            cpkType = CpkType.AxisXCPK;
                            this.DoAxisXCPK();
                            break;
                        case "btnAxisY":
                            cpkType = CpkType.AxisYCPK;
                            this.DoAxisYCPK();
                            break;
                        case "btnAxisXY":
                            cpkType = CpkType.AxisXYCPK;
                            this.DoAxisXYCPK();
                            break;
                        case "btnAxisZ":
                            cpkType = CpkType.AxisZCPK;
                            this.DoAxisZCPK();
                            break;
                        default:
                            cpkType = CpkType.None;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }

            });
            
            this.prog.ExpandAllGridItems();
            this.prog.Refresh();
            this.EnableButton();
            this.isRunning = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            switch (this.cpkType)
            {
                case CpkType.Valve1:
                    CPKMgr.Instance.StopOne(typeof(Valve1WeightCPK).Name);
                    break;
                case CpkType.Valve2:
                    CPKMgr.Instance.StopOne(typeof(Valve2WeightCPK).Name);
                    break;
                case CpkType.AxisXCPK:
                    CPKMgr.Instance.StopOne(typeof(AxisXCPK).Name);
                    break;
                case CpkType.AxisYCPK:
                    CPKMgr.Instance.StopOne(typeof(AxisYCPK).Name);
                    break;
                case CpkType.AxisXYCPK:
                    CPKMgr.Instance.StopOne(typeof(AxisXYCPK).Name);
                    break;
                case CpkType.AxisZCPK:
                    CPKMgr.Instance.StopOne(typeof(AxisZCPK).Name);
                    break;
            }
            this.isRunning = false;
        }
        private void DoAxisXCPK()
        {
            if (!this.cbUseASV.Checked)
            {
                this.checkGage();
            }
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ1.Text));
            if (!res.IsOk)
            {
                return;
            }
            
            res = CpkMove.MoveToPosHigh(double.Parse(this.txtXStartX.Text), double.Parse(this.txtXStartY.Text));
            if (!res.IsOk)
            {
                return;
            }
                      
            double diff=double.Parse(this.txtXEndY.Text) - double.Parse(this.txtXStartY.Text);
            if (Math.Abs(diff)>5)
            {
                MessageBox.Show("示教时请确保设备Y轴固定", "", MessageBoxButtons.OKCancel);
                return;
            }
            CPKMgr.Instance.ExecuteOne(typeof(AxisXCPK).Name);
        }

        private void DoAxisYCPK()
        {
            if (!this.cbUseASV.Checked)
            {
                this.checkGage();
            }
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ2.Text));
            if (!res.IsOk)
            {
                return;
            }
           
            res = CpkMove.MoveToPosHigh(double.Parse(this.txtYStartX.Text), double.Parse(this.txtYStartY.Text));
            if (!res.IsOk)
            {
                return;
            }
            
            double diff = double.Parse(this.txtYEndX.Text) - double.Parse(this.txtYStartX.Text);
            if (Math.Abs(diff) > 5)
            {
                MessageBox.Show("示教时请确保设备X轴固定", "", MessageBoxButtons.OKCancel);
                return;
            }
            CPKMgr.Instance.ExecuteOne(typeof(AxisYCPK).Name);
        }

        private void DoAxisXYCPK()
        {
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ3.Text));
            if (!res.IsOk)
            {
                return;
            }

            res = CpkMove.MoveToPosHigh(double.Parse(this.txtXYStartX.Text), double.Parse(this.txtXYStartY.Text));
            if (!res.IsOk)
            {
                return;
            }
            CPKMgr.Instance.ExecuteOne(typeof(AxisXYCPK).Name);
        }

        private void DoAxisZCPK()
        {
            if (!this.cbUseLaser.Checked)
            {
                this.checkGage();
            }
            Result res = Result.OK;
            res = CpkMove.MoveZHigh(double.Parse(this.txtZStart.Text));
            if (res != Result.OK)
            {
                return;
            }
            res = CpkMove.MoveZHigh(double.Parse(this.txtZStart.Text));
            if (res != Result.OK)
            {
                return;
            }
            CPKMgr.Instance.ExecuteOne(typeof(AxisZCPK).Name);
        }

        public void DisableButton()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            this.btnStop.Enabled = true;
           
        }
        public void EnableButton()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
                   
        }
       
        private void DialogCPK_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isRunning)
            {
                e.Cancel = true;
                MessageBox.Show("CPK 测试执行中....");
                return;
            }
            CPKMgr.Instance.SavePrm();
            CompareObj.CompareProperty(CPKMgr.Instance.Prm, this.cpkPrmBackUp, null, this.GetType().Name);
        }

        private void DialogCPK_Load(object sender, EventArgs e)
        {
            LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(CPKMgr.Instance.Prm,this.GetType().Name);
            this.prog.SelectedObject = proxyObj;            
            this.prog.ExpandAllGridItems();           
            //Result ret = Machine.Instance.DigitalGage.Init();          
        }
        /// <summary>
        /// X轴起点示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisXTeach1_Click(object sender, EventArgs e)
        {
            this.txtXStartX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtXStartY.Text = Machine.Instance.Robot.PosY.ToString("F3");
            AxisXCPK cpk =CPKMgr.Instance.FindByName(typeof(AxisXCPK).Name) as AxisXCPK;
            if (cpk != null)
            {
                cpk.Start = new PointD(double.Parse(this.txtXStartX.Text), double.Parse(this.txtXStartY.Text));
            }
        }
        /// <summary>
        /// X轴终点示教 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisXTeach2_Click(object sender, EventArgs e)
        {
            this.txtXEndX.Text=Machine.Instance.Robot.PosX.ToString("F3");
            this.txtXEndY.Text= Machine.Instance.Robot.PosY.ToString("F3");
            this.txtPosZ1.Text = Machine.Instance.Robot.PosZ.ToString("F3");
            AxisXCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXCPK).Name) as AxisXCPK;
            if (cpk != null)
            {
                cpk.End = new PointD(double.Parse(this.txtXEndX.Text), double.Parse(this.txtXEndY.Text));
                cpk.PosZ = double.Parse(this.txtPosZ1.Text);
            }
        }
        /// <summary>
        /// 移动到X轴起点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisXGoTo1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(()=> 
            {
                Result res = Result.OK;
                res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ1.Text));
                if (!res.IsOk)
                {
                    return;
                }
                AxisXCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXCPK).Name) as AxisXCPK;
                if (cpk != null)
                {
                    res = CpkMove.MoveToPosHigh(double.Parse(this.txtXStartX.Text), double.Parse(this.txtXStartY.Text));
                    if (!res.IsOk)
                    {
                        return;
                    }
                }
               
            });
            
             
        }
        /// <summary>
        /// 移动到X轴终点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisXGoTo2_Click(object sender, EventArgs e)
        {
            double diff = double.Parse(this.txtXEndY.Text) - double.Parse(this.txtXStartY.Text);
            if (Math.Abs(diff) > 5)
            {
                MessageBox.Show("示教时请确保设备Y轴固定", "", MessageBoxButtons.OKCancel);
                return;
            }
            Task.Factory.StartNew(()=> {
                Result res = Result.OK;
                res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ1.Text));
                if (!res.IsOk)
                {
                    return;
                }
                AxisXCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXCPK).Name) as AxisXCPK;
                if (cpk != null)
                {
                    PointD p = cpk.calPoint();
                    res = CpkMove.MoveToPosHigh(p.X, p.Y);
                    if (!res.IsOk)
                    {
                        return;
                    }
                    res = CpkMove.MoveToPosSlowly(double.Parse(this.txtXEndX.Text), double.Parse(this.txtXEndY.Text));
                    if (!res.IsOk)
                    {
                        return;
                    }

                }
            });
         
        }
                
        /// <summary>
        /// Y 轴起点示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisYTeach1_Click(object sender, EventArgs e)
        {
            this.txtYStartX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtYStartY.Text = Machine.Instance.Robot.PosY.ToString("F3");
            
            AxisYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisYCPK).Name) as AxisYCPK;
            if (cpk != null)
            {
                cpk.Start = new PointD(double.Parse(this.txtYStartX.Text), double.Parse(this.txtYStartY.Text));
            }
        }
        /// <summary>
        /// Y 轴终点示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisYTeach2_Click(object sender, EventArgs e)
        {
            this.txtYEndX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtYEndY.Text = Machine.Instance.Robot.PosY.ToString("F3");
            this.txtPosZ2.Text = Machine.Instance.Robot.PosZ.ToString("F3");
            AxisYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisYCPK).Name) as AxisYCPK;
            if (cpk != null)
            {
                cpk.End = new PointD(double.Parse(this.txtYEndX.Text), double.Parse(this.txtYEndY.Text));
                cpk.PosZ = double.Parse(this.txtPosZ2.Text);

            }

        }
        /// <summary>
        /// Y轴起点再现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisYGoTo1_Click(object sender, EventArgs e)
        {
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ2.Text));
            if (!res.IsOk)
            {
                return;
            }
            AxisYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisYCPK).Name) as AxisYCPK;
            if (cpk != null)
            {
                res = CpkMove.MoveToPosHigh(double.Parse(this.txtYStartX.Text), double.Parse(this.txtYStartY.Text));
                if (!res.IsOk)
                {
                    return;
                }
            }
            //CpkMove.moveToPos(double.Parse(this.txtYStartX.Text), double.Parse(this.txtYStartY.Text), double.Parse(this.txtPosZ2.Text));
        }
        /// <summary>
        /// Y轴终点再现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisYGoTo2_Click(object sender, EventArgs e)
        {
            double diff = double.Parse(this.txtYEndX.Text) - double.Parse(this.txtYStartX.Text);
            if (Math.Abs(diff) > 5)
            {
                MessageBox.Show("示教时请确保设备X轴固定", "", MessageBoxButtons.OKCancel);
                return;
            }
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ2.Text));
            if (!res.IsOk)
            {
                return;
            }
            AxisYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisYCPK).Name) as AxisYCPK;
            if (cpk != null)
            {
                PointD p = cpk.calPoint();
                res = CpkMove.MoveToPosHigh(p.X, p.Y);
                if (!res.IsOk)
                {
                    return;
                }
                res = CpkMove.MoveToPosSlowly(double.Parse(this.txtYEndX.Text), double.Parse(this.txtYEndY.Text));
                if (!res.IsOk)
                {
                    return;
                }

            }
            //CpkMove.moveToPosDown(double.Parse(this.txtYEndX.Text), double.Parse(this.txtYEndY.Text), double.Parse(this.txtPosZ2.Text));
        }
        /// <summary>
        /// Z轴起点示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisZTeach1_Click(object sender, EventArgs e)
        {
            this.txtZStart.Text = Machine.Instance.Robot.PosZ.ToString("F3");            
            AxisZCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisZCPK).Name) as AxisZCPK;
            if (cpk!=null)
            {
                cpk.ZStart = double.Parse(this.txtZStart.Text);
            }

        }
        /// <summary>
        /// Z轴终点示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisZTeach2_Click(object sender, EventArgs e)
        {
            this.txtZEnd.Text = Machine.Instance.Robot.PosZ.ToString("F3");
            this.txtZPosX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtZPosY.Text = Machine.Instance.Robot.PosY.ToString("F3");
            AxisZCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisZCPK).Name) as AxisZCPK;
            if (cpk!=null)
            {
                cpk.ZEnd = double.Parse(this.txtZEnd.Text);
                cpk.Pos = new PointD(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text));
            }
        }
        /// <summary>
        /// Z轴起点再现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisZGoTo1_Click(object sender, EventArgs e)
        {
            Result res = Result.OK;

            //res = Machine.Instance.Robot.MovePosXYAndReply(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text));
            res = Machine.Instance.Robot.ManualMovePosXYAndReply(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text));
            if (res != Result.OK)
            {
                return;
            }
            res = CpkMove.MoveZHigh(double.Parse(this.txtZStart.Text));
            if (res != Result.OK)
            {
                return;
            }
            
            //this.move4CpkZ(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text), double.Parse(this.txtZStart.Text));
        }
        /// <summary>
        /// Z轴终点再现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAxisZGoTo2_Click(object sender, EventArgs e)
        {
            
            Result res = Result.OK;
            //res = Machine.Instance.Robot.MovePosXYAndReply(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text));
            res = Machine.Instance.Robot.ManualMovePosXYAndReply(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text));
            if (res != Result.OK)
            {
                return;
            }
            AxisZCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisZCPK).Name) as AxisZCPK;
            if (cpk != null)
            {
                double z = cpk.calPoint();
                res = CpkMove.MoveZHigh(z);
                if (!res.IsOk)
                {
                    return;
                }
                res = CpkMove.MoveZSlowly(double.Parse(this.txtZEnd.Text));
                if (!res.IsOk)
                {
                    return;
                }

            }
            //this.move4CpkZ(double.Parse(this.txtZPosX.Text), double.Parse(this.txtZPosY.Text), double.Parse(this.txtZEnd.Text));
        }
        
        private void checkGage()
        {
            Result ret = Machine.Instance.DigitalGage.Init();
            if (!ret.IsOk)
            {
                //MessageBox.Show("Connection to the gage connect failed");
                MessageBox.Show("连接高度规失败");
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void btnEditASV_Click(object sender, EventArgs e)
        {
            this.inspection.SetImage(
               Machine.Instance.Camera.Executor.CurrentBytes,
               Machine.Instance.Camera.Executor.ImageWidth,
               Machine.Instance.Camera.Executor.ImageHeight);
            this.inspection.ShowEditWindow();
        }

        private void cbUseASV_CheckedChanged(object sender, EventArgs e)
        {
            int cpkMeasureType = this.cbUseASV.Checked ? 1 : 0;
            (CPKMgr.Instance.FindByName(typeof(AxisXCPK).Name) as AxisXCPK).CpkMeasureType = cpkMeasureType;
            (CPKMgr.Instance.FindByName(typeof(AxisYCPK).Name) as AxisYCPK).CpkMeasureType = cpkMeasureType;
        }

        private void cbUseLaser_CheckedChanged(object sender, EventArgs e)
        {
            (CPKMgr.Instance.FindByName(typeof(AxisZCPK).Name) as AxisZCPK).CpkMeasureType = this.cbUseLaser.Checked ? 1 : 0;
        }

        private void btnAxisXYTeach1_Click(object sender, EventArgs e)
        {
            this.txtXYStartX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtXYStartY.Text = Machine.Instance.Robot.PosY.ToString("F3");

            AxisXYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXYCPK).Name) as AxisXYCPK;
            if (cpk != null)
            {
                cpk.Start = new PointD(double.Parse(this.txtXYStartX.Text), double.Parse(this.txtXYStartY.Text));
            }
        }

        private void btnAxisXYTeach2_Click(object sender, EventArgs e)
        {
            this.txtXYEndX.Text = Machine.Instance.Robot.PosX.ToString("F3");
            this.txtXYEndY.Text = Machine.Instance.Robot.PosY.ToString("F3");
            this.txtPosZ3.Text = Machine.Instance.Robot.PosZ.ToString("F3");
            AxisXYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXYCPK).Name) as AxisXYCPK;
            if (cpk != null)
            {
                cpk.End = new PointD(double.Parse(this.txtXYEndX.Text), double.Parse(this.txtXYEndY.Text));
                cpk.PosZ = double.Parse(this.txtPosZ3.Text);
            }
        }

        private void btnAxisXYGoTo1_Click(object sender, EventArgs e)
        {
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ3.Text));
            if (!res.IsOk)
            {
                return;
            }
            AxisXYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXYCPK).Name) as AxisXYCPK;
            if (cpk != null)
            {
                res = CpkMove.MoveToPosHigh(double.Parse(this.txtXYStartX.Text), double.Parse(this.txtXYStartY.Text));
                if (!res.IsOk)
                {
                    return;
                }
            }
        }

        private void btnAxisXYGoTo2_Click(object sender, EventArgs e)
        {
            Result res = Result.OK;
            res = Machine.Instance.Robot.MovePosZAndReply(double.Parse(this.txtPosZ3.Text));
            if (!res.IsOk)
            {
                return;
            }
            AxisXYCPK cpk = CPKMgr.Instance.FindByName(typeof(AxisXYCPK).Name) as AxisXYCPK;
            if (cpk != null)
            {
                res = CpkMove.MoveToPosSlowly(double.Parse(this.txtXYEndX.Text), double.Parse(this.txtXYEndY.Text));
                if (!res.IsOk)
                {
                    return;
                }
            }
        }
    }

    public class AlarmInfoCPK
    {
        public static AlarmInfo CPKgotoError => AlarmInfo.Create(AlarmLevel.Error, 750,"轴CPK", "移动到CPK位置时发生错误", AlarmHandleType.ImmediateHandle);//do Axis cpk , go to the CPK position error 
    }
    public enum CpkType
    {
        None,
        Valve1,
        Valve2,
        AxisXCPK,
        AxisYCPK,
        AxisXYCPK,
        AxisZCPK
    }
}
