using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnInitItems : UserControlEx, IMsgSender
    {
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strMoveHome = "Move Home";
        private string strInitMotion = "Init Motion";
        private string strInitVision = "Init Vision";
        private string strInitSensors = "Init Sensors";

        public NaviBtnInitItems()
        {
            InitializeComponent();
            lngResources.Add(strMoveHome, "Move Home");
            lngResources.Add(strInitMotion, "Init Motion");
            lngResources.Add(strInitVision, "Init Vision");
            lngResources.Add(strInitSensors, "Init Sensors");
            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(strMoveHome);
            this.cms.Items.Add(strInitMotion);
            this.cms.Items.Add(strInitVision);
            this.cms.Items.Add(strInitSensors);
            this.btnDetail.Click += BtnDetail_Click;
            this.cms.ItemClicked += Cms_ItemClicked;
            this.ReadLanguageResources();
            this.btnDetail.MouseMove += this.ReadDisplayTip;
            this.btnDetail.MouseLeave += this.DisopTip;
        }

        private async void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                if (e.ClickedItem.Text == lngResources[strMoveHome])
                {
                    Machine.Instance.MoveHome();
                }
                else if (e.ClickedItem.Text == lngResources[strInitMotion])
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitMotion();
                    AlarmServer.Instance.MachineInitDone = true;
                }
                else if (e.ClickedItem.Text == lngResources[strInitVision])
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitVision();
                    AlarmServer.Instance.MachineInitDone = true;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                    }));
                }
                else if (e.ClickedItem.Text == lngResources[strInitSensors])
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitSensors();
                    AlarmServer.Instance.MachineInitDone = true;
                }
            });
            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                lngResources[strMoveHome] = this.ReadKeyValueFromResources(strMoveHome);
                lngResources[strInitMotion] = this.ReadKeyValueFromResources(strInitMotion);
                lngResources[strInitVision] = this.ReadKeyValueFromResources(strInitVision);
                lngResources[strInitSensors] = this.ReadKeyValueFromResources(strInitSensors);
            }
            this.cms.Items[0].Text = lngResources[strMoveHome];
            this.cms.Items[1].Text = lngResources[strInitMotion];
            this.cms.Items[2].Text = lngResources[strInitVision];
            this.cms.Items[3].Text = lngResources[strInitSensors];
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, 0, 0);
        }
    }
}
