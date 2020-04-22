namespace Anda.Fluid.Domain.Sensors
{
    partial class SettingBarcodeScanControl
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
            this.cbxVendor = new System.Windows.Forms.ComboBox();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblCmdRead = new System.Windows.Forms.Label();
            this.tbxCmdRead = new System.Windows.Forms.TextBox();
            this.tbxDelimiter = new System.Windows.Forms.TextBox();
            this.lblDelimiter = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxVendor
            // 
            this.cbxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendor.FormattingEnabled = true;
            this.cbxVendor.Location = new System.Drawing.Point(97, 20);
            this.cbxVendor.Name = "cbxVendor";
            this.cbxVendor.Size = new System.Drawing.Size(121, 20);
            this.cbxVendor.TabIndex = 3;
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(41, 23);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(41, 12);
            this.lblVendor.TabIndex = 2;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCmdRead
            // 
            this.lblCmdRead.AutoSize = true;
            this.lblCmdRead.Location = new System.Drawing.Point(35, 61);
            this.lblCmdRead.Name = "lblCmdRead";
            this.lblCmdRead.Size = new System.Drawing.Size(47, 12);
            this.lblCmdRead.TabIndex = 4;
            this.lblCmdRead.Text = "CmdRead";
            this.lblCmdRead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxCmdRead
            // 
            this.tbxCmdRead.Location = new System.Drawing.Point(97, 58);
            this.tbxCmdRead.Name = "tbxCmdRead";
            this.tbxCmdRead.Size = new System.Drawing.Size(121, 21);
            this.tbxCmdRead.TabIndex = 5;
            // 
            // tbxDelimiter
            // 
            this.tbxDelimiter.Location = new System.Drawing.Point(97, 97);
            this.tbxDelimiter.Name = "tbxDelimiter";
            this.tbxDelimiter.Size = new System.Drawing.Size(121, 21);
            this.tbxDelimiter.TabIndex = 7;
            this.tbxDelimiter.WordWrap = false;
            // 
            // lblDelimiter
            // 
            this.lblDelimiter.AutoSize = true;
            this.lblDelimiter.Location = new System.Drawing.Point(23, 100);
            this.lblDelimiter.Name = "lblDelimiter";
            this.lblDelimiter.Size = new System.Drawing.Size(59, 12);
            this.lblDelimiter.TabIndex = 6;
            this.lblDelimiter.Text = "Delimiter";
            this.lblDelimiter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(25, 138);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingBarcodeScanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.tbxDelimiter);
            this.Controls.Add(this.lblDelimiter);
            this.Controls.Add(this.tbxCmdRead);
            this.Controls.Add(this.lblCmdRead);
            this.Controls.Add(this.cbxVendor);
            this.Controls.Add(this.lblVendor);
            this.Name = "SettingBarcodeScanControl";
            this.Size = new System.Drawing.Size(263, 191);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxVendor;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.Label lblCmdRead;
        private System.Windows.Forms.TextBox tbxCmdRead;
        private System.Windows.Forms.TextBox tbxDelimiter;
        private System.Windows.Forms.Label lblDelimiter;
        private System.Windows.Forms.Button btnTest;
    }
}
