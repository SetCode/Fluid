namespace Anda.Fluid.Domain.Motion
{
    partial class PositionVControl
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
            this.ChkSwitch = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblz = new System.Windows.Forms.Label();
            this.lbly = new System.Windows.Forms.Label();
            this.lblx = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkSwitch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblz);
            this.groupBox1.Controls.Add(this.lbly);
            this.groupBox1.Controls.Add(this.lblx);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(177, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Postion";
            // 
            // ChkSwitch
            // 
            this.ChkSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkSwitch.AutoSize = true;
            this.ChkSwitch.Location = new System.Drawing.Point(146, 59);
            this.ChkSwitch.Name = "ChkSwitch";
            this.ChkSwitch.Size = new System.Drawing.Size(35, 18);
            this.ChkSwitch.TabIndex = 6;
            this.ChkSwitch.Text = "S";
            this.ChkSwitch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // lblz
            // 
            this.lblz.AutoSize = true;
            this.lblz.Location = new System.Drawing.Point(39, 59);
            this.lblz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblz.Name = "lblz";
            this.lblz.Size = new System.Drawing.Size(48, 14);
            this.lblz.TabIndex = 5;
            this.lblz.Text = "label6";
            // 
            // lbly
            // 
            this.lbly.AutoSize = true;
            this.lbly.Location = new System.Drawing.Point(39, 40);
            this.lbly.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbly.Name = "lbly";
            this.lbly.Size = new System.Drawing.Size(48, 14);
            this.lbly.TabIndex = 4;
            this.lbly.Text = "label5";
            // 
            // lblx
            // 
            this.lblx.AutoSize = true;
            this.lblx.Location = new System.Drawing.Point(39, 21);
            this.lblx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblx.Name = "lblx";
            this.lblx.Size = new System.Drawing.Size(48, 14);
            this.lblx.TabIndex = 3;
            this.lblx.Text = "label4";
            // 
            // PositionVControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PositionVControl";
            this.Size = new System.Drawing.Size(177, 80);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblz;
        private System.Windows.Forms.Label lbly;
        private System.Windows.Forms.Label lblx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkSwitch;
    }
}
