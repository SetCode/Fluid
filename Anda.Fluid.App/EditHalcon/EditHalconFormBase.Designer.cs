namespace Anda.Fluid.App.EditHalcon
{
    partial class EditHalconFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFileImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClearAllROI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRect1 = new System.Windows.Forms.ToolStripButton();
            this.btnRect2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRealTimeImage = new System.Windows.Forms.TabPage();
            this.tabCurrent = new System.Windows.Forms.TabPage();
            this.tabReference = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSettlingTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chxFixedROI = new System.Windows.Forms.CheckBox();
            this.btnRegistImage = new System.Windows.Forms.Button();
            this.andaLightControl1 = new Anda.Fluid.Domain.Vision.AndaLightControl();
            this.cameraView1 = new Anda.Fluid.Domain.Vision.CameraView();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabRealTimeImage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFileImage,
            this.toolStripSeparator1,
            this.btnZoomReset,
            this.toolStripSeparator4,
            this.btnClearAllROI,
            this.toolStripSeparator2,
            this.btnRect1,
            this.btnRect2,
            this.toolStripSeparator3,
            this.btnExecute,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 37);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFileImage
            // 
            this.btnFileImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileImage.Image = global::Anda.Fluid.App.Properties.Resources.Open_30px;
            this.btnFileImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFileImage.Name = "btnFileImage";
            this.btnFileImage.Size = new System.Drawing.Size(34, 34);
            this.btnFileImage.Text = "打开";
            this.btnFileImage.Click += new System.EventHandler(this.btnFileImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // btnZoomReset
            // 
            this.btnZoomReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomReset.Image = global::Anda.Fluid.App.Properties.Resources.Fit_to_Width_30px;
            this.btnZoomReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.Size = new System.Drawing.Size(34, 34);
            this.btnZoomReset.Text = "适应窗口";
            this.btnZoomReset.Click += new System.EventHandler(this.btnZoomReset_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
            // 
            // btnClearAllROI
            // 
            this.btnClearAllROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearAllROI.Image = global::Anda.Fluid.App.Properties.Resources.Trash_Can_30px;
            this.btnClearAllROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearAllROI.Name = "btnClearAllROI";
            this.btnClearAllROI.Size = new System.Drawing.Size(34, 34);
            this.btnClearAllROI.Text = "清空ROI";
            this.btnClearAllROI.Click += new System.EventHandler(this.btnClearAllROI_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
            // 
            // btnRect1
            // 
            this.btnRect1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRect1.Image = global::Anda.Fluid.App.Properties.Resources.Rectangle_Stroked_30px;
            this.btnRect1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRect1.Name = "btnRect1";
            this.btnRect1.Size = new System.Drawing.Size(34, 34);
            this.btnRect1.Text = "矩形1";
            this.btnRect1.Click += new System.EventHandler(this.btnRect1_Click);
            // 
            // btnRect2
            // 
            this.btnRect2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRect2.Image = global::Anda.Fluid.App.Properties.Resources.Rotate_to_Portrait_30px;
            this.btnRect2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRect2.Name = "btnRect2";
            this.btnRect2.Size = new System.Drawing.Size(34, 34);
            this.btnRect2.Text = "矩形2";
            this.btnRect2.Click += new System.EventHandler(this.btnRect2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 37);
            // 
            // btnExecute
            // 
            this.btnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecute.Image = global::Anda.Fluid.App.Properties.Resources.Play_30px;
            this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(34, 34);
            this.btnExecute.Text = "执行";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 37);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabRealTimeImage);
            this.tabControl1.Controls.Add(this.tabCurrent);
            this.tabControl1.Controls.Add(this.tabReference);
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 448);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabRealTimeImage
            // 
            this.tabRealTimeImage.Controls.Add(this.cameraView1);
            this.tabRealTimeImage.Location = new System.Drawing.Point(4, 26);
            this.tabRealTimeImage.Name = "tabRealTimeImage";
            this.tabRealTimeImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabRealTimeImage.Size = new System.Drawing.Size(462, 418);
            this.tabRealTimeImage.TabIndex = 2;
            this.tabRealTimeImage.Text = "相机";
            this.tabRealTimeImage.UseVisualStyleBackColor = true;
            // 
            // tabCurrent
            // 
            this.tabCurrent.Location = new System.Drawing.Point(4, 26);
            this.tabCurrent.Name = "tabCurrent";
            this.tabCurrent.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrent.Size = new System.Drawing.Size(462, 418);
            this.tabCurrent.TabIndex = 0;
            this.tabCurrent.Text = "当前图像";
            this.tabCurrent.UseVisualStyleBackColor = true;
            // 
            // tabReference
            // 
            this.tabReference.Location = new System.Drawing.Point(4, 26);
            this.tabReference.Name = "tabReference";
            this.tabReference.Padding = new System.Windows.Forms.Padding(3);
            this.tabReference.Size = new System.Drawing.Size(462, 418);
            this.tabReference.TabIndex = 1;
            this.tabReference.Text = "参考图像";
            this.tabReference.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(476, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 610);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnIgnore);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudSettlingTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chxFixedROI);
            this.groupBox2.Controls.Add(this.btnRegistImage);
            this.groupBox2.Location = new System.Drawing.Point(4, 484);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 166);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(292, 120);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(81, 30);
            this.btnIgnore.TabIndex = 7;
            this.btnIgnore.Text = "跳过";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(205, 120);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(81, 30);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(379, 120);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 30);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "ms";
            // 
            // nudSettlingTime
            // 
            this.nudSettlingTime.Location = new System.Drawing.Point(97, 68);
            this.nudSettlingTime.Name = "nudSettlingTime";
            this.nudSettlingTime.Size = new System.Drawing.Size(120, 25);
            this.nudSettlingTime.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "拍照前等待";
            // 
            // chxFixedROI
            // 
            this.chxFixedROI.AutoSize = true;
            this.chxFixedROI.Location = new System.Drawing.Point(158, 30);
            this.chxFixedROI.Name = "chxFixedROI";
            this.chxFixedROI.Size = new System.Drawing.Size(88, 21);
            this.chxFixedROI.TabIndex = 1;
            this.chxFixedROI.Text = "锁定ROI";
            this.chxFixedROI.UseVisualStyleBackColor = true;
            this.chxFixedROI.CheckedChanged += new System.EventHandler(this.chxFixedROI_CheckedChanged);
            // 
            // btnRegistImage
            // 
            this.btnRegistImage.Location = new System.Drawing.Point(8, 24);
            this.btnRegistImage.Name = "btnRegistImage";
            this.btnRegistImage.Size = new System.Drawing.Size(120, 30);
            this.btnRegistImage.TabIndex = 0;
            this.btnRegistImage.Text = "注册参考图像";
            this.btnRegistImage.UseVisualStyleBackColor = true;
            this.btnRegistImage.Click += new System.EventHandler(this.btnRegistImage_Click);
            // 
            // andaLightControl1
            // 
            this.andaLightControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.andaLightControl1.Location = new System.Drawing.Point(254, 5);
            this.andaLightControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.andaLightControl1.Name = "andaLightControl1";
            this.andaLightControl1.Size = new System.Drawing.Size(442, 26);
            this.andaLightControl1.TabIndex = 4;
            // 
            // cameraView1
            // 
            this.cameraView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraView1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraView1.Location = new System.Drawing.Point(3, 3);
            this.cameraView1.Name = "cameraView1";
            this.cameraView1.ShapeType = 0;
            this.cameraView1.Size = new System.Drawing.Size(456, 412);
            this.cameraView1.TabIndex = 0;
            // 
            // EditHalconFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.andaLightControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditHalconFormBase";
            this.Text = "EditHalconFormBase";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabRealTimeImage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFileImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnZoomReset;
        private System.Windows.Forms.ToolStripButton btnClearAllROI;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCurrent;
        private System.Windows.Forms.TabPage tabReference;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRect1;
        private System.Windows.Forms.ToolStripButton btnRect2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnExecute;
        private System.Windows.Forms.Button btnRegistImage;
        private System.Windows.Forms.CheckBox chxFixedROI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        protected Domain.Vision.AndaLightControl andaLightControl1;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.NumericUpDown nudSettlingTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabPage tabRealTimeImage;
        private Domain.Vision.CameraView cameraView1;
    }
}