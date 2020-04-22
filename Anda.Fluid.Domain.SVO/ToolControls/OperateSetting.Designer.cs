namespace Anda.Fluid.Domain.SVO
{
    partial class OperateSetting
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpRestartSetting = new System.Windows.Forms.GroupBox();
            this.grpRestartSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(43, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "IsReStart";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpRestartSetting
            // 
            this.grpRestartSetting.Controls.Add(this.checkBox1);
            this.grpRestartSetting.Location = new System.Drawing.Point(12, 12);
            this.grpRestartSetting.Name = "grpRestartSetting";
            this.grpRestartSetting.Size = new System.Drawing.Size(169, 41);
            this.grpRestartSetting.TabIndex = 3;
            this.grpRestartSetting.TabStop = false;
            this.grpRestartSetting.Text = "RestartSetting";
            // 
            // OperateSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 67);
            this.Controls.Add(this.grpRestartSetting);
            this.Controls.Add(this.button1);
            this.Name = "OperateSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OperateSetting";
            this.Load += new System.EventHandler(this.SettingTest_Load);
            this.grpRestartSetting.ResumeLayout(false);
            this.grpRestartSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grpRestartSetting;
    }
}