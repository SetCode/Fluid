namespace Anda.Fluid.App.Metro.Forms
{
    partial class LoadingMetro
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
            this.metroSetProgressBar1 = new MetroSet_UI.Controls.MetroSetProgressBar();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.lblMessage = new MetroSet_UI.Controls.MetroSetLabel();
            this.SuspendLayout();
            // 
            // metroSetProgressBar1
            // 
            this.metroSetProgressBar1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.metroSetProgressBar1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.metroSetProgressBar1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.metroSetProgressBar1.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.metroSetProgressBar1.DisabledProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetProgressBar1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetProgressBar1.Location = new System.Drawing.Point(15, 69);
            this.metroSetProgressBar1.Maximum = 100;
            this.metroSetProgressBar1.Minimum = 0;
            this.metroSetProgressBar1.Name = "metroSetProgressBar1";
            this.metroSetProgressBar1.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal;
            this.metroSetProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetProgressBar1.Size = new System.Drawing.Size(770, 43);
            this.metroSetProgressBar1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetProgressBar1.StyleManager = this.styleManager1;
            this.metroSetProgressBar1.TabIndex = 0;
            this.metroSetProgressBar1.Text = "metroSetProgressBar1";
            this.metroSetProgressBar1.ThemeAuthor = "Narwin";
            this.metroSetProgressBar1.ThemeName = "MetroDark";
            this.metroSetProgressBar1.Value = 0;
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(15, 135);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(770, 40);
            this.lblMessage.Style = MetroSet_UI.Design.Style.Dark;
            this.lblMessage.StyleManager = this.styleManager1;
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "metroSetLabel1";
            this.lblMessage.ThemeAuthor = "Narwin";
            this.lblMessage.ThemeName = "MetroDark";
            // 
            // LoadingMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 203);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.metroSetProgressBar1);
            this.Name = "LoadingMetro";
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.Text = "Loading...";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.Controls.MetroSetProgressBar metroSetProgressBar1;
        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetLabel lblMessage;
    }
}