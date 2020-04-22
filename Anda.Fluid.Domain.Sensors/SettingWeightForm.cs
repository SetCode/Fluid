using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using Newtonsoft.Json;
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
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Sensors
{

    public partial class SettingWeightForm : FormEx
    {
        #region 语言切换文本变量
        private string resetMsg = "加载默认参数，结果参数会清空！是否继续？";
        private string msgTitle = "提示";
	#endregion
	private ValveWeightPrm valveWeightPrmBackUp;
        private ValveWeightPrm valveWeightPrm;
	/// <summary>
        /// 当前在编辑的阀
        /// </summary>
        private ValveType valveKey;
        public SettingWeightForm()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        public override void ReadLanguageResources(bool saveButton = false, bool saveRadioButton = false, bool saveCheckBox = false, bool saveLabel = false)
        {
            base.ReadLanguageResources(saveButton, saveRadioButton, saveCheckBox, saveLabel);
            resetMsg = this.ReadKeyValueFromResources("resetMsg");
            msgTitle = this.ReadKeyValueFromResources("msgTitle");
        }
        public override void SaveLanguageResources(bool skipButton = false, bool saveRadioButton = false, bool saveCheckBox = false, bool saveLabel = false)
        {
            this.SaveKeyValueToResources("resetMsg", resetMsg);
            this.SaveKeyValueToResources("msgTitle", msgTitle);
            this.SaveProportyGridLngText(new ValveWeightPrm(ValveType.Valve1));
            base.SaveLanguageResources(skipButton, saveRadioButton, saveCheckBox, saveLabel);
        }
        public SettingWeightForm Setup(ValveWeightPrm valWtPrm)
        {
            this.valveKey = valWtPrm.Key;
            LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(valWtPrm, this.GetType().Name);
            this.propertyGrid1.SelectedObject = proxyObj;
            this.valveWeightPrm = valWtPrm;
            this.valveWeightPrmBackUp = (ValveWeightPrm)valWtPrm.Clone();
            return this;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //重新加载默认参数

            if (MessageBox.Show(resetMsg, msgTitle, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Machine.Instance.ResetWeightSetting();
                Machine.Instance.ResetValveWeightSettings();
                this.RefreshPrgs();
            }

        }


        private void RefreshPrgs()
        {
            this.propertyGrid1.ExpandAllGridItems();
            this.propertyGrid1.Refresh();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            ValveType otherPrm;
            if (this.valveKey == ValveType.Valve1)
            {
                otherPrm = ValveType.Valve2;
            }
            else
            {
                otherPrm = ValveType.Valve1;
            }
            ValveWeightPrmMgr.Instance.FindBy(otherPrm).Percentage = ValveWeightPrmMgr.Instance.FindBy(this.valveKey).Percentage;
            Machine.Instance.Scale.Scalable.SavePrm();
           
            ValveWeightPrmMgr.Instance.Save();
            this.Close();
        }

        private void SettingWeightForm_Load(object sender, EventArgs e)
        {
            this.RefreshPrgs();
        }

        private void SettingWeightForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.valveWeightPrm!=null && this.valveWeightPrmBackUp!=null)
            {
                //参数修改记录                       
                CompareObj.CompareProperty(this.valveWeightPrm, this.valveWeightPrmBackUp);
            }
            
        }
    }


}
