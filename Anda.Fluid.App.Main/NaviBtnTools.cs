using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Domain.Sensors;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.DataStatistics;

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnTools : UserControlEx
    {
        private ContextMenuStrip cms;

        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strIO = "IO";
        private string strConveyorIO = "ConveyorIO";
        private string strScale = "Scale";
        private string strHeight = "Height";
        private string strHeater = "Heater";
        private string strCamera = "Camera";

        public NaviBtnTools()
        {
            InitializeComponent();
            lngResources.Add(strIO, "IO");
            lngResources.Add(strConveyorIO, "ConveyorIO");
            lngResources.Add(strScale, "Scale");
            lngResources.Add(strHeight, "Height");
            lngResources.Add(strHeater, "Heater");
            lngResources.Add(strCamera, "Camera");
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(lngResources[strIO]).Name = strIO;
            this.cms.Items.Add(lngResources[strConveyorIO]).Name = strConveyorIO;
            this.cms.Items.Add(lngResources[strScale]).Name = strScale;
            this.cms.Items.Add(lngResources[strHeight]).Name = strHeight;
            this.cms.Items.Add(lngResources[strHeater]).Name = strHeater;
            this.cms.Items.Add(lngResources[strCamera]).Name = strCamera;
            if (RoleMgr.Instance.CurrentRole != null)
            {
                UpdateUI();
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnTools.Click += BtnTools_Click;
            this.btnTools.MouseMove += this.ReadDisplayTip;
            this.btnTools.MouseLeave += this.DisopTip;
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                lngResources[strIO] = this.ReadKeyValueFromResources(strIO);
                lngResources[strConveyorIO] = this.ReadKeyValueFromResources(strConveyorIO);
                lngResources[strScale] = this.ReadKeyValueFromResources(strScale);
                lngResources[strHeight] = this.ReadKeyValueFromResources(strHeight);
                lngResources[strHeater] = this.ReadKeyValueFromResources(strHeater);
                lngResources[strCamera] = this.ReadKeyValueFromResources(strCamera);
                foreach (string temp in lngResources.Keys)
                {
                    if (lngResources[temp] == "")
                    {
                        this.cms.Items[temp].Text = temp;
                    }
                    else
                    {
                        this.cms.Items[temp].Text = lngResources[temp];
                    }
                }
            }
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(strIO, lngResources[strIO]);
            this.SaveKeyValueToResources(strConveyorIO, lngResources[strConveyorIO]);
            this.SaveKeyValueToResources(strScale, lngResources[strScale]);
            this.SaveKeyValueToResources(strHeight, lngResources[strHeight]);
            this.SaveKeyValueToResources(strHeater, lngResources[strHeater]);
            this.SaveKeyValueToResources(strCamera, lngResources[strCamera]);
        }
        /// <summary>
        /// 权限管理UI更新
        /// </summary>
        public void UpdateUI()
        {
            this.cms.Items[strIO].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterIoForm;
            this.cms.Items[strConveyorIO].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterIoForm;
            this.cms.Items[strScale].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterScaleForm;
            this.cms.Items[strHeight].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterHeightForm;
            this.cms.Items[strHeater].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterHeaterForm;
            this.cms.Items[strCamera].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanEnterCameraForm;
            this.ReadLanguageResources();
        }

        private void BtnTools_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == lngResources[strIO])
            {
                FormMgr.Show<IOForm>(this);
            }
            else if (itemText == lngResources[strConveyorIO])
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
                {
                    new Drive.Conveyor.LeadShine.Forms.IOForms.IOForm().ShowDialog();
                }              
            }
            else if (itemText == lngResources[strScale])
            {
                new FormWeight().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strHeight])
            {
                new DialogHeight().ShowDialog();
            }
            else if (itemText == lngResources[strHeater])
            {
                new FormThermostat().ShowDialog();
            }
            else if (itemText == lngResources[strCamera])
            {
                new TriggerDemo().ShowDialog();
                //new StatisticsForm().ShowDialog();
            }

        }
    }
}
