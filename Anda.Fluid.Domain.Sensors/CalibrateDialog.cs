using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;
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

namespace Anda.Fluid.Domain.Sensors
{
    public partial class CalibrateDialog : FormEx
    {

        #region  汉化
        private string doneStr = "done";
        private string step0Str = "校准开始....\r\n 请清空胶杯，点击Next进行下一步 ";
        private string step11Str = "清零指令发送成功,点击Next进行下一步";
        private string step12Str = "清零指令发送失败，确认天平是否正常，请点击Cancel";
        private string step21Str = "外校命令发送成功，点击Next进行下一步";
        private string step22Str = "外校命令发送失败，请确认天平是否正常";
        private string step31Str = "读取-200.0000g成功，请放入200g砝码并点击Next进行下一步";
        private string step32Str = "读取-200.0000g失败，请确认天平是否正常，点击Cancel取消校准";
        private string step41Str = "读取+200.0000g成功,点击Next进行下一步";
        private string step42Str = "读取+200.0000g失败，请确认天平是否正常，点击Cancel取消校准";
        private string step51Str = "天平校准成功,点击done完成";
        private string step52Str = "天平校准失败，请确认天平是否正常，点击Cancel取消校准";
        #endregion
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private IScalable scale;
        private int step=0;
        private bool laststepStatus = true;
        private bool stepStatus = false;
        private bool Calibdone = false;

