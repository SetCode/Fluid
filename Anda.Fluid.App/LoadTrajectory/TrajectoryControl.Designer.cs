namespace Anda.Fluid.App.LoadTrajectory
{
    partial class TrajectoryControl
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
            this.SuspendLayout();
            // 
            // TrajectoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.Name = "TrajectoryControl";
            this.Size = new System.Drawing.Size(510, 351);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TrajectoryControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrajectoryControl_MouseDown);
            this.MouseEnter += new System.EventHandler(this.TrajectoryControl_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrajectoryControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrajectoryControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
