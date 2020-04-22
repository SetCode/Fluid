namespace DrawingPanel
{
    partial class DrawingPanelControl
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
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(3, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 26);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "图像回原点";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // DrawingPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.Controls.Add(this.btnReset);
            this.Name = "DrawingPanelControl";
            this.Size = new System.Drawing.Size(611, 464);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingPanelControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseDown);
            this.MouseEnter += new System.EventHandler(this.drawingPanel_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
    }
}
