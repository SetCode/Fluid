namespace Anda.Fluid.Domain.SoftFunction.LotControl
{
    partial class LotControlSettingForm
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
            this.tbxLotID = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblCutPos = new System.Windows.Forms.Label();
            this.tbxLotStartPos = new Anda.Fluid.Controls.IntTextBox();
            this.tbxLotEndPos = new Anda.Fluid.Controls.IntTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set Lot ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbxLotID
            // 
            this.tbxLotID.Location = new System.Drawing.Point(129, 14);
            this.tbxLotID.Name = "tbxLotID";
            this.tbxLotID.Size = new System.Drawing.Size(234, 21);
            this.tbxLotID.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(288, 52);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblCutPos
            // 
            this.lblCutPos.Location = new System.Drawing.Point(12, 57);
            this.lblCutPos.Name = "lblCutPos";
            this.lblCutPos.Size = new System.Drawing.Size(100, 18);
            this.lblCutPos.TabIndex = 5;
            this.lblCutPos.Text = "LotId Position:";
            this.lblCutPos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbxLotStartPos
            // 
            this.tbxLotStartPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbxLotStartPos.Location = new System.Drawing.Point(129, 54);
            this.tbxLotStartPos.Name = "tbxLotStartPos";
            this.tbxLotStartPos.Size = new System.Drawing.Size(34, 21);
            this.tbxLotStartPos.TabIndex = 6;
            // 
            // tbxLotEndPos
            // 
            this.tbxLotEndPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbxLotEndPos.Location = new System.Drawing.Point(186, 54);
            this.tbxLotEndPos.Name = "tbxLotEndPos";
            this.tbxLotEndPos.Size = new System.Drawing.Size(34, 21);
            this.tbxLotEndPos.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "-";
            // 
            // LotControlSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 88);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxLotEndPos);
            this.Controls.Add(this.tbxLotStartPos);
            this.Controls.Add(this.lblCutPos);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbxLotID);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LotControlSettingForm";
            this.Text = "LotControlSettingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxLotID;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblCutPos;
        private Controls.IntTextBox tbxLotStartPos;
        private Controls.IntTextBox tbxLotEndPos;
        private System.Windows.Forms.Label label2;
    }
}