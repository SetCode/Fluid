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
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingHeaterControl : UserControl,IMsgSender
    {
        public SettingHeaterControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Omron);
            this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Aika);
            this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Disable);
            this.cbxVendor.SelectedIndex = 0;
            this.cbxVendor.SelectedIndexChanged += CbxVendor_SelectedIndexChanged;
        }

        private void CbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            HeaterControllerMgr.Vendor vendor = (HeaterControllerMgr.Vendor)this.cbxVendor.SelectedItem;
            Machine.Instance.HeaterController1.SetHeater(vendor);
            Machine.Instance.HeaterController2.SetHeater(vendor);

            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
        }

        public SettingHeaterControl Setup()
        {
            this.cbxVendor.SelectedItem = SensorMgr.Instance.Heater.Vendor;
            return this;
        }
    }
}
