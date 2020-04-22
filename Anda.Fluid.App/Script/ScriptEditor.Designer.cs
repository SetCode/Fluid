namespace Anda.Fluid.App.Script
{
    partial class ScriptEditor
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmsDisableOperation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsrPreDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrBackDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrAllEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrOthersDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrThisDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new Anda.Fluid.App.Script.ScrollListView();
            this.cmsDisableOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 323);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmsDisableOperation
            // 
            this.cmsDisableOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsrPreDisable,
            this.tsrBackDisable,
            this.tsrAllEnable,
            this.tsrOthersDisable,
            this.tsrThisDisable,
            this.tsiReverse});
            this.cmsDisableOperation.Name = "cmsDisableOperation";
            this.cmsDisableOperation.Size = new System.Drawing.Size(149, 136);
            // 
            // tsrPreDisable
            // 
            this.tsrPreDisable.Name = "tsrPreDisable";
            this.tsrPreDisable.Size = new System.Drawing.Size(148, 22);
            this.tsrPreDisable.Text = "屏蔽上方指令";
            this.tsrPreDisable.Click += new System.EventHandler(this.tsrPreDisable_Click);
            // 
            // tsrBackDisable
            // 
            this.tsrBackDisable.Name = "tsrBackDisable";
            this.tsrBackDisable.Size = new System.Drawing.Size(148, 22);
            this.tsrBackDisable.Text = "屏蔽后续指令";
            this.tsrBackDisable.Click += new System.EventHandler(this.tsrBackDisable_Click);
            // 
            // tsrAllEnable
            // 
            this.tsrAllEnable.Name = "tsrAllEnable";
            this.tsrAllEnable.Size = new System.Drawing.Size(148, 22);
            this.tsrAllEnable.Text = "激活所有指令";
            this.tsrAllEnable.Click += new System.EventHandler(this.tsrAllEnable_Click);
            // 
            // tsrOthersDisable
            // 
            this.tsrOthersDisable.Name = "tsrOthersDisable";
            this.tsrOthersDisable.Size = new System.Drawing.Size(148, 22);
            this.tsrOthersDisable.Text = "屏蔽其余指令";
            this.tsrOthersDisable.Click += new System.EventHandler(this.tsrOthersDisable_Click);
            // 
            // tsrThisDisable
            // 
            this.tsrThisDisable.Name = "tsrThisDisable";
            this.tsrThisDisable.Size = new System.Drawing.Size(148, 22);
            this.tsrThisDisable.Text = "屏蔽当前指令";
            this.tsrThisDisable.Click += new System.EventHandler(this.tsrThisDisable_Click);
            // 
            // tsiReverse
            // 
            this.tsiReverse.Name = "tsiReverse";
            this.tsiReverse.Size = new System.Drawing.Size(148, 22);
            this.tsiReverse.Text = "逆序选中轨迹";
            this.tsiReverse.Click += new System.EventHandler(this.tsiReverse_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(20, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(323, 323);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Name = "ScriptEditor";
            this.Size = new System.Drawing.Size(343, 323);
            this.cmsDisableOperation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ScrollListView listView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip cmsDisableOperation;
        private System.Windows.Forms.ToolStripMenuItem tsrPreDisable;
        private System.Windows.Forms.ToolStripMenuItem tsrBackDisable;
        private System.Windows.Forms.ToolStripMenuItem tsrAllEnable;
        private System.Windows.Forms.ToolStripMenuItem tsrOthersDisable;
        private System.Windows.Forms.ToolStripMenuItem tsrThisDisable;
        private System.Windows.Forms.ToolStripMenuItem tsiReverse;
    }
}
