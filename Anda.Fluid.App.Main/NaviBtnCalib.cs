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
using Anda.Fluid.Domain.FluProgram;


namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnCalib : UserControlEx, IMsgSender
    {
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strCameraCalib = "Camera Calibration";
        private string strScriptedCalib = "Scripted Valve Offsets";
        private string strFourDirectionValveSetup = "四方位阀组校准";
        private string strRTVPurgeSetup = "RTV清洗动作设置";
        private string strNeedleCalibration = "Calibrate Needle";
        private string strStripMapping = "Strip Mapping";       
        public NaviBtnCalib()
        {
            InitializeComponent();
            //先生成对应数据结构
            lngResources.Add(strCameraCalib, "Camera Calibration");
            lngResources.Add(strScriptedCalib, "Scripted Valve Offsets");
            lngResources.Add(strFourDirectionValveSetup, "四方位阀组校准");
            lngResources.Add(strRTVPurgeSetup, "RTV清洗动作设置");
            lngResources.Add(strNeedleCalibration, "Calibrate Needle");
            lngResources.Add(strStripMapping, "Strip Mapping");
           
            //再读取文本数据到数据结构
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(lngResources[strCameraCalib]).Name = strCameraCalib;
            this.cms.Items.Add(lngResources[strScriptedCalib]).Name = strScriptedCalib;
            this.cms.Items.Add(strFourDirectionValveSetup).Name = strFourDirectionValveSetup;
            this.cms.Items.Add(strRTVPurgeSetup).Name = strRTVPurgeSetup;
            this.cms.Items.Add(lngResources[strNeedleCalibration]).Name = strNeedleCalibration;
            this.cms.Items.Add(lngResources[strStripMapping]).Name = strStripMapping;
           
            if (RoleMgr.Instance.CurrentRole!=null)
            {
                UpdateUI();
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnCalib.Click += NaviBtnCalib_Click;
            this.btnCalib.MouseMove += this.ReadDisplayTip;
            this.btnCalib.MouseLeave += this.DisopTip;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(strCameraCalib, lngResources[strCameraCalib]);
            this.SaveKeyValueToResources(strScriptedCalib, lngResources[strScriptedCalib]);
            this.SaveKeyValueToResources(strFourDirectionValveSetup, lngResources[strFourDirectionValveSetup]);
            this.SaveKeyValueToResources(strRTVPurgeSetup, lngResources[strRTVPurgeSetup]);
            this.SaveKeyValueToResources(strNeedleCalibration, lngResources[strNeedleCalibration]);
            this.SaveKeyValueToResources(strStripMapping, lngResources[strStripMapping]);
            
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
                lngResources[strCameraCalib] = this.ReadKeyValueFromResources(strCameraCalib);
                lngResources[strScriptedCalib] = this.ReadKeyValueFromResources(strScriptedCalib);
                lngResources[strFourDirectionValveSetup] = this.ReadKeyValueFromResources(strFourDirectionValveSetup);
                lngResources[strRTVPurgeSetup] = this.ReadKeyValueFromResources(strRTVPurgeSetup);
                lngResources[strNeedleCalibration] = this.ReadKeyValueFromResources(strNeedleCalibration);
                lngResources[strStripMapping] = this.ReadKeyValueFromResources(strStripMapping);
                
            }
            this.cms.Items[strCameraCalib].Text = lngResources[strCameraCalib];
            this.cms.Items[strScriptedCalib].Text = lngResources[strScriptedCalib];
            this.cms.Items[strFourDirectionValveSetup].Text = lngResources[strFourDirectionValveSetup];
            this.cms.Items[strRTVPurgeSetup].Text = lngResources[strRTVPurgeSetup];
            this.cms.Items[strNeedleCalibration].Text = lngResources[strNeedleCalibration];
            this.cms.Items[strStripMapping].Text = lngResources[strStripMapping];
            
        }
        public void UpdateUI()
        {
            this.cms.Items[strCameraCalib].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupAxes;
            this.cms.Items[strScriptedCalib].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanSetupIO;
            this.cms.Items[strFourDirectionValveSetup].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanScriptedCalib;
            this.cms.Items[strRTVPurgeSetup].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanScriptedCalib;
            this.cms.Items[strNeedleCalibration].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanScriptedCalib;
            this.cms.Items[strStripMapping].Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanStripMapping;
            
            this.ReadLanguageResources();
        }

        private void NaviBtnCalib_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, new Point(0, 0));
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == lngResources[strCameraCalib])
            {
                new DialogCalibCamera().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strScriptedCalib])
            {
                new SVOForm().ShowDialog();
            }
            else if (itemText == strFourDirectionValveSetup)
            {
                new FourDireciotnValveCorrectForm().ShowDialog();
            }
            else if (itemText == strRTVPurgeSetup)
            {
                if (Machine.Instance.Setting.MachineSelect != MachineSelection.RTV)
                    return;
                RTVPurgeForm rtvForm = new RTVPurgeForm();
                rtvForm.Setup();
                rtvForm.ShowDialog();
            }
            else if (itemText == lngResources[strNeedleCalibration])
            {
                //new DialogNeedleAngle().ShowDialog();
                new DialogNeedleAngleWithPlasticene().Setup().ShowDialog();
            }
            else if (itemText == lngResources[strStripMapping])
            {
                new DialogCalibMap().ShowDialog();
            }
          
        }
    }
}
