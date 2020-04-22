using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnVision : UserControl
    {
        public NaviBtnVision()
        {
            InitializeComponent();

            this.btnCamera.Click += BtnCamera_Click;
            UserControlEx UserControl = new UserControlEx();
            this.btnCamera.MouseMove += UserControl.ReadDisplayTip;
            this.btnCamera.MouseLeave += UserControl.DisopTip;
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            FormMgr.Show<CameraForm>(this);
        }
    }
}
