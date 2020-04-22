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
    public partial class NewUserForm : FormEx
    {
        private RoleType newAccountType = RoleType.Operator;
        public NewUserForm()
        {
            InitializeComponent();
            RoleType type = AccountMgr.Instance.CurrentAccount.RoleType;
            if (type == RoleType.Technician || type == RoleType.Operator)
            {
                rdoSupervisor.Enabled = false;
                rdoTechnician.Enabled = false;
            }
            rdoOperator.Checked = true;
            rdoOperator.CheckedChanged += RdoOperator_CheckedChanged;
            rdoTechnician.CheckedChanged += RdoTechnician_CheckedChanged;
            rdoSupervisor.CheckedChanged += RdoSupervisor_CheckedChanged;
            btnCancel.Click += BtnCancel_Click;
            this.ReadLanguageResources();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RdoSupervisor_CheckedChanged(object sender, EventArgs e)
        {
            newAccountType = RoleType.Supervisor;
        }

        private void RdoTechnician_CheckedChanged(object sender, EventArgs e)
        {
            newAccountType = RoleType.Technician;
        }

        private void RdoOperator_CheckedChanged(object sender, EventArgs e)
        {
            newAccountType = RoleType.Operator;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string errMessage;
            if (txtPassword.TextLength <= 0 || txtUserName.TextLength <= 0 || txtRePassword.TextLength <= 0)
            {
                //errMessage = "User name or password cannot be empty!";
                errMessage = "用户和密码不可以为空!";
                MessageBox.Show(errMessage);
                return;
            }
            if (AccountMgr.Instance.ContainsAccount(txtUserName.Text))
            {
                //errMessage = "The user name already exists!";
                errMessage = "用户已经存在!";
                MessageBox.Show(errMessage);
                return;
            }
            if (!txtPassword.Text.Equals(txtRePassword.Text))
            {
                //errMessage = "The first password is different from second password!";
                errMessage = "第一个密码和第二个密码不同!";
                MessageBox.Show(errMessage);
                return;
            }
            AccountMgr.Instance.Add(new Account(txtUserName.Text, txtPassword.Text, newAccountType));
            string msg = string.Format("Add  {0} account {1} successfully, ", newAccountType, txtUserName.Text);
            Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
            this.Close();
        }
    }
}
