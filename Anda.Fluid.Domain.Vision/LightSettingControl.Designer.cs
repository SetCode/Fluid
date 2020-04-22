namespace Anda.Fluid.Domain.Vision
{
    partial class LightSettingControl
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
            this.tbRed = new System.Windows.Forms.TrackBar();
            this.lblChn1 = new System.Windows.Forms.Label();
            this.lblChn2 = new System.Windows.Forms.Label();
            this.tbGreen = new System.Windows.Forms.TrackBar();
            this.lblChn3 = new System.Windows.Forms.Label();
            this.tbBlue = new System.Windows.Forms.TrackBar();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.lblChn4 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblChnBlue = new System.Windows.Forms.Label();
            this.lblChnGreen = new System.Windows.Forms.Label();
            this.lblChnRed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRed
            // 
            this.tbRed.Location = new System.Drawing.Point(51, 21);
            this.tbRed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbRed.Name = "tbRed";
            this.tbRed.Size = new System.Drawing.Size(123, 45);
            this.tbRed.TabIndex = 0;
            this.tbRed.ValueChanged += new System.EventHandler(this.tbRed_ValueChanged);
            // 
            // lblChn1
            // 
            this.lblChn1.AutoSize = true;
            this.lblChn1.Location = new System.Drawing.Point(178, 24);
            this.lblChn1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChn1.Name = "lblChn1";
            this.lblChn1.Size = new System.Drawing.Size(16, 14);
            this.lblChn1.TabIndex = 1;
            this.lblChn1.Text = "0";
            // 
            // lblChn2
            // 
            this.lblChn2.AutoSize = true;
            this.lblChn2.Location = new System.Drawing.Point(178, 44);
            this.lblChn2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChn2.Name = "lblChn2";
            this.lblChn2.Size = new System.Drawing.Size(16, 14);
            this.lblChn2.TabIndex = 4;
            this.lblChn2.Text = "0";
            // 
            // tbGreen
            // 
            this.tbGreen.Location = new System.Drawing.Point(51, 41);
            this.tbGreen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbGreen.Name = "tbGreen";
            this.tbGreen.Size = new System.Drawing.Size(123, 45);
            this.tbGreen.TabIndex = 3;
            this.tbGreen.ValueChanged += new System.EventHandler(this.tbGreen_ValueChanged);
            // 
            // lblChn3
            // 
            this.lblChn3.AutoSize = true;
            this.lblChn3.Location = new System.Drawing.Point(178, 65);
            this.lblChn3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChn3.Name = "lblChn3";
            this.lblChn3.Size = new System.Drawing.Size(16, 14);
            this.lblChn3.TabIndex = 7;
            this.lblChn3.Text = "0";
            // 
            // tbBlue
            // 
            this.tbBlue.Location = new System.Drawing.Point(51, 62);
            this.tbBlue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbBlue.Name = "tbBlue";
            this.tbBlue.Size = new System.Drawing.Size(123, 45);
            this.tbBlue.TabIndex = 6;
            this.tbBlue.TickFrequency = 10;
            this.tbBlue.ValueChanged += new System.EventHandler(this.tbBlue_ValueChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(17, 114);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(57, 18);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "back";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // lblChn4
            // 
            this.lblChn4.AutoSize = true;
            this.lblChn4.Location = new System.Drawing.Point(215, 110);
            this.lblChn4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChn4.Name = "lblChn4";
            this.lblChn4.Size = new System.Drawing.Size(16, 14);
            this.lblChn4.TabIndex = 10;
            this.lblChn4.Text = "0";
            // 
            // trackBar4
            // 
            this.trackBar4.Location = new System.Drawing.Point(73, 113);
            this.trackBar4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(144, 45);
            this.trackBar4.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblChnBlue);
            this.groupBox1.Controls.Add(this.lblChnGreen);
            this.groupBox1.Controls.Add(this.lblChnRed);
            this.groupBox1.Controls.Add(this.lblChn4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.tbBlue);
            this.groupBox1.Controls.Add(this.tbGreen);
            this.groupBox1.Controls.Add(this.tbRed);
            this.groupBox1.Controls.Add(this.lblChn3);
            this.groupBox1.Controls.Add(this.lblChn2);
            this.groupBox1.Controls.Add(this.lblChn1);
            this.groupBox1.Controls.Add(this.trackBar4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(235, 86);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "光源设置";
            // 
            // lblChnBlue
            // 
            this.lblChnBlue.AutoSize = true;
            this.lblChnBlue.Location = new System.Drawing.Point(22, 65);
            this.lblChnBlue.Name = "lblChnBlue";
            this.lblChnBlue.Size = new System.Drawing.Size(20, 14);
            this.lblChnBlue.TabIndex = 14;
            this.lblChnBlue.Text = "蓝";
            // 
            // lblChnGreen
            // 
            this.lblChnGreen.AutoSize = true;
            this.lblChnGreen.Location = new System.Drawing.Point(22, 44);
            this.lblChnGreen.Name = "lblChnGreen";
            this.lblChnGreen.Size = new System.Drawing.Size(20, 14);
            this.lblChnGreen.TabIndex = 13;
            this.lblChnGreen.Text = "绿";
            // 
            // lblChnRed
            // 
            this.lblChnRed.AutoSize = true;
            this.lblChnRed.Location = new System.Drawing.Point(22, 24);
            this.lblChnRed.Name = "lblChnRed";
            this.lblChnRed.Size = new System.Drawing.Size(20, 14);
            this.lblChnRed.TabIndex = 12;
            this.lblChnRed.Text = "红";
            // 
            // LightSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LightSettingControl";
            this.Size = new System.Drawing.Size(235, 86);
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar tbRed;
        private System.Windows.Forms.Label lblChn1;
        private System.Windows.Forms.Label lblChn2;
        private System.Windows.Forms.TrackBar tbGreen;
        private System.Windows.Forms.Label lblChn3;
        private System.Windows.Forms.TrackBar tbBlue;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label lblChn4;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblChnRed;
        private System.Windows.Forms.Label lblChnBlue;
        private System.Windows.Forms.Label lblChnGreen;
    }
}
