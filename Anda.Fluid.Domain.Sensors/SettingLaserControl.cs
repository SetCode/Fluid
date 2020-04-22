using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingLaserControl : UserControlEx, IMsgSender
    {
        private LaserSetting settingBackUp;
        public SettingLaserControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.cbxVendor.Items.Add(Laser.Vendor.IL);
            this.cbxVendor.Items.Add(Laser.Vendor.SickOD2);
            this.cbxVendor.Items.Add(Laser.Vendor.Disable);
            this.cbxVendor.SelectedIndex = 0;
            this.cbxVendor.SelectedIndexChanged += CbxVendor_SelectedIndexChanged;

            this.txtReadCmd.ReadOnly = true;
        }

        private void CbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Laser.Vendor vendor = (Laser.Vendor)this.cbxVendor.SelectedItem;
            Machine.Instance.Laser.SetLaserable(vendor);
            this.txtReadCmd.Text = Machine.Instance.Laser.Laserable.CmdReadValue;

            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
            CompareObj.CompareProperty(SensorMgr.Instance.Laser, this.settingBackUp, null, this.GetType().Name, true);
        }

        public SettingLaserControl Setup()
        {
            this.ReadLanguageResources();
            this.cbxVendor.SelectedItem = SensorMgr.Instance.Laser.Vendor;
            this.txtReadCmd.Text = Machine.Instance.Laser.Laserable.CmdReadValue;
            this.settingBackUp = (LaserSetting)SensorMgr.Instance.Laser.Clone();
            return this;
        }
    }
}
