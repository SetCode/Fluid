using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class TeachPrime : TeachFormBase, IClickable
    {
        private int flag = 0;
        private CameraControl cameraControl;
        private FindCircle findCircle;
        private System.Windows.Forms.Button btnTeachZ;
        private System.Windows.Forms.NumericUpDown nudZDistance;
        private System.Windows.Forms.Label lblZDistance;
        private System.Windows.Forms.TextBox txtCenterZ;
        private System.Windows.Forms.TextBox txtCenterY;
        private System.Windows.Forms.TextBox txtCenterX;
        private System.Windows.Forms.Label lblCenterLocation;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.Label lblDiagram;


        #region 语言切换字符串变量

        private string[] lblTip = new string[16]
        {
            "Teach Center Method",
            "Select teach method and press [Next].",
            "Instructions",
            "Target centre of Primer using camera. \rPress[Teach] to continue.",
            "Target centre of Primer and press [Teach].",
            "Target circumference point 1 on Primer using camera. \rPress[Teach] to continue.",
            "Target Primer circumference point 1 and press [Teach].",
            "Target circumference point 2 on Primer using camera. \rPress[Teach] to continue.",
            "Target Primer circumference point 2 and press [Teach].",
            "Target circumference point 3 on Primer using camera. \rPress[Teach] to continue.",
            "Target primer circumference point 3 and press [Teach].",
            "Looking for the Primer center...",
            "Be looking for...",
            "Move has ended.\rCan observe the needle is or not in the center location of Primer." +
                                     "\rPress [Done] if is center location.\rIf is not,maybe teach needle to camera again if necesseay.",
            "Move has ended,press[Done].",
            "Teach Primer location\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n          " +
            " or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove a" +
            "fter your response"
    };

        #endregion

        public TeachPrime()
        {
            InitializeComponent();
            this.picDiagram.Image = Properties.Resources.TeachPrime;
            this.UpdateByFlag();
            this.ReadLanguageResources();
            this.findCircle.grpSwitch.Text = this.lblTip[0];
            this.lblDiagram.Text = this.lblTip[15];
            this.FormClosed += TeachPrime_FormClosed;
        }

        private void TeachPrime_FormClosed(object sender, FormClosedEventArgs e)
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

        private void TeachPrime_Load(object sender, EventArgs e)
        {
            this.nudZDistance.Value = (decimal)Machine.Instance.Robot.CalibPrm.PrimeZ;
            this.txtCenterX.Text = Machine.Instance.Robot.CalibPrm.PrimeLoc.X.ToString();
            this.txtCenterY.Text = Machine.Instance.Robot.CalibPrm.PrimeLoc.Y.ToString();
            this.txtCenterZ.Text = Machine.Instance.Robot.CalibPrm.PrimeZ.ToString();
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

        public void DoPrev()
        {
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
            if (DataSetting.Default.DoneStepCount <= 8)
            {
                DataSetting.Default.DoneStepCount = 8;
            }
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.CalibPrm.SavedItem = 8;
            DataSetting.Default.PrimeZDistance = this.nudZDistance.Value;

            Machine.Instance.Robot.CalibPrm.PrimeZ = Convert.ToDouble(this.nudZDistance.Value);
            Machine.Instance.Robot.SaveCalibPrm();
            DataSetting.Save();
            //是否需要？
            Machine.Instance.UpdateLocations();

            StepStateMgr.Instance.FindBy(7).IsDone = true;
            StepStateMgr.Instance.FindBy(7).IsChecked();
            this.DialogResult = DialogResult.OK;
            this.Close();
            //参数修改记录
            //CompareObj.CompareProperty(Machine.Instance.Robot.CalibPrm, Machine.Instance.Robot.CalibPrmBackUp);
        }

        public void DoHelp()
        {

        }

        public void DoCancel()
        {
            this.Close();
        }
        private void btnTeachZ_Click(object sender, EventArgs e)
        {
            this.nudZDistance.Value = (decimal)Machine.Instance.Robot.PosZ;
        }
        private void TeachPrime_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();

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
                    this.BranchesFirstStep(DataSetting.Default.PrimeCenter, this.lblTip[2], this.lblTip[3], this.lblTip[4]);
                    break;
                case 2:
                    //保存单点位置
                    DataSetting.Default.PrimeCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //求得阀排胶的位置
                    Machine.Instance.Robot.CalibPrm.PrimeLoc = new PointD(DataSetting.Default.PrimeCenter.X, DataSetting.Default.PrimeCenter.Y);

                    this.LastStep();
                    break;

                //三点求圆心分支
                case 5:
                    this.BranchesFirstStep(DataSetting.Default.PrimeCircumferenceP1, this.lblTip[2], this.lblTip[5], this.lblTip[6]);
                    break;
                case 10:
                    //保存点P1
                    DataSetting.Default.PrimeCircumferenceP1 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    if (DataSetting.Default.PrimeCircumferenceP2.X == 0 && DataSetting.Default.PrimeCircumferenceP2.Y == 0)
                    {
                        DataSetting.Default.PrimeCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //
                    //移动到点P2
                    //Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.PrimeCircumferenceP2);
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.PrimeCircumferenceP2);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[7];
                    this.lblTitle.Text = this.lblTip[8];
                    break;
                case 15:
                    //保存点P2
                    DataSetting.Default.PrimeCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    if (DataSetting.Default.PrimeCircumferenceP3.X == 0 && DataSetting.Default.PrimeCircumferenceP3.Y == 0)
                    {
                        DataSetting.Default.PrimeCircumferenceP3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //
                    //移动到点P3
                    //Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.PrimeCircumferenceP3);
                    Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.PrimeCircumferenceP3);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[9];
                    this.lblTitle.Text = this.lblTip[10];
                    break;
                case 20:
                    //保存点P3
                    DataSetting.Default.PrimeCircumferenceP3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //三点求圆心
                    DataSetting.Default.PrimeCenter = MathUtils.CalculateCircleCenter(DataSetting.Default.PrimeCircumferenceP1,
                        DataSetting.Default.PrimeCircumferenceP2, DataSetting.Default.PrimeCircumferenceP3);

                    this.LastStep();
                    break;
                //喷嘴找圆心分支
                case 50:
                    this.BranchesFirstStep(DataSetting.Default.PrimeCenter.ToNeedle(ValveType.Valve1), this.lblTip[2], this.lblTip[3], this.lblTip[4]);
                    break;
                case 100:
                    //保存单点位置
                    DataSetting.Default.PrimeCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY).ToCamera(ValveType.Valve1);
                    //求得阀排胶的位置
                    Machine.Instance.Robot.CalibPrm.PrimeLoc = new PointD(DataSetting.Default.PrimeCenter.X, DataSetting.Default.PrimeCenter.Y);

                    this.LastStep();
                    break;
            }
        }

        public void BranchesFirstStep(PointD loc,string grpSwitchText,string lblMsgText,string lblTitleText)
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZAndReply();
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

        private void LastStep()
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();

            //运动前
            this.findCircle.lblMessage.Text = this.lblTip[11];
            this.lblTitle.Text = this.lblTip[12];
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.btnTeachZ.Enabled = false;

            //开始运动后
            Task.Factory.StartNew(new Action(() =>
            {
                //运动到Prime中心并下降
                //Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.PrimeCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                //        DataSetting.Default.PrimeCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);
                Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.PrimeCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                        DataSetting.Default.PrimeCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);
                Machine.Instance.Robot.MovePosZAndReply((double)this.nudZDistance.Value);

                //运动结束
                this.BeginInvoke(new Action(() =>
                {
                    this.findCircle.lblMessage.Text = this.lblTip[13];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnTeachZ.Enabled = true;
                    this.lblTitle.Text = this.lblTip[14];
                }));
            }));
        }

        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.btnTeachZ = new System.Windows.Forms.Button();
            this.nudZDistance = new System.Windows.Forms.NumericUpDown();
            this.lblZDistance = new System.Windows.Forms.Label();
            this.txtCenterZ = new System.Windows.Forms.TextBox();
            this.txtCenterY = new System.Windows.Forms.TextBox();
            this.txtCenterX = new System.Windows.Forms.TextBox();
            this.lblCenterLocation = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
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
            // cameraControl
            // 
            this.cameraControl.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = new System.Drawing.Size(569, 441);
            this.cameraControl.TabIndex = 0;
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(28, 16);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(450, 90);
            this.findCircle.TabIndex = 0;
            // 
            // btnTeachZ
            // 
            this.btnTeachZ.Location = new System.Drawing.Point(422, 157);
            this.btnTeachZ.Name = "btnTeachZ";
            this.btnTeachZ.Size = new System.Drawing.Size(63, 23);
            this.btnTeachZ.TabIndex = 14;
            this.btnTeachZ.Text = "Teach Z";
            this.btnTeachZ.UseVisualStyleBackColor = true;
            this.btnTeachZ.Click += new System.EventHandler(this.btnTeachZ_Click);
            // 
            // nudZDistance
            // 
            this.nudZDistance.DecimalPlaces = 4;
            this.nudZDistance.Location = new System.Drawing.Point(328, 158);
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
            this.nudZDistance.TabIndex = 13;
            // 
            // lblZDistance
            // 
            this.lblZDistance.AutoSize = true;
            this.lblZDistance.Location = new System.Drawing.Point(34, 158);
            this.lblZDistance.Name = "lblZDistance";
            this.lblZDistance.Size = new System.Drawing.Size(275, 24);
            this.lblZDistance.TabIndex = 12;
            this.lblZDistance.Text = "Desired Z distance between needle and cup lid\r\n(+=below lid,-=above lid)";
            // 
            // txtCenterZ
            // 
            this.txtCenterZ.Enabled = false;
            this.txtCenterZ.Location = new System.Drawing.Point(381, 118);
            this.txtCenterZ.Name = "txtCenterZ";
            this.txtCenterZ.Size = new System.Drawing.Size(72, 21);
            this.txtCenterZ.TabIndex = 11;
            // 
            // txtCenterY
            // 
            this.txtCenterY.Enabled = false;
            this.txtCenterY.Location = new System.Drawing.Point(288, 118);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(72, 21);
            this.txtCenterY.TabIndex = 10;
            // 
            // txtCenterX
            // 
            this.txtCenterX.Enabled = false;
            this.txtCenterX.Location = new System.Drawing.Point(195, 118);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(72, 21);
            this.txtCenterX.TabIndex = 9;
            // 
            // lblCenterLocation
            // 
            this.lblCenterLocation.AutoSize = true;
            this.lblCenterLocation.Location = new System.Drawing.Point(76, 121);
            this.lblCenterLocation.Name = "lblCenterLocation";
            this.lblCenterLocation.Size = new System.Drawing.Size(113, 12);
            this.lblCenterLocation.TabIndex = 8;
            this.lblCenterLocation.Text = "Center Location : ";
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(186, 31);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(290, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 5;
            this.picDiagram.TabStop = false;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(25, 31);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(155, 216);
            this.lblDiagram.TabIndex = 4;
            this.lblDiagram.Text = "Teach Primer location\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n          " +
    " or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove a" +
    "fter your response";
            // 
            // TeachPrime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(723, 657);
            this.Name = "TeachPrime";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachPrime_FormClosing);
            this.Load += new System.EventHandler(this.TeachPrime_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}
