namespace Anda.Fluid.Domain.Dialogs
{
    partial class JogMapForm
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
            this.btnXp = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnXn = new System.Windows.Forms.Button();
            this.btnYp = new System.Windows.Forms.Button();
            this.btnYn = new System.Windows.Forms.Button();
            this.btnLearning = new System.Windows.Forms.Button();
            this.rbnBilinear = new System.Windows.Forms.RadioButton();
            this.rbnRBF = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXp
            // 
            this.btnXp.Location = new System.Drawing.Point(131, 175);
            this.btnXp.Name = "btnXp";
            this.btnXp.Size = new System.Drawing.Size(40, 40);
            this.btnXp.TabIndex = 0;
            this.btnXp.Text = "X+";
            this.btnXp.UseVisualStyleBackColor = true;
            this.btnXp.Click += new System.EventHandler(this.btnXp_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(39, 86);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(132, 25);
            this.numericUpDown1.TabIndex = 1;
            // 
            // btnXn
            // 
            this.btnXn.Location = new System.Drawing.Point(39, 175);
            this.btnXn.Name = "btnXn";
            this.btnXn.Size = new System.Drawing.Size(40, 40);
            this.btnXn.TabIndex = 2;
            this.btnXn.Text = "X-";
            this.btnXn.UseVisualStyleBackColor = true;
            this.btnXn.Click += new System.EventHandler(this.btnXn_Click);
            // 
            // btnYp
            // 
            this.btnYp.Location = new System.Drawing.Point(85, 129);
            this.btnYp.Name = "btnYp";
            this.btnYp.Size = new System.Drawing.Size(40, 40);
            this.btnYp.TabIndex = 3;
            this.btnYp.Text = "Y+";
            this.btnYp.UseVisualStyleBackColor = true;
            this.btnYp.Click += new System.EventHandler(this.btnYp_Click);
            // 
            // btnYn
            // 
            this.btnYn.Location = new System.Drawing.Point(85, 221);
            this.btnYn.Name = "btnYn";
            this.btnYn.Size = new System.Drawing.Size(40, 40);
            this.btnYn.TabIndex = 4;
            this.btnYn.Text = "Y-";
            this.btnYn.UseVisualStyleBackColor = true;
            this.btnYn.Click += new System.EventHandler(this.btnYn_Click);
            // 
            // btnLearning
            // 
            this.btnLearning.Location = new System.Drawing.Point(104, 12);
            this.btnLearning.Name = "btnLearning";
            this.btnLearning.Size = new System.Drawing.Size(98, 30);
            this.btnLearning.TabIndex = 5;
            this.btnLearning.Text = "Learning";
            this.btnLearning.UseVisualStyleBackColor = true;
            this.btnLearning.Click += new System.EventHandler(this.btnLearning_Click);
            // 
            // rbnBilinear
            // 
            this.rbnBilinear.AutoSize = true;
            this.rbnBilinear.Location = new System.Drawing.Point(12, 12);
            this.rbnBilinear.Name = "rbnBilinear";
            this.rbnBilinear.Size = new System.Drawing.Size(84, 21);
            this.rbnBilinear.TabIndex = 6;
            this.rbnBilinear.TabStop = true;
            this.rbnBilinear.Text = "Bilinear";
            this.rbnBilinear.UseVisualStyleBackColor = true;
            this.rbnBilinear.CheckedChanged += new System.EventHandler(this.rbnBilinear_CheckedChanged);
            // 
            // rbnRBF
            // 
            this.rbnRBF.AutoSize = true;
            this.rbnRBF.Location = new System.Drawing.Point(12, 39);
            this.rbnRBF.Name = "rbnRBF";
            this.rbnRBF.Size = new System.Drawing.Size(78, 21);
            this.rbnRBF.TabIndex = 7;
            this.rbnRBF.TabStop = true;
            this.rbnRBF.Text = "RBF2D";
            this.rbnRBF.UseVisualStyleBackColor = true;
            this.rbnRBF.CheckedChanged += new System.EventHandler(this.rbnRBF_CheckedChanged);
            // 
            // JogMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 278);
            this.Controls.Add(this.rbnRBF);
            this.Controls.Add(this.rbnBilinear);
            this.Controls.Add(this.btnLearning);
            this.Controls.Add(this.btnYn);
            this.Controls.Add(this.btnYp);
            this.Controls.Add(this.btnXn);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnXp);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "JogMapForm";
            this.Text = "JogMapForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXp;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnXn;
        private System.Windows.Forms.Button btnYp;
        private System.Windows.Forms.Button btnYn;
        private System.Windows.Forms.Button btnLearning;
        private System.Windows.Forms.RadioButton rbnBilinear;
        private System.Windows.Forms.RadioButton rbnRBF;
    }
}