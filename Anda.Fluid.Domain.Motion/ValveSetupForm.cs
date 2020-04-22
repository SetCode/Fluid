using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.UI;
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
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Motion
{
    public partial class ValveSetupForm : FormEx, IMsgSender
    {
        private string strValveSetup1 = "单阀";
        private string strValveSetup2 = "双阀";
        private Valve valve1, valve2;
        private MachineSetting settingBackUp;

        public ValveSetupForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.btnEdit1.Click += BtnEdit1_Click;
            this.btnDelete1.Click += BtnDelete1_Click;
            this.btnSaveAs1.Click += BtnSaveAs1_Click;
            this.btnEdit2.Click += BtnEdit2_Click;
            this.btnDelete2.Click += BtnDelete2_Click;
            this.btnSaveAs2.Click += BtnSaveAs2_Click;

            this.cmbValveSelect.SelectedIndexChanged += CmbValveSelect_SelectedIndexChanged;
            this.listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            this.listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            this.ReadLanguageResources();

        }
        public ValveSetupForm Setup()
        {
            this.listBox1.Items.Add("喷射阀[DJ-Series]");
            this.listBox1.Items.Add("螺杆阀[SV-Series]");
            this.listBox1.Items.Add("齿轮泵阀[Gear-Series]");

            this.listBox2.Items.Add("喷射阀[DJ-Series]");
            this.listBox2.Items.Add("螺杆阀[SV-Series]");
            this.listBox2.Items.Add("齿轮泵阀[Gear-Series]");

            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                this.listBox1.SelectedIndex = 0;
                this.valve1 = new JtValve(Machine.Instance.Valve1);
            }
            else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
            {
                this.listBox1.SelectedIndex = 1;
                this.valve1 = new SvValve(Machine.Instance.Valve1);
            }
            else if(Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.listBox1.SelectedIndex = 2;
                this.valve1 = new GearValve(Machine.Instance.Valve1);
            }

            if (Machine.Instance.Valve2.ValveSeries == ValveSeries.喷射阀)
            {
                this.listBox2.SelectedIndex = 0;
                this.valve2 = new JtValve(Machine.Instance.Valve2);
            }
            else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀)
            {
                this.listBox2.SelectedIndex = 1;
                this.valve2 = new SvValve(Machine.Instance.Valve2);
            }
            else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.listBox2.SelectedIndex = 2;
                this.valve2 = new GearValve(Machine.Instance.Valve2);
            }


            this.cmbValveSelect.Items.Add(strValveSetup1);
            this.cmbValveSelect.Items.Add(strValveSetup2);
            this.cmbValveSelect.SelectedIndex = (int)Machine.Instance.Setting.ValveSelect;
            if (Machine.Instance.Setting != null)
            {
                this.settingBackUp = (MachineSetting)Machine.Instance.Setting.Clone();
            }
            return this;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources(ValveSelection.单阀.ToString(), strValveSetup1);
            this.SaveKeyValueToResources(ValveSelection.双阀.ToString(), strValveSetup2);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            strValveSetup1 = this.ReadKeyValueFromResources(ValveSelection.单阀.ToString());
            strValveSetup2 = this.ReadKeyValueFromResources(ValveSelection.双阀.ToString());
        }

        private void BtnEdit1_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve1).ShowDialog();
        }

        private void BtnDelete1_Click(object sender, EventArgs e)
        {

        }

        private void BtnSaveAs1_Click(object sender, EventArgs e)
        {

        }

        private void BtnEdit2_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve2).ShowDialog();
        }

        private void BtnDelete2_Click(object sender, EventArgs e)
        {

        }

        private void BtnSaveAs2_Click(object sender, EventArgs e)
        {

        }

        private void CmbValveSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateUI();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.listBox1.SelectedItem.ToString();

            if ((int)Machine.Instance.Valve1.ValveSeries != this.listBox1.SelectedIndex)
            {
                if (this.listBox1.SelectedIndex == (int)ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve1 = new JtValve(this.valve1, ValvePrmMgr.Instance.FindBy(0));
                }
                else if (this.listBox1.SelectedIndex == (int)ValveSeries.螺杆阀)
                {
                    Machine.Instance.Valve1 = new SvValve(this.valve1, ValvePrmMgr.Instance.FindBy(0));
                }
                else if (this.listBox1.SelectedIndex == (int)ValveSeries.齿轮泵阀)
                {
                    Machine.Instance.Valve1 = new GearValve(this.valve1, ValvePrmMgr.Instance.FindBy(0));
                }
                //单阀模式下直接将副阀的类型与主阀同步，避免切换双阀更改再切换回去
                if (this.cmbValveSelect.SelectedIndex == 0)
                {
                    this.listBox2.SelectedIndex = this.listBox1.SelectedIndex;
                }
            }
            Machine.Instance.Valve1.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve1);
            //this.valve1.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve1);
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = this.listBox2.SelectedItem.ToString();

            if ((int)Machine.Instance.Valve2.ValveSeries != this.listBox2.SelectedIndex)
            {
                if (this.listBox2.SelectedIndex == (int)ValveSeries.喷射阀)
                {
                    Machine.Instance.Valve2 = new JtValve(this.valve2, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
                }
                else if (this.listBox2.SelectedIndex == (int)ValveSeries.螺杆阀)
                {
                    Machine.Instance.Valve2 = new SvValve(this.valve2, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
                }
                else if (this.listBox2.SelectedIndex == (int)ValveSeries.齿轮泵阀)
                {
                    Machine.Instance.Valve2 = new GearValve(this.valve2, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
                }

            }

            Machine.Instance.Valve2.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve2);
        }

        private void updateUI()
        {
            if (this.cmbValveSelect.SelectedIndex == 0)
            {
                this.groupBox2.Enabled = false;
            }
            else
            {
                this.groupBox2.Enabled = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Machine.Instance.Setting.ValveSelect = (ValveSelection)this.cmbValveSelect.SelectedIndex;
            if (!Machine.Instance.Valve1.ValveSeries.Equals(Machine.Instance.Valve2.ValveSeries))
            {
                MessageBox.Show("请选择同类型阀组");
                return;
            }            
            Machine.Instance.SetupValve();
            Machine.Instance.Valve1.ValveSeries = (ValveSeries)this.listBox1.SelectedIndex;
            Machine.Instance.Valve2.ValveSeries = (ValveSeries)this.listBox2.SelectedIndex;
            ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires = (ValveSeries)this.listBox1.SelectedIndex;

            ValvePrmMgr.Instance.FindBy(ValveType.Valve2).ValveSeires = (ValveSeries)this.listBox2.SelectedIndex;
            Machine.Instance.Setting.Save();
            Machine.Instance.SaveValveSettings();

            if (Machine.Instance.Setting.ValveSelect==ValveSelection.双阀)
            {
                if (Machine.Instance.Valve1.ValveSeries == Machine.Instance.Valve2.ValveSeries)
                {
                    if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
                    {
                        Machine.Instance.DualValve = new JtDualValve(CardMgr.Instance.FindBy(0), Machine.Instance.Valve1, Machine.Instance.Valve2);
                    }
                    else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
                    {
                        Machine.Instance.DualValve = new SvDualValve(CardMgr.Instance.FindBy(0), Machine.Instance.Valve1, Machine.Instance.Valve2);
                    }
                    else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
                    {
                        Machine.Instance.DualValve = new GearDualValve(CardMgr.Instance.FindBy(0), Machine.Instance.Valve1, Machine.Instance.Valve2);
                    }

                }
            }          
            MsgCenter.Broadcast(MachineMsg.SETUP_VALVE, this, Machine.Instance.Valve1.ValveSeries, Machine.Instance.Valve2.ValveSeries);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ValvePrmMgr.Instance.InsertIndex((int)valve1.ValveType, valve1.Prm);
            ValvePrmMgr.Instance.InsertIndex((int)valve2.ValveType, valve2.Prm);
            Machine.Instance.Valve1 = this.valve1;
            Machine.Instance.Valve2 = this.valve2;

            this.Close();
            CompareObj.CompareProperty(Machine.Instance.Setting, this.settingBackUp, null, this.GetType().Name);
        }
    }
}