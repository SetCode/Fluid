namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditLineWeightForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdoWhole = new System.Windows.Forms.RadioButton();
            this.rdoEach = new System.Windows.Forms.RadioButton();
            this.nudWhole = new System.Windows.Forms.NumericUpDown();
            this.nudEach = new System.Windows.Forms.NumericUpDown();
            this.btnApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAllLinesStyle = new System.Windows.Forms.Label();
            this.cbxAllLineStyle = new System.Windows.Forms.ComboBox();
            this.btnEditLineStyle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEach)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(-1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 501);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(597, 535);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(516, 535);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdoWhole
            // 
            this.rdoWhole.AutoSize = true;
            this.rdoWhole.Location = new System.Drawing.Point(12, 7);
            this.rdoWhole.Name = "rdoWhole";
            this.rdoWhole.Size = new System.Drawing.Size(65, 18);
            this.rdoWhole.TabIndex = 3;
            this.rdoWhole.TabStop = true;
            this.rdoWhole.Text = "whole";
            this.rdoWhole.UseVisualStyleBackColor = true;
            this.rdoWhole.CheckedChanged += new System.EventHandler(this.rBtnWhole_CheckedChanged);
            // 
            // rdoEach
            // 
            this.rdoEach.AutoSize = true;
            this.rdoEach.Location = new System.Drawing.Point(259, 7);
            this.rdoEach.Name = "rdoEach";
            this.rdoEach.Size = new System.Drawing.Size(56, 18);
            this.rdoEach.TabIndex = 4;
            this.rdoEach.TabStop = true;
            this.rdoEach.Text = "each";
            this.rdoEach.UseVisualStyleBackColor = true;
            this.rdoEach.CheckedChanged += new System.EventHandler(this.rBtnEach_CheckedChanged);
            // 
            // nudWhole
            // 
            this.nudWhole.Location = new System.Drawing.Point(83, 5);
            this.nudWhole.Name = "nudWhole";
            this.nudWhole.Size = new System.Drawing.Size(120, 22);
            this.nudWhole.TabIndex = 5;
            // 
            // nudEach
            // 
            this.nudEach.Location = new System.Drawing.Point(321, 5);
            this.nudEach.Name = "nudEach";
            this.nudEach.Size = new System.Drawing.Size(120, 22);
            this.nudEach.TabIndex = 6;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(480, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "mg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(447, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "mg";
            // 
            // lblAllLinesStyle
            // 
            this.lblAllLinesStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAllLinesStyle.AutoSize = true;
            this.lblAllLinesStyle.Location = new System.Drawing.Point(12, 539);
            this.lblAllLinesStyle.Name = "lblAllLinesStyle";
            this.lblAllLinesStyle.Size = new System.Drawing.Size(101, 14);
            this.lblAllLinesStyle.TabIndex = 10;
            this.lblAllLinesStyle.Text = "All Lines Style";
            // 
            // cbxAllLineStyle
            // 
            this.cbxAllLineStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxAllLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAllLineStyle.FormattingEnabled = true;
            this.cbxAllLineStyle.Location = new System.Drawing.Point(119, 536);
            this.cbxAllLineStyle.Name = "cbxAllLineStyle";
            this.cbxAllLineStyle.Size = new System.Drawing.Size(90, 22);
            this.cbxAllLineStyle.TabIndex = 11;
            // 
            // btnEditLineStyle
            // 
            this.btnEditLineStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditLineStyle.Location = new System.Drawing.Point(215, 535);
            this.btnEditLineStyle.Name = "btnEditLineStyle";
            this.btnEditLineStyle.Size = new System.Drawing.Size(75, 23);
            this.btnEditLineStyle.TabIndex = 12;
            this.btnEditLineStyle.Text = "edit";
            this.btnEditLineStyle.UseVisualStyleBackColor = true;
            this.btnEditLineStyle.Click += new System.EventHandler(this.btnEditLineStyle_Click);
            // 
            // EditLineWeightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.btnEditLineStyle);
            this.Controls.Add(this.cbxAllLineStyle);
            this.Controls.Add(this.lblAllLinesStyle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.nudEach);
            this.Controls.Add(this.nudWhole);
            this.Controls.Add(this.rdoEach);
            this.Controls.Add(this.rdoWhole);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EditLineWeightForm";
            this.Text = "EditLineWeightForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudWhole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdoWhole;
        private System.Windows.Forms.RadioButton rdoEach;
        private System.Windows.Forms.NumericUpDown nudWhole;
        private System.Windows.Forms.NumericUpDown nudEach;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAllLinesStyle;
        private System.Windows.Forms.ComboBox cbxAllLineStyle;
        private System.Windows.Forms.Button btnEditLineStyle;
    }
}