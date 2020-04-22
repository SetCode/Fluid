using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.App.Metro.Forms;
using Anda.Fluid.Infrastructure.International.Access;
using Anda.Fluid.Infrastructure.International;
using static Anda.Fluid.Infrastructure.International.AccessEnums;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Domain.AccessControl.User;
using System.Diagnostics;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageSetupAccess : MetroSetUserControl, IAccessControllable,IMsgSender
    {
        //权限执行
        private AccessExecutor accessExecutor;
        public PageSetupAccess()
        {
            InitializeComponent();
            this.initialDgvComponent();
            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }

        private void btnSetDefaultAccess_Click(object sender, EventArgs e)
        {
            AccessControlMgr.Instance.Clear();
            string[] strArray = System.Enum.GetNames(typeof(ContainerKeys));
            List<IAccessControllable> containerList = AccessControlMgr.Instance.accessControls.OrderBy(item => item.Key).ToList();

            foreach (var item in containerList)
            {                
                item.SetDefaultAccess();
            }
            AccessControlMgr.Instance.Save();
            //加载权限
            this.loadAccess();
        }
        
        private void PageSetupAccess_Load(object sender, EventArgs e)
        {
            this.loadAccess();
        }
        #region DataGridView
        private void initialDgvComponent()
        {
            this.dgvAccess.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAccess.ColumnHeadersHeight = 50;
            this.dgvAccess.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvAccess.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAccess.AllowUserToDeleteRows = false;
            this.dgvAccess.AllowUserToOrderColumns = false;

            this.dgvAccess.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvAccess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccess.RowsDefaultCellStyle.BackColor = Color.White;
            this.dgvAccess.RowsDefaultCellStyle.ForeColor = Color.Black;

            this.dgvAccess.Columns.Clear();

            DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
            txtCol.Name = "Access";
            txtCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvAccess.Columns.Add(txtCol);

            DataGridViewCheckBoxColumn ckbCol = new DataGridViewCheckBoxColumn();
            ckbCol.Name = "Operator";
            ckbCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvAccess.Columns.Add(ckbCol);


            ckbCol = new DataGridViewCheckBoxColumn();
            ckbCol.Name = "Technician";
            ckbCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvAccess.Columns.Add(ckbCol);

            ckbCol = new DataGridViewCheckBoxColumn();
            ckbCol.Name = "Supervisor";
            ckbCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvAccess.Columns.Add(ckbCol);
            

        }
        private void loadAccess()
        {
            this.dgvAccess.Rows.Clear();
            foreach (ContainerAccess item in AccessControlMgr.Instance.ContainerAccessList)
            {
                this.AddContainerAccess(item);
            }
        }
        private void AddContainerAccess(ContainerAccess containerAccess)
        {
            DataGridViewRow dr = new DataGridViewRow();
            dr.HeaderCell.Value = containerAccess.ContainerAccessDescription;
            dr.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ControlAccess container = containerAccess.GetContainerAccess();
            DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
            dr.Cells.Add(txtCell);
            if (container==null)
            {
                MessageBox.Show("缺少窗体控件权限");
                return;
            }
            dr.Tag = container;
            for (int i = 0; i < 3; i++)
            {
                DataGridViewCheckBoxCell ckbCell = new DataGridViewCheckBoxCell();                
                dr.Cells.Add(ckbCell);               
            }
            
            if (container == null)
            {
                dr.Cells[1].Value = true;
                dr.Cells[2].Value = true;
                dr.Cells[3].Value = true;
            }
            else
            {
                dr.Cells[1].Value = (RoleEnums.Operator - container.RoleLevel) >= 0 ? true : false;
                dr.Cells[2].Value = (RoleEnums.Technician - container.RoleLevel) >= 0 ? true : false;
                dr.Cells[3].Value = (RoleEnums.Supervisor - container.RoleLevel) >= 0 ? true : false;
            }
                   
            this.dgvAccess.Rows.Add(dr);

            foreach (ControlAccess item in containerAccess.ControlAccessList)
            {
                if (item.AccessDesrciption==containerAccess.ContainerAccessDescription)
                {
                    continue;
                }
                dr = new DataGridViewRow();
                dr.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dr.Tag = item;
                DataGridViewTextBoxCell txtCellChild = new DataGridViewTextBoxCell();
                txtCellChild.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dr.Cells.Add(txtCellChild);
                for (int i = 0; i < 3; i++)
                {
                    DataGridViewCheckBoxCell ckbCell = new DataGridViewCheckBoxCell();
                    dr.Cells.Add(ckbCell);
                }
                dr.Cells[0].Value = item.AccessDesrciption;
                dr.Cells[1].Value = (RoleEnums.Operator - item.RoleLevel) >= 0 ? true : false;
                dr.Cells[2].Value = (RoleEnums.Technician - item.RoleLevel) >= 0 ? true : false;
                dr.Cells[3].Value = (RoleEnums.Supervisor - item.RoleLevel) >= 0 ? true : false;
                this.dgvAccess.Rows.Add(dr);
            }

        }

        #endregion 

        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageSetupAccess;
        public Control Control => this;
        public ContainerAccess CurrContainerAccess { get; set; } = new ContainerAccess();
        public ContainerAccess DefaultContainerAccess { get; set; } = new ContainerAccess();
        public List<AccessObj> UserAccessControls { get; set; } = new List<AccessObj>();

        public void SetupUserAccessControl()
        {

        }
        public void SetDefaultAccess()
        {
           
                this.DefaultContainerAccess = new ContainerAccess();
           
            //上面
            string containerName = this.GetType().Name;
            this.DefaultContainerAccess.ContainerName = containerName;
            this.DefaultContainerAccess.ContainerAccessDescription = "权限设置";
            this.DefaultContainerAccess.ControlAccessList.Clear();
            this.DefaultContainerAccess.AddContainerTechnician();

            AccessControlMgr.Instance.AddContainerAccess(this.DefaultContainerAccess);
        }

        public void LoadAccess()
        {
            this.accessExecutor.LoadAccess();           
        }



        public void UpdateUIByAccess()
        {
            this.accessExecutor.UpdateUIByAccess();
            if (AccessControlMgr.Instance.CurrRole == AccessEnums.RoleEnums.Developer)
            {
                this.btnSetDefaultAccess.Visible = true;
            }
            else
            {
                this.btnSetDefaultAccess.Visible = false;
            }
        }




        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
           
            foreach (DataGridViewRow item in this.dgvAccess.Rows)
            {
                if (item.Tag is ControlAccess)
                {
                    ControlAccess access = item.Tag as ControlAccess;
                    RoleEnums roleLevel = RoleEnums.Operator;
                    if ((bool)item.Cells[1].Value)
                    {
                        roleLevel = RoleEnums.Operator;
                    }
                    else if ((bool)item.Cells[2].Value)
                    {
                        roleLevel = RoleEnums.Technician;
                    }
                    else if ((bool)item.Cells[3].Value)
                    {
                        roleLevel = RoleEnums.Supervisor;
                    }
                    access.RoleLevel = roleLevel;
                }
            }
            MsgCenter.Broadcast(MsgConstants.MODIFY_ACCESS, this, null);
            AccessControlMgr.Instance.Save();
        }
               

        private void updateRow(int row, int col, bool value)
        {           
            if (value)
            {                
                for (int i = col; i < 4; i++)
                {
                    this.dgvAccess.Rows[row].Cells
                                            [i].Value = true;
                }
            }
            else
            {
                for (int i = 1; i <=col; i++)
                {
                    this.dgvAccess.Rows[row].Cells
                                            [i].Value = false;
                }
            }
        }             

        private void dgvAccess_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {           
            if (e.RowIndex > -1 && e.ColumnIndex > 0)
            {
                ControlAccess access = this.dgvAccess.Rows[e.RowIndex].Tag as ControlAccess;
                //bool value = (bool)this.dgvAccess.Rows[e.RowIndex].Cells
                //[e.ColumnIndex].Value;
                bool value = (bool)this.dgvAccess.Rows[e.RowIndex].Cells
                [e.ColumnIndex].EditedFormattedValue;
               
                this.updateRow(e.RowIndex, e.ColumnIndex, value);
                #region 
                //if (access.IsContanerAccess())
                //{
                //    //找到窗体权限
                //    int rowStart = e.RowIndex;
                //    int rowEnd = e.RowIndex;
                //    for (int i = rowStart + 1; i < this.dgvAccess.Rows.Count; i++)
                //    {
                //        ControlAccess conAccess = this.dgvAccess.Rows[i].Tag as ControlAccess;
                //        if (conAccess.ContainerName == access.ContainerName)
                //        {
                //            rowEnd = i;
                //        }
                //        else
                //        {
                //            break;
                //        }
                //    }                  
                //    for (int rowIndex = rowStart; rowIndex <= rowEnd; rowIndex++)
                //    {
                //        this.dgvAccess.Rows[rowIndex].Cells[e.ColumnIndex].Value = value;
                //        this.updateRow(rowIndex, e.ColumnIndex, value);
                //    }

                //}
                //else
                //{
                //    this.updateRow(e.RowIndex, e.ColumnIndex, value);
                //}
                #endregion

            }
        }

        
    }
}
