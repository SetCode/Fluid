using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
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
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Motion
{
    public partial class IOSetupForm : FormEx, IMsgSender
    {
        private MachineSetting SettingBackUp;
        public IOSetupForm()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.comboBox1.Items.Add(MachineSelection.AD16);
            this.comboBox1.Items.Add(MachineSelection.iJet7);
            this.comboBox1.Items.Add(MachineSelection.iJet6);
            this.comboBox1.Items.Add(MachineSelection.AD19);
            this.comboBox1.SelectedItem = Machine.Instance.Setting.MachineSelect;

            this.SettingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Machine.Instance.Setting.MachineSelect = (MachineSelection)this.comboBox1.SelectedItem;
            Machine.Instance.SetupIO();
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            Machine.Instance.Setting.Save();
            this.Close();
            CompareObj.CompareProperty(Machine.Instance.Setting, this.SettingBackUp, null, this.GetType().Name);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
