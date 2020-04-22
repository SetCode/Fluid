namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditDoForm1
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
            this.listBoxPatterns = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ckbReverse = new System.Windows.Forms.CheckBox();
            this.lblValveType = new System.Windows.Forms.Label();
            this.cbxValveType = new System.Windows.Forms.ComboBox();
            this.lblBoardNo = new System.Windows.Forms.Label();
            this.txtBoardNo = new Anda.Fluid.Controls.IntTextBox();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.listBoxPatterns);
            this.gbx1.Controls.Add(this.label1);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.txtBoardNo);
            this.gbx2.Controls.Add(this.lblBoardNo);
            this.gbx2.Controls.Add(this.cbxValveType);
            this.gbx2.Controls.Add(this.lblValveType);
            this.gbx2.Controls.Add(this.ckbReverse);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.tbOriginX);
            this.gbx2.Controls.Add(this.btnSelect);
            this.gbx2.Controls.Add(this.tbOriginY);
            this.gbx2.Controls.Add(this.btnGoTo);
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(10, 35);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.Size = new System.Drawing.Size(252, 242);
            this.listBoxPatterns.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pattern list:";
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Location = new System.Drawing.Point(208, 47);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(67, 22);
            this.tbOriginY.TabIndex = 7;
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Location = new System.Drawing.Point(135, 47);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(67, 22);
            this.tbOriginX.TabIndex = 8;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(362, 46);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 5;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(281, 46);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Origin:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(362, 196);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(281, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckbReverse
            // 
            this.ckbReverse.AutoSize = true;
            this.ckbReverse.Location = new System.Drawing.Point(135, 90);
            this.ckbReverse.Name = "ckbReverse";
            this.ckbReverse.Size = new System.Drawing.Size(80, 18);
            this.ckbReverse.TabIndex = 11;
            this.ckbReverse.Text = "Reverse";
            this.ckbReverse.UseVisualStyleBackColor = true;
            // 
            // lblValveType
            // 
            this.lblValveType.Location = new System.Drawing.Point(79, 126);
            this.lblValveType.Name = "lblValveType";
            this.lblValveType.Size = new System.Drawing.Size(97, 19);
            this.lblValveType.TabIndex = 12;
            this.lblValveType.Text = "Valve Type :";
            // 
            // cbxValveType
            // 
            this.cbxValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxValveType.FormattingEnabled = true;
            this.cbxValveType.Location = new System.Drawing.Point(182, 123);
            this.cbxValveType.Name = "cbxValveType";
            this.cbxValveType.Size = new System.Drawing.Size(121, 22);
            this.cbxValveType.TabIndex = 13;
            // 
            // lblBoardNo
            // 
            this.lblBoardNo.AutoSize = true;
            this.lblBoardNo.Location = new System.Drawing.Point(79, 166);
            this.lblBoardNo.Name = "lblBoardNo";
            this.lblBoardNo.Size = new System.Drawing.Size(77, 14);
            this.lblBoardNo.TabIndex = 14;
            this.lblBoardNo.Text = "所属穴位号:";
            // 
            // txtBoardNo
            // 
            this.txtBoardNo.BackColor = System.Drawing.Color.White;
            this.txtBoardNo.Location = new System.Drawing.Point(162, 163);
            this.txtBoardNo.Name = "txtBoardNo";
            this.txtBoardNo.Size = new System.Drawing.Size(100, 22);
            this.txtBoardNo.TabIndex = 15;
            // 
            // EditDoForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditDoForm1";
            this.Text = "EditDoForm1";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.Label label1;
        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ckbReverse;
        private System.Windows.Forms.Label lblValveType;
        private System.Windows.Forms.ComboBox cbxValveType;
        private System.Windows.Forms.Label lblBoardNo;
        private Controls.IntTextBox txtBoardNo;
    }
}