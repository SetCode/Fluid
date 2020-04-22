namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditMeasurementForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nudSettingTime = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLower = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudUpper = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMeasureType = new System.Windows.Forms.ComboBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.ckbNeedMeasureHeight = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.heightControl1 = new Anda.Fluid.App.EditCmdLineForms.HeightControl();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLocationHeightX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbLocationHeightY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGotoHeight = new System.Windows.Forms.Button();
            this.btnTeachHeight = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.nudHeightLower = new System.Windows.Forms.NumericUpDown();
            this.nudHeightUpper = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ckbMeasureLineWidth = new System.Windows.Forms.CheckBox();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpper)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightUpper)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.groupBox1);
            this.gbx1.Size = new System.Drawing.Size(270, 298);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.ckbMeasureLineWidth);
            this.gbx2.Controls.Add(this.label8);
            this.gbx2.Controls.Add(this.label7);
            this.gbx2.Controls.Add(this.nudHeightLower);
            this.gbx2.Controls.Add(this.nudHeightUpper);
            this.gbx2.Controls.Add(this.tbLocationHeightX);
            this.gbx2.Controls.Add(this.label11);
            this.gbx2.Controls.Add(this.tbLocationHeightY);
            this.gbx2.Controls.Add(this.btnGotoHeight);
            this.gbx2.Controls.Add(this.btnTeachHeight);
            this.gbx2.Controls.Add(this.heightControl1);
            this.gbx2.Controls.Add(this.ckbNeedMeasureHeight);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnOK);
            this.gbx2.Location = new System.Drawing.Point(3, 418);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 37;
            this.label3.Text = "位置:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(17, 39);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 38;
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(103, 39);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 39;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(195, 65);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(54, 23);
            this.btnGoTo.TabIndex = 40;
            this.btnGoTo.Text = "再现";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(195, 39);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(54, 23);
            this.btnTeach.TabIndex = 41;
            this.btnTeach.Text = "示教";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 36;
            this.label1.Text = "视觉流程索引：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(124, 125);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 22);
            this.comboBox1.TabIndex = 35;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(195, 124);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(54, 23);
            this.btnEdit.TabIndex = 34;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(334, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(415, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(176, 251);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 44;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(210, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 14);
            this.label10.TabIndex = 49;
            this.label10.Text = "ms";
            // 
            // nudSettingTime
            // 
            this.nudSettingTime.Location = new System.Drawing.Point(124, 94);
            this.nudSettingTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSettingTime.Name = "nudSettingTime";
            this.nudSettingTime.Size = new System.Drawing.Size(79, 22);
            this.nudSettingTime.TabIndex = 48;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 14);
            this.label9.TabIndex = 47;
            this.label9.Text = "拍照前等待时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 14);
            this.label2.TabIndex = 40;
            this.label2.Text = "结果:";
            // 
            // nudLower
            // 
            this.nudLower.Location = new System.Drawing.Point(90, 224);
            this.nudLower.Name = "nudLower";
            this.nudLower.Size = new System.Drawing.Size(80, 22);
            this.nudLower.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 14);
            this.label5.TabIndex = 48;
            this.label5.Text = "下限:";
            // 
            // nudUpper
            // 
            this.nudUpper.Location = new System.Drawing.Point(90, 193);
            this.nudUpper.Name = "nudUpper";
            this.nudUpper.Size = new System.Drawing.Size(80, 22);
            this.nudUpper.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 14);
            this.label6.TabIndex = 50;
            this.label6.Text = "上限:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 14);
            this.label4.TabIndex = 51;
            this.label4.Text = "测量类型：";
            // 
            // cmbMeasureType
            // 
            this.cmbMeasureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeasureType.FormattingEnabled = true;
            this.cmbMeasureType.Location = new System.Drawing.Point(124, 156);
            this.cmbMeasureType.Name = "cmbMeasureType";
            this.cmbMeasureType.Size = new System.Drawing.Size(57, 22);
            this.cmbMeasureType.TabIndex = 50;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(90, 251);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(80, 22);
            this.txtResult.TabIndex = 51;
            // 
            // ckbNeedMeasureHeight
            // 
            this.ckbNeedMeasureHeight.AutoSize = true;
            this.ckbNeedMeasureHeight.Location = new System.Drawing.Point(114, 226);
            this.ckbNeedMeasureHeight.Name = "ckbNeedMeasureHeight";
            this.ckbNeedMeasureHeight.Size = new System.Drawing.Size(52, 18);
            this.ckbNeedMeasureHeight.TabIndex = 52;
            this.ckbNeedMeasureHeight.Text = "测高";
            this.ckbNeedMeasureHeight.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbMeasureType);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.nudLower);
            this.groupBox1.Controls.Add(this.nudSettingTime);
            this.groupBox1.Controls.Add(this.nudUpper);
            this.groupBox1.Controls.Add(this.btnGoTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnTeach);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbLocationX);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.tbLocationY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 283);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测线宽";
            // 
            // heightControl1
            // 
            this.heightControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightControl1.Location = new System.Drawing.Point(7, 74);
            this.heightControl1.Name = "heightControl1";
            this.heightControl1.Size = new System.Drawing.Size(448, 138);
            this.heightControl1.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 14);
            this.label7.TabIndex = 54;
            this.label7.Text = "位置:";
            // 
            // tbLocationHeightX
            // 
            this.tbLocationHeightX.BackColor = System.Drawing.Color.White;
            this.tbLocationHeightX.Location = new System.Drawing.Point(17, 36);
            this.tbLocationHeightX.Name = "tbLocationHeightX";
            this.tbLocationHeightX.Size = new System.Drawing.Size(72, 22);
            this.tbLocationHeightX.TabIndex = 55;
            // 
            // tbLocationHeightY
            // 
            this.tbLocationHeightY.BackColor = System.Drawing.Color.White;
            this.tbLocationHeightY.Location = new System.Drawing.Point(94, 36);
            this.tbLocationHeightY.Name = "tbLocationHeightY";
            this.tbLocationHeightY.Size = new System.Drawing.Size(72, 22);
            this.tbLocationHeightY.TabIndex = 56;
            // 
            // btnGotoHeight
            // 
            this.btnGotoHeight.Location = new System.Drawing.Point(240, 35);
            this.btnGotoHeight.Name = "btnGotoHeight";
            this.btnGotoHeight.Size = new System.Drawing.Size(55, 23);
            this.btnGotoHeight.TabIndex = 57;
            this.btnGotoHeight.Text = "再现";
            this.btnGotoHeight.UseVisualStyleBackColor = true;
            this.btnGotoHeight.Click += new System.EventHandler(this.btnGotoHeight_Click);
            // 
            // btnTeachHeight
            // 
            this.btnTeachHeight.Location = new System.Drawing.Point(179, 36);
            this.btnTeachHeight.Name = "btnTeachHeight";
            this.btnTeachHeight.Size = new System.Drawing.Size(55, 23);
            this.btnTeachHeight.TabIndex = 58;
            this.btnTeachHeight.Text = "示教";
            this.btnTeachHeight.UseVisualStyleBackColor = true;
            this.btnTeachHeight.Click += new System.EventHandler(this.btnTeachHeight_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(319, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 14);
            this.label8.TabIndex = 55;
            this.label8.Text = "上限:";
            // 
            // nudHeightLower
            // 
            this.nudHeightLower.Location = new System.Drawing.Point(363, 56);
            this.nudHeightLower.Name = "nudHeightLower";
            this.nudHeightLower.Size = new System.Drawing.Size(80, 22);
            this.nudHeightLower.TabIndex = 52;
            // 
            // nudHeightUpper
            // 
            this.nudHeightUpper.Location = new System.Drawing.Point(363, 25);
            this.nudHeightUpper.Name = "nudHeightUpper";
            this.nudHeightUpper.Size = new System.Drawing.Size(80, 22);
            this.nudHeightUpper.TabIndex = 54;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(319, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 14);
            this.label11.TabIndex = 53;
            this.label11.Text = "下限:";
            // 
            // ckbMeasureLineWidth
            // 
            this.ckbMeasureLineWidth.AutoSize = true;
            this.ckbMeasureLineWidth.Location = new System.Drawing.Point(24, 226);
            this.ckbMeasureLineWidth.Name = "ckbMeasureLineWidth";
            this.ckbMeasureLineWidth.Size = new System.Drawing.Size(65, 18);
            this.ckbMeasureLineWidth.TabIndex = 59;
            this.ckbMeasureLineWidth.Text = "测线宽";
            this.ckbMeasureLineWidth.UseVisualStyleBackColor = true;
            // 
            // EditMeasurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 671);
            this.Name = "EditMeasurementForm";
            this.Text = "EditMeasurementForm";
            this.gbx1.ResumeLayout(false);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpper)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightUpper)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbLocationX;
        private Controls.DoubleTextBox tbLocationY;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudSettingTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudUpper;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudLower;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMeasureType;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.CheckBox ckbNeedMeasureHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private HeightControl heightControl1;
        private System.Windows.Forms.Label label7;
        private Controls.DoubleTextBox tbLocationHeightX;
        private Controls.DoubleTextBox tbLocationHeightY;
        private System.Windows.Forms.Button btnGotoHeight;
        private System.Windows.Forms.Button btnTeachHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudHeightLower;
        private System.Windows.Forms.NumericUpDown nudHeightUpper;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckbMeasureLineWidth;
    }
}