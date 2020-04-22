namespace Anda.Fluid.App.EditMark
{
    partial class EditBadMarkForm
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
            this.rbModelFind = new System.Windows.Forms.RadioButton();
            this.rbGrayScale = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbNgSkip = new System.Windows.Forms.RadioButton();
            this.rbOkSkip = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbModelFind
            // 
            this.rbModelFind.AutoSize = true;
            this.rbModelFind.Location = new System.Drawing.Point(18, 21);
            this.rbModelFind.Name = "rbModelFind";
            this.rbModelFind.Size = new System.Drawing.Size(92, 18);
            this.rbModelFind.TabIndex = 0;
            this.rbModelFind.TabStop = true;
            this.rbModelFind.Text = "ModelFind";
            this.rbModelFind.UseVisualStyleBackColor = true;
            // 
            // rbGrayScale
            // 
            this.rbGrayScale.AutoSize = true;
            this.rbGrayScale.Location = new System.Drawing.Point(137, 21);
            this.rbGrayScale.Name = "rbGrayScale";
            this.rbGrayScale.Size = new System.Drawing.Size(93, 18);
            this.rbGrayScale.TabIndex = 1;
            this.rbGrayScale.TabStop = true;
            this.rbGrayScale.Text = "GrayScale";
            this.rbGrayScale.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.rbModelFind);
            this.groupBox1.Controls.Add(this.rbGrayScale);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 54);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FindType";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(255, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbNgSkip);
            this.groupBox2.Controls.Add(this.rbOkSkip);
            this.groupBox2.Location = new System.Drawing.Point(391, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 57);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SkipMode";
            // 
            // rbNgSkip
            // 
            this.rbNgSkip.AutoSize = true;
            this.rbNgSkip.Location = new System.Drawing.Point(18, 21);
            this.rbNgSkip.Name = "rbNgSkip";
            this.rbNgSkip.Size = new System.Drawing.Size(78, 18);
            this.rbNgSkip.TabIndex = 0;
            this.rbNgSkip.TabStop = true;
            this.rbNgSkip.Text = "NG Skip";
            this.rbNgSkip.UseVisualStyleBackColor = true;
            // 
            // rbOkSkip
            // 
            this.rbOkSkip.AutoSize = true;
            this.rbOkSkip.Location = new System.Drawing.Point(123, 21);
            this.rbOkSkip.Name = "rbOkSkip";
            this.rbOkSkip.Size = new System.Drawing.Size(78, 18);
            this.rbOkSkip.TabIndex = 1;
            this.rbOkSkip.TabStop = true;
            this.rbOkSkip.Text = "OK Skip";
            this.rbOkSkip.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(640, 31);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(721, 31);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 700);
            this.panel1.TabIndex = 6;
            // 
            // EditBadMarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 782);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EditBadMarkForm";
            this.Text = "EditBadMark";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbModelFind;
        private System.Windows.Forms.RadioButton rbGrayScale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbNgSkip;
        private System.Windows.Forms.RadioButton rbOkSkip;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEdit;
    }
}