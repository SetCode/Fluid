namespace Anda.Fluid.App.EditInspection
{
    partial class EditInspectionForm
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.rbnTaught = new System.Windows.Forms.RadioButton();
            this.rbnPlace = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbLocationX);
            this.gbx2.Controls.Add(this.tbLocationY);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.btnTeach);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnOK);
            this.gbx2.Controls.Add(this.rbnPlace);
            this.gbx2.Controls.Add(this.rbnTaught);
            this.gbx2.Controls.Add(this.txtResult);
            this.gbx2.Controls.Add(this.btnTest);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.comboBox1);
            this.gbx2.Controls.Add(this.btnEdit);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(338, 89);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(206, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 22);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "InspectionKey";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(338, 129);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(11, 158);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(483, 22);
            this.txtResult.TabIndex = 4;
            // 
            // rbnTaught
            // 
            this.rbnTaught.AutoSize = true;
            this.rbnTaught.Location = new System.Drawing.Point(50, 131);
            this.rbnTaught.Name = "rbnTaught";
            this.rbnTaught.Size = new System.Drawing.Size(113, 18);
            this.rbnTaught.TabIndex = 5;
            this.rbnTaught.TabStop = true;
            this.rbnTaught.Text = "find at taught";
            this.rbnTaught.UseVisualStyleBackColor = true;
            // 
            // rbnPlace
            // 
            this.rbnPlace.AutoSize = true;
            this.rbnPlace.Location = new System.Drawing.Point(201, 131);
            this.rbnPlace.Name = "rbnPlace";
            this.rbnPlace.Size = new System.Drawing.Size(107, 18);
            this.rbnPlace.TabIndex = 6;
            this.rbnPlace.TabStop = true;
            this.rbnPlace.Text = "find in-place";
            this.rbnPlace.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(419, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(29, 33);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 17;
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(115, 33);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 18;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(282, 32);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 19;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(201, 32);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(75, 23);
            this.btnTeach.TabIndex = 20;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // EditInspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditInspectionForm";
            this.Text = "EditInspectionForm";
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RadioButton rbnTaught;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbnPlace;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbLocationX;
        private Controls.DoubleTextBox tbLocationY;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnTeach;
    }
}