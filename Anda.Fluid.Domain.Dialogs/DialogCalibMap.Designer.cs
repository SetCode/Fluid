namespace Anda.Fluid.Domain.Dialogs
{
    partial class DialogCalibMap
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
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSettlingTime = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDwellTime = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnTeachOrigin = new System.Windows.Forms.Button();
            this.btnGotoOrigin = new System.Windows.Forms.Button();
            this.btnGotoHend = new System.Windows.Forms.Button();
            this.btnTeachHend = new System.Windows.Forms.Button();
            this.tbHendY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbHendX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGotoVend = new System.Windows.Forms.Button();
            this.btnTeachVend = new System.Windows.Forms.Button();
            this.tbVendY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbVendX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chxRbfd = new System.Windows.Forms.CheckBox();
            this.chxAsv = new System.Windows.Forms.CheckBox();
            this.btnAsv = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.gbxCam.SuspendLayout();
            this.gbxContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDwellTime)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCam
            // 
            this.gbxCam.Size = new System.Drawing.Size(567, 387);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Size = new System.Drawing.Size(559, 366);
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.panel1);
            this.gbxContent.Controls.Add(this.btnFind);
            this.gbxContent.Controls.Add(this.btnGoto);
            this.gbxContent.Controls.Add(this.listBox1);
            this.gbxContent.Location = new System.Drawing.Point(4, 420);
            this.gbxContent.Size = new System.Drawing.Size(567, 239);
            // 
            // gbxOption
            // 
            this.gbxOption.Location = new System.Drawing.Point(578, 37);
            this.gbxOption.Size = new System.Drawing.Size(245, 162);
            // 
            // gbxJog
            // 
            this.gbxJog.Location = new System.Drawing.Point(629, 477);
            // 
            // positionVControl1
            // 
            this.positionVControl1.Location = new System.Drawing.Point(580, 394);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(578, 277);
            // 
            // nudInterval
            // 
            this.nudInterval.Location = new System.Drawing.Point(109, 130);
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(78, 22);
            this.nudInterval.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Interval";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "SettlingTime";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(193, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "ms";
            // 
            // nudSettlingTime
            // 
            this.nudSettlingTime.Location = new System.Drawing.Point(109, 158);
            this.nudSettlingTime.Name = "nudSettlingTime";
            this.nudSettlingTime.Size = new System.Drawing.Size(78, 22);
            this.nudSettlingTime.TabIndex = 8;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(172, 18);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(65, 48);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(193, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "ms";
            // 
            // nudDwellTime
            // 
            this.nudDwellTime.Location = new System.Drawing.Point(109, 186);
            this.nudDwellTime.Name = "nudDwellTime";
            this.nudDwellTime.Size = new System.Drawing.Size(78, 22);
            this.nudDwellTime.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "DwellTime";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(340, 39);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(221, 186);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnGoto
            // 
            this.btnGoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoto.Location = new System.Drawing.Point(485, 10);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(76, 23);
            this.btnGoto.TabIndex = 14;
            this.btnGoto.Text = "Goto";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(403, 10);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(76, 23);
            this.btnFind.TabIndex = 15;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(87, 18);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(68, 23);
            this.btnVerify.TabIndex = 11;
            this.btnVerify.Text = "verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Origin:";
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbOriginX.Location = new System.Drawing.Point(11, 21);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(70, 22);
            this.tbOriginX.TabIndex = 17;
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbOriginY.Location = new System.Drawing.Point(87, 21);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(70, 22);
            this.tbOriginY.TabIndex = 18;
            // 
            // btnTeachOrigin
            // 
            this.btnTeachOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeachOrigin.Location = new System.Drawing.Point(157, 20);
            this.btnTeachOrigin.Name = "btnTeachOrigin";
            this.btnTeachOrigin.Size = new System.Drawing.Size(60, 23);
            this.btnTeachOrigin.TabIndex = 19;
            this.btnTeachOrigin.Text = "Teach";
            this.btnTeachOrigin.UseVisualStyleBackColor = true;
            this.btnTeachOrigin.Click += new System.EventHandler(this.btnTeachOrigin_Click);
            // 
            // btnGotoOrigin
            // 
            this.btnGotoOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoOrigin.Location = new System.Drawing.Point(223, 20);
            this.btnGotoOrigin.Name = "btnGotoOrigin";
            this.btnGotoOrigin.Size = new System.Drawing.Size(60, 23);
            this.btnGotoOrigin.TabIndex = 20;
            this.btnGotoOrigin.Text = "Goto";
            this.btnGotoOrigin.UseVisualStyleBackColor = true;
            this.btnGotoOrigin.Click += new System.EventHandler(this.btnGotoOrigin_Click);
            // 
            // btnGotoHend
            // 
            this.btnGotoHend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoHend.Location = new System.Drawing.Point(223, 62);
            this.btnGotoHend.Name = "btnGotoHend";
            this.btnGotoHend.Size = new System.Drawing.Size(60, 23);
            this.btnGotoHend.TabIndex = 25;
            this.btnGotoHend.Text = "Goto";
            this.btnGotoHend.UseVisualStyleBackColor = true;
            this.btnGotoHend.Click += new System.EventHandler(this.btnGotoHend_Click);
            // 
            // btnTeachHend
            // 
            this.btnTeachHend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeachHend.Location = new System.Drawing.Point(157, 62);
            this.btnTeachHend.Name = "btnTeachHend";
            this.btnTeachHend.Size = new System.Drawing.Size(60, 23);
            this.btnTeachHend.TabIndex = 24;
            this.btnTeachHend.Text = "Teach";
            this.btnTeachHend.UseVisualStyleBackColor = true;
            this.btnTeachHend.Click += new System.EventHandler(this.btnTeachHend_Click);
            // 
            // tbHendY
            // 
            this.tbHendY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbHendY.Location = new System.Drawing.Point(87, 63);
            this.tbHendY.Name = "tbHendY";
            this.tbHendY.Size = new System.Drawing.Size(70, 22);
            this.tbHendY.TabIndex = 23;
            // 
            // tbHendX
            // 
            this.tbHendX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbHendX.Location = new System.Drawing.Point(11, 63);
            this.tbHendX.Name = "tbHendX";
            this.tbHendX.Size = new System.Drawing.Size(70, 22);
            this.tbHendX.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "Xend:";
            // 
            // btnGotoVend
            // 
            this.btnGotoVend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoVend.Location = new System.Drawing.Point(223, 103);
            this.btnGotoVend.Name = "btnGotoVend";
            this.btnGotoVend.Size = new System.Drawing.Size(60, 23);
            this.btnGotoVend.TabIndex = 30;
            this.btnGotoVend.Text = "Goto";
            this.btnGotoVend.UseVisualStyleBackColor = true;
            this.btnGotoVend.Click += new System.EventHandler(this.btnGotoVend_Click);
            // 
            // btnTeachVend
            // 
            this.btnTeachVend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeachVend.Location = new System.Drawing.Point(157, 103);
            this.btnTeachVend.Name = "btnTeachVend";
            this.btnTeachVend.Size = new System.Drawing.Size(60, 23);
            this.btnTeachVend.TabIndex = 29;
            this.btnTeachVend.Text = "Teach";
            this.btnTeachVend.UseVisualStyleBackColor = true;
            this.btnTeachVend.Click += new System.EventHandler(this.btnTeachVend_Click);
            // 
            // tbVendY
            // 
            this.tbVendY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbVendY.Location = new System.Drawing.Point(87, 104);
            this.tbVendY.Name = "tbVendY";
            this.tbVendY.Size = new System.Drawing.Size(70, 22);
            this.tbVendY.TabIndex = 28;
            // 
            // tbVendX
            // 
            this.tbVendX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbVendX.Location = new System.Drawing.Point(11, 104);
            this.tbVendX.Name = "tbVendX";
            this.tbVendX.Size = new System.Drawing.Size(70, 22);
            this.tbVendX.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 14);
            this.label9.TabIndex = 26;
            this.label9.Text = "Yend:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnGotoVend);
            this.panel1.Controls.Add(this.tbOriginX);
            this.panel1.Controls.Add(this.btnTeachVend);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbOriginY);
            this.panel1.Controls.Add(this.nudDwellTime);
            this.panel1.Controls.Add(this.tbVendY);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnTeachOrigin);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbVendX);
            this.panel1.Controls.Add(this.nudSettlingTime);
            this.panel1.Controls.Add(this.btnGotoOrigin);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudInterval);
            this.panel1.Controls.Add(this.btnGotoHend);
            this.panel1.Controls.Add(this.tbHendX);
            this.panel1.Controls.Add(this.btnTeachHend);
            this.panel1.Controls.Add(this.tbHendY);
            this.panel1.Location = new System.Drawing.Point(8, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 221);
            this.panel1.TabIndex = 31;
            // 
            // chxRbfd
            // 
            this.chxRbfd.AutoSize = true;
            this.chxRbfd.Location = new System.Drawing.Point(10, 21);
            this.chxRbfd.Name = "chxRbfd";
            this.chxRbfd.Size = new System.Drawing.Size(71, 18);
            this.chxRbfd.TabIndex = 31;
            this.chxRbfd.Text = "RBF2D";
            this.chxRbfd.UseVisualStyleBackColor = true;
            // 
            // chxAsv
            // 
            this.chxAsv.AutoSize = true;
            this.chxAsv.Location = new System.Drawing.Point(10, 45);
            this.chxAsv.Name = "chxAsv";
            this.chxAsv.Size = new System.Drawing.Size(53, 18);
            this.chxAsv.TabIndex = 12;
            this.chxAsv.Text = "ASV";
            this.chxAsv.UseVisualStyleBackColor = true;
            this.chxAsv.CheckedChanged += new System.EventHandler(this.chxAsv_CheckedChanged);
            // 
            // btnAsv
            // 
            this.btnAsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsv.Location = new System.Drawing.Point(87, 43);
            this.btnAsv.Name = "btnAsv";
            this.btnAsv.Size = new System.Drawing.Size(68, 23);
            this.btnAsv.TabIndex = 13;
            this.btnAsv.Text = "edit";
            this.btnAsv.UseVisualStyleBackColor = true;
            this.btnAsv.Click += new System.EventHandler(this.btnAsv_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnAsv);
            this.groupBox2.Controls.Add(this.chxAsv);
            this.groupBox2.Controls.Add(this.btnVerify);
            this.groupBox2.Controls.Add(this.chxRbfd);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Location = new System.Drawing.Point(580, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 72);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(571, 585);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(55, 68);
            this.btnTest.TabIndex = 14;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnMapMove_Click);
            // 
            // DialogCalibMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 662);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox2);
            this.Name = "DialogCalibMap";
            this.Text = "DialogCalibMap";
            this.Controls.SetChildIndex(this.gbxOption, 0);
            this.Controls.SetChildIndex(this.gbxJog, 0);
            this.Controls.SetChildIndex(this.positionVControl1, 0);
            this.Controls.SetChildIndex(this.gbxCam, 0);
            this.Controls.SetChildIndex(this.gbxContent, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.btnTest, 0);
            this.gbxCam.ResumeLayout(false);
            this.gbxContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDwellTime)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudSettlingTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudDwellTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label label1;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.Button btnGotoOrigin;
        private System.Windows.Forms.Button btnTeachOrigin;
        private Controls.DoubleTextBox tbOriginY;
        private System.Windows.Forms.Button btnGotoVend;
        private System.Windows.Forms.Button btnTeachVend;
        private Controls.DoubleTextBox tbVendY;
        private Controls.DoubleTextBox tbVendX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGotoHend;
        private System.Windows.Forms.Button btnTeachHend;
        private Controls.DoubleTextBox tbHendY;
        private Controls.DoubleTextBox tbHendX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chxRbfd;
        private System.Windows.Forms.Button btnAsv;
        private System.Windows.Forms.CheckBox chxAsv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTest;
    }
}