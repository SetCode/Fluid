namespace Anda.Fluid.Domain.Dialogs
{
    partial class DialogBase
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
            this.gbxContent = new System.Windows.Forms.GroupBox();
            this.gbxCam = new System.Windows.Forms.GroupBox();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbxJog = new System.Windows.Forms.GroupBox();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.gbxOption = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lightSettingControl1 = new Anda.Fluid.Domain.Vision.LightSettingControl();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.gbxCam.SuspendLayout();
            this.gbxJog.SuspendLayout();
            this.gbxOption.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxContent
            // 
            this.gbxContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxContent.Location = new System.Drawing.Point(4, 477);
            this.gbxContent.Name = "gbxContent";
            this.gbxContent.Size = new System.Drawing.Size(571, 182);
            this.gbxContent.TabIndex = 4;
            this.gbxContent.TabStop = false;
            // 
            // gbxCam
            // 
            this.gbxCam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxCam.Controls.Add(this.cameraControl1);
            this.gbxCam.Location = new System.Drawing.Point(4, 37);
            this.gbxCam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxCam.Name = "gbxCam";
            this.gbxCam.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxCam.Size = new System.Drawing.Size(564, 443);
            this.gbxCam.TabIndex = 0;
            this.gbxCam.TabStop = false;
            this.gbxCam.Text = "Camera";
            // 
            // cameraControl1
            // 
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(4, 18);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(555, 422);
            this.cameraControl1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(13, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // gbxJog
            // 
            this.gbxJog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxJog.Controls.Add(this.jogControl1);
            this.gbxJog.Location = new System.Drawing.Point(639, 483);
            this.gbxJog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxJog.Name = "gbxJog";
            this.gbxJog.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxJog.Size = new System.Drawing.Size(189, 176);
            this.gbxJog.TabIndex = 2;
            this.gbxJog.TabStop = false;
            this.gbxJog.Text = "Jog";
            // 
            // jogControl1
            // 
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(8, 21);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(180, 200);
            this.jogControl1.TabIndex = 0;
            // 
            // gbxOption
            // 
            this.gbxOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxOption.Controls.Add(this.btnCancel);
            this.gbxOption.Controls.Add(this.btnDone);
            this.gbxOption.Controls.Add(this.btnHelp);
            this.gbxOption.Controls.Add(this.btnTeach);
            this.gbxOption.Controls.Add(this.btnNext);
            this.gbxOption.Controls.Add(this.btnPrev);
            this.gbxOption.Location = new System.Drawing.Point(585, 37);
            this.gbxOption.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxOption.Name = "gbxOption";
            this.gbxOption.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbxOption.Size = new System.Drawing.Size(245, 171);
            this.gbxOption.TabIndex = 1;
            this.gbxOption.TabStop = false;
            this.gbxOption.Text = "Option";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(54, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(54, 108);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(126, 23);
            this.btnDone.TabIndex = 4;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(54, 79);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(126, 23);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(54, 50);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(126, 23);
            this.btnTeach.TabIndex = 2;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(120, 21);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(54, 21);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(60, 23);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lightSettingControl1);
            this.groupBox1.Location = new System.Drawing.Point(583, 292);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // lightSettingControl1
            // 
            this.lightSettingControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lightSettingControl1.Location = new System.Drawing.Point(2, 10);
            this.lightSettingControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lightSettingControl1.Name = "lightSettingControl1";
            this.lightSettingControl1.Size = new System.Drawing.Size(239, 85);
            this.lightSettingControl1.TabIndex = 0;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.positionVControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionVControl1.Location = new System.Drawing.Point(583, 399);
            this.positionVControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(248, 84);
            this.positionVControl1.TabIndex = 3;
            // 
            // DialogBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 662);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxContent);
            this.Controls.Add(this.gbxCam);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.positionVControl1);
            this.Controls.Add(this.gbxJog);
            this.Controls.Add(this.gbxOption);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogBase";
            this.Text = "DialogBase";
            this.gbxCam.ResumeLayout(false);
            this.gbxJog.ResumeLayout(false);
            this.gbxOption.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbxCam;
        private System.Windows.Forms.Label lblTitle;
        private Motion.JogControl jogControl1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        protected Vision.CameraControl cameraControl1;
        protected System.Windows.Forms.GroupBox gbxContent;
        protected System.Windows.Forms.GroupBox gbxOption;
        protected System.Windows.Forms.GroupBox gbxJog;
        protected Motion.PositionVControl positionVControl1;
        private Vision.LightSettingControl lightSettingControl1;
        protected System.Windows.Forms.GroupBox groupBox1;
    }
}