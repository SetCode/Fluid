namespace Anda.Fluid.App.Metro.Pages
{
    partial class PageAlarmsLog
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
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.alarmControl1 = new Anda.Fluid.Infrastructure.Alarming.AlarmControl();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // alarmControl1
            // 
            this.alarmControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmControl1.ForeColor = System.Drawing.Color.Black;
            this.alarmControl1.Location = new System.Drawing.Point(12, 12);
            this.alarmControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.alarmControl1.Name = "alarmControl1";
            this.alarmControl1.Size = new System.Drawing.Size(576, 576);
            this.alarmControl1.TabIndex = 0;
            // 
            // PageAlarmsLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.alarmControl1);
            this.Name = "PageAlarmsLog";
            this.Size = new System.Drawing.Size(600, 600);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private Infrastructure.Alarming.AlarmControl alarmControl1;
    }
}
