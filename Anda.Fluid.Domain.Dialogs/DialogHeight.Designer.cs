namespace Anda.Fluid.Domain.Dialogs
{
    partial class DialogHeight
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
            this.laserControl1 = new Anda.Fluid.Domain.Dialogs.LaserControl();
            this.gbxCam.SuspendLayout();
            this.gbxContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCam
            // 
            this.gbxCam.Size = new System.Drawing.Size(570, 443);
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.laserControl1);
            this.gbxContent.Size = new System.Drawing.Size(570, 182);
            // 
            // gbxJog
            // 
            this.gbxJog.Location = new System.Drawing.Point(631, 477);
            // 
            // positionVControl1
            // 
            this.positionVControl1.Location = new System.Drawing.Point(582, 393);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(582, 277);
            // 
            // laserControl1
            // 
            this.laserControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laserControl1.Location = new System.Drawing.Point(314, 30);
            this.laserControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.laserControl1.Name = "laserControl1";
            this.laserControl1.Size = new System.Drawing.Size(213, 118);
            this.laserControl1.TabIndex = 0;
            // 
            // DialogHeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 662);
            this.Name = "DialogHeight";
            this.Text = "DialogHeight";
            this.gbxCam.ResumeLayout(false);
            this.gbxContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LaserControl laserControl1;
    }
}