namespace Anda.Fluid.App.EditHalcon
{
    partial class EditBlobsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbrThresholdMin = new System.Windows.Forms.TrackBar();
            this.lblThresholdMin = new System.Windows.Forms.Label();
            this.lblThresholdMax = new System.Windows.Forms.Label();
            this.tbrThresholdMax = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAreaMax = new System.Windows.Forms.Label();
            this.tbrAreaMax = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAreaMin = new System.Windows.Forms.Label();
            this.tbrAreaMin = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chxFilled = new System.Windows.Forms.CheckBox();
            this.lvwBlobs = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrThresholdMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrThresholdMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAreaMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAreaMin)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindow
            // 
            this.hWindow.Size = new System.Drawing.Size(456, 444);
            this.hWindow.WindowSize = new System.Drawing.Size(456, 444);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwBlobs);
            this.groupBox1.Controls.Add(this.chxFilled);
            this.groupBox1.Controls.Add(this.lblAreaMax);
            this.groupBox1.Controls.Add(this.tbrAreaMax);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblAreaMin);
            this.groupBox1.Controls.Add(this.tbrAreaMin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblThresholdMax);
            this.groupBox1.Controls.Add(this.tbrThresholdMax);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblThresholdMin);
            this.groupBox1.Controls.Add(this.tbrThresholdMin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Size = new System.Drawing.Size(308, 688);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "阈值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "下限：";
            // 
            // tbrThresholdMin
            // 
            this.tbrThresholdMin.Location = new System.Drawing.Point(9, 66);
            this.tbrThresholdMin.Name = "tbrThresholdMin";
            this.tbrThresholdMin.Size = new System.Drawing.Size(287, 45);
            this.tbrThresholdMin.TabIndex = 3;
            this.tbrThresholdMin.Scroll += new System.EventHandler(this.tbrThresholdMin_Scroll);
            // 
            // lblThresholdMin
            // 
            this.lblThresholdMin.AutoSize = true;
            this.lblThresholdMin.Location = new System.Drawing.Point(65, 46);
            this.lblThresholdMin.Name = "lblThresholdMin";
            this.lblThresholdMin.Size = new System.Drawing.Size(18, 17);
            this.lblThresholdMin.TabIndex = 4;
            this.lblThresholdMin.Text = "0";
            // 
            // lblThresholdMax
            // 
            this.lblThresholdMax.AutoSize = true;
            this.lblThresholdMax.Location = new System.Drawing.Point(65, 101);
            this.lblThresholdMax.Name = "lblThresholdMax";
            this.lblThresholdMax.Size = new System.Drawing.Size(18, 17);
            this.lblThresholdMax.TabIndex = 7;
            this.lblThresholdMax.Text = "0";
            // 
            // tbrThresholdMax
            // 
            this.tbrThresholdMax.Location = new System.Drawing.Point(9, 121);
            this.tbrThresholdMax.Name = "tbrThresholdMax";
            this.tbrThresholdMax.Size = new System.Drawing.Size(287, 45);
            this.tbrThresholdMax.TabIndex = 6;
            this.tbrThresholdMax.Scroll += new System.EventHandler(this.tbrThresholdMax_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "上限：";
            // 
            // lblAreaMax
            // 
            this.lblAreaMax.AutoSize = true;
            this.lblAreaMax.Location = new System.Drawing.Point(68, 234);
            this.lblAreaMax.Name = "lblAreaMax";
            this.lblAreaMax.Size = new System.Drawing.Size(18, 17);
            this.lblAreaMax.TabIndex = 14;
            this.lblAreaMax.Text = "0";
            // 
            // tbrAreaMax
            // 
            this.tbrAreaMax.Location = new System.Drawing.Point(12, 254);
            this.tbrAreaMax.Name = "tbrAreaMax";
            this.tbrAreaMax.Size = new System.Drawing.Size(287, 45);
            this.tbrAreaMax.TabIndex = 13;
            this.tbrAreaMax.Scroll += new System.EventHandler(this.tbrAreaMax_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "上限：";
            // 
            // lblAreaMin
            // 
            this.lblAreaMin.AutoSize = true;
            this.lblAreaMin.Location = new System.Drawing.Point(68, 180);
            this.lblAreaMin.Name = "lblAreaMin";
            this.lblAreaMin.Size = new System.Drawing.Size(18, 17);
            this.lblAreaMin.TabIndex = 11;
            this.lblAreaMin.Text = "0";
            // 
            // tbrAreaMin
            // 
            this.tbrAreaMin.Location = new System.Drawing.Point(12, 200);
            this.tbrAreaMin.Name = "tbrAreaMin";
            this.tbrAreaMin.Size = new System.Drawing.Size(287, 45);
            this.tbrAreaMin.TabIndex = 10;
            this.tbrAreaMin.Scroll += new System.EventHandler(this.tbrAreaMin_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "下限：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "面积：";
            // 
            // chxFilled
            // 
            this.chxFilled.AutoSize = true;
            this.chxFilled.Location = new System.Drawing.Point(239, 26);
            this.chxFilled.Name = "chxFilled";
            this.chxFilled.Size = new System.Drawing.Size(57, 21);
            this.chxFilled.TabIndex = 15;
            this.chxFilled.Text = "填充";
            this.chxFilled.UseVisualStyleBackColor = true;
            this.chxFilled.CheckedChanged += new System.EventHandler(this.chxFilled_CheckedChanged);
            // 
            // lvwBlobs
            // 
            this.lvwBlobs.HideSelection = false;
            this.lvwBlobs.Location = new System.Drawing.Point(6, 290);
            this.lvwBlobs.Name = "lvwBlobs";
            this.lvwBlobs.Size = new System.Drawing.Size(296, 320);
            this.lvwBlobs.TabIndex = 16;
            this.lvwBlobs.UseCompatibleStateImageBehavior = false;
            this.lvwBlobs.View = System.Windows.Forms.View.List;
            // 
            // EditBlobsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditBlobsForm";
            this.Text = "EditBlobForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrThresholdMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrThresholdMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAreaMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAreaMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblThresholdMax;
        private System.Windows.Forms.TrackBar tbrThresholdMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblThresholdMin;
        private System.Windows.Forms.TrackBar tbrThresholdMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAreaMax;
        private System.Windows.Forms.TrackBar tbrAreaMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAreaMin;
        private System.Windows.Forms.TrackBar tbrAreaMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chxFilled;
        private System.Windows.Forms.ListView lvwBlobs;
    }
}