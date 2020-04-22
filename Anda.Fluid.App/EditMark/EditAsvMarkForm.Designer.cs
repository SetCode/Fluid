namespace Anda.Fluid.App.EditMark
{
    partial class EditAsvMarkForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rbnPlace = new System.Windows.Forms.RadioButton();
            this.rbnTaught = new System.Windows.Forms.RadioButton();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtResultX1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtResultA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtResultY2 = new System.Windows.Forms.TextBox();
            this.txtResultX2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResultY1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtStandardA = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStandardY2 = new System.Windows.Forms.TextBox();
            this.txtStandardX2 = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStandardY1 = new System.Windows.Forms.TextBox();
            this.txtStandardX1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudSettingTime = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbResultType = new System.Windows.Forms.ComboBox();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.label10);
            this.gbx1.Controls.Add(this.nudSettingTime);
            this.gbx1.Controls.Add(this.label9);
            this.gbx1.Controls.Add(this.groupBox1);
            this.gbx1.Controls.Add(this.rbnTaught);
            this.gbx1.Controls.Add(this.btnTest);
            this.gbx1.Controls.Add(this.rbnPlace);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.cbResultType);
            this.gbx2.Controls.Add(this.label15);
            this.gbx2.Controls.Add(this.groupBox2);
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbLocationX);
            this.gbx2.Controls.Add(this.tbLocationY);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.btnTeach);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnOK);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.comboBox1);
            this.gbx2.Controls.Add(this.btnEdit);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 29;
            this.label3.Text = "Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(28, 30);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 30;
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(114, 30);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 31;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(281, 29);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 32;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(200, 29);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(75, 23);
            this.btnTeach.TabIndex = 33;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(418, 215);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 27;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rbnPlace
            // 
            this.rbnPlace.AutoSize = true;
            this.rbnPlace.Location = new System.Drawing.Point(28, 52);
            this.rbnPlace.Name = "rbnPlace";
            this.rbnPlace.Size = new System.Drawing.Size(107, 18);
            this.rbnPlace.TabIndex = 26;
            this.rbnPlace.TabStop = true;
            this.rbnPlace.Text = "find in-place";
            this.rbnPlace.UseVisualStyleBackColor = true;
            // 
            // rbnTaught
            // 
            this.rbnTaught.AutoSize = true;
            this.rbnTaught.Location = new System.Drawing.Point(28, 21);
            this.rbnTaught.Name = "rbnTaught";
            this.rbnTaught.Size = new System.Drawing.Size(113, 18);
            this.rbnTaught.TabIndex = 25;
            this.rbnTaught.TabStop = true;
            this.rbnTaught.Text = "find at taught";
            this.rbnTaught.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(167, 21);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 24;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "InspectionKey";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(114, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 22);
            this.comboBox1.TabIndex = 22;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(246, 76);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtResultX1
            // 
            this.txtResultX1.Location = new System.Drawing.Point(63, 32);
            this.txtResultX1.Name = "txtResultX1";
            this.txtResultX1.Size = new System.Drawing.Size(100, 22);
            this.txtResultX1.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtResultA);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtResultY2);
            this.groupBox1.Controls.Add(this.txtResultX2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtResultY1);
            this.groupBox1.Controls.Add(this.txtResultX1);
            this.groupBox1.Location = new System.Drawing.Point(17, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 179);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "result";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(35, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 14);
            this.label14.TabIndex = 43;
            this.label14.Text = "A";
            // 
            // txtResultA
            // 
            this.txtResultA.Location = new System.Drawing.Point(63, 144);
            this.txtResultA.Name = "txtResultA";
            this.txtResultA.Size = new System.Drawing.Size(100, 22);
            this.txtResultA.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 14);
            this.label5.TabIndex = 41;
            this.label5.Text = "Y2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 14);
            this.label12.TabIndex = 38;
            this.label12.Text = "X2";
            // 
            // txtResultY2
            // 
            this.txtResultY2.Location = new System.Drawing.Point(63, 116);
            this.txtResultY2.Name = "txtResultY2";
            this.txtResultY2.Size = new System.Drawing.Size(100, 22);
            this.txtResultY2.TabIndex = 40;
            // 
            // txtResultX2
            // 
            this.txtResultX2.Location = new System.Drawing.Point(63, 88);
            this.txtResultX2.Name = "txtResultX2";
            this.txtResultX2.Size = new System.Drawing.Size(100, 22);
            this.txtResultX2.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 14);
            this.label4.TabIndex = 37;
            this.label4.Text = "Y1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 14);
            this.label2.TabIndex = 34;
            this.label2.Text = "X1";
            // 
            // txtResultY1
            // 
            this.txtResultY1.Location = new System.Drawing.Point(63, 60);
            this.txtResultY1.Name = "txtResultY1";
            this.txtResultY1.Size = new System.Drawing.Size(100, 22);
            this.txtResultY1.TabIndex = 35;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtStandardA);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtStandardY2);
            this.groupBox2.Controls.Add(this.txtStandardX2);
            this.groupBox2.Controls.Add(this.btnRegister);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtStandardY1);
            this.groupBox2.Controls.Add(this.txtStandardX1);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 128);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "reference";
            // 
            // txtStandardA
            // 
            this.txtStandardA.Location = new System.Drawing.Point(38, 89);
            this.txtStandardA.Name = "txtStandardA";
            this.txtStandardA.Size = new System.Drawing.Size(100, 22);
            this.txtStandardA.TabIndex = 46;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 14);
            this.label13.TabIndex = 45;
            this.label13.Text = "A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 14);
            this.label6.TabIndex = 44;
            this.label6.Text = "Y2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 14);
            this.label11.TabIndex = 41;
            this.label11.Text = "X2";
            // 
            // txtStandardY2
            // 
            this.txtStandardY2.Location = new System.Drawing.Point(188, 60);
            this.txtStandardY2.Name = "txtStandardY2";
            this.txtStandardY2.Size = new System.Drawing.Size(100, 22);
            this.txtStandardY2.TabIndex = 43;
            // 
            // txtStandardX2
            // 
            this.txtStandardX2.Location = new System.Drawing.Point(38, 60);
            this.txtStandardX2.Name = "txtStandardX2";
            this.txtStandardX2.Size = new System.Drawing.Size(100, 22);
            this.txtStandardX2.TabIndex = 42;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(213, 88);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 40;
            this.btnRegister.Text = "register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 37;
            this.label7.Text = "Y1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 14);
            this.label8.TabIndex = 34;
            this.label8.Text = "X1";
            // 
            // txtStandardY1
            // 
            this.txtStandardY1.Location = new System.Drawing.Point(188, 32);
            this.txtStandardY1.Name = "txtStandardY1";
            this.txtStandardY1.Size = new System.Drawing.Size(100, 22);
            this.txtStandardY1.TabIndex = 35;
            // 
            // txtStandardX1
            // 
            this.txtStandardX1.Location = new System.Drawing.Point(38, 32);
            this.txtStandardX1.Name = "txtStandardX1";
            this.txtStandardX1.Size = new System.Drawing.Size(100, 22);
            this.txtStandardX1.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 36;
            this.label9.Text = "SettlingTime";
            // 
            // nudSettingTime
            // 
            this.nudSettingTime.Location = new System.Drawing.Point(110, 88);
            this.nudSettingTime.Name = "nudSettingTime";
            this.nudSettingTime.Size = new System.Drawing.Size(93, 22);
            this.nudSettingTime.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(209, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 14);
            this.label10.TabIndex = 38;
            this.label10.Text = "ms";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(334, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 14);
            this.label15.TabIndex = 40;
            this.label15.Text = "输出方式";
            // 
            // cbResultType
            // 
            this.cbResultType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResultType.FormattingEnabled = true;
            this.cbResultType.Location = new System.Drawing.Point(399, 77);
            this.cbResultType.Name = "cbResultType";
            this.cbResultType.Size = new System.Drawing.Size(88, 22);
            this.cbResultType.TabIndex = 41;
            this.cbResultType.SelectedIndexChanged += new System.EventHandler(this.cbResultType_SelectedIndexChanged);
            // 
            // EditAsvMarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditAsvMarkForm";
            this.Text = "EditAsvMarkForm";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbLocationX;
        private Controls.DoubleTextBox tbLocationY;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbnPlace;
        private System.Windows.Forms.RadioButton rbnTaught;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResultY1;
        private System.Windows.Forms.TextBox txtResultX1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStandardY1;
        private System.Windows.Forms.TextBox txtStandardX1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudSettingTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStandardY2;
        private System.Windows.Forms.TextBox txtStandardX2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtResultY2;
        private System.Windows.Forms.TextBox txtResultX2;
        private System.Windows.Forms.TextBox txtStandardA;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtResultA;
        private System.Windows.Forms.ComboBox cbResultType;
        private System.Windows.Forms.Label label15;
    }
}