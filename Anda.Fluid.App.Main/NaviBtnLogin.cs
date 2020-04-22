using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.AccessControl;
using  Anda.Fluid.Infrastructure.International;
namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnLogin : UserControl
    {
        public NaviBtnLogin()
        {
            InitializeComponent();
            this.btnLogin.Click += BtnLogin_Click;
            UserControlEx UserControlEx = new UserControlEx();
            this.btnLogin.MouseMove += UserControlEx.ReadDisplayTip;
            this.btnLogin.MouseLeave += UserControlEx.DisopTip;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().ShowDialog();
        }
    }
}
