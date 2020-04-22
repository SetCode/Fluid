namespace Anda.Fluid.Domain.Sensors
{
    partial class CalibrateDialog
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
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMsgShow = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblActWeight = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpWeight = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.grpWeight.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(12, 234);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(104, 234);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(197, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "WeightSpecification:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValue
            // 
            this.lblValue.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(158, 18);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(63, 14);
            this.lblValue.TabIndex = 5;
            this.lblValue.Text = "0";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMsgShow);
            this.panel1.Location = new System.Drawing.Point(12, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 141);
            this.panel1.TabIndex = 6;
            // 
            // txtMsgShow
            // 
            this.txtMsgShow.BackColor = System.Drawing.SystemColors.Control;
            this.txtMsgShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsgShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsgShow.Location = new System.Drawing.Point(0, 0);
            this.txtMsgShow.Multiline = true;
            this.txtMsgShow.Name = "txtMsgShow";
            this.txtMsgShow.ReadOnly = true;
            this.txtMsgShow.Size = new System.Drawing.Size(260, 141);
            this.txtMsgShow.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(227, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "mg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(227, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "mg";
            // 
            // lblActWeight
            // 
            this.lblActWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActWeight.Location = new System.Drawing.Point(158, 40);
            this.lblActWeight.Name = "lblActWeight";
            this.lblActWeight.Size = new System.Drawing.Size(63, 14);
            this.lblActWeight.TabIndex = 9;
            this.lblActWeight.Text = "0";
            this.lblActWeight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "ActualWeight:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpWeight
            // 
            this.grpWeight.Controls.Add(this.label1);
            this.grpWeight.Controls.Add(this.label5);
            this.grpWeight.Controls.Add(this.label3);
            this.grpWeight.Controls.Add(this.lblValue);
            this.grpWeight.Controls.Add(this.lblActWeight);
            this.grpWeight.Controls.Add(this.label2);
            this.grpWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpWeight.Location = new System.Drawing.Point(12, 5);
            this.grpWeight.Name = "grpWeight";
            this.grpWeight.Size = new System.Drawing.Size(260, 69);
            this.grpWeight.TabIndex = 11;
            this.grpWeight.TabStop = false;
            this.grpWeight.Text = "Weight";
            // 
            // CalibrateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 265);
            this.Controls.Add(this.grpWeight);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Name = "CalibrateDialog";
            this.Text = "CalibrateDialog";
            this.Load += new System.EventHandler(this.CalibrateDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpWeight.ResumeLayout(false);
            this.grpWeight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMsgShow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblActWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpWeight;
    }
}