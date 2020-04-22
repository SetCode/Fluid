using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Motion.ActiveItems;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal class TeachScale : TeachFormBase, IClickable
    {
        private int flag = 0;
        private double prevZHeight = 0;
        private double currentZHeight = 0;
        private bool isDoPrev = false;

        #region 新添加的控件字段
        private CameraControl cameraControl;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.TextBox txtCenterZ;
        private System.Windows.Forms.TextBox txtCenterY;
        private System.Windows.Forms.TextBox txtCenterX;
        private System.Windows.Forms.Label lblCenterLocation;
        private FindCircle findCircle;
        private NumericUpDown nudZDistance;
        private Label lblZDistance;
        private Button btnTeachZ;
        private System.Windows.Forms.Label lblDiagram;

        #region 语言切换字符串变量

        private string[] lblTip = new string[20]
        {
            "Teach Center Method",
            "Select teach method and press [Next].",
            "Instructions",
            "Target centre of Scale system plate using camera. \rPress[Teach] to continue.",
            "Target centre of scale and press [Teach].",
            "Target circumference point 1 on Scale using camera. \rPress[Teach] to continue.",
            "Target scale circumference point 1 and press [Teach].",
            "Target circumference point 2 on Scale using camera. \rPress[Teach] to continue.",
            "Target scale circumference point 2 and press [Teach].",
            "Target circumference point 3 on Scale using camera. \rPress[Teach] to continue.",
            "Target scale circumference point 3 and press [Teach].",
            "Height sense will be performed at location viewed by camera.\rAdjust height sense location if necessary.\rPress [Teach] to continue.",
            "Target height sense location on scale system plate.",
            "可能触发限位，建议检查限位位置",
            "激光异常",
            "Looking for the Scale center...",
            "Be looking for...",
            "Move has ended.\rCan observe the needle is or not in the center location of scale." +
                                     "\rPress [Done] if is center location.\rIf is not,maybe teach again if necesseay.",
            "Move has ended,press[Done].",
            "Teach Scale location\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n           " +
                "or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove af" +
                "ter your response"
        };

        #endregion

        #endregion
        public TeachScale()
        {
            InitializeComponent();
            this.nudZDistance.Minimum = -1000;
            this.nudZDistance.Maximum = 1000;
            if (Machine.Instance.Laser.Laserable.Vendor != Laser.Vendor.Disable)
            {
                this.nudZDistance.Value = DataSetting.Default.ScaleZDistance;
            }
            else
            {
                this.nudZDistance.Value = (decimal)(Machine.Instance.Robot.CalibPrm.ScaleIntervalHeight);
            }               
            this.picDiagram.Image = Properties.Resources.TeachScale;
            this.UpdateByFlag();
            this.ReadLanguageResources();
            this.lblDiagram.Text = this.lblTip[19];
            this.FormClosed += TeachScale_FormClosed;
        }

        private void TeachScale_FormClosed(object sender, FormClosedEventArgs e)
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
                if (temp != "")
                {
                    this.lblTip[i] = temp;
                }
            }
        }

        public void DoHelp()
        {

        }
        public void DoPrev()
        {
            this.isDoPrev = true;
            if (this.findCircle.rdoFindCircleOnePoint.Checked)
            {
                flag--;
                UpdateByFlag();
            }
            else if (this.findCircle.rdoFindCircleThreePoint.Checked)
            {
                if (flag > 5 && flag <= 25)
                {
                    flag = 5;
                }
                else
                {
                    flag -= 5;
                }

                UpdateByFlag();
            }
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                if (50 < flag && flag < 500)
                {
                    flag = 50;
                }
                else
                {
                    flag -= 50;
                }
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
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                flag += 50;
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
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                flag += 50;
                UpdateByFlag();
            }
        }

        public void DoDone()
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();
            //保存
            if (DataSetting.Default.DoneStepCount <= 9)
            {
                DataSetting.Default.DoneStepCount = 9;
            }
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.CalibPrm.SavedItem = 9;
            DataSetting.Default.ScaleZDistance = this.nudZDistance.Value;
            Machine.Instance.Robot.CalibPrm.ScaleIntervalHeight = Convert.ToDouble(this.nudZDistance.Value);
            Machine.Instance.Robot.SaveCalibPrm();
            DataSetting.Save();
            Machine.Instance.UpdateLocations();

            StepStateMgr.Instance.FindBy(8).IsDone = true;
            StepStateMgr.Instance.FindBy(8).IsChecked();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            //参数修改记录
            //CompareObj.CompareProperty(Machine.Instance.Robot.CalibPrm, Machine.Instance.Robot.CalibPrmBackUp);
        }

        public void DoCancel()
        {

            this.Close();
        }
        private void TeachScale_Load(object sender, EventArgs e)
        {
            this.txtCenterX.Text = Machine.Instance.Robot.CalibPrm.ScaleLoc.X.ToString();
            this.txtCenterY.Text = Machine.Instance.Robot.CalibPrm.ScaleLoc.Y.ToString();
            this.txtCenterZ.Text = (Machine.Instance.Robot.CalibPrm.ScaleZbyHS +
                Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight).ToString();
        }
        private void TeachScale_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();
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
                    this.findCircle.grpSwitch.Text = this.lblTip[0];
                    this.findCircle.rdoFindCircleOnePoint.Show();
                    this.findCircle.rdoFindCircleThreePoint.Show();
                    this.findCircle.lblMessage.Hide();

                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = this.lblTip[1];
                    break;
                //单点找圆心分支
                case 1:
                    this.BranchesFirstStep(DataSetting.Default.ScaleCenter, this.lblTip[2],this.lblTip[3],this.lblTip[4]);
                    break;
                case 2:
                    Machine.Instance.Robot.MoveSafeZ();
                    //重新赋值相机称重位中心点
                    if (!isDoPrev)
                    {
                        DataSetting.Default.ScaleCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    this.nudZDistance.Enabled = true;
                    this.SecondLastStep();
                    break;
                case 3:
                    this.nudZDistance.Enabled = false;
                    this.LastStep();
                    break;

                //三点求圆心分支
                case 5:
                    this.BranchesFirstStep(DataSetting.Default.ScaleCircumferenceP1, this.lblTip[2], this.lblTip[5], this.lblTip[6]);
                    break;
                case 10:
                    //保存点P1
                    DataSetting.Default.ScaleCircumferenceP1 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //判断是否为第一次拾取,如果是,给定P2,P3一个值,使其不回原点.
                    if (DataSetting.Default.ScaleCircumferenceP2.X == 0 && DataSetting.Default.ScaleCircumferenceP2.Y == 0)
                    {
                        DataSetting.Default.ScaleCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //移动到默认点P2
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.ScaleCircumferenceP2);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[7];
                    this.lblTitle.Text = this.lblTip[8];
                    break;
                case 15:
                    //保存点P2
                    DataSetting.Default.ScaleCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //判断是否为第一次拾取,如果是,给定P2,P3一个值,使其不回原点.
                    if (DataSetting.Default.ScaleCircumferenceP3.X == 0 && DataSetting.Default.ScaleCircumferenceP3.Y == 0)
                    {
                        DataSetting.Default.ScaleCircumferenceP3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //移动到默认点P3
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.ScaleCircumferenceP3);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[9];
                    this.lblTitle.Text = this.lblTip[10];
                    break;
                case 20:
                    Machine.Instance.Robot.MoveSafeZ();
                    this.btnTeachZ.Enabled = false;
                    //三点求圆心
                    this.nudZDistance.Enabled = true;
                    DataSetting.Default.ScaleCenter = MathUtils.CalculateCircleCenter(DataSetting.Default.ScaleCircumferenceP1,
                        DataSetting.Default.ScaleCircumferenceP2, DataSetting.Default.ScaleCircumferenceP3);
                    this.SecondLastStep();
                    break;
                case 25:

                    this.nudZDistance.Enabled = false;
                    this.LastStep();
                    break;

                case 50:
                    BranchesFirstStep(DataSetting.Default.ScaleCenter.ToNeedle(ValveType.Valve1), this.lblTip[2], this.lblTip[3], this.lblTip[4]);
                    break;
                case 100:
                    Machine.Instance.Robot.MoveSafeZ();
                    //重新赋值相机称重位中心点
                    if (!isDoPrev)
                    {
                        DataSetting.Default.ScaleCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY).ToCamera(ValveType.Valve1);
                    }
                    this.nudZDistance.Enabled = true;
                    this.SecondLastStep();
                    break;
                case 150:
                    this.nudZDistance.Enabled = false;
                    this.LastStep();
                    break;
            }
            this.isDoPrev = false;
        }

        public void BranchesFirstStep(PointD loc,string grpSwitchText,string lblMsgText,string lblTitleText)
        {
            Machine.Instance.Robot.MoveSafeZAndReply();
            //移动到默认点
            Machine.Instance.Robot.ManualMovePosXYAndReply(loc);
            //
            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(this.cameraControl);
            this.cameraControl.Show();
            this.findCircle.grpSwitch.Text = grpSwitchText;
            this.findCircle.HideAllrdoAndShowMsglbl(true);
            this.findCircle.lblMessage.Text = lblMsgText;

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = true;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = lblTitleText;
        }

        private void SecondLastStep()
        {
            //保存点胶阀在称重位的中心位置
            Machine.Instance.Robot.CalibPrm.ScaleLoc = new PointD(DataSetting.Default.ScaleCenter.X, DataSetting.Default.ScaleCenter.Y);

            this.findCircle.lblMessage.Text = this.lblTip[11];
            this.lblTitle.Text = this.lblTip[12];

            //如果没有测高就返回
            if (Machine.Instance.Laser.Laserable.Vendor == Laser.Vendor.Disable)
                return;

            //移动到测高位置
            Machine.Instance.Robot.ManualMovePosXYAndReply(Machine.Instance.Robot.CalibPrm.ScaleHS);
        }
        /// <summary>
        /// 进行寻找称重中心测试
        /// </summary>
        private void LastStep()
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();

            if (Machine.Instance.Laser.Laserable.Vendor != Laser.Vendor.Disable)
            {
                //保存测高位置
                Machine.Instance.Robot.CalibPrm.ScaleHS = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                //关闭光源
                Machine.Instance.Light.None();
                //

                //将激光移动到测高位置
                Result moveResult = Machine.Instance.Robot.ManualMovePosXYAndReply(new PointD(Machine.Instance.Robot.PosX + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                    Machine.Instance.Robot.PosY + Machine.Instance.Robot.CalibPrm.HeightCamera.Y));

                if (!moveResult.IsOk)
                {
                    MessageBox.Show(this.lblTip[13]);
                    return;
                }

                //获取激光返回的高度值
                int rst = -1;
                double zByHs;

                Machine.Instance.MeasureHeightBefore();
                rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out zByHs);
                Machine.Instance.MeasureHeightAfter();

                if (rst != 0)
                {
                    MessageBox.Show(this.lblTip[14]);
                    return;
                }
                Machine.Instance.Robot.CalibPrm.ScaleZbyHS = zByHs;

            }

            //运动前
            this.findCircle.lblMessage.Text = this.lblTip[15];
            this.lblTitle.Text = this.lblTip[16];
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.btnTeachZ.Enabled = false;

            //开始运动后
            Task.Factory.StartNew(() =>
            {
                if (Machine.Instance.Laser.Laserable.Vendor != Laser.Vendor.Disable)
                {
                    //打开光源
                    Machine.Instance.Light.ResetToLast();
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.ScaleCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                        DataSetting.Default.ScaleCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);
                    Machine.Instance.Robot.MovePosZAndReply(Machine.Instance.Robot.CalibPrm.StandardZ + Machine.Instance.Robot.CalibPrm.ScaleZbyHS - Machine.Instance.Robot.CalibPrm.StandardHeight + (double)this.nudZDistance.Value);
                    Thread.Sleep(200);
                    this.prevZHeight = Machine.Instance.Robot.PosZ;
                }
                else
                {
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.ScaleCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                        DataSetting.Default.ScaleCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);

                    Machine.Instance.Robot.MovePosZ(Machine.Instance.Robot.CalibPrm.ScaleIntervalHeight);
                    MessageBox.Show("此时可示教Z轴高度");
                }
                            
                //运动结束
                this.BeginInvoke(new Action(() =>
                {
                    this.findCircle.lblMessage.Text = this.lblTip[17];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnTeachZ.Enabled = true;
                    this.lblTitle.Text = this.lblTip[18];
                }));
            });
        }
        private void btnTeachZ_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Laser.Laserable.Vendor != Laser.Vendor.Disable)
            {
                this.currentZHeight = Machine.Instance.Robot.PosZ;
                double dz = this.currentZHeight - this.prevZHeight;
                this.prevZHeight = this.currentZHeight;
                double value = MathUtils.Limit((double)this.nudZDistance.Value + dz, (double)this.nudZDistance.Minimum, (double)this.nudZDistance.Maximum);
                this.nudZDistance.Value = (decimal)value;
            }
            else
            {
                Machine.Instance.Robot.CalibPrm.ScaleZbyHS = Machine.Instance.Robot.PosZ;
                Machine.Instance.Robot.CalibPrm.ScaleZbyHS = Machine.Instance.Robot.PosZ - Machine.Instance.Robot.CalibPrm.StandardZ
                        + Machine.Instance.Robot.CalibPrm.StandardHeight - Machine.Instance.Robot.CalibPrm.ScaleIntervalHeight;
                this.nudZDistance.Value = (decimal)Machine.Instance.Robot.PosZ;
            }
            
        }

        private void InitializeComponent()
        {
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.txtCenterZ = new System.Windows.Forms.TextBox();
            this.txtCenterY = new System.Windows.Forms.TextBox();
            this.txtCenterX = new System.Windows.Forms.TextBox();
            this.lblCenterLocation = new System.Windows.Forms.Label();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.lblZDistance = new System.Windows.Forms.Label();
            this.nudZDistance = new System.Windows.Forms.NumericUpDown();
            this.btnTeachZ = new System.Windows.Forms.Button();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.btnTeachZ);
            this.grpResultTest.Controls.Add(this.nudZDistance);
            this.grpResultTest.Controls.Add(this.lblZDistance);
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
            this.picDiagram.Location = new System.Drawing.Point(179, 34);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(290, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 5;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(18, 34);
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
            this.txtCenterZ.Location = new System.Drawing.Point(371, 124);
            this.txtCenterZ.Name = "txtCenterZ";
            this.txtCenterZ.Size = new System.Drawing.Size(72, 21);
            this.txtCenterZ.TabIndex = 9;
            // 
            // txtCenterY
            // 
            this.txtCenterY.Enabled = false;
            this.txtCenterY.Location = new System.Drawing.Point(278, 124);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(72, 21);
            this.txtCenterY.TabIndex = 8;
            // 
            // txtCenterX
            // 
            this.txtCenterX.Enabled = false;
            this.txtCenterX.Location = new System.Drawing.Point(185, 124);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(72, 21);
            this.txtCenterX.TabIndex = 7;
            // 
            // lblCenterLocation
            // 
            this.lblCenterLocation.AutoSize = true;
            this.lblCenterLocation.Location = new System.Drawing.Point(66, 127);
            this.lblCenterLocation.Name = "lblCenterLocation";
            this.lblCenterLocation.Size = new System.Drawing.Size(113, 12);
            this.lblCenterLocation.TabIndex = 6;
            this.lblCenterLocation.Text = "Center Location : ";
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(31, 28);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(450, 90);
            this.findCircle.TabIndex = 5;
            // 
            // cameraControl
            // 
            this.cameraControl.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
            // 
            // lblZDistance
            // 
            this.lblZDistance.AutoSize = true;
            this.lblZDistance.Location = new System.Drawing.Point(29, 159);
            this.lblZDistance.Name = "lblZDistance";
            this.lblZDistance.Size = new System.Drawing.Size(275, 24);
            this.lblZDistance.TabIndex = 10;
            this.lblZDistance.Text = "Desired Z distance between needle and cup lid\r\n(+=below lid,-=above lid)";
            // 
            // nudZDistance
            // 
            this.nudZDistance.DecimalPlaces = 4;
            this.nudZDistance.Location = new System.Drawing.Point(325, 157);
            this.nudZDistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudZDistance.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudZDistance.Name = "nudZDistance";
            this.nudZDistance.Size = new System.Drawing.Size(72, 21);
            this.nudZDistance.TabIndex = 11;
            // 
            // btnTeachZ
            // 
            this.btnTeachZ.Enabled = false;
            this.btnTeachZ.Location = new System.Drawing.Point(419, 156);
            this.btnTeachZ.Name = "btnTeachZ";
            this.btnTeachZ.Size = new System.Drawing.Size(56, 23);
            this.btnTeachZ.TabIndex = 12;
            this.btnTeachZ.Text = "Teach Z";
            this.btnTeachZ.UseVisualStyleBackColor = true;
            this.btnTeachZ.Click += new System.EventHandler(this.btnTeachZ_Click);
            // 
            // TeachScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(723, 657);
            this.Name = "TeachScale";
            this.Text = "Anda Fluidmove - Teach location for Valve Scale location";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachScale_FormClosing);
            this.Load += new System.EventHandler(this.TeachScale_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}
