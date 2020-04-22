using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;
using System.Diagnostics;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    class TeachNeedleToCamera : TeachFormBase, IClickable
    {
        private int flag = 0;
        private ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
        private Label lblDiagram;
        private PictureBox picDiagram;
        private CameraControl cameraControl;
        public TeachNeedleToCamera()
            :base()
        {
            this.InitializeComponent();
            this.UpdateByFlag();
        }
        public void DoHelp()
        {
            throw new NotImplementedException();
        }

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            //移动到SafeZ

            //计算Needle到点胶阀的距离并赋值
            //DataSetting.Default.NeedleCameraOffset = new DataPoint(
            //    Math.Abs(DataSetting.Default.NeedleMark.X - DataSetting.Default.CameraNeedleMark.X),
            //    Math.Abs(DataSetting.Default.NeedleMark.Y - DataSetting.Default.CameraNeedleMark.Y));
            //DataSetting.Default.Save();

            StepStateMgr.Instance.FindBy(1).IsDone = true;
            this.checkedChange.Task2Checked();
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
            flag--;
            UpdateByFlag();
        }

        public void DoTeach()
        {
            flag++;
            UpdateByFlag();
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
                    this.picDiagram.Image = Properties.Resources.TeachNeedleToCamera;// FileToImage(@"TeachNeedleToCamera.PNG");
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();                  
                    this.picDiagram.Show();
                    break;
                case 1:

                    Task.Factory.StartNew(new Action(() =>
                    {
                        //抬起到SafeZ
                        //          = DataSetting.Default.SafeZ;                 

                        //移动到设置文件中Needle对应的Mark Point
                        //          = DataSetting.Default.NeedleMark
                    }));
                    this.lblTitle.Text = "Align needle on mark and press [Teach]";
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.pnlDisplay.Controls.Clear();
                    this.picDiagram.Image = Properties.Resources.TeachNeedleToCamera;// FileToImage(@"TeachNeedleToCamera.PNG");
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 2:
                    //Needle到Mark的坐标点进行赋值
                    //DataSetting.Default.NeedleMark =new DataPoint(21,21) ;

                    Task.Factory.StartNew(new Action(() =>
                    {
                        //抬起到SafeZ
                        //            = DataSetting.Default.SafeZ;     

                        //移动到设置文件中Camera对应的Needle Mark Point
                        //            = DataSetting.Default.CameraNeedleMark;
                    }));                  

                    //加载相机控件                    
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    //
                    this.lblTitle.Text = "Align camera on needle mark and press [Teach]";
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.btnPrev.Enabled = true;
                    
                    break;
                case 3:
                    //Camera到NeedleMark的坐标点进行赋值
                    //DataSetting.Default.CameraNeedleMark = new DataPoint(22, 22);
                    //
                    this.lblTitle.Text = "Teach compelet,Press [Done];Want again,Press [Prev]";
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnPrev.Enabled = true;
                    break;
            }
        }     

        private void TeachNeedleToCamera_FormClosing(object sender, FormClosingEventArgs e)
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
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.lblDiagram);
            this.pnlDisplay.Location = new System.Drawing.Point(6, 10);
            this.pnlDisplay.Size = new System.Drawing.Size(444, 397);
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(177, 26);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(254, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 0;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(12, 127);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(149, 120);
            this.lblDiagram.TabIndex = 24;
            this.lblDiagram.Text = "Teach the distance\r\nof Needle with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:D" +
    "ispenser will\r\nmove after your response";
            //
            //cameraControl
            //
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.Location = new Point(0, 0);
            // 
            // TeachNeedleToCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(664, 690);
            this.Name = "TeachNeedleToCamera";
            this.Text = "Anda Fluidmove-Teach needle to camera XY offset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachNeedleToCamera_FormClosing);
            this.grpOperation.ResumeLayout(false);
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
