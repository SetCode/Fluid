namespace Anda.Fluid.App.Metro.Pages
{
    partial class PageSetupAccess
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.btnSetDefaultAccess = new MetroSet_UI.Controls.MetroSetButton();
            this.dgvAccess = new System.Windows.Forms.DataGridView();
            this.btnOK = new MetroSet_UI.Controls.MetroSetButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // btnSetDefaultAccess
            // 
            this.btnSetDefaultAccess.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnSetDefaultAccess.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSetDefaultAccess.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSetDefaultAccess.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSetDefaultAccess.HoverTextColor = System.Drawing.Color.White;
            this.btnSetDefaultAccess.Location = new System.Drawing.Point(15, 671);
            this.btnSetDefaultAccess.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnSetDefaultAccess.Name = "btnSetDefaultAccess";
            this.btnSetDefaultAccess.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnSetDefaultAccess.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSetDefaultAccess.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSetDefaultAccess.PressTextColor = System.Drawing.Color.White;
            this.btnSetDefaultAccess.Selected = false;
            this.btnSetDefaultAccess.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSetDefaultAccess.SelectedTextColor = System.Drawing.Color.White;
            this.btnSetDefaultAccess.ShowBorder = true;
            this.btnSetDefaultAccess.Size = new System.Drawing.Size(114, 23);
            this.btnSetDefaultAccess.Style = MetroSet_UI.Design.Style.Dark;
            this.btnSetDefaultAccess.StyleManager = this.styleManager1;
            this.btnSetDefaultAccess.TabIndex = 0;
            this.btnSetDefaultAccess.Text = "默认权限文件";
            this.btnSetDefaultAccess.ThemeAuthor = "Narwin";
            this.btnSetDefaultAccess.ThemeName = "MetroDark";
            this.btnSetDefaultAccess.Click += new System.EventHandler(this.btnSetDefaultAccess_Click);
            // 
            // dgvAccess
            // 
            this.dgvAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccess.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccess.Location = new System.Drawing.Point(15, 15);
            this.dgvAccess.Name = "dgvAccess";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvAccess.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccess.RowTemplate.Height = 23;
            this.dgvAccess.Size = new System.Drawing.Size(647, 622);
            this.dgvAccess.TabIndex = 1;
            this.dgvAccess.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAccess_CellMouseUp);
            // 
            // btnOK
            // 
            this.btnOK.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnOK.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnOK.HoverTextColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(460, 671);
            this.btnOK.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnOK.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnOK.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnOK.PressTextColor = System.Drawing.Color.White;
            this.btnOK.Selected = false;
            this.btnOK.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnOK.SelectedTextColor = System.Drawing.Color.White;
            this.btnOK.ShowBorder = true;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = MetroSet_UI.Design.Style.Dark;
            this.btnOK.StyleManager = this.styleManager1;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.ThemeAuthor = "Narwin";
            this.btnOK.ThemeName = "MetroDark";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // PageSetupAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvAccess);
            this.Controls.Add(this.btnSetDefaultAccess);
            this.Name = "PageSetupAccess";
            this.Size = new System.Drawing.Size(677, 709);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.PageSetupAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetButton btnSetDefaultAccess;
        private System.Windows.Forms.DataGridView dgvAccess;
        private MetroSet_UI.Controls.MetroSetButton btnOK;
    }
}
