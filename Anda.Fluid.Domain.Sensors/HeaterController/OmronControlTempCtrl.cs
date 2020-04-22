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

namespace Anda.Fluid.Domain.Sensors.HeaterController
{
    public partial class OmronControlTempCtrl : UserControlEx
    {
        private string SetErrMsg = "设置失败\r\n请重试或检查数值\r\n值不能小于等于0！";
        private string SetTempOffsetErrMsg = "设置失败\r\n请重试或检查数值\r\n漂移值必须小于等于0！";
        private string TempInfo = "温度";
        private string TempUpperLimitInfo = "温度上限";
        private string TempLowerLimitInfo = "温度下限";
        private string TempOffsetInfo = "温度漂移";
        public OmronControlTempCtrl()
        {
            InitializeComponent();
            this.ReadLanguageResources();

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.rdoValve2.Enabled = false;
            }
            this.GetValveTemp(0);
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
                     Convert.ToDouble(this.nudTemp.Value),0, Machine.Instance.HeaterController1));

                HeaterPrmMgr.Instance.FindBy(0).Standard[0] = (double)this.nudTemp.Value;
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                     Convert.ToDouble(this.nudTemp.Value),0, Machine.Instance.HeaterController2));
                HeaterPrmMgr.Instance.FindBy(1).Standard[0] = (double)this.nudTemp.Value;
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
                HeaterMessage hm = new HeaterMessage(HeaterMsg.设置温度上限值, Convert.ToDouble(this.nudUpAlarm.Value),0, Machine.Instance.HeaterController1);
                Machine.Instance.HeaterController1.Fire(hm);

                hm = new HeaterMessage(HeaterMsg.获取温度上限值, Machine.Instance.HeaterController1,0);
                Machine.Instance.HeaterController1.Fire(hm);

                HeaterPrmMgr.Instance.FindBy(0).High[0] = (double)this.nudUpAlarm.Value;
            }
            else
            {
                HeaterMessage hm = new HeaterMessage(HeaterMsg.设置温度上限值, Convert.ToDouble(this.nudUpAlarm.Value),0, Machine.Instance.HeaterController2);
                Machine.Instance.HeaterController2.Fire(hm);

                hm = new HeaterMessage(HeaterMsg.获取温度上限值, Machine.Instance.HeaterController2,0);
                Machine.Instance.HeaterController2.Fire(hm);

                HeaterPrmMgr.Instance.FindBy(1).High[0] = (double)this.nudUpAlarm.Value;
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
                     Convert.ToDouble(this.nudDownAlarm.Value),0, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1,0));

                HeaterPrmMgr.Instance.FindBy(0).Low[0] = (double)this.nudDownAlarm.Value;
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                    Convert.ToDouble(this.nudDownAlarm.Value),0, Machine.Instance.HeaterController2));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController2,0));

                HeaterPrmMgr.Instance.FindBy(1).Low[0] = (double)this.nudDownAlarm.Value;
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
                    Convert.ToDouble(this.nudTempOffset.Value),0, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController1,0));

                HeaterPrmMgr.Instance.FindBy(0).Offset[0] = (double)this.nudTempOffset.Value;
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.设置温度漂移值,
                     Convert.ToDouble(this.nudTempOffset.Value),0, Machine.Instance.HeaterController2));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController2,0));

                HeaterPrmMgr.Instance.FindBy(1).Offset[0] = (double)this.nudTempOffset.Value;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.rdoValve1.Checked)
            {
                this.lblTemp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[0].ToString());
                this.lblUpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[0].ToString());
                this.lblDownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[0].ToString());
                this.lblTempOffset.Text = string.Format("{0}：{1} ℃", TempOffsetInfo, Machine.Instance.HeaterController1.TempOffset[0].ToString());
            }
            if (this.rdoValve2.Checked)
            {
                this.lblTemp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController2.CurrentTemp[0].ToString());
                this.lblUpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController2.TempHighValue[0].ToString());
                this.lblDownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController2.TempLowValue[0].ToString());
                this.lblTempOffset.Text = string.Format("{0}：{1} ℃", TempOffsetInfo, Machine.Instance.HeaterController2.TempOffset[0].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int valveNo = 0;
            if (this.rdoValve1.Checked)
            {
                valveNo = 0;
            }
            else
            {
                valveNo = 1;
            }

            if (this.rdoContinueHeating.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(valveNo).IsContinuseHeating = true;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(valveNo).IsContinuseHeating = false;
            }
            if (this.chkIdleClosed.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(valveNo).CloseHeatingWhenIdle = true;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(valveNo).CloseHeatingWhenIdle = false;
            }
            HeaterPrmMgr.Instance.FindBy(valveNo).IdleDecideTime = this.intTxtClosedDecideTime.Value;

            HeaterPrmMgr.Instance.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valveNo"></param>
        private void GetValveTemp(int valveNo)
        {
            //向温控器发送相关消息
            if (valveNo == 0)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                Machine.Instance.HeaterController1, 0));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 0));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController1, 0));
            }
            else
            {
                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController2, 0));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController2, 0));

                Machine.Instance.HeaterController2.Fire(new HeaterMessage(HeaterMsg.获取温度漂移值,
                    Machine.Instance.HeaterController2, 0));
            }

            //获取温度相关数据
            this.nudTemp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(valveNo).Standard[0];
            this.nudUpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(valveNo).High[0];
            this.nudDownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(valveNo).Low[0];
            this.nudTempOffset.Value = (decimal)HeaterPrmMgr.Instance.FindBy(valveNo).Offset[0];

            //获取相关设置
            if (HeaterPrmMgr.Instance.FindBy(valveNo).IsContinuseHeating)
            {
                this.rdoContinueHeating.Checked = true;
            }
            else
            {
                this.rdoManufactureHeating.Checked = true;
            }
            if (HeaterPrmMgr.Instance.FindBy(valveNo).CloseHeatingWhenIdle)
            {
                this.chkIdleClosed.Checked = true;
            }
            else
            {
                this.chkIdleClosed.Checked = false;
            }
            this.intTxtClosedDecideTime.Text = HeaterPrmMgr.Instance.FindBy(valveNo).IdleDecideTime.ToString();

            //获取该温控器是否报警
            if (valveNo == 0)
            {
                if (Machine.Instance.HeaterController1.AlarmEnable)
                {
                    this.chkAlarmEnable.Checked = true;
                }
                else
                {
                    this.chkAlarmEnable.Checked = false;
                }
            }
            else
            {
                if (Machine.Instance.HeaterController2.AlarmEnable)
                {
                    this.chkAlarmEnable.Checked = true;
                }
                else
                {
                    this.chkAlarmEnable.Checked = false;
                }
            }
        }

        private void rdoValve1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoValve1.Checked)
            {
                this.GetValveTemp(0);
            }
            else
            {
                this.GetValveTemp(1);
            }
        }

        private void rdoContinueHeating_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoContinueHeating.Checked == true)
            {
                this.chkIdleClosed.Enabled = false;
            }
            else
            {
                this.chkIdleClosed.Enabled = true;
            }
        }

        private void chkAlarmEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoValve1.Checked)
            {
                Machine.Instance.HeaterController1.AlarmEnable = this.chkAlarmEnable.Checked;
            }
            else
            {
                Machine.Instance.HeaterController2.AlarmEnable = this.chkAlarmEnable.Checked;
            }
        }
    }
}
