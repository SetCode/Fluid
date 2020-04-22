using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.AccessControl
{
    /// <summary>
    /// 权限管理界面
    /// </summary>
    public partial class FeatureAccessForm : FormEx, IMsgSender
    {
        private CheckBox cbxOperatorSelectAll = new CheckBox();
        private CheckBox cbxTechnicianSelectAll = new CheckBox();
        private CheckBox cbxSuperVisorSelectAll = new CheckBox();
        public FeatureAccessForm()
        {
            InitializeComponent();
            cbxOperatorSelectAll.Text = "";
            cbxTechnicianSelectAll.Text = "";
            cbxSuperVisorSelectAll.Text = "";
            this.Controls.Add(cbxOperatorSelectAll);
            this.Controls.Add(cbxTechnicianSelectAll);
            this.Controls.Add(cbxSuperVisorSelectAll);
            cbxOperatorSelectAll.Click += CbxOperatorSelectAll_Click;
            cbxTechnicianSelectAll.Click += CbxTechnicianSelectAll_Click;
            cbxSuperVisorSelectAll.Click += CbxSuperVisorSelectAll_Click;
            dgvAccess.CellPainting += DgvAccess_CellPainting;
            dgvAccess.CellContentClick += DgvAccess_CellContentClick;
            dgvAccess.CurrentCellDirtyStateChanged += DgvAccess_CurrentCellDirtyStateChanged;
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            int otherFormAccessCount = RoleMgr.Instance.Developer.OtherFormAccess.GetLength();
            int mainFormAccessCount = RoleMgr.Instance.Developer.MainFormAccess.GetLength();
            int programFormAccessCount = RoleMgr.Instance.Developer.ProgramFormAccess.GetLength();
            int i = 0;
            this.dgvAccess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgvAccess.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dgvAccess.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            foreach (DataGridViewRow row in dgvAccess.Rows)
            {
                string text = "";
                if (i < mainFormAccessCount)
                {
                    text = this.ReadKeyValueFromResources(RoleMgr.Instance.Developer.MainFormAccess.GetDescription(i));
                }
                else if (i < mainFormAccessCount + programFormAccessCount)
                {
                    int j = i - mainFormAccessCount;
                    text = this.ReadKeyValueFromResources(RoleMgr.Instance.Developer.ProgramFormAccess.GetDescription(j));
                }
                else if (i < mainFormAccessCount + programFormAccessCount + otherFormAccessCount)
                {
                    int j = i - mainFormAccessCount - programFormAccessCount;
                    text = this.ReadKeyValueFromResources(RoleMgr.Instance.Developer.OtherFormAccess.GetDescription(j));
                }
                if (!text.Equals(""))
                {
                    row.HeaderCell.Value = text;
                }
                i++;
            }
            foreach (DataGridViewColumn column in dgvAccess.Columns)
            {
                string text = this.ReadKeyValueFromResources(column.Name);
                if (!text.Equals(""))
                {
                    column.HeaderText = text;
                }
            }
            this.dgvAccess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccess.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAccess.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            foreach (DataGridViewColumn column in dgvAccess.Columns)
            {
                this.SaveKeyValueToResources(column.Name, column.HeaderText);
            }
            int otherFormAccessCount = RoleMgr.Instance.Developer.OtherFormAccess.GetLength();
            int mainFormAccessCount = RoleMgr.Instance.Developer.MainFormAccess.GetLength();
            int programFormAccessCount = RoleMgr.Instance.Developer.ProgramFormAccess.GetLength();
            for (int i = 0; i < mainFormAccessCount + programFormAccessCount + otherFormAccessCount; i++)
            {
                string text = "";
                if (i < mainFormAccessCount)
                {
                    int j = i;
                    text = RoleMgr.Instance.Developer.MainFormAccess.GetDescription(j);
                }
                else if (i < mainFormAccessCount + programFormAccessCount)
                {
                    int j = i - mainFormAccessCount;
                    text = RoleMgr.Instance.Developer.ProgramFormAccess.GetDescription(j);
                }
                else if (i < mainFormAccessCount + programFormAccessCount + otherFormAccessCount)
                {
                    int j = i - mainFormAccessCount - programFormAccessCount;
                    text = RoleMgr.Instance.Developer.OtherFormAccess.GetDescription(j);
                }
                if (!text.Equals(""))
                {
                    this.SaveKeyValueToResources(text, text);
                }
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int otherFormAccessCount = RoleMgr.Instance.Developer.OtherFormAccess.GetLength();
            int mainFormAccessCount = RoleMgr.Instance.Developer.MainFormAccess.GetLength();
            int programFormAccessCount = RoleMgr.Instance.Developer.ProgramFormAccess.GetLength();
            int i = 0;
            for (; i < mainFormAccessCount; i++)
            {
                RoleMgr.Instance.Operator.MainFormAccess[i] = (bool)dgvAccess.Rows[i].Cells[0].Value;
                RoleMgr.Instance.Technician.MainFormAccess[i] = (bool)dgvAccess.Rows[i].Cells[1].Value;
                RoleMgr.Instance.Supervisor.MainFormAccess[i] = (bool)dgvAccess.Rows[i].Cells[2].Value;
            }
            for (; i < mainFormAccessCount + programFormAccessCount; i++)
            {
                int j = i - mainFormAccessCount;
                RoleMgr.Instance.Operator.ProgramFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[0].Value;
                RoleMgr.Instance.Technician.ProgramFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[1].Value;
                RoleMgr.Instance.Supervisor.ProgramFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[2].Value;
            }
            for (; i < mainFormAccessCount + programFormAccessCount + otherFormAccessCount; i++)
            {
                int j = i - mainFormAccessCount - programFormAccessCount;
                RoleMgr.Instance.Operator.OtherFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[0].Value;
                RoleMgr.Instance.Technician.OtherFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[1].Value;
                RoleMgr.Instance.Supervisor.OtherFormAccess[j] = (bool)dgvAccess.Rows[i].Cells[2].Value;
            }
            MsgCenter.Broadcast(MsgConstants.MODIFY_ACCESS, this, null);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FeatureAccessForm_Load(object sender, EventArgs e)
        {
            int mainFormAccessCount = RoleMgr.Instance.Developer.MainFormAccess.GetLength();
            int programFormAccessCount = RoleMgr.Instance.Developer.ProgramFormAccess.GetLength();
            int otherFormAccessCount = RoleMgr.Instance.Developer.OtherFormAccess.GetLength();
            int i = 0;
            for (; i < mainFormAccessCount; i++)
            {
                dgvAccess.Rows.Add();
                dgvAccess.Rows[i].HeaderCell.Value = RoleMgr.Instance.Developer.MainFormAccess.GetDescription(i);
                dgvAccess.Rows[i].Cells[0].Value = RoleMgr.Instance.Operator.MainFormAccess[i];
                dgvAccess.Rows[i].Cells[1].Value = RoleMgr.Instance.Technician.MainFormAccess[i];
                dgvAccess.Rows[i].Cells[2].Value = RoleMgr.Instance.Supervisor.MainFormAccess[i];
            }
            for (; i < mainFormAccessCount + programFormAccessCount; i++)
            {
                int j = i - mainFormAccessCount;
                dgvAccess.Rows.Add();
                dgvAccess.Rows[i].HeaderCell.Value = RoleMgr.Instance.Developer.ProgramFormAccess.GetDescription(j);
                dgvAccess.Rows[i].Cells[0].Value = RoleMgr.Instance.Operator.ProgramFormAccess[j];
                dgvAccess.Rows[i].Cells[1].Value = RoleMgr.Instance.Technician.ProgramFormAccess[j];
                dgvAccess.Rows[i].Cells[2].Value = RoleMgr.Instance.Supervisor.ProgramFormAccess[j];
            }
            for (; i < mainFormAccessCount + programFormAccessCount + otherFormAccessCount; i++)
            {
                int j = i - mainFormAccessCount - programFormAccessCount;
                dgvAccess.Rows.Add();
                dgvAccess.Rows[i].HeaderCell.Value = RoleMgr.Instance.Developer.OtherFormAccess.GetDescription(j);
                dgvAccess.Rows[i].Cells[0].Value = RoleMgr.Instance.Operator.OtherFormAccess[j];
                dgvAccess.Rows[i].Cells[1].Value = RoleMgr.Instance.Technician.OtherFormAccess[j];
                dgvAccess.Rows[i].Cells[2].Value = RoleMgr.Instance.Supervisor.OtherFormAccess[j];
            }
            cbxOperatorSelectAll.Checked = CheckAllCbxState(0);
            cbxTechnicianSelectAll.Checked = CheckAllCbxState(1);
            cbxSuperVisorSelectAll.Checked = CheckAllCbxState(2);
            this.ReadLanguageResources();
        }

        private void DgvAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                bool isSelectAll = CheckAllCbxState(e.ColumnIndex);
                if (e.ColumnIndex == 0)
                {
                    cbxOperatorSelectAll.Checked = isSelectAll;
                }
                else if (e.ColumnIndex == 1)
                {
                    cbxTechnicianSelectAll.Checked = isSelectAll;
                }
                else if (e.ColumnIndex == 2)
                {
                    cbxSuperVisorSelectAll.Checked = isSelectAll;
                }
            }
        }

        private void CbxSuperVisorSelectAll_Click(object sender, EventArgs e)
        {
            bool selectState = cbxSuperVisorSelectAll.Checked;
            SelectColumnAll(2, selectState);
        }

        private void CbxTechnicianSelectAll_Click(object sender, EventArgs e)
        {
            bool selectState = cbxTechnicianSelectAll.Checked;
            SelectColumnAll(1, selectState);
        }

        private void CbxOperatorSelectAll_Click(object sender, EventArgs e)
        {
            bool selectState = cbxOperatorSelectAll.Checked;
            SelectColumnAll(0, selectState);
        }

        private void DgvAccess_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAccess.IsCurrentCellDirty)
            {
                dgvAccess.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvAccess_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    Point p = this.dgvAccess.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    p.Offset(this.dgvAccess.Left, this.dgvAccess.Top);
                    this.cbxOperatorSelectAll.Location = p;
                    this.cbxOperatorSelectAll.Size = new Size(15, 15);
                    this.cbxOperatorSelectAll.Visible = true;
                    this.cbxOperatorSelectAll.BringToFront();
                }
                else if (e.ColumnIndex == 1)
                {
                    Point p = this.dgvAccess.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    p.Offset(this.dgvAccess.Left, this.dgvAccess.Top);
                    this.cbxTechnicianSelectAll.Location = p;
                    this.cbxTechnicianSelectAll.Size = new Size(15, 15);
                    this.cbxTechnicianSelectAll.Visible = true;
                    this.cbxTechnicianSelectAll.BringToFront();
                }
                else if (e.ColumnIndex == 2)
                {
                    Point p = this.dgvAccess.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    p.Offset(this.dgvAccess.Left, this.dgvAccess.Top);
                    this.cbxSuperVisorSelectAll.Location = p;
                    this.cbxSuperVisorSelectAll.Size = new Size(15, 15);
                    this.cbxSuperVisorSelectAll.Visible = true;
                    this.cbxSuperVisorSelectAll.BringToFront();
                }
            }
        }

        private bool CheckAllCbxState(int index)
        {
            dgvAccess.EndEdit();
            bool result = true;
            for (int i = 0; i < dgvAccess.Rows.Count; i++)
            {
                result &= (bool)dgvAccess.Rows[i].Cells[index].Value;
            }
            return result;
        }
        private void SelectColumnAll(int index, bool value)
        {
            dgvAccess.EndEdit();
            for (int i = 0; i < dgvAccess.Rows.Count; i++)
            {
                dgvAccess.Rows[i].Cells[index].Value = value;
            }
        }
    }
}
