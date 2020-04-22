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
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageSetupMachine : MetroSetUserControl, IMsgSender,IAccessControllable
    {
        private MachineSetting SettingBackUp = new MachineSetting();
        //权限执行
        private AccessExecutor accessExecutor;
        public PageSetupMachine()
        {
            InitializeComponent();
            this.ShowBorder = false;
            this.propertyGrid1.SelectedObject = Machine.Instance.Setting;
            if (Machine.Instance.Setting != null)
            {
                this.SettingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
            }
            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }
        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageSetupMachine;
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
            this.DefaultContainerAccess.ContainerAccessDescription = "设置机型";

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
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Machine.Instance.SetupValve();
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this);
            Machine.Instance.Setting.Save();
            CompareObj.CompareProperty(Machine.Instance.Setting, this.SettingBackUp, null, this.GetType().Name);

        }
    }
}
