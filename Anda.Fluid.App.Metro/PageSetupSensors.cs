using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using MetroSet_UI.Controls;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Domain.Sensors;
using System.IO.Ports;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Drive.Sensors.DigitalGage;
using Anda.Fluid.Drive.Sensors.Barcode;
using Anda.Fluid.Infrastructure.DataStruct;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupSensors : MetroSetUserControl, IMsgSender
    {
        private const string DEVICELIST = "deviceList";
        private string HEIGHT = "测高";
        private string SCALE = "称重";
        private string HEATER = "加热";
        private string PROPORS = "比例阀";
        private string GAGE = "高度规";
        private string SCANNER = "条码枪";
        private MetroSetButton btnSelected;
        private EasySerialPort currEasySerialPort;
        private EasySerialPort currEasySerialPortBackUp;
        private SettingLaserControl settingLaser;
        private SettingScaleControl settingScale;
        private SettingProportionerControl settingPropors;
        private SettingBarcodeScanControl settingScanner;
        private SettingHeaterControl settingHeater;
        private Timer timer;
        public PageSetupSensors()
        {
            InitializeComponent();
            this.ShowBorder = false;

            //this.ReadLanguageResources();
            this.settingLaser = new SettingLaserControl().Setup();
            this.settingScale = new SettingScaleControl().Setup();
            this.settingPropors = new SettingProportionerControl().Setup();
            this.settingScanner = new SettingBarcodeScanControl().Setup();
            this.settingHeater = new SettingHeaterControl().Setup();

            this.setupTree();
            this.setupGroupBoxCom();
            this.btnHeight_Click(null, null);

            Control[] controls = this.Controls.Find(this.label2.Name, true);
            Control[] controls2 = this.Controls.Find(this.label4.Name, true);
            this.timer = new Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        #region Serial Port

        private void setupGroupBoxCom()
        {
            for (int i = 1; i <= 11; i++)
            {
                this.cbxCom.Items.Add(string.Format("COM{0}", i));
            }
            this.cbxBaudRate.Items.Add(9600);
            this.cbxBaudRate.Items.Add(14400);
            this.cbxBaudRate.Items.Add(19200);
            this.cbxBaudRate.Items.Add(38400);
            this.cbxBaudRate.Items.Add(57600);
            this.cbxBaudRate.Items.Add(115200);
            this.cbxBaudRate.Items.Add(128000);

            this.cbxParity.Items.Add(Parity.None);
            this.cbxParity.Items.Add(Parity.Odd);
            this.cbxParity.Items.Add(Parity.Even);
            this.cbxParity.Items.Add(Parity.Mark);
            this.cbxParity.Items.Add(Parity.Space);

            for (int i = 4; i <= 8; i++)
            {
                this.cbxDataBits.Items.Add(i);
            }

            this.cbxStopBits.Items.Add(StopBits.None);
            this.cbxStopBits.Items.Add(StopBits.One);
            this.cbxStopBits.Items.Add(StopBits.Two);
            this.cbxStopBits.Items.Add(StopBits.OnePointFive);

            this.cbxCom.SelectedIndex = 0;
            this.cbxBaudRate.SelectedIndex = 0;
            this.cbxParity.SelectedIndex = 0;
            this.cbxDataBits.SelectedIndex = 0;
            this.cbxStopBits.SelectedIndex = 0;

            this.cbxCom.SelectedIndexChanged += CbxCom_SelectedIndexChanged;
            this.cbxBaudRate.SelectedIndexChanged += CbxBaudRate_SelectedIndexChanged;
            this.cbxParity.SelectedIndexChanged += CbxParity_SelectedIndexChanged;
            this.cbxDataBits.SelectedIndexChanged += CbxDataBits_SelectedIndexChanged;
            this.cbxStopBits.SelectedIndexChanged += CbxStopBits_SelectedIndexChanged;
        }

        private void CbxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
                return;
            this.currEasySerialPort.StopBits = (StopBits)this.cbxStopBits.SelectedItem;
        }

        private void CbxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
                return;
            this.currEasySerialPort.DataBits = (DataBits)this.cbxDataBits.SelectedItem;
        }

        private void CbxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
                return;
            this.currEasySerialPort.Parity = (Parity)this.cbxParity.SelectedItem;
        }

        private void CbxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
                return;
            this.currEasySerialPort.BaudRate = (BaudRate)this.cbxBaudRate.SelectedItem;
        }

        private void CbxCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
                return;
            this.currEasySerialPort.PortName = this.cbxCom.SelectedItem as string;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.currEasySerialPort?.Open();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.currEasySerialPort?.Close();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            ByteData data = null;
            string cmd = this.txtSend.Text;
            await Task.Factory.StartNew(() =>
            {
                data = this.currEasySerialPort?.WriteAndGetReply(cmd, TimeSpan.FromSeconds(1));
            });

            this.txtReceive.Text = data?.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.currEasySerialPort == null)
            {
                return;
            }

            if (this.currEasySerialPort.Connected)
            {
                this.btnConnect.BackColor = Color.Green;
                this.btnDisconnect.BackColor = SystemColors.Control;
            }
            else
            {
                this.btnConnect.BackColor = SystemColors.Control;
                this.btnDisconnect.BackColor = Color.Red;
            }
        }

        #endregion


        private void setupEasySerialPort(EasySerialPort esp)
        {
            this.currEasySerialPort = esp;
            this.cbxCom.SelectedItem = esp.PortName;
            this.cbxBaudRate.SelectedItem = (int)esp.BaudRate;
            this.cbxParity.SelectedItem = esp.Parity;
            this.cbxDataBits.SelectedItem = (int)esp.DataBits;
            this.cbxStopBits.SelectedItem = esp.StopBits;
            this.currEasySerialPortBackUp = (EasySerialPort)this.currEasySerialPort.Clone();
        }

        private void setupTree()
        {
            this.btnHeight.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnScale.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnHeater.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnPropor.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnGage.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnScanner.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
        }

        private void btnHeight_Click(object sender, EventArgs e)
        {
            this.selectSensor(HEIGHT);
            this.selectButton(btnHeight);
        }

        private void btnScale_Click(object sender, EventArgs e)
        {
            this.selectSensor(SCALE);
            this.selectButton(btnScale);
        }

        private void btnHeater_Click(object sender, EventArgs e)
        {
            this.selectSensor(HEATER);
            this.selectButton(btnHeater);
        }

        private void btnPropor_Click(object sender, EventArgs e)
        {
            this.selectSensor(PROPORS);
            this.selectButton(btnPropor);
        }

        private void btnGage_Click(object sender, EventArgs e)
        {
            this.selectSensor(GAGE);
            this.selectButton(btnGage);
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            this.selectSensor(SCANNER);
            this.selectButton(btnScanner);
        }

        private void selectButton(MetroSetButton btn)
        {
            if (this.btnSelected != null)
            {
                this.btnSelected.Selected = false;
            }
            this.btnSelected = btn;
            this.btnSelected.Selected = true;
        }

        private void selectSensor(string sensorType)
        {
            this.gbxSensor.Controls.Clear();
            if (sensorType == HEIGHT)
            {
                LaserableCom laserable = Machine.Instance.Laser.Laserable as LaserableCom;
                this.setupEasySerialPort(laserable.EasySerialPort);
                this.gbxSensor.Controls.Add(this.settingLaser.Setup());
            }
            else if (sensorType == SCALE)
            {
                ScalableCom scalable = Machine.Instance.Scale.Scalable as ScalableCom;
                this.setupEasySerialPort(scalable.EasySerialPort);
                this.gbxSensor.Controls.Add(this.settingScale.Setup());
            }
            else if (sensorType == HEATER)
            {
                if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
                {
                    AiKaThermostat heaterable = Machine.Instance.HeaterController1.HeaterControllable as AiKaThermostat;
                    this.setupEasySerialPort(heaterable.EasySerialPort);
                }
                else if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Omron)
                {
                    ThermostatOmron heaterable = Machine.Instance.HeaterController1.HeaterControllable as ThermostatOmron;
                    this.setupEasySerialPort(heaterable.EasySerialPort);
                }
                else
                {
                    InvalidThermostat heaterable = Machine.Instance.HeaterController1.HeaterControllable as InvalidThermostat;
                    this.setupEasySerialPort(heaterable.EasySerialPort);
                }
                this.gbxSensor.Controls.Add(this.settingHeater.Setup());
            }
            else if (sensorType == PROPORS)
            {
                ProportionorCom propor = Machine.Instance.Valve1.Proportioner.Proportional as ProportionorCom;
                this.setupEasySerialPort(propor.EasySerialPort);
                this.gbxSensor.Controls.Add(this.settingPropors.Setup());
            }
            else if (sensorType == GAGE)
            {
                DigitalGagableCom gagable = Machine.Instance.DigitalGage.DigitalGagable as DigitalGagableCom;
                this.setupEasySerialPort(gagable.EasySerialPort);
            }
            else if (sensorType == SCANNER)
            {
                BarcodeScannableCom scanner = Machine.Instance.BarcodeSanncer.BarcodeScannable as BarcodeScannableCom;
                this.setupEasySerialPort(scanner.EasySerialPort);
                this.gbxSensor.Controls.Add(this.settingScanner.Setup());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
            CompareObj.CompareProperty(this.currEasySerialPort, this.currEasySerialPortBackUp, null, this.GetType().Name, true);

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {

        }
    }
}
