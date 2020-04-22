namespace Anda.Fluid.App.Main
{
    partial class SensorsReadValueForm
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
            this.btnReadValue = new System.Windows.Forms.Button();
            this.tbxValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnReadValue
            // 
            this.btnReadValue.Location = new System.Drawing.Point(285, 46);
            this.btnReadValue.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnReadValue.Name = "btnReadValue";
            this.btnReadValue.Size = new System.Drawing.Size(125, 33);
            this.btnReadValue.TabIndex = 0;
            this.btnReadValue.Text = "读取";
            this.btnReadValue.UseVisualStyleBackColor = true;
            this.btnReadValue.Click += new System.EventHandler(this.btnReadValue_Click);
            // 
            // tbxValue
            // 
            this.tbxValue.Location = new System.Drawing.Point(14, 13);
            this.tbxValue.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbxValue.Name = "tbxValue";
            this.tbxValue.Size = new System.Drawing.Size(396, 25);
            this.tbxValue.TabIndex = 1;
            // 
            // SensorsReadValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 92);
            this.Controls.Add(this.tbxValue);
            this.Controls.Add(this.btnReadValue);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SensorsReadValueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SensorsReadValueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadValue;
        private System.Windows.Forms.TextBox tbxValue;
    }
}