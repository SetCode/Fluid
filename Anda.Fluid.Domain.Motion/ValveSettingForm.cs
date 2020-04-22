using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
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
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Motion
{
    public partial class ValveSettingForm : FormEx
    {
        private Valve valve;

        private JtValvePrm jtValvePrmBackUp = new JtValvePrm();
        private SvValvePrm svValvePrmBackUp = new SvValvePrm();
        private GearValvePrm gearValvePrmBackUp = new GearValvePrm();
        public ValveSettingForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.btnReset.Click += BtnReset_Click;
            this.btnSave.Click += BtnSave_Click;
            this.ReadLanguageResources();
        }

        public ValveSettingForm Setup(Valve valve)
        {
            this.valve = valve;

            if (this.valve.ValveSeries == ValveSeries.喷射阀)
            {
                JtValve jtValve = (JtValve)valve;
                LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(jtValve.Prm, this.GetType().Name);
                this.propertyGrid1.SelectedObject = proxyObj;
                this.jtValvePrmBackUp = (JtValvePrm)jtValve.Prm.Clone();
            }
            else if (this.valve.ValveSeries == ValveSeries.螺杆阀)
            {
                SvValve svValve = (SvValve)valve;
                LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(svValve.Prm, this.GetType().Name);
                this.propertyGrid1.SelectedObject = proxyObj;
                this.svValvePrmBackUp = (SvValvePrm)svValve.Prm.Clone();
            }
            else if (this.valve.ValveSeries == ValveSeries.齿轮泵阀)
            {
                GearValve gearValve = (GearValve)valve;
                LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(gearValve.Prm, this.GetType().Name);
                this.propertyGrid1.SelectedObject = proxyObj;
                this.gearValvePrmBackUp = (GearValvePrm)gearValve.Prm.Clone();
            }

            return this;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveProportyGridLngText(new JtValvePrm());
            this.SaveProportyGridLngText(new SvValvePrm());
            this.SaveProportyGridLngText(new GearValvePrm());
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string tag = this.GetType().Name;
            if (this.valve.ValveSeries == ValveSeries.喷射阀)
            {
                JtValve jtValve = (JtValve)this.valve;
                ValvePrmMgr.Instance.FindBy(this.valve.ValveType).JtValvePrm = jtValve.Prm;
                CompareObj.CompareProperty(jtValve.Prm, this.jtValvePrmBackUp, null, tag);
            }
            else if (this.valve.ValveSeries == ValveSeries.螺杆阀)
            {
                SvValve svValve = (SvValve)this.valve;
                ValvePrmMgr.Instance.FindBy(this.valve.ValveType).SvValvePrm = svValve.Prm;
                CompareObj.CompareProperty(svValve.Prm, this.svValvePrmBackUp, null, tag);
            }
            else if (this.valve.ValveSeries == ValveSeries.齿轮泵阀)
            {
                GearValve gearValve = (GearValve)this.valve;
                ValvePrmMgr.Instance.FindBy(this.valve.ValveType).GearValvePrm = gearValve.Prm;
                CompareObj.CompareProperty(gearValve.Prm, this.gearValvePrmBackUp, null, tag);
            }

            Machine.Instance.SaveValveSettings();
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Reset Valve Setting?", "Valve Setting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            if (MessageBox.Show("将阀参数设置为默认值?", "阀设置", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //设置为默认
                Machine.Instance.ResetValveSettings();

                if (this.valve.ValveSeries == ValveSeries.喷射阀)
                {
                    JtValve jtValve = (JtValve)valve;
                    this.propertyGrid1.SelectedObject = jtValve.Prm;
                }
                else if (this.valve.ValveSeries == ValveSeries.螺杆阀)
                {
                    SvValve svValve = (SvValve)valve;
                    this.propertyGrid1.SelectedObject = svValve.Prm;
                }
                else if (this.valve.ValveSeries == ValveSeries.齿轮泵阀)
                {
                    GearValve gearValve = (GearValve)valve;
                    this.propertyGrid1.SelectedObject = gearValve.Prm;
                }

            }
        }
    }
}
