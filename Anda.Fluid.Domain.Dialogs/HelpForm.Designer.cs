namespace Anda.Fluid.Domain.Dialogs
{
    partial class HelpForm
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
            this.rtbHelp = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbHelp
            // 
            this.rtbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHelp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbHelp.Location = new System.Drawing.Point(0, 0);
            this.rtbHelp.Name = "rtbHelp";
            this.rtbHelp.Size = new System.Drawing.Size(355, 467);
            this.rtbHelp.TabIndex = 0;
            this.rtbHelp.Text = "";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 467);
            this.Controls.Add(this.rtbHelp);
            this.Name = "HelpForm";
            this.Text = "Help";
            this.Load += new System.EventHandler(this.Help_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbHelp;
    }
}