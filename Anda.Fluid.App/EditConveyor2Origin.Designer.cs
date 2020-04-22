namespace Anda.Fluid.App
{
    partial class EditConveyor2Origin
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
            this.btnGoto = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnGoto);
            this.gbx1.Controls.Add(this.btnTeach);
            this.gbx1.Controls.Add(this.label5);
            this.gbx1.Controls.Add(this.tbY);
            this.gbx1.Controls.Add(this.tbX);
            this.gbx1.Controls.Add(this.label6);
            this.gbx1.Controls.Add(this.label7);
            this.gbx1.Controls.Add(this.btnOk);
            this.gbx1.Controls.Add(this.btnCancel);
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(185, 78);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(75, 23);
            this.btnGoto.TabIndex = 32;
            this.btnGoto.Text = "Go To";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(185, 49);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(75, 23);
            this.btnTeach.TabIndex = 33;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 14);
            this.label5.TabIndex = 31;
            this.label5.Text = "Conveyor2 origin:";
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(61, 77);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(118, 22);
            this.tbY.TabIndex = 29;
            this.tbY.Text = "0";
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(61, 49);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(118, 22);
            this.tbX.TabIndex = 30;
            this.tbX.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 14);
            this.label6.TabIndex = 27;
            this.label6.Text = "Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 28;
            this.label7.Text = "X:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(185, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(104, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditConveyor2Origin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditConveyor2Origin";
            this.Text = "EditConveyor2Origin";
            this.Load += new System.EventHandler(this.EditConveyor2Origin_Load);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Label label5;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}