namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditMoveAbsXyForm1
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
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNeedle2 = new System.Windows.Forms.RadioButton();
            this.rbLaser = new System.Windows.Forms.RadioButton();
            this.rbNeedle1 = new System.Windows.Forms.RadioButton();
            this.rbCamera = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnOk);
            this.gbx1.Controls.Add(this.btnCancel);
            this.gbx1.Controls.Add(this.btnGoTo);
            this.gbx1.Controls.Add(this.btnSelect);
            this.gbx1.Controls.Add(this.tbY);
            this.gbx1.Controls.Add(this.tbX);
            this.gbx1.Controls.Add(this.label2);
            this.gbx1.Controls.Add(this.label1);
            this.gbx1.Controls.Add(this.groupBox1);
            this.gbx1.Size = new System.Drawing.Size(270, 303);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(187, 156);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 13;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(187, 128);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(37, 156);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(144, 22);
            this.tbY.TabIndex = 11;
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(37, 128);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(145, 22);
            this.tbX.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "X:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNeedle2);
            this.groupBox1.Controls.Add(this.rbLaser);
            this.groupBox1.Controls.Add(this.rbNeedle1);
            this.groupBox1.Controls.Add(this.rbCamera);
            this.groupBox1.Location = new System.Drawing.Point(7, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 88);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // rbNeedle2
            // 
            this.rbNeedle2.AutoSize = true;
            this.rbNeedle2.Location = new System.Drawing.Point(152, 54);
            this.rbNeedle2.Name = "rbNeedle2";
            this.rbNeedle2.Size = new System.Drawing.Size(86, 18);
            this.rbNeedle2.TabIndex = 2;
            this.rbNeedle2.Text = "NEEDLE2";
            this.rbNeedle2.UseVisualStyleBackColor = true;
            // 
            // rbLaser
            // 
            this.rbLaser.AutoSize = true;
            this.rbLaser.Location = new System.Drawing.Point(152, 21);
            this.rbLaser.Name = "rbLaser";
            this.rbLaser.Size = new System.Drawing.Size(68, 18);
            this.rbLaser.TabIndex = 1;
            this.rbLaser.TabStop = true;
            this.rbLaser.Text = "LASER";
            this.rbLaser.UseVisualStyleBackColor = true;
            // 
            // rbNeedle1
            // 
            this.rbNeedle1.AutoSize = true;
            this.rbNeedle1.Location = new System.Drawing.Point(19, 54);
            this.rbNeedle1.Name = "rbNeedle1";
            this.rbNeedle1.Size = new System.Drawing.Size(86, 18);
            this.rbNeedle1.TabIndex = 0;
            this.rbNeedle1.Text = "NEEDLE1";
            this.rbNeedle1.UseVisualStyleBackColor = true;
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Checked = true;
            this.rbCamera.Location = new System.Drawing.Point(16, 21);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(80, 18);
            this.rbCamera.TabIndex = 0;
            this.rbCamera.TabStop = true;
            this.rbCamera.Text = "CAMERA";
            this.rbCamera.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(188, 273);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(107, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditMoveAbsXyForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditMoveAbsXyForm1";
            this.Text = "EditMoveAbsXyForm1";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLaser;
        private System.Windows.Forms.RadioButton rbNeedle1;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbNeedle2;
    }
}