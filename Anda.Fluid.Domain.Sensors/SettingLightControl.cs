using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingLightControl : UserControl
    {
        public SettingLightControl()
        {
            InitializeComponent();
            this.cbxVendor.Items.Add(LightVendor.Anda);
            this.cbxVendor.Items.Add(LightVendor.OPT);
            this.cbxVendor.Items.Add(LightVendor.Custom);
        }

        public SettingLightControl Setup()
        {
            this.cbxVendor.SelectedItem = SensorMgr.Instance.Light.Vendor;
            return this;
        }

        private void cbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorMgr.Instance.Light.Vendor = (LightVendor)this.cbxVendor.SelectedItem;
            Machine.Instance.Light.GetLight(SensorMgr.Instance.Light);

            SensorMgr.Instance.Save();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Machine.Instance.Light.Init();
        }
    }
}
