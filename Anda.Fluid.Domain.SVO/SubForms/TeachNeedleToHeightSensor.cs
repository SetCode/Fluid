using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;
using System.Drawing;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal class TeachNeedleToHeightSensor : TeachFormBase, IClickable
    {
        private int flag = 0;
        private int valvelNo;
        private ValveType curValve;
        #region 
        private CameraControl cameraControl;
        private FindCircle findCircle;
        private System.Windows.Forms.Label lblCycles;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Label lblZOffsetResult;
        private System.Windows.Forms.TextBox txtZOffsetResult;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.Label lblDiagram;
        private NumericUpDown nudCycles;
        private Label lblVelocity;
        private NumericUpDown nudVelocity;
        private TextBox txtRead;
        private TextBox txtState;
        private Button btnRead;
        private NumericUpDown nudTolerance;
        private Button btnState;
        private Label label2;
        private TextBox txtNeedlePosZ;
        private Label lblLaserHeight;
        private TextBox txtLaserHeight;
        #endregion

        #region 语言切换字符串变量

        private string[] lblTip = new string[25]
        {
            "Teach Center Method",
            "Select teach method and press [Next].",
            "Target centre point on tactile using camera. \rPress[Teach] to continue.",
            "Target tactile centre point and press [Teach].",
            "Instructions",
            "Target circumference point 1 on tactile using camera. \rPress[Teach] to continue.",
            "Target tactile circumference point 1 and press [Teach].",
            "Target circumference point 2 on tactile using camera. \rPress[Teach] to continue.",
            "Target tactile circumference point 2 and press [Teach].",
            "Target circumference point 3 on tactile using camera. \rPress[Teach] to continue.",
            "Target tactile circumference point 3 and press [Teach].",
            "This machine will move it's parts. \rKeep safe distance and press [Next] to continue.",
            "Prepare for machine's parts move and press [Next]",
            "Measuring the Z height...",
            "Measurement has ended...",
            "Measurement has ended,press[Done].",
            "Running cycle",
            "可能触发限位，建议检查限位位置.",
            "激光读取失败.",
            "可能触发Z轴软限位，建议重设软限位位置.",
            "Vavel2 Z Offset",
            "connected",
            "disconnected",
            "failed",
            "Teach center of tactile\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n        " +
            "   or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove" +
            " after your response"
    };

        #endregion

        public TeachNeedleToHeightSensor(int vavelNo)
        {
            this.valvelNo = vavelNo;
            if (valvelNo == 1)
            {
                this.curValve = ValveType.Valve1;
            }
            else
            {
                this.curValve = ValveType.Valve2;
            }
            InitializeComponent();
            this.picDiagram.Image = Properties.Resources.TeachNeedleToHeightSensor;
            UpdateByFlag();

            this.txtState.ReadOnly = true;
            this.txtRead.ReadOnly = true;
            this.ReadLanguageResources();
            this.lblDiagram.Text = this.lblTip[24];

            this.txtLaserHeight.ReadOnly = true;
            this.txtNeedlePosZ.ReadOnly = true;
            this.FormClosed += TeachNeedleToHeightSensor_FormClosed;
        }

        private void TeachNeedleToHeightSensor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.picDiagram.Image.Dispose();
            this.Dispose(true);
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private TeachNeedleToHeightSensor()
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

        public void DoCancel()
        {

            this.Close();
        }

        public void DoDone()
        {
            Machine.Instance.Robot.CalibPrm.HeightSensorCenter = DataSetting.Default.HeightSensorCenter;
            if (this.valvelNo == 1)
            {
                //保存
                if (DataSetting.Default.DoneStepCount <= 6)
                {
                    DataSetting.Default.DoneStepCount = 6;
                }                
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 6;
                Machine.Instance.Robot.SaveCalibPrm();
                DataSetting.Save();

                StepStateMgr.Instance.FindBy(5).IsDone = true;
                StepStateMgr.Instance.FindBy(5).IsChecked();
            }
            else if (this.valvelNo == 2)
            {
                //保存
                if (DataSetting.Default.DoneStepCount <= 13)
                {
                    DataSetting.Default.DoneStepCount = 13;
                }
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 13;
                Machine.Instance.Robot.SaveCalibPrm();
                DataSetting.Save();

                StepStateMgr.Instance.FindBy(12).IsDone = true;
                StepStateMgr.Instance.FindBy(12).IsChecked();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            //参数修改记录
            //CompareObj.CompareProperty(Machine.Instance.Robot.CalibPrm, Machine.Instance.Robot.CalibPrmBackUp);
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
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                flag += 40;
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
                if (4 < flag && flag <= 20)
                {
                    flag = 4;
                }
                else
                {
                    flag -= 4;
                }

                UpdateByFlag();
            }
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                if (40 < flag && flag <= 120)
                {
                    flag = 40;
                }
                else
                {
                    flag -= 40;
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
                flag += 4;
                UpdateByFlag();
            }
            else if (this.findCircle.rdoTeachByNeedle.Checked)
            {
                flag += 40;
                UpdateByFlag();
            }
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
                    this.findCircle.HideAllrdoAndShowMsglbl(false);

                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    if (Machine.Instance.Laser.Laserable.Vendor == Laser.Vendor.Disable) 
                    {
                        this.btnDone.Enabled = true;
                    }
                    else
                    {
                        this.btnDone.Enabled = false;
                    }
                    this.lblTitle.Text = this.lblTip[1];
                    break;

                //单点找圆心分支
                case 1:
                    BranchesFirstStep(DataSetting.Default.HeightSensorCenter, this.lblTip[4], this.lblTip[2], this.lblTip[3]);
                    break;
                case 2:
                    //保存相机位置
                    DataSetting.Default.HeightSensorCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //
                    //对中心点进行赋值
                    DataSetting.Default.HeightCenter = DataSetting.Default.HeightSensorCenter;

                    SecondLastInMeasurement();
                    break;
                case 3:
                    LastStepInMeasurement();
                    break;

                //三点求圆心分支
                case 4:
                    BranchesFirstStep(DataSetting.Default.HeightCircumferenceP1, this.lblTip[4], this.lblTip[5], this.lblTip[6]);
                    break;
                case 8:
                    //保存点P1
                    DataSetting.Default.HeightCircumferenceP1 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    if (DataSetting.Default.HeightCircumferenceP2.X == 0 && DataSetting.Default.HeightCircumferenceP2.Y == 0)
                    {
                        DataSetting.Default.HeightCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //
                    //移动到默认点p2
                    Machine.Instance.Robot.MoveSafeZ();
                    //Machine.Instance.Robot.MovePosXY(DataSetting.Default.HeightCircumferenceP2);
                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.HeightCircumferenceP2);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[7];
                    this.lblTitle.Text = this.lblTip[8];
                    break;
                case 12:
                    //保存点P2
                    DataSetting.Default.HeightCircumferenceP2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    if (DataSetting.Default.HeightCircumferenceP3.X == 0 && DataSetting.Default.HeightCircumferenceP3.Y == 0)
                    {
                        DataSetting.Default.HeightCircumferenceP3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    }
                    //
                    //移动到默认点p3
                    Machine.Instance.Robot.MoveSafeZ();
                    //Machine.Instance.Robot.MovePosXY(DataSetting.Default.HeightCircumferenceP3);
                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.HeightCircumferenceP3);
                    //
                    this.findCircle.lblMessage.Text = this.lblTip[9];
                    this.lblTitle.Text = this.lblTip[10];
                    break;
                case 16:
                    //保存点P3
                    DataSetting.Default.HeightCircumferenceP3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    //
                    //通过三点求得圆心
                    DataSetting.Default.HeightCenter = MathUtils.CalculateCircleCenter(DataSetting.Default.HeightCircumferenceP1,
                        DataSetting.Default.HeightCircumferenceP2, DataSetting.Default.HeightCircumferenceP3);
                    //保存相机位置
                    DataSetting.Default.HeightSensorCenter = DataSetting.Default.HeightCenter;
                    SecondLastInMeasurement();
                    break;
                case 20:
                    LastStepInMeasurement();
                    break;
                case 40:

                    BranchesFirstStep(DataSetting.Default.HeightSensorCenter.ToNeedle(this.curValve), this.lblTip[4], this.lblTip[2], this.lblTip[3]);
                    break;
                case 80:
                    //示教胶阀坐标--转换为相机坐标保存
                    DataSetting.Default.HeightSensorCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY).ToCamera(this.curValve);
                    //
                    //对中心点进行赋值
                    DataSetting.Default.HeightCenter = DataSetting.Default.HeightSensorCenter;

                    SecondLastInMeasurement();
                    break;
                case 120:
                    LastStepInMeasurement();
                    break;
            }

        }
        /// <summary>
        /// 确认可以进行运动
        /// </summary>
        private void SecondLastInMeasurement()
        {
            this.findCircle.lblMessage.Text = this.lblTip[11];

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = this.lblTip[12];
        }
        /// <summary>
        /// 进行Z轴测量
        /// </summary>
        private void LastStepInMeasurement()
        {
            //测量中
            this.findCircle.lblMessage.Text = this.lblTip[13];
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
            this.btnCancel.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                bool b = BeginZAxisMeasurement();
                this.BeginInvoke(new Action(() =>
                {
                    if (!b)
                    {

                    }
                    //测量结束
                    this.findCircle.lblMessage.Text = this.lblTip[14];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnCancel.Enabled = true;
                    this.lblTitle.Text = this.lblTip[15];
                }));
            });
        }
        private bool BeginZAxisMeasurement()
        {
            int cycles = (int)this.nudCycles.Value;
            double totalZ = 0;
            double sumTwiceZ = 0;
            double centerH;

            for (int i = 0; i < cycles; i++)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.lblTitle.Text = String.Format("{0} {1}...", this.lblTip[16], i + 1);
                }));
                //关闭光源
                Machine.Instance.Light.None();
                //
                //抬起到SafeZ
                Machine.Instance.Robot.MoveSafeZAndReply();

                //将激光移动到圆盘中心
                //Result moveResult1 = Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                //    DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                Result moveResult1 = Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                    DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                if (!moveResult1.IsOk)
                {
                    MessageBox.Show(this.lblTip[17]);
                    return false;
                }
                int result = -1;
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(true);
                //} 
                Machine.Instance.MeasureHeightBefore();
                //记录激光测高数据
                result = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out centerH);
                this.BeginInvoke(new Action(() =>
                {
                    this.txtLaserHeight.Text = centerH.ToString("0.000");
                }));
              
                Machine.Instance.MeasureHeightAfter();
                //Result res = Machine.Instance.MeasureHeight(out centerH);
                //result = res.IsOk ? 0 : -1;

                //判断激光能否使用
                if (result != 0)
                {
                    MessageBox.Show(this.lblTip[18]);
                    return false;
                }

                totalZ += centerH;

                //打开光源
                Machine.Instance.Light.ResetToLast();
                //

                //将阀移动至圆盘中心
                if (this.valvelNo == 1)
                {
                    //Result moveResult2 = Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                    // DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);
                    Result moveResult2 = Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X,
                     DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y);
                    if (!moveResult2.IsOk)
                    {
                        MessageBox.Show(this.lblTip[17]);
                        return false;
                    }
                }
                else if (this.valvelNo == 2)
                {
                    //Result moveResult3 = Machine.Instance.Robot.MovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                    // DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);

                    Result moveResult3 = Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.HeightCenter.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                    DataSetting.Default.HeightCenter.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);
                    if (!moveResult3.IsOk)
                    {
                        MessageBox.Show(this.lblTip[17]);
                        return false;
                    }
                }
                //

                //反复下压两次
                for (int j = 0; j < 2; j++)
                {
                    Result moveResult4 = Machine.Instance.Robot.MoveZOnDIAndReply(Convert.ToDouble(this.nudVelocity.Value),
                        (int)DiType.对刀仪, Infrastructure.StsType.High);
                    if (!moveResult4.IsOk)
                    {
                        MessageBox.Show(this.lblTip[19]);
                        return false;
                    }

                    sumTwiceZ += Machine.Instance.Robot.PosZ; //下降到底时Z坐标
                    Machine.Instance.Robot.MoveSafeZAndReply();
                }

            }
            //结果计算            
            if (this.valvelNo == 1)
            {
                Machine.Instance.Robot.CalibPrm.StandardHeight = totalZ / cycles;
                Machine.Instance.Robot.CalibPrm.StandardZ = sumTwiceZ / (cycles * 2);
                this.BeginInvoke(new Action(() =>
                {
                    this.txtZOffsetResult.Text = (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight).ToString();
                    this.txtNeedlePosZ.Text = Machine.Instance.Robot.CalibPrm.StandardZ.ToString("0.000");
                }));
            }
            else if (this.valvelNo == 2)
            {
                Machine.Instance.Robot.CalibPrm.StandardHeight2 = totalZ / cycles;
                Machine.Instance.Robot.CalibPrm.StandardZ2 = sumTwiceZ / (cycles * 2);
                double vavel2ZOffset = Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2;

                this.BeginInvoke(new Action(() =>
                {
                    this.txtZOffsetResult.Text = (vavel2ZOffset - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight)).ToString();
                    this.txtNeedlePosZ.Text = Machine.Instance.Robot.CalibPrm.StandardZ.ToString("0.000");
                }));
                Machine.Instance.Robot.CalibPrm.ValveZOffset2to1 = Machine.Instance.Robot.CalibPrm.StandardZ2 - Machine.Instance.Robot.CalibPrm.StandardHeight2 - (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight);
                if (Math.Abs(Machine.Instance.Robot.CalibPrm.ValveZOffset2to1) > (double)this.nudTolerance.Value)
                {
                    this.txtZOffsetResult.BackColor = Color.Red;
                }
                else
                {
                    this.txtZOffsetResult.BackColor = Color.Green;
                }
            }



            return true;
        }

        public void DoHelp()
        {

        }

        /// <summary>
        /// 每一个分支的第一步
        /// </summary>
        /// <param name="loc">要去的位置</param>
        /// <param name="grpSwitchText">groupbox要显示的文本</param>
        /// <param name="lblMessageText">lblMessage要显示的文本</param>
        public void BranchesFirstStep(PointD loc,string grpSwitchText,string lblMessageText,string lblTitleText)
        {
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZ();
            //移动到默认点p1
            Machine.Instance.Robot.ManualMovePosXY(loc);
            //
            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(this.cameraControl);
            this.cameraControl.Show();
            this.findCircle.grpSwitch.Text = grpSwitchText;
            this.findCircle.HideAllrdoAndShowMsglbl(true);
            this.findCircle.lblMessage.Text = lblMessageText;

            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = true;
            this.btnDone.Enabled = false;
            this.lblTitle.Text = lblTitleText;
        }

        private void TeachNeedleToHeightSensor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SVOForm.Instance.IsRunToEnd = false;
        }

        private void TeachNeedleToHeightSensor_Load(object sender, EventArgs e)
        {
            this.findCircle.rdoFindCircleThreePoint.Checked = true;
            this.findCircle.rdoFindCircleOnePoint.Checked = false;
            if (this.valvelNo == 1)
            {
                this.txtZOffsetResult.Text = (Machine.Instance.Robot.CalibPrm.StandardZ - Machine.Instance.Robot.CalibPrm.StandardHeight).ToString();
            }
            else if (this.valvelNo == 2)
            {
                this.lblZOffsetResult.Text = this.lblTip[20];
            }
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                double value;
                bool b = false;
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(true);
                //}
                Machine.Instance.MeasureHeightBefore();
                b = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value) >= 0;
                Machine.Instance.MeasureHeightAfter();
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(false);
                //}
                //Result res = Machine.Instance.MeasureHeight(out value);
                //b = res.IsOk ? true : false;
                this.BeginInvoke(new Action(() =>
                {
                    if (b)
                    {
                        this.txtState.BackColor = Color.Green;
                        this.txtState.Text = this.lblTip[21];
                    }
                    else
                    {
                        this.txtState.BackColor = Color.Red;
                        this.txtState.Text = this.lblTip[22];
                    }
                }));
            });
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                double value;
                int rtn = -1;
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(true);
                //}
                Machine.Instance.MeasureHeightBefore();
                rtn = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value);
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(false);
                //}
                Machine.Instance.MeasureHeightAfter();
                //Result res = Machine.Instance.MeasureHeight(out value);
                //rtn = res.IsOk ? 0 : -1;

                this.BeginInvoke(new Action(() =>
                {
                    if (rtn == 0)
                    {
                        this.txtRead.BackColor = Color.White;
                        this.txtRead.Text = value.ToString("0.000");
                    }
                    else
                    {
                        this.txtRead.BackColor = Color.Red;
                        this.txtRead.Text = this.lblTip[23];
                    }
                }));
            });
        }
        #region 初始化控件
        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.findCircle = new Anda.Fluid.Domain.SVO.SubForms.FindCircle();
            this.lblCycles = new System.Windows.Forms.Label();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.lblZOffsetResult = new System.Windows.Forms.Label();
            this.txtZOffsetResult = new System.Windows.Forms.TextBox();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.nudCycles = new System.Windows.Forms.NumericUpDown();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.nudVelocity = new System.Windows.Forms.NumericUpDown();
            this.btnState = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.lblLaserHeight = new System.Windows.Forms.Label();
            this.txtLaserHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNeedlePosZ = new System.Windows.Forms.TextBox();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.label2);
            this.grpResultTest.Controls.Add(this.txtNeedlePosZ);
            this.grpResultTest.Controls.Add(this.lblLaserHeight);
            this.grpResultTest.Controls.Add(this.txtLaserHeight);
            this.grpResultTest.Controls.Add(this.nudTolerance);
            this.grpResultTest.Controls.Add(this.nudVelocity);
            this.grpResultTest.Controls.Add(this.lblVelocity);
            this.grpResultTest.Controls.Add(this.nudCycles);
            this.grpResultTest.Controls.Add(this.lblCycles);
            this.grpResultTest.Controls.Add(this.lblTolerance);
            this.grpResultTest.Controls.Add(this.lblZOffsetResult);
            this.grpResultTest.Controls.Add(this.txtZOffsetResult);
            this.grpResultTest.Controls.Add(this.findCircle);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.txtRead);
            this.pnlDisplay.Controls.Add(this.txtState);
            this.pnlDisplay.Controls.Add(this.btnRead);
            this.pnlDisplay.Controls.Add(this.btnState);
            this.pnlDisplay.Controls.Add(this.picDiagram);
            this.pnlDisplay.Controls.Add(this.lblDiagram);
            this.pnlDisplay.Size = new System.Drawing.Size(505, 470);
            // 
            // cameraControl
            // 
            this.cameraControl.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
            // 
            // findCircle
            // 
            this.findCircle.Location = new System.Drawing.Point(30, 8);
            this.findCircle.Name = "findCircle";
            this.findCircle.Size = new System.Drawing.Size(460, 61);
            this.findCircle.TabIndex = 0;
            // 
            // lblCycles
            // 
            this.lblCycles.AutoSize = true;
            this.lblCycles.Location = new System.Drawing.Point(28, 83);
            this.lblCycles.Name = "lblCycles";
            this.lblCycles.Size = new System.Drawing.Size(119, 12);
            this.lblCycles.TabIndex = 11;
            this.lblCycles.Text = "Measurement cycles:";
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(28, 126);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(131, 12);
            this.lblTolerance.TabIndex = 12;
            this.lblTolerance.Text = "Offset tolerance +/-:";
            // 
            // lblZOffsetResult
            // 
            this.lblZOffsetResult.AutoSize = true;
            this.lblZOffsetResult.Location = new System.Drawing.Point(300, 125);
            this.lblZOffsetResult.Name = "lblZOffsetResult";
            this.lblZOffsetResult.Size = new System.Drawing.Size(155, 12);
            this.lblZOffsetResult.TabIndex = 13;
            this.lblZOffsetResult.Text = "Needle to probe Z offset:";
            // 
            // txtZOffsetResult
            // 
            this.txtZOffsetResult.Location = new System.Drawing.Point(406, 123);
            this.txtZOffsetResult.Name = "txtZOffsetResult";
            this.txtZOffsetResult.Size = new System.Drawing.Size(72, 21);
            this.txtZOffsetResult.TabIndex = 16;
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(18, 26);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(155, 216);
            this.lblDiagram.TabIndex = 0;
            this.lblDiagram.Text = "Teach center of tactile\r\nusing camera\r\n\r\n\r\n\r\n\r\nTeach one center point\r\n\r\n        " +
    "   or\r\n\r\nTeach three circumference\r\npoints\r\n\r\n\r\n\r\n\r\nWARNING:Dispenser will\r\nmove" +
    " after your response";
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(179, 26);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(290, 332);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 1;
            this.picDiagram.TabStop = false;
            // 
            // nudCycles
            // 
            this.nudCycles.Location = new System.Drawing.Point(165, 81);
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
            // lblVelocity
            // 
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.Location = new System.Drawing.Point(300, 83);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(137, 12);
            this.lblVelocity.TabIndex = 19;
            this.lblVelocity.Text = "Descent Velocity of Z:";
            // 
            // nudVelocity
            // 
            this.nudVelocity.DecimalPlaces = 6;
            this.nudVelocity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudVelocity.Location = new System.Drawing.Point(406, 81);
            this.nudVelocity.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudVelocity.Name = "nudVelocity";
            this.nudVelocity.Size = new System.Drawing.Size(72, 21);
            this.nudVelocity.TabIndex = 20;
            this.nudVelocity.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnState
            // 
            this.btnState.Location = new System.Drawing.Point(109, 280);
            this.btnState.Name = "btnState";
            this.btnState.Size = new System.Drawing.Size(64, 23);
            this.btnState.TabIndex = 2;
            this.btnState.Text = "state";
            this.btnState.UseVisualStyleBackColor = true;
            this.btnState.Click += new System.EventHandler(this.btnState_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(109, 309);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(64, 23);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(3, 282);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(100, 21);
            this.txtState.TabIndex = 4;
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(3, 309);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(100, 21);
            this.txtRead.TabIndex = 5;
            // 
            // nudTolerance
            // 
            this.nudTolerance.DecimalPlaces = 3;
            this.nudTolerance.Location = new System.Drawing.Point(165, 123);
            this.nudTolerance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(71, 21);
            this.nudTolerance.TabIndex = 21;
            this.nudTolerance.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // lblLaserHeight
            // 
            this.lblLaserHeight.AutoSize = true;
            this.lblLaserHeight.Location = new System.Drawing.Point(28, 165);
            this.lblLaserHeight.Name = "lblLaserHeight";
            this.lblLaserHeight.Size = new System.Drawing.Size(125, 12);
            this.lblLaserHeight.TabIndex = 22;
            this.lblLaserHeight.Text = "LaserStandardHeight:";
            // 
            // txtLaserHeight
            // 
            this.txtLaserHeight.Location = new System.Drawing.Point(165, 161);
            this.txtLaserHeight.Name = "txtLaserHeight";
            this.txtLaserHeight.Size = new System.Drawing.Size(72, 21);
            this.txtLaserHeight.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "NeedlePosZ:";
            // 
            // txtNeedlePosZ
            // 
            this.txtNeedlePosZ.Location = new System.Drawing.Point(406, 161);
            this.txtNeedlePosZ.Name = "txtNeedlePosZ";
            this.txtNeedlePosZ.Size = new System.Drawing.Size(72, 21);
            this.txtNeedlePosZ.TabIndex = 25;
            // 
            // TeachNeedleToHeightSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(764, 657);
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
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
