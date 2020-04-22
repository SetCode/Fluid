using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SoftFunction.LotControl
{
    public partial class LotControlSettingForm : FormEx
    {
        private FluidProgram fluidProgram;
        private RuntimeSettings runtimeSettingsBackUp;
        public LotControlSettingForm(FluidProgram fluidProgram)
        {
            InitializeComponent();
            this.fluidProgram = fluidProgram;
            this.tbxLotStartPos.Text = fluidProgram.RuntimeSettings.LotIdStartPos.ToString();
            this.tbxLotEndPos.Text = fluidProgram.RuntimeSettings.LotIdEndPos.ToString();
            this.ReadLanguageResources();
            if (this.fluidProgram.RuntimeSettings != null)
            {
                this.runtimeSettingsBackUp = (RuntimeSettings)this.fluidProgram.RuntimeSettings.Clone();
            }
        }

        private LotControlSettingForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //string tipText = "End Pos cannot be smaller than Start Pos";
            string tipText = "终点坐标不能小于起点坐标";
            if (tbxLotEndPos.Value < tbxLotStartPos.Value)
            {
                MessageBox.Show(tipText);
                return;
            }
            this.fluidProgram.RuntimeSettings.LotId = this.tbxLotID.Text;
            this.fluidProgram.RuntimeSettings.LotIdStartPos = tbxLotStartPos.Value;
            this.fluidProgram.RuntimeSettings.LotIdEndPos = tbxLotEndPos.Value;
            this.Close();
            if (this.fluidProgram.RuntimeSettings!=null && this.runtimeSettingsBackUp!=null)
            {
                CompareObj.CompareField(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
                CompareObj.CompareProperty(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
            }
            
        }
    }
}
