namespace Anda.Fluid.Domain.Dialogs
{
    partial class DialogCalibCamera
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
            this.gbxROI = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudRoiWidth = new System.Windows.Forms.NumericUpDown();
            this.nudRoiHeight = new System.Windows.Forms.NumericUpDown();
            this.nudInc = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblOffsetY = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOffsetX = new System.Windows.Forms.Label();
            this.lblScale = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbxCam.SuspendLayout();
            this.gbxContent.SuspendLayout();
            this.gbxROI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoiWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoiHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInc)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCam
            // 
            this.gbxCam.Size = new System.Drawing.Size(556, 443);
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.gbxROI);
            this.gbxContent.Controls.Add(this.nudInc);
            this.gbxContent.Controls.Add(this.label6);
            this.gbxContent.Controls.Add(this.groupBox5);
            this.gbxContent.Size = new System.Drawing.Size(559, 182);
            // 
            // gbxOption
            // 
            this.gbxOption.Location = new System.Drawing.Point(584, 37);
            // 
            // gbxJog
            // 
            this.gbxJog.Location = new System.Drawing.Point(633, 477);
            // 
            // positionVControl1
            // 
            this.positionVControl1.Location = new System.Drawing.Point(584, 393);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(584, 269);
            // 
            // gbxROI
            // 
            this.gbxROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxROI.Controls.Add(this.label5);
            this.gbxROI.Controls.Add(this.label1);
            this.gbxROI.Controls.Add(this.nudRoiWidth);
            this.gbxROI.Controls.Add(this.nudRoiHeight);
            this.gbxROI.Location = new System.Drawing.Point(6, 58);
            this.gbxROI.Name = "gbxROI";
            this.gbxROI.Size = new System.Drawing.Size(219, 81);
            this.gbxROI.TabIndex = 10;
            this.gbxROI.TabStop = false;
            this.gbxROI.Text = "ROI";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Width";
            // 
            // nudRoiWidth
            // 
            this.nudRoiWidth.Location = new System.Drawing.Point(98, 19);
            this.nudRoiWidth.Name = "nudRoiWidth";
            this.nudRoiWidth.Size = new System.Drawing.Size(100, 22);
            this.nudRoiWidth.TabIndex = 1;
            // 
            // nudRoiHeight
            // 
            this.nudRoiHeight.Location = new System.Drawing.Point(98, 47);
            this.nudRoiHeight.Name = "nudRoiHeight";
            this.nudRoiHeight.Size = new System.Drawing.Size(100, 22);
            this.nudRoiHeight.TabIndex = 2;
            // 
            // nudInc
            // 
            this.nudInc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudInc.Location = new System.Drawing.Point(104, 21);
            this.nudInc.Name = "nudInc";
            this.nudInc.Size = new System.Drawing.Size(100, 22);
            this.nudInc.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "Inc(mm)";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.lblOffsetY);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.lblOffsetX);
            this.groupBox5.Controls.Add(this.lblScale);
            this.groupBox5.Controls.Add(this.lblAngle);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(231, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(305, 118);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Vision Calibration Result";
            // 
            // lblOffsetY
            // 
            this.lblOffsetY.AutoSize = true;
            this.lblOffsetY.Location = new System.Drawing.Point(77, 90);
            this.lblOffsetY.Name = "lblOffsetY";
            this.lblOffsetY.Size = new System.Drawing.Size(16, 14);
            this.lblOffsetY.TabIndex = 8;
            this.lblOffsetY.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 7;
            this.label7.Text = "OffsetY:";
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new System.Drawing.Point(77, 68);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(16, 14);
            this.lblOffsetX.TabIndex = 6;
            this.lblOffsetX.Text = "0";
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(77, 46);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(16, 14);
            this.lblScale.TabIndex = 5;
            this.lblScale.Text = "1";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(77, 24);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(16, 14);
            this.lblAngle.TabIndex = 4;
            this.lblAngle.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "OffsetX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Scale:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Angle:";
            // 
            // DialogCalibCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 662);
            this.Name = "DialogCalibCamera";
            this.Text = "CalibCamera";
            this.gbxCam.ResumeLayout(false);
            this.gbxContent.ResumeLayout(false);
            this.gbxContent.PerformLayout();
            this.gbxROI.ResumeLayout(false);
            this.gbxROI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoiWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoiHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInc)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxROI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudRoiWidth;
        private System.Windows.Forms.NumericUpDown nudRoiHeight;
        private System.Windows.Forms.NumericUpDown nudInc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblOffsetY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOffsetX;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}