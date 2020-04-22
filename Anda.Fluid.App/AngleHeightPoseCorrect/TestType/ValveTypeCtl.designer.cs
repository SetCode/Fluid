namespace Anda.Fluid.APP.AngleHeightPoseCorrect.TestType
{
    partial class TiltTypeCtl
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
            this.btnChangePosture = new System.Windows.Forms.Button();
            this.txtAngle = new Anda.Fluid.Controls.DoubleTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxPosture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.btnChangePosture);
            this.groupBox1.Controls.Add(this.txtAngle);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxPosture);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 240);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第一阶段";
            // 
            // btnChangePosture
            // 
            this.btnChangePosture.Location = new System.Drawing.Point(91, 81);
            this.btnChangePosture.Name = "btnChangePosture";
            this.btnChangePosture.Size = new System.Drawing.Size(75, 23);
            this.btnChangePosture.TabIndex = 93;
            this.btnChangePosture.Text = "变换姿态";
            this.btnChangePosture.UseVisualStyleBackColor = true;
            this.btnChangePosture.Click += new System.EventHandler(this.btnChangePosture_Click);
            // 
            // txtAngle
            // 
            this.txtAngle.BackColor = System.Drawing.Color.White;
            this.txtAngle.Location = new System.Drawing.Point(78, 53);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(88, 22);
            this.txtAngle.TabIndex = 92;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 56);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 14);
            this.label6.TabIndex = 91;
            this.label6.Text = "角度:";
            // 
            // cbxPosture
            // 
            this.cbxPosture.FormattingEnabled = true;
            this.cbxPosture.Items.AddRange(new object[] {
            "不倾斜",
            "左倾斜",
            "右倾斜",
            "前倾斜",
            "后倾斜"});
            this.cbxPosture.Location = new System.Drawing.Point(79, 25);
            this.cbxPosture.Name = "cbxPosture";
            this.cbxPosture.Size = new System.Drawing.Size(87, 22);
            this.cbxPosture.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 14);
            this.label2.TabIndex = 89;
            this.label2.Text = "方位选择:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(91, 211);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 94;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DispenseLineCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DispenseLineCtl";
            this.Size = new System.Drawing.Size(205, 247);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChangePosture;
        private Controls.DoubleTextBox txtAngle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxPosture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
    }
}
