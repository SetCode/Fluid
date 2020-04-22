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

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnInit : UserControl, IMsgSender
    {
        private ContextMenuStrip cms;
        private string strMoveHome = "Move Home";
        private string strInitMotion = "Init Motion";
        private string strInitVision = "Init Vision";
        private string strInitSensors = "Init Sensors";

        public NaviBtnInit()
        {
            InitializeComponent();

            this.cms = new ContextMenuStrip();
            this.cms.Items.Add(strMoveHome);
            this.cms.Items.Add(strInitMotion);
            this.cms.Items.Add(strInitVision);
            this.cms.Items.Add(strInitSensors);
            this.btnInit.Click += BtnInit_Click;
            this.btnDetail.Click += BtnDetail_Click;
            this.cms.ItemClicked += Cms_ItemClicked;
        }

        private async void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                if (e.ClickedItem.Text == strMoveHome)
                {
                    Machine.Instance.MoveHome();
                }
                else if (e.ClickedItem.Text == strInitMotion)
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitMotion();
                    AlarmServer.Instance.MachineInitDone = true;
                }
                else if (e.ClickedItem.Text == strInitVision)
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitVision();
                    AlarmServer.Instance.MachineInitDone = true;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                    }));
                }
                else if (e.ClickedItem.Text == strInitSensors)
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitSensors();
                    AlarmServer.Instance.MachineInitDone = true;
                }
            });
            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, 0, 0);
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            Task.Factory.StartNew(() =>
            {
                Machine.Instance.InitAll();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.Broadcast(MsgType.IDLE, this, null);
                    MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                }));
            });
            loadingForm.ShowDialog();
        }
    }
}
