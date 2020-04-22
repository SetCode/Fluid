using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Heater;
using System.Threading;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class UserControlTempCtrl : UserControlEx
    {
        private string SetErrMsg = "设置失败\r\n请重试或检查数值\r\n值不能小于等于0！";
        private string SetTempOffsetErrMsg = "设置失败\r\n请重试或检查数值\r\n漂移值必须小于等于0！";
        private string TempInfo = "温度";
        private string TempUpperLimitInfo = "温度上限";
        private string TempLowerLimitInfo = "温度下限";
        private string TempOffsetInfo = "温度漂移";
        public UserControlTempCtrl()
        {
            InitializeComponent();
            this.ReadLanguageResources();

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.rdoValve2.Enabled = false;
            }

            this.GetValve1Temp();
        }
        public override void ReadLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.ReadKeyValueFromResources("SetErrMsg");
            this.ReadKeyValueFromResources("SetTempOffsetErrMsg");
            this.ReadKeyValueFromResources("TempInfo");
            this.ReadKeyValueFromResources("TempUpperLimitInfo");
            this.ReadKeyValueFromResources("TempLowerLimitInfo");
            this.ReadKeyValueFromResources("TempOffsetInfo");
        }
        public override void SaveLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
        
            this.SaveKeyValueToResources("SetErrMsg", SetErrMsg);
            this.SaveKeyValueToResources("SetTempOffsetErrMsg", SetErrMsg);
            this.SaveKeyValueToResources("TempInfo", TempInfo);
            this.SaveKeyValueToResources("TempUpperLimitInfo", TempUpperLimitInfo);
            this.SaveKeyValueToResources("TempLowerLimitInfo", TempLowerLimitInfo);
            this.SaveKeyValueToResources("TempOffsetInfo", TempOffsetInfo);
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }
        private void btnSetTemp_Click(object sender, EventArgs e)
        {
            if (this.nudTemp.Value <= 0)
            {
                this.richTxtMsg.Text = SetErrMsg;
                return;
            }
            this.richTxtMsg.Text = null;
            if (this.rdoValve1.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                     Convert.ToDouble(this.nudTemp.Value), Machine.Instance.HeaterController1));
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                     Convert.ToDouble(this.nudTemp.Value), Machine.Instance.HeaterController2));
            }
        }

        private void btnSetAlarmUp_Click(object sender, EventArgs e)
        {
            if (this.nudUpAlarm.Value <= 0)
            {
                this.richTxtMsg.Text = SetErrMsg;
                return;
            }
            this.richTxtMsg.Text = null;

            if (this.rdoValve1.Checked)
            {
                HeaterMessage hm = new HeaterMessage(HeaterMsg.设置温度上限值, Convert.ToDouble(this.nudUpAlarm.Value), Machine.Instance.HeaterController1);
                Machine.Instance.HeaterController1.Fire(hm);

                hm = new HeaterMessage(HeaterMsg.获取温度上限值, Machine.Instance.HeaterController1);
                Machine.Instance.HeaterController1.Fire(hm);
            }
            else
            {
                HeaterMessage hm = new HeaterMessage(HeaterMsg.设置温度上限值, Convert.ToDouble(this.nudUpAlarm.Value), Machine.Instance.HeaterController2);
                Machine.Instance.HeaterController2.Fire(hm);

                hm = new HeaterMessage(HeaterMsg.获取温度上限值, Machine.Instance.HeaterController2);
                Machine.Instance.HeaterController2.Fire(hm);
            }
                     
        }

        private void btnSetAlarmLow_Click(object sender, EventArgs e)
        {
            if (this.nudDownAlarm.Value <= 0)
            {
                this.richTxtMsg.Text = SetErrMsg;
                return;
            }
            this.richTxtMsg.Text = null;

            if (this.rdoValve1.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudDownAlarm.Value), Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1));
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                    Convert.ToDouble(this.nudDownAlarm.Value), Machine.Instance.HeaterController2));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController2));
            }            
        }

        private void btnSetTempOffset_Click(object sender, EventArgs e)
        {
            if (this.nudTempOffset.Value >= 0)
            {
                this.richTxtMsg.Text = SetTempOffsetErrMsg;
                return;
            }
            this.richTxtMsg.Text = null;

            if (this.rdoValve1.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度漂移值,
                    Convert.ToDouble(this.nudTempOffset.Value), Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController1));
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置温度漂移值,
                     Convert.ToDouble(this.nudTempOffset.Value), Machine.Instance.HeaterController2));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController2));
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.rdoValve1.Checked)
            {
                this.lblTemp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp.ToString());
                this.lblUpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue.ToString());
                this.lblDownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue.ToString());
                this.lblTempOffset.Text = string.Format("{0}：{1} ℃", TempOffsetInfo, Machine.Instance.HeaterController1.TempOffset.ToString());
            }
            if (this.rdoValve2.Checked)
            {
                this.lblTemp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController2.CurrentTemp.ToString());
                this.lblUpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController2.TempHighValue.ToString());
                this.lblDownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController2.TempLowValue.ToString());
                this.lblTempOffset.Text = string.Format("{0}：{1} ℃", TempOffsetInfo, Machine.Instance.HeaterController2.TempOffset.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HeaterPrmMgr.Instance.FindBy(0).Standard = Machine.Instance.HeaterController1.CurrentTemp;
            HeaterPrmMgr.Instance.FindBy(0).High = Machine.Instance.HeaterController1.TempHighValue;
            HeaterPrmMgr.Instance.FindBy(0).Low = Machine.Instance.HeaterController1.TempLowValue;
            HeaterPrmMgr.Instance.FindBy(0).Offset = Machine.Instance.HeaterController1.TempOffset;

            HeaterPrmMgr.Instance.FindBy(1).Standard = Machine.Instance.HeaterController2.CurrentTemp;
            HeaterPrmMgr.Instance.FindBy(1).High = Machine.Instance.HeaterController2.TempHighValue;
            HeaterPrmMgr.Instance.FindBy(1).Low = Machine.Instance.HeaterController2.TempLowValue;
            HeaterPrmMgr.Instance.FindBy(1).Offset = Machine.Instance.HeaterController2.TempOffset;
            HeaterPrmMgr.Instance.Save();
        }

        private void GetValve1Temp()
        {
            Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                Machine.Instance.HeaterController1));

            Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                Machine.Instance.HeaterController1));

            Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                Machine.Instance.HeaterController1));

            this.nudTemp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard;
            this.nudUpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High;
            this.nudDownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low;
            this.nudTempOffset.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Offset;
        }

        private void GetValve2Temp()
        {
            Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                Machine.Instance.HeaterController2));

            Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                Machine.Instance.HeaterController2));

            Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                Machine.Instance.HeaterController2));

            this.nudTemp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(1).Standard;
            this.nudUpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(1).High;
            this.nudDownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(1).Low;
            this.nudTempOffset.Value = (decimal)HeaterPrmMgr.Instance.FindBy(1).Offset;
        }

        private void rdoValve1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoValve1.Checked)
            {
                this.GetValve1Temp();
            }
            else
            {
                this.GetValve2Temp();
            }
        }
    }
}
