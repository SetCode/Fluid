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
    class TeachPurge:TeachFormBase,IClickable
    {
        private ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
        private int flag = 0;       

        #region 新添加的控件
        private System.Windows.Forms.PictureBox picDiagram;
        private FindCircle findCircle;
        private System.Windows.Forms.TextBox txtCenterZ;
        private System.Windows.Forms.TextBox txtCenterY;
        private System.Windows.Forms.TextBox txtCenterX;
        private System.Windows.Forms.Label lblCenterLocation;
        private System.Windows.Forms.Label lblDiagram;
        private CameraControl cameraControl;
        #endregion
        public TeachPurge()
        {
            InitializeComponent();
            this.picDiagram.Image = Properties.Resources.TeachPurge;// this.FileToImage(@"TeachPurge.PNG");
            this.UpdateByFlag();
        }
        public void DoHelp()
        {
            throw new NotImplementedException();
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
                flag -= 5;
                UpdateByFlag();
            }
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
                flag += 5;
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
            else if (this.findCircle.rdoFindCircleThreePoint.Checked)
            {
                flag += 5;
                UpdateByFlag();
            }
        }

        public void DoDone()
        {

            //抬起到SafeZ

            //
            //DataSetting.Default.Save();

            StepStateMgr.Instance.FindBy(4).IsDone = true;
            this.checkedChange.Task5Checked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoCancel()
        {            
            this.Close();
        }
        private void TeachPurge_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //抬起到SafeZ
            //            = DataSetting.Default.SafeZ;
            SVOForm.Instance.IsRunToEnd = false;
        }

        private void TeachPurge_Load(object sender, EventArgs e)
        {
            //this.txtCenterX.Text = DataSetting.Default.PurgeLocation.X.ToString();
            //this.txtCenterY.Text = DataSetting.Default.PurgeLocation.Y.ToString();
            //this.txtCenterZ.Text = (DataSetting.Default.PurgeZByHs - DataSetting.Default.HeightOffsetZ).ToString();
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
                    //移动到单点位置默认点
                    //  =DataSetting.Default.PurgeCenter
                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target centre of Purge boot using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target centre of Purge boot and press [Teach].";

                    break;
                case 2:
                    //保存单点位置
                    //   DataSetting.Default.PurgeCenter=
                    this.ThirdLastStep();
                    break;
                case 3:
                    this.SecondLastStep();
                    break;
                case 4:
                    this.LastStep();
                    break;

                //三点求圆心分支
                case 5:
                    //移动到点P1
                    //    = DataSetting.Default.PurgeCircumferenceP1;
                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target circumference point 1 on Purge using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target purge circumference point 1 and press [Teach].";
                    break;
                case 10:
                    //保存点P1
                    //   DataSetting.Default.PurgeCircumferenceP1=;
                    //
                    //移动到点P2
                    //   = DataSetting.Default.PurgeCircumferenceP2;
                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 2 on Purge using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target purge circumference point 2 and press [Teach].";
                    break;
                case 15:
                    //保存点P2
                    //   DataSetting.Default.PurgeCircumferenceP2=;
                    //
                    //移动到点P3
                    //   = DataSetting.Default.PurgeCircumferenceP3;
                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 3 on Purge using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target purge circumference point 3 and press [Teach].";
                    break;
                case 20:
                    //保存点P3
                    //   DataSetting.Default.PurgeCircumferenceP3=;
                    //
                    this.ThirdLastStep();
                    break;
                case 25:
                    this.SecondLastStep();
                    break;
                case 30:
                    this.LastStep();
                    break;

            }
        }
        /// <summary>
        /// 用相机找测高位置
        /// </summary>
        private void ThirdLastStep()
        {
            this.findCircle.lblMessage.Text = "Height sense will be performed at location viewed by camera.\rAdjust height sense location if necessary.\rPress [Teach] to continue.";
            this.lblTitle.Text = "Target height sense location on purge cup lid.";

            //移动到测高位置
            //   = DataSetting.Default.PurgeHs
            
        }
        /// <summary>
        /// 确认可以进行运动
        /// </summary>
        private void SecondLastStep()
        {
            //获取激光返回的高度值
            //   = DataSetting.Default.PurgeZByHs;
            //保存测高位置
            //   DataSetting.Default.PurgeHs=
            this.findCircle.lblMessage.Text = "This machine will move it's parts. \rKeep safe distance and press [Next] to continue.";

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = "Prepare for machine's parts move and press [Next]";
        }
        /// <summary>
        /// 进行寻找清洁中心测试
        /// </summary>
        private void LastStep()
        {
            //运动前
            this.findCircle.lblMessage.Text = "Looking for the Purge center...";
            this.lblTitle.Text = "Be looking for...";
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;

            //开始运动后
            Task.Factory.StartNew(() =>
            {
                //运动到purge中心并下降

                Thread.Sleep(1000);
                //

                //运动结束
                this.BeginInvoke(new Action(() =>
                {
                    this.findCircle.lblMessage.Text = "Move has ended.\rCan observe the needle is or not in the center location of purge."+
                         "\rPress [Done] if is center location.\rIf is not,maybe teach needle to camera again if necesseay.";
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.lblTitle.Text = "Move has ended,press[Done].";
                }));
            });
        }
        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.lblCenterLocation = new System.Windows.Forms.Label();
            this.txtCenterX = new System.Windows.Forms.TextBox();
            this.txtCenterY = new System.Windows.Forms.TextBox();
            this.txtCenterZ = new System.Windows.Forms.TextBox();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.txtCenterZ);
            this.grpResultTest.Controls.Add(this.txtCenterY);
            this.grpResultTest.Controls.Add(this.txtCenterX);
            this.grpResultTest.Controls.Add(this.lblCenterLocation);
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
            this.cameraControl.Dock = DockStyle.Fill;
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(172, 34);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(269, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 3;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(11, 34);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(155, 216);
            this.lblDiagram.TabIndex = 2;
            this.lblDiagram.Text = "Teach Purge location\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n           " +
    "or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove af" +
    "ter your response";
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(4, 10);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(450, 90);
            this.findCircle.TabIndex = 0;
            // 
            // lblCenterLocation
            // 
            this.lblCenterLocation.AutoSize = true;
            this.lblCenterLocation.Location = new System.Drawing.Point(32, 139);
            this.lblCenterLocation.Name = "lblCenterLocation";
            this.lblCenterLocation.Size = new System.Drawing.Size(113, 12);
            this.lblCenterLocation.TabIndex = 1;
            this.lblCenterLocation.Text = "Center Location : ";
            // 
            // txtCenterX
            // 
            this.txtCenterX.Enabled = false;
            this.txtCenterX.Location = new System.Drawing.Point(151, 136);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(72, 21);
            this.txtCenterX.TabIndex = 2;
            // 
            // txtCenterY
            // 
            this.txtCenterY.Enabled = false;
            this.txtCenterY.Location = new System.Drawing.Point(244, 136);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(72, 21);
            this.txtCenterY.TabIndex = 3;
            // 
            // txtCenterZ
            // 
            this.txtCenterZ.Enabled = false;
            this.txtCenterZ.Location = new System.Drawing.Point(337, 136);
            this.txtCenterZ.Name = "txtCenterZ";
            this.txtCenterZ.Size = new System.Drawing.Size(72, 21);
            this.txtCenterZ.TabIndex = 4;
            // 
            // TeachPurge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(671, 690);
            this.Name = "TeachPurge";
            this.Text = "Anda Fluidmove - Teach location for Valve Purge location";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachPurge_FormClosing);
            this.Load += new System.EventHandler(this.TeachPurge_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
    }
}
