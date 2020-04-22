namespace Anda.Fluid.App.Metro
{
    partial class PageSetupCamera
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
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.cmbVendor = new MetroSet_UI.Controls.MetroSetComboBox();
            this.cbxReverseX = new MetroSet_UI.Controls.MetroSetCheckBox();
            this.cbxReverseY = new MetroSet_UI.Controls.MetroSetCheckBox();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.btnSave = new MetroSet_UI.Controls.MetroSetButton();
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
            // cmbVendor
            // 
            this.cmbVendor.AllowDrop = true;
            this.cmbVendor.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.cmbVendor.BackColor = System.Drawing.Color.Transparent;
            this.cmbVendor.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.cmbVendor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.cmbVendor.CausesValidation = false;
            this.cmbVendor.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmbVendor.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cmbVendor.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cmbVendor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendor.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.cmbVendor.FormattingEnabled = true;
            this.cmbVendor.ItemHeight = 20;
            this.cmbVendor.Location = new System.Drawing.Point(181, 64);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cmbVendor.SelectedItemForeColor = System.Drawing.Color.White;
            this.cmbVendor.Size = new System.Drawing.Size(121, 26);
            this.cmbVendor.Style = MetroSet_UI.Design.Style.Dark;
            this.cmbVendor.StyleManager = this.styleManager1;
            this.cmbVendor.TabIndex = 0;
            this.cmbVendor.ThemeAuthor = "Narwin";
            this.cmbVendor.ThemeName = "MetroDark";
            // 
            // cbxReverseX
            // 
            this.cbxReverseX.BackColor = System.Drawing.Color.Transparent;
            this.cbxReverseX.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbxReverseX.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cbxReverseX.Checked = false;
            this.cbxReverseX.CheckSignColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxReverseX.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.cbxReverseX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxReverseX.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.cbxReverseX.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.cbxReverseX.Location = new System.Drawing.Point(181, 118);
            this.cbxReverseX.Name = "cbxReverseX";
            this.cbxReverseX.SignStyle = MetroSet_UI.Enums.SignStyle.Sign;
            this.cbxReverseX.Size = new System.Drawing.Size(108, 16);
            this.cbxReverseX.Style = MetroSet_UI.Design.Style.Dark;
            this.cbxReverseX.StyleManager = this.styleManager1;
            this.cbxReverseX.TabIndex = 1;
            this.cbxReverseX.Text = "X取反";
            this.cbxReverseX.ThemeAuthor = "Narwin";
            this.cbxReverseX.ThemeName = "MetroDark";
            this.cbxReverseX.CheckedChanged += new MetroSet_UI.Controls.MetroSetCheckBox.CheckedChangedEventHandler(this.cbxReverseX_CheckedChanged);
            // 
            // cbxReverseY
            // 
            this.cbxReverseY.BackColor = System.Drawing.Color.Transparent;
            this.cbxReverseY.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbxReverseY.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cbxReverseY.Checked = false;
            this.cbxReverseY.CheckSignColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxReverseY.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.cbxReverseY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxReverseY.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.cbxReverseY.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.cbxReverseY.Location = new System.Drawing.Point(181, 163);
            this.cbxReverseY.Name = "cbxReverseY";
            this.cbxReverseY.SignStyle = MetroSet_UI.Enums.SignStyle.Sign;
            this.cbxReverseY.Size = new System.Drawing.Size(108, 16);
            this.cbxReverseY.Style = MetroSet_UI.Design.Style.Dark;
            this.cbxReverseY.StyleManager = this.styleManager1;
            this.cbxReverseY.TabIndex = 2;
            this.cbxReverseY.Text = "Y取反";
            this.cbxReverseY.ThemeAuthor = "Narwin";
            this.cbxReverseY.ThemeName = "MetroDark";
            this.cbxReverseY.CheckedChanged += new MetroSet_UI.Controls.MetroSetCheckBox.CheckedChangedEventHandler(this.cbxReverseY_CheckedChanged);
            // 
            // metroSetLabel1
            // 
            this.metroSetLabel1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel1.Location = new System.Drawing.Point(115, 67);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(60, 23);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel1.StyleManager = this.styleManager1;
            this.metroSetLabel1.TabIndex = 3;
            this.metroSetLabel1.Text = "类型";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "MetroDark";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSave.HoverTextColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(510, 238);
            this.btnSave.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnSave.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSave.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSave.PressTextColor = System.Drawing.Color.White;
            this.btnSave.Selected = false;
            this.btnSave.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.SelectedTextColor = System.Drawing.Color.White;
            this.btnSave.ShowBorder = true;
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = MetroSet_UI.Design.Style.Dark;
            this.btnSave.StyleManager = this.styleManager1;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.ThemeAuthor = "Narwin";
            this.btnSave.ThemeName = "MetroDark";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PageSetupCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.metroSetLabel1);
            this.Controls.Add(this.cbxReverseY);
            this.Controls.Add(this.cbxReverseX);
            this.Controls.Add(this.cmbVendor);
            this.Name = "PageSetupCamera";
            this.Size = new System.Drawing.Size(600, 600);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetComboBox cmbVendor;
        private MetroSet_UI.Controls.MetroSetCheckBox cbxReverseX;
        private MetroSet_UI.Controls.MetroSetCheckBox cbxReverseY;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private MetroSet_UI.Controls.MetroSetButton btnSave;
    }
}
