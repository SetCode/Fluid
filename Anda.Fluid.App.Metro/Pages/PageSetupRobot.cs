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
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageSetupRobot : MetroSetUserControl, IAccessControllable
    {
        private RobotDefaultPrm DefaultPrmBackUp;
        //权限执行
        private AccessExecutor accessExecutor;
        public PageSetupRobot()
        {
            InitializeComponent();
            this.ShowBorder = false;
            if (Machine.Instance.Robot != null)
            {
                this.propertyGrid1.SelectedObject = Machine.Instance.Robot.DefaultPrm;
            }
            if (Machine.Instance.Robot.DefaultPrm != null)
            {
                this.DefaultPrmBackUp = (RobotDefaultPrm)Machine.Instance.Robot.DefaultPrm.Clone();
            }

            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.TrcPrm.VelMax = Machine.Instance.Robot.DefaultPrm.MaxVelXY;
            Machine.Instance.Robot.TrcPrm.AccMax = Machine.Instance.Robot.DefaultPrm.MaxAccXY;
            Machine.Instance.Robot.SaveDefaultPrm();
            CompareObj.CompareProperty(Machine.Instance.Robot.DefaultPrm, this.DefaultPrmBackUp, null, this.GetType().Name);

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (MetroSetMessageBox.Show(this, "将模组的参数设置为默认值?", "警告",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SettingUtil.ResetToDefault<RobotDefaultPrm>(Machine.Instance.Robot.DefaultPrm);
                //LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(Machine.Instance.Robot.DefaultPrm, this.GetType().Name);
                this.propertyGrid1.SelectedObject = Machine.Instance.Robot.DefaultPrm;
                CompareObj.CompareProperty(Machine.Instance.Robot.DefaultPrm, this.DefaultPrmBackUp, null, this.GetType().Name);
            }
        }

        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageSetupRobot;
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
            this.DefaultContainerAccess.ContainerAccessDescription = "运动参数设置";

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
