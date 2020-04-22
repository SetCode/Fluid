namespace Anda.Fluid.App.Main
{
    partial class NavigateRun
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
            this.naviBtnInitItems1 = new Anda.Fluid.App.Main.NaviBtnInitItems();
            this.naviBtnInitAll1 = new Anda.Fluid.App.Main.NaviBtnInitAll();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // naviBtnInitItems1
            // 
            this.naviBtnInitItems1.Location = new System.Drawing.Point(3, 59);
            this.naviBtnInitItems1.Name = "naviBtnInitItems1";
            this.naviBtnInitItems1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnInitItems1.TabIndex = 48;
            // 
            // naviBtnInitAll1
            // 
            this.naviBtnInitAll1.Location = new System.Drawing.Point(3, 3);
            this.naviBtnInitAll1.Name = "naviBtnInitAll1";
            this.naviBtnInitAll1.Size = new System.Drawing.Size(75, 50);
            this.naviBtnInitAll1.TabIndex = 47;
            // 
            // btnAbort
            // 
            this.btnAbort.Image = global::Anda.Fluid.App.Main.Properties.Resources.Cancel_30px;
            this.btnAbort.Location = new System.Drawing.Point(3, 339);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 50);
            this.btnAbort.TabIndex = 46;
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnStep
            // 
            this.btnStep.Image = global::Anda.Fluid.App.Main.Properties.Resources.Stairs_30px;
            this.btnStep.Location = new System.Drawing.Point(3, 227);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 50);
            this.btnStep.TabIndex = 45;
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::Anda.Fluid.App.Main.Properties.Resources.Stop_30px;
            this.btnStop.Location = new System.Drawing.Point(3, 395);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 50);
            this.btnStop.TabIndex = 44;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = global::Anda.Fluid.App.Main.Properties.Resources.Pause_30px;
            this.btnPause.Location = new System.Drawing.Point(3, 283);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 50);
            this.btnPause.TabIndex = 43;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::Anda.Fluid.App.Main.Properties.Resources.Play_30px;
            this.btnStart.Location = new System.Drawing.Point(3, 171);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 50);
            this.btnStart.TabIndex = 42;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // NavigateRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.naviBtnInitItems1);
            this.Controls.Add(this.naviBtnInitAll1);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Name = "NavigateRun";
            this.Size = new System.Drawing.Size(80, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStart;
        private NaviBtnInitAll naviBtnInitAll1;
        private NaviBtnInitItems naviBtnInitItems1;
    }
}
