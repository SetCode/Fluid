namespace Anda.Fluid.Domain.SVO.SubForms
{
    partial class TeachScrape
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
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.btnGoto = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // jogControl1
            // 
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(10, 70);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(180, 158);
            this.jogControl1.TabIndex = 0;
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(199, 29);
            this.btnGoto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(100, 27);
            this.btnGoto.TabIndex = 10;
            this.btnGoto.Text = "Goto";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(199, 160);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(100, 27);
            this.btnDone.TabIndex = 9;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(199, 92);
            this.btnTeach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(100, 27);
            this.btnTeach.TabIndex = 8;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scrape Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(13, 33);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(177, 21);
            this.txtLocation.TabIndex = 6;
            // 
            // TeachScrape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 226);
            this.Controls.Add(this.btnGoto);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnTeach);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.jogControl1);
            this.Name = "TeachScrape";
            this.Text = "TeachScrape";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Motion.JogControl jogControl1;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocation;
    }
}