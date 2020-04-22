namespace Anda.Fluid.App.Main
{
    partial class NaviBtnAlarms
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
            this.btnAlarms = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAlarms
            // 
            this.btnAlarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAlarms.Image = global::Anda.Fluid.App.Main.Properties.Resources.Error_30px;
            this.btnAlarms.Location = new System.Drawing.Point(0, 0);
            this.btnAlarms.Name = "btnAlarms";
            this.btnAlarms.Size = new System.Drawing.Size(75, 50);
            this.btnAlarms.TabIndex = 17;
            this.btnAlarms.UseVisualStyleBackColor = true;
            // 
            // NaviBtnAlarms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAlarms);
            this.Name = "NaviBtnAlarms";
            this.Size = new System.Drawing.Size(75, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAlarms;
    }
}
