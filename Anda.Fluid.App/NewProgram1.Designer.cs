namespace Anda.Fluid.App
{
    partial class NewProgram1
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnOk);
            this.gbx1.Controls.Add(this.btnCancel);
            this.gbx1.Controls.Add(this.btnGoTo);
            this.gbx1.Controls.Add(this.btnSelect);
            this.gbx1.Controls.Add(this.label4);
            this.gbx1.Controls.Add(this.tbY);
            this.gbx1.Controls.Add(this.tbX);
            this.gbx1.Controls.Add(this.label3);
            this.gbx1.Controls.Add(this.label2);
            this.gbx1.Controls.Add(this.tbName);
            this.gbx1.Controls.Add(this.label1);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(63, 15);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(199, 22);
            this.tbName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "Workpice origin:";
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(63, 97);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(118, 22);
            this.tbY.TabIndex = 11;
            this.tbY.Text = "0";
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(63, 69);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(118, 22);
            this.tbX.TabIndex = 12;
            this.tbX.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "X:";
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(187, 98);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 14;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(187, 69);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(187, 260);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(106, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NewProgram1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "NewProgram1";
            this.Text = "NewProgram1";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}