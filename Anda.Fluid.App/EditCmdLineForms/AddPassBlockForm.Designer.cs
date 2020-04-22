using Anda.Fluid.Controls;

namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class AddPassBlockForm
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
            this.lblIndexFrom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStartIndex = new Anda.Fluid.Controls.IntTextBox();
            this.tbEndIndex = new Anda.Fluid.Controls.IntTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIndexFrom
            // 
            this.lblIndexFrom.AutoSize = true;
            this.lblIndexFrom.Location = new System.Drawing.Point(40, 28);
            this.lblIndexFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIndexFrom.Name = "lblIndexFrom";
            this.lblIndexFrom.Size = new System.Drawing.Size(80, 14);
            this.lblIndexFrom.TabIndex = 0;
            this.lblIndexFrom.Text = "Index from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "--";
            // 
            // tbStartIndex
            // 
            this.tbStartIndex.BackColor = System.Drawing.Color.White;
            this.tbStartIndex.Location = new System.Drawing.Point(135, 24);
            this.tbStartIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbStartIndex.Name = "tbStartIndex";
            this.tbStartIndex.Size = new System.Drawing.Size(53, 22);
            this.tbStartIndex.TabIndex = 1;
            // 
            // tbEndIndex
            // 
            this.tbEndIndex.BackColor = System.Drawing.Color.White;
            this.tbEndIndex.Location = new System.Drawing.Point(229, 24);
            this.tbEndIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbEndIndex.Name = "tbEndIndex";
            this.tbEndIndex.Size = new System.Drawing.Size(53, 22);
            this.tbEndIndex.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(43, 82);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(184, 82);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // AddPassBlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 133);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbEndIndex);
            this.Controls.Add(this.tbStartIndex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIndexFrom);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AddPassBlockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add pass block";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIndexFrom;
        private System.Windows.Forms.Label label2;
        private IntTextBox tbStartIndex;
        private IntTextBox tbEndIndex;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}