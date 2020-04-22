namespace Anda.Fluid.App
{
    partial class EditFormBase
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.gbx2 = new System.Windows.Forms.GroupBox();
            this.gbx1 = new System.Windows.Forms.GroupBox();
            this.lightSettingControl1 = new Anda.Fluid.Domain.Vision.LightSettingControl();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cameraControl1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 400);
            this.panel1.TabIndex = 8;
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(0, 0);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(500, 400);
            this.cameraControl1.TabIndex = 4;
            // 
            // gbx2
            // 
            this.gbx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbx2.Location = new System.Drawing.Point(3, 409);
            this.gbx2.Name = "gbx2";
            this.gbx2.Size = new System.Drawing.Size(500, 250);
            this.gbx2.TabIndex = 7;
            this.gbx2.TabStop = false;
            // 
            // gbx1
            // 
            this.gbx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbx1.Location = new System.Drawing.Point(510, 3);
            this.gbx1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbx1.Name = "gbx1";
            this.gbx1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbx1.Size = new System.Drawing.Size(270, 308);
            this.gbx1.TabIndex = 3;
            this.gbx1.TabStop = false;
            // 
            // lightSettingControl1
            // 
            this.lightSettingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightSettingControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lightSettingControl1.Location = new System.Drawing.Point(510, 317);
            this.lightSettingControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lightSettingControl1.Name = "lightSettingControl1";
            this.lightSettingControl1.Size = new System.Drawing.Size(270, 88);
            this.lightSettingControl1.TabIndex = 9;
            // 
            // jogControl1
            // 
            this.jogControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(600, 502);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(180, 157);
            this.jogControl1.TabIndex = 6;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.positionVControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionVControl1.Location = new System.Drawing.Point(510, 412);
            this.positionVControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(270, 84);
            this.positionVControl1.TabIndex = 5;
            // 
            // EditFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.lightSettingControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbx2);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.positionVControl1);
            this.Controls.Add(this.gbx1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EditFormBase";
            this.Text = "EditFormBase";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbx1;
        protected Domain.Vision.CameraControl cameraControl1;
        private Domain.Motion.PositionVControl positionVControl1;
        private Domain.Motion.JogControl jogControl1;
        protected System.Windows.Forms.GroupBox gbx2;
        private System.Windows.Forms.Panel panel1;
        private Domain.Vision.LightSettingControl lightSettingControl1;
    }
}