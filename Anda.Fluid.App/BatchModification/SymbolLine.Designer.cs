namespace Anda.Fluid.App.BatchModification
{
    partial class SymbolLine
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
            this.tbTransitionR = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMinTolerance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxTolerance = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // tbTransitionR
            // 
            this.tbTransitionR.BackColor = System.Drawing.Color.White;
            this.tbTransitionR.Location = new System.Drawing.Point(141, 34);
            this.tbTransitionR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbTransitionR.Name = "tbTransitionR";
            this.tbTransitionR.Size = new System.Drawing.Size(95, 22);
            this.tbTransitionR.TabIndex = 77;
            this.tbTransitionR.Text = "2.000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 14);
            this.label3.TabIndex = 76;
            this.label3.Text = "过渡半径(mm):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 81;
            this.label1.Text = "Tolerance-:";
            // 
            // nudMinTolerance
            // 
            this.nudMinTolerance.Location = new System.Drawing.Point(141, 98);
            this.nudMinTolerance.Name = "nudMinTolerance";
            this.nudMinTolerance.Size = new System.Drawing.Size(84, 22);
            this.nudMinTolerance.TabIndex = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 14);
            this.label2.TabIndex = 79;
            this.label2.Text = "Tolerance+:";
            // 
            // nudMaxTolerance
            // 
            this.nudMaxTolerance.Location = new System.Drawing.Point(141, 70);
            this.nudMaxTolerance.Name = "nudMaxTolerance";
            this.nudMaxTolerance.Size = new System.Drawing.Size(84, 22);
            this.nudMaxTolerance.TabIndex = 78;
            // 
            // SymbolLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudMinTolerance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudMaxTolerance);
            this.Controls.Add(this.tbTransitionR);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SymbolLine";
            this.Size = new System.Drawing.Size(273, 150);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.DoubleTextBox tbTransitionR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMinTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxTolerance;
    }
}
