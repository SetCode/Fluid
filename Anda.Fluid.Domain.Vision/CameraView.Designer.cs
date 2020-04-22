namespace Anda.Fluid.Domain.Vision
{
    partial class CameraView
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
            this.picCamera = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCamera.Location = new System.Drawing.Point(0, 0);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(569, 441);
            this.picCamera.TabIndex = 0;
            this.picCamera.TabStop = false;
            // 
            // CameraView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picCamera);
            this.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CameraView";
            this.Size = new System.Drawing.Size(569, 441);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCamera;
    }
}
