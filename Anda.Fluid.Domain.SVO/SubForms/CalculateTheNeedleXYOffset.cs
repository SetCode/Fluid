using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal class CalculateTheNeedleXYOffset : TeachFormBase, IClickable
    {
        private PictureBox picDiagram;
        private CameraControl cameraControl;
        private int valveNo;
        private int flag = 0;
        private bool case2IsDone = false;
        private double dot2TotalOffset, dot3TotalOffset, dot4TotalOffset;

        #region
        private System.Windows.Forms.GroupBox grpVerification;
        private System.Windows.Forms.Label lblmm2;
        private System.Windows.Forms.TextBox txtDot2Offset;
        private System.Windows.Forms.Label lblDot2;
        private System.Windows.Forms.RadioButton rdoFail;
        private System.Windows.Forms.RadioButton rdoOK;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblmm;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label lblmm4;
        private System.Windows.Forms.TextBox txtDot4Offset;
        private System.Windows.Forms.Label lblDot4;
        private System.Windows.Forms.Label lblmm3;
        private System.Windows.Forms.TextBox txtDot3Offset;
        private System.Windows.Forms.Label lblDot3;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Button btnEditDot;
        private System.Windows.Forms.ComboBox cbxDotStyle;
        #endregion

        private DotParam dotParam = null;
        private Motion.ValveControl valveControl1;
        private DotStyle dotStyle = DotStyle.TYPE_1;
        private Label label3;
        private Label label2;
        private NumericUpDown nudSprayTime;
        private Label label1;
        private Button btnSrcImg;


        #region 语言文本切换字符串变量
        private string[] lblTip = new string[14]
            {
                "Place substrate and prepare the machine move.",
                "Use Camera:Teach center point of substrate.",
                "激光异常",
                "Dispensing reference dot 1...",
                "Dispensing reference dot 2...",
                "Dispensing reference dot 3...",
                "Dispensing reference dot 4...",
                "Task has done,press [Next]",
                "Align camera on dot1 and press [Teach]",
                "Align camera on dot2 and press [Teach]",
                "Align camera on dot3 and press [Teach]",
                "Align camera on dot4 and press [Teach]",
                "Press [Done] to accept results",
                "清洗运动到位异常"
            };
        #endregion
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

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            for (int i = 0; i < lblTip.Length; i++)
            {
                this.SaveKeyValueToResources(string.Format("Tip{0}", i), lblTip[i]);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }
        public CalculateTheNeedleXYOffset(int valveNo)
        {
            this.valveNo = valveNo;
            InitializeComponent();
            this.valveControl1.Setup(valveNo);
            if (this.valveNo == 1)
            {
                this.picDiagram.Image = Properties.Resources.CalculateTheNeedleXYOffset;
            }
            else
            {
                this.picDiagram.Image = Properties.Resources.CalculateTheNeedle2XYOffset;
            }

            UpdateByFlag();

            for (int i = 0; i < 10; i++)
            {
                cbxDotStyle.Items.Add("Type " + (i + 1));
            }
            cbxDotStyle.SelectedIndex = 9;
            dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(dotStyle);
            this.ReadLanguageResources();
			this.FormClosed += CalculateTheNeedleXYOffset_FormClosed;
        }

        private void CalculateTheNeedleXYOffset_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.picDiagram.Image.Dispose();
            this.Dispose(true);
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private CalculateTheNeedleXYOffset()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }
        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            if (this.valveNo == 1)
            {
                if (DataSetting.Default.DoneStepCount <= 10)
                {
                    DataSetting.Default.DoneStepCount = 10;
                }
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 10;
                DataSetting.Save();
                Machine.Instance.Robot.SaveCalibPrm();

                StepStateMgr.Instance.FindBy(9).IsDone = true;
                StepStateMgr.Instance.FindBy(9).IsChecked();
            }
            else
            {
                if (DataSetting.Default.DoneStepCount <= 14)
                {
                    DataSetting.Default.DoneStepCount = 14;
                }
                Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
                Machine.Instance.Robot.CalibPrm.SavedItem = 14;
                DataSetting.Save();
                Machine.Instance.Robot.SaveCalibPrm();

                StepStateMgr.Instance.FindBy(13).IsDone = true;
                StepStateMgr.Instance.FindBy(13).IsChecked();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoHelp()
        {

        }

        public void DoNext()
        {
            flag++;
            UpdateByFlag();
        }

        public void DoPrev()
        {
            if (flag > 3)
            {
                flag = 3;
            }
            else
            {
                flag--;
            }
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
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.picDiagram);
                    this.picDiagram.Show();

                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = this.lblTip[0];
                    break;
                case 1:
                    Task.Factory.StartNew(new Action(() =>
                    {
                        //抬起到SafeZ
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        //移动到默认平台中心点                
                        Machine.Instance.Robot.ManualMovePosXYAndReply(DataSetting.Default.SubstrateCenter);

                        if (Machine.Instance.Laser.Laserable.Vendor == Laser.Vendor.Disable)
                        {
                            Machine.Instance.Robot.MovePosZAndReply(DataSetting.Default.NeedleJetZ);
                            MessageBox.Show("此时的Z轴高度为打胶时的参考高度");
                        }
                    }));

                    case2IsDone = false;
                    //
                    this.pnlDisplay.Controls.Clear();
                    this.pnlDisplay.Controls.Add(this.cameraControl);
                    this.cameraControl.Dock = DockStyle.Fill;
                    this.cameraControl.Show();


                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTitle.Text = this.lblTip[1];
                    break;
                case 2:
                    if (case2IsDone == false)
                    {
                        //重新赋值平台中心点
                        DataSetting.Default.SubstrateCenter = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);

                        if (Machine.Instance.Laser.Laserable.Vendor == Laser.Vendor.Disable)
                        {
                            DataSetting.Default.NeedleJetZ = Machine.Instance.Robot.PosZ;
                        }

                    }
                    else
                    {

                    }
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnEditDot.Enabled = false;
                    this.cbxDotStyle.Enabled = false;

                    this.dotStyle = (DotStyle)this.cbxDotStyle.SelectedIndex;
                    this.dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(dotStyle);

                    Task.Factory.StartNew(() =>
                    {
                        double z1 = 0;
                        double z2 = 0;
                        double z3 = 0;
                        double z4 = 0;
                        if (Machine.Instance.Laser.Laserable.Vendor != Laser.Vendor.Disable)
                        {
                            int rst;
                            //关闭光源
                            Machine.Instance.Light.None();

                            Machine.Instance.MeasureHeightBefore();

                            //去点1处测高
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                                DataSetting.Default.SubstrateCenter.Y + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                            double height1;
                            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height1);
                            if (rst != 0)
                            {
                                MessageBox.Show(this.lblTip[2]);
                                return;
                            }
                            z1 = Machine.Instance.Robot.CalibPrm.StandardZ + (height1 - Machine.Instance.Robot.CalibPrm.StandardHeight);

                            //去点2处测高
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                                DataSetting.Default.SubstrateCenter.Y + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                            double height2;
                            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height2);

                            if (rst != 0)
                            {
                                MessageBox.Show(this.lblTip[2]);
                                return;
                            }
                            z2 = Machine.Instance.Robot.CalibPrm.StandardZ + (height2 - Machine.Instance.Robot.CalibPrm.StandardHeight);

                            //去点3处测高
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                                DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                            double height3;
                            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height3);
                            if (rst != 0)
                            {
                                MessageBox.Show(this.lblTip[2]);
                                return;
                            }
                            z3 = Machine.Instance.Robot.CalibPrm.StandardZ + (height3 - Machine.Instance.Robot.CalibPrm.StandardHeight);

                            //去点4处测高
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                                DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                            double height4;
                            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height4);

                            if (rst != 0)
                            {
                                MessageBox.Show(this.lblTip[2]);
                                return;
                            }
                            z4 = Machine.Instance.Robot.CalibPrm.StandardZ + (height4 - Machine.Instance.Robot.CalibPrm.StandardHeight);

                            //打开光源
                            Machine.Instance.Light.ResetToLast();
                            Machine.Instance.MeasureHeightAfter();
                        }
                        else
                        {
                            z1 = DataSetting.Default.NeedleJetZ;
                        }

                        //去清洗位置洗喷嘴
                        Result result;

                        Machine.Instance.Robot.MoveSafeZAndReply();
                        if (this.valveNo == 1)
                        {
                            result = Machine.Instance.Valve1.DoPurgeAndPrime();
                        }
                        else
                        {
                            result = Machine.Instance.Valve2.DoPurgeAndPrime();
                        }
                        if (!result.IsOk)
                        {
                            MessageBox.Show(lblTip[13]);
                            return;
                        }
                        this.BeginInvoke(new Action(() =>
                        {
                            //到点胶位置1点胶
                            this.lblTitle.Text = this.lblTip[3];
                        }));
                        //抬起到SafeZ
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        if (this.valveNo == 1)
                        {
                            //移动到点胶位置1
                            PointD p1 = (DataSetting.Default.SubstrateCenter + new PointD(-5, 5)).ByNeedleCamera(ValveType.Valve1);
                            Machine.Instance.Robot.ManualMovePosXYAndReply(p1);
                            //点胶
                            this.DotByParam(1, z1);
                        }
                        else
                        {
                            //移动到点胶位置1
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X - 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                                DataSetting.Default.SubstrateCenter.Y + 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);
                            //点胶
                            this.DotByParam(2, z1);
                        }


                        //到点胶位置2点胶
                        this.BeginInvoke(new Action(() =>
                        {
                            this.lblTitle.Text = this.lblTip[4];
                        }));
                        //抬起到SafeZ
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        if (this.valveNo == 1)
                        {
                            //移动到点胶位置2
                            PointD p2 = (DataSetting.Default.SubstrateCenter + new PointD(5, 5)).ByNeedleCamera(Drive.ValveSystem.ValveType.Valve1);
                            Machine.Instance.Robot.ManualMovePosXYAndReply(p2);
                            //点胶
                            this.DotByParam(1, z1);
                        }
                        else
                        {
                            //移动到点胶位置2
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                                DataSetting.Default.SubstrateCenter.Y + 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);
                            //点胶
                            this.DotByParam(2, z1);
                        }

                        //到点胶位置3点胶
                        this.BeginInvoke(new Action(() =>
                        {
                            this.lblTitle.Text = this.lblTip[5];
                        }));
                        //抬起到SafeZ
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        if (this.valveNo == 1)
                        {
                            //移动到点胶位置3
                            PointD p3 = (DataSetting.Default.SubstrateCenter + new PointD(-5, -5)).ByNeedleCamera(Drive.ValveSystem.ValveType.Valve1);
                            Machine.Instance.Robot.ManualMovePosXYAndReply(p3);
                            //点胶
                            this.DotByParam(1, z1);
                        }
                        else
                        {
                            //移动到点胶位置3
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X - 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                                DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);
                            //点胶
                            this.DotByParam(2, z1);
                        }


                        //到点胶位置4点胶
                        this.BeginInvoke(new Action(() =>
                        {
                            this.lblTitle.Text = this.lblTip[6];
                        }));
                        //抬起到SafeZ
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        if (this.valveNo == 1)
                        {
                            //移动到点胶位置4
                            PointD p4 = (DataSetting.Default.SubstrateCenter + new PointD(5, -5)).ByNeedleCamera(Drive.ValveSystem.ValveType.Valve1);
                            Machine.Instance.Robot.ManualMovePosXYAndReply(p4);
                            //点胶
                            this.DotByParam(1, z1);
                        }
                        else
                        {
                            //移动到点胶位置4
                            Machine.Instance.Robot.ManualMovePosXYAndReply(
                                DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                                DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y);
                            //点胶
                            this.DotByParam(2, z1);
                        }


                        //四点点胶完成
                        this.BeginInvoke(new Action(() =>
                        {
                            this.lblTitle.Text = this.lblTip[7];
                            this.btnPrev.Enabled = true;
                            this.btnNext.Enabled = true;
                            this.btnTeach.Enabled = false;
                            this.btnDone.Enabled = false;
                            this.btnEditDot.Enabled = true;
                            this.cbxDotStyle.Enabled = true;
                        }));
                    });
                    break;
                case 3:
                    case2IsDone = true;
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //移动到点1附近
                    PointD p = (DataSetting.Default.SubstrateCenter + new PointD(-5, 5)).ByNeedleJet(Drive.ValveSystem.ValveType.Valve1);
                    Machine.Instance.Robot.ManualMovePosXY(p);

                    this.lblTitle.Text = this.lblTip[8];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.txtDot2Offset.BackColor = Color.White;
                    this.txtDot3Offset.BackColor = Color.White;
                    this.txtDot4Offset.BackColor = Color.White;

                    break;
                case 4:
                    //计算点胶阀中心和真实点胶中心的差值
                    if (this.valveNo == 1)
                    {
                        Machine.Instance.Robot.CalibPrm.NeedleJet1 =
                            (Machine.Instance.Robot.PosXY.ToSystem() - (DataSetting.Default.SubstrateCenter + new PointD(-5, 5)).ToSystem()).ToPoint();
                    }
                    else
                    {
                        Machine.Instance.Robot.CalibPrm.NeedleJet2 = new PointD(
                        Machine.Instance.Robot.PosX - (DataSetting.Default.SubstrateCenter.X - 5),
                        Machine.Instance.Robot.PosY - (DataSetting.Default.SubstrateCenter.Y + 5));
                    }

                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //移动到点2附近
                    p = (DataSetting.Default.SubstrateCenter + new PointD(5, 5)).ByNeedleJet(Drive.ValveSystem.ValveType.Valve1);
                    Machine.Instance.Robot.ManualMovePosXY(p);

                    this.lblTitle.Text = this.lblTip[9];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 5:
                    //计算修正后，在点2时点胶阀中心和实际点胶中心的差值
                    PointD dot2Offset = new PointD();
                    if (this.valveNo == 1)
                    {
                        dot2Offset = (Machine.Instance.Robot.PosXY.ToSystem() - ((DataSetting.Default.SubstrateCenter + new PointD(5, 5)).ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleJet1)).ToPoint();
                    }
                    else
                    {
                        dot2Offset.X = Machine.Instance.Robot.PosX - (DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.X);
                        dot2Offset.Y = Machine.Instance.Robot.PosY - (DataSetting.Default.SubstrateCenter.Y + 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);
                    }
                    dot2TotalOffset = Math.Sqrt(Math.Pow(dot2Offset.X, 2) + Math.Pow(dot2Offset.Y, 2));
                    this.txtDot2Offset.Text = dot2TotalOffset.ToString();

                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //移动到点3附近
                    p = (DataSetting.Default.SubstrateCenter + new PointD(-5, -5)).ByNeedleJet(Drive.ValveSystem.ValveType.Valve1);
                    Machine.Instance.Robot.ManualMovePosXY(p);

                    this.lblTitle.Text = this.lblTip[10];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 6:
                    //计算修正后，在点3时点胶阀中心和实际点胶中心的差值
                    PointD dot3Offset = new PointD();
                    if (this.valveNo == 1)
                    {
                        dot3Offset = (Machine.Instance.Robot.PosXY.ToSystem() - ((DataSetting.Default.SubstrateCenter + new PointD(-5, -5)).ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleJet1)).ToPoint();
                    }
                    else
                    {
                        dot3Offset.X = Machine.Instance.Robot.PosX - (DataSetting.Default.SubstrateCenter.X - 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.X);
                        dot3Offset.Y = Machine.Instance.Robot.PosY - (DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);
                    }
                    dot3TotalOffset = Math.Pow((Math.Pow(dot3Offset.X, 2) + Math.Pow(dot3Offset.Y, 2)), 0.5);
                    this.txtDot3Offset.Text = dot3TotalOffset.ToString();

                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //移动到点4附近
                    p = (DataSetting.Default.SubstrateCenter + new PointD(5, -5)).ByNeedleJet(Drive.ValveSystem.ValveType.Valve1);
                    Machine.Instance.Robot.ManualMovePosXY(p);

                    this.lblTitle.Text = this.lblTip[11];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.rdoOK.Checked = false;
                    this.rdoFail.Checked = false;
                    this.rdoOK.Enabled = false;
                    this.rdoFail.Enabled = false;
                    break;
                case 7:
                    //抬起到SafeZ
                    Machine.Instance.Robot.MoveSafeZAndReply();

                    //计算修正后，在点4时点胶阀中心和实际点胶中心的差值
                    PointD dot4Offset = new PointD();
                    if (this.valveNo == 1)
                    {
                        dot4Offset = (Machine.Instance.Robot.PosXY.ToSystem() - ((DataSetting.Default.SubstrateCenter + new PointD(5, -5)).ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleJet1)).ToPoint();
                    }
                    else
                    {
                        dot4Offset.X = Machine.Instance.Robot.PosX - (DataSetting.Default.SubstrateCenter.X + 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.X);
                        dot4Offset.Y = Machine.Instance.Robot.PosY - (DataSetting.Default.SubstrateCenter.Y - 5 + Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);
                    }
                    dot4TotalOffset = Math.Pow((Math.Pow(dot4Offset.X, 2) + Math.Pow(dot4Offset.Y, 2)), 0.5);
                    this.txtDot4Offset.Text = dot4TotalOffset.ToString();

                    if (dot2TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot2Offset.BackColor = Color.DarkRed;
                    }
                    if (dot3TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot3Offset.BackColor = Color.DarkRed;
                    }
                    if (dot4TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot4Offset.BackColor = Color.DarkRed;
                    }
                    if ((dot2TotalOffset <= Convert.ToDouble(this.txtTolerance.Text))
                        && (dot3TotalOffset <= Convert.ToDouble(this.txtTolerance.Text))
                            && (dot4TotalOffset <= Convert.ToDouble(this.txtTolerance.Text)))
                    {
                        this.rdoOK.Checked = true;
                        this.rdoOK.Enabled = true;
                        this.txtDot2Offset.BackColor = Color.White;
                        this.txtDot3Offset.BackColor = Color.White;
                        this.txtDot4Offset.BackColor = Color.White;
                    }
                    this.lblTitle.Text = this.lblTip[12];
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    break;

            }
        }

        private void btnEditDot_Click(object sender, EventArgs e)
        {
            new EditDotParamsForm(FluidProgram.CurrentOrDefault().ProgramSettings.DotParamList).ShowDialog();
        }

        private void btnSrcImg_Click(object sender, EventArgs e)
        {
            this.cameraControl.SwitchSizeMode();
        }

        private Result DotByParam(int vavelNo, double z)
        {
            // 下降到指定高度
            Result ret = Machine.Instance.Robot.MovePosZAndReply(z + dotParam.DispenseGap, dotParam.DownSpeed, dotParam.DownAccel);
            if (!ret.IsOk)
            {
                return ret;
            }
            Thread.Sleep(TimeSpan.FromSeconds(dotParam.SettlingTime));

            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                if (dotParam.MultiShotDelta > 0)
                {
                    // 开始喷胶
                    for (int i = 0; i < dotParam.NumShots; i++)
                    {

                        // 喷射一滴胶水
                        if (vavelNo == 1)
                        {
                            Machine.Instance.Valve1.SprayOneAndWait();
                        }
                        else
                        {
                            Machine.Instance.Valve2.SprayOneAndWait();
                        }

                        DateTime sprayEnd = DateTime.Now;

                        // 非最后一滴胶水，抬高一段距离 Multi-shot Delta
                        if (dotParam.MultiShotDelta > 0 && i != dotParam.NumShots - 1)
                        {
                            ret = Machine.Instance.Robot.MoveIncZAndReply(dotParam.MultiShotDelta, dotParam.RetractSpeed, dotParam.RetractAccel);
                            if (!ret.IsOk)
                            {
                                return ret;
                            }
                        }

                        // 等待一段时间 Dwell Time
                        double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                        double realDwellTime = dotParam.DwellTime - ellapsed;
                        if (realDwellTime > 0)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                        }
                    }
                }
                else
                {
                    if (vavelNo == 1)
                    {
                        Machine.Instance.Valve1.SprayCycleAndWait((short)dotParam.NumShots);
                    }
                    else
                    {
                        Machine.Instance.Valve2.SprayCycleAndWait((short)dotParam.NumShots);
                    }

                    DateTime sprayEnd = DateTime.Now;

                    // 等待一段时间 Dwell Time
                    double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                    double realDwellTime = dotParam.DwellTime - ellapsed;
                    if (realDwellTime > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                    }
                }
            }
            else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                if (vavelNo == 1)
                {
                    Machine.Instance.Valve1.SprayOneAndWait((int)this.nudSprayTime.Value);
                }
                else
                {
                    Machine.Instance.Valve2.SprayOneAndWait((int)this.nudSprayTime.Value);
                }

                // 等待一段时间 Dwell Time
                DateTime sprayEnd = DateTime.Now;
                double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                double realDwellTime = dotParam.DwellTime - ellapsed;
                if (realDwellTime > 0)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                }
            }


            // 抬高一段距离 Retract Distance
            if (dotParam.RetractDistance > 0)
            {
                ret = Machine.Instance.Robot.MoveIncZAndReply(dotParam.RetractDistance, dotParam.RetractSpeed, dotParam.RetractAccel);
            }

            return ret;
        }

        #region 初始化控件
        private void InitializeComponent()
        {
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.grpVerification = new System.Windows.Forms.GroupBox();
            this.lblmm4 = new System.Windows.Forms.Label();
            this.txtDot4Offset = new System.Windows.Forms.TextBox();
            this.lblDot4 = new System.Windows.Forms.Label();
            this.lblmm3 = new System.Windows.Forms.Label();
            this.txtDot3Offset = new System.Windows.Forms.TextBox();
            this.lblDot3 = new System.Windows.Forms.Label();
            this.lblmm2 = new System.Windows.Forms.Label();
            this.txtDot2Offset = new System.Windows.Forms.TextBox();
            this.lblDot2 = new System.Windows.Forms.Label();
            this.rdoFail = new System.Windows.Forms.RadioButton();
            this.rdoOK = new System.Windows.Forms.RadioButton();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblmm = new System.Windows.Forms.Label();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.cbxDotStyle = new System.Windows.Forms.ComboBox();
            this.btnEditDot = new System.Windows.Forms.Button();
            this.valveControl1 = new Anda.Fluid.Domain.Motion.ValveControl();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSprayTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.btnSrcImg = new System.Windows.Forms.Button();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            this.grpVerification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.label3);
            this.grpResultTest.Controls.Add(this.label2);
            this.grpResultTest.Controls.Add(this.nudSprayTime);
            this.grpResultTest.Controls.Add(this.label1);
            this.grpResultTest.Controls.Add(this.valveControl1);
            this.grpResultTest.Controls.Add(this.btnEditDot);
            this.grpResultTest.Controls.Add(this.cbxDotStyle);
            this.grpResultTest.Controls.Add(this.grpVerification);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.picDiagram);
            // 
            // cameraControl
            // 
            this.cameraControl.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 0;
            // 
            // grpVerification
            // 
            this.grpVerification.Controls.Add(this.lblmm4);
            this.grpVerification.Controls.Add(this.txtDot4Offset);
            this.grpVerification.Controls.Add(this.lblDot4);
            this.grpVerification.Controls.Add(this.lblmm3);
            this.grpVerification.Controls.Add(this.txtDot3Offset);
            this.grpVerification.Controls.Add(this.lblDot3);
            this.grpVerification.Controls.Add(this.lblmm2);
            this.grpVerification.Controls.Add(this.txtDot2Offset);
            this.grpVerification.Controls.Add(this.lblDot2);
            this.grpVerification.Controls.Add(this.rdoFail);
            this.grpVerification.Controls.Add(this.rdoOK);
            this.grpVerification.Controls.Add(this.lblResult);
            this.grpVerification.Controls.Add(this.lblmm);
            this.grpVerification.Controls.Add(this.txtTolerance);
            this.grpVerification.Controls.Add(this.lblTolerance);
            this.grpVerification.Location = new System.Drawing.Point(7, 20);
            this.grpVerification.Name = "grpVerification";
            this.grpVerification.Size = new System.Drawing.Size(184, 172);
            this.grpVerification.TabIndex = 0;
            this.grpVerification.TabStop = false;
            this.grpVerification.Text = "Verification";
            // 
            // lblmm4
            // 
            this.lblmm4.AutoSize = true;
            this.lblmm4.Location = new System.Drawing.Point(161, 116);
            this.lblmm4.Name = "lblmm4";
            this.lblmm4.Size = new System.Drawing.Size(17, 12);
            this.lblmm4.TabIndex = 15;
            this.lblmm4.Text = "mm";
            // 
            // txtDot4Offset
            // 
            this.txtDot4Offset.Enabled = false;
            this.txtDot4Offset.Location = new System.Drawing.Point(101, 112);
            this.txtDot4Offset.Name = "txtDot4Offset";
            this.txtDot4Offset.Size = new System.Drawing.Size(55, 21);
            this.txtDot4Offset.TabIndex = 14;
            // 
            // lblDot4
            // 
            this.lblDot4.AutoSize = true;
            this.lblDot4.Location = new System.Drawing.Point(6, 116);
            this.lblDot4.Name = "lblDot4";
            this.lblDot4.Size = new System.Drawing.Size(77, 12);
            this.lblDot4.TabIndex = 13;
            this.lblDot4.Text = "Dot4 Offset:";
            // 
            // lblmm3
            // 
            this.lblmm3.AutoSize = true;
            this.lblmm3.Location = new System.Drawing.Point(161, 87);
            this.lblmm3.Name = "lblmm3";
            this.lblmm3.Size = new System.Drawing.Size(17, 12);
            this.lblmm3.TabIndex = 12;
            this.lblmm3.Text = "mm";
            // 
            // txtDot3Offset
            // 
            this.txtDot3Offset.Enabled = false;
            this.txtDot3Offset.Location = new System.Drawing.Point(101, 83);
            this.txtDot3Offset.Name = "txtDot3Offset";
            this.txtDot3Offset.Size = new System.Drawing.Size(55, 21);
            this.txtDot3Offset.TabIndex = 11;
            // 
            // lblDot3
            // 
            this.lblDot3.AutoSize = true;
            this.lblDot3.Location = new System.Drawing.Point(6, 87);
            this.lblDot3.Name = "lblDot3";
            this.lblDot3.Size = new System.Drawing.Size(77, 12);
            this.lblDot3.TabIndex = 10;
            this.lblDot3.Text = "Dot3 Offset:";
            // 
            // lblmm2
            // 
            this.lblmm2.AutoSize = true;
            this.lblmm2.Location = new System.Drawing.Point(161, 56);
            this.lblmm2.Name = "lblmm2";
            this.lblmm2.Size = new System.Drawing.Size(17, 12);
            this.lblmm2.TabIndex = 9;
            this.lblmm2.Text = "mm";
            // 
            // txtDot2Offset
            // 
            this.txtDot2Offset.Enabled = false;
            this.txtDot2Offset.Location = new System.Drawing.Point(101, 52);
            this.txtDot2Offset.Name = "txtDot2Offset";
            this.txtDot2Offset.Size = new System.Drawing.Size(55, 21);
            this.txtDot2Offset.TabIndex = 8;
            // 
            // lblDot2
            // 
            this.lblDot2.AutoSize = true;
            this.lblDot2.Location = new System.Drawing.Point(6, 58);
            this.lblDot2.Name = "lblDot2";
            this.lblDot2.Size = new System.Drawing.Size(77, 12);
            this.lblDot2.TabIndex = 7;
            this.lblDot2.Text = "Dot2 Offset:";
            // 
            // rdoFail
            // 
            this.rdoFail.AutoSize = true;
            this.rdoFail.Enabled = false;
            this.rdoFail.Location = new System.Drawing.Point(122, 144);
            this.rdoFail.Name = "rdoFail";
            this.rdoFail.Size = new System.Drawing.Size(47, 16);
            this.rdoFail.TabIndex = 6;
            this.rdoFail.TabStop = true;
            this.rdoFail.Text = "Fail";
            this.rdoFail.UseVisualStyleBackColor = true;
            // 
            // rdoOK
            // 
            this.rdoOK.AutoSize = true;
            this.rdoOK.Enabled = false;
            this.rdoOK.Location = new System.Drawing.Point(65, 144);
            this.rdoOK.Name = "rdoOK";
            this.rdoOK.Size = new System.Drawing.Size(35, 16);
            this.rdoOK.TabIndex = 5;
            this.rdoOK.TabStop = true;
            this.rdoOK.Text = "OK";
            this.rdoOK.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(6, 145);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(47, 12);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Result:";
            // 
            // lblmm
            // 
            this.lblmm.AutoSize = true;
            this.lblmm.Location = new System.Drawing.Point(161, 29);
            this.lblmm.Name = "lblmm";
            this.lblmm.Size = new System.Drawing.Size(17, 12);
            this.lblmm.TabIndex = 2;
            this.lblmm.Text = "mm";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(101, 25);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(56, 21);
            this.txtTolerance.TabIndex = 1;
            this.txtTolerance.Text = "0.02";
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(6, 29);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(89, 12);
            this.lblTolerance.TabIndex = 0;
            this.lblTolerance.Text = "Tolerance:(±)";
            // 
            // cbxDotStyle
            // 
            this.cbxDotStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDotStyle.FormattingEnabled = true;
            this.cbxDotStyle.Location = new System.Drawing.Point(346, 59);
            this.cbxDotStyle.Name = "cbxDotStyle";
            this.cbxDotStyle.Size = new System.Drawing.Size(70, 20);
            this.cbxDotStyle.TabIndex = 1;
            // 
            // btnEditDot
            // 
            this.btnEditDot.Location = new System.Drawing.Point(422, 57);
            this.btnEditDot.Name = "btnEditDot";
            this.btnEditDot.Size = new System.Drawing.Size(75, 23);
            this.btnEditDot.TabIndex = 2;
            this.btnEditDot.Text = "edit";
            this.btnEditDot.UseVisualStyleBackColor = true;
            this.btnEditDot.Click += new System.EventHandler(this.btnEditDot_Click);
            // 
            // valveControl1
            // 
            this.valveControl1.Location = new System.Drawing.Point(346, 94);
            this.valveControl1.Name = "valveControl1";
            this.valveControl1.Size = new System.Drawing.Size(123, 54);
            this.valveControl1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "非喷射型阀出胶时间设置:";
            // 
            // nudSprayTime
            // 
            this.nudSprayTime.Location = new System.Drawing.Point(346, 23);
            this.nudSprayTime.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudSprayTime.Name = "nudSprayTime";
            this.nudSprayTime.Size = new System.Drawing.Size(84, 21);
            this.nudSprayTime.TabIndex = 5;
            this.nudSprayTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "点胶参数设置:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "手动出胶:";
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(0, 0);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(499, 395);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 7;
            this.picDiagram.TabStop = false;
            // 
            // btnSrcImg
            // 
            this.btnSrcImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrcImg.Location = new System.Drawing.Point(593, 239);
            this.btnSrcImg.Name = "btnSrcImg";
            this.btnSrcImg.Size = new System.Drawing.Size(48, 23);
            this.btnSrcImg.TabIndex = 25;
            this.btnSrcImg.Text = "原图";
            this.btnSrcImg.UseVisualStyleBackColor = true;
            this.btnSrcImg.Click += new System.EventHandler(this.btnSrcImg_Click);
            // 
            // CalculateTheNeedleXYOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(723, 657);
            this.Controls.Add(this.btnSrcImg);
            this.Name = "CalculateTheNeedleXYOffset";
            this.Text = "Anda Fluid - Calculate the needle XY offset";
            this.Controls.SetChildIndex(this.grpOperation, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.grpResultTest, 0);
            this.Controls.SetChildIndex(this.btnSrcImg, 0);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.grpVerification.ResumeLayout(false);
            this.grpVerification.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
