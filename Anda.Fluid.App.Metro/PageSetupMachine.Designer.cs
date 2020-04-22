namespace Anda.Fluid.App.Metro
{
    partial class PageSetupMachine
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
            this.BtnSave = new MetroSet_UI.Controls.MetroSetButton();
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
            this.propertyGrid1.Size = new System.Drawing.Size(576, 400);
            this.propertyGrid1.TabIndex = 0;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.DisabledForeColor = System.Drawing.Color.Gray;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnSave.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.BtnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.BtnSave.HoverTextColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(505, 418);
            this.BtnSave.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.NormalTextColor = System.Drawing.Color.LightGray;
            this.BtnSave.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.BtnSave.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.BtnSave.PressTextColor = System.Drawing.Color.White;
            this.BtnSave.Selected = false;
            this.BtnSave.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.BtnSave.SelectedTextColor = System.Drawing.Color.White;
            this.BtnSave.ShowBorder = true;
            this.BtnSave.Size = new System.Drawing.Size(80, 30);
            this.BtnSave.Style = MetroSet_UI.Design.Style.Dark;
            this.BtnSave.StyleManager = this.styleManager1;
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "保存";
            this.BtnSave.ThemeAuthor = "Narwin";
            this.BtnSave.ThemeName = "MetroDark";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // PageSetupMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "PageSetupMachine";
            this.Size = new System.Drawing.Size(600, 600);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private MetroSet_UI.Controls.MetroSetButton BtnSave;
    }
}
