namespace Anda.Fluid.App.Metro
{
    partial class PageSetupRobot
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnSave = new MetroSet_UI.Controls.MetroSetButton();
            this.btnDefault = new MetroSet_UI.Controls.MetroSetButton();
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
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(576, 642);
            this.propertyGrid1.TabIndex = 0;
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
            this.btnSave.Location = new System.Drawing.Point(510, 660);
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
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.ThemeAuthor = "Narwin";
            this.btnSave.ThemeName = "MetroDark";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnDefault.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnDefault.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnDefault.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnDefault.HoverTextColor = System.Drawing.Color.White;
            this.btnDefault.Location = new System.Drawing.Point(12, 660);
            this.btnDefault.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnDefault.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnDefault.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnDefault.PressTextColor = System.Drawing.Color.White;
            this.btnDefault.Selected = false;
            this.btnDefault.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnDefault.SelectedTextColor = System.Drawing.Color.White;
            this.btnDefault.ShowBorder = true;
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.Style = MetroSet_UI.Design.Style.Dark;
            this.btnDefault.StyleManager = this.styleManager1;
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "默认值";
            this.btnDefault.ThemeAuthor = "Narwin";
            this.btnDefault.ThemeName = "MetroDark";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // PageSetupRobot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "PageSetupRobot";
            this.Size = new System.Drawing.Size(600, 698);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetButton btnSave;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private MetroSet_UI.Controls.MetroSetButton btnDefault;
    }
}
