namespace Anda.Fluid.Domain.Dialogs.RTVPurge
{
    partial class RTVPurgeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGotoEnd = new System.Windows.Forms.Button();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnTeachEnd = new System.Windows.Forms.Button();
            this.btnLineStartTeach = new System.Windows.Forms.Button();
            this.txtLineEndY = new System.Windows.Forms.TextBox();
            this.txtLineStartY = new System.Windows.Forms.TextBox();
            this.txtLineEndX = new System.Windows.Forms.TextBox();
            this.txtLineStartX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxArrayYDirection = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtArrayXInterval = new Anda.Fluid.Controls.DoubleTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.txtArrayYCount = new Anda.Fluid.Controls.IntTextBox();
            this.txtArrayYInterval = new Anda.Fluid.Controls.DoubleTextBox();
            this.txtArrayXCount = new Anda.Fluid.Controls.IntTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxArrayXDirection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCycles = new Anda.Fluid.Controls.IntTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDelay = new Anda.Fluid.Controls.IntTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVel = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPrimeX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrimeY = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrimeZ = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnGotoPrime = new System.Windows.Forms.Button();
            this.btnTeachPrime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGotoEnd);
            this.groupBox1.Controls.Add(this.btnGotoStart);
            this.groupBox1.Controls.Add(this.btnTeachEnd);
            this.groupBox1.Controls.Add(this.btnLineStartTeach);
            this.groupBox1.Controls.Add(this.txtLineEndY);
            this.groupBox1.Controls.Add(this.txtLineStartY);
            this.groupBox1.Controls.Add(this.txtLineEndX);
            this.groupBox1.Controls.Add(this.txtLineStartX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 89);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(423, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基准线段设置";
            // 
            // btnGotoEnd
            // 
            this.btnGotoEnd.Location = new System.Drawing.Point(265, 57);
            this.btnGotoEnd.Name = "btnGotoEnd";
            this.btnGotoEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGotoEnd.TabIndex = 9;
            this.btnGotoEnd.Text = "到位置";
            this.btnGotoEnd.UseVisualStyleBackColor = true;
            this.btnGotoEnd.Click += new System.EventHandler(this.btnGotoEnd_Click);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(265, 22);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 8;
            this.btnGotoStart.Text = "到位置";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnTeachEnd
            // 
            this.btnTeachEnd.Location = new System.Drawing.Point(346, 57);
            this.btnTeachEnd.Name = "btnTeachEnd";
            this.btnTeachEnd.Size = new System.Drawing.Size(75, 23);
            this.btnTeachEnd.TabIndex = 7;
            this.btnTeachEnd.Text = "示教";
            this.btnTeachEnd.UseVisualStyleBackColor = true;
            this.btnTeachEnd.Click += new System.EventHandler(this.btnTeachEnd_Click);
            // 
            // btnLineStartTeach
            // 
            this.btnLineStartTeach.Location = new System.Drawing.Point(346, 21);
            this.btnLineStartTeach.Name = "btnLineStartTeach";
            this.btnLineStartTeach.Size = new System.Drawing.Size(75, 23);
            this.btnLineStartTeach.TabIndex = 3;
            this.btnLineStartTeach.Text = "示教";
            this.btnLineStartTeach.UseVisualStyleBackColor = true;
            this.btnLineStartTeach.Click += new System.EventHandler(this.btnLineStartTeach_Click);
            // 
            // txtLineEndY
            // 
            this.txtLineEndY.Location = new System.Drawing.Point(157, 58);
            this.txtLineEndY.Name = "txtLineEndY";
            this.txtLineEndY.ReadOnly = true;
            this.txtLineEndY.Size = new System.Drawing.Size(102, 22);
            this.txtLineEndY.TabIndex = 6;
            // 
            // txtLineStartY
            // 
            this.txtLineStartY.Location = new System.Drawing.Point(157, 22);
            this.txtLineStartY.Name = "txtLineStartY";
            this.txtLineStartY.ReadOnly = true;
            this.txtLineStartY.Size = new System.Drawing.Size(102, 22);
            this.txtLineStartY.TabIndex = 2;
            // 
            // txtLineEndX
            // 
            this.txtLineEndX.Location = new System.Drawing.Point(49, 58);
            this.txtLineEndX.Name = "txtLineEndX";
            this.txtLineEndX.ReadOnly = true;
            this.txtLineEndX.Size = new System.Drawing.Size(102, 22);
            this.txtLineEndX.TabIndex = 5;
            // 
            // txtLineStartX
            // 
            this.txtLineStartX.Location = new System.Drawing.Point(49, 22);
            this.txtLineStartX.Name = "txtLineStartX";
            this.txtLineStartX.ReadOnly = true;
            this.txtLineStartX.Size = new System.Drawing.Size(102, 22);
            this.txtLineStartX.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "终点：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "起点：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxArrayYDirection);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtArrayXInterval);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl);
            this.groupBox2.Controls.Add(this.txtArrayYCount);
            this.groupBox2.Controls.Add(this.txtArrayYInterval);
            this.groupBox2.Controls.Add(this.txtArrayXCount);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbxArrayXDirection);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 133);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "阵列设置";
            // 
            // cbxArrayYDirection
            // 
            this.cbxArrayYDirection.FormattingEnabled = true;
            this.cbxArrayYDirection.Location = new System.Drawing.Point(330, 25);
            this.cbxArrayYDirection.Name = "cbxArrayYDirection";
            this.cbxArrayYDirection.Size = new System.Drawing.Size(87, 22);
            this.cbxArrayYDirection.TabIndex = 18;
            this.cbxArrayYDirection.SelectedIndexChanged += new System.EventHandler(this.cbxArrayYDirection_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(215, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 14);
            this.label11.TabIndex = 17;
            this.label11.Text = "阵列Y方向：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "X方向间隔(mm):";
            // 
            // txtArrayXInterval
            // 
            this.txtArrayXInterval.BackColor = System.Drawing.Color.White;
            this.txtArrayXInterval.Location = new System.Drawing.Point(123, 97);
            this.txtArrayXInterval.Name = "txtArrayXInterval";
            this.txtArrayXInterval.Size = new System.Drawing.Size(86, 22);
            this.txtArrayXInterval.TabIndex = 8;
            this.txtArrayXInterval.TextChanged += new System.EventHandler(this.txtArrayXInterval_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 14);
            this.label6.TabIndex = 16;
            this.label6.Text = "Y方向间隔(mm):";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(215, 63);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(108, 14);
            this.lbl.TabIndex = 10;
            this.lbl.Text = "阵列Y方向数量：";
            // 
            // txtArrayYCount
            // 
            this.txtArrayYCount.BackColor = System.Drawing.Color.White;
            this.txtArrayYCount.Location = new System.Drawing.Point(331, 60);
            this.txtArrayYCount.Name = "txtArrayYCount";
            this.txtArrayYCount.Size = new System.Drawing.Size(86, 22);
            this.txtArrayYCount.TabIndex = 9;
            this.txtArrayYCount.TextChanged += new System.EventHandler(this.txtArrayYCount_TextChanged);
            // 
            // txtArrayYInterval
            // 
            this.txtArrayYInterval.BackColor = System.Drawing.Color.White;
            this.txtArrayYInterval.Location = new System.Drawing.Point(331, 97);
            this.txtArrayYInterval.Name = "txtArrayYInterval";
            this.txtArrayYInterval.Size = new System.Drawing.Size(86, 22);
            this.txtArrayYInterval.TabIndex = 15;
            this.txtArrayYInterval.TextChanged += new System.EventHandler(this.txtArrayYInterval_TextChanged);
            // 
            // txtArrayXCount
            // 
            this.txtArrayXCount.BackColor = System.Drawing.Color.White;
            this.txtArrayXCount.Location = new System.Drawing.Point(123, 60);
            this.txtArrayXCount.Name = "txtArrayXCount";
            this.txtArrayXCount.Size = new System.Drawing.Size(86, 22);
            this.txtArrayXCount.TabIndex = 14;
            this.txtArrayXCount.TextChanged += new System.EventHandler(this.txtArrayXCount_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "X方向数量：";
            // 
            // cbxArrayXDirection
            // 
            this.cbxArrayXDirection.FormattingEnabled = true;
            this.cbxArrayXDirection.Location = new System.Drawing.Point(123, 25);
            this.cbxArrayXDirection.Name = "cbxArrayXDirection";
            this.cbxArrayXDirection.Size = new System.Drawing.Size(86, 22);
            this.cbxArrayXDirection.TabIndex = 12;
            this.cbxArrayXDirection.SelectedIndexChanged += new System.EventHandler(this.cbxArrayXDirection_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "阵列X方向：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCycles);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtDelay);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtVel);
            this.groupBox3.Location = new System.Drawing.Point(12, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(406, 93);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "清洗设置";
            // 
            // txtCycles
            // 
            this.txtCycles.BackColor = System.Drawing.Color.White;
            this.txtCycles.Location = new System.Drawing.Point(304, 24);
            this.txtCycles.Name = "txtCycles";
            this.txtCycles.Size = new System.Drawing.Size(76, 22);
            this.txtCycles.TabIndex = 22;
            this.txtCycles.TextChanged += new System.EventHandler(this.txtCycles_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 14);
            this.label9.TabIndex = 21;
            this.label9.Text = "重复次数：";
            // 
            // txtDelay
            // 
            this.txtDelay.BackColor = System.Drawing.Color.White;
            this.txtDelay.Location = new System.Drawing.Point(118, 56);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(88, 22);
            this.txtDelay.TabIndex = 20;
            this.txtDelay.TextChanged += new System.EventHandler(this.txtDelay_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 14);
            this.label8.TabIndex = 19;
            this.label8.Text = "开胶延时(ms)：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "清洗速度:";
            // 
            // txtVel
            // 
            this.txtVel.BackColor = System.Drawing.Color.White;
            this.txtVel.Location = new System.Drawing.Point(89, 24);
            this.txtVel.Name = "txtVel";
            this.txtVel.Size = new System.Drawing.Size(117, 22);
            this.txtVel.TabIndex = 17;
            this.txtVel.TextChanged += new System.EventHandler(this.txtVel_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 449);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(343, 449);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 425);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(306, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "注意：点下确认后，清洗会从第一条线段开始执行。";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(443, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(338, 313);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // jogControl1
            // 
            this.jogControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(528, 326);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(176, 157);
            this.jogControl1.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGotoPrime);
            this.groupBox4.Controls.Add(this.btnTeachPrime);
            this.groupBox4.Controls.Add(this.txtPrimeZ);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtPrimeY);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txtPrimeX);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(12, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(421, 78);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "排胶位设置";
            // 
            // txtPrimeX
            // 
            this.txtPrimeX.Location = new System.Drawing.Point(48, 20);
            this.txtPrimeX.Name = "txtPrimeX";
            this.txtPrimeX.ReadOnly = true;
            this.txtPrimeX.Size = new System.Drawing.Size(102, 22);
            this.txtPrimeX.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 14);
            this.label12.TabIndex = 2;
            this.label12.Text = "X：";
            // 
            // txtPrimeY
            // 
            this.txtPrimeY.Location = new System.Drawing.Point(48, 49);
            this.txtPrimeY.Name = "txtPrimeY";
            this.txtPrimeY.ReadOnly = true;
            this.txtPrimeY.Size = new System.Drawing.Size(102, 22);
            this.txtPrimeY.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 14);
            this.label13.TabIndex = 4;
            this.label13.Text = "Y：";
            // 
            // txtPrimeZ
            // 
            this.txtPrimeZ.Location = new System.Drawing.Point(200, 20);
            this.txtPrimeZ.Name = "txtPrimeZ";
            this.txtPrimeZ.ReadOnly = true;
            this.txtPrimeZ.Size = new System.Drawing.Size(102, 22);
            this.txtPrimeZ.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(165, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 14);
            this.label14.TabIndex = 6;
            this.label14.Text = "Z：";
            // 
            // btnGotoPrime
            // 
            this.btnGotoPrime.Location = new System.Drawing.Point(265, 49);
            this.btnGotoPrime.Name = "btnGotoPrime";
            this.btnGotoPrime.Size = new System.Drawing.Size(75, 23);
            this.btnGotoPrime.TabIndex = 10;
            this.btnGotoPrime.Text = "到位置";
            this.btnGotoPrime.UseVisualStyleBackColor = true;
            this.btnGotoPrime.Click += new System.EventHandler(this.btnGotoPrime_Click);
            // 
            // btnTeachPrime
            // 
            this.btnTeachPrime.Location = new System.Drawing.Point(346, 48);
            this.btnTeachPrime.Name = "btnTeachPrime";
            this.btnTeachPrime.Size = new System.Drawing.Size(75, 23);
            this.btnTeachPrime.TabIndex = 9;
            this.btnTeachPrime.Text = "示教";
            this.btnTeachPrime.UseVisualStyleBackColor = true;
            this.btnTeachPrime.Click += new System.EventHandler(this.btnTeachPrime_Click);
            // 
            // RTVPurgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 484);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RTVPurgeForm";
            this.Text = "RTV清洗设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl;
        private Controls.IntTextBox txtArrayYCount;
        private Controls.DoubleTextBox txtArrayXInterval;
        private System.Windows.Forms.Button btnTeachEnd;
        private System.Windows.Forms.Button btnLineStartTeach;
        private System.Windows.Forms.TextBox txtLineEndY;
        private System.Windows.Forms.TextBox txtLineStartY;
        private System.Windows.Forms.TextBox txtLineEndX;
        private System.Windows.Forms.TextBox txtLineStartX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxArrayXDirection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private Controls.DoubleTextBox txtArrayYInterval;
        private Controls.IntTextBox txtArrayXCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private Controls.DoubleTextBox txtVel;
        private Controls.IntTextBox txtDelay;
        private System.Windows.Forms.Label label8;
        private Controls.IntTextBox txtCycles;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView listView1;
        private Motion.JogControl jogControl1;
        private System.Windows.Forms.ComboBox cbxArrayYDirection;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnGotoEnd;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGotoPrime;
        private System.Windows.Forms.Button btnTeachPrime;
        private System.Windows.Forms.TextBox txtPrimeZ;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPrimeY;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrimeX;
        private System.Windows.Forms.Label label12;
    }
}