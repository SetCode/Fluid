namespace Anda.Fluid.Domain.Dialogs
{
    partial class LaserControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.btnStatus = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRead);
            this.groupBox1.Controls.Add(this.btnStatus);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.btnMeasure);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 112);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Height Sensor";
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(6, 48);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(100, 22);
            this.txtRead.TabIndex = 1;
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(112, 19);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(75, 23);
            this.btnStatus.TabIndex = 2;
            this.btnStatus.Text = "state";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(6, 20);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 22);
            this.txtStatus.TabIndex = 3;
            // 
            // btnMeasure
            // 
            this.btnMeasure.Location = new System.Drawing.Point(6, 77);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(181, 23);
            this.btnMeasure.TabIndex = 12;
            this.btnMeasure.Text = "do measure";
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(112, 47);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // LaserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LaserControl";
            this.Size = new System.Drawing.Size(213, 118);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRead;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.Button btnRead;
    }
}
