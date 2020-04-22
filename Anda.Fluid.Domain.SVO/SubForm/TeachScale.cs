using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Domain.Vision;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    class TeachScale:TeachFormBase,IClickable
    {
        private ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
        private int flag = 0;

        #region 新添加的控件字段
        private CameraControl cameraControl;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.TextBox txtCenterZ;
        private System.Windows.Forms.TextBox txtCenterY;
        private System.Windows.Forms.TextBox txtCenterX;
        private System.Windows.Forms.Label lblCenterLocation;
        private FindCircle findCircle;
        private System.Windows.Forms.Label lblDiagram;
        #endregion
        public TeachScale()
        {
            InitializeComponent();
            this.picDiagram.Image = Properties.Resources.TeachScale;// this.FileToImage(@"TeachScale.PNG");
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

            this.checkedChange.Task6Checked();
            this.checkedChange.CompleteChecked();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public void DoCancel()
        {
            
            this.Close();
        }
        private void TeachScale_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //抬起到SafeZ

            //

            SVOForm.Instance.IsRunToEnd = false;
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

                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target centre of Scale system plate using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target centre of scale and press [Teach].";

                    break;
                case 2:
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
                    //移动到默认点P1

                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    this.findCircle.grpSwitch.Text = "Instructions";
                    this.findCircle.rdoFindCircleOnePoint.Hide();
                    this.findCircle.rdoFindCircleThreePoint.Hide();
                    this.findCircle.lblMessage.Text = "Target circumference point 1 on Scale using camera. \rPress[Teach] to continue.";
                    this.findCircle.lblMessage.Show();

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = "Target scale circumference point 1 and press [Teach].";
                    break;
                case 10:
                    //移动到默认点P2

                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 2 on Scale using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target scale circumference point 2 and press [Teach].";
                    break;
                case 15:
                    //移动到默认点P3

                    //
                    this.findCircle.lblMessage.Text = "Target circumference point 3 on Scale using camera. \rPress[Teach] to continue.";
                    this.lblTitle.Text = "Target scale circumference point 3 and press [Teach].";
                    break;
                case 20:
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
            this.lblTitle.Text = "Target height sense location on scale system plate.";
        }
        /// <summary>
        /// 确认可以进行运动
        /// </summary>
        private void SecondLastStep()
        {
            this.findCircle.lblMessage.Text = "This machine will move it's parts. \rKeep safe distance and press [Next] to continue.";

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = "Prepare for machine's parts move and press [Next]";
        }
        /// <summary>
        /// 进行寻找称重中心测试
        /// </summary>
        private void LastStep()
        {
            //运动前
            this.findCircle.lblMessage.Text = "Looking for the Scale center...";
            this.lblTitle.Text = "Be looking for...";
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;

            //开始运动后
            Task.Factory.StartNew(() =>
            {
                //运动到purge中心并下降
                Thread.Sleep(3000);
                //

                //运动结束
                this.BeginInvoke(new Action(() =>
                {
                    this.findCircle.lblMessage.Text = "Move has ended.\rCan observe the needle is or not in the center location of scale." +
                         "\rPress [Done] if is center location.\rIf is not,maybe teach again if necesseay.";
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
            this.cameraControl = new CameraControl();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.txtCenterZ = new System.Windows.Forms.TextBox();
            this.txtCenterY = new System.Windows.Forms.TextBox();
            this.txtCenterX = new System.Windows.Forms.TextBox();
            this.lblCenterLocation = new System.Windows.Forms.Label();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraControl
            // 
            this.cameraControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
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
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(172, 34);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(269, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 5;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(11, 34);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(155, 216);
            this.lblDiagram.TabIndex = 4;
            this.lblDiagram.Text = "Teach Scale location\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n           " +
    "or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove af" +
    "ter your response";
            // 
            // txtCenterZ
            // 
            this.txtCenterZ.Enabled = false;
            this.txtCenterZ.Location = new System.Drawing.Point(337, 154);
            this.txtCenterZ.Name = "txtCenterZ";
            this.txtCenterZ.Size = new System.Drawing.Size(72, 21);
            this.txtCenterZ.TabIndex = 9;
            // 
            // txtCenterY
            // 
            this.txtCenterY.Enabled = false;
            this.txtCenterY.Location = new System.Drawing.Point(244, 154);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(72, 21);
            this.txtCenterY.TabIndex = 8;
            // 
            // txtCenterX
            // 
            this.txtCenterX.Enabled = false;
            this.txtCenterX.Location = new System.Drawing.Point(151, 154);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(72, 21);
            this.txtCenterX.TabIndex = 7;
            // 
            // lblCenterLocation
            // 
            this.lblCenterLocation.AutoSize = true;
            this.lblCenterLocation.Location = new System.Drawing.Point(32, 157);
            this.lblCenterLocation.Name = "lblCenterLocation";
            this.lblCenterLocation.Size = new System.Drawing.Size(113, 12);
            this.lblCenterLocation.TabIndex = 6;
            this.lblCenterLocation.Text = "Center Location : ";
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(4, 28);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(450, 90);
            this.findCircle.TabIndex = 5;
            // 
            // TeachScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(664, 690);
            this.Name = "TeachScale";
            this.Text = "Anda Fluidmove - Teach location for Valve Scale location";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachScale_FormClosing);
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
