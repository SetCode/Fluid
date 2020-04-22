using Anda.Fluid.Domain.AccessControl.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.AccessControl
{
    /// <summary>
    /// 用户登录窗口
    /// </summary>
    public partial class LoginForm : FormEx
    {
        public LoginForm()
        {
            InitializeComponent();
            //控件权限管控
            btnUserMgr.Enabled = RoleMgr.Instance.CurrentRole.OtherFormAccess.CanUserMgrForm;
            this.ReadLanguageResources();
        }

        private void btnSetupPassword_Click(object sender, EventArgs e)
        {
            new SetupPasswordForm().ShowDialog();
        }

        private void btnUserMgr_Click(object sender, EventArgs e)
        {
            new UserManagementForm().ShowDialog();
        }
        // 登录时的简单验证
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string message;
            if (cboxNameId.Text.Length <= 0 || txtPassword.Text.Length <= 0)
            {
                //message = "Name or Password cannot be empty!";
                message = "名字和密码不可以为空!";
                MessageBox.Show(message);
                return;
            }
            if (!AccountMgr.Instance.ContainsAccount(cboxNameId.Text))
            {
                //message = "The User :" + txtNameId.Text + " do not exit";
                message = "用户 :" + cboxNameId.Text + " 不存在";
                MessageBox.Show(message);
                return;
            }
            Account tempAccount = AccountMgr.Instance.FindBy(cboxNameId.Text);
            if (tempAccount.Password != txtPassword.Text)
            {
                //message = "Wrong password!";
                message = "密码错误!";
                MessageBox.Show(message);
                return;
            }
            AccountMgr.Instance.SwitchUser(tempAccount);
            this.Close();
            string msg = string.Format("account {0} login succussfully", tempAccount.NameId);
            Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            new NewUserForm().ShowDialog();
        }

        private void btnSwitchOperator_Click(object sender, EventArgs e)
        {
            AccountMgr.Instance.SwitchUser(AccountMgr.Instance.FindBy(RoleType.Operator));
        }
    }
}
