namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditChangeSvSpeedForm
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
            this.tbWaitInMills = new Anda.Fluid.Controls.IntTextBox();
            this.tbSpeed = new Anda.Fluid.Controls.IntTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbWaitInMills
            // 
            this.tbWaitInMills.BackColor = System.Drawing.Color.White;
            this.tbWaitInMills.Location = new System.Drawing.Point(126, 86);
            this.tbWaitInMills.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tbWaitInMills.Name = "tbWaitInMills";
            this.tbWaitInMills.Size = new System.Drawing.Size(107, 22);
            this.tbWaitInMills.TabIndex = 2;
            // 
            // tbSpeed
            // 
            this.tbSpeed.BackColor = System.Drawing.Color.White;
            this.tbSpeed.Location = new System.Drawing.Point(126, 22);
            this.tbSpeed.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(107, 22);
            this.tbSpeed.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(30, 137);
            this.btnOk.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 31);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 137);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 31);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Wait:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Speed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "r/s";
            // 
            // EditChangeSvSpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 180);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.tbWaitInMills);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EditChangeSvSpeedForm";
            this.Text = "EditChangeSvSpeedForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.IntTextBox tbWaitInMills;
        private Controls.IntTextBox tbSpeed;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}