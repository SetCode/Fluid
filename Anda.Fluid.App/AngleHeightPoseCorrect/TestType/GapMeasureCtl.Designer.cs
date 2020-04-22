namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    partial class GapMeasureCtl
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
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.nudVelocity = new System.Windows.Forms.NumericUpDown();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.nudCycles = new System.Windows.Forms.NumericUpDown();
            this.lblCycles = new System.Windows.Forms.Label();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.lblZOffsetResult = new System.Windows.Forms.Label();
            this.txtGap = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.btnPress = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStandardZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycles)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudTolerance
            // 
            this.nudTolerance.DecimalPlaces = 3;
            this.nudTolerance.Location = new System.Drawing.Point(116, 78);
            this.nudTolerance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(88, 22);
            this.nudTolerance.TabIndex = 29;
            this.nudTolerance.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // nudVelocity
            // 
            this.nudVelocity.DecimalPlaces = 6;
            this.nudVelocity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudVelocity.Location = new System.Drawing.Point(116, 48);
            this.nudVelocity.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudVelocity.Name = "nudVelocity";
            this.nudVelocity.Size = new System.Drawing.Size(88, 22);
            this.nudVelocity.TabIndex = 28;
            this.nudVelocity.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblVelocity
            // 
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.Location = new System.Drawing.Point(8, 52);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(64, 14);
            this.lblVelocity.TabIndex = 27;
            this.lblVelocity.Text = "下压速度:";
            // 
            // nudCycles
            // 
            this.nudCycles.Location = new System.Drawing.Point(116, 19);
            this.nudCycles.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCycles.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudCycles.Name = "nudCycles";
            this.nudCycles.Size = new System.Drawing.Size(88, 22);
            this.nudCycles.TabIndex = 26;
            this.nudCycles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblCycles
            // 
            this.lblCycles.AutoSize = true;
            this.lblCycles.Location = new System.Drawing.Point(6, 23);
            this.lblCycles.Name = "lblCycles";
            this.lblCycles.Size = new System.Drawing.Size(64, 14);
            this.lblCycles.TabIndex = 22;
            this.lblCycles.Text = "下压次数:";
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(8, 82);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(66, 14);
            this.lblTolerance.TabIndex = 23;
            this.lblTolerance.Text = "容差 +/-:";
            // 
            // lblZOffsetResult
            // 
            this.lblZOffsetResult.AutoSize = true;
            this.lblZOffsetResult.Location = new System.Drawing.Point(6, 143);
            this.lblZOffsetResult.Name = "lblZOffsetResult";
            this.lblZOffsetResult.Size = new System.Drawing.Size(64, 14);
            this.lblZOffsetResult.TabIndex = 24;
            this.lblZOffsetResult.Text = "距板高度:";
            // 
            // txtGap
            // 
            this.txtGap.Location = new System.Drawing.Point(115, 139);
            this.txtGap.Name = "txtGap";
            this.txtGap.Size = new System.Drawing.Size(89, 22);
            this.txtGap.TabIndex = 25;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(11, 168);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(193, 63);
            this.txtMsg.TabIndex = 30;
            this.txtMsg.Text = "";
            // 
            // btnPress
            // 
            this.btnPress.Location = new System.Drawing.Point(11, 237);
            this.btnPress.Name = "btnPress";
            this.btnPress.Size = new System.Drawing.Size(76, 24);
            this.btnPress.TabIndex = 32;
            this.btnPress.Text = "执行下压";
            this.btnPress.UseVisualStyleBackColor = true;
            this.btnPress.Click += new System.EventHandler(this.btnPress_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(11, 276);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 33;
            this.btnReset.Text = "重做";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStandardZ);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCycles);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.txtGap);
            this.groupBox1.Controls.Add(this.btnPress);
            this.groupBox1.Controls.Add(this.lblZOffsetResult);
            this.groupBox1.Controls.Add(this.lblTolerance);
            this.groupBox1.Controls.Add(this.txtMsg);
            this.groupBox1.Controls.Add(this.nudCycles);
            this.groupBox1.Controls.Add(this.nudTolerance);
            this.groupBox1.Controls.Add(this.lblVelocity);
            this.groupBox1.Controls.Add(this.nudVelocity);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 305);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第三阶段";
            // 
            // txtStandardZ
            // 
            this.txtStandardZ.Location = new System.Drawing.Point(115, 109);
            this.txtStandardZ.Name = "txtStandardZ";
            this.txtStandardZ.Size = new System.Drawing.Size(89, 22);
            this.txtStandardZ.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 34;
            this.label1.Text = "标准高度:";
            // 
            // GapMeasureCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "GapMeasureCtl";
            this.Size = new System.Drawing.Size(219, 311);
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycles)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.NumericUpDown nudVelocity;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.NumericUpDown nudCycles;
        private System.Windows.Forms.Label lblCycles;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Label lblZOffsetResult;
        private System.Windows.Forms.TextBox txtGap;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Button btnPress;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtStandardZ;
        private System.Windows.Forms.Label label1;
    }
}
