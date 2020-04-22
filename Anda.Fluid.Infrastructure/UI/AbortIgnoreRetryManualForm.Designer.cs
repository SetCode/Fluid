namespace Anda.Fluid.Infrastructure.UI
{
    partial class AbortIgnoreRetryManualForm
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
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(14, 95);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 30);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(186, 95);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 30);
            this.btnRetry.TabIndex = 1;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(272, 95);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(75, 30);
            this.btnManual.TabIndex = 2;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(334, 77);
            this.textBox1.TabIndex = 3;
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(100, 95);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(75, 30);
            this.btnIgnore.TabIndex = 4;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // AbortIgnoreRetryManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 134);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnAbort);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AbortIgnoreRetryManualForm";
            this.Text = "AbortRetryIgnoreManualForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnIgnore;
    }
}