namespace Anda.Fluid.Domain.Dialogs
{
    partial class ManualSprayControl
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
            this.nudValve2 = new System.Windows.Forms.NumericUpDown();
            this.lblValve2 = new System.Windows.Forms.Label();
            this.lblValve1 = new System.Windows.Forms.Label();
            this.btnValve2Stop = new System.Windows.Forms.Button();
            this.btnValve2Spray = new System.Windows.Forms.Button();
            this.btnAir2 = new System.Windows.Forms.Button();
            this.nudAir2 = new System.Windows.Forms.NumericUpDown();
            this.btnEditValve2 = new System.Windows.Forms.Button();
            this.btnValve1Stop = new System.Windows.Forms.Button();
            this.btnValve1Spray = new System.Windows.Forms.Button();
            this.btnAir1 = new System.Windows.Forms.Button();
            this.nudAir1 = new System.Windows.Forms.NumericUpDown();
            this.btnEditValve1 = new System.Windows.Forms.Button();
            this.nudValve1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudValve2
            // 
            this.nudValve2.Location = new System.Drawing.Point(8, 177);
            this.nudValve2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudValve2.Name = "nudValve2";
            this.nudValve2.Size = new System.Drawing.Size(180, 25);
            this.nudValve2.TabIndex = 32;
            // 
            // lblValve2
            // 
            this.lblValve2.AutoSize = true;
            this.lblValve2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblValve2.ForeColor = System.Drawing.Color.White;
            this.lblValve2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblValve2.Location = new System.Drawing.Point(5, 126);
            this.lblValve2.Name = "lblValve2";
            this.lblValve2.Size = new System.Drawing.Size(60, 17);
            this.lblValve2.TabIndex = 31;
            this.lblValve2.Text = "Valve2";
            this.lblValve2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblValve1
            // 
            this.lblValve1.AutoSize = true;
            this.lblValve1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblValve1.ForeColor = System.Drawing.Color.White;
            this.lblValve1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblValve1.Location = new System.Drawing.Point(5, 5);
            this.lblValve1.Name = "lblValve1";
            this.lblValve1.Size = new System.Drawing.Size(60, 17);
            this.lblValve1.TabIndex = 30;
            this.lblValve1.Text = "Valve1";
            this.lblValve1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnValve2Stop
            // 
            this.btnValve2Stop.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Stop_16px;
            this.btnValve2Stop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnValve2Stop.Location = new System.Drawing.Point(148, 208);
            this.btnValve2Stop.Name = "btnValve2Stop";
            this.btnValve2Stop.Size = new System.Drawing.Size(41, 23);
            this.btnValve2Stop.TabIndex = 29;
            this.btnValve2Stop.UseVisualStyleBackColor = true;
            this.btnValve2Stop.Click += new System.EventHandler(this.btnValve2Stop_Click);
            // 
            // btnValve2Spray
            // 
            this.btnValve2Spray.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Play_16px;
            this.btnValve2Spray.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnValve2Spray.Location = new System.Drawing.Point(102, 208);
            this.btnValve2Spray.Name = "btnValve2Spray";
            this.btnValve2Spray.Size = new System.Drawing.Size(41, 23);
            this.btnValve2Spray.TabIndex = 28;
            this.btnValve2Spray.UseVisualStyleBackColor = true;
            this.btnValve2Spray.Click += new System.EventHandler(this.btnValve2Spray_Click);
            // 
            // btnAir2
            // 
            this.btnAir2.ForeColor = System.Drawing.Color.Black;
            this.btnAir2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAir2.Location = new System.Drawing.Point(135, 145);
            this.btnAir2.Name = "btnAir2";
            this.btnAir2.Size = new System.Drawing.Size(53, 27);
            this.btnAir2.TabIndex = 27;
            this.btnAir2.Text = "气压";
            this.btnAir2.UseVisualStyleBackColor = true;
            this.btnAir2.Click += new System.EventHandler(this.btnAir2_Click);
            // 
            // nudAir2
            // 
            this.nudAir2.Location = new System.Drawing.Point(8, 146);
            this.nudAir2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudAir2.Name = "nudAir2";
            this.nudAir2.Size = new System.Drawing.Size(120, 25);
            this.nudAir2.TabIndex = 26;
            // 
            // btnEditValve2
            // 
            this.btnEditValve2.ForeColor = System.Drawing.Color.Black;
            this.btnEditValve2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditValve2.Location = new System.Drawing.Point(8, 208);
            this.btnEditValve2.Name = "btnEditValve2";
            this.btnEditValve2.Size = new System.Drawing.Size(75, 23);
            this.btnEditValve2.TabIndex = 25;
            this.btnEditValve2.Text = "edit";
            this.btnEditValve2.UseVisualStyleBackColor = true;
            this.btnEditValve2.Click += new System.EventHandler(this.btnEditValve2_Click);
            // 
            // btnValve1Stop
            // 
            this.btnValve1Stop.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Stop_16px;
            this.btnValve1Stop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnValve1Stop.Location = new System.Drawing.Point(148, 87);
            this.btnValve1Stop.Name = "btnValve1Stop";
            this.btnValve1Stop.Size = new System.Drawing.Size(41, 23);
            this.btnValve1Stop.TabIndex = 24;
            this.btnValve1Stop.UseVisualStyleBackColor = true;
            this.btnValve1Stop.Click += new System.EventHandler(this.btnValve1Stop_Click);
            // 
            // btnValve1Spray
            // 
            this.btnValve1Spray.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Play_16px;
            this.btnValve1Spray.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnValve1Spray.Location = new System.Drawing.Point(101, 87);
            this.btnValve1Spray.Name = "btnValve1Spray";
            this.btnValve1Spray.Size = new System.Drawing.Size(41, 23);
            this.btnValve1Spray.TabIndex = 23;
            this.btnValve1Spray.UseVisualStyleBackColor = true;
            this.btnValve1Spray.Click += new System.EventHandler(this.btnValve1Spray_Click);
            // 
            // btnAir1
            // 
            this.btnAir1.ForeColor = System.Drawing.Color.Black;
            this.btnAir1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAir1.Location = new System.Drawing.Point(136, 25);
            this.btnAir1.Name = "btnAir1";
            this.btnAir1.Size = new System.Drawing.Size(52, 25);
            this.btnAir1.TabIndex = 22;
            this.btnAir1.Text = "气压";
            this.btnAir1.UseVisualStyleBackColor = true;
            this.btnAir1.Click += new System.EventHandler(this.btnAir1_Click);
            // 
            // nudAir1
            // 
            this.nudAir1.Location = new System.Drawing.Point(8, 25);
            this.nudAir1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudAir1.Name = "nudAir1";
            this.nudAir1.Size = new System.Drawing.Size(121, 25);
            this.nudAir1.TabIndex = 21;
            // 
            // btnEditValve1
            // 
            this.btnEditValve1.ForeColor = System.Drawing.Color.Black;
            this.btnEditValve1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditValve1.Location = new System.Drawing.Point(8, 87);
            this.btnEditValve1.Name = "btnEditValve1";
            this.btnEditValve1.Size = new System.Drawing.Size(75, 23);
            this.btnEditValve1.TabIndex = 20;
            this.btnEditValve1.Text = "edit";
            this.btnEditValve1.UseVisualStyleBackColor = true;
            this.btnEditValve1.Click += new System.EventHandler(this.btnEditValve1_Click);
            // 
            // nudValve1
            // 
            this.nudValve1.Location = new System.Drawing.Point(8, 56);
            this.nudValve1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudValve1.Name = "nudValve1";
            this.nudValve1.Size = new System.Drawing.Size(180, 25);
            this.nudValve1.TabIndex = 19;
            // 
            // ManualSprayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.nudValve2);
            this.Controls.Add(this.lblValve2);
            this.Controls.Add(this.lblValve1);
            this.Controls.Add(this.btnValve2Stop);
            this.Controls.Add(this.btnValve2Spray);
            this.Controls.Add(this.btnAir2);
            this.Controls.Add(this.nudAir2);
            this.Controls.Add(this.btnEditValve2);
            this.Controls.Add(this.btnValve1Stop);
            this.Controls.Add(this.btnValve1Spray);
            this.Controls.Add(this.btnAir1);
            this.Controls.Add(this.nudAir1);
            this.Controls.Add(this.btnEditValve1);
            this.Controls.Add(this.nudValve1);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ManualSprayControl";
            this.Size = new System.Drawing.Size(194, 237);
            ((System.ComponentModel.ISupportInitialize)(this.nudValve2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudValve2;
        private System.Windows.Forms.Label lblValve2;
        private System.Windows.Forms.Label lblValve1;
        private System.Windows.Forms.Button btnValve2Stop;
        private System.Windows.Forms.Button btnValve2Spray;
        private System.Windows.Forms.Button btnAir2;
        private System.Windows.Forms.NumericUpDown nudAir2;
        private System.Windows.Forms.Button btnEditValve2;
        private System.Windows.Forms.Button btnValve1Stop;
        private System.Windows.Forms.Button btnValve1Spray;
        private System.Windows.Forms.Button btnAir1;
        private System.Windows.Forms.NumericUpDown nudAir1;
        private System.Windows.Forms.Button btnEditValve1;
        private System.Windows.Forms.NumericUpDown nudValve1;
    }
}
