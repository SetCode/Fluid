namespace Anda.Fluid.Domain.Sensors
{
    partial class SettingScaleControl
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
            this.cbxVendor = new System.Windows.Forms.ComboBox();
            this.txtPrint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTare = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZero = new System.Windows.Forms.TextBox();
            this.ckbDTR = new System.Windows.Forms.CheckBox();
            this.ckbRTS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor";
            // 
            // cbxVendor
            // 
            this.cbxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendor.FormattingEnabled = true;
            this.cbxVendor.Location = new System.Drawing.Point(107, 21);
            this.cbxVendor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxVendor.Name = "cbxVendor";
            this.cbxVendor.Size = new System.Drawing.Size(160, 22);
            this.cbxVendor.TabIndex = 1;
            // 
            // txtPrint
            // 
            this.txtPrint.Location = new System.Drawing.Point(107, 61);
            this.txtPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPrint.Name = "txtPrint";
            this.txtPrint.Size = new System.Drawing.Size(160, 22);
            this.txtPrint.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Print";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tare";
            // 
            // txtTare
            // 
            this.txtTare.Location = new System.Drawing.Point(107, 92);
            this.txtTare.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTare.Name = "txtTare";
            this.txtTare.Size = new System.Drawing.Size(160, 22);
            this.txtTare.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zero";
            // 
            // txtZero
            // 
            this.txtZero.Location = new System.Drawing.Point(107, 124);
            this.txtZero.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtZero.Name = "txtZero";
            this.txtZero.Size = new System.Drawing.Size(160, 22);
            this.txtZero.TabIndex = 6;
            // 
            // ckbDTR
            // 
            this.ckbDTR.AutoSize = true;
            this.ckbDTR.Location = new System.Drawing.Point(107, 173);
            this.ckbDTR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ckbDTR.Name = "ckbDTR";
            this.ckbDTR.Size = new System.Drawing.Size(53, 18);
            this.ckbDTR.TabIndex = 8;
            this.ckbDTR.Text = "DTR";
            this.ckbDTR.UseVisualStyleBackColor = true;
            this.ckbDTR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ckbDTR_MouseClick);
            // 
            // ckbRTS
            // 
            this.ckbRTS.AutoSize = true;
            this.ckbRTS.Location = new System.Drawing.Point(212, 173);
            this.ckbRTS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ckbRTS.Name = "ckbRTS";
            this.ckbRTS.Size = new System.Drawing.Size(52, 18);
            this.ckbRTS.TabIndex = 9;
            this.ckbRTS.Text = "RTS";
            this.ckbRTS.UseVisualStyleBackColor = true;
            this.ckbRTS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ckbRTS_MouseClick);
            // 
            // SettingScaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckbRTS);
            this.Controls.Add(this.ckbDTR);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtZero);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrint);
            this.Controls.Add(this.cbxVendor);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SettingScaleControl";
            this.Size = new System.Drawing.Size(408, 222);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxVendor;
        private System.Windows.Forms.TextBox txtPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTare;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZero;
        private System.Windows.Forms.CheckBox ckbDTR;
        private System.Windows.Forms.CheckBox ckbRTS;
    }
}
