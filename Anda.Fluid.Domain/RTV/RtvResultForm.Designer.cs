namespace Anda.Fluid.Domain.RTV
{
    partial class RtvResultForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthUpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthDownValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightUpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightDownValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStopVoice = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnSaveInMes = new System.Windows.Forms.Button();
            this.btnNoSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.widthValue,
            this.widthUpValue,
            this.widthDownValue,
            this.heightValue,
            this.heightUpValue,
            this.heightDownValue});
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(746, 373);
            this.dataGridView1.TabIndex = 3;
            // 
            // No
            // 
            this.No.HeaderText = "编号";
            this.No.Name = "No";
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
            // btnStopVoice
            // 
            this.btnStopVoice.Location = new System.Drawing.Point(670, 386);
            this.btnStopVoice.Name = "btnStopVoice";
            this.btnStopVoice.Size = new System.Drawing.Size(75, 23);
            this.btnStopVoice.TabIndex = 4;
            this.btnStopVoice.Text = "关闭警报";
            this.btnStopVoice.UseVisualStyleBackColor = true;
            this.btnStopVoice.Click += new System.EventHandler(this.btnStopVoice_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(1, 386);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 23);
            this.btnRetry.TabIndex = 5;
            this.btnRetry.Text = "重新测试";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnSaveInMes
            // 
            this.btnSaveInMes.Location = new System.Drawing.Point(224, 386);
            this.btnSaveInMes.Name = "btnSaveInMes";
            this.btnSaveInMes.Size = new System.Drawing.Size(75, 23);
            this.btnSaveInMes.TabIndex = 7;
            this.btnSaveInMes.Text = "上传数据";
            this.btnSaveInMes.UseVisualStyleBackColor = true;
            this.btnSaveInMes.Click += new System.EventHandler(this.btnSaveInMes_Click);
            // 
            // btnNoSave
            // 
            this.btnNoSave.Location = new System.Drawing.Point(447, 386);
            this.btnNoSave.Name = "btnNoSave";
            this.btnNoSave.Size = new System.Drawing.Size(75, 23);
            this.btnNoSave.TabIndex = 8;
            this.btnNoSave.Text = "不上传";
            this.btnNoSave.UseVisualStyleBackColor = true;
            this.btnNoSave.Click += new System.EventHandler(this.btnNoSave_Click);
            // 
            // RtvResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 421);
            this.Controls.Add(this.btnNoSave);
            this.Controls.Add(this.btnSaveInMes);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnStopVoice);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RtvResultForm";
            this.Text = "检测结果";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RtvResultForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthUpValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthDownValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightUpValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightDownValue;
        private System.Windows.Forms.Button btnStopVoice;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnSaveInMes;
        private System.Windows.Forms.Button btnNoSave;
    }
}