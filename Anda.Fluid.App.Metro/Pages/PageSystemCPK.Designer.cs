namespace Anda.Fluid.App.Metro.Pages
{
    partial class PageSystemCPK
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
            this.btnCPK = new MetroSet_UI.Controls.MetroSetButton();
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
            // btnCPK
            // 
            this.btnCPK.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnCPK.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCPK.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnCPK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnCPK.HoverTextColor = System.Drawing.Color.White;
            this.btnCPK.Location = new System.Drawing.Point(37, 36);
            this.btnCPK.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnCPK.Name = "btnCPK";
            this.btnCPK.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnCPK.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnCPK.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnCPK.PressTextColor = System.Drawing.Color.White;
            this.btnCPK.Selected = false;
            this.btnCPK.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnCPK.SelectedTextColor = System.Drawing.Color.White;
            this.btnCPK.ShowBorder = true;
            this.btnCPK.Size = new System.Drawing.Size(114, 36);
            this.btnCPK.Style = MetroSet_UI.Design.Style.Dark;
            this.btnCPK.StyleManager = this.styleManager1;
            this.btnCPK.TabIndex = 0;
            this.btnCPK.Text = "CPK";
            this.btnCPK.ThemeAuthor = "Narwin";
            this.btnCPK.ThemeName = "MetroDark";
            this.btnCPK.Click += new System.EventHandler(this.btnCPK_Click);
            // 
            // PageSystemCPK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCPK);
            this.Name = "PageSystemCPK";
            this.Size = new System.Drawing.Size(600, 600);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetButton btnCPK;
    }
}
