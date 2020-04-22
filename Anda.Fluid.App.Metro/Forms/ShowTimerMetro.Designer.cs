namespace Anda.Fluid.App.Metro.Forms
{
    partial class ShowTimerMetro
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
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblSleepTime = new MetroSet_UI.Controls.MetroSetLabel();
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
            // metroSetLabel1
            // 
            this.metroSetLabel1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel1.Location = new System.Drawing.Point(35, 94);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(125, 23);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel1.StyleManager = this.styleManager1;
            this.metroSetLabel1.TabIndex = 0;
            this.metroSetLabel1.Text = "Wating for...";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "MetroDark";
            // 
            // lblSleepTime
            // 
            this.lblSleepTime.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSleepTime.Location = new System.Drawing.Point(166, 94);
            this.lblSleepTime.Name = "lblSleepTime";
            this.lblSleepTime.Size = new System.Drawing.Size(196, 23);
            this.lblSleepTime.Style = MetroSet_UI.Design.Style.Dark;
            this.lblSleepTime.StyleManager = this.styleManager1;
            this.lblSleepTime.TabIndex = 1;
            this.lblSleepTime.Text = "0.0 s";
            this.lblSleepTime.ThemeAuthor = "Narwin";
            this.lblSleepTime.ThemeName = "MetroDark";
            // 
            // ShowTimerMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(377, 197);
            this.Controls.Add(this.lblSleepTime);
            this.Controls.Add(this.metroSetLabel1);
            this.Name = "ShowTimerMetro";
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.Text = "Wait";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetLabel lblSleepTime;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
    }
}