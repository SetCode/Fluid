using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.SVO.SubForms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.SVO
{
    public partial class SVOForm : FormEx, ICheckedChangable
    {
        public static SVOForm Instance;

        #region
        private StepState clickSts0 = new StepState(0);
        private StepState clickSts1 = new StepState(1);
        private StepState clickSts2 = new StepState(2);
        private StepState clickSts3 = new StepState(3);
        private StepState clickSts4 = new StepState(4);
        private StepState clickSts5 = new StepState(5);
        private StepState clickSts6 = new StepState(6);
        private StepState clickSts7 = new StepState(7);
        private StepState clickSts8 = new StepState(8);
        private StepState clickSts9 = new StepState(9);
        private StepState clickSts10 = new StepState(10);
        private StepState clickSts11 = new StepState(11);
        private StepState clickSts12 = new StepState(12);
        private StepState clickSts13 = new StepState(13);
        private StepState clickSts14 = new StepState(14);
        #endregion
        private int selectedStep = 0;

        public bool IsRunToEnd = false;


        public SVOForm(bool showExit = true)
        {
            InitializeComponent();
            if(!showExit)
            {
                this.btnExit.Visible = false;
            }
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            Instance = this;

            DataSetting.Load();
            this.LoadStepState();

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.tlpTaskVavel2.Hide();
                this.toxOffsetX.Hide();
                this.toxOffsetY.Hide();
                this.toxOffsetZ.Hide();
                this.lelOffsetXY.Hide();
                this.lelOffsetZ.Hide();
            }

            this.ReadLanguageResources();
        }
        public void LoadStepState()
        {

            if (DataSetting.Default.IsReStart)
           {
                StepStateMgr.Instance
                          .Add(clickSts0)
                          .Add(clickSts1)
                          .Add(clickSts2)
                          .Add(clickSts3)
                          .Add(clickSts4)
                          .Add(clickSts5)
                          .Add(clickSts6)
                          .Add(clickSts7)
                          .Add(clickSts8)
                          .Add(clickSts9)
                          .Add(clickSts10)
                          .Add(clickSts11)
                          .Add(clickSts12)
                          .Add(clickSts13)
                          .Add(clickSts14);
           }                
            else
            {
                if (StepStateMgr.Instance.FindBy(0) == null)
                {
                    StepStateMgr.Instance
                        .Add(clickSts0)
                        .Add(clickSts1)
                        .Add(clickSts2)
                        .Add(clickSts3)
                        .Add(clickSts4)
                        .Add(clickSts5)
                        .Add(clickSts6)
                        .Add(clickSts7)
                        .Add(clickSts8)
                        .Add(clickSts9)
                        .Add(clickSts10)
                        .Add(clickSts11)
                        .Add(clickSts12)
                        .Add(clickSts13)
                        .Add(clickSts14);

                    int doneCount = DataSetting.Default.DoneStepCount;
                    for (int i = 0; i < doneCount; i++)
                    {
                        StepStateMgr.Instance.FindBy(i).IsChecked();
                        StepStateMgr.Instance.FindBy(i).IsDone = true;
                    }

                }
                else
                {
                    int doneCount = DataSetting.Default.DoneStepCount;
                    for (int i = 0; i < doneCount; i++)
                    {
                        StepStateMgr.Instance.FindBy(i).IsChecked();
                    }
                }
            }
            
        }
        private void SVOForm_Load(object sender, EventArgs e)
        {
            //读取设置文件
            DataSetting.Load();
            //Machine.Instance.Robot.LoadCalibPrm();
            if(DataSetting.Default.VavelNo==1)
            {
                this.tabVavel2.Hide();
            }

            this.chkSoftLimitZ.BackColor = Color.CornflowerBlue;
            this.chkVavel2NeedleCamera.BackColor = Color.CornflowerBlue;
            this.toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
            this.toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
            this.toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
        }
        private void btnRunToEnd_Click(object sender, EventArgs e)
        {
            int lastStep = this.selectedStep - 1;

            if (lastStep >= 0)
            {
                if (DataSetting.Default.DoneStepCount<this.selectedStep)
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                    return;
                }
            }

            this.IsRunToEnd = true;

            DialogResult dr = DialogResult.OK;
            int allStep = 0;
            if (DataSetting.Default.VavelNo == 1)
            {
                allStep = 11;
            }
            else if (DataSetting.Default.VavelNo==2)
            {
                allStep = 15;
            }
            for (int i = selectedStep; i < allStep; i++)
            {
                switch (i)
                {
                    case 0:
                        dr = new TeachMinZ().ShowDialog();
                        break;
                    case 1:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(0).IsDone)
                        {
                            dr = new TeachSafeZ().ShowDialog();
                        }
                        break;
                    case 2:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(1).IsDone)
                        {
                            dr = new DialogCalibCamera().Setup().ShowDialog();
                        }
                        break;
                    case 3:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(2).IsDone)
                        {
                            dr = new TeachNeedleToCamera(1).ShowDialog();
                        }
                        break;
                    case 4:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(3).IsDone)
                        {
                            dr = new TeachLaserToCamera().ShowDialog();
                        }
                        break;
                    case 5:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(4).IsDone)
                        {
                            dr = new TeachNeedleToHeightSensor(1).ShowDialog();
                        }
                        break;
                    case 6:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(5).IsDone)
                        {
                            dr = new TeachPurge().ShowDialog();
                        }
                        break;
                    case 7:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(6).IsDone)
                        {
                            dr = new TeachPrime().ShowDialog();
                        }
                        break;
                    case 8:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(7).IsDone)
                        {
                            dr = new TeachScale().ShowDialog();
                        }
                        break;
                    case 9:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(8).IsDone)
                        {
                            dr = new CalculateTheNeedleXYOffset(1).ShowDialog();
                        }
                        break;
                    case 10:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(9).IsDone)
                        {
                            dr = new TeachScrape().ShowDialog();
                        }
                        break;
                    case 11:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(10).IsDone)
                        {
                            dr = new TeachNeedleToCamera(2).ShowDialog();
                        }
                        break;
                    case 12:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(11).IsDone)
                        {
                            dr = new TeachNeedleToHeightSensor(2).ShowDialog();
                        }
                        break;
                    case 13:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(12).IsDone)
                        {
                            dr = new CalculateTheNeedleXYOffset(2).ShowDialog();
                        }
                        break;
                    case 14:
                        if (dr == DialogResult.OK && StepStateMgr.Instance.FindBy(13).IsDone)
                        {
                            dr = new CalculateSlopeAmongVavel1AndVavel2().ShowDialog();
                        }
                        break;
                }
                Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = (Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight));
                toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
                Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X));
                Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y));
                toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
                toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
            }
        }
        private void btnSingleStep_Click(object sender, EventArgs e)
        {
            if (this.tabTask.SelectedTab == this.tabVavel1) 
            {
                int row = 0;
                for (int i = 0; i < this.tlpTaskVavel1.RowCount; i++)
                {
                    if (this.tlpTaskVavel1.GetControlFromPosition(0, i).BackColor == Color.CornflowerBlue)
                    {
                        row = i;
                    }
                }
                if (PrevTaskIsChecked((CheckBox)this.tlpTaskVavel1.GetControlFromPosition(0, row)))
                {
                    switch (row)
                    {
                        case 0:
                            new TeachMinZ().ShowDialog();
                            break;
                        case 1:
                            new TeachSafeZ().ShowDialog();
                            break;
                        case 2:
                            new DialogCalibCamera().Setup().ShowDialog();
                            break;
                        case 3:
                            new TeachNeedleToCamera(1).ShowDialog();
                            break;
                        case 4:
                            new TeachLaserToCamera().ShowDialog();
                            break;
                        case 5:
                            new TeachNeedleToHeightSensor(1).ShowDialog();
                            Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = (Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight));
                            toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
                            break;
                        case 6:
                            new TeachPurge().ShowDialog();
                            break;
                        case 7:
                            new TeachPrime().ShowDialog();
                            break;
                        case 8:
                            new TeachScale().ShowDialog();
                            break;
                        case 9:
                            new CalculateTheNeedleXYOffset(1).ShowDialog();
                            Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X));
                            Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y));
                            toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
                            toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
                            break;
                        case 10:
                            new TeachScrape().ShowDialog();
                            break;
                    }
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }
            }
            else if (this.tabTask.SelectedTab == this.tabVavel2)
            {
                int row = 0;
                for (int i = 0; i < this.tlpTaskVavel2.RowCount; i++)
                {
                    if (this.tlpTaskVavel2.GetControlFromPosition(0, i).BackColor == Color.CornflowerBlue)
                    {
                        row = i;
                    }
                }
                if (PrevTaskIsChecked((CheckBox)this.tlpTaskVavel2.GetControlFromPosition(0, row)))
                {
                    switch (row)
                    {
                        case 0:
                            new TeachNeedleToCamera(2).ShowDialog();
                            break;
                        case 1:
                            new TeachNeedleToHeightSensor(2).ShowDialog();
                            Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = (Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight));
                            toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
                            break;
                        case 2:
                            new CalculateTheNeedleXYOffset(2).ShowDialog();
                            Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X));
                            Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y));
                            toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
                            toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
                            break;
                        case 3:
                            new CalculateSlopeAmongVavel1AndVavel2().ShowDialog();
                            break;
                    }
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }
            }

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            clickSts0.IsDone = false; clickSts1.IsDone = false; clickSts2.IsDone = false;
            clickSts3.IsDone = false; clickSts4.IsDone = false; clickSts5.IsDone = false;
            clickSts6.IsDone = false; clickSts7.IsDone = false; clickSts8.IsDone = false;
            clickSts9.IsDone = false; clickSts10.IsDone = false; clickSts11.IsDone = false;
            clickSts12.IsDone = false; clickSts13.IsDone = false; clickSts14.IsDone = false;
            for (int i = 0; i < this.tlpTaskVavel1.RowCount; i++)
            {
                ((CheckBox)this.tlpTaskVavel1.GetControlFromPosition(0, i)).Checked = false;
            }
            for (int i = 0; i < this.tlpTaskVavel2.RowCount; i++)
            {
                ((CheckBox)this.tlpTaskVavel2.GetControlFromPosition(0, i)).Checked = false;
            }
            DataSetting.Default.DoneStepCount = 0;
            DataSetting.Save();

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            new OperateSetting().ShowDialog();
        }

        #region 单击变色
        private void Vavel1Task_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.tlpTaskVavel1.RowCount; i++)
            {
                this.tlpTaskVavel1.GetControlFromPosition(0, i).BackColor = Color.White;
            }

            this.selectedStep = this.tlpTaskVavel1.GetRow((CheckBox)sender);
            this.tlpTaskVavel1.GetControlFromPosition(0, selectedStep).BackColor = Color.CornflowerBlue;
        }
        private void Vavel2Task_CLick(object sender,EventArgs e)
        {
            for (int i = 0; i < this.tlpTaskVavel2.RowCount; i++)
            {
                this.tlpTaskVavel2.GetControlFromPosition(0, i).BackColor = Color.White;
            }

            this.selectedStep = this.tlpTaskVavel2.GetRow((CheckBox)sender)+10;
            int index = this.tlpTaskVavel2.GetRow((CheckBox)sender);
            this.tlpTaskVavel2.GetControlFromPosition(0, index).BackColor = Color.CornflowerBlue;
        }
        #endregion

        #region 单个任务的双击触发事件
        private void chkSoftLimitZ_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts0.UpdateClick(DateTime.Now);
            if (this.clickSts0.IsDoubleClicked)
            {
                new TeachMinZ().ShowDialog();
            }
        }

        private void chkCameraCalibration_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts1.UpdateClick(DateTime.Now);

            if (this.clickSts1.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new DialogCalibCamera().Setup().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkTeachSafeZ_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts2.UpdateClick(DateTime.Now);

            if (this.clickSts2.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachSafeZ().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }

        private void chkTeachNeedleToCamera_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts3.UpdateClick(DateTime.Now);

            if (this.clickSts3.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachNeedleToCamera(1).ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }

        private void chkTeachLaserToCamera_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts4.UpdateClick(DateTime.Now);

            if (this.clickSts4.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachLaserToCamera().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
            
        }
        private void chkTeachNeedleToHeight_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts5.UpdateClick(DateTime.Now);

            if (this.clickSts5.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachNeedleToHeightSensor(1).ShowDialog();
                    Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = (Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight));
                    toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }

        }

        private void chkTeachPurge_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts6.UpdateClick(DateTime.Now);

            if (this.clickSts6.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachPurge().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkTeachPrimer_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts7.UpdateClick(DateTime.Now);

            if (this.clickSts7.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachPrime().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkTeachScale_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts8.UpdateClick(DateTime.Now);

            if (this.clickSts8.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachScale().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkCalculateTheNeedleXYOffset_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts9.UpdateClick(DateTime.Now);

            if (this.clickSts9.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new CalculateTheNeedleXYOffset(1).ShowDialog();
                    Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X));
                    Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y));
                    toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
                    toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkTeachVavel1Scrape_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts10.UpdateClick(DateTime.Now);

            if (this.clickSts10.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachScrape().ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkVavel2NeedleCamera_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts11.UpdateClick(DateTime.Now);

            if (this.clickSts11.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachNeedleToCamera(2).ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkVavel2NeedleZOffset_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts12.UpdateClick(DateTime.Now);

            if (this.clickSts12.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new TeachNeedleToHeightSensor(2).ShowDialog();
                    Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = (Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight));
                    toxOffsetZ.Text = Machine.Instance.Robot.CalibPrm.ValveZOffset2to1.ToString();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }
        private void chkVave2lNeedleXYOffset_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts13.UpdateClick(DateTime.Now);

            if (this.clickSts13.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new CalculateTheNeedleXYOffset(2).ShowDialog();
                    Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X));
                    Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y = ((Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y) - (Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y));
                    toxOffsetX.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.X.ToString();
                    toxOffsetY.Text = Machine.Instance.Robot.CalibPrm.ValveXYOffset2to1.Y.ToString();
                }
                else
                {
                    //MessageBox.Show("Task must execute in order!");
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }

        private void chkVavel2AssociateToVavel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.clickSts14.UpdateClick(DateTime.Now);

            if (this.clickSts14.IsDoubleClicked)
            {
                if (PrevTaskIsChecked((CheckBox)sender))
                {
                    new CalculateSlopeAmongVavel1AndVavel2().ShowDialog();
                }
                else
                {
                    MessageBox.Show("任务必须按顺序执行!");
                }

            }
        }

        #endregion
        private bool PrevTaskIsChecked(CheckBox chb)
        {
            int prevRow = 0;
            bool isChecked = false;
            if (this.tlpTaskVavel1.Contains(chb))
            {
                prevRow = this.tlpTaskVavel1.GetRow(chb) - 1;
                if (prevRow == -1)
                {
                    return true;
                }
                isChecked = ((CheckBox)this.tlpTaskVavel1.GetControlFromPosition(0, prevRow)).Checked;
            }
            else
            {
                prevRow = this.tlpTaskVavel2.GetRow(chb) - 1;
                if (prevRow == -1)
                {
                    return true;
                }
                isChecked = ((CheckBox)this.tlpTaskVavel2.GetControlFromPosition(0, prevRow)).Checked;
            }
                  
            if (isChecked)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #region
        public void Task0Checked()
        {
            this.chkSoftLimitZ.Checked = true;
        }
        public void Task0UnChecked()
        {
            this.chkSoftLimitZ.Checked = false;
        }
        public void Task1Checked()
        {
            this.chkTeachSafeZ.Checked = true;
        }
        public void Task1UnChecked()
        {
            this.chkTeachSafeZ.Checked = false;
        }
        public void Task2Checked()
        {
            this.chkCameraCalibration.Checked = true;
            
        }
        public void Task2UnChecked()
        {
            this.chkCameraCalibration.Checked = false;
           
        }
        public void Task3Checked()
        {
            this.chkTeachNeedleToCamera.Checked = true;
        }

        public void Task3UnChecked()
        {
            this.chkTeachNeedleToCamera.Checked = false;
        }

        public void Task4Checked()
        {

            this.chkTeachLaserToCamera.Checked = true;
        }

        public void Task4UnChecked()
        {
            this.chkTeachLaserToCamera.Checked = false;
        }

        public void Task5Checked()
        {
            this.chkTeachNeedleToHeight.Checked = true;
        }

        public void Task5UnChecked()
        {
            this.chkTeachNeedleToHeight.Checked = false;

        }

        public void Task6Checked()
        {
            this.chkTeachPurge.Checked = true;
        }

        public void Task6UnChecked()
        {
            this.chkTeachPurge.Checked = false;
        }

        public void Task7Checked()
        {
            this.chkTeachPrimer.Checked = true;
        }

        public void Task7UnChecked()
        {
            this.chkTeachPrimer.Checked = false;
        }

        public void Task8Checked()
        {
            this.chkTeachScale.Checked = true;
        }

        public void Task8UnChecked()
        {
            this.chkTeachScale.Checked = false;
        }
        public void Task9Checked()
        {
            this.chkCalculateTheNeedleXYOffset.Checked = true;
        }

        public void Task9UnChecked()
        {
            this.chkCalculateTheNeedleXYOffset.Checked = false;
        }

        public void Task10Checked()
        {
            this.chkTeachVavel1Scrape.Checked = true;
        }

        public void Task10UnChecked()
        {
            this.chkTeachVavel1Scrape.Checked = true;
        }
        public void Vavel1CompleteChecked()
        {
            this.chkVavel1Complete.Checked = true;
        }
        public void Vavel1CompleteUnChecked()
        {
            this.chkVavel1Complete.Checked = false;
        }
        public void Task11Checked()
        {
            this.chkVavel2NeedleCamera.Checked = true;
        }

        public void Task11UnChecked()
        {
            this.chkVavel2NeedleCamera.Checked = false;
        }
        public void Task12Checked()
        {
            this.chkVavel2NeedleZOffset.Checked = true;
        }

        public void Task12UnChecked()
        {
            this.chkVavel2NeedleZOffset.Checked = false;
        }

        public void Task13Checked()
        {
            this.chkVavel2NeedleXYOffset.Checked = true;
        }

        public void Task13UnChecked()
        {
            this.chkVavel2NeedleXYOffset.Checked = false;
        }
        public void Task14Checked()
        {
            this.chkVavel2AssociateToVavel1.Checked = true;
        }

        public void Task14UnChecked()
        {
            this.chkVavel2AssociateToVavel1.Checked = false;
        }
        public void Vavel2CompleteChecked()
        {
            this.chkVavel2Complete.Checked = true;
        }
        public void Vavel2CompleteUnChecked()
        {
            this.chkVavel2Complete.Checked = false;
        }
        #endregion
        private void tabTask_Selected(object sender, TabControlEventArgs e)
        {
            if (this.tabTask.SelectedTab == tabVavel1)
            {
                this.selectedStep = 0;

            }
            else if (this.tabTask.SelectedTab == tabVavel2)
            {
                this.selectedStep = 11;
            }
        }

    }
}
