using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal class TeachLaserToCamera : TeachFormBase, IClickable
    {
        private int flag = 0;
        private Label lblDiagram;
        private PictureBox picDiagram;
        private CameraControl cameraControl;

        #region 语言切换字符串变量

        private string[] lblTip = new string[4]
        {
            "The Machine will move, press [Next] to continue.",
            "Align laser on mark and press [Teach]",
            "Align camera on laser mark and press [Teach]",
            "Teach the distance\r\nof Laser with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:Di" + "spenser will\r\nmove after your response"
        };

        #endregion
        public TeachLaserToCamera()
        {
            this.InitializeComponent();
            this.UpdateByFlag();
            this.ReadLanguageResources();
            this.lblDiagram.Text = this.lblTip[3];
            this.FormClosed += TeachLaserToCamera_FormClosed;
        }

        private void TeachLaserToCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.picDiagram.Image.Dispose();
            this.Dispose(true);
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            for (int i = 0; i < lblTip.Length; i++)
            {
                this.SaveKeyValueToResources(string.Format("Tip{0}", i), lblTip[i]);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            for (int i = 0; i < lblTip.Length; i++)
            {
                string temp = "";
                temp = this.ReadKeyValueFromResources(string.Format("Tip{0}", i));
                //空值不读取
                if (temp != "")
                {
                    lblTip[i] = temp;
                }
            }
        }

        public void DoHelp()
        { }
        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            this.AllDone();
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
            if (flag == 3)
            {
                this.AllDone();
            }
            else
            {
                this.UpdateByFlag();
            }

        }

        private void UpdateByFlag()
        {
            switch (flag)
            {
                case 0:
                    this.lblTitle.Text = this.lblTip[0];
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    if (Machine.Instance.Laser.Laserable.Vendor == Laser.Vendor.Disable)
                    {
                        this.btnDone.Enabled = true;
                    }
                    else
                    {
                        this.btnDone.Enabled = false;
                    }
                    this.btnTeach.Enabled = false;
                    this.picDiagram.Image = Properties.Resources.TeachLaserToCamera1;
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 1:
                    //关闭光源
                    Machine.Instance.Light.None();
                    //

                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZ();
                    //移动到设置文件中Laser对应的Mark Point
                    //Machine.Instance.Robot.MovePosXY(DataSetting.Default.LaserMark);

                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.LaserMark.X, DataSetting.Default.LaserMark.Y);

                    this.lblTitle.Text = this.lblTip[1];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.pnlDisplay.Controls.Clear();
                    this.picDiagram.Image = Properties.Resources.TeachLaserToCamera1;
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 2:
                    //打开光源
                    Machine.Instance.Light.ResetToLast();
                    //

                    //Laser到Mark的坐标点进行赋值
                    DataSetting.Default.LaserMark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZ();
                    //移动到设置文件中camera对应的Laser Mark Point
                    //Machine.Instance.Robot.MovePosXY(DataSetting.Default.CameraLaserMark);                    
                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.CameraLaserMark.X, DataSetting.Default.CameraLaserMark.Y);

                    //加载相机控件                    
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    //

                    this.lblTitle.Text = this.lblTip[2];
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.btnPrev.Enabled = true;
                    break;
            }
        }

        private void TeachLaserToCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            SVOForm.Instance.IsRunToEnd = false;
        }
        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
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
            // 
            // cameraControl
            // 
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(179, 26);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(290, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 0;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(19, 133);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(149, 120);
            this.lblDiagram.TabIndex = 25;
            this.lblDiagram.Text = "Teach the distance\r\nof Laser with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:Di" +
    "spenser will\r\nmove after your response";
            // 
            // TeachLaserToCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(723, 657);
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

        private void AllDone()
        {
            //Camera到NeedleMark的坐标点进行赋值
            DataSetting.Default.CameraLaserMark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);

            //计算Laser到点胶阀的距离并赋值
            Machine.Instance.Robot.CalibPrm.HeightCamera = new PointD(
                (DataSetting.Default.LaserMark.X - DataSetting.Default.CameraLaserMark.X),
                (DataSetting.Default.LaserMark.Y - DataSetting.Default.CameraLaserMark.Y));

            //保存
            if (DataSetting.Default.DoneStepCount <= 5)
            {
                DataSetting.Default.DoneStepCount = 5;
            }
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.CalibPrm.SavedItem = 5;
            Machine.Instance.Robot.SaveCalibPrm();
            DataSetting.Save();

            StepStateMgr.Instance.FindBy(4).IsDone = true;
            StepStateMgr.Instance.FindBy(4).IsChecked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
