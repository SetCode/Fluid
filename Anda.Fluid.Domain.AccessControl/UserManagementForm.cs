using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.AccessControl
{
    /// <summary>
    /// 用户权限配置界面
    /// </summary>
    public partial class UserManagementForm : FormEx
    {
        public UserManagementForm()
        {
            InitializeComponent();
            UpdateData();
            dgvUser.AllowUserToAddRows = false;
            txtNameColumn.ReadOnly = true;
            txtRoleTypeColumn.ReadOnly = true;
            this.ReadLanguageResources();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            new NewUserForm().ShowDialog();
            UpdateData();
            this.Invalidate();
        }

        public void UpdateData()
        {
            dgvUser.Rows.Clear();
            int i = 0;
            foreach (Account item in AccountMgr.Instance.getAccountList())
            {
                dgvUser.Rows.Add();
                dgvUser.Rows[i].HeaderCell.Value = string.Format("{0}", i);
                dgvUser.Rows[i].Cells[1].Value = item.NameId;
                dgvUser.Rows[i].Cells[2].Value = item.RoleType.ToString();

              

                if (item.RoleType.ToString().Equals(RoleType.Supervisor.ToString()))
                {
                    dgvUser.Rows[i].Cells[2].Value = "管理员";
                }
                if (item.RoleType.ToString().Equals(RoleType.Operator.ToString()))
                {
             
                    dgvUser.Rows[i].Cells[2].Value = "操作员";
                }
                if (item.RoleType.ToString().Equals(RoleType.Technician.ToString()))
                {
        
                    dgvUser.Rows[i].Cells[2].Value = "工程师";
                }




                i++;
            }
        }
        /// <summary>
        /// 特殊控件重载语言保存
        /// </summary>
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            foreach (DataGridViewColumn column in dgvUser.Columns)
            {
                this.SaveKeyValueToResources(column.Name, column.HeaderText);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }
        /// <summary>
        /// 特殊控件重载语言加载
        /// </summary>
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            foreach (DataGridViewColumn column in dgvUser.Columns)
            {
                string text = this.ReadKeyValueFromResources(column.Name);
                if (!text.Equals(""))
                {
                    column.HeaderText = text;
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            List<string> deleteList = new List<string>();
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                if ((bool)dgvUser.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    deleteList.Add((string)dgvUser.Rows[i].Cells[1].Value);
                }
            }
            //删除当前用户提示防呆
            //string message1 = "Confirm deletion of selected items?";
            string message1 = "确定删除选中用户?";
            //string message2 = "Delete";
            string message2 = "删除";
            if (deleteList.Contains(AccountMgr.Instance.CurrentAccount.NameId))
            {
                //message1 = "Current User is selected, confirm deletion of selection item?";
                message1 = "当前用户被选中，确定删除吗?";
            }
            DialogResult result = MessageBox.Show(message1, message2, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            foreach (string item in deleteList)
            {
                AccountMgr.Instance.Remove(item);
                string msg = string.Format("删除用户 {0} 成功！", item);
                Logger.DEFAULT.Info(LogCategory.SETTING, msg);
            }
            UpdateData();
            this.Invalidate();
        }
    }
}
