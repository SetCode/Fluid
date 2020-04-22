namespace Anda.Fluid.App.Main
{
    partial class NaviBtnConfig
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
            this.btnConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConfig
            // 
            this.btnConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfig.Image = global::Anda.Fluid.App.Main.Properties.Resources.Settings_30px;
            this.btnConfig.Location = new System.Drawing.Point(0, 0);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 50);
            this.btnConfig.TabIndex = 16;
            this.btnConfig.UseVisualStyleBackColor = true;
            // 
            // NaviBtnConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfig);
            this.Name = "NaviBtnConfig";
            this.Size = new System.Drawing.Size(75, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfig;
    }
}
