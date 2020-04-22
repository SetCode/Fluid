using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    class TeachLaserToCamera : TeachFormBase, IClickable
    {
        private ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
        private int flag = 0;
        private Label lblDiagram;
        private PictureBox picDiagram;
        private CameraControl cameraControl;
        public TeachLaserToCamera()
        {
            this.InitializeComponent();
            this.UpdateByFlag();
        }
        public void DoHelp()
        { }
        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            //计算Laser到点胶阀的距离并赋值
            //DataSetting.Default.LaserCameraOffset = new DataPoint(
            //    Math.Abs(DataSetting.Default.LaserMark.X - DataSetting.Default.CameraLaserMark.X),
            //    Math.Abs(DataSetting.Default.LaserMark.Y - DataSetting.Default.CameraLaserMark.Y));
            //DataSetting.Default.Save();

            StepStateMgr.Instance.FindBy(2).IsDone = true;
            this.checkedChange.Task3Checked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoNext()
        {
            flag++;
            UpdateByFlag();
            
        }

        public void DoPrev()
        {
            this.flag--;
            this.UpdateByFlag();
        }

        public void DoTeach()
        {
            this.flag++;
            this.UpdateByFlag();
        }
   
        private void UpdateByFlag()
        {
            switch (flag)
            {
                case 0:
                    this.lblTitle.Text = "The Machine will move, press [Next] to continue.";
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.picDiagram.Image = Properties.Resources.TeachLaserToCamera; //this.FileToImage(@"TeachLaserToCamera.PNG");
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 1:
                    Task.Factory.StartNew(new Action(() =>
                    {
                        //抬起到SafeZ
                        //   =DataSetting.Default.SafeZ    

                        //移动到设置文件中Laser对应的Mark Point
                        //     =DataSetting.Default.LaserMark
                        //
                    }));                    
                    this.lblTitle.Text = "Align laser on mark and press [Teach]";
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.pnlDisplay.Controls.Clear();
                    this.picDiagram.Image = Properties.Resources.TeachLaserToCamera;// this.FileToImage(@"TeachLaserToCamera.PNG");
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 2:
                    //Laser到Mark的坐标点进行赋值
                    //DataSetting.Default.NeedleMark = new DataPoint(31,31) ;

                    Task.Factory.StartNew(new Action(() =>
                    {
                        //抬起到SafeZ
                        //            = DataSetting.Default.SafeZ;                 

                        //移动到设置文件中camera对应的Laser Mark Point
                        //            = DataSetting.Default.CameraLaserMark;
                    }));                    

                    //加载相机控件                    
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    //

                    this.lblTitle.Text = "Align camera on laser mark and press [Teach]";
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.btnPrev.Enabled = true;
                    break;
                case 3:
                    //Camera到NeedleMark的坐标点进行赋值
                    //DataSetting.Default.CameraNeedleMark = new DataPoint(32, 32);
                    //
                    this.lblTitle.Text = "Teach compelet,Press [Done];Want again,Press [Prev]";
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnPrev.Enabled = true;
                    break;
            }
        }

        private void TeachLaserToCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            //抬起到SafeZ
            //            = DataSetting.Default.SafeZ;
            SVOForm.Instance.IsRunToEnd = false;
        }
        private void InitializeComponent()
        {
            this.cameraControl = new CameraControl();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.grpOperation.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            //
            //cameraControl
            //
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.Location = new Point(0, 0);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.lblDiagram);
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(177, 26);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new Size(254, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 0;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(15, 133);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(149, 120);
            this.lblDiagram.TabIndex = 25;
            this.lblDiagram.Text = "Teach the distance\r\nof Laser with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:Di" +
    "spenser will\r\nmove after your response";
            // 
            // TeachLaserToCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(664, 690);
            this.Name = "TeachLaserToCamera";
            this.Text = "Anda Fluidmove-Teach laser to camera XY offset ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachLaserToCamera_FormClosing);
            this.grpOperation.ResumeLayout(false);
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
