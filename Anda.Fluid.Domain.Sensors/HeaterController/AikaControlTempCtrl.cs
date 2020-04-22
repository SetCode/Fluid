using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive;
using System.Threading;

namespace Anda.Fluid.Domain.Sensors.HeaterController
{
    public partial class AikaControlTempCtrl : UserControl
    {
        private string TempInfo = "温度";
        private string TempUpperLimitInfo = "温度上限";
        private string TempLowerLimitInfo = "温度下限";
        public AikaControlTempCtrl()
        {
            InitializeComponent();

            this.SetUp();
        }

        private void SetUp()
        {
            HeaterPrmMgr.Instance.Load();
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[0])
            {
                this.chkChanel1Enable.Checked = true;
                this.nudChanel1Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[0];
                this.nudChanel1UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[0];
                this.nudChanel1DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[0];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[1])
            {
                this.chkChanel2Enable.Checked = true;
                this.nudChanel2Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[1];
                this.nudChanel2UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[1];
                this.nudChanel2DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[1];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[2])
            {
                this.chkChanel3Enable.Checked = true;
                this.nudChanel3Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[2];
                this.nudChanel3UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[2];
                this.nudChanel3DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[2];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[3])
            {
                this.chkChanel4Enable.Checked = true;
                this.nudChanel4Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[3];
                this.nudChanel4UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[3];
                this.nudChanel4DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[3];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[4])
            {
                this.chkChanel5Enable.Checked = true;
                this.nudChanel5Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[4];
                this.nudChanel5UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[4];
                this.nudChanel5DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[4];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[5])
            {
                this.chkChanel6Enable.Checked = true;
                this.nudChanel6Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[5];
                this.nudChanel6UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[5];
                this.nudChanel6DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[5];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[6])
            {
                this.chkChanel7Enable.Checked = true;
                this.nudChanel7Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[6];
                this.nudChanel7UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[6];
                this.nudChanel7DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[6];
            }
            if (HeaterPrmMgr.Instance.FindBy(0).acitveChanel[7])
            {
                this.chkChanel8Enable.Checked = true;
                this.nudChanel8Temp.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Standard[7];
                this.nudChanel8UpAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).High[7];
                this.nudChanel8DownAlarm.Value = (decimal)HeaterPrmMgr.Instance.FindBy(0).Low[7];
            }

            if (HeaterPrmMgr.Instance.FindBy(0).IsContinuseHeating)
            {
                this.rdoContinueHeating.Checked = true;
            }
            else
            {
                this.rdoManufactureHeating.Checked = true;
            }

            if (HeaterPrmMgr.Instance.FindBy(0).CloseHeatingWhenIdle)
            {
                this.chkIdleClosed.Checked = true;
            }
            else
            {
                this.chkIdleClosed.Checked = false;
            }

