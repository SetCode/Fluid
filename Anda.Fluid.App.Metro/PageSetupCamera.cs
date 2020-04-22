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
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupCamera : MetroSetUserControl, IMsgSender
    {
        public CameraPrm prmBackUp;
        public PageSetupCamera()
        {
            InitializeComponent();
            this.ShowBorder = false;
            this.setup();
        }

        private void setup()
        {
            if (Machine.Instance.Camera == null)
            {
                return;
            }
            this.cmbVendor.Items.Add(Camera.Vendor.Basler);
            this.cmbVendor.Items.Add(Camera.Vendor.Hik);
            this.cmbVendor.SelectedItem = Machine.Instance.Camera.Prm.Vendor;
            this.cmbVendor.SelectedIndexChanged += cbxVendor_SelectedIndexChanged;
            this.cbxReverseX.Checked = Machine.Instance.Camera.Prm.ReverseX;
            this.cbxReverseY.Checked = Machine.Instance.Camera.Prm.ReverseY;
            if (Machine.Instance.Camera.Prm == null)
            {
                return;
            }
            this.prmBackUp = (CameraPrm)Machine.Instance.Camera.Prm.Clone();

        }

        private void cbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera.Vendor vendor = (Camera.Vendor)this.cmbVendor.SelectedItem;
            Machine.Instance.Camera.Close();
            Machine.Instance.Camera.SelectVendor(vendor);
            Machine.Instance.InitVision();
            MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
        }

        private void cbxReverseX_CheckedChanged(object sender)
        {
            Machine.Instance.Camera.SetReverseX(this.cbxReverseX.Checked);
        }

        private void cbxReverseY_CheckedChanged(object sender)
        {
            Machine.Instance.Camera.SetReverseY(this.cbxReverseY.Checked);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this);
            CameraPrmMgr.Instance.Save();
            if (Machine.Instance.Camera.Prm != null && this.prmBackUp != null)
            {
                CompareObj.CompareProperty(Machine.Instance.Camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
        }
    }
}
