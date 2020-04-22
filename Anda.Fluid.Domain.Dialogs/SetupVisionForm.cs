using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class SetupVisionForm : JogFormBase, IMsgSender
    {
        public CameraPrm prmBackUp;
        public SetupVisionForm()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
        }

        public SetupVisionForm Setup()
        {
            this.cbxVendor.Items.Add(Camera.Vendor.Basler);
            this.cbxVendor.Items.Add(Camera.Vendor.Hik);
            this.cbxVendor.SelectedItem = Machine.Instance.Camera.Prm.Vendor;
            this.cbxVendor.SelectedIndexChanged += cbxVendor_SelectedIndexChanged;
            this.cbxReverseX.Checked = Machine.Instance.Camera.Prm.ReverseX;
            this.cbxReverseY.Checked = Machine.Instance.Camera.Prm.ReverseY;
            if (Machine.Instance.Camera.Prm != null)
            {
                this.prmBackUp = (CameraPrm)Machine.Instance.Camera.Prm.Clone();
            }          
            return this;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this);
            CameraPrmMgr.Instance.Save();            
            this.Close();
            if (Machine.Instance.Camera.Prm!=null && this.prmBackUp!=null)
            {
                CompareObj.CompareProperty(Machine.Instance.Camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxReverseX_CheckedChanged(object sender, EventArgs e)
        {
            Machine.Instance.Camera.SetReverseX(this.cbxReverseX.Checked);           
        }

        private void cbxReverseY_CheckedChanged(object sender, EventArgs e)
        {
            Machine.Instance.Camera.SetReverseY(this.cbxReverseY.Checked);           
        }

        private void cbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera.Vendor vendor = (Camera.Vendor)this.cbxVendor.SelectedItem;
            Machine.Instance.Camera.Close();
            Machine.Instance.Camera.SelectVendor(vendor);
            Machine.Instance.InitVision();
            MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
            this.cameraControl1.SetupCamera(Machine.Instance.Camera);
        }

       
    }
}
