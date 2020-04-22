namespace Anda.Fluid.Domain.Motion
{
    partial class IOForm
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
            this.gbxDI = new System.Windows.Forms.GroupBox();
            this.gbxDO = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gbxDI
            // 
            this.gbxDI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxDI.Location = new System.Drawing.Point(2, 0);
            this.gbxDI.Name = "gbxDI";
            this.gbxDI.Size = new System.Drawing.Size(307, 607);
            this.gbxDI.TabIndex = 0;
            this.gbxDI.TabStop = false;
            this.gbxDI.Text = "DI";
            // 
            // gbxDO
            // 
            this.gbxDO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDO.Location = new System.Drawing.Point(315, 0);
            this.gbxDO.Name = "gbxDO";
            this.gbxDO.Size = new System.Drawing.Size(699, 607);
            this.gbxDO.TabIndex = 1;
            this.gbxDO.TabStop = false;
            this.gbxDO.Text = "DO";
            // 
            // IOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 610);
            this.Controls.Add(this.gbxDO);
            this.Controls.Add(this.gbxDI);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "IOForm";
            this.Text = "FormIO";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDI;
        private System.Windows.Forms.GroupBox gbxDO;
    }
}