namespace Anda.Fluid.Domain.Vision
{
    partial class CameraForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.SuspendLayout();
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(0, 0);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(519, 421);
            this.cameraControl1.TabIndex = 0;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 421);
            this.Controls.Add(this.cameraControl1);
            this.Name = "CameraForm";
            this.Text = "FormCamera";
            this.Load += new System.EventHandler(this.CameraForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CameraControl cameraControl1;
    }
}

