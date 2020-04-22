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
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageSetupCamera : MetroSetUserControl, IMsgSender, IAccessControllable
    {
        public CameraPrm prmBackUp;

        //权限执行
        private AccessExecutor accessExecutor;

        public PageSetupCamera()
        {
            InitializeComponent();
            this.ShowBorder = false;
            this.setup();
            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
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
        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageSetupCamera;
        public Control Control => this;


        public ContainerAccess CurrContainerAccess { get; set; } = new ContainerAccess();


        public ContainerAccess DefaultContainerAccess { get; set; } = new ContainerAccess();


        public List<AccessObj> UserAccessControls { get; set; } = new List<AccessObj>();

        public void SetupUserAccessControl()
        {

        }
        public void SetDefaultAccess()
        {
            
            this.DefaultContainerAccess = new ContainerAccess();
           
            //上面
            string containerName = this.GetType().Name;
            this.DefaultContainerAccess.ContainerName = containerName;
            this.DefaultContainerAccess.ContainerAccessDescription = "相机设置";

            this.DefaultContainerAccess.AddContainerTechnician();

            AccessControlMgr.Instance.AddContainerAccess(this.DefaultContainerAccess);
        }

        public void LoadAccess()
        {
            this.accessExecutor.LoadAccess();
        }



        public void UpdateUIByAccess()
        {
            this.accessExecutor.UpdateUIByAccess();
        }
              
        #endregion

    }
}
