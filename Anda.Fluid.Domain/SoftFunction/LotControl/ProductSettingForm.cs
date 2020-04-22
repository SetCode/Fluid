using System;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.SoftFunction.LotControl
{

    ///<summary>
    /// Description	:主界面参数设置界面，面向所有操作者
    /// Author  	:liyi
    /// Date		:2019/06/24
    ///</summary>   
    public partial class ProductSettingForm : FormEx
    {
        private FluidProgram fluidProgram;
        private RuntimeSettings runtimeSettingsBackUp;
        public ProductSettingForm(FluidProgram fluidProgram)
        {
            InitializeComponent();
            this.fluidProgram = fluidProgram;
            this.cbxLotControl.Checked = this.fluidProgram.LotControlEnable;
            this.cbxStartLotById.Checked = this.fluidProgram.RuntimeSettings.IsStartLotById;
            this.ReadLanguageResources();
            if (this.fluidProgram.RuntimeSettings != null)
            {
                this.runtimeSettingsBackUp = (RuntimeSettings)this.fluidProgram.RuntimeSettings.Clone();
            }
        }
        /// <summary>
        /// 仅用于语言文件生成
        /// </summary>
        private ProductSettingForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool lotControlEnable = this.fluidProgram.LotControlEnable;
            this.fluidProgram.LotControlEnable = this.cbxLotControl.Checked;
            this.fluidProgram.RuntimeSettings.IsStartLotById = this.cbxStartLotById.Checked;
            this.Close();
            if (lotControlEnable != this.fluidProgram.LotControlEnable)
            {
                string msg = string.Format("LotControlEnable oldValue:{0}->newValue{1}", lotControlEnable, this.fluidProgram.LotControlEnable);
                Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
            }
            if (this.fluidProgram.RuntimeSettings!=null && this.runtimeSettingsBackUp!=null)
            {
                CompareObj.CompareProperty(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
                CompareObj.CompareProperty(this.fluidProgram.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
