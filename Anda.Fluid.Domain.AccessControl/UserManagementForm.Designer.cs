namespace Anda.Fluid.Domain.AccessControl
{
    partial class UserManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.selectCbxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRoleTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUser
            // 
            this.dgvUser.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectCbxColumn,
            this.txtNameColumn,
            this.txtRoleTypeColumn});
            this.dgvUser.Location = new System.Drawing.Point(13, 12);
            this.dgvUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.Size = new System.Drawing.Size(579, 249);
            this.dgvUser.TabIndex = 0;
            // 
            // selectCbxColumn
            // 
            this.selectCbxColumn.FalseValue = false;
            this.selectCbxColumn.HeaderText = "Select";
            this.selectCbxColumn.Name = "selectCbxColumn";
            this.selectCbxColumn.TrueValue = true;
            // 
            // txtNameColumn
            // 
            this.txtNameColumn.HeaderText = "User name";
            this.txtNameColumn.Name = "txtNameColumn";
            this.txtNameColumn.Width = 200;
            // 
            // txtRoleTypeColumn
            // 
            this.txtRoleTypeColumn.HeaderText = "Role";
            this.txtRoleTypeColumn.Name = "txtRoleTypeColumn";
            this.txtRoleTypeColumn.Width = 130;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(13, 267);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 27);
            this.btnAddUser.TabIndex = 1;
            this.btnAddUser.Text = "New User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(121, 267);
            this.btnDeleteUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(145, 27);
            this.btnDeleteUser.TabIndex = 2;
            this.btnDeleteUser.Text = "Delete select";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(492, 267);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // UserManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 306);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.dgvUser);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserManagementForm";
            this.Text = "UserManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectCbxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtRoleTypeColumn;
    }
}