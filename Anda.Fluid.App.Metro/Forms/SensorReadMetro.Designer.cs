namespace Anda.Fluid.App.Metro.Forms
{
    partial class SensorReadMetro
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
            this.txtValue = new MetroSet_UI.Controls.MetroSetTextBox();
            this.btnRead = new MetroSet_UI.Controls.MetroSetButton();
            this.metroSetControlBox1 = new MetroSet_UI.Controls.MetroSetControlBox();
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
            // txtValue
            // 
            this.txtValue.AutoCompleteCustomSource = null;
            this.txtValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtValue.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtValue.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValue.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValue.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtValue.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtValue.Image = null;
            this.txtValue.Lines = null;
            this.txtValue.Location = new System.Drawing.Point(15, 58);
            this.txtValue.MaxLength = 32767;
            this.txtValue.Multiline = false;
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = false;
            this.txtValue.Size = new System.Drawing.Size(478, 30);
            this.txtValue.Style = MetroSet_UI.Design.Style.Dark;
            this.txtValue.StyleManager = this.styleManager1;
            this.txtValue.TabIndex = 0;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValue.ThemeAuthor = "Narwin";
            this.txtValue.ThemeName = "MetroDark";
            this.txtValue.UseSystemPasswordChar = false;
            this.txtValue.WatermarkText = "";
            // 
            // btnRead
            // 
            this.btnRead.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnRead.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnRead.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnRead.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnRead.HoverTextColor = System.Drawing.Color.White;
            this.btnRead.Location = new System.Drawing.Point(399, 121);
            this.btnRead.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnRead.Name = "btnRead";
            this.btnRead.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnRead.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnRead.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnRead.PressTextColor = System.Drawing.Color.White;
            this.btnRead.Selected = false;
            this.btnRead.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnRead.SelectedTextColor = System.Drawing.Color.White;
            this.btnRead.ShowBorder = true;
            this.btnRead.Size = new System.Drawing.Size(94, 28);
            this.btnRead.Style = MetroSet_UI.Design.Style.Dark;
            this.btnRead.StyleManager = this.styleManager1;
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "读取";
            this.btnRead.ThemeAuthor = "Narwin";
            this.btnRead.ThemeName = "MetroDark";
            this.btnRead.Click += new System.EventHandler(this.btnReadValue_Click);
            // 
            // metroSetControlBox1
            // 
            this.metroSetControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroSetControlBox1.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroSetControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.metroSetControlBox1.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.metroSetControlBox1.DisabledForeColor = System.Drawing.Color.Silver;
            this.metroSetControlBox1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetControlBox1.Location = new System.Drawing.Point(403, 6);
            this.metroSetControlBox1.MaximizeBox = false;
            this.metroSetControlBox1.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroSetControlBox1.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            this.metroSetControlBox1.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            this.metroSetControlBox1.MinimizeBox = false;
            this.metroSetControlBox1.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroSetControlBox1.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            this.metroSetControlBox1.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            this.metroSetControlBox1.Name = "metroSetControlBox1";
            this.metroSetControlBox1.Size = new System.Drawing.Size(100, 25);
            this.metroSetControlBox1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetControlBox1.StyleManager = this.styleManager1;
            this.metroSetControlBox1.TabIndex = 2;
            this.metroSetControlBox1.Text = "metroSetControlBox1";
            this.metroSetControlBox1.ThemeAuthor = "Narwin";
            this.metroSetControlBox1.ThemeName = "MetroDark";
            // 
            // SensorReadMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(508, 175);
            this.Controls.Add(this.metroSetControlBox1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtValue);
            this.Name = "SensorReadMetro";
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.Text = "SensorReadMetro";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetButton btnRead;
        private MetroSet_UI.Controls.MetroSetTextBox txtValue;
        private MetroSet_UI.Controls.MetroSetControlBox metroSetControlBox1;
    }
}