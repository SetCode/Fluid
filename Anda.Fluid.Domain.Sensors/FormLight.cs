using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Sensors;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class FormLight : Form
    {
        public FormLight()
        {
            InitializeComponent();
            
        }
        private ILightingController light = null;
        private LightingCom lightCom = null;
        //连接
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text == "Conncect")
            {
                //light = new Light(0, new LightingOPT(1, textBoxIP.Text.Trim()));
                light =  new LightingOPT(1, textBoxIP.Text.Trim());
                this.lightCom = light as LightingCom;
                var ret = this.lightCom.Connect(new TimeSpan(0, 0, 0, 0, 40));
                if (ret)
                {
                    this.richTextBoxState.AppendText($"connect:{ret.ToString()}" + Environment.NewLine);
                    if (light.GetChannelState(LightChn.Red) == 1 && light.GetChannelState(LightChn.Green) == 1 && light.GetChannelState(LightChn.Blue) == 1)
                    {
                        StringBuilder sn = new StringBuilder();
                        StringBuilder ip = new StringBuilder();
                        StringBuilder mask = new StringBuilder();
                        StringBuilder gateWay = new StringBuilder();
                        this.richTextBoxState.AppendText($"all RGB light connected!" + Environment.NewLine);
                        this.light.ReadSN(sn);//读取设备序列号
                        this.txtSN.Text = sn.ToString();
                        this.light.ReadIPConfig(ip, mask, gateWay);//读取IP配置
                        this.txtIP.Text = ip.ToString();
                        this.txtSubnetMask.Text = mask.ToString();
                        this.txtGateway.Text = gateWay.ToString();
                        labelConnectState.Text = "connected";
                        buttonConnect.Text = "Disconnect";
                    }
                    else
                    {
                        this.richTextBoxState.AppendText($"one or more RGB light disconnected!" + Environment.NewLine);
                    }
                }
                else
                {
                    this.richTextBoxState.AppendText($"connect:{ret.ToString()}" + Environment.NewLine);
                } 
            }
            else
            {
                this.lightCom.Disconnect();
                buttonConnect.Text = "Connect";
                labelConnectState.Text = "disconnected";
            }
        }
        //打开、关闭单个通道
        private void buttonOpenChannel_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            int ret = -1;
            switch (button.Name)
            {
                case "R_Open":
                    ret = this.light.TurnOnChannel(LightChn.Red);
                    this.buttonRChannelOpen.Text = "R_Close";
                    break;
                case "G_Open":
                    ret = this.light.TurnOnChannel(LightChn.Green);
                    this.buttonGChannelOpen.Text = "G_Close";
                    break;
                case "B_Open":
                    ret = this.light.TurnOnChannel(LightChn.Blue);
                    this.buttonBChannelOpen.Text = "B_Close";
                    break;
                case "RGB_Open":
                    ret = this.light.TurnOnMultiChannel(RGBCombi(comboBoxRGB), RGBCombi(comboBoxRGB).Length);
                    this.buttonBChannelOpen.Text = "RGB_Close";
                    break;
                case "R_Close":
                    ret = this.light.TurnOffChannel(LightChn.Red);
                    break;
                case "G_Close":
                    ret = this.light.TurnOffChannel(LightChn.Green);
                    break;
                case "B_Close":
                    ret = this.light.TurnOffChannel(LightChn.Blue);
                    break;
                case "RGB_Close":
                    ret = this.light.TurnOnMultiChannel(RGBCombi(comboBoxRGB), RGBCombi(comboBoxRGB).Length);
                    this.buttonBChannelOpen.Text = "RGB_Open";
                    break;
                default:
                    break;
            }
            if (ret == 0)
            {
                richTextBoxState.AppendText($"{ button.Name} is true");
            }
            else
            {
                richTextBoxState.AppendText($"{ button.Name}'s error code is {ret}");
            }
        }
        //多通道组合
        private int[] RGBCombi(ComboBox combobox)
        {
            int[] rgb = null;
            switch (combobox.SelectedItem.ToString())
            {
                case "R":
                    rgb = new int[] {1};
                    break;
                case "G":
                    rgb = new int[] { 2 };
                    break;
                case "B":
                    rgb = new int[] { 3 };
                    break;
                case "RG":
                    rgb = new int[] { 1,2 };
                    break;
                case "RB":
                    rgb = new int[] { 1, 3 };
                    break;
                case "GB":
                    rgb = new int[] { 2,3 };
                    break;
                case "RGB":
                    rgb = new int[] {1, 2, 3 };
                    break;
                default:
                    break;
            }
            return rgb;
        }
        //选择rgb通道
        private LightChn RGBSwitch(ComboBox combobox)
        {
            LightChn rgb = 0;
            switch (combobox.SelectedItem.ToString())
            {
                case "R":
                    rgb = LightChn.Red;
                    break;
                case "G":
                    rgb = LightChn.Green;
                    break;
                case "B":
                    rgb = LightChn.Blue;
                    break;
                default:
                    break;
            }
            return rgb;
        }
        //设置亮度
        private void hScrollBarSetChannelValue_Scroll(object sender, ScrollEventArgs e)
        {
            var scroll = sender as HScrollBar;
            int value = 0;
            switch (scroll.Name)
            {
                case "hScrollBarChannel_R":
                    value = this.hScrollBarChannel_R.Value;
                    this.light.SetIntensity(LightChn.Red,value);
                    this.textBoxR_Value.Text = this.light.ReadIntensity(LightChn.Red).ToString();//再次读取数值
                    break;
                case "hScrollBarChannel_G":
                    value = this.hScrollBarChannel_G.Value;
                    this.light.SetIntensity(LightChn.Green, value);
                    this.textBoxG_Value.Text = this.light.ReadIntensity(LightChn.Green).ToString();//再次读取数值
                    break;
                case "hScrollBarChannel_B":
                    value = this.hScrollBarChannel_B.Value;
                    this.light.SetIntensity(LightChn.Blue, value);
                    this.textBoxB_Value.Text = this.light.ReadIntensity(LightChn.Blue).ToString();//再次读取数值
                    break;
                case "hScrollBarChannel_RGB":
                    int length = RGBCombi(comboBoxRGB).Length;
                    var arrayChannel = RGBCombi(comboBoxRGB);
                    Intensity[] arrayIntensity = new Intensity[length];
                    for (int i = 0; i < length; i++)
                    {
                        arrayIntensity[i].channel = arrayChannel[i];
                        arrayIntensity[i].channel = hScrollBarChannel_RGB.Value;
                    }
                    this.light.SetMultiIntensity(arrayIntensity,arrayIntensity.Length);
                    break;
                default:
                    break;
            }
        }
        //设置单通道脉宽
        private void btnTrigWidth_Click(object sender, EventArgs e)
        {
            var value = 0;
            this.light.SetTriggerWidth(RGBSwitch(cmbRGBTrigWidth),int.Parse(this.txtTrigWidth.Text));
            this.txtTrigWidth.Text = this.light.ReadTriggerWidth(RGBSwitch(cmbRGBTrigWidth),ref value).ToString();//再次读取脉宽值
        }
        //设置多通道脉宽
        private void btnMultiTrigWidth_Click(object sender, EventArgs e)
        {
            int length = RGBCombi(cmbMultiTrigWidth).Length;
            var arrayChannel = RGBCombi(cmbMultiTrigWidth);
            TriggerWidth[] arrayTrigWidth = new TriggerWidth[length];
            for (int i = 0; i < length; i++)
            {
                arrayTrigWidth[i].channel = arrayChannel[i];
                arrayTrigWidth[i].channel = int.Parse(this.txtMultiTrigWidth.Text);
            }
            this.light.SetMultiTriggerWidth(arrayTrigWidth, arrayTrigWidth.Length);
        }
        //设置单通道高亮脉宽
        private void btnHBTrigWidth_Click(object sender, EventArgs e)
        {
            var value = 0;
            this.light.SetHBTriggerWidth(RGBSwitch(cmbRGBTrigWidth), int.Parse(this.txtTrigWidth.Text));
            this.txtTrigWidth.Text = this.light.ReadHBTriggerWidth(RGBSwitch(cmbRGBTrigWidth), ref value).ToString();//再次读取脉宽值
        }
        //设置多通道高亮脉宽
        private void btnHBMultiTrigWidth_Click(object sender, EventArgs e)
        {
            int length = RGBCombi(cmbMultiTrigWidth).Length;
            var arrayChannel = RGBCombi(cmbMultiTrigWidth);
            HBTriggerWidth[] arrayHBTrigWidth = new HBTriggerWidth[length];
            for (int i = 0; i < length; i++)
            {
                arrayHBTrigWidth[i].channel = arrayChannel[i];
                arrayHBTrigWidth[i].channel = int.Parse(this.txtMultiTrigWidth.Text);
            }
            this.light.SetMultiHBTriggerWidth(arrayHBTrigWidth, arrayHBTrigWidth.Length);
        }
        /// <summary>
        /// 调用函数是否需要返回值、是否需要断电备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkResponse_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            switch (chk.Name)
            {
                case "chkBackup":
                    this.light.EnablePowerOffBackup(this.chkBackup.Checked);
                    break;
                case "chkResponse":
                    this.light.EnableResponse(this.chkResponse.Checked);
                    break;
                default:
                    break;
            }
        }
        //软触发
        private void btnSoftTrig_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Name)
            {
                case "btnSoftTrig":
                    this.light.SoftwareTrigger(RGBSwitch(cmbSoftTrig), int.Parse(this.txtTrigTime.Text));
                    break;
                case "btnMultiSoftTrig":
                    int length = RGBCombi(cmbMultiTrigWidth).Length;
                    var arrayChannel = RGBCombi(cmbMultiTrigWidth);
                    MaxCurrent[] arrayTrigTime = new MaxCurrent[length];
                    for (int i = 0; i < length; i++)
                    {
                        arrayTrigTime[i].channel = arrayChannel[i];
                        arrayTrigTime[i].channel = int.Parse(this.txtMultiTrigWidth.Text);
                    }
                    this.light.MultiSoftwareTrigger(arrayTrigTime, arrayTrigTime.Length);
                    break;
                default:
                    break;
            }
        }
        //工作模式
        private void btnWorkMode_Click(object sender, EventArgs e)
        {
            int workmode = -1;
            this.light.SetWorkMode((int)cmbWorkMode.SelectedItem);
            if (this.light.ReadWorkMode(ref workmode) == 0)//再次读取
            {
                this.cmbWorkMode.SelectedItem = workmode.ToString();
            }
        }
        //触发极性
        private void btnActivation_Click(object sender, EventArgs e)
        {
            int Ativation = -1;
            this.light.SetTriggerActivation((int)cmbActivation.SelectedItem);
            if (this.light.ReadTriggerActivation(ref Ativation) == 0)//再次读取
            {
                this.cmbActivation.SelectedItem = Ativation.ToString();
            }
        }
        //最大电流
        private void btnMaxCurrent_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Name)
            {
                case "btnMaxCurrent":
                    this.light.SetMaxCurrent(RGBSwitch(cmbMaxCurrent), int.Parse(this.txtMaxCurrent.Text));
                    break;
                case "btnMultiMaxCurrent":
                    int length = RGBCombi(cmbMultiMaxCurrent).Length;
                    var arrayChannel = RGBCombi(cmbMultiMaxCurrent);
                    MaxCurrent[] arrayMultiMaxCurrent = new MaxCurrent[length];
                    for (int i = 0; i < length; i++)
                    {
                        arrayMultiMaxCurrent[i].channel = arrayChannel[i];
                        arrayMultiMaxCurrent[i].channel = int.Parse(this.txtMaxCurrent.Text);
                    }
                    this.light.SetMultiMaxCurrent(arrayMultiMaxCurrent, arrayMultiMaxCurrent.Length);
                    break;
                default:
                    break;
            }
        }
    }
}
