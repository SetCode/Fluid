namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class HeightControl
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
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoardHeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMinTolerance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.laserControl1 = new Anda.Fluid.Domain.Dialogs.LaserControl();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(126, 47);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(84, 23);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnApply);
            this.groupBox2.Controls.Add(this.txtBoardHeight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nudMinTolerance);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudMaxTolerance);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(217, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 133);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Board";
            // 
            // txtBoardHeight
            // 
            this.txtBoardHeight.BackColor = System.Drawing.Color.White;
            this.txtBoardHeight.Location = new System.Drawing.Point(126, 22);
            this.txtBoardHeight.Name = "txtBoardHeight";
            this.txtBoardHeight.Size = new System.Drawing.Size(84, 22);
            this.txtBoardHeight.TabIndex = 6;
            this.txtBoardHeight.Text = "0.000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tolerance-";
            // 
            // nudMinTolerance
            // 
            this.nudMinTolerance.Location = new System.Drawing.Point(126, 102);
            this.nudMinTolerance.Name = "nudMinTolerance";
            this.nudMinTolerance.Size = new System.Drawing.Size(84, 22);
            this.nudMinTolerance.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tolerance+";
            // 
            // nudMaxTolerance
            // 
            this.nudMaxTolerance.Location = new System.Drawing.Point(126, 74);
            this.nudMaxTolerance.Name = "nudMaxTolerance";
            this.nudMaxTolerance.Size = new System.Drawing.Size(84, 22);
            this.nudMaxTolerance.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Standard Height";
            // 
            // laserControl1
            // 
            this.laserControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laserControl1.Location = new System.Drawing.Point(4, 3);
            this.laserControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.laserControl1.Name = "laserControl1";
            this.laserControl1.Size = new System.Drawing.Size(213, 118);
            this.laserControl1.TabIndex = 15;
            // 
            // HeightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.laserControl1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "HeightControl";
            this.Size = new System.Drawing.Size(441, 144);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTolerance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMinTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxTolerance;
        private System.Windows.Forms.Label label1;
        private Controls.DoubleTextBox txtBoardHeight;
        private System.Windows.Forms.Button btnApply;
        private Domain.Dialogs.LaserControl laserControl1;
    }
}
