namespace Anda.Fluid.Domain.SVO.SubForms
{
    partial class TeachMeasureHeightAndMarkZ
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGotoMH = new System.Windows.Forms.Button();
            this.txtMeasureHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.btnStatus = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGotoMark = new System.Windows.Forms.Button();
            this.txtMarkZ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.groupBox3);
            this.grpResultTest.Controls.Add(this.groupBox1);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.cameraControl1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGotoMH);
            this.groupBox1.Controls.Add(this.txtMeasureHeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(7, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MeasureHeightZ";
            // 
            // btnGotoMH
            // 
            this.btnGotoMH.Location = new System.Drawing.Point(151, 32);
            this.btnGotoMH.Name = "btnGotoMH";
            this.btnGotoMH.Size = new System.Drawing.Size(75, 23);
            this.btnGotoMH.TabIndex = 35;
            this.btnGotoMH.Text = "Go to";
            this.btnGotoMH.UseVisualStyleBackColor = true;
            this.btnGotoMH.Click += new System.EventHandler(this.btnGotoMH_Click);
            // 
            // txtMeasureHeight
            // 
            this.txtMeasureHeight.Enabled = false;
            this.txtMeasureHeight.Location = new System.Drawing.Point(45, 33);
            this.txtMeasureHeight.Name = "txtMeasureHeight";
            this.txtMeasureHeight.Size = new System.Drawing.Size(100, 21);
            this.txtMeasureHeight.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "Value:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRead);
            this.groupBox2.Controls.Add(this.btnStatus);
            this.groupBox2.Controls.Add(this.txtStatus);
            this.groupBox2.Controls.Add(this.btnRead);
            this.groupBox2.Location = new System.Drawing.Point(18, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 86);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Height Sensor";
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(6, 48);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(100, 21);
            this.txtRead.TabIndex = 1;
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(112, 19);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(75, 23);
            this.btnStatus.TabIndex = 2;
            this.btnStatus.Text = "state";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(6, 20);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 21);
            this.txtStatus.TabIndex = 3;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(112, 47);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGotoMark);
            this.groupBox3.Controls.Add(this.txtMarkZ);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(251, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 187);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CaptureZ";
            // 
            // btnGotoMark
            // 
            this.btnGotoMark.Location = new System.Drawing.Point(151, 32);
            this.btnGotoMark.Name = "btnGotoMark";
            this.btnGotoMark.Size = new System.Drawing.Size(75, 23);
            this.btnGotoMark.TabIndex = 35;
            this.btnGotoMark.Text = "Go to";
            this.btnGotoMark.UseVisualStyleBackColor = true;
            this.btnGotoMark.Click += new System.EventHandler(this.btnGotoMark_Click);
            // 
            // txtMarkZ
            // 
            this.txtMarkZ.Enabled = false;
            this.txtMarkZ.Location = new System.Drawing.Point(45, 33);
            this.txtMarkZ.Name = "txtMarkZ";
            this.txtMarkZ.Size = new System.Drawing.Size(100, 21);
            this.txtMarkZ.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "Value:";
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(0, 0);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(500, 395);
            this.cameraControl1.TabIndex = 0;
            // 
            // TeachMeasureHeightAndMarkZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 657);
            this.Name = "TeachMeasureHeightAndMarkZ";
            this.Text = "TeachMeasureHeightAndMarkZ";
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.pnlDisplay.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRead;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnGotoMH;
        private System.Windows.Forms.TextBox txtMeasureHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGotoMark;
        private System.Windows.Forms.TextBox txtMarkZ;
        private System.Windows.Forms.Label label2;
        private Vision.CameraControl cameraControl1;
    }
}