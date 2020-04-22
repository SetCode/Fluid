using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingConveyorHeaterControl : UserControl
    {
        public SettingConveyorHeaterControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            //this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Omron);
            //this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Aika);
            //this.cbxVendor.Items.Add(HeaterControllerMgr.Vendor.Disable);
            //this.cbxVendor.SelectedIndex = 0;
            //this.cbxVendor.SelectedIndexChanged += CbxVendor_SelectedIndexChanged;
        }
    }
}
