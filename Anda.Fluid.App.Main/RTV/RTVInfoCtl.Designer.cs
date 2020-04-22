namespace Anda.Fluid.App.Main.RTV
{
    partial class RTVInfoCtl
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.widthValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthUpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthDownValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightUpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightDownValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "条形码:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(61, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(591, 22);
            this.textBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.widthValue,
            this.widthUpValue,
            this.widthDownValue,
            this.heightValue,
            this.heightUpValue,
            this.heightDownValue});
            this.dataGridView1.Location = new System.Drawing.Point(7, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(645, 112);
            this.dataGridView1.TabIndex = 2;
            // 
            // widthValue
            // 
            this.widthValue.HeaderText = "测宽值";
            this.widthValue.Name = "widthValue";
            // 
            // widthUpValue
            // 
            this.widthUpValue.HeaderText = "上公差";
            this.widthUpValue.Name = "widthUpValue";
            // 
            // widthDownValue
            // 
            this.widthDownValue.HeaderText = "下公差";
            this.widthDownValue.Name = "widthDownValue";
            // 
            // heightValue
            // 
            this.heightValue.HeaderText = "测高值";
            this.heightValue.Name = "heightValue";
            // 
            // heightUpValue
            // 
            this.heightUpValue.HeaderText = "上公差";
            this.heightUpValue.Name = "heightUpValue";
            // 
            // heightDownValue
            // 
            this.heightDownValue.HeaderText = "下公差";
            this.heightDownValue.Name = "heightDownValue";
            // 
            // RTVInfoCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RTVInfoCtl";
            this.Size = new System.Drawing.Size(664, 146);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthUpValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthDownValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightUpValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightDownValue;
    }
}
