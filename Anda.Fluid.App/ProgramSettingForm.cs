using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Proportionor;
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
using System.Reflection;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Domain.Custom;
using Anda.Fluid.App.CustomDataUI;
using Anda.Fluid.Domain.Dialogs.Soak;
using Anda.Fluid.Drive.Sensors.HeightMeasure;

namespace Anda.Fluid.App
{
    public partial class ProgramSettingForm : FormEx
    {
        private FluidProgram fluidProgram;
        private RuntimeSettings runtimeSettingsBackUp;
        private CustomControlBase dataControl;

        public ProgramSettingForm(FluidProgram fluidProgram)
        {
            //打开温控器
            HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.打开程序参数设定界面时);
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.打开程序参数设定界面时);
            }
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this.fluidProgram = fluidProgram;

            this.nudSingleDotWt.DecimalPlaces = 4;
            this.nudSingleDotWt.Minimum = 0.00M;
            this.nudSingleDotWt.Maximum = 10;
            this.nudSingleDotWt.Increment = 0.0001M;
            if (this.fluidProgram.RuntimeSettings.SingleDropWeight <= 0)
            {
                this.tabControl1.SelectedTab = this.tabPage1;
                this.nudSingleDotWt.BackColor = Color.Red;              
            }
            this.nudSingleDotWt.Value = (decimal)this.fluidProgram.RuntimeSettings.SingleDropWeight;

            this.nudAirPressure.Minimum = 0;
            this.nudAirPressure.Maximum = 500;
            this.nudAirPressure.Value = this.fluidProgram.RuntimeSettings.AirPressure;

            this.nudAirPressure2.Minimum = 0;
            this.nudAirPressure2.Maximum = 500;
            this.nudAirPressure2.Value = this.fluidProgram.RuntimeSettings.AirPressure2;

            this.nudValve1Temprature.Value = (decimal)this.fluidProgram.RuntimeSettings.Valve1Temperature;

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {               
                this.nudValve2Temprature.Enabled = false;
                this.nudValve2Temprature.Value = 0;
                this.nudAirPressure2.Enabled = false;
                this.nudAirPressure2.Value = 0;
            }
            else
            {
                this.nudValve2Temprature.Value = (decimal)this.fluidProgram.RuntimeSettings.Valve2Temperature;
                this.nudAirPressure2.Value = this.fluidProgram.RuntimeSettings.AirPressure2;
            }

            this.txtBoardHeight.ReadOnly = true;
            this.txtBoardHeight.Text = this.fluidProgram.RuntimeSettings.StandardBoardHeight.ToString("0.000");

            this.nudMaxHeight.Minimum = 0;
            this.nudMaxHeight.Maximum = 10;
            this.nudMaxHeight.Increment = 0.001M;
            this.nudMaxHeight.DecimalPlaces = 3;
            this.nudMaxHeight.Value = (decimal)this.fluidProgram.RuntimeSettings.MaxTolerance;

            this.nudMinHeight.Minimum = -10;
            this.nudMinHeight.Maximum = 0;
            this.nudMinHeight.Increment = 0.001M;
            this.nudMinHeight.DecimalPlaces = 3;
            this.nudMinHeight.Value = (decimal)this.fluidProgram.RuntimeSettings.MinTolerance;

            this.nudSimulDistence.Minimum = 0;
            this.nudSimulDistence.Maximum = 195;
            this.nudSimulDistence.Increment = 0.001M;
            this.nudSimulDistence.DecimalPlaces = 3;
            this.nudSimulDistence.Value = (decimal)this.fluidProgram.RuntimeSettings.SimulDistence;

            this.nudSimulOffsetX.Minimum = -10;
            this.nudSimulOffsetX.Maximum = 10;
            this.nudSimulOffsetX.Increment = 0.001M;
            this.nudSimulOffsetX.DecimalPlaces = 3;
            this.nudSimulOffsetX.Value = (decimal)this.fluidProgram.RuntimeSettings.SimulOffsetX;

            this.nudSimulOffsetY.Minimum = -10;
            this.nudSimulOffsetY.Maximum = 10;
            this.nudSimulOffsetY.Increment = 0.001M;
            this.nudSimulOffsetY.DecimalPlaces = 3;
            this.nudSimulOffsetY.Value = (decimal)this.fluidProgram.RuntimeSettings.SimulOffsetY;

            this.nudAutoScaleCount.Minimum = 1;
            this.nudAutoScaleCount.Maximum = 10000;
            this.nudAutoScaleHour.Maximum = 24;
            this.nudAutoScaleMinu.Maximum = 59;
            this.nudAutoScaleSeco.Maximum = 59;
            this.nudAutoScaleCount.Value = MathUtils.Limit(this.fluidProgram.RuntimeSettings.AutoScaleCount, (int)nudAutoScaleCount.Minimum, (int)nudAutoScaleCount.Maximum);
            this.nudAutoScaleHour.Value = this.fluidProgram.RuntimeSettings.AutoScaleSpan.Hours;
            this.nudAutoScaleMinu.Value = this.fluidProgram.RuntimeSettings.AutoScaleSpan.Minutes;
            this.nudAutoScaleSeco.Value = this.fluidProgram.RuntimeSettings.AutoScaleSpan.Seconds;

            this.nudAutoPurgeCount.Minimum = 1;
            this.nudAutoPurgeCount.Maximum = 10000;
            this.nudAutoPurgeHour.Maximum = 24;
            this.nudAutoPurgeMinu.Maximum = 59;
            this.nudAutoPurgeSeco.Maximum = 59;
            this.nudAutoPurgeCount.Value = MathUtils.Limit(this.fluidProgram.RuntimeSettings.AutoPurgeCount, (int)nudAutoPurgeCount.Minimum, (int)nudAutoPurgeCount.Maximum);
            this.nudAutoPurgeHour.Value = this.fluidProgram.RuntimeSettings.AutoPurgeSpan.Hours;
            this.nudAutoPurgeMinu.Value = this.fluidProgram.RuntimeSettings.AutoPurgeSpan.Minutes;
            this.nudAutoPurgeSeco.Value = this.fluidProgram.RuntimeSettings.AutoPurgeSpan.Seconds;

            this.chxAutoScaleSpan.Checked = this.fluidProgram.RuntimeSettings.IsAutoScaleSpan;
            this.chxAutoScaleCount.Checked = this.fluidProgram.RuntimeSettings.IsAutoScaleCount;
            this.chxAutoPurgeSpan.Checked = this.fluidProgram.RuntimeSettings.IsAutoPurgeSpan;
            this.chxAutoPurgeCount.Checked = this.fluidProgram.RuntimeSettings.IsAutoPurgeCount;

            this.cbxPurge.Checked = this.fluidProgram.RuntimeSettings.PurgeBeforeStart;
            this.cbxScale.Checked = this.fluidProgram.RuntimeSettings.ScaleBeforeStart;
            this.cbxHalfAdjust.Checked = this.fluidProgram.RuntimeSettings.isHalfAdjust;

            #region 飞拍参数
            this.cbxFlyEnable.Checked = this.fluidProgram.RuntimeSettings.isFlyMarks;
            this.cbxIsRowFirst.Checked = this.fluidProgram.RuntimeSettings.FlyIsRowFirst;
            this.tbxFlySpeed.Text = this.fluidProgram.RuntimeSettings.FlySpeed.ToString();
            this.tbxFlyAcc.Text = this.fluidProgram.RuntimeSettings.FlyAcc.ToString();
            this.tbxCornerSpeed.Text = this.fluidProgram.RuntimeSettings.FlyCornerSpeed.ToString();
            this.tbxPreDistance.Text = this.fluidProgram.RuntimeSettings.FlyPreDistance.ToString();
            this.cbxAdjustFlyOffset.Checked = this.fluidProgram.RuntimeSettings.isAdjustFlyOffset;
            this.cbxNGReshoot.Checked = this.fluidProgram.RuntimeSettings.IsNGReshoot;
            this.cbxFlyOriginPos.Checked = this.fluidProgram.RuntimeSettings.FlyOriginPos;
            if (this.fluidProgram.RuntimeSettings.DisposeThreadCount < 1)
            {
                this.fluidProgram.RuntimeSettings.DisposeThreadCount = 1;
            }
            this.tbxThreadCount.Text = this.fluidProgram.RuntimeSettings.DisposeThreadCount.ToString();

            this.tbxFlyAcc.Enabled = this.cbxFlyEnable.Checked;
            this.tbxFlySpeed.Enabled = this.cbxFlyEnable.Checked;
            this.cbxIsRowFirst.Enabled = this.cbxFlyEnable.Checked;
            this.tbxCornerSpeed.Enabled = this.cbxFlyEnable.Checked;
            this.tbxPreDistance.Enabled = this.cbxFlyEnable.Checked;
            this.tbxThreadCount.Enabled = this.cbxFlyEnable.Checked;
            this.cbxAdjustFlyOffset.Enabled = this.cbxFlyEnable.Checked;
            this.cbxNGReshoot.Enabled = this.cbxFlyEnable.Checked;
            this.cbxFlyOriginPos.Enabled = this.cbxFlyEnable.Checked;

            this.ckbSync.Checked = this.fluidProgram.RuntimeSettings.IsSyncSingleDropWeight;
            if (this.ckbSync.Checked)
            {
                this.fluidProgram.RuntimeSettings.SingleDropWeight = Machine.Instance.Valve1.weightPrm.SingleDotWeight;
            }
            #endregion

            this.chxAutoSkipNgMark.Checked = this.fluidProgram.RuntimeSettings.AutoSkipNgMarks;
            this.chxSaveMarkImages.Checked = this.fluidProgram.RuntimeSettings.SaveMarkImages;
            this.ckbSaveMeasureImages.Checked = this.fluidProgram.RuntimeSettings.SaveMeasureMentImages;
            this.ckbMarkSort.Checked = this.fluidProgram.RuntimeSettings.MarksSort;
            this.chxBackToWorkpieceOrigin.Checked = this.fluidProgram.RuntimeSettings.Back2WorkpieceOrigin;
            this.ckbMeasureHeightSort.Checked = this.fluidProgram.RuntimeSettings.MeasureCmdsSort;
            this.txtConveyor2Origin.Text = this.fluidProgram.Conveyor2OriginOffset.ToString();

            #region 连续前瞻

            if (this.fluidProgram.RuntimeSettings.FluidMoveMode == FluidMoveMode.普通)
            {
                this.rbnNormal.Checked = true;
            }
            else
            {
                this.rbnConti.Checked = true;
            }
            this.nudLookTime.Minimum = 1;
            this.nudLookTime.Maximum = 10;
            this.nudLookTime.Value = (decimal)MathUtils.Limit(this.fluidProgram.RuntimeSettings.LookTime, 1, 10);
            this.nudLookAccMax.Minimum = 1;
            this.nudLookAccMax.Maximum = 5;
            this.nudLookAccMax.Increment = 0.1M;
            this.nudLookAccMax.DecimalPlaces = 1;
            this.nudLookAccMax.Value = (decimal)MathUtils.Limit(this.fluidProgram.RuntimeSettings.LookAccMax, 1, 5);
            this.nudLookCount.Minimum = 10;
            this.nudLookCount.Maximum = 500;
            this.nudLookCount.Value = (decimal)MathUtils.Limit(this.fluidProgram.RuntimeSettings.LookCount, 10, 500);

            #endregion

            #region 浸泡
            this.nudDoSoakHour.Maximum = 24;
            this.nudDoSoakMin.Maximum = 59;
            this.nudDoSoakSec.Maximum = 59;
            this.nudDoSoakHour.Value = this.fluidProgram.RuntimeSettings.AutoSoakSpan.Hours;
            this.nudDoSoakMin.Value = this.fluidProgram.RuntimeSettings.AutoSoakSpan.Minutes;
            this.nudDoSoakSec.Value = this.fluidProgram.RuntimeSettings.AutoSoakSpan.Seconds;

            this.chxAutoSoakSpan.Checked = this.fluidProgram.RuntimeSettings.IsAutoSoakSpan;
            #endregion

            this.tbAbsZValue.Text = this.fluidProgram.RuntimeSettings.BoardZValue.ToString("0.000");
            if (Machine.Instance.Laser.Laserable.GetType() != typeof(LaserableDisable))
            {
                this.btnTeachAbsZ.Enabled = false;
            }

            this.LoadDataControl();
            this.dataControl.Parent = this.tabPage7;
            this.dataControl.Dock = DockStyle.Fill;
            this.dataControl.LoadParam(this.fluidProgram);

            if (this.fluidProgram.RuntimeSettings != null)
            {
                this.runtimeSettingsBackUp = (RuntimeSettings)this.fluidProgram.RuntimeSettings.Clone();
            }
            this.ReadLanguageResources();
        }

        /// <summary>
        /// 仅用于保存语言资源时调用
        /// </summary>
        private ProgramSettingForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((double)this.nudSingleDotWt.Value <= 0)
            {
                this.tabControl1.SelectedTab = this.tabPage1;
                this.nudSingleDotWt.BackColor = Color.Red;
                return;
            }
            this.nudSingleDotWt.BackColor = System.Drawing.SystemColors.Window;
            this.fluidProgram.RuntimeSettings.SingleDropWeight = (double)this.nudSingleDotWt.Value;

            // 设置气压1
            if (this.fluidProgram.RuntimeSettings.AirPressure != (int)this.nudAirPressure.Value)
            {
                this.fluidProgram.RuntimeSettings.AirPressure = (int)this.nudAirPressure.Value;
                Machine.Instance.Valve1.Proportioner.Proportional.SetValue((ushort)this.nudAirPressure.Value);
            }
            // 设置气压2
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                if (this.fluidProgram.RuntimeSettings.AirPressure2 != (int)this.nudAirPressure2.Value)
                {
                    this.fluidProgram.RuntimeSettings.AirPressure2 = (int)this.nudAirPressure2.Value;
                    Proportioner.Sleep();
                    Machine.Instance.Valve2.Proportioner.Proportional.SetValue((ushort)this.nudAirPressure2.Value);
                }
            }
            //设置温度
            if (this.fluidProgram.RuntimeSettings.Valve1Temperature != (double)this.nudValve1Temprature.Value)
            {
                this.fluidProgram.RuntimeSettings.Valve1Temperature = (double)this.nudValve1Temprature.Value;
                Machine.Instance.HeaterController1.HeaterPrm.Standard[0] = this.fluidProgram.RuntimeSettings.Valve1Temperature;
                Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, (double)this.nudValve1Temprature.Value, 0);
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                if (this.fluidProgram.RuntimeSettings.Valve2Temperature != (double)this.nudValve2Temprature.Value)
                {
                    this.fluidProgram.RuntimeSettings.Valve2Temperature = (double)this.nudValve2Temprature.Value;
                    if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
                    {
                        Machine.Instance.HeaterController1.HeaterPrm.Standard[1] = this.fluidProgram.RuntimeSettings.Valve2Temperature;
                        Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, (double)this.nudValve2Temprature.Value, 1);
                    }
                    else
                    {
                        Machine.Instance.HeaterController2.HeaterPrm.Standard[0] = this.fluidProgram.RuntimeSettings.Valve2Temperature;
                        Machine.Instance.HeaterController2.Fire(HeaterMsg.设置标准温度值, (double)this.nudValve2Temprature.Value, 0);
                    }
                }
            }

            this.fluidProgram.RuntimeSettings.isHalfAdjust = this.cbxHalfAdjust.Checked;
            this.fluidProgram.RuntimeSettings.MaxTolerance = (double)this.nudMaxHeight.Value;
            this.fluidProgram.RuntimeSettings.MinTolerance = (double)this.nudMinHeight.Value;
            this.fluidProgram.RuntimeSettings.SimulDistence = (double)this.nudSimulDistence.Value;
            this.fluidProgram.RuntimeSettings.SimulOffsetX = (double)this.nudSimulOffsetX.Value;
            this.fluidProgram.RuntimeSettings.SimulOffsetY = (double)this.nudSimulOffsetY.Value;

            #region 称重清洗吐液,浸泡
            this.fluidProgram.RuntimeSettings.PurgeBeforeStart = this.cbxPurge.Checked;
            this.fluidProgram.RuntimeSettings.ScaleBeforeStart = this.cbxScale.Checked;
            this.fluidProgram.RuntimeSettings.IsAutoScaleSpan = this.chxAutoScaleSpan.Checked;
            this.fluidProgram.RuntimeSettings.IsAutoScaleCount = this.chxAutoScaleCount.Checked;
            this.fluidProgram.RuntimeSettings.AutoScaleCount = (int)this.nudAutoScaleCount.Value;
            this.fluidProgram.RuntimeSettings.AutoScaleSpan = new TimeSpan((int)this.nudAutoScaleHour.Value, (int)this.nudAutoScaleMinu.Value, (int)this.nudAutoScaleSeco.Value);
            this.fluidProgram.RuntimeSettings.IsAutoPurgeSpan = this.chxAutoPurgeSpan.Checked;
            this.fluidProgram.RuntimeSettings.IsAutoPurgeCount = this.chxAutoPurgeCount.Checked;
            this.fluidProgram.RuntimeSettings.AutoPurgeCount = (int)this.nudAutoPurgeCount.Value;
            this.fluidProgram.RuntimeSettings.AutoPurgeSpan = new TimeSpan((int)this.nudAutoPurgeHour.Value, (int)this.nudAutoPurgeMinu.Value, (int)this.nudAutoPurgeSeco.Value);
            this.fluidProgram.RuntimeSettings.IsSyncSingleDropWeight = this.ckbSync.Checked;

            this.fluidProgram.RuntimeSettings.AutoSoakSpan = new TimeSpan((int)this.nudDoSoakHour.Value, (int)this.nudDoSoakMin.Value, (int)this.nudDoSoakSec.Value);

            #endregion

            #region marks
            this.fluidProgram.RuntimeSettings.AutoSkipNgMarks = this.chxAutoSkipNgMark.Checked;
            this.fluidProgram.RuntimeSettings.SaveMarkImages = this.chxSaveMarkImages.Checked;
            this.fluidProgram.RuntimeSettings.SaveMeasureMentImages = this.ckbSaveMeasureImages.Checked;
            this.fluidProgram.RuntimeSettings.MarksSort = this.ckbMarkSort.Checked;
            this.fluidProgram.RuntimeSettings.Back2WorkpieceOrigin = this.chxBackToWorkpieceOrigin.Checked;
            this.fluidProgram.RuntimeSettings.MeasureCmdsSort = this.ckbMeasureHeightSort.Checked;

            #endregion

            #region 飞拍参数
            this.fluidProgram.RuntimeSettings.isFlyMarks = this.cbxFlyEnable.Checked;
            this.fluidProgram.RuntimeSettings.FlyIsRowFirst = this.cbxIsRowFirst.Checked;
            //如果飞拍运动参数改动需要重新校对飞拍校正值
            if (this.tbxFlySpeed.Value != this.fluidProgram.RuntimeSettings.FlySpeed
                || this.tbxCornerSpeed.Value != this.fluidProgram.RuntimeSettings.FlyCornerSpeed
                || this.tbxFlyAcc.Value != this.fluidProgram.RuntimeSettings.FlyAcc)
            {
                this.fluidProgram.RuntimeSettings.FlyOffsetIsValid = false;
            }
            this.fluidProgram.RuntimeSettings.FlySpeed = this.tbxFlySpeed.Value;
            this.fluidProgram.RuntimeSettings.FlyAcc = this.tbxFlyAcc.Value;
            this.fluidProgram.RuntimeSettings.FlyCornerSpeed = this.tbxCornerSpeed.Value;
            this.fluidProgram.RuntimeSettings.FlyPreDistance = this.tbxPreDistance.Value;
            this.fluidProgram.RuntimeSettings.isAdjustFlyOffset = this.cbxAdjustFlyOffset.Checked;
            this.fluidProgram.RuntimeSettings.IsNGReshoot = this.cbxNGReshoot.Checked;
            this.fluidProgram.RuntimeSettings.FlyOriginPos = this.cbxFlyOriginPos.Checked;
            this.fluidProgram.RuntimeSettings.DisposeThreadCount = this.tbxThreadCount.Value;
            if (this.fluidProgram.RuntimeSettings.DisposeThreadCount < 1)
            {
                this.fluidProgram.RuntimeSettings.DisposeThreadCount = 1;
            }
            else if (this.fluidProgram.RuntimeSettings.DisposeThreadCount > 5)
            {
                this.fluidProgram.RuntimeSettings.DisposeThreadCount = 5;
            }
            #endregion

            #region 连续前瞻

            this.fluidProgram.RuntimeSettings.FluidMoveMode = rbnNormal.Checked ? FluidMoveMode.普通 : FluidMoveMode.连续;
            this.fluidProgram.RuntimeSettings.LookTime = (double)this.nudLookTime.Value;
            this.fluidProgram.RuntimeSettings.LookAccMax = (double)this.nudLookAccMax.Value;
            this.fluidProgram.RuntimeSettings.LookCount = (int)this.nudLookCount.Value;

            #endregion

            #region 浸泡

            this.fluidProgram.RuntimeSettings.AutoSoakSpan = new TimeSpan((int)this.nudDoSoakHour.Value, (int)this.nudDoSoakMin.Value, (int)this.nudDoSoakSec.Value);           

           this.fluidProgram.RuntimeSettings.IsAutoSoakSpan = this.chxAutoSoakSpan.Checked;
            #endregion

            // data 参数保存
            this.dataControl.SetParam(this.fluidProgram);

            this.Close();
            CompareObj.CompareProperty(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
            CompareObj.CompareField(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditBoardHeight_Click(object sender, EventArgs e)
        {
            if (new EditBoardHeightForm(this.fluidProgram).ShowDialog() == DialogResult.OK)
            {
                this.txtBoardHeight.Text = this.fluidProgram.RuntimeSettings.StandardBoardHeight.ToString("0.000");
                this.nudMaxHeight.Value = (decimal)this.fluidProgram.RuntimeSettings.MaxTolerance;
                this.nudMinHeight.Value = (decimal)this.fluidProgram.RuntimeSettings.MinTolerance;
            }
        }

        private void btnEditConveyor2Origin_Click(object sender, EventArgs e)
        {
            if (new EditConveyor2Origin().ShowDialog() == DialogResult.OK)
            {
                this.txtConveyor2Origin.Text = this.fluidProgram.Conveyor2OriginOffset.ToString();
            }
        }

        private void cbxFlyEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.tbxFlyAcc.Enabled = this.cbxFlyEnable.Checked;
            this.tbxFlySpeed.Enabled = this.cbxFlyEnable.Checked;
            this.cbxIsRowFirst.Enabled = this.cbxFlyEnable.Checked;
            this.tbxCornerSpeed.Enabled = this.cbxFlyEnable.Checked;
            this.tbxPreDistance.Enabled = this.cbxFlyEnable.Checked;
            this.cbxAdjustFlyOffset.Enabled = this.cbxFlyEnable.Checked;
            this.cbxNGReshoot.Enabled = this.cbxFlyEnable.Checked;
            this.cbxFlyOriginPos.Enabled = this.cbxFlyEnable.Checked;
            this.tbxThreadCount.Enabled = this.cbxFlyEnable.Checked;
        }

        private void ckbSync_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbSync.Checked)
            {
                this.fluidProgram.RuntimeSettings.SingleDropWeight = Machine.Instance.Valve1.weightPrm.SingleDotWeight;
                this.nudSingleDotWt.Value = (decimal)this.fluidProgram.RuntimeSettings.SingleDropWeight;
            }
        }

        private void ProgramSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.关闭程序参数设定界面时);
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.关闭程序参数设定界面时);
            }
        }

        public void LoadDataControl()
        {
            switch (Machine.Instance.Setting.MachineSelect)
            {
                case MachineSelection.RTV:
                    this.dataControl = new RTVDataControl();
                    break;
                case MachineSelection.AFN:
                    this.dataControl = new AmphnolDataControl();
                    break;
                default:
                    this.dataControl = new DefaultDataControl();
                    break;
            }
        }
        #region 
        private void btnSoakPostion_Click(object sender, EventArgs e)
        {
            new DialogSoakSetting().ShowDialog();
        }
        #endregion

        private void btnTeachAbsZ_Click(object sender, EventArgs e)
        {
            this.fluidProgram.RuntimeSettings.BoardZValue = Machine.Instance.Robot.PosZ;
            this.tbAbsZValue.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
        }
    }
}
