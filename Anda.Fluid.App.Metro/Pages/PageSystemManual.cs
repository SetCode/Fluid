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
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageSystemManual : MetroSetUserControl, IAccessControllable
    {
        //权限执行
        private AccessExecutor accessExecutor;

        public PageSystemManual()
        {
            InitializeComponent();
            this.ShowBorder = false;

            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }

        private void btnIO_Click(object sender, EventArgs e)
        {
            FormMgr.Show<IOForm>(this);
        }

        private void btnConveyorIO_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                FormMgr.Show<Drive.Conveyor.LeadShine.Forms.IOForms.IOForm>(this);
            }
        }

        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageSystemManual;

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
            this.DefaultContainerAccess.ContainerAccessDescription = "手动调试";

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
