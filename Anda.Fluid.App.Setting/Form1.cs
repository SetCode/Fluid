using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Setting
{
    public partial class Form1 : Form
    {
        private MachineSetting machineSetting;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.cmbMachine.Items.Add(MachineSelection.AD16);
            this.cmbMachine.Items.Add(MachineSelection.iJet7);
            this.cmbMachine.Items.Add(MachineSelection.iJet6);
            this.cmbMachine.Items.Add(MachineSelection.AD19);

            this.cmbValve.Items.Add(ValveSelection.单阀);
            this.cmbValve.Items.Add(ValveSelection.双阀);

            this.cmbConveyor.Items.Add(ConveyorSelection.单轨);
            this.cmbConveyor.Items.Add(ConveyorSelection.双轨);

            this.btnOk.Click += BtnOk_Click;
            this.btnCancel.Click += BtnCancel_Click;

            this.Setup();
        }

        private void Setup()
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(MachineSetting).Name;
            machineSetting = JsonUtil.Deserialize<MachineSetting>(path);
            if(machineSetting == null)
            {
                machineSetting = new MachineSetting();
            }

            this.cmbMachine.SelectedIndex = (int)machineSetting.MachineSelect;
            this.cmbValve.SelectedIndex = (int)machineSetting.ValveSelect;
            this.cmbConveyor.SelectedIndex = (int)machineSetting.ConveyorSelect;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(MachineSetting).Name;
            machineSetting.MachineSelect = (MachineSelection)cmbMachine.SelectedIndex;
            machineSetting.ValveSelect = (ValveSelection)cmbValve.SelectedIndex;
            machineSetting.ConveyorSelect = (ConveyorSelection)cmbConveyor.SelectedIndex;
            JsonUtil.Serialize<MachineSetting>(path, machineSetting);

            List<Inspection> list = new List<Inspection>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Inspection() { Key = i });
            }
            XmlUtils.Serialize<List<Inspection>>("Inspections.xml", list);


            //this.Close();
        }
    }
}
