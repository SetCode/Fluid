using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;
using System.Drawing;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    class TeachNeedleToHeightSensor : TeachFormBase, IClickable
    {
        private ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
        private int flag = 0;

        #region 
        private CameraControl cameraControl;
        private FindCircle findCircle;
        private System.Windows.Forms.Label lblCycles;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Label labelZOffsetResult;
        private System.Windows.Forms.TextBox txtZOffsetResult;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.Label lblDiagram;
        private Button btnStopMotion;
        private NumericUpDown nudCycles;
        private System.Windows.Forms.TextBox txtTolerance;
        #endregion 

        public TeachNeedleToHeightSensor()
        {
            InitializeComponent();
            this.picDiagram.Image = Properties.Resources.TeachNeedleToHeightSensor;// this.FileToImage(@"TeachNeedleToHeightSensor.PNG");
            UpdateByFlag();
        }
        public void DoCancel()
        {            
           
            this.Close();
        }

        public void DoDone()
        {
            //抬起到SafeZ

            //
            //
            //DataSetting.Default.Save();

            StepStateMgr.Instance.FindBy(3).IsDone = true;
            this.checkedChange.Task4Checked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoNext()
        {
            if (this.findCircle.rdoFindCircleOnePoint.Checked)
            {
                flag++;
                UpdateByFlag();
            }
            else if (this.findCircle.rdoFindCircleThreePoint.Checked)
            {
                flag += 4;
                UpdateByFlag();
            }
        }

        public void DoPrev()
        {
            if (this.findCircle.rdoFindCircleOnePoint.Checked)
            {
                flag--;
                UpdateByFlag();
            }
            else if (this.findCircle.rdoFindCircleThreePoint.Checked)
            {
                flag -= 4;
                UpdateByFlag();
            }
        }

        public void DoTeach()
        {
            if (this.findCircle.rdoFindCircleOnePoint.Checked)
            {
                flag++;
                UpdateByFlag();
            }
            else if(this.findCircle.rdoFindCircleThreePoint.Checked)
            {
                flag += 4;
                UpdateByFlag();
            }
        }
        private void btnStopMotion_Click(object sender, EventArgs e)
        {

        }
        
        private void UpdateByFlag()
        {
            switch (flag)
            {
                case 0:
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.picDiagram.Show();
                    this.lblDiagram.Show();
                    this.findCircle.grpSwitch.Text = "Teach Center Method";
                    this.findCircle.rdoFindCircleOnePoint.Show();
                    this.findCircle.rdoFindCircleThreePoint.Show();
                    this.findCircle.lblMessage.Hide();

                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Select teach method and press [Next].";
                    break;

                    //单点找圆心分支
                case 1:
                    //移动到默认点
                    //         = DataSetting.Default.HeightSensorCenter;
                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target centre point on tactile using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target tactile centre point and press [Teach].";

                    break;
                case 2:
                    //保存相机位置
                    //         DataSetting.Default.HeightSensorCenter=;
                    //
                    //对中心点进行赋值
                    //    DataSetting.Default.HeightCenter = DataSetting.Default.HeightSensorCenter;

                    SecondLastInMeasurement();
                    break;
                case 3:
                    LastStepInMeasurement();                   
                    break;

                    //三点求圆心分支
                case 4:
                    //移动到默认点p1
                    //         = DataSetting.Default.HeightCircumferenceP1;
                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target circumference point 1 on tactile using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target tactile circumference point 1 and press [Teach].";
                    break;
                case 8:
                    //保存点P1
                    //         DataSetting.Default.HeightCircumferenceP1=;
                    //
                    //移动到默认点p2
                    //         = DataSetting.Default.HeightCircumferenceP2;
                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 2 on tactile using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target tactile circumference point 2 and press [Teach].";
                    break;
                case 12:
                    //保存点P2
                    //         DataSetting.Default.HeightCircumferenceP2=;
                    //
                    //移动到默认点p3
                    //         = DataSetting.Default.HeightCircumferenceP3;
                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 3 on tactile using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target tactile circumference point 3 and press [Teach].";
                    break;
                case 16:
                    //保存点P3
                    //         DataSetting.Default.HeightCircumferenceP3=;
                    //
                    //通过三点求得圆心
                    //DataSetting.Default.HeightCenter = MathTools.CalculateCircleCenter(DataSetting.Default.HeightCircumferenceP1,
                    //    DataSetting.Default.HeightCircumferenceP2, DataSetting.Default.HeightCircumferenceP3);
                    SecondLastInMeasurement();
                    break;
                case 20:
                    LastStepInMeasurement();
                    break;
            }

        }
        /// <summary>
        /// 确认可以进行运动
        /// </summary>
        private void SecondLastInMeasurement()
        {
            this.findCircle.lblMessage.Text = "This machine will move it's parts. \rKeep safe distance and press [Next] to continue.";

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = "Prepare for machine's parts move and press [Next]";
        }
        /// <summary>
        /// 进行Z轴测量
        /// </summary>
        private void LastStepInMeasurement()
        {
            //测量中
            this.findCircle.lblMessage.Text = "Measuring the Z height...";
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnStopMotion.Enabled = true;
            this.btnStopMotion.BackColor = Color.DarkOrange;

            Task.Factory.StartNew(() =>
            {
                BeginZAxisMeasurement();
                this.BeginInvoke(new Action(() =>
                {
                    //测量结束
                    this.findCircle.lblMessage.Text = "Measurement has ended...";
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnCancel.Enabled = true;
                    this.lblTitle.Text = "Measurement has ended,press[Done].";
                }));
            });
        }
        private void BeginZAxisMeasurement()
        {
            int cycles = (int)this.nudCycles.Value;
            double totalOffset = 0;
            double totalZ = 0;
            double sumTwiceZ = 0;
            bool isok;
            //抬起到SafeZ
            //            = DataSetting.Default.SafeZ;
            for (int i = 0; i < cycles; i++)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.lblTitle.Text = String.Format("Running cycle {0}...", i + 1);
                }));

                //抬起到SafeZ
                //            = DataSetting.Default.SafeZ;

                //将激光移动到圆盘中心
                //       = DataSetting.Default.HeightCenter;
                //

                //判断激光能否使用
                isok = true;
                if(!isok)
                {
                    //发出相关信息报警，并且只能选择退出当前Teach窗体
                    return;
                }
                //

                //记录激光测高数据
                //    totalZ+=
                //

                //将阀移动至圆盘中心(相机到阀的偏差以及Height中心位置共同求得，还不知道该＋或者-)
                  
                //

                //反复下压两次
                for (int j = 0; j < 2; j++)
                {
                    //
                    //sumTwiceZ+=
                }

                Thread.Sleep(2000);
            }
            //结果计算
            //totalOffset=totalZ+-sumTwiceZ/2
            //DataSetting.Default.StandardHeight = totalZ / cycles;
            //DataSetting.Default.StandardZ = sumTwiceZ / (cycles + 2);
            //DataSetting.Default.HeightOffsetZ= DataSetting.Default.StandardHeight+-DataSetting.Default.StandardZ

            //this.txtZOffsetResult.Text = DataSetting.Default.HeightOffsetZ.ToString();
        }

        public void DoHelp()
        {
            //throw new NotImplementedException();
        }

        private void TeachNeedleToHeightSensor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //抬起到SafeZ
            //            = DataSetting.Default.SafeZ;
            SVOForm.Instance.IsRunToEnd = false;
        }

        private void TeachNeedleToHeightSensor_Load(object sender, EventArgs e)
        {
            //this.txtZOffsetResult.Text = DataSetting.Default.HeightOffsetZ.ToString();
        }
        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.lblCycles = new System.Windows.Forms.Label();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.labelZOffsetResult = new System.Windows.Forms.Label();
            this.txtZOffsetResult = new System.Windows.Forms.TextBox();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.btnStopMotion = new System.Windows.Forms.Button();
            this.nudCycles = new System.Windows.Forms.NumericUpDown();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycles)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.nudCycles);
            this.grpResultTest.Controls.Add(this.btnStopMotion);
            this.grpResultTest.Controls.Add(this.lblCycles);
            this.grpResultTest.Controls.Add(this.lblTolerance);
            this.grpResultTest.Controls.Add(this.labelZOffsetResult);
            this.grpResultTest.Controls.Add(this.txtZOffsetResult);
            this.grpResultTest.Controls.Add(this.txtTolerance);
            this.grpResultTest.Controls.Add(this.findCircle);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.picDiagram);
            this.pnlDisplay.Controls.Add(this.lblDiagram);
            // 
            // cameraControl
            // 
            this.cameraControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(4, 10);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(448, 90);
            this.findCircle.TabIndex = 0;
            // 
            // lblCycles
            // 
            this.lblCycles.AutoSize = true;
            this.lblCycles.Location = new System.Drawing.Point(88, 127);
            this.lblCycles.Name = "lblCycles";
            this.lblCycles.Size = new System.Drawing.Size(119, 12);
            this.lblCycles.TabIndex = 11;
            this.lblCycles.Text = "Measurement cycles:";
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(76, 153);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(131, 12);
            this.lblTolerance.TabIndex = 12;
            this.lblTolerance.Text = "Offset tolerance +/-:";
            // 
            // labelZOffsetResult
            // 
            this.labelZOffsetResult.AutoSize = true;
            this.labelZOffsetResult.Location = new System.Drawing.Point(56, 182);
            this.labelZOffsetResult.Name = "labelZOffsetResult";
            this.labelZOffsetResult.Size = new System.Drawing.Size(155, 12);
            this.labelZOffsetResult.TabIndex = 13;
            this.labelZOffsetResult.Text = "Needle to probe Z offset:";
            // 
            // txtZOffsetResult
            // 
            this.txtZOffsetResult.Location = new System.Drawing.Point(213, 177);
            this.txtZOffsetResult.Name = "txtZOffsetResult";
            this.txtZOffsetResult.Size = new System.Drawing.Size(72, 21);
            this.txtZOffsetResult.TabIndex = 16;
            // 
            // txtTolerance
            // 
            this.txtTolerance.Enabled = false;
            this.txtTolerance.Location = new System.Drawing.Point(213, 150);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(72, 21);
            this.txtTolerance.TabIndex = 15;
            this.txtTolerance.Text = "0.02";
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(16, 26);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(155, 216);
            this.lblDiagram.TabIndex = 0;
            this.lblDiagram.Text = "Teach center of tactile\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n        " +
    "   or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove" +
    " after your response";
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(177, 26);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(254, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 1;
            this.picDiagram.TabStop = false;
            // 
            // btnStopMotion
            // 
            this.btnStopMotion.BackColor = System.Drawing.SystemColors.Control;
            this.btnStopMotion.Enabled = false;
            this.btnStopMotion.Location = new System.Drawing.Point(330, 150);
            this.btnStopMotion.Name = "btnStopMotion";
            this.btnStopMotion.Size = new System.Drawing.Size(75, 23);
            this.btnStopMotion.TabIndex = 17;
            this.btnStopMotion.Text = "Stop";
            this.btnStopMotion.UseVisualStyleBackColor = false;
            this.btnStopMotion.Click += new System.EventHandler(this.btnStopMotion_Click);
            // 
            // nudCycles
            // 
            this.nudCycles.Location = new System.Drawing.Point(214, 121);
            this.nudCycles.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCycles.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudCycles.Name = "nudCycles";
            this.nudCycles.Size = new System.Drawing.Size(71, 21);
            this.nudCycles.TabIndex = 18;
            this.nudCycles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // TeachNeedleToHeightSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(671, 690);
            this.Name = "TeachNeedleToHeightSensor";
            this.Text = "Anda Fluid move - Teach Needle to Height Sensor Z Offset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachNeedleToHeightSensor_FormClosing);
            this.Load += new System.EventHandler(this.TeachNeedleToHeightSensor_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
