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
    public partial class NaviBtnTest : UserControlEx, IMsgSender
    {
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strCalCpk = "Calculate CPK";
        private string strPatternWeight = "PatternWeight";
       
        public NaviBtnTest()
        {
            InitializeComponent();
            //先生成对应数据结构
            lngResources.Add(strCalCpk, "Calculate CPK");
            lngResources.Add(strPatternWeight, "PatternWeight");
   
            //再读取文本数据到数据结构
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(lngResources[strCalCpk]).Name = strCalCpk;
            this.cms.Items.Add(lngResources[strPatternWeight]).Name = strPatternWeight;
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnCalib.Click += NaviBtnCalib_Click;
            if (RoleMgr.Instance.CurrentRole!=null)
            {
                UpdateUI();
            }            
            this.btnCalib.MouseMove += this.ReadDisplayTip;
            this.btnCalib.MouseLeave += this.DisopTip;       
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(strCalCpk, lngResources[strCalCpk]);
            this.SaveKeyValueToResources(strPatternWeight, lngResources[strPatternWeight]);
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
                lngResources[strCalCpk] = this.ReadKeyValueFromResources(strCalCpk);
                lngResources[strPatternWeight] = this.ReadKeyValueFromResources(strPatternWeight);
            }
            this.cms.Items[strCalCpk].Text = lngResources[strCalCpk];
            this.cms.Items[strPatternWeight].Text = lngResources[strPatternWeight];
        }
        public void UpdateUI()
        {
            this.cms.Items[strCalCpk].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanCalCpk;
            this.cms.Items[strPatternWeight].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupSensors;
            this.ReadLanguageResources();
        }

        private void NaviBtnCalib_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == lngResources[strCalCpk])
            {
                new DialogCPK().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strPatternWeight])
            {
                new DialogPatternWeight().ShowDialog();
            }
        }
    }
}
