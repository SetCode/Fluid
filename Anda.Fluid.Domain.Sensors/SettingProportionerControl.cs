using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class SettingProportionerControl : UserControlEx, IMsgSender
    {
        private ProportionerSetting settingBackUp;
        public SettingProportionerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            for (int i = 1; i <= 8; i++)
            {
                this.cbxChn1.Items.Add(i);
                this.cbxChn2.Items.Add(i);
            }
            this.cbxChn1.SelectedIndex = 0;
            this.cbxChn1.SelectedIndexChanged += CbxChn1_SelectedIndexChanged;
            this.cbxChn2.SelectedIndex = 0;
            this.cbxChn2.SelectedIndexChanged += CbxChn2_SelectedIndexChanged;

            this.cbxControlType1.Items.Add(Proportioner.ControlType.Direct);
            this.cbxControlType1.Items.Add(Proportioner.ControlType.PLC);
            this.cbxControlType1.Items.Add(Proportioner.ControlType.Disable);
            this.cbxControlType1.SelectedIndex = 0;
            this.cbxControlType1.SelectedIndexChanged += CbxControlType1_SelectedIndexChanged;

            this.cbxControlType2.Items.Add(Proportioner.ControlType.Direct);
            this.cbxControlType2.Items.Add(Proportioner.ControlType.PLC);
            this.cbxControlType2.Items.Add(Proportioner.ControlType.Disable);
            this.cbxControlType2.SelectedIndex = 0;
            this.cbxControlType2.SelectedIndexChanged += CbxControlType2_SelectedIndexChanged;

            if(Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.groupBox2.Enabled = false;
            }
        }

        private void CbxControlType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proportioner.ControlType controlType = (Proportioner.ControlType)cbxControlType1.SelectedItem;
            Machine.Instance.Proportioner1.SetProportionor(1, controlType, SensorMgr.Instance.Proportioners);
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();

            CompareObj.CompareProperty(SensorMgr.Instance.Proportioners, this.settingBackUp, true);
        }

        private void CbxControlType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proportioner.ControlType controlType = (Proportioner.ControlType)cbxControlType2.SelectedItem;
            Machine.Instance.Proportioner2.SetProportionor(2, controlType, SensorMgr.Instance.Proportioners);
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();

            CompareObj.CompareProperty(SensorMgr.Instance.Proportioners, this.settingBackUp, true);
        }

        private void CbxChn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorMgr.Instance.Proportioners.Channel1 = (int)cbxChn1.SelectedItem;
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
            CompareObj.CompareProperty(SensorMgr.Instance.Proportioners, this.settingBackUp, true);
        }

        private void CbxChn2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorMgr.Instance.Proportioners.Channel2 = (int)cbxChn2.SelectedItem;
            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
            CompareObj.CompareProperty(SensorMgr.Instance.Proportioners, this.settingBackUp, true);
        }

        public SettingProportionerControl Setup()
        {
            this.ReadLanguageResources();
            this.cbxControlType1.SelectedItem = SensorMgr.Instance.Proportioners.ControlType1;
            this.cbxControlType2.SelectedItem = SensorMgr.Instance.Proportioners.ControlType2;
            this.cbxChn1.SelectedItem = SensorMgr.Instance.Proportioners.Channel1;
            this.cbxChn2.SelectedItem = SensorMgr.Instance.Proportioners.Channel2;
            if (SensorMgr.Instance.Proportioners != null)
            {
                this.settingBackUp = (ProportionerSetting)SensorMgr.Instance.Proportioners.Clone();
            }
            return this;
        }
    }
}
