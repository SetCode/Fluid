namespace Anda.Fluid.Domain.AccessControl
{
    partial class FeatureAccessForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvAccess = new System.Windows.Forms.DataGridView();
            this.operatorColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.technicianColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.supervisorColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(567, 510);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 33);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(452, 510);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 33);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvAccess
            // 
            this.dgvAccess.AllowUserToAddRows = false;
            this.dgvAccess.AllowUserToResizeColumns = false;
            this.dgvAccess.AllowUserToResizeRows = false;
            this.dgvAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccess.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operatorColumn,
            this.technicianColumn,
            this.supervisorColumn});
            this.dgvAccess.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAccess.Location = new System.Drawing.Point(0, 0);
            this.dgvAccess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAccess.Name = "dgvAccess";
            this.dgvAccess.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvAccess.RowTemplate.Height = 30;
            this.dgvAccess.Size = new System.Drawing.Size(690, 501);
            this.dgvAccess.TabIndex = 14;
            // 
            // operatorColumn
            // 
            this.operatorColumn.FalseValue = false;
            this.operatorColumn.HeaderText = "Operator";
            this.operatorColumn.MinimumWidth = 50;
            this.operatorColumn.Name = "operatorColumn";
            this.operatorColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operatorColumn.TrueValue = true;
            // 
            // technicianColumn
            // 
            this.technicianColumn.FalseValue = false;
            this.technicianColumn.HeaderText = "Technician";
            this.technicianColumn.Name = "technicianColumn";
            this.technicianColumn.TrueValue = true;
            // 
            // supervisorColumn
            // 
            this.supervisorColumn.FalseValue = false;
            this.supervisorColumn.HeaderText = "Supervisor";
            this.supervisorColumn.Name = "supervisorColumn";
            this.supervisorColumn.TrueValue = true;
            // 
            // FeatureAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 555);
            this.Controls.Add(this.dgvAccess);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureAccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeatureAccessForm";
            this.Load += new System.EventHandler(this.FeatureAccessForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn operatorColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn technicianColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn supervisorColumn;
    }
}