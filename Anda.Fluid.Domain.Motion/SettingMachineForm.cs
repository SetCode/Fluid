using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.AccessControl.User;
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
    public partial class SettingMachineForm : FormEx, IMsgSender
    {
        private MachineSetting SettingBackUp = new MachineSetting();
        public SettingMachineForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.propertyGrid1.SelectedObject = Machine.Instance.Setting;
            if (Machine.Instance.Setting != null)
            {
                this.SettingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
            }
            this.ReadLanguageResources();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Machine.Instance.SetupValve();
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this);
            Machine.Instance.Setting.Save();
            this.Close();
            CompareObj.CompareProperty(Machine.Instance.Setting, this.SettingBackUp, null, this.GetType().Name);
            Account tempAccount = AccountMgr.Instance.FindBy("Supervisor");
            AccountMgr.Instance.SwitchUser(tempAccount);
        }
    }
}
