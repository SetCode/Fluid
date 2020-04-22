using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;
using System.Diagnostics;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.Dialogs;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal class TeachNeedleToCamera : TeachFormBase, IClickable
    {
        private int vavelNo;
        private int flag = 0;
        private Label lblDiagram;
        private PictureBox picDiagram;
        private CameraControl cameraControl;

        #region 语言切换字符串变量

        private string[] lblTip = new string[4]
        {
            "The Machine will move, press [Next] to continue.",
            "Align needle on mark and press [Teach]",
            "Align camera on needle mark and press [Teach]",
            "Teach the distance\r\nof Needle with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:D" +
            "ispenser will\r\nmove after your response"
        };

        #endregion
        public TeachNeedleToCamera(int vavelNo)
            :base()
        {
            this.vavelNo = vavelNo;
            this.InitializeComponent();
            this.UpdateByFlag();
            this.ReadLanguageResources();
            this.lblDiagram.Text = this.lblTip[3];
            this.FormClosed += TeachNeedleToCamera_FormClosed;
        }

        private void TeachNeedleToCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.picDiagram.Image.Dispose();
            this.Dispose(true);
        }

        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private TeachNeedleToCamera()
        {
            InitializeComponent();
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
                if (temp != "")
                {
                    this.lblTip[i] = temp;
                }
            }
        }

        public void DoHelp()
        {
            
        }

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            this.SaveData();
            this.DialogResult = DialogResult.OK;
            this.Close();
            if (Machine.Instance.Setting.MachineSelect==MachineSelection.YBSX)
            {
                new DialogNeedleAngleWithPlasticene2().ShowDialog();
            }
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
            if (flag == 3)
            {
                this.SaveData();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                UpdateByFlag();
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
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = false;
                    if (this.vavelNo == 1)
                    {
                        this.picDiagram.Image = Properties.Resources.TeachNeedleToCamera;
                    }
                    else if (this.vavelNo == 2)
                    {
                        this.picDiagram.Image = Properties.Resources.TeachVavel2ToCamera;
                    }                  
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();                  
                    this.picDiagram.Show();
                    break;
                case 1:
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZ();
                    //移动到设置文件中Needle对应的Mark Point
                    if (this.vavelNo == 1)
                    {
                        //Machine.Instance.Robot.MovePosXY(DataSetting.Default.Needle1Mark);
                        Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.Needle1Mark);

                    }
                    else if (this.vavelNo == 2)
                    {
                        //Machine.Instance.Robot.MovePosXY(DataSetting.Default.Needle2Mark);
                        Machine.Instance.Robot.MovePosAB(0, 0);
                        Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.Needle2Mark);
                    }
                    this.lblTitle.Text = this.lblTip[1];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.pnlDisplay.Controls.Clear();
                    if (this.vavelNo == 1)
                    {
                        this.picDiagram.Image = Properties.Resources.TeachNeedleToCamera;
                    }
                    else if (this.vavelNo == 2)
                    {
                        this.picDiagram.Image = Properties.Resources.TeachVavel2ToCamera;
                    }
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.pnlDisplay.Controls.Add(this.lblDiagram);
                    this.lblDiagram.Show();
                    this.picDiagram.Show();
                    break;
                case 2:
                    //Needle到Mark的坐标点进行赋值
                    if (this.vavelNo == 1)
                    {
                        DataSetting.Default.Needle1Mark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    else if (this.vavelNo == 2)
                    {
                        DataSetting.Default.Needle2Mark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }                
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZ();
                    //移动到设置文件中Camera对应的Needle Mark Point
                    //Machine.Instance.Robot.MovePosXY(DataSetting.Default.CameraNeedle1Mark);   
                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.CameraNeedle1Mark);

                    //加载相机控件                    
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Show();
                    //
                    this.lblTitle.Text = this.lblTip[2];
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = true;
                    this.btnPrev.Enabled = true;                    
                    break;

            }
        }     

        private void TeachNeedleToCamera_FormClosing(object sender, FormClosingEventArgs e)
        {           
            SVOForm.Instance.IsRunToEnd = false;
        }

        private void SaveData()
        {
            if (this.vavelNo == 1)
            {
                //保存Camera到NeedleMark的坐标点进行赋值
                DataSetting.Default.CameraNeedle1Mark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);

                //计算Needle到点胶阀的距离并赋值
                //Machine.Instance.Robot.CalibPrm.NeedleCamera1 = new PointD(
                //    (DataSetting.Default.Needle1Mark.X - DataSetting.Default.CameraNeedle1Mark.X),
                //    (DataSetting.Default.Needle1Mark.Y - DataSetting.Default.CameraNeedle1Mark.Y));

                //计算Needle到点胶阀的距离，转换为系统坐标
                Machine.Instance.Robot.CalibPrm.NeedleCamera1 = (DataSetting.Default.Needle1Mark.ToSystem() - DataSetting.Default.CameraNeedle1Mark.ToSystem()).ToPoint();

                //保存
                if (DataSetting.Default.DoneStepCount <= 4)
                {
                    DataSetting.Default.DoneStepCount = 4;
                }
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 4;
                Machine.Instance.Robot.SaveCalibPrm();
                DataSetting.Save();

                StepStateMgr.Instance.FindBy(3).IsDone = true;
                StepStateMgr.Instance.FindBy(3).IsChecked();
            }
            else if (this.vavelNo == 2)
            {
                //保存Camera到NeedleMark的坐标点进行赋值
                DataSetting.Default.CameraNeedle1Mark = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);

                //计算Needle到点胶阀的距离并赋值
                Machine.Instance.Robot.CalibPrm.NeedleCamera2 = new PointD(
                    (DataSetting.Default.Needle2Mark.X - DataSetting.Default.CameraNeedle1Mark.X),
                    (DataSetting.Default.Needle2Mark.Y - DataSetting.Default.CameraNeedle1Mark.Y));

                //保存
                if (DataSetting.Default.DoneStepCount <= 12)
                {
                    DataSetting.Default.DoneStepCount = 12;
                }
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 12;
                Machine.Instance.Robot.SaveCalibPrm();
                DataSetting.Save();

                StepStateMgr.Instance.FindBy(11).IsDone = true;
                StepStateMgr.Instance.FindBy(11).IsChecked();
            }
        }
        #region 初始化控件
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
            this.pnlDisplay.Location = new System.Drawing.Point(6, 10);
            this.pnlDisplay.Size = new System.Drawing.Size(500, 397);
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
            this.lblDiagram.Location = new System.Drawing.Point(19, 127);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(149, 120);
            this.lblDiagram.TabIndex = 24;
            this.lblDiagram.Text = "Teach the distance\r\nof Needle with Camera\r\nusing mark point.\r\n\r\n\r\n\r\n\r\n\r\nWARNING:D" +
    "ispenser will\r\nmove after your response";
            // 
            // TeachNeedleToCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(723, 657);
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
        #endregion 
    }
}
