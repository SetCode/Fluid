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
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.AccessControl
{
    /// <summary>
    /// 密码修改界面
    /// </summary>
    public partial class SetupPasswordForm : FormEx
    {
        public SetupPasswordForm()
        {
            InitializeComponent();
            lblUserName.Text = AccountMgr.Instance.CurrentAccount.NameId;
            this.ReadLanguageResources();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 密码检查

            string errMessage;
            if (this.txtReEnterPassword.Text.Length < 1 || this.txtOldPassword.Text.Length < 1 || this.txtNewPassword.Text.Length < 1)
            {
                //errMessage = "Password cannot be empty!";
                errMessage = "密码不可以为空!";
                MessageBox.Show(errMessage);
                return;
            }
            if (!this.txtNewPassword.Text.Equals(this.txtReEnterPassword.Text))
            {
                //errMessage = "The first password is different from second password!";
                errMessage = "第一个密码和第二个密码不同!";
                MessageBox.Show(errMessage);
                return;
            }
            if (this.txtNewPassword.Text.Equals(this.txtOldPassword.Text))
            {
                //errMessage = "The new password cannot be same as the old password!";
                errMessage = "新密码和旧密码不可以相同!";
                MessageBox.Show(errMessage);
                return;
            }
            if (this.txtOldPassword.Text != AccountMgr.Instance.CurrentAccount.Password)
            {
                //errMessage = "The old password is wrong!";
                errMessage = "旧密码错误!";
                MessageBox.Show(errMessage);
                return;
            }

            #endregion

            AccountMgr.Instance.CurrentAccount.ChangePassword(this.txtNewPassword.Text);
            string msg = string.Format("{0} set up pass successfully", lblUserName.Text);
            Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
