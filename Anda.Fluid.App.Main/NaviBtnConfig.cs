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
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.App.Settings;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.SVO;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.App.EditInspection;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Domain.Dialogs.Cpks;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.SoftFunction.PatternWeight;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.App.AngleHeightPoseCorrect;
using Anda.Fluid.Domain.Dialogs.RTVPurge;

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnConfig : UserControlEx, IMsgSender
    {
        private ContextMenuStrip cms;
        private ToolStripMenuItem tsiLanguage;
        private ToolStripMenuItem tsiUnit;

        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strLanguage = "Language";
        private string strEnglish = "English";
        private string strChinese = "Chinese";
        private string strUnit = "Unit";
        private string strMm = "mm";
        private string strInch = "inch";
        private string strSetupMotion = "Setup Motion";
        private Image imgChecked;

        public NaviBtnConfig()
        {
            InitializeComponent();
            //先生成对应数据结构
            lngResources.Add(strLanguage, "Language");
            lngResources.Add(strEnglish, "English");
            lngResources.Add(strChinese, "Chinese");
            lngResources.Add(strUnit, "Unit");
            lngResources.Add(strMm, "mm");
            //lngResources.Add(strInch, "inch");
            lngResources.Add(strSetupMotion, "Setup Motion");
            //再读取文本数据到数据结构
            this.imgChecked = Properties.Resources.Checkmark_16px;
            this.tsiLanguage = new ToolStripMenuItem(lngResources[strLanguage]);
            this.tsiLanguage.DropDownItems.Add(lngResources[strEnglish]);
            //this.tsiLanguage.DropDownItems[0].Enabled = false;
            this.tsiLanguage.DropDownItems.Add(lngResources[strChinese], this.imgChecked);
            this.tsiUnit = new ToolStripMenuItem(lngResources[strUnit]);
            this.tsiUnit.DropDownItems.Add(strMm, this.imgChecked);
            //this.tsiUnit.DropDownItems.Add(strInch);
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(this.tsiLanguage);
            this.cms.Items.Add(this.tsiUnit);
            this.cms.Items.Add(lngResources[strSetupMotion]).Name = lngResources[strSetupMotion];
            if (RoleMgr.Instance.CurrentRole!=null)
            {
                UpdateUI();
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnConfig.Click += NaviBtnConfig_Click;
            this.tsiLanguage.DropDownItemClicked += TsiLanguage_DropDownItemClicked;
            this.tsiUnit.DropDownItemClicked += TsiUnit_DropDownItemClicked;
            this.btnConfig.MouseMove += this.ReadDisplayTip;
            this.btnConfig.MouseLeave += this.DisopTip; ;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(strLanguage, lngResources[strLanguage]);
            this.SaveKeyValueToResources(strEnglish, lngResources[strEnglish]);
            this.SaveKeyValueToResources(strChinese, lngResources[strChinese]);
            this.SaveKeyValueToResources(strUnit, lngResources[strUnit]);
            this.SaveKeyValueToResources(strMm, lngResources[strMm]);
            this.SaveKeyValueToResources(strInch, lngResources[strInch]);
            this.SaveKeyValueToResources(strSetupMotion, lngResources[strSetupMotion]);
        }
        /// <summary>
        /// 更新控件显示文本
        /// </summary>
        /// <param name="skipButton"></param>
        /// <param name="skipRadioButton"></param>
        /// <param name="skipCheckBox"></param>
        /// <param name="skipLabel"></param>
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                lngResources[strLanguage] = this.ReadKeyValueFromResources(strLanguage);
                lngResources[strEnglish] = this.ReadKeyValueFromResources(strEnglish);
                lngResources[strChinese] = this.ReadKeyValueFromResources(strChinese);
                lngResources[strUnit] = this.ReadKeyValueFromResources(strUnit);
                lngResources[strMm] = this.ReadKeyValueFromResources(strMm);
                lngResources[strInch] = this.ReadKeyValueFromResources(strInch);
                lngResources[strSetupMotion] = this.ReadKeyValueFromResources(strSetupMotion);
            }
            this.tsiLanguage.Text = lngResources[strLanguage];
            this.tsiLanguage.DropDownItems[0].Text = lngResources[strEnglish];
            this.tsiLanguage.DropDownItems[1].Text = lngResources[strChinese];
            this.tsiUnit.Text = lngResources[strUnit];
            this.tsiUnit.DropDownItems[0].Text = lngResources[strMm];
            //this.tsiUnit.DropDownItems[1].Text = lngResources[strInch];
            this.cms.Items[strSetupMotion].Text = lngResources[strSetupMotion];
        }
        public void UpdateUI()
        {
            this.tsiLanguage.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSwitchLanguage;
            this.tsiUnit.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSwitchUnit;
            this.cms.Items[strSetupMotion].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupMotion;
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

        private void NaviBtnConfig_Click(object sender, EventArgs e)
        {
            if (MainForm.Ins.Config.Lang == LanguageType.en_US)
            {
                this.tsiLanguage.DropDownItems[0].Image = this.imgChecked;
                this.tsiLanguage.DropDownItems[1].Image = null;
            }
            else
            {
                this.tsiLanguage.DropDownItems[0].Image = null;
                this.tsiLanguage.DropDownItems[1].Image = this.imgChecked;
            }
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == lngResources[strSetupMotion])
            {
                new SettingRobotForm().ShowDialog();
            }
            else if (itemText == "生成语言文件")
            {
                LanguageHelper.Instance.SaveAllFormLanguageResource();
                Account tempAccount = AccountMgr.Instance.FindBy("Supervisor");
                AccountMgr.Instance.SwitchUser(tempAccount);
            }
        }

        private void TsiLanguage_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == this.lngResources[strEnglish])
            {
                MsgCenter.Broadcast(LngMsg.SWITCH_LNG, this, LanguageType.en_US);
                this.tsiLanguage.DropDownItems[0].Image = this.imgChecked;
                this.tsiLanguage.DropDownItems[1].Image = null;
            }
            else
            {
                MsgCenter.Broadcast(LngMsg.SWITCH_LNG, this, LanguageType.zh_CN);
                this.tsiLanguage.DropDownItems[0].Image = null;
                this.tsiLanguage.DropDownItems[1].Image = this.imgChecked;
            }
        }

        private void TsiUnit_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == this.lngResources[strMm])
            {
                this.tsiUnit.DropDownItems[0].Image = this.imgChecked;
                this.tsiUnit.DropDownItems[1].Image = null;
            }
            else
            {
                this.tsiUnit.DropDownItems[0].Image = null;
                this.tsiUnit.DropDownItems[1].Image = this.imgChecked;
            }
        }
    }
}
