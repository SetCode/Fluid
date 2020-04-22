namespace Anda.Fluid.App
{
    partial class AddPattern1
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
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbPatternName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnOk);
            this.gbx1.Controls.Add(this.btnCancel);
            this.gbx1.Controls.Add(this.btnGoTo);
            this.gbx1.Controls.Add(this.btnSelect);
            this.gbx1.Controls.Add(this.tbOriginY);
            this.gbx1.Controls.Add(this.tbOriginX);
            this.gbx1.Controls.Add(this.tbPatternName);
            this.gbx1.Controls.Add(this.label2);
            this.gbx1.Controls.Add(this.label1);
            this.gbx1.Size = new System.Drawing.Size(270, 293);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Location = new System.Drawing.Point(66, 86);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(115, 22);
            this.tbOriginY.TabIndex = 4;
            this.tbOriginY.Text = "0";
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Location = new System.Drawing.Point(66, 58);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(115, 22);
            this.tbOriginX.TabIndex = 5;
            this.tbOriginX.Text = "0";
            // 
            // tbPatternName
            // 
            this.tbPatternName.Location = new System.Drawing.Point(66, 21);
            this.tbPatternName.Name = "tbPatternName";
            this.tbPatternName.Size = new System.Drawing.Size(196, 22);
            this.tbPatternName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Origin:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(187, 85);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 7;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(187, 57);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(187, 262);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(106, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddPattern1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "AddPattern1";
            this.Text = "AddPattern1";
            this.Load += new System.EventHandler(this.AddPattern1_Load);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.TextBox tbPatternName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}