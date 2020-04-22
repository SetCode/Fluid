using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Motion
{
    public partial class SettingRobotForm : SettingFormBase
    {
        private RobotDefaultPrm DefaultPrmBackUp;
        public SettingRobotForm()
        {
            InitializeComponent();
            this.SetupTree();

            this.OnSaveClicked += SettingRobotForm_OnSaveClicked;
            this.OnResetClicked += SettingRobotForm_OnResetClicked;
            if (Machine.Instance.Robot.DefaultPrm != null)
            {
                this.DefaultPrmBackUp = (RobotDefaultPrm)Machine.Instance.Robot.DefaultPrm.Clone();
            }
            this.ReadLanguageResources();
        }

        private void SettingRobotForm_OnResetClicked()
        {
            //if (MessageBox.Show("Reset robot default setting to default?", "Warning",
            //MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            if (MessageBox.Show("将模组的参数设置为默认值?", "警告",
                                   MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SettingUtil.ResetToDefault<RobotDefaultPrm>(Machine.Instance.Robot.DefaultPrm);
                LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(Machine.Instance.Robot.DefaultPrm,this.GetType().Name);
                this.propertyGrid1.SelectedObject = Machine.Instance.Robot.DefaultPrm;

                CompareObj.CompareProperty(Machine.Instance.Robot.DefaultPrm, this.DefaultPrmBackUp, null, this.GetType().Name);
            }
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveProportyGridLngText(Machine.Instance.Robot.DefaultPrm);
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void SettingRobotForm_OnSaveClicked()
        {
            Machine.Instance.Robot.TrcPrm.VelMax = Machine.Instance.Robot.DefaultPrm.MaxVelXY;
            Machine.Instance.Robot.TrcPrm.AccMax = Machine.Instance.Robot.DefaultPrm.MaxAccXY;
            Machine.Instance.Robot.SaveDefaultPrm();
            CompareObj.CompareProperty(Machine.Instance.Robot.DefaultPrm, this.DefaultPrmBackUp, null, this.GetType().Name);
        }

        private void SetupTree()
        {
            TreeNode node = new TreeNode("默认参数");
            this.tvwList.Nodes.Add(node);
            this.propertyGrid1.SelectedObject = Machine.Instance.Robot.DefaultPrm;
        }
    }
}
