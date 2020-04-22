namespace Anda.Fluid.Domain.Vision
{
    partial class AndaLightControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblExpo = new System.Windows.Forms.Label();
            this.cbxLight = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbrGain = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbrExposure = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblGain);
            this.panel1.Controls.Add(this.lblExpo);
            this.panel1.Controls.Add(this.cbxLight);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbrGain);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbrExposure);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 26);
            this.panel1.TabIndex = 2;
            // 
            // lblGain
            // 
            this.lblGain.AutoSize = true;
            this.lblGain.Location = new System.Drawing.Point(318, 6);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(40, 12);
            this.lblGain.TabIndex = 12;
            this.lblGain.Text = "10000";
            // 
            // lblExpo
            // 
            this.lblExpo.AutoSize = true;
            this.lblExpo.Location = new System.Drawing.Point(143, 6);
            this.lblExpo.Name = "lblExpo";
            this.lblExpo.Size = new System.Drawing.Size(40, 12);
            this.lblExpo.TabIndex = 11;
            this.lblExpo.Text = "10000";
            // 
            // cbxLight
            // 
            this.cbxLight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLight.FormattingEnabled = true;
            this.cbxLight.Location = new System.Drawing.Point(364, 3);
            this.cbxLight.Name = "cbxLight";
            this.cbxLight.Size = new System.Drawing.Size(75, 20);
            this.cbxLight.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gain";
            // 
            // tbrGain
            // 
            this.tbrGain.Location = new System.Drawing.Point(204, 2);
            this.tbrGain.Name = "tbrGain";
            this.tbrGain.Size = new System.Drawing.Size(120, 45);
            this.tbrGain.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expo";
            // 
            // tbrExposure
            // 
            this.tbrExposure.Location = new System.Drawing.Point(28, 2);
            this.tbrExposure.Name = "tbrExposure";
            this.tbrExposure.Size = new System.Drawing.Size(120, 45);
            this.tbrExposure.TabIndex = 8;
            // 
            // AndaLightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AndaLightControl";
            this.Size = new System.Drawing.Size(442, 26);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrExposure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblExpo;
        private System.Windows.Forms.ComboBox cbxLight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbrGain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbrExposure;
    }
}
