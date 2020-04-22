using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class CalculateSlopeAmongVavel1AndVavel2 : TeachFormBase, IClickable
    {
        private DotParam dotParam;
        private DotStyle dotStyle;

        private double axisANegativeLimitPos, axisBNegativeLimitPos;
        private PointD cameraPosWithAPositiveLimit, cameraPosWithANegativeLimit,
                       cameraPosWithBPositiveLimit, cameraPosWithBNegativeLimit;

        #region 语言切换字符串变量

        private string[] lblTip = new string[10]
        {
            "Choice CorrectionMode and Axis,prepare the machine move.",
            "Use Camera:Select dispense postion in Positive Limit.",
            "Move to Plasticine postion in Positive Limit and make a Pit.",
            "Dispensing reference dots",
            "Move to Plasticine postion in Negative Limit and make a Pit.",
            "Calculate has been done",
            "移动失败，可能触发限位，请检查。",
            "激光异常",
            "结果异常，将设为默认值.",
            "Align camera on needle mark and press [Next]"
        };

        #endregion


        private int flag = 0;

        public CalculateSlopeAmongVavel1AndVavel2()
        {
            InitializeComponent();

            this.picDiagram.Image = Properties.Resources.AssociateVavel1andVavel2;
            this.txtAxisAResult.Text = Machine.Instance.Robot.CalibPrm.HorizontalRatio.ToString();
            this.txtAxisBResult.Text = Machine.Instance.Robot.CalibPrm.VerticalRatio.ToString();

            this.Init();

            this.UpdateByFlag();
            this.ReadLanguageResources();
            this.FormClosed += CalculateSlopeAmongVavel1AndVavel2_FormClosed;
        }

        private void CalculateSlopeAmongVavel1AndVavel2_FormClosed(object sender, FormClosedEventArgs e)
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

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            DataSetting.Default.DoneStepCount = 15;
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.CalibPrm.SavedItem = 15;
            DataSetting.Save();
            Machine.Instance.Robot.SaveCalibPrm();
            StepStateMgr.Instance.FindBy(14).IsDone = true;
            StepStateMgr.Instance.FindBy(14).IsChecked();

            this.Close();
        }

        public void DoHelp()
        {

        }

        public void DoNext()
        {
            this.UpdateSavePos();
            this.flag++;
            this.UpdateByFlag();
        }

        public void DoPrev()
        {
            this.flag--;
            this.UpdateByFlag();
        }

        public void DoTeach()
        {
            this.UpdateSavePos();
            this.flag++;
            this.UpdateByFlag();
        }

        private void UpdateByFlag()
        {
            switch (DataSetting.Default.selectCorrectionAxis)
            {
                case CorrectionAxis.A轴校正:
                    if (DataSetting.Default.selectDispenseMode)
                    {
                        this.ACorrectionInDispense();
                    }
                    else
                    {
                        this.ACorrectionInPlasticine();
                    }
                    break;
                case CorrectionAxis.B轴校正:
                    if (DataSetting.Default.selectDispenseMode)
                    {
                        this.BCorrectionInDispense();
                    }
                    else
                    {
                        this.BCorrectionInPlasticine();
                    }
                    break;
                case CorrectionAxis.A轴和B轴同时校正:
                    if (DataSetting.Default.selectDispenseMode)
                    {
                        this.AandBCorrectionInDispense();
                    }
                    else
                    {
                        this.AandBCorrectionInPlasticine();
                    }
                    break;
            }
        }

        /// <summary>
        /// A轴校正逻辑（打点模式）
        /// </summary>
        private void ACorrectionInDispense()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.HorizontalPosInDispense);
                    break;
                case 2:
                    this.DispensingPot();
                    break;
                case 3:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, true);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, false);
                    break;
                case 5:
                    this.CalculateResult();
                    break;
            }
        }

        /// <summary>
        /// A轴校正逻辑（戳橡皮泥模式）
        /// </summary>
        private void ACorrectionInPlasticine()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.HorizontalPosInPlasticine);
                    break;
                case 2:
                    this.MoveToNegativeLimit(Machine.Instance.Robot.AxisA);
                    break;
                case 3:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, true);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, false);
                    break;
                case 5:
                    this.CalculateResult();
                    break;
            }
        }

        /// <summary>
        /// B轴校正逻辑（打点模式）
        /// </summary>
        private void BCorrectionInDispense()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.VerticalPosInDispense);
                    break;
                case 2:
                    this.DispensingPot();
                    break;
                case 3:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, true);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, false);
                    break;
                case 5:
                    this.CalculateResult();
                    break;
            }
        }

        /// <summary>
        /// B轴校正逻辑（戳橡皮泥模式）
        /// </summary>
        private void BCorrectionInPlasticine()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.VerticalPosInPlasticine);
                    break;
                case 2:
                    this.MoveToNegativeLimit(Machine.Instance.Robot.AxisB);
                    break;
                case 3:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, true);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, false);
                    break;
                case 5:
                    this.CalculateResult();
                    break;
            }
        }

        /// <summary>
        /// A轴和B轴校正逻辑（打点模式）
        /// </summary>
        private void AandBCorrectionInDispense()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.HorizontalPosInDispense);
                    break;
                case 2:
                    this.DispensingPot();
                    break;
                case 3:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, true);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, false);
                    break;
                case 5:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, false);
                    break;
                case 6:
                    this.CalculateResult();
                    break;
            }
        }

        /// <summary>
        /// A轴和B轴校正逻辑（戳橡皮泥模式）
        /// </summary>
        private void AandBCorrectionInPlasticine()
        {
            switch (flag)
            {
                case 0:
                    this.InitFormControl();
                    break;
                case 1:
                    this.MoveToBeginningPosition(DataSetting.Default.HorizontalPosInPlasticine);
                    break;
                case 2:
                    this.MoveToNegativeLimit(Machine.Instance.Robot.AxisA);
                    break;
                case 3:
                    this.MoveToNegativeLimit(Machine.Instance.Robot.AxisB);
                    break;
                case 4:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, true);
                    break;
                case 5:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisA, false);
                    break;
                case 6:
                    this.MoveCameraToLimitPos(Machine.Instance.Robot.AxisB, false);
                    break;
                case 7:
                    this.CalculateResult();
                    break;
            }
        }

        private void InitFormControl()
        {
            this.cameraControl.Hide();
            this.picDiagram.Show();

            this.grpCorrectionAxis.Enabled = true;
            this.grpCorrectionMode.Enabled = true;
            this.grpVavelParam.Enabled = false;
            this.grpResult.Enabled = false;

            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = true;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;

            this.lblTitle.Text = this.lblTip[0];

        }

        private void MoveToBeginningPosition(PointD beginningPos)
        {
            this.picDiagram.Hide();
            this.cameraControl.Show();

            this.grpCorrectionMode.Enabled = false;
            this.grpCorrectionAxis.Enabled = false;
            this.grpResult.Enabled = false;

            if (DataSetting.Default.selectDispenseMode)
            {
                this.grpVavelParam.Enabled = true;
            }
            else
            {
                this.grpVavelParam.Enabled = false;
            }

            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnTeach.Enabled = false;
            this.btnDone.Enabled = false;

            if (DataSetting.Default.selectDispenseMode)
            {
                this.lblTitle.Text = this.lblTip[1];
            }
            else
            {
                this.lblTitle.Text = this.lblTip[2];
            }

            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();

                Result result = Machine.Instance.Robot.MovePosXYABAndReply(beginningPos, new PointD(0, 0), (int)Machine.Instance.Setting.CardSelect);
                if (!result.IsOk)
                {
                    this.btnPrev.Enabled = true;
                    return;
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                }));

            }));

        }

        private void DispensingPot()
        {
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.grpVavelParam.Enabled = false;

            this.lblTitle.Text = this.lblTip[3];

            this.dotStyle = (DotStyle)this.cbxDotStyle.SelectedIndex;
            this.dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(this.dotStyle);

            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();

                Result result;

                double dot1Z;//, dot2Z, dot3Z;
                result = this.HeightMeasure(out dot1Z);
                if (!result.IsOk)
                {
                    this.btnPrev.Enabled = true;
                    return;
                }

                result = Machine.Instance.Valve2.DoPurgeAndPrime();
                if (!result.IsOk)
                {
                    this.btnPrev.Enabled = true;
                    return;
                }

                this.ContinueDot(dot1Z);

                this.BeginInvoke(new Action(() =>
                {
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                }));
            }));
        }

        private void MoveToNegativeLimit(Axis axisName)
        {
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;

            this.lblTitle.Text = this.lblTip[4];

            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();

                Result result;
                if (axisName == Machine.Instance.Robot.AxisA)
                {
                    result = Machine.Instance.Robot.MoveIncAAndReply(-1000, 10);
                    Thread.Sleep(1000);
                    this.axisANegativeLimitPos = Machine.Instance.Robot.PosA;
                }
                else
                {
                    result = Machine.Instance.Robot.MoveIncBAndReply(-200, 10);
                    Thread.Sleep(1000);
                    this.axisBNegativeLimitPos = Machine.Instance.Robot.PosB;
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                }));
            }));
        }

        private void MoveCameraToLimitPos(Axis axisName, bool isPositiveLimit)
        {
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnDone.Enabled = false;
            this.grpResult.Enabled = false;

            this.lblTitle.Text = this.lblTip[9];

            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();

                Result result;
                if (axisName == Machine.Instance.Robot.AxisA)
                {
                    if (isPositiveLimit)
                    {
                        //result = Machine.Instance.Robot.MoveIncXYAndReply(new PointD(-Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                        //   -Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y));
                        result = Machine.Instance.Robot.ManualMoveIncXYAndReply(new PointD(-Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                           -Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y));
                    }
                    else
                    {
                        //result = Machine.Instance.Robot.MoveIncXYAndReply(new PointD(this.axisANegativeLimitPos, 0));
                        result = Machine.Instance.Robot.ManualMoveIncXYAndReply(new PointD(this.axisANegativeLimitPos, 0));
                    }
                }
                else
                {
                    if (isPositiveLimit)
                    {
                        //result = Machine.Instance.Robot.MoveIncXYAndReply(new PointD(-Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                        //    -Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y));
                        result = Machine.Instance.Robot.ManualMoveIncXYAndReply(new PointD(-Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                            -Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y));
                    }
                    else
                    {
                        //result = Machine.Instance.Robot.MoveIncXYAndReply(new PointD(0, this.axisBNegativeLimitPos));
                        result = Machine.Instance.Robot.ManualMoveIncXYAndReply(new PointD(0, this.axisBNegativeLimitPos));
                    }
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                }));
            }));
        }

        private void CalculateResult()
        {
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnDone.Enabled = true;
            this.grpResult.Enabled = true;

            this.lblTitle.Text = this.lblTip[5];

            switch (DataSetting.Default.selectCorrectionAxis)
            {
                case CorrectionAxis.A轴校正:
                    this.CalculateAxisARatio();
                    break;
                case CorrectionAxis.B轴校正:
                    this.CalculateAxisBRatio();
                    break;
                case CorrectionAxis.A轴和B轴同时校正:
                    this.CalculateAxisARatio();
                    this.CalculateAxisBRatio();
                    break;
            }

        }

        private void UpdateSavePos()
        {
            switch (DataSetting.Default.selectCorrectionAxis)
            {
                case CorrectionAxis.A轴校正:
                    this.UpdateAxisASavePos();
                    break;
                case CorrectionAxis.B轴校正:
                    this.UpdateAxisBSavePos();
                    break;
                case CorrectionAxis.A轴和B轴同时校正:
                    this.UpdateAxisABSavePos();
                    break;
            }
        }

        private void UpdateAxisASavePos()
        {
            switch (this.flag)
            {
                case 1:
                    if (DataSetting.Default.selectDispenseMode)
                    {
                        DataSetting.Default.HorizontalPosInDispense = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                    }
                    else
                    {
                        DataSetting.Default.HorizontalPosInPlasticine = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                    }
                    break;
                case 3:
                    this.cameraPosWithAPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                        Machine.Instance.Robot.PosY);
                    break;
                case 4:
                    this.cameraPosWithANegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                       Machine.Instance.Robot.PosY);
                    break;
            }

        }

        private void UpdateAxisBSavePos()
        {
            switch (this.flag)
            {
                case 1:
                    if (DataSetting.Default.selectDispenseMode)
                    {
                        DataSetting.Default.VerticalPosInDispense = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                    }
                    else
                    {
                        DataSetting.Default.VerticalPosInPlasticine = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                    }
                    break;
                case 3:
                    this.cameraPosWithBPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                        Machine.Instance.Robot.PosY);
                    break;
                case 4:
                    this.cameraPosWithBNegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                       Machine.Instance.Robot.PosY);
                    break;
            }
        }

        private void UpdateAxisABSavePos()
        {
            if (DataSetting.Default.selectDispenseMode)
            {
                switch (this.flag)
                {
                    case 1:
                        DataSetting.Default.HorizontalPosInDispense = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                        break;
                    case 3:
                        this.cameraPosWithAPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                            Machine.Instance.Robot.PosY);
                        break;
                    case 4:
                        this.cameraPosWithANegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        this.cameraPosWithBPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        break;
                    case 5:
                        this.cameraPosWithBNegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        break;

                }
            }
            else
            {
                switch (this.flag)
                {
                    case 1:
                        DataSetting.Default.HorizontalPosInPlasticine = new PointD(Machine.Instance.Robot.PosX,
                              Machine.Instance.Robot.PosY);
                        break;
                    case 4:
                        this.cameraPosWithAPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                            Machine.Instance.Robot.PosY);
                        break;
                    case 5:
                        this.cameraPosWithANegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        this.cameraPosWithBPositiveLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        break;
                    case 6:
                        this.cameraPosWithBNegativeLimit = new PointD(Machine.Instance.Robot.PosX,
                           Machine.Instance.Robot.PosY);
                        break;

                }
            }
        }

        private Result HeightMeasure(out double dot1Z)//(out double dot1Z,out double dot2Z,out double dot3Z)
        {
            //关闭光源
            Machine.Instance.Light.None();

            double dot1Height;//dot2Height, dot3Height;
            Result rst;

            if (DataSetting.Default.selectCorrectionAxis == CorrectionAxis.A轴校正)
            {
                rst = this.MoveAndHeighting(DataSetting.Default.HorizontalPosInDispense, out dot1Height);
                if (!rst.IsOk)
                {
                    dot1Z = 0;
                    return Result.FAILED;
                }

                dot1Z = dot1Height;
            }
            else if (DataSetting.Default.selectCorrectionAxis == CorrectionAxis.B轴校正)
            {
                rst = this.MoveAndHeighting(DataSetting.Default.VerticalPosInDispense, out dot1Height);
                if (!rst.IsOk)
                {
                    dot1Z = 0;
                    return Result.FAILED;
                }

                dot1Z = dot1Height;
            }
            else
            {
                rst = this.MoveAndHeighting(DataSetting.Default.HorizontalPosInDispense, out dot1Height);
                if (!rst.IsOk)
                {
                    dot1Z = 0;
                    return Result.FAILED;
                }

                dot1Z = dot1Height;
            }

            //打开光源
            Machine.Instance.Light.ResetToLast();
            //
            return Result.OK;
        }

        private Result MoveAndHeighting(PointD targetPos, out double dotHeight)
        {
            PointD heightPos = new PointD();
            heightPos.X = targetPos.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X;
            heightPos.Y = targetPos.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y;
            //Result moveResult = Machine.Instance.Robot.MovePosXYAndReply(heightPos);
            Result moveResult = Machine.Instance.Robot.ManualMovePosXYAndReply(heightPos);
            if (!moveResult.IsOk)
            {
                MessageBox.Show(this.lblTip[6]);
                dotHeight = 0;
                return Result.FAILED;
            }
            double height;
            int laserResult=-1;
            //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            //{
            //    DoType.测高阀.Set(true);
            //}
            Machine.Instance.MeasureHeightBefore();
            laserResult = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height);
            Machine.Instance.MeasureHeightAfter();
            //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            //{
            //    DoType.测高阀.Set(false);
            //}
            //Result res =Machine.Instance.MeasureHeight(out height);
            //laserResult = res.IsOk ? 0 : -1;
            if (laserResult != 0)
            {
                MessageBox.Show(this.lblTip[7]);
                dotHeight = 0;
                return Result.FAILED;
            }

            dotHeight = Machine.Instance.Robot.CalibPrm.StandardZ + (height - Machine.Instance.Robot.CalibPrm.StandardHeight);
            return Result.OK;
        }

        private void ContinueDot(double z1)
        {
            Machine.Instance.Robot.MoveSafeZAndReply();

            switch (DataSetting.Default.selectCorrectionAxis)
            {
                case CorrectionAxis.A轴校正:
                    Machine.Instance.Robot.MovePosXYABAndReply(new PointD(DataSetting.Default.HorizontalPosInDispense.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                       DataSetting.Default.HorizontalPosInDispense.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y), new PointD(0, 0), (int)Machine.Instance.Setting.CardSelect);
                    this.DotByParam(z1);
                    Thread.Sleep(50);
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.MoveIncAAndReply(-1000, 10);
                    this.DotByParam(z1);
                    this.axisANegativeLimitPos = Machine.Instance.Robot.PosA;
                    break;
                case CorrectionAxis.B轴校正:
                    Machine.Instance.Robot.MovePosXYABAndReply(new PointD(DataSetting.Default.VerticalPosInDispense.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                       DataSetting.Default.VerticalPosInDispense.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y), new PointD(0, 0), (int)Machine.Instance.Setting.CardSelect);
                    this.DotByParam(z1);
                    Thread.Sleep(50);
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.MoveIncBAndReply(-200, 10);
                    this.DotByParam(z1);
                    this.axisBNegativeLimitPos = Machine.Instance.Robot.PosB;
                    break;
                case CorrectionAxis.A轴和B轴同时校正:
                    Machine.Instance.Robot.MovePosXYABAndReply(new PointD(DataSetting.Default.HorizontalPosInDispense.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X,
                        DataSetting.Default.HorizontalPosInDispense.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y), new PointD(0, 0), (int)Machine.Instance.Setting.CardSelect);
                    this.DotByParam(z1);
                    Thread.Sleep(50);
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.MoveIncAAndReply(-1000, 10);
                    this.DotByParam(z1);
                    this.axisANegativeLimitPos = Machine.Instance.Robot.PosA;
                    Thread.Sleep(50);
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.MoveIncBAndReply(-200, 10);
                    this.DotByParam(z1);
                    this.axisBNegativeLimitPos = Machine.Instance.Robot.PosB;
                    break;
            }
        }

        private Result DotByParam(double z)
        {
            // 下降到指定高度
            Result ret = Machine.Instance.Robot.MovePosZAndReply(z + dotParam.DispenseGap, dotParam.DownSpeed, dotParam.DownAccel);
            if (!ret.IsOk)
            {
                return ret;
            }
            Thread.Sleep(TimeSpan.FromSeconds(dotParam.SettlingTime));



            if (Machine.Instance.Valve2.ValveSeries == ValveSeries.喷射阀)
            {
                if (dotParam.MultiShotDelta > 0)
                {
                    // 开始喷胶
                    for (int i = 0; i < dotParam.NumShots; i++)
                    {
                        Machine.Instance.Valve2.SprayOneAndWait();

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
                    Machine.Instance.Valve2.SprayCycleAndWait((short)dotParam.NumShots);

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
            else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                Machine.Instance.Valve2.SprayOneAndWait((int)this.nudSprayTime.Value);

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

        private void CalculateAxisARatio()
        {
            double axisXDistance = Math.Abs(this.cameraPosWithAPositiveLimit.X - this.cameraPosWithANegativeLimit.X);
            double axisADistance = Math.Abs(this.axisANegativeLimitPos);
            double horizontalRatio = axisXDistance / axisADistance;
            this.txtAxisAResult.Text = horizontalRatio.ToString();
            if (horizontalRatio < 0.9 || horizontalRatio > 1.1)
            {
                this.txtAxisBResult.Text = this.lblTip[8];
                DataSetting.Default.HorizontalRatio = 1;
                Machine.Instance.Robot.CalibPrm.HorizontalRatio = 1;
            }
            else
            {
                DataSetting.Default.HorizontalRatio = horizontalRatio;
                Machine.Instance.Robot.CalibPrm.HorizontalRatio = horizontalRatio;
            }

        }

        private void CalculateAxisBRatio()
        {
            double axisYDistance = Math.Abs(this.cameraPosWithBPositiveLimit.Y - this.cameraPosWithBNegativeLimit.Y);
            double axisBDistance = Math.Abs(this.axisBNegativeLimitPos);
            double verticalRatio = axisYDistance / axisBDistance;
            this.txtAxisBResult.Text = verticalRatio.ToString();
            if (verticalRatio < 0.9 || verticalRatio > 1.1)
            {
                this.txtAxisBResult.Text = this.lblTip[8];
                DataSetting.Default.VerticalRatio = 1;
                Machine.Instance.Robot.CalibPrm.VerticalRatio = 1;
            }
            else
            {
                DataSetting.Default.VerticalRatio = verticalRatio;
                Machine.Instance.Robot.CalibPrm.VerticalRatio = verticalRatio;
            }
        }

        private void Init()
        {
            this.valveControl1.Setup(2);
            for (int i = 0; i < 10; i++)
            {
                this.cbxDotStyle.Items.Add("Type " + (i + 1));
            }
            this.cbxDotStyle.SelectedIndex = 0;
            this.dotStyle = DotStyle.TYPE_1;
            this.dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(this.dotStyle);

            if (DataSetting.Default.selectDispenseMode)
            {
                this.rdoDispenseMode.Checked = true;
            }
            else
            {
                this.rdoPlasticineMode.Checked = true;
            }
            switch (DataSetting.Default.selectCorrectionAxis)
            {
                case CorrectionAxis.A轴校正:
                    this.rdoAxisA.Checked = true;
                    break;
                case CorrectionAxis.B轴校正:
                    this.rdoAxisB.Checked = true;
                    break;
                case CorrectionAxis.A轴和B轴同时校正:
                    this.rdoAxisAandB.Checked = true;
                    break;
            }
        }

        private void btnEditDot_Click(object sender, EventArgs e)
        {
            new EditDotParamsForm(FluidProgram.CurrentOrDefault().ProgramSettings.DotParamList).ShowDialog();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoPlasticineMode.Checked)
            {
                DataSetting.Default.selectDispenseMode = false;
            }
            else
            {
                DataSetting.Default.selectDispenseMode = true;
            }

            if (this.rdoAxisA.Checked)
            {
                DataSetting.Default.selectCorrectionAxis = CorrectionAxis.A轴校正;
            }
            else if (this.rdoAxisB.Checked)
            {
                DataSetting.Default.selectCorrectionAxis = CorrectionAxis.B轴校正;
            }
            else if (this.rdoAxisAandB.Checked)
            {
                DataSetting.Default.selectCorrectionAxis = CorrectionAxis.A轴和B轴同时校正;
            }
        }
    }
}
