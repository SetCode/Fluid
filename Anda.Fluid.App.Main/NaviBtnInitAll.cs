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
    public partial class NaviBtnInitAll : UserControl, IMsgSender
    {
        public NaviBtnInitAll()
        {
            InitializeComponent();
            this.btnInit.Click += BtnInit_Click;
            UserControlEx UserControlEx = new UserControlEx();
            this.btnInit.MouseMove += UserControlEx.ReadDisplayTip;
            this.btnInit.MouseLeave += UserControlEx.DisopTip;
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
