using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogNeedleAngleWithPlasticene2 : JogFormBase
    {
        #region Fields
        private int step = 0;
        private PointD needlePos = new PointD();
        private double needlePosZPlasticene = 0;
    

        private PointD p1;
        private PointD p2;
        private PointD p3;
        private PointD p4;

        private double angleGap = 0;
        private double angleRotated = 0;

        public double moveVel = 5;
        public double moveAcc = 1;

        //private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private Inspection inspection;
        #endregion 

        public DialogNeedleAngleWithPlasticene2()
        {
            InitializeComponent();


            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterParent;

            this.txtPokeX.ReadOnly = true;
            this.txtPokeY.ReadOnly = true;
            this.txtPokeZ.ReadOnly = true;

            this.txtP1X.ReadOnly = true;
            this.txtP1Y.ReadOnly = true;
            this.txtP2X.ReadOnly = true;
            this.txtP2Y.ReadOnly = true;
            this.txtP3X.ReadOnly = true;
            this.txtP3Y.ReadOnly = true;
            this.txtP4X.ReadOnly = true;
            this.txtP4Y.ReadOnly = true;

            this.txtRotated.ReadOnly = true;
            this.txtGapAngle.ReadOnly = true;

            this.nudZSpeed.Minimum = 1;
            this.nudZSpeed.Maximum = 10;
            this.nudZSpeed.DecimalPlaces = 1;
            this.nudZSpeed.Increment = 1;
            this.nudZSpeed.Value = 5;

            this.nudOffsetX.Minimum = 1;
            this.nudOffsetX.Maximum = 20;
            this.nudOffsetX.DecimalPlaces = 3;
            this.nudOffsetX.Increment = (decimal)0.01;
            this.nudOffsetX.Value = 5;

            this.nudPosZ.Minimum = -100;
            this.nudPosZ.Maximum = 2;
            this.nudPosZ.DecimalPlaces = 3;
            this.nudPosZ.Increment = (decimal)0.01;
            this.nudPosZ.Value = (decimal)NeedleCalibrationSetting2.Default.PosZ;

            this.ReadLanguageResources();
        }
        public DialogNeedleAngleWithPlasticene2 Setup()
        {
            this.needlePos = Machine.Instance.Robot.CalibPrm.NeedlePosition == null ? new PointD(0, 0) : Machine.Instance.Robot.CalibPrm.NeedlePosition;
            this.needlePosZPlasticene = Machine.Instance.Robot.CalibPrm.NeedlePosZPlasticene;
           
            this.p1 = new PointD(Properties.Settings.Default.needleP1X, Properties.Settings.Default.needleP1Y);
            this.p2 = new PointD(Properties.Settings.Default.needleP2X, Properties.Settings.Default.needleP2Y);
            this.p3 = new PointD(Properties.Settings.Default.needleP3X, Properties.Settings.Default.needleP3Y);
            this.p4 = new PointD(Properties.Settings.Default.needleP4X, Properties.Settings.Default.needleP4Y);

            this.updatePos();
            return this;
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {

            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.step > 0)
            {
                this.step--;
                this.UpdateByFlag();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.step < 9)
            {
                this.step++;
                this.UpdateByFlag();
            }
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.teachPosition();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.CalibPrm.NeedlePosition = this.needlePos;
            Machine.Instance.Robot.CalibPrm.NeedlePosZPlasticene = this.needlePosZPlasticene;
            Machine.Instance.Robot.CalibPrm.NeedleRotated = this.angleRotated;
            Machine.Instance.Robot.CalibPrm.AngleGap = this.angleGap;

            Machine.Instance.Robot.CalibPrm.Direct = this.ckbReverse.Checked ? -1 : 1;
          
            Machine.Instance.Robot.SaveCalibPrm();

            Properties.Settings.Default.needleP1X = this.p1.X;
            Properties.Settings.Default.needleP1Y = this.p1.Y;
            Properties.Settings.Default.needleP2X = this.p2.X;
            Properties.Settings.Default.needleP2Y = this.p2.Y;
            Properties.Settings.Default.needleP3X = this.p3.X;
            Properties.Settings.Default.needleP3Y = this.p3.Y;
            Properties.Settings.Default.needleP4X = this.p4.X;
            Properties.Settings.Default.needleP4Y = this.p4.Y;

            NeedleCalibrationSetting.Save();
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }
        private PointD poke = new PointD();
        private PointD pokeOffset = new PointD();
        private PointD pokeOffsetPicked = new PointD();
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateByFlag()
        {
            switch (this.step)
            {
                case 0:
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTest.Enabled = false;

                    this.btnTeachP1.Enabled = false;
                    this.btnGotoP1.Enabled = false;
                    this.btnTeachP2.Enabled = false;
                    this.btnGotoP2.Enabled = false;
                    this.btnTeachP3.Enabled = false;
                    this.btnGotoP3.Enabled = false;
                    this.btnTeachP4.Enabled = false;
                    this.btnGotoP4.Enabled = false;
                    this.lblTips.Text = "点击下一步开始校准";
                    break;
                case 1:
                    //移动到默认位置
                    Machine.Instance.Robot.MoveSafeZ();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(NeedleCalibrationSetting2.Default.PokePos);

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTips.Text = "请示检测点，示教完成后点击下一步";
                    break;
                case 2:
                    this.lblTips.Text = "请示教同心度检测Z轴高度，示教完成后点击下一步";
                    Machine.Instance.Robot.MoveSafeZ();
                    this.poke = NeedleCalibrationSetting2.Default.PokePos.ToNeedle(Drive.ValveSystem.ValveType.Valve1);
                    Machine.Instance.Robot.ManualMovePosXY(this.poke);
                    Machine.Instance.Robot.MovePosZAndReply((double)this.nudPosZ.Value, (double)this.nudZSpeed.Value);
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 3:
                    //移动到戳橡皮泥点
                    this.MovePokeOffset();
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 4:
                    //确保针嘴的垂直
                    Machine.Instance.Robot.MoveSafeZAndReply();                   
                    Machine.Instance.Robot.ManualMovePosXYAndReply(this.pokeOffset);

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 5:
                    //确保针嘴的垂直
                    this.checkVerticality();
                    break;
                case 6:
                    //移动到默认位置
                    Machine.Instance.Robot.MoveSafeZ();
                    Machine.Instance.Robot.ManualMovePosXY(this.needlePos);
                    //if (Machine.Instance.Robot.AxisR!=null)
                    //{
                    //    Machine.Instance.Robot.MovePosRAndReply(0);
                    //}
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    this.lblTips.Text = "请示教戳橡皮泥点，示教完成后点击下一步";
                    break;
                case 7:
                    this.lblTips.Text = "请示教戳橡皮泥Z轴高度，示教完成后点击下一步";
                    Machine.Instance.Robot.MoveSafeZ();
                    Machine.Instance.Robot.ManualMovePosXY(this.needlePos.ToNeedle(Drive.ValveSystem.ValveType.Valve1));
                    Machine.Instance.Robot.MovePosZAndReply(this.needlePosZPlasticene, (double)this.nudZSpeed.Value);
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 8:
                    Machine.Instance.Robot.MoveSafeZ();
                    Machine.Instance.Robot.ManualMovePosXY(this.needlePos);
                    //拾取四个点       
                    this.btnTeachP1.Enabled = true;
                    this.btnGotoP1.Enabled = true;
                    this.btnTeachP2.Enabled = true;
                    this.btnGotoP2.Enabled = true;
                    this.btnTeachP3.Enabled = true;
                    this.btnGotoP3.Enabled = true;
                    this.btnTeachP4.Enabled = true;
                    this.btnGotoP4.Enabled = true;

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.lblTips.Text = "请用相机拾取针嘴缺口在橡皮泥上的四个点";
                    break;
                case 9:
                    //执行，针嘴角度
                    this.getGapAngle();
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.lblTips.Text = "计算得到针嘴角度和R轴的角度";
                    this.btnTest.Enabled = true;
                    break;



            }
        }


        private void teachPosition()
        {
            switch (this.step)
            {

                case 1:
                    NeedleCalibrationSetting2.Default.PokePos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    this.txtTestPosX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
                    this.txtTestPosY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 2:
                    NeedleCalibrationSetting2.Default.PosZ = Machine.Instance.Robot.PosZ;
                    this.nudPosZ.Value = (decimal)Machine.Instance.Robot.PosZ;

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 6:
                    this.needlePos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    this.txtPokeX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
                    this.txtPokeY.Text = Machine.Instance.Robot.PosY.ToString("0.000");

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 7:
                    this.needlePosZPlasticene = Machine.Instance.Robot.PosZ;
                    this.txtPokeZ.Text = Machine.Instance.Robot.PosZ.ToString("0.000");

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
            }
        }
        
       
        /// <summary>
        /// 计算角度
        /// </summary>
        private void getGapAngle()
        {
            if (this.p1 == null || this.p2 == null || this.p3 == null || this.p4 == null)
            {
                MessageBox.Show("请先示教四个点");
                return;
            }
            double angle12 = MathUtils.CalculateDegree(this.p1, this.p2);
            double angle34 = MathUtils.CalculateDegree(this.p3, this.p4);
            double delta = Math.Abs(angle12 - angle34);
            if (delta > 90)
            {
                delta = 180 - delta;
            }
            if (delta > 2)
            {
                MessageBox.Show("点1和点2拾取有误");
                return;
            }
            //double dis12 = this.p1.DistanceTo(this.p2);
            //double dis13 = this.p1.DistanceTo(this.p3);
            //double dis14 = this.p1.DistanceTo(this.p4);
            //if (dis12<dis13 && dis12<dis14)
            //{
            //    MessageBox.Show("点1点2应该是长边上两点");
            //    return;
            //}
            //this.angleGap = (angle12+ angle34) / 2;
            PointD mid12 = new PointD((this.p1.X + this.p2.X) / 2, (this.p1.Y + this.p2.Y) / 2);
            PointD mid34 = new PointD((this.p3.X + this.p4.X) / 2, (this.p3.Y + this.p4.Y) / 2);
            this.angleGap = MathUtils.CalculateDegree(mid12, mid34);
            if (Machine.Instance.Robot.AxisR != null)
            {
                this.angleRotated = Machine.Instance.Robot.AxisR.Pos;
                this.txtRotated.Text = Machine.Instance.Robot.AxisR.Pos.ToString("0.000");
            }
            this.txtGapAngle.Text = this.angleGap.ToString("0.000");
        }


        private void DialogNeedleAngle_Load(object sender, EventArgs e)
        {
            NeedleCalibrationSetting.Load();
            this.inspection = InspectionMgr.Instance.FindBy(NeedleCalibrationSetting.Default.NeedleMeasurePrm.InspectionKey);
           
            this.cameraControl1.SetExposure(NeedleCalibrationSetting.Default.NeedleMeasurePrm.ExposureTime);
            this.cameraControl1.SetGain(NeedleCalibrationSetting.Default.NeedleMeasurePrm.Gain);
            this.cameraControl1.SelectLight(NeedleCalibrationSetting.Default.NeedleMeasurePrm.ExecutePrm);
          
            this.nudUpper.Increment = (decimal)0.001;
            this.nudUpper.DecimalPlaces = 3;
            this.nudLower.Increment = (decimal)0.001;
            this.nudLower.DecimalPlaces = 3;

            this.nudUpper.Value = (decimal)NeedleCalibrationSetting.Default.NeedleMeasurePrm.Upper;
            this.nudLower.Value = (decimal)NeedleCalibrationSetting.Default.NeedleMeasurePrm.Lower;

            this.UpdateByFlag();
        }

        #region 相机拾取针嘴在橡皮泥上戳的四个点

        private void btnTeachP1_Click(object sender, EventArgs e)
        {
            this.p1 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.updatePos();
        }

        private void btnGotoP1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (this.p1 == null)
                {
                    this.p1 = this.needlePos;
                }
                Machine.Instance.Robot.ManualMovePosXY(this.p1);
            });

        }

        private void btnTeachP2_Click(object sender, EventArgs e)
        {
            this.p2 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.updatePos();
        }

        private void btnGotoP2_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (this.p2 == null)
                {
                    this.p2 = this.needlePos;
                }
                Machine.Instance.Robot.ManualMovePosXY(this.p2);
            });

        }

        private void btnTeachP3_Click(object sender, EventArgs e)
        {
            this.p3 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.updatePos();
        }

        private void btnGotoP3_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (this.p3 == null)
                {
                    this.p3 = this.needlePos;
                }
                Machine.Instance.Robot.ManualMovePosXY(this.p3);
            });

        }

        private void btnTeachP4_Click(object sender, EventArgs e)
        {
            this.p4 = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.updatePos();
        }

        private void btnGotoP4_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (this.p4 == null)
                {
                    this.p4 = this.needlePos;
                }
                Machine.Instance.Robot.ManualMovePosXY(this.p4);
            });

        }

        private void updatePos()
        {
            this.txtP1X.Text = (this.p1 == null ? 0 : this.p1.X).ToString("0.000");
            this.txtP1Y.Text = (this.p1 == null ? 0 : this.p1.Y).ToString("0.000");

            this.txtP2X.Text = (this.p2 == null ? 0 : this.p2.X).ToString("0.000");
            this.txtP2Y.Text = (this.p2 == null ? 0 : this.p2.Y).ToString("0.000");

            this.txtP3X.Text = (this.p3 == null ? 0 : this.p3.X).ToString("0.000");
            this.txtP3Y.Text = (this.p3 == null ? 0 : this.p3.Y).ToString("0.000");

            this.txtP4X.Text = (this.p4 == null ? 0 : this.p4.X).ToString("0.000");
            this.txtP4Y.Text = (this.p4 == null ? 0 : this.p4.Y).ToString("0.000");

            this.txtCenterX.Text = (this.pokeOffsetPicked == null ? 0 : this.pokeOffsetPicked.X).ToString("0.000");
            this.txtCenterY.Text = (this.pokeOffsetPicked == null ? 0 : this.pokeOffsetPicked.Y).ToString("0.000");
        }



        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            double delta = 0 - this.angleGap;
            double angle = this.angleRotated + (this.ckbReverse.Checked ? -1 : 1) * delta;
            Machine.Instance.Robot.MovePosR(angle);
        }
        #region 同心度
        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.pokeOffsetPicked = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            this.updatePos();
            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;
        }

        private void btnGotoCenter_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (this.pokeOffsetPicked == null)
                {
                    this.pokeOffsetPicked =NeedleCalibrationSetting2.Default.PokePos;
                }
                Machine.Instance.Robot.ManualMovePosXY(this.pokeOffsetPicked);
            });
        }

        private void nudUpper_ValueChanged(object sender, EventArgs e)
        {
            NeedleCalibrationSetting2.Default.Upper = (double)this.nudUpper.Value;
        }

        private void nudLower_ValueChanged(object sender, EventArgs e)
        {
            NeedleCalibrationSetting2.Default.Lower = (double)this.nudLower.Value;
        }
        /// <summary>
        /// 针嘴移动到示教点+偏移量 处
        /// </summary>
        private void MovePokeOffset()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();
                this.pokeOffset = NeedleCalibrationSetting2.Default.PokePos + new PointD((double)this.nudOffsetX.Value, 0);
                Machine.Instance.Robot.ManualMovePosXYAndReply(this.pokeOffset.ToNeedle(Drive.ValveSystem.ValveType.Valve1));
                //double posR = Machine.Instance.Robot.PosR;
                ////旋转180
                //Machine.Instance.Robot.MovePosRAndReply(posR+180);
                Machine.Instance.Robot.MovePosZAndReply(NeedleCalibrationSetting2.Default.PosZ, (double)this.nudZSpeed.Value);

            }));
        }

        /// <summary>
        /// 检测同心度
        /// </summary>
        private void checkVerticality()
        {
            if (this.pokeOffset==null || this.pokeOffsetPicked==null)
            {
                MessageBox.Show("请先示教戳橡皮泥位置");
                return;
            }
            double result = this.pokeOffset.DistanceTo(this.pokeOffsetPicked);
            this.txtResult.BackColor = Color.Red;
            this.txtResult.Text = result.ToString("0.000");
            if (result <= (double)this.nudUpper.Value && result >= (double)this.nudLower.Value)
            {
                this.txtResult.BackColor = Color.Green;
            }

        }
        /// <summary>
        /// 测试button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.checkVerticality();
        }
        #endregion 
    }

    public class NeedleCalibrationSetting2
    {
        public static NeedleCalibrationSetting2 Default = new NeedleCalibrationSetting2();

        public PointD PokePos = new PointD();

        public double PosZ;
        public double Upper;
        public double Lower;
        public static void Save()
        {
            JsonUtil.Serialize(typeof(NeedleCalibrationSetting).Name, Default);
        }

        public static void Load()
        {
            Default = JsonUtil.Deserialize<NeedleCalibrationSetting2>(typeof(NeedleCalibrationSetting2).Name);
            if (Default == null)
            {
                Default = new NeedleCalibrationSetting2();
            }
        }
    }
}
