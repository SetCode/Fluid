namespace Anda.Fluid.Domain.Sensors
{
    partial class SettingConveyorHeaterControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxVendor
            // 
            this.cbxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendor.FormattingEnabled = true;
            this.cbxVendor.Location = new System.Drawing.Point(95, 29);
            this.cbxVendor.Name = "cbxVendor";
            this.cbxVendor.Size = new System.Drawing.Size(121, 20);
            this.cbxVendor.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vendor";
            // 
            // SettingConveyorHeaterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxVendor);
            this.Controls.Add(this.label1);
            this.Name = "SettingConveyorHeaterControl";
            this.Size = new System.Drawing.Size(332, 163);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxVendor;
        private System.Windows.Forms.Label label1;
    }
}
