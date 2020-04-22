namespace Anda.Fluid.Domain.Sensors
{
    partial class SettingProportionerControl
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
            this.lblCmd = new System.Windows.Forms.Label();
            this.cbxChn1 = new System.Windows.Forms.ComboBox();
            this.cbxControlType1 = new System.Windows.Forms.ComboBox();
            this.lblContorlType = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxChn2 = new System.Windows.Forms.ComboBox();
            this.cbxControlType2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCmd
            // 
            this.lblCmd.AutoSize = true;
            this.lblCmd.Location = new System.Drawing.Point(32, 23);
            this.lblCmd.Name = "lblCmd";
            this.lblCmd.Size = new System.Drawing.Size(47, 12);
            this.lblCmd.TabIndex = 3;
            this.lblCmd.Text = "Channel";
            // 
            // cbxChn1
            // 
            this.cbxChn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChn1.FormattingEnabled = true;
            this.cbxChn1.Location = new System.Drawing.Point(88, 20);
            this.cbxChn1.Name = "cbxChn1";
            this.cbxChn1.Size = new System.Drawing.Size(121, 20);
            this.cbxChn1.TabIndex = 4;
            // 
            // cbxControlType1
            // 
            this.cbxControlType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxControlType1.FormattingEnabled = true;
            this.cbxControlType1.Location = new System.Drawing.Point(88, 46);
            this.cbxControlType1.Name = "cbxControlType1";
            this.cbxControlType1.Size = new System.Drawing.Size(121, 20);
            this.cbxControlType1.TabIndex = 5;
            // 
            // lblContorlType
            // 
            this.lblContorlType.AutoSize = true;
            this.lblContorlType.Location = new System.Drawing.Point(11, 49);
            this.lblContorlType.Name = "lblContorlType";
            this.lblContorlType.Size = new System.Drawing.Size(71, 12);
            this.lblContorlType.TabIndex = 6;
            this.lblContorlType.Text = "ControlType";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCmd);
            this.groupBox1.Controls.Add(this.lblContorlType);
            this.groupBox1.Controls.Add(this.cbxChn1);
            this.groupBox1.Controls.Add(this.cbxControlType1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 85);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "valve1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbxChn2);
            this.groupBox2.Controls.Add(this.cbxControlType2);
            this.groupBox2.Location = new System.Drawing.Point(3, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 85);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "valve2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Channel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "ControlType";
            // 
            // cbxChn2
            // 
            this.cbxChn2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChn2.FormattingEnabled = true;
            this.cbxChn2.Location = new System.Drawing.Point(88, 20);
            this.cbxChn2.Name = "cbxChn2";
            this.cbxChn2.Size = new System.Drawing.Size(121, 20);
            this.cbxChn2.TabIndex = 4;
            // 
            // cbxControlType2
            // 
            this.cbxControlType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxControlType2.FormattingEnabled = true;
            this.cbxControlType2.Location = new System.Drawing.Point(88, 46);
            this.cbxControlType2.Name = "cbxControlType2";
            this.cbxControlType2.Size = new System.Drawing.Size(121, 20);
            this.cbxControlType2.TabIndex = 5;
            // 
            // SettingProportionerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingProportionerControl";
            this.Size = new System.Drawing.Size(360, 217);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblCmd;
        private System.Windows.Forms.ComboBox cbxChn1;
        private System.Windows.Forms.ComboBox cbxControlType1;
        private System.Windows.Forms.Label lblContorlType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxChn2;
        private System.Windows.Forms.ComboBox cbxControlType2;
    }
}
