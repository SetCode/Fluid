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

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupMachine : MetroSetUserControl, IMsgSender
    {
        private MachineSetting SettingBackUp = new MachineSetting();
        public PageSetupMachine()
        {
            InitializeComponent();
            this.ShowBorder = false;
            this.propertyGrid1.SelectedObject = Machine.Instance.Setting;
            if (Machine.Instance.Setting != null)
            {
                this.SettingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Machine.Instance.SetupValve();
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this);
            Machine.Instance.Setting.Save();
            CompareObj.CompareProperty(Machine.Instance.Setting, this.SettingBackUp, null, this.GetType().Name);

        }
    }
}
