using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.Conveyor.Forms
{
    public partial class ConveyorSettingControl : UserControlEx
    {
        private int conveyorNo;
        private ConveyorPrm converyorPrmBackUp;
        public ConveyorSettingControl()
        {
            ConveyorPrmMgr.Instance.Load();
            this.ReadLanguageResources();
            InitializeComponent();
            if (Machine.Instance.Setting.MachineSelect != MachineSelection.RTV)
            {
                this.tabRTVSetting.Hide();
            }
        }
        public void SetUp(int conveyorNo)
        {
            this.conveyorNo = conveyorNo;

            #region 基础参数
            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode == ConveyorSubsiteMode.Singel) 
            {
                this.rdoSingleSite.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode == ConveyorSubsiteMode.Triple)
            {
                this.rdoTripleSites.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode == ConveyorSubsiteMode.PreAndDispense)
            {
                this.rdoPreAndDispense.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode == ConveyorSubsiteMode.DispenseAndInsulation)
            {
                this.rdoDispenseAndInsulation.Checked = true;
            }

            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection == BoardDirection.LeftToRight)
            {
                this.rdoLeft2Right.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection == BoardDirection.RightToLeft)
            {
                this.rdoRight2Left.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection == BoardDirection.LeftToLeft)
            {
                this.rdoLeft2Left.Checked = true;
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection == BoardDirection.RightToRight)
            {
                this.rdoRight2Right.Checked = true;
            }

            this.nudConveyorSpeed.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).Speed;

            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).EnterIsSMEMA)
            {
                this.rdoEnterSMEMAMode.Checked = true;
            }
            else
            {
                this.rdoEnterStandaloneMode.Checked = true;
            }

            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ExitIsSMEMA)
            {
                this.rdoExitSMEMAMode.Checked = true;
            }
            else
            {
                this.rdoExitStandaloneMode.Checked = true;
            }

            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsPulse)
            {
                this.rdoPulseSignal.Checked = true;
            }
            else
            {
                this.rdoContinuousSignal.Checked = true;
            }

            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsSingleInteraction)
            {
                this.chkSingleInteraction.Checked = true;
            }
            else
            {
                this.chkSingleInteraction.Checked = false;
            }

            this.nudPulseTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PulseTime;

            if(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ReceiveSMEMAIsPulse)
            {
                this.rdoReceiveIsPulse.Checked = true;
            }
            else
            {
                this.rdoReceiveIsContinues.Checked = true;
            }

            this.nudUpStreamStuckTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).UpStramStuckTime;

            this.nudDownStreamStuckTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).DownStreamStuckTime;


            if (ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).AutoExitBoard)
            {
                this.rdoStraightExit.Checked = true;
            }
            else
            {
                this.rdoManualTake.Checked = true;
            }

            this.ckbWaitOutInExitSensor.Checked = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).IsWaitOutInExitSensor;

            #endregion

            #region 运行参数
            this.nudCheckTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).CheckTime;

            this.nudStopperDelay.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).StopperUpDelay;

            this.nudLiftDelay.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).LiftUpDelay;

            this.nudStuckCoefficent.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).StuckCoefficent;

            this.nudAccTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).AccTime ;

            //预热站参数
            this.nudPreLongMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.StuckTime);

            this.nudPreShortMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.BoardArrivedDelay);

            this.nudPreHeatingTime.Value = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.HeatingTime;

            //点胶站参数
            this.nudWorkingSiteLongMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.EnterStuckTime);

            this.nudWorkingSiteShortMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.BoardArrivedDelay);

            this.nudWorkingSiteExitDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.ExitStuckTime);

            this.nudWorkingHeatingTime.Value = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.HeatingTime;

            //成品站参数
            this.nudFinishedSiteLongMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.StuckTime);

            this.nudFinishedSiteShortMoveDistance.Value = this.Time2Distance(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.BoardArrivedDelay);

            this.nudFinishedSiteHeatingTime.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.HeatingTime;

            this.nudBoardLeftDelay.Value = (decimal)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardLeftDelay;
            #endregion

            #region RTV参数
            this.chkIOEnable.Checked = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.IOEnable;
            this.txtIOTime.Text = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.IOStuckTime.ToString();
            this.txtUpConveyorTIme.Text = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.UpConveyorTurnTime.ToString();
            this.txtDownConveyorTime.Text = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.DownConveyorTurnTime.ToString();
            #endregion

            #region 其他参数设置
            this.cbxConveyorScan.Checked = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ConveyorScan;
            #endregion

            this.UpdateEnable();
            this.converyorPrmBackUp = (ConveyorPrm)ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).Clone();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 基础设定
            if (this.rdoSingleSite.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode =  ConveyorSubsiteMode.Singel;
            }
            else if (this.rdoTripleSites.Checked) 
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode = ConveyorSubsiteMode.Triple;
            }
            else if (this.rdoPreAndDispense.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode = ConveyorSubsiteMode.PreAndDispense;
            }
            else if (this.rdoDispenseAndInsulation.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SubsiteMode = ConveyorSubsiteMode.DispenseAndInsulation;
            }

            if (this.rdoLeft2Right.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection = BoardDirection.LeftToRight;
            }
            else if (this.rdoRight2Left.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection = BoardDirection.RightToLeft;
            }
            else if (this.rdoLeft2Left.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection = BoardDirection.LeftToLeft;
            }
            else if (this.rdoRight2Right.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardDirection = BoardDirection.RightToRight;
            }

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).Speed = (double)this.nudConveyorSpeed.Value;

            if (this.rdoEnterSMEMAMode.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).EnterIsSMEMA = true;
            }
            else if (this.rdoEnterStandaloneMode.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).EnterIsSMEMA = false;
            }

            if (this.rdoExitSMEMAMode.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ExitIsSMEMA = true;
            }
            else if (this.rdoExitStandaloneMode.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ExitIsSMEMA = false;
            }

            if (this.rdoContinuousSignal.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsPulse = false;
            }
            else if (this.rdoPulseSignal.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsPulse = true;
            }

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PulseTime = (int)this.nudPulseTime.Value;

            if (this.rdoReceiveIsContinues.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ReceiveSMEMAIsPulse = false;
            }
            else if (this.rdoReceiveIsPulse.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ReceiveSMEMAIsPulse = true;
            }

            if (this.chkSingleInteraction.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsSingleInteraction = true;
            }
            else 
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).SMEMAIsSingleInteraction = false;
            }

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).IsWaitOutInExitSensor = this.ckbWaitOutInExitSensor.Checked;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).UpStramStuckTime = (int)this.nudUpStreamStuckTime.Value;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).DownStreamStuckTime = (int)this.nudDownStreamStuckTime.Value;


            if (this.rdoStraightExit.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).AutoExitBoard = true;
            }
            else if (this.rdoManualTake.Checked)
            {
                ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).AutoExitBoard = false;
            }
            #endregion

            #region 运行参数设定
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).CheckTime = (int)this.nudCheckTime.Value;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).StopperUpDelay = (int)this.nudStopperDelay.Value;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).LiftUpDelay = (int)this.nudLiftDelay.Value;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).StuckCoefficent = (int)this.nudStuckCoefficent.Value;

            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).AccTime = (double)this.nudAccTime.Value;

            //预热站参数
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.HeatingTime = (long)this.nudPreHeatingTime.Value;
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.StuckTime = this.Distance2Time(this.nudPreLongMoveDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).PreSitePrm.BoardArrivedDelay = this.Distance2Time(this.nudPreShortMoveDistance.Value);

            //点胶站参数
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.EnterStuckTime = this.Distance2Time((int)this.nudWorkingSiteLongMoveDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.BoardArrivedDelay = this.Distance2Time((int)this.nudWorkingSiteShortMoveDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.ExitStuckTime = this.Distance2Time((int)this.nudWorkingSiteExitDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).WorkingSitePrm.HeatingTime = (long)this.nudWorkingHeatingTime.Value;

            //成品站参数
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.StuckTime = this.Distance2Time((int)this.nudFinishedSiteLongMoveDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.BoardArrivedDelay = this.Distance2Time((int)this.nudFinishedSiteShortMoveDistance.Value);
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).FinishedSitePrm.HeatingTime = (long)this.nudFinishedSiteHeatingTime.Value;
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).BoardLeftDelay = (int)this.nudBoardLeftDelay.Value;
            #endregion

            #region RTV参数
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.IOEnable = this.chkIOEnable.Checked;
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.IOStuckTime= this.txtIOTime.Value;
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.UpConveyorTurnTime = this.txtUpConveyorTIme.Value;
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).RTVPrm.DownConveyorTurnTime = this.txtDownConveyorTime.Value;
            #endregion

            #region 其他参数设置
            ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).ConveyorScan = this.cbxConveyorScan.Checked;
            #endregion

            //打开控制器并写入速度
            try
            {
                SensorMgr.Instance.Conveyor1.Write("L5 " + this.ChangeSpeedToController(this.nudConveyorSpeed.Value) + "F");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ConveyorPrmMgr.Instance.Save();
            if (this.converyorPrmBackUp!=null && ConveyorPrmMgr.Instance.FindBy(this.conveyorNo)!=null)
            {
                CompareObj.CompareProperty(ConveyorPrmMgr.Instance.FindBy(this.conveyorNo), this.converyorPrmBackUp, null, this.GetType().Name);
            }            
        }
        private int Distance2Time(decimal distance)
        {
            double totalDistance = (double)distance * (double)this.nudStuckCoefficent.Value;
            double speed = (double)this.nudConveyorSpeed.Value;
            double time = (totalDistance / speed)*1000;
            return (int)Math.Round(time);
        }
        private decimal Time2Distance(int time)
        {
            double speed = ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).Speed;
            double totalDistance = (speed * time / 1000) / ConveyorPrmMgr.Instance.FindBy(this.conveyorNo).StuckCoefficent;
            return (decimal)totalDistance;
        }
        private void UpdateUI(object sender, EventArgs e)
        {
            this.UpdateEnable();
            this.UpdateChecked();
        }

        private void UpdateEnable()
        {
            this.grpPresite.Enabled = true;
            this.grpFinishedSite.Enabled = true;
            this.rdoLeft2Left.Enabled = true;
            this.rdoRight2Right.Enabled = true;
            this.grpStandaloneMode.Enabled = true;
            this.grpSMEMAMode.Enabled = true;

            if (this.rdoSingleSite.Checked)
            {
                this.grpPresite.Enabled = false;
                this.grpFinishedSite.Enabled = false;
            }
            if (!this.rdoSingleSite.Checked)
            {
                this.rdoLeft2Left.Enabled = false;
                this.rdoRight2Right.Enabled = false;
            }
        }

        private void UpdateChecked()
        {
            if (this.rdoTripleSites.Checked)
            {
                if (this.rdoLeft2Left.Checked || this.rdoRight2Right.Checked)
                {
                    this.rdoLeft2Right.Checked = true;
                    this.rdoRight2Left.Checked = false;
                    this.rdoLeft2Left.Checked = false;
                    this.rdoRight2Right.Checked = false;
                }
            }
        }

        private string ChangeSpeedToController(decimal speed)
        {
            double controllerValue = 0;
            controllerValue = (1000000 * 52.124) / ((double)speed * 2 * 6400);
            return Math.Round(controllerValue).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.conveyorNo == 0)
            {
                string s = string.Format("轨道模式：{0}", FlagBitMgr.Instance.FindBy(0).State.IntegralState);
                this.label1.Text = s;
            }
            else
            {
                string s = string.Format("轨道模式：{0}", FlagBitMgr.Instance.FindBy(1).State.IntegralState);
                this.label1.Text = s;
            }
        }
    }
}
