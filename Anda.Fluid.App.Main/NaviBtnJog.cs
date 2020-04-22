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
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Infrastructure.International;
namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnJog : UserControl
    {
        public NaviBtnJog()
        {
            InitializeComponent();

            this.btnJog.Click += BtnJog_Click;
            UserControlEx UserControlEx = new UserControlEx();
            this.btnJog.MouseMove += UserControlEx.ReadDisplayTip;
            this.btnJog.MouseLeave += UserControlEx.DisopTip;
        }

        private void BtnJog_Click(object sender, EventArgs e)
        {
            FormMgr.Show<JogForm>(this);
            FormMgr.GetForm<JogForm>().UpdateUI();
        }
    }
}
