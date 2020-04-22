namespace Anda.Fluid.App.Main
{
    partial class RunInfoControl2
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
            this.txtDotWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMeasureHeight = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDotWeight
            // 
            this.txtDotWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDotWeight.Location = new System.Drawing.Point(109, 6);
            this.txtDotWeight.Name = "txtDotWeight";
            this.txtDotWeight.Size = new System.Drawing.Size(137, 21);
            this.txtDotWeight.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "单点重量";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Location = new System.Drawing.Point(109, 60);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(137, 21);
            this.txtBarcode.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "条码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "测高值";
            // 
            // txtMeasureHeight
            // 
            this.txtMeasureHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMeasureHeight.Location = new System.Drawing.Point(109, 33);
            this.txtMeasureHeight.Name = "txtMeasureHeight";
            this.txtMeasureHeight.Size = new System.Drawing.Size(137, 21);
            this.txtMeasureHeight.TabIndex = 26;
            // 
            // RunInfoControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMeasureHeight);
            this.Controls.Add(this.txtDotWeight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "RunInfoControl2";
            this.Size = new System.Drawing.Size(257, 249);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDotWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMeasureHeight;
    }
}
