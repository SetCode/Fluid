using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingBarcodeScanControl : UserControlEx,IMsgSender
    {
        private BarcodeScanSetting settingBackUp;
        private int conveyorNo = 0;
        public SettingBarcodeScanControl(int conveyorNo)
        {
            InitializeComponent();
            this.tbxCmdRead.ReadOnly = true;
            this.conveyorNo = conveyorNo;
            this.cbxVendor.Items.Add(Drive.Sensors.Barcode.BarcodeScanner.Vendor.SR700);
            this.cbxVendor.Items.Add(Drive.Sensors.Barcode.BarcodeScanner.Vendor.Disable);
            this.cbxVendor.SelectedIndex = 0;

            this.cbxVendor.SelectedIndexChanged += CbxVendor_SelectedIndexChanged;
        }

        private void CbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drive.Sensors.Barcode.BarcodeScanner.Vendor vendor = (Drive.Sensors.Barcode.BarcodeScanner.Vendor)this.cbxVendor.SelectedItem;
            if(this.conveyorNo == 0)
            {
                Machine.Instance.BarcodeSanncer1.SetBarcodeScannable(vendor);
                this.tbxCmdRead.Text = Machine.Instance.BarcodeSanncer1.BarcodeScannable.CmdReadValue;
                this.tbxDelimiter.Text = Machine.Instance.BarcodeSanncer1.BarcodeScannable.Delimiter.ToString();
                MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
                SensorMgr.Instance.Save();
                CompareObj.CompareProperty(SensorMgr.Instance.barcodeScanner1, this.settingBackUp, true);
            }
            else
            {
                Machine.Instance.BarcodeSanncer2.SetBarcodeScannable(vendor);
                this.tbxCmdRead.Text = Machine.Instance.BarcodeSanncer2.BarcodeScannable.CmdReadValue;
                this.tbxDelimiter.Text = Machine.Instance.BarcodeSanncer2.BarcodeScannable.Delimiter.ToString();
                MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
                SensorMgr.Instance.Save();
                CompareObj.CompareProperty(SensorMgr.Instance.barcodeScanner2, this.settingBackUp, true);
            }
        }

        public SettingBarcodeScanControl ChangeConveyorNo(int conveyorNo)
        {
            this.conveyorNo = conveyorNo;
            return this;
        }

        public SettingBarcodeScanControl Setup()
        {
            this.cbxVendor.SelectedItem = SensorMgr.Instance.barcodeScanner1.Vendor;
            if (this.conveyorNo == 0)
            {
                this.tbxCmdRead.Text = Machine.Instance.BarcodeSanncer1.BarcodeScannable.CmdReadValue;
                this.tbxDelimiter.Text = Machine.Instance.BarcodeSanncer1.BarcodeScannable.Delimiter.ToString();
                this.settingBackUp = (BarcodeScanSetting)SensorMgr.Instance.barcodeScanner1.Clone();
            }
            else
            {
                this.tbxCmdRead.Text = Machine.Instance.BarcodeSanncer2.BarcodeScannable.CmdReadValue;
                this.tbxDelimiter.Text = Machine.Instance.BarcodeSanncer2.BarcodeScannable.Delimiter.ToString();
                this.settingBackUp = (BarcodeScanSetting)SensorMgr.Instance.barcodeScanner2.Clone();
            }
            
            return this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            Machine.Instance.GetCurConveyorBarcodeScanner(this.conveyorNo).BarcodeScannable.ReadValue(TimeSpan.FromSeconds(2), out str);
            if (string.IsNullOrEmpty(str))
            {
                //str = "read error";
                str = "读取错误";
            }
            MessageBox.Show(str);
        }
    }
}
