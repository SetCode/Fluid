namespace Anda.Fluid.Domain.Motion
{
    partial class ValveControl
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
            this.nudValve1 = new System.Windows.Forms.NumericUpDown();
            this.cbxValve1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudValve1
            // 
            this.nudValve1.Location = new System.Drawing.Point(4, 3);
            this.nudValve1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudValve1.Name = "nudValve1";
            this.nudValve1.Size = new System.Drawing.Size(111, 21);
            this.nudValve1.TabIndex = 4;
            // 
            // cbxValve1
            // 
            this.cbxValve1.AutoSize = true;
            this.cbxValve1.Location = new System.Drawing.Point(43, 31);
            this.cbxValve1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxValve1.Name = "cbxValve1";
            this.cbxValve1.Size = new System.Drawing.Size(60, 16);
            this.cbxValve1.TabIndex = 3;
            this.cbxValve1.Text = "Valve1";
            this.cbxValve1.UseVisualStyleBackColor = true;
            // 
            // ValveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudValve1);
            this.Controls.Add(this.cbxValve1);
            this.Name = "ValveControl";
            this.Size = new System.Drawing.Size(123, 54);
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudValve1;
        private System.Windows.Forms.CheckBox cbxValve1;
    }
}
