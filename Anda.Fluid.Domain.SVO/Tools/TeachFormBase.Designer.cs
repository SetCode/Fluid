using Anda.Fluid.Domain.Motion;

namespace Anda.Fluid.Domain.SVO
{
    partial class TeachFormBase
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
            this.grpOperation = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.grpMotion = new System.Windows.Forms.GroupBox();
            this.grpLighting = new System.Windows.Forms.GroupBox();
            this.grpResultTest = new System.Windows.Forms.GroupBox();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpDisplay = new System.Windows.Forms.GroupBox();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.grpOperation.SuspendLayout();
            this.grpMotion.SuspendLayout();
            this.grpDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOperation
            // 
            this.grpOperation.Controls.Add(this.btnHelp);
            this.grpOperation.Controls.Add(this.btnNext);
            this.grpOperation.Controls.Add(this.btnDone);
            this.grpOperation.Controls.Add(this.btnCancel);
            this.grpOperation.Controls.Add(this.btnTeach);
            this.grpOperation.Controls.Add(this.btnPrev);
            this.grpOperation.Location = new System.Drawing.Point(522, 11);
            this.grpOperation.Name = "grpOperation";
            this.grpOperation.Size = new System.Drawing.Size(195, 222);
            this.grpOperation.TabIndex = 15;
            this.grpOperation.TabStop = false;
            this.grpOperation.Text = "Operation";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(54, 131);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(93, 28);
            this.btnHelp.TabIndex = 20;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(109, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 28);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(54, 94);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(93, 28);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(54, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(54, 57);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(93, 28);
            this.btnTeach.TabIndex = 4;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(34, 20);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(56, 28);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // grpMotion
            // 
            this.grpMotion.Controls.Add(this.jogControl1);
            this.grpMotion.Location = new System.Drawing.Point(522, 453);
            this.grpMotion.Name = "grpMotion";
            this.grpMotion.Size = new System.Drawing.Size(195, 198);
            this.grpMotion.TabIndex = 17;
            this.grpMotion.TabStop = false;
            this.grpMotion.Text = "Motion";
            // 
            // grpLighting
            // 
            this.grpLighting.Location = new System.Drawing.Point(522, 239);
            this.grpLighting.Name = "grpLighting";
            this.grpLighting.Size = new System.Drawing.Size(195, 110);
            this.grpLighting.TabIndex = 18;
            this.grpLighting.TabStop = false;
            this.grpLighting.Text = "Lighting";
            // 
            // grpResultTest
            // 
            this.grpResultTest.Location = new System.Drawing.Point(5, 453);
            this.grpResultTest.Name = "grpResultTest";
            this.grpResultTest.Size = new System.Drawing.Size(511, 198);
            this.grpResultTest.TabIndex = 20;
            this.grpResultTest.TabStop = false;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Location = new System.Drawing.Point(6, 12);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(500, 395);
            this.pnlDisplay.TabIndex = 21;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(48, 17);
            this.lblTitle.TabIndex = 22;
            this.lblTitle.Text = "Title";
            // 
            // grpDisplay
            // 
            this.grpDisplay.Controls.Add(this.pnlDisplay);
            this.grpDisplay.Location = new System.Drawing.Point(5, 34);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Size = new System.Drawing.Size(511, 413);
            this.grpDisplay.TabIndex = 23;
            this.grpDisplay.TabStop = false;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Location = new System.Drawing.Point(522, 355);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(195, 92);
            this.positionVControl1.TabIndex = 24;
            // 
            // jogControl1
            // 
            this.jogControl1.Location = new System.Drawing.Point(6, 12);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(184, 184);
            this.jogControl1.TabIndex = 0;
            // 
            // TeachFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 657);
            this.Controls.Add(this.positionVControl1);
            this.Controls.Add(this.grpResultTest);
            this.Controls.Add(this.grpDisplay);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpLighting);
            this.Controls.Add(this.grpMotion);
            this.Controls.Add(this.grpOperation);
            this.Name = "TeachFormBase";
            this.Text = "TeachFormBase";
            this.grpOperation.ResumeLayout(false);
            this.grpMotion.ResumeLayout(false);
            this.grpDisplay.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox grpOperation;
        public System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpMotion;
        private System.Windows.Forms.GroupBox grpLighting;
        private System.Windows.Forms.GroupBox grpDisplay;
        public System.Windows.Forms.Button btnHelp;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnTeach;
        public System.Windows.Forms.Button btnPrev;
        public System.Windows.Forms.GroupBox grpResultTest;
        public System.Windows.Forms.Panel pnlDisplay;
        public System.Windows.Forms.Label lblTitle;
        private JogControl jogControl1;
        private PositionVControl positionVControl1;
    }
}