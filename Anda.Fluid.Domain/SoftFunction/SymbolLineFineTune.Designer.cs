namespace Anda.Fluid.Domain.SoftFunction
{
    partial class SymbolLineFineTune
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
            this.btnModify = new System.Windows.Forms.Button();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.SuspendLayout();
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.btnModify.Location = new System.Drawing.Point(334, 510);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(113, 47);
            this.btnModify.TabIndex = 0;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(1, 2);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(781, 502);
            this.cameraControl1.TabIndex = 1;
            // 
            // SymbolLineFineTune
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 565);
            this.Controls.Add(this.cameraControl1);
            this.Controls.Add(this.btnModify);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SymbolLineFineTune";
            this.Text = "SymbolLineFineTune";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SymbolLineFineTune_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnModify;
        private Vision.CameraControl cameraControl1;
    }
}