            this.intTxtClosedDecideTime.Text = HeaterPrmMgr.Instance.FindBy(0).IdleDecideTime.ToString();

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (this.chkChanel1Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel1Temp.Value), 0, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值, 
                    Convert.ToDouble(this.nudChanel1UpAlarm.Value), 0, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值, 
                    Machine.Instance.HeaterController1, 0));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel1DownAlarm.Value), 0, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 0));
            }

            if (this.chkChanel2Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel2Temp.Value), 1, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel2UpAlarm.Value), 1, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值, 
                    Machine.Instance.HeaterController1, 1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel2DownAlarm.Value), 1, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 1));
            }

            if (this.chkChanel3Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel3Temp.Value), 2, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel3UpAlarm.Value), 2, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 2));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel3DownAlarm.Value), 2, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 2));
            }

            if (this.chkChanel4Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel4Temp.Value), 3, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel4UpAlarm.Value), 3, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 3));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel4DownAlarm.Value), 3, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 3));
            }

            if (this.chkChanel5Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel5Temp.Value), 4, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel5UpAlarm.Value), 4, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 4));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel5DownAlarm.Value), 4, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 4));
            }

            if (this.chkChanel6Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel6Temp.Value), 5, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel6UpAlarm.Value), 5, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 5));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel6DownAlarm.Value), 5, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 5));
            }

            if (this.chkChanel7Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel7Temp.Value), 6, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel7UpAlarm.Value), 6, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 6));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel7DownAlarm.Value), 6, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 6));
            }

            if (this.chkChanel8Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    Convert.ToDouble(this.nudChanel8Temp.Value), 7, Machine.Instance.HeaterController1));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    Convert.ToDouble(this.nudChanel8UpAlarm.Value), 7, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度上限值,
                    Machine.Instance.HeaterController1, 7));

                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                     Convert.ToDouble(this.nudChanel8DownAlarm.Value), 7, Machine.Instance.HeaterController1));
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.获取温度下限值,
                    Machine.Instance.HeaterController1, 7));
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.chkChanel1Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[0] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[0] = (double)this.nudChanel1Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[0] = (double)this.nudChanel1UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[0] = (double)this.nudChanel1DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[0] = false;
            }

            if (this.chkChanel2Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[1] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[1] = (double)this.nudChanel2Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[1] = (double)this.nudChanel2UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[1] = (double)this.nudChanel2DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[1] = false;
            }

            if (this.chkChanel3Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[2] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[2] = (double)this.nudChanel3Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[2] = (double)this.nudChanel3UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[2] = (double)this.nudChanel3DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[2] = false;
            }

            if (this.chkChanel4Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[3] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[3] = (double)this.nudChanel4Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[3] = (double)this.nudChanel4UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[3] = (double)this.nudChanel4DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[3] = false;
            }

            if (this.chkChanel5Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[4] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[4] = (double)this.nudChanel5Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[4] = (double)this.nudChanel5UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[4] = (double)this.nudChanel5DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[4] = false;
            }

            if (this.chkChanel6Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[5] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[5] = (double)this.nudChanel6Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[5] = (double)this.nudChanel6UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[5] = (double)this.nudChanel6DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[5] = false;
            }

            if (this.chkChanel7Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[6] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[6] = (double)this.nudChanel7Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[6] = (double)this.nudChanel7UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[6] = (double)this.nudChanel7DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[6] = false;
            }

            if (this.chkChanel8Enable.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[7] = true;
                HeaterPrmMgr.Instance.FindBy(0).Standard[7] = (double)this.nudChanel8Temp.Value;
                HeaterPrmMgr.Instance.FindBy(0).High[7] = (double)this.nudChanel8UpAlarm.Value;
                HeaterPrmMgr.Instance.FindBy(0).Low[7] = (double)this.nudChanel8DownAlarm.Value;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).acitveChanel[7] = false;
            }

            if (this.rdoContinueHeating.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).IsContinuseHeating = true;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).IsContinuseHeating = false;
            }
            if (this.chkIdleClosed.Checked)
            {
                HeaterPrmMgr.Instance.FindBy(0).CloseHeatingWhenIdle = true;
            }
            else
            {
                HeaterPrmMgr.Instance.FindBy(0).CloseHeatingWhenIdle = false;
            }
            HeaterPrmMgr.Instance.FindBy(0).IdleDecideTime = this.intTxtClosedDecideTime.Value;

            HeaterPrmMgr.Instance.Save();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HeaterPrmMgr.Instance.FindBy(0).acitveChanel.Length; i++)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.停止加热,
                    Machine.Instance.HeaterController1, i));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.chkChanel1Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 0));
            }

            if (this.chkChanel2Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 1));
            }

            if (this.chkChanel3Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 2));
            }

            if (this.chkChanel4Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 3));
            }

            if (this.chkChanel5Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 4));
            }

            if (this.chkChanel6Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 5));
            }

            if (this.chkChanel7Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 6));
            }

            if (this.chkChanel8Enable.Checked)
            {
                Machine.Instance.HeaterController1.Fire(new HeaterMessage(HeaterMsg.开始加热,
                    Machine.Instance.HeaterController1, 7));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.chkChanel1Enable.Checked)
            {
                this.lblChanel1Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[0].ToString());
                this.lblChanel1UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[0].ToString());
                this.lblChanel1DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[0].ToString());
            }
            if (this.chkChanel2Enable.Checked)
            {
                this.lblChanel2Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[1].ToString());
                this.lblChanel2UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[1].ToString());
                this.lblChanel2DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[1].ToString());
            }
            if (this.chkChanel3Enable.Checked)
            {
                this.lblChanel3Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[2].ToString());
                this.lblChanel3UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[2].ToString());
                this.lblChanel3DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[2].ToString());
            }
            if (this.chkChanel4Enable.Checked)
            {
                this.lblChanel4Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[3].ToString());
                this.lblChanel4UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[3].ToString());
                this.lblChanel4DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[3].ToString());
            }
            if (this.chkChanel5Enable.Checked)
            {
                this.lblChanel5Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[4].ToString());
                this.lblChanel5UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[4].ToString());
                this.lblChanel5DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[4].ToString());
            }
            if (this.chkChanel6Enable.Checked)
            {
                this.lblChanel6Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[5].ToString());
                this.lblChanel6UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[5].ToString());
                this.lblChanel6DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[5].ToString());
            }
            if (this.chkChanel7Enable.Checked)
            {
                this.lblChanel7Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[6].ToString());
                this.lblChanel7UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[6].ToString());
                this.lblChanel7DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[6].ToString());
            }
            if (this.chkChanel8Enable.Checked)
            {
                this.lblChanel8Temp.Text = string.Format("{0}：{1} ℃", TempInfo, Machine.Instance.HeaterController1.CurrentTemp[7].ToString());
                this.lblChanel8UpAlarm.Text = string.Format("{0}：{1} ℃", TempUpperLimitInfo, Machine.Instance.HeaterController1.TempHighValue[7].ToString());
                this.lblChanel8DownAlarm.Text = string.Format("{0}：{1} ℃", TempLowerLimitInfo, Machine.Instance.HeaterController1.TempLowValue[7].ToString());
            }
        }

        private void rdoContinueHeating_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoContinueHeating.Checked == true)
            {
                this.chkIdleClosed.Enabled = false;
            }
            else
                this.chkIdleClosed.Enabled = true;
        }
    }
}
