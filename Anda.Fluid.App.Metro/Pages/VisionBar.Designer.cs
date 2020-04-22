namespace Anda.Fluid.App.Metro.Pages
{
    partial class VisionBar
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
            this.lblGain = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblExpo = new MetroSet_UI.Controls.MetroSetLabel();
            this.btnShape = new MetroSet_UI.Controls.MetroSetButtonImg();
            this.btnLight = new MetroSet_UI.Controls.MetroSetButtonImg();
            this.nudRaduis = new System.Windows.Forms.NumericUpDown();
            this.tbrExposure = new System.Windows.Forms.TrackBar();
            this.tbrGain = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.nudRaduis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrGain)).BeginInit();
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
            // lblGain
            // 
            this.lblGain.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblGain.Location = new System.Drawing.Point(278, 10);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(61, 23);
            this.lblGain.Style = MetroSet_UI.Design.Style.Dark;
            this.lblGain.StyleManager = this.styleManager1;
            this.lblGain.TabIndex = 78;
            this.lblGain.Text = "10000";
            this.lblGain.ThemeAuthor = "Narwin";
            this.lblGain.ThemeName = "MetroDark";
            // 
            // lblExpo
            // 
            this.lblExpo.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblExpo.Location = new System.Drawing.Point(110, 10);
            this.lblExpo.Name = "lblExpo";
            this.lblExpo.Size = new System.Drawing.Size(61, 23);
            this.lblExpo.Style = MetroSet_UI.Design.Style.Dark;
            this.lblExpo.StyleManager = this.styleManager1;
            this.lblExpo.TabIndex = 77;
            this.lblExpo.Text = "10000";
            this.lblExpo.ThemeAuthor = "Narwin";
            this.lblExpo.ThemeName = "MetroDark";
            // 
            // btnShape
            // 
            this.btnShape.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnShape.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnShape.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnShape.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnShape.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnShape.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnShape.HoverTextColor = System.Drawing.Color.White;
            this.btnShape.Image = global::Anda.Fluid.App.Metro.Properties.Resources.Rect_24px_1186322;
            this.btnShape.ImageAlpha = 1F;
            this.btnShape.Location = new System.Drawing.Point(442, 7);
            this.btnShape.Name = "btnShape";
            this.btnShape.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnShape.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnShape.NormalTextColor = System.Drawing.Color.White;
            this.btnShape.Offset = 4;
            this.btnShape.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnShape.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnShape.PressTextColor = System.Drawing.Color.White;
            this.btnShape.ShowBorder = false;
            this.btnShape.Size = new System.Drawing.Size(24, 24);
            this.btnShape.Style = MetroSet_UI.Design.Style.Dark;
            this.btnShape.StyleManager = this.styleManager1;
            this.btnShape.TabIndex = 75;
            this.btnShape.ThemeAuthor = "Narwin";
            this.btnShape.ThemeName = "MetroDark";
            // 
            // btnLight
            // 
            this.btnLight.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnLight.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnLight.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnLight.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLight.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnLight.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnLight.HoverTextColor = System.Drawing.Color.White;
            this.btnLight.Image = global::Anda.Fluid.App.Metro.Properties.Resources.numbers_3_24px;
            this.btnLight.ImageAlpha = 1F;
            this.btnLight.Location = new System.Drawing.Point(335, 7);
            this.btnLight.Name = "btnLight";
            this.btnLight.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnLight.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnLight.NormalTextColor = System.Drawing.Color.White;
            this.btnLight.Offset = 4;
            this.btnLight.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnLight.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnLight.PressTextColor = System.Drawing.Color.White;
            this.btnLight.ShowBorder = false;
            this.btnLight.Size = new System.Drawing.Size(24, 24);
            this.btnLight.Style = MetroSet_UI.Design.Style.Dark;
            this.btnLight.StyleManager = this.styleManager1;
            this.btnLight.TabIndex = 74;
            this.btnLight.ThemeAuthor = "Narwin";
            this.btnLight.ThemeName = "MetroDark";
            // 
            // nudRaduis
            // 
            this.nudRaduis.Location = new System.Drawing.Point(365, 6);
            this.nudRaduis.Name = "nudRaduis";
            this.nudRaduis.Size = new System.Drawing.Size(71, 25);
            this.nudRaduis.TabIndex = 79;
            // 
            // tbrExposure
            // 
            this.tbrExposure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbrExposure.Location = new System.Drawing.Point(0, 7);
            this.tbrExposure.Name = "tbrExposure";
            this.tbrExposure.Size = new System.Drawing.Size(115, 45);
            this.tbrExposure.TabIndex = 80;
            // 
            // tbrGain
            // 
            this.tbrGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbrGain.Location = new System.Drawing.Point(167, 7);
            this.tbrGain.Name = "tbrGain";
            this.tbrGain.Size = new System.Drawing.Size(115, 45);
            this.tbrGain.TabIndex = 81;
            // 
            // VisionBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnLight);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.lblExpo);
            this.Controls.Add(this.tbrGain);
            this.Controls.Add(this.tbrExposure);
            this.Controls.Add(this.nudRaduis);
            this.Controls.Add(this.btnShape);
            this.Name = "VisionBar";
            this.Size = new System.Drawing.Size(471, 40);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.VisionBar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRaduis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrGain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetLabel lblGain;
        private MetroSet_UI.Controls.MetroSetLabel lblExpo;
        private MetroSet_UI.Controls.MetroSetButtonImg btnShape;
        private MetroSet_UI.Controls.MetroSetButtonImg btnLight;
        private System.Windows.Forms.NumericUpDown nudRaduis;
        private System.Windows.Forms.TrackBar tbrGain;
        private System.Windows.Forms.TrackBar tbrExposure;
    }
}
