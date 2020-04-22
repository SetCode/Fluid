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
    public partial class NaviBtnAdvanced : UserControlEx
    {
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strSetupMachine = "Setup Machine";
        private string strSetupMotion = "Setup Motion";
        private string strSetupAxes = "Setup Axes";
        private string strSetupIO = "Setup IO";
        private string strSetupVision = "Setup Vision";
        private string strSetupValves = "Setup Valves";
        private string strSetupSensors = "Setup Sensors";
        private string strSetupConveyor = "Setup Conveyor";
        private string strStripMapping = "Strip Mapping";
        private string strSetupAccess = "Setup Access";

        public NaviBtnAdvanced()
        {
            InitializeComponent();
            lngResources.Add(strSetupMachine, "Setup Machine");
            lngResources.Add(strSetupMotion, "Setup Motion");
            lngResources.Add(strSetupAxes, "Setup Axes");
            lngResources.Add(strSetupIO, "Setup IO");
            lngResources.Add(strSetupVision, "Setup Vision");
            lngResources.Add(strSetupValves, "Setup Valves");
            lngResources.Add(strSetupSensors, "Setup Sensors");
            lngResources.Add(strSetupConveyor, "Setup Conveyor");
            lngResources.Add(strStripMapping, "Strip Mapping");
            lngResources.Add(strSetupAccess, "Setup Access");
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(lngResources[strSetupMachine]).Name = strSetupMachine;
            this.cms.Items.Add(lngResources[strSetupMotion]).Name = strSetupMotion;
            this.cms.Items.Add(lngResources[strSetupAxes]).Name = strSetupAxes;
            this.cms.Items.Add(lngResources[strSetupIO]).Name = strSetupIO;
            this.cms.Items.Add(lngResources[strSetupVision]).Name = strSetupVision;
            this.cms.Items.Add(lngResources[strSetupValves]).Name = strSetupValves;
            this.cms.Items.Add(lngResources[strSetupSensors]).Name = strSetupSensors;
            this.cms.Items.Add(lngResources[strSetupConveyor]).Name = strSetupConveyor;
            this.cms.Items.Add(lngResources[strStripMapping]).Name = strStripMapping;
            this.cms.Items.Add(lngResources[strSetupAccess]).Name = strSetupAccess;
            if (RoleMgr.Instance.CurrentRole != null)
            {
                UpdateUI();
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnAdvanced.Click += BtnAdvanced_Click;
        }
        public override void SaveLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            this.SaveKeyValueToResources(strSetupMachine, lngResources[strSetupMachine]);
            this.SaveKeyValueToResources(strSetupMotion, lngResources[strSetupMotion]);
            this.SaveKeyValueToResources(strSetupAxes, lngResources[strSetupAxes]);
            this.SaveKeyValueToResources(strSetupIO, lngResources[strSetupIO]);
            this.SaveKeyValueToResources(strSetupVision, lngResources[strSetupVision]);
            this.SaveKeyValueToResources(strSetupValves, lngResources[strSetupValves]);
            this.SaveKeyValueToResources(strSetupSensors, lngResources[strSetupSensors]);
            this.SaveKeyValueToResources(strSetupConveyor, lngResources[strSetupConveyor]);
            this.SaveKeyValueToResources(strStripMapping, lngResources[strStripMapping]);
            this.SaveKeyValueToResources(strSetupAccess, lngResources[strSetupAccess]);
        }
        public override void ReadLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            if (this.HasLngResources())
            {
                lngResources[strSetupMachine] = this.ReadKeyValueFromResources(strSetupMachine);
                lngResources[strSetupMotion] = this.ReadKeyValueFromResources(strSetupMotion);
                lngResources[strSetupAxes] = this.ReadKeyValueFromResources(strSetupAxes);
                lngResources[strSetupIO] = this.ReadKeyValueFromResources(strSetupIO);
                lngResources[strSetupVision] = this.ReadKeyValueFromResources(strSetupVision);
                lngResources[strSetupValves] = this.ReadKeyValueFromResources(strSetupValves);
                lngResources[strSetupSensors] = this.ReadKeyValueFromResources(strSetupSensors);
                lngResources[strSetupConveyor] = this.ReadKeyValueFromResources(strSetupConveyor);
                lngResources[strStripMapping] = this.ReadKeyValueFromResources(strStripMapping);
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
                }
            }
        }
        public void UpdateUI()
        {
            this.cms.Items[strSetupMachine].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupMotion;
            this.cms.Items[strSetupMotion].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupMotion;
            this.cms.Items[strSetupAxes].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupAxes;
            this.cms.Items[strSetupIO].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupIO;
            this.cms.Items[strSetupVision].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupVision;
            this.cms.Items[strSetupValves].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupValves;
            this.cms.Items[strSetupSensors].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupSensors;
            this.cms.Items[strSetupConveyor].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupConveyor;
            this.cms.Items[strStripMapping].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanStripMapping;
            this.cms.Items[strSetupAccess].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupAccess;
            if (RoleMgr.Instance.IsDeveloper())
            {
                if (!this.cms.Items.ContainsKey("生成语言文件"))
                {
                    this.cms.Items.Add("生成语言文件").Name = "生成语言文件";
                }
            }
            else
            {
                if (this.cms.Items.ContainsKey("生成语言文件"))
                {
                    this.cms.Items.RemoveByKey("生成语言文件");
                }
            }
            this.ReadLanguageResources();
        }

        private void BtnAdvanced_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if(itemText == lngResources[strSetupMachine])
            {
                new SettingMachineForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupMotion])
            {
                new SettingRobotForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupAxes])
            {
                new SettingAxesForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupIO])
            {
                //new IOSetupForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupVision])
            {
                new SetupVisionForm().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strSetupValves])
            {
                new ValveSetupForm().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strSetupSensors])
            {
                new SettingSensorsForm().ShowDialog();
            }
            else if (itemText == lngResources[strSetupConveyor])
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.进入轨道参数界面);
            }
            else if (itemText == lngResources[strStripMapping])
            {
                new DialogCalibMap().ShowDialog();
            }
            else if (itemText == lngResources[strSetupAccess])
            {
                new FeatureAccessForm().ShowDialog();
            }
            else if (itemText == "生成语言文件")
            {
                LanguageHelper.Instance.SaveAllFormLanguageResource();
            }
        }
    }
}
