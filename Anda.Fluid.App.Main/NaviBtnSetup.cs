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
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.AccessControl;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnSetup : UserControlEx
    {
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strSetupMachine = "Setup Machine";
        private string strSetupAxes = "Setup Axes";
        private string strSetupVision = "Setup Vision";
        private string strSetupValves = "Setup Valves";
        private string strSetupSensors = "Setup Sensors";
        private string strSetupConveyor = "Setup Conveyor";
        private string strSetupLocations = "Setup Locations";
        private string strSetupAccess = "Setup Access";

        public NaviBtnSetup()
        {
            InitializeComponent();

            lngResources.Add(strSetupAxes, "Setup Axes");
            lngResources.Add(strSetupVision, "Setup Vision");
            lngResources.Add(strSetupValves, "Setup Valves");
            lngResources.Add(strSetupConveyor, "Setup Conveyor");
            lngResources.Add(strSetupLocations, "Setup Locations");
            lngResources.Add(strSetupAccess, "Setup Access");
            lngResources.Add(strSetupSensors, "Setup Sensors");
            lngResources.Add(strSetupMachine, "Setup Machine");
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(lngResources[strSetupAxes]).Name = strSetupAxes;
            this.cms.Items.Add(lngResources[strSetupVision]).Name = strSetupVision;
            this.cms.Items.Add(lngResources[strSetupValves]).Name = strSetupValves;
            this.cms.Items.Add(lngResources[strSetupConveyor]).Name = strSetupConveyor;
            this.cms.Items.Add(lngResources[strSetupLocations]).Name = strSetupLocations;
            this.cms.Items.Add(lngResources[strSetupAccess]).Name = strSetupAccess;
            this.cms.Items.Add(lngResources[strSetupSensors]).Name = strSetupSensors;
            this.cms.Items.Add(lngResources[strSetupMachine]).Name = strSetupMachine;
            if (RoleMgr.Instance.CurrentRole != null)
            {
                UpdateUI();
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnAdvanced.Click += BtnAdvanced_Click;
            this.btnAdvanced.MouseMove += this.ReadDisplayTip;
            this.btnAdvanced.MouseLeave += this.DisopTip;
        }
        public override void SaveLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            this.SaveKeyValueToResources(strSetupMachine, lngResources[strSetupMachine]);
            this.SaveKeyValueToResources(strSetupAxes, lngResources[strSetupAxes]);
            this.SaveKeyValueToResources(strSetupVision, lngResources[strSetupVision]);
            this.SaveKeyValueToResources(strSetupValves, lngResources[strSetupValves]);
            this.SaveKeyValueToResources(strSetupSensors, lngResources[strSetupSensors]);
            this.SaveKeyValueToResources(strSetupConveyor, lngResources[strSetupConveyor]);
            this.SaveKeyValueToResources(strSetupLocations, lngResources[strSetupLocations]);
            this.SaveKeyValueToResources(strSetupAccess, lngResources[strSetupAccess]);
        }
        public override void ReadLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            if (this.HasLngResources())
            {
                lngResources[strSetupMachine] = this.ReadKeyValueFromResources(strSetupMachine);
                lngResources[strSetupAxes] = this.ReadKeyValueFromResources(strSetupAxes);
                lngResources[strSetupVision] = this.ReadKeyValueFromResources(strSetupVision);
                lngResources[strSetupValves] = this.ReadKeyValueFromResources(strSetupValves);
                lngResources[strSetupSensors] = this.ReadKeyValueFromResources(strSetupSensors);
                lngResources[strSetupConveyor] = this.ReadKeyValueFromResources(strSetupConveyor);
                lngResources[strSetupLocations] = this.ReadKeyValueFromResources(strSetupLocations);
                lngResources[strSetupAccess] = this.ReadKeyValueFromResources(strSetupAccess);
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
                    if(((temp == "Setup Machine") || (temp == "Setup Sensors")) && !RoleMgr.Instance.IsDeveloper())
                    {
                        this.cms.Items[temp].Text = "";
                    }
                }
            }
        }
        public void UpdateUI()
        {
            if(RoleMgr.Instance.IsDeveloper())
            {
                this.cms.Items[strSetupMachine].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupMotion;
                this.cms.Items[strSetupSensors].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupSensors;
            }
            this.cms.Items[strSetupAxes].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupAxes;
            this.cms.Items[strSetupVision].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupVision;
            this.cms.Items[strSetupValves].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupValves;
            this.cms.Items[strSetupConveyor].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupConveyor;
            this.cms.Items[strSetupLocations].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupVision;
            this.cms.Items[strSetupAccess].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupAccess;
            this.ReadLanguageResources();
        }

        private void BtnAdvanced_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == lngResources[strSetupMachine] && RoleMgr.Instance.IsDeveloper())
            {
                new SettingMachineForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupAxes])
            {
                new SettingAxesForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupVision])
            {
                new SetupVisionForm().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strSetupValves])
            {
                new ValveSetupForm().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strSetupSensors] && RoleMgr.Instance.IsDeveloper())
            {
                new SettingSensorsForm().ShowDialog();
                Account tempAccount = AccountMgr.Instance.FindBy("Supervisor");
                AccountMgr.Instance.SwitchUser(tempAccount);
            }
            else if (itemText == lngResources[strSetupConveyor])
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.进入轨道参数界面);
            }
            else if (itemText == lngResources[strSetupLocations])
            {
                new DialogEditLocations().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strSetupAccess])
            {
                new FeatureAccessForm().ShowDialog();
            }
        }
    }
}
