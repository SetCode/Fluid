namespace Anda.Fluid.Domain.Sensors
{
    partial class FormWeight
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
            this.userControlWeight2 = new Anda.Fluid.Domain.Sensors.UserControlWeight();
            this.SuspendLayout();
            // 
            // userControlWeight2
            // 
            this.userControlWeight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlWeight2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlWeight2.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlWeight2.Location = new System.Drawing.Point(0, 0);
            this.userControlWeight2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlWeight2.Name = "userControlWeight2";
            this.userControlWeight2.Size = new System.Drawing.Size(654, 501);
            this.userControlWeight2.TabIndex = 0;
            // 
            // FormWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 501);
            this.Controls.Add(this.userControlWeight2);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FormWeight";
            this.Text = "FormWeight";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlWeight userControlWeight2;
    }
}