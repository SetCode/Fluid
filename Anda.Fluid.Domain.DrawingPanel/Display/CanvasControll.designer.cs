namespace DrawingPanel.Display
{
    partial class CanvasControll
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtTrackWidth = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tsbBackGroundLeftTopColor = new System.Windows.Forms.ToolStripButton();
            this.tsbBackGroundRightBottomColor = new System.Windows.Forms.ToolStripButton();
            this.tsbGridEnable = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectBoxColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmTrackNormalColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTrackSelectedColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTrackDisableColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTrackClickColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNormalMarkColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBadMarkColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCheckDotColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHeightColor = new System.Windows.Forms.ToolStripMenuItem();
            this.canvasDisplay1 = new DrawingPanel.Display.CanvasDisplay();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBackGroundLeftTopColor,
            this.tsbBackGroundRightBottomColor,
            this.tsbGridEnable,
            this.tsbSelectBoxColor,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripComboBox1,
            this.toolStripLabel1,
            this.tstxtTrackWidth,
            this.toolStripLabel3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.toolStripSeparator6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(454, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "网格密度";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "150",
            "200"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "轨迹宽度";
            // 
            // tstxtTrackWidth
            // 
            this.tstxtTrackWidth.Name = "tstxtTrackWidth";
            this.tstxtTrackWidth.Size = new System.Drawing.Size(50, 25);
            this.tstxtTrackWidth.TextChanged += new System.EventHandler(this.tstxtTrackWidth_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel3.Text = "轨迹颜色";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(661, 28);
            this.vScrollBar1.Maximum = 0;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 354);
            this.vScrollBar1.TabIndex = 2;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 382);
            this.hScrollBar1.Maximum = 0;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(661, 18);
            this.hScrollBar1.TabIndex = 3;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // tsbBackGroundLeftTopColor
            // 
            this.tsbBackGroundLeftTopColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBackGroundLeftTopColor.Image = global::DrawingPanel.Properties.Resources.TopLeftColor;
            this.tsbBackGroundLeftTopColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackGroundLeftTopColor.Name = "tsbBackGroundLeftTopColor";
            this.tsbBackGroundLeftTopColor.Size = new System.Drawing.Size(23, 22);
            this.tsbBackGroundLeftTopColor.Text = "背景左侧颜色";
            this.tsbBackGroundLeftTopColor.Click += new System.EventHandler(this.tsbBackGroundLeftTopColor_Click);
            // 
            // tsbBackGroundRightBottomColor
            // 
            this.tsbBackGroundRightBottomColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBackGroundRightBottomColor.Image = global::DrawingPanel.Properties.Resources.RightBottomColor;
            this.tsbBackGroundRightBottomColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackGroundRightBottomColor.Name = "tsbBackGroundRightBottomColor";
            this.tsbBackGroundRightBottomColor.Size = new System.Drawing.Size(23, 22);
            this.tsbBackGroundRightBottomColor.Text = "背景右侧颜色";
            this.tsbBackGroundRightBottomColor.Click += new System.EventHandler(this.tsbBackGroundRightBottomColor_Click);
            // 
            // tsbGridEnable
            // 
            this.tsbGridEnable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGridEnable.Image = global::DrawingPanel.Properties.Resources.Prison_48px;
            this.tsbGridEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGridEnable.Name = "tsbGridEnable";
            this.tsbGridEnable.Size = new System.Drawing.Size(23, 22);
            this.tsbGridEnable.Text = "背景网格";
            this.tsbGridEnable.Click += new System.EventHandler(this.tsbGridEnable_Click);
            // 
            // tsbSelectBoxColor
            // 
            this.tsbSelectBoxColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelectBoxColor.Image = global::DrawingPanel.Properties.Resources.Select_All_50px;
            this.tsbSelectBoxColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectBoxColor.Name = "tsbSelectBoxColor";
            this.tsbSelectBoxColor.Size = new System.Drawing.Size(23, 22);
            this.tsbSelectBoxColor.Text = "拉选框颜色";
            this.tsbSelectBoxColor.Click += new System.EventHandler(this.tsbSelectBoxColor_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmTrackNormalColor,
            this.tsmTrackSelectedColor,
            this.tsmTrackDisableColor,
            this.tsmTrackClickColor,
            this.tsmNormalMarkColor,
            this.tsmBadMarkColor,
            this.tsmCheckDotColor,
            this.tsmHeightColor});
            this.toolStripDropDownButton1.Image = global::DrawingPanel.Properties.Resources.iOS_Photos_48px;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "轨迹颜色";
            // 
            // tsmTrackNormalColor
            // 
            this.tsmTrackNormalColor.Name = "tsmTrackNormalColor";
            this.tsmTrackNormalColor.Size = new System.Drawing.Size(158, 22);
            this.tsmTrackNormalColor.Text = "普通状态颜色";
            this.tsmTrackNormalColor.Click += new System.EventHandler(this.tsmTrackNormalColor_Click);
            // 
            // tsmTrackSelectedColor
            // 
            this.tsmTrackSelectedColor.Name = "tsmTrackSelectedColor";
            this.tsmTrackSelectedColor.Size = new System.Drawing.Size(158, 22);
            this.tsmTrackSelectedColor.Text = "选中状态颜色";
            this.tsmTrackSelectedColor.Click += new System.EventHandler(this.tsmTrackSelectedColor_Click);
            // 
            // tsmTrackDisableColor
            // 
            this.tsmTrackDisableColor.Name = "tsmTrackDisableColor";
            this.tsmTrackDisableColor.Size = new System.Drawing.Size(158, 22);
            this.tsmTrackDisableColor.Text = "屏蔽状态颜色";
            this.tsmTrackDisableColor.Click += new System.EventHandler(this.tsmTrackDisableColor_Click);
            // 
            // tsmTrackClickColor
            // 
            this.tsmTrackClickColor.Name = "tsmTrackClickColor";
            this.tsmTrackClickColor.Size = new System.Drawing.Size(158, 22);
            this.tsmTrackClickColor.Text = "点击状态颜色";
            this.tsmTrackClickColor.Click += new System.EventHandler(this.tsmTrackClickColor_Click);
            // 
            // tsmNormalMarkColor
            // 
            this.tsmNormalMarkColor.Name = "tsmNormalMarkColor";
            this.tsmNormalMarkColor.Size = new System.Drawing.Size(158, 22);
            this.tsmNormalMarkColor.Text = "普通Mark颜色";
            this.tsmNormalMarkColor.Click += new System.EventHandler(this.tsmAsvMarkColor_Click);
            // 
            // tsmBadMarkColor
            // 
            this.tsmBadMarkColor.Name = "tsmBadMarkColor";
            this.tsmBadMarkColor.Size = new System.Drawing.Size(158, 22);
            this.tsmBadMarkColor.Text = "Bad Mark颜色";
            this.tsmBadMarkColor.Click += new System.EventHandler(this.tsmBadMarkColor_Click);
            // 
            // tsmCheckDotColor
            // 
            this.tsmCheckDotColor.Name = "tsmCheckDotColor";
            this.tsmCheckDotColor.Size = new System.Drawing.Size(158, 22);
            this.tsmCheckDotColor.Text = "检查点颜色";
            this.tsmCheckDotColor.Click += new System.EventHandler(this.tsmCheckDotColor_Click);
            // 
            // tsmHeightColor
            // 
            this.tsmHeightColor.Name = "tsmHeightColor";
            this.tsmHeightColor.Size = new System.Drawing.Size(158, 22);
            this.tsmHeightColor.Text = "测高指令颜色";
            this.tsmHeightColor.Click += new System.EventHandler(this.tsmHeightColor_Click);
            // 
            // canvasDisplay1
            // 
            this.canvasDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasDisplay1.BackColor = System.Drawing.SystemColors.Info;
            this.canvasDisplay1.Location = new System.Drawing.Point(0, 28);
            this.canvasDisplay1.Name = "canvasDisplay1";
            this.canvasDisplay1.OnlyLook = false;
            this.canvasDisplay1.OuterScale = 1F;
            this.canvasDisplay1.Size = new System.Drawing.Size(661, 354);
            this.canvasDisplay1.TabIndex = 0;
            this.canvasDisplay1.XTransDis = 0F;
            this.canvasDisplay1.YTransDis = 0F;
            this.canvasDisplay1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasDisplay1_MouseDown);
            this.canvasDisplay1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasDisplay1_MouseMove);
            this.canvasDisplay1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasDisplay1_MouseUp);
            // 
            // CanvasControll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.canvasDisplay1);
            this.Name = "CanvasControll";
            this.Size = new System.Drawing.Size(681, 401);
            this.Leave += new System.EventHandler(this.CanvasControll_Leave);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CanvasDisplay canvasDisplay1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBackGroundLeftTopColor;
        private System.Windows.Forms.ToolStripButton tsbBackGroundRightBottomColor;
        private System.Windows.Forms.ToolStripButton tsbGridEnable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbSelectBoxColor;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmTrackNormalColor;
        private System.Windows.Forms.ToolStripMenuItem tsmTrackSelectedColor;
        private System.Windows.Forms.ToolStripMenuItem tsmTrackDisableColor;
        private System.Windows.Forms.ToolStripTextBox tstxtTrackWidth;
        private System.Windows.Forms.ToolStripMenuItem tsmTrackClickColor;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmNormalMarkColor;
        private System.Windows.Forms.ToolStripMenuItem tsmBadMarkColor;
        private System.Windows.Forms.ToolStripMenuItem tsmCheckDotColor;
        private System.Windows.Forms.ToolStripMenuItem tsmHeightColor;
    }
}
