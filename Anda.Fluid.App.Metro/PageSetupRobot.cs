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

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupRobot : MetroSetUserControl
    {
        private RobotDefaultPrm DefaultPrmBackUp;
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
    }
}
