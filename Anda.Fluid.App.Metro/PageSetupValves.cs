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
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupValves : MetroSetUserControl, IMsgSender
    {
        private string strValveSetup1 = "单阀";
        private string strValveSetup2 = "双阀";
        private Valve valve1, valve2;
        private MachineSetting settingBackUp;

        public PageSetupValves()
        {
            InitializeComponent();
            this.ShowBorder = false;

            this.cmbValveSelect.SelectedIndexChanged += CmbValveSelect_SelectedIndexChanged;
            this.metroSetListBox1.SelectedIndexChanged += MetroSetListBox1_SelectedIndexChanged; ;
            this.metroSetListBox2.SelectedIndexChanged += MetroSetListBox2_SelectedIndexChanged; ;
            //this.ReadLanguageResources();

            this.setup();
        }

        public void setup()
        {
            this.metroSetListBox1.Items.Add("Valve1[DJ-Series]");
            this.metroSetListBox1.Items.Add("Valve1[SV-Series]");

            this.metroSetListBox2.Items.Add("Valve1[DJ-Series]");
            this.metroSetListBox2.Items.Add("Valve1[SV-Series]");

            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                this.metroSetListBox1.SelectedIndex = 0;
                this.valve1 = new JtValve(Machine.Instance.Valve1);
            }
            else
            {
                this.metroSetListBox1.SelectedIndex = 1;
                this.valve1 = new SvValve(Machine.Instance.Valve1);
            }
            if (Machine.Instance.Valve2.ValveSeries == ValveSeries.喷射阀)
            {
                this.metroSetListBox2.SelectedIndex = 0;
                this.valve2 = new JtValve(Machine.Instance.Valve2);
            }
            else
            {
                this.metroSetListBox2.SelectedIndex = 1;
                this.valve2 = new SvValve(Machine.Instance.Valve2);
            }

            this.cmbValveSelect.Items.Add(strValveSetup1);
            this.cmbValveSelect.Items.Add(strValveSetup2);
            this.cmbValveSelect.SelectedIndex = (int)Machine.Instance.Setting.ValveSelect;
            if (Machine.Instance.Setting != null)
            {
                this.settingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
            }
        }

        private void btnEdit1_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve1).ShowDialog();
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve2).ShowDialog();
        }

        private void CmbValveSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbValveSelect.SelectedIndex == 0)
            {
                this.metroSetPanel2.Enabled = false;
            }
            else
            {
                this.metroSetPanel2.Enabled = true;
            }
        }

        private void MetroSetListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.metroSetTextBox1.Text = this.metroSetListBox1.SelectedItem.ToString();

            if ((int)Machine.Instance.Valve1.ValveSeries != this.metroSetListBox1.SelectedIndex)
            {
                if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve1 = new SvValve(this.valve1, ValvePrmMgr.Instance.FindBy(0));
                }
                else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
                {
                    Machine.Instance.Valve1 = new JtValve(this.valve1, ValvePrmMgr.Instance.FindBy(0));
                }
            }
            Machine.Instance.Valve1.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve1);

        }

        private void MetroSetListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.metroSetTextBox2.Text = this.metroSetListBox2.SelectedItem.ToString();

            if ((int)Machine.Instance.Valve2.ValveSeries != this.metroSetListBox2.SelectedIndex)
            {
                if (Machine.Instance.Valve2.ValveSeries == ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve2 = new SvValve(this.valve2, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
                }
                else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀)
                {
                    Machine.Instance.Valve2 = new JtValve(this.valve2, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
                }
            }
            Machine.Instance.Valve2.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve2);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Machine.Instance.Setting.ValveSelect = (ValveSelection)this.cmbValveSelect.SelectedIndex;
            if (!Machine.Instance.Valve1.ValveSeries.Equals(Machine.Instance.Valve2.ValveSeries) && Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                MetroSetMessageBox.Show(this, "请选择同类型阀组", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Machine.Instance.SetupValve();
            Machine.Instance.Valve1.ValveSeries = (ValveSeries)this.metroSetListBox1.SelectedIndex;
            Machine.Instance.Valve2.ValveSeries = (ValveSeries)this.metroSetListBox2.SelectedIndex;
            ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires = (ValveSeries)this.metroSetListBox1.SelectedIndex;

            ValvePrmMgr.Instance.FindBy(ValveType.Valve2).ValveSeires = (ValveSeries)this.metroSetListBox2.SelectedIndex;
            Machine.Instance.Setting.Save();
            Machine.Instance.SaveValveSettings();

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                if (Machine.Instance.Valve1.ValveSeries == Machine.Instance.Valve2.ValveSeries)
                {
                    if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
                    {
                        Machine.Instance.DualValve = new JtDualValve(CardMgr.Instance.FindBy(0), Machine.Instance.Valve1, Machine.Instance.Valve2);
                    }
                    if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
                    {
                        Machine.Instance.DualValve = new SvDualValve(CardMgr.Instance.FindBy(0), Machine.Instance.Valve1, Machine.Instance.Valve2);
                    }
                }
            }
            MsgCenter.Broadcast(MachineMsg.SETUP_VALVE, this, Machine.Instance.Valve1.ValveSeries, Machine.Instance.Valve2.ValveSeries);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ValvePrmMgr.Instance.InsertIndex((int)valve1.ValveType, valve1.Prm);
            ValvePrmMgr.Instance.InsertIndex((int)valve2.ValveType, valve2.Prm);
            Machine.Instance.Valve1 = this.valve1;
            Machine.Instance.Valve2 = this.valve2;
            CompareObj.CompareProperty(Machine.Instance.Setting, this.settingBackUp, null, this.GetType().Name);

        }
    }
}