        private bool testFlag = false;
        public CalibrateDialog()
        {
            InitializeComponent();
            lngResources.Add(doneStr, doneStr);
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;           
        }
        public CalibrateDialog Setup(IScalable scale)
        {
            this.scale = scale;
            return this;
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            if (this.HasLngResources())
            {
                lngResources[doneStr] = this.ReadKeyValueFromResources(doneStr);
            }
            
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);
            this.SaveKeyValueToResources(doneStr, lngResources[doneStr]);

        }
        //next button
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.laststepStatus == true)
            {
                step++;
            }
            else
            {
                step+=0;
            }
            this.CalibPross();
        }

        private void CalibPross()
        {
            switch (step)
            {
                case 0:
                    this.setText("校准开始....\r\n请清空胶杯，点击Next进行下一步 ");
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnCancel.Enabled = true;
                    break;
                case 1:
                    this.zeroScale();
                    break;
           
                case 2:
                    this.ExtenCalibrateCmd();
                    break;
                case 3:
                    this.PrintBeforePutWeight();
                    break;
                case 4:
                    this.PrintAfterPutWeight();
                    break;
                case 5:
                    this.Calibrating();
                    break;
                case 6:
                    this.done();
                    break;
                default:
                    break;

            }

        }
        //step=1
        private bool scaleCommunicationTest()
        {           
            if (testFlag)
            {
                this.setText("通讯正常,请清空胶杯，点击Next进行下一步 " + this.step);
                this.stepStatus = true;
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = true;
                this.btnCancel.Enabled = true;
            }
            else
            {
                this.setText("通讯异常，请点击Cancel，返回上级界面重新启动天平并确保天平通讯正常  "+this.step);
                this.stepStatus = false;                
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = true;              
                this.btnCancel.Enabled = true;
            }
            this.laststepStatus = this.stepStatus;
            return this.stepStatus;
        }

     
        private bool zeroScale()
        {
            //scale.Zero();
            if (scale.Zero())
            {
                string str = string.Format("清零指令发送成功,点击Next进行下一步");
                this.setText(str + this.step);
                this.EnableButtons();
                this.stepStatus = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "Scale zero successfully");
            }
            else
            {
                this.setText("清零指令发送失败，确认天平是否正常，请点击Cancel");
                this.stepStatus = false;

                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = true;
                this.btnCancel.Enabled = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "Scale failed to zero");
            }
            
            this.laststepStatus = this.stepStatus;
            return this.stepStatus;
        }
        //STEP=3
        private bool ExtenCalibrateCmd()
        {            
            if (scale.ExternalCali())
            {
                string str = string.Format("外校命令发送成功，点击Next进行下一步");
                this.setText(str);
                this.EnableButtons();
                this.stepStatus = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "send the command ESCW successfully");
            }
            else
            {                
                string str = string.Format("外校命令发送失败，请确认天平是否正常");
                this.setText(str);
                this.stepStatus = false;

                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = true;
                this.btnCancel.Enabled = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "failed to sent the command ESCW");
            }
            this.laststepStatus = this.stepStatus;
            return this.stepStatus;

        }


        private bool PrintBeforePutWeight()
        {
            double weight;
            bool ret = this.readAndReplay(TimeSpan.FromMilliseconds(5000),out weight);
            this.setlblActWeight(weight.ToString());
            if ((0- weight*1000) ==  this.scale.Prm.ScaleCalibWeight)
            {
                string str = string.Format("读取-200.0000g成功，请放入200g砝码并点击Next进行下一步");
                this.setText(str + this.step);
                this.EnableButtons();
                this.stepStatus = true;
                string msg = string.Format("the current value is {0}", weight);
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
            }
            else
            {
                string str = string.Format("读取-200.0000g失败，请确认天平是否正常，点击Cancel取消校准");
                this.setText(str);
                this.EnableButtons();
                this.stepStatus = false;
                string msg = string.Format("the current value is {0}", weight);
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
            }
            this.laststepStatus = this.stepStatus;
            return true;
        }

        public bool PrintAfterPutWeight()
        {
            double weight;
            bool ret = this.readAndReplay(TimeSpan.FromMilliseconds(5000), out weight);
            this.setlblActWeight(weight.ToString());
            if ((weight * 1000) == this.scale.Prm.ScaleCalibWeight)
            {
                string str = string.Format("读取+200.0000g成功,点击Next进行下一步");
                this.setText(str + this.step);
                this.EnableButtons();
                this.stepStatus = true;
                string msg = string.Format("the current value is {0}", weight);
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
            }
            else
            {
                string str = string.Format("读取+200.0000g失败，请确认天平是否正常，点击Cancel取消校准");
                this.setText(str);               
                this.stepStatus = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnCancel.Enabled = true;
                string msg = string.Format("the current value is {0}", weight);
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
            }
            this.laststepStatus = this.stepStatus;
            return true;
        }

        private bool Calibrating()
        {
            double weight;
            bool ret = this.readAndReplay(TimeSpan.FromMilliseconds(10000), out weight);
            this.setlblActWeight(weight.ToString());
            if ((weight ) == this.scale.Prm.ScaleCalibWeight)
            {
                //this.btnNext.Text = "done";
                this.btnNext.Text = "完成";
                string str = string.Format("天平校准成功,点击完成");
                this.setText(str);
                this.EnableButtons();
                this.stepStatus = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "Calibration of the scale is done ");
            }
            else
            {
                string str = string.Format("天平校准失败，请确认天平是否正常，点击Cancel取消校准");
                this.setText(str);

                this.stepStatus = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnCancel.Enabled = true;
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, "Calibration of the scale is unsuccessful ");
            }
            this.laststepStatus = this.stepStatus;
            return true;
        }

        private bool readAndReplay(TimeSpan timeout, out double weight)
        {
            bool readFlag = false;
            DateTime startTime = DateTime.Now;
            
            while (true)
            {
                weight = this.getResult();

                if (Math.Abs(weight * 1000) == this.scale.Prm.ScaleCalibWeight || weight == this.scale.Prm.ScaleCalibWeight)
                {
                    readFlag = true;
                    break;
                }
                if (DateTime.Now - startTime > timeout)
                {
                    readFlag = false;
                    break;
                }
                Thread.Sleep(1000);
            }
            this.setlblActWeight(weight.ToString());
            return readFlag;
        }
        private double getResult()
        {
            string valueStr;

            this.scale.Print(TimeSpan.FromMilliseconds(this.scale.Prm.SingleReadTimeOut), out valueStr);
            double value;
            double.TryParse(valueStr, out value);
            return value;
        }


        //step=5
        private bool done()
        {
            this.Calibdone = true;
            this.Close();
            return true;
        }


        private void CalibrateDialog_Load(object sender, EventArgs e)
        {
            this.step = 0;
            this.CalibPross();
            this.lblValue.Text = this.scale.Prm.ScaleCalibWeight.ToString();

        }
        private void setText(string msg)
        {
            this.txtMsgShow.Text = msg;
        }
        private void setlblActWeight(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.lblActWeight.Text = value;
            }
            
        }
        //cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void EnableButtons()
        {
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = true;
            this.btnCancel.Enabled = true;
        }
        //prev button
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.step <= 0)
            {
                this.step = 0;
            }
            else
            {
                this.step--;
            }
            
            this.CalibPross();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.testFlag = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.testFlag = false;
        }
    }
}
