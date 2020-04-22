namespace Anda.Fluid.Domain.Motion
{
    partial class JogControl
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnHighOrLow = new System.Windows.Forms.Button();
            this.btnValveSelect = new System.Windows.Forms.Button();
            this.jogComboBox1 = new Anda.Fluid.Domain.Motion.JogComboBox();
            this.btnRp = new System.Windows.Forms.Button();
            this.btnRn = new System.Windows.Forms.Button();
            this.btnZn = new System.Windows.Forms.Button();
            this.btnZp = new System.Windows.Forms.Button();
            this.btnXp = new System.Windows.Forms.Button();
            this.btnYn = new System.Windows.Forms.Button();
            this.btnXn = new System.Windows.Forms.Button();
            this.btnYp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(4, 129);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 18);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Inc Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnHighOrLow
            // 
            this.btnHighOrLow.Location = new System.Drawing.Point(3, 3);
            this.btnHighOrLow.Name = "btnHighOrLow";
            this.btnHighOrLow.Size = new System.Drawing.Size(25, 25);
            this.btnHighOrLow.TabIndex = 10;
            this.btnHighOrLow.Text = "H";
            this.btnHighOrLow.UseVisualStyleBackColor = true;
            // 
            // btnValveSelect
            // 
            this.btnValveSelect.Location = new System.Drawing.Point(94, 3);
            this.btnValveSelect.Name = "btnValveSelect";
            this.btnValveSelect.Size = new System.Drawing.Size(25, 25);
            this.btnValveSelect.TabIndex = 12;
            this.btnValveSelect.Text = "1";
            this.btnValveSelect.UseVisualStyleBackColor = true;
            this.btnValveSelect.Click += new System.EventHandler(this.btnValveSelect_Click);
            // 
            // jogComboBox1
            // 
            this.jogComboBox1.FormattingEnabled = true;
            this.jogComboBox1.Location = new System.Drawing.Point(97, 127);
            this.jogComboBox1.Name = "jogComboBox1";
            this.jogComboBox1.Size = new System.Drawing.Size(77, 22);
            this.jogComboBox1.TabIndex = 11;
            // 
            // btnRp
            // 
            this.btnRp.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Reboot_24px;
            this.btnRp.Location = new System.Drawing.Point(87, 87);
            this.btnRp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRp.Name = "btnRp";
            this.btnRp.Size = new System.Drawing.Size(32, 32);
            this.btnRp.TabIndex = 14;
            this.btnRp.UseVisualStyleBackColor = true;
            // 
            // btnRn
            // 
            this.btnRn.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Rotate_24px1;
            this.btnRn.Location = new System.Drawing.Point(4, 87);
            this.btnRn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRn.Name = "btnRn";
            this.btnRn.Size = new System.Drawing.Size(32, 32);
            this.btnRn.TabIndex = 13;
            this.btnRn.UseVisualStyleBackColor = true;
            // 
            // btnZn
            // 
            this.btnZn.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Long_Arrow_Down_16px;
            this.btnZn.Location = new System.Drawing.Point(134, 79);
            this.btnZn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnZn.Name = "btnZn";
            this.btnZn.Size = new System.Drawing.Size(40, 40);
            this.btnZn.TabIndex = 5;
            this.btnZn.UseVisualStyleBackColor = true;
            // 
            // btnZp
            // 
            this.btnZp.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Long_Arrow_Up_16px;
            this.btnZp.Location = new System.Drawing.Point(134, 3);
            this.btnZp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnZp.Name = "btnZp";
            this.btnZp.Size = new System.Drawing.Size(40, 40);
            this.btnZp.TabIndex = 4;
            this.btnZp.UseVisualStyleBackColor = true;
            // 
            // btnXp
            // 
            this.btnXp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnXp.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Right_16px;
            this.btnXp.Location = new System.Drawing.Point(80, 41);
            this.btnXp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXp.Name = "btnXp";
            this.btnXp.Size = new System.Drawing.Size(40, 40);
            this.btnXp.TabIndex = 3;
            this.btnXp.UseVisualStyleBackColor = true;
            // 
            // btnYn
            // 
            this.btnYn.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Down_16px;
            this.btnYn.Location = new System.Drawing.Point(42, 79);
            this.btnYn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnYn.Name = "btnYn";
            this.btnYn.Size = new System.Drawing.Size(40, 40);
            this.btnYn.TabIndex = 2;
            this.btnYn.UseVisualStyleBackColor = true;
            // 
            // btnXn
            // 
            this.btnXn.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Left_16px;
            this.btnXn.Location = new System.Drawing.Point(4, 41);
            this.btnXn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXn.Name = "btnXn";
            this.btnXn.Size = new System.Drawing.Size(40, 40);
            this.btnXn.TabIndex = 1;
            this.btnXn.UseVisualStyleBackColor = true;
            // 
            // btnYp
            // 
            this.btnYp.Image = global::Anda.Fluid.Domain.Motion.Properties.Resources.Up_16px;
            this.btnYp.Location = new System.Drawing.Point(42, 3);
            this.btnYp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnYp.Name = "btnYp";
            this.btnYp.Size = new System.Drawing.Size(40, 40);
            this.btnYp.TabIndex = 0;
            this.btnYp.UseVisualStyleBackColor = true;
            // 
            // JogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRp);
            this.Controls.Add(this.btnRn);
            this.Controls.Add(this.btnValveSelect);
            this.Controls.Add(this.jogComboBox1);
            this.Controls.Add(this.btnHighOrLow);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnZn);
            this.Controls.Add(this.btnZp);
            this.Controls.Add(this.btnXp);
            this.Controls.Add(this.btnYn);
            this.Controls.Add(this.btnXn);
            this.Controls.Add(this.btnYp);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "JogControl";
            this.Size = new System.Drawing.Size(180, 158);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYp;
        private System.Windows.Forms.Button btnXn;
        private System.Windows.Forms.Button btnYn;
        private System.Windows.Forms.Button btnXp;
        private System.Windows.Forms.Button btnZp;
        private System.Windows.Forms.Button btnZn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnHighOrLow;
        private JogComboBox jogComboBox1;
        private System.Windows.Forms.Button btnValveSelect;
        private System.Windows.Forms.Button btnRn;
        private System.Windows.Forms.Button btnRp;
    }
}
