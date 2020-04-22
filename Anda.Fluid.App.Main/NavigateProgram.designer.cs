namespace Anda.Fluid.App.Main
{
    partial class NavigateProgram
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
            this.naviBtnLoc1 = new Anda.Fluid.App.Main.NaviBtnLoc();
            this.naviBtnAdvanced1 = new Anda.Fluid.App.Main.NaviBtnSetup();
            this.naviBtnConfig1 = new Anda.Fluid.App.Main.NaviBtnConfig();
            this.naviBtnTools1 = new Anda.Fluid.App.Main.NaviBtnTools();
            this.naviBtnVision1 = new Anda.Fluid.App.Main.NaviBtnVision();
            this.naviBtnJog1 = new Anda.Fluid.App.Main.NaviBtnJog();
            this.naviBtnAlarms1 = new Anda.Fluid.App.Main.NaviBtnAlarms();
            this.btnMain = new System.Windows.Forms.Button();
            this.naviBtnLogin1 = new Anda.Fluid.App.Main.NaviBtnLogin();
            this.SuspendLayout();
            // 
            // naviBtnLoc1
            // 
            this.naviBtnLoc1.Location = new System.Drawing.Point(3, 171);
            this.naviBtnLoc1.Name = "naviBtnLoc1";
            this.naviBtnLoc1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnLoc1.TabIndex = 39;
            // 
            // naviBtnAdvanced1
            // 
            this.naviBtnAdvanced1.Location = new System.Drawing.Point(3, 339);
            this.naviBtnAdvanced1.Name = "naviBtnAdvanced1";
            this.naviBtnAdvanced1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnAdvanced1.TabIndex = 38;
            // 
            // naviBtnConfig1
            // 
            this.naviBtnConfig1.Location = new System.Drawing.Point(3, 283);
            this.naviBtnConfig1.Name = "naviBtnConfig1";
            this.naviBtnConfig1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnConfig1.TabIndex = 37;
            // 
            // naviBtnTools1
            // 
            this.naviBtnTools1.Location = new System.Drawing.Point(3, 227);
            this.naviBtnTools1.Name = "naviBtnTools1";
            this.naviBtnTools1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnTools1.TabIndex = 36;
            // 
            // naviBtnVision1
            // 
            this.naviBtnVision1.Location = new System.Drawing.Point(3, 59);
            this.naviBtnVision1.Name = "naviBtnVision1";
            this.naviBtnVision1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnVision1.TabIndex = 35;
            // 
            // naviBtnJog1
            // 
            this.naviBtnJog1.Location = new System.Drawing.Point(3, 3);
            this.naviBtnJog1.Name = "naviBtnJog1";
            this.naviBtnJog1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnJog1.TabIndex = 34;
            // 
            // naviBtnAlarms1
            // 
            this.naviBtnAlarms1.Location = new System.Drawing.Point(3, 395);
            this.naviBtnAlarms1.Name = "naviBtnAlarms1";
            this.naviBtnAlarms1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnAlarms1.TabIndex = 40;
            // 
            // btnMain
            // 
            this.btnMain.Image = global::Anda.Fluid.App.Main.Properties.Resources.Return_30px;
            this.btnMain.Location = new System.Drawing.Point(2, 451);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(75, 50);
            this.btnMain.TabIndex = 31;
            this.btnMain.UseVisualStyleBackColor = true;
            // 
            // naviBtnLogin1
            // 
            this.naviBtnLogin1.Location = new System.Drawing.Point(3, 115);
            this.naviBtnLogin1.Name = "naviBtnLogin1";
            this.naviBtnLogin1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnLogin1.TabIndex = 41;
            // 
            // NavigateProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.naviBtnLogin1);
            this.Controls.Add(this.naviBtnAlarms1);
            this.Controls.Add(this.naviBtnLoc1);
            this.Controls.Add(this.naviBtnAdvanced1);
            this.Controls.Add(this.naviBtnConfig1);
            this.Controls.Add(this.naviBtnTools1);
            this.Controls.Add(this.naviBtnVision1);
            this.Controls.Add(this.naviBtnJog1);
            this.Controls.Add(this.btnMain);
            this.Name = "NavigateProgram";
            this.Size = new System.Drawing.Size(81, 506);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnMain;
        private NaviBtnJog naviBtnJog1;
        private NaviBtnVision naviBtnVision1;
        private NaviBtnTools naviBtnTools1;
        private NaviBtnConfig naviBtnConfig1;
        private NaviBtnSetup naviBtnAdvanced1;
        private NaviBtnLoc naviBtnLoc1;
        private NaviBtnAlarms naviBtnAlarms1;
        private NaviBtnLogin naviBtnLogin1;
    }
}
