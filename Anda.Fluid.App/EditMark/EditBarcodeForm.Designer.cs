namespace Anda.Fluid.App.EditMark
{
    partial class EditBarcodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nudSettingTime = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudMinLength = new System.Windows.Forms.NumericUpDown();
            this.lblMinLength = new System.Windows.Forms.Label();
            this.nudMaxLength = new System.Windows.Forms.NumericUpDown();
            this.lblMaxLength = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.rbnTaught = new System.Windows.Forms.RadioButton();
            this.rbnPlace = new System.Windows.Forms.RadioButton();
            this.cmbFindCodeMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLength)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.nudMaxLength);
            this.gbx1.Controls.Add(this.label9);
            this.gbx1.Controls.Add(this.lblMaxLength);
            this.gbx1.Controls.Add(this.nudSettingTime);
            this.gbx1.Controls.Add(this.nudMinLength);
            this.gbx1.Controls.Add(this.label10);
            this.gbx1.Controls.Add(this.lblMinLength);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.cmbFindCodeMethod);
            this.gbx2.Controls.Add(this.rbnTaught);
            this.gbx2.Controls.Add(this.rbnPlace);
            this.gbx2.Controls.Add(this.btnTest);
            this.gbx2.Controls.Add(this.textBox1);
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
            this.label3.TabIndex = 39;
            this.label3.Text = "Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(28, 30);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 40;
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(114, 30);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 41;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(281, 29);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 42;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(200, 29);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(75, 23);
            this.btnTeach.TabIndex = 43;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(418, 215);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 37;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 14);
            this.label1.TabIndex = 36;
            this.label1.Text = "InspectionKey";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(129, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 22);
            this.comboBox1.TabIndex = 35;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(261, 108);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 34;
            this.btnEdit.Text = "edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 14);
            this.label10.TabIndex = 46;
            this.label10.Text = "ms";
            // 
            // nudSettingTime
            // 
            this.nudSettingTime.Location = new System.Drawing.Point(121, 43);
            this.nudSettingTime.Name = "nudSettingTime";
            this.nudSettingTime.Size = new System.Drawing.Size(93, 22);
            this.nudSettingTime.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "SettlingTime";
            // 
            // nudMinLength
            // 
            this.nudMinLength.Location = new System.Drawing.Point(121, 86);
            this.nudMinLength.Name = "nudMinLength";
            this.nudMinLength.Size = new System.Drawing.Size(93, 22);
            this.nudMinLength.TabIndex = 48;
            // 
            // lblMinLength
            // 
            this.lblMinLength.AutoSize = true;
            this.lblMinLength.Location = new System.Drawing.Point(25, 88);
            this.lblMinLength.Name = "lblMinLength";
            this.lblMinLength.Size = new System.Drawing.Size(75, 14);
            this.lblMinLength.TabIndex = 47;
            this.lblMinLength.Text = "MinLength";
            // 
            // nudMaxLength
            // 
            this.nudMaxLength.Location = new System.Drawing.Point(121, 114);
            this.nudMaxLength.Name = "nudMaxLength";
            this.nudMaxLength.Size = new System.Drawing.Size(93, 22);
            this.nudMaxLength.TabIndex = 50;
            // 
            // lblMaxLength
            // 
            this.lblMaxLength.AutoSize = true;
            this.lblMaxLength.Location = new System.Drawing.Point(25, 116);
            this.lblMaxLength.Name = "lblMaxLength";
            this.lblMaxLength.Size = new System.Drawing.Size(79, 14);
            this.lblMaxLength.TabIndex = 49;
            this.lblMaxLength.Text = "MaxLength";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 177);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 22);
            this.textBox1.TabIndex = 44;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(337, 148);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 45;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // rbnTaught
            // 
            this.rbnTaught.AutoSize = true;
            this.rbnTaught.Location = new System.Drawing.Point(12, 150);
            this.rbnTaught.Name = "rbnTaught";
            this.rbnTaught.Size = new System.Drawing.Size(113, 18);
            this.rbnTaught.TabIndex = 46;
            this.rbnTaught.TabStop = true;
            this.rbnTaught.Text = "find at taught";
            this.rbnTaught.UseVisualStyleBackColor = true;
            // 
            // rbnPlace
            // 
            this.rbnPlace.AutoSize = true;
            this.rbnPlace.Location = new System.Drawing.Point(168, 150);
            this.rbnPlace.Name = "rbnPlace";
            this.rbnPlace.Size = new System.Drawing.Size(107, 18);
            this.rbnPlace.TabIndex = 47;
            this.rbnPlace.TabStop = true;
            this.rbnPlace.Text = "find in-place";
            this.rbnPlace.UseVisualStyleBackColor = true;
            // 
            // cmbFindCodeMethod
            // 
            this.cmbFindCodeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFindCodeMethod.FormattingEnabled = true;
            this.cmbFindCodeMethod.Location = new System.Drawing.Point(129, 65);
            this.cmbFindCodeMethod.Name = "cmbFindCodeMethod";
            this.cmbFindCodeMethod.Size = new System.Drawing.Size(126, 22);
            this.cmbFindCodeMethod.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 14);
            this.label2.TabIndex = 49;
            this.label2.Text = "FindCodeMethod";
            // 
            // EditBarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditBarcodeForm";
            this.Text = "EditBarcodeForm";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLength)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudSettingTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudMaxLength;
        private System.Windows.Forms.Label lblMaxLength;
        private System.Windows.Forms.NumericUpDown nudMinLength;
        private System.Windows.Forms.Label lblMinLength;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbnTaught;
        private System.Windows.Forms.RadioButton rbnPlace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFindCodeMethod;
    }
}