namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditDoMultipassForm1
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
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblValveType = new System.Windows.Forms.Label();
            this.cbxValveType = new System.Windows.Forms.ComboBox();
            this.txtBoardNo = new Anda.Fluid.Controls.IntTextBox();
            this.lblBoardNo = new System.Windows.Forms.Label();
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
            this.gbx2.Controls.Add(this.cbxValveType);
            this.gbx2.Controls.Add(this.lblBoardNo);
            this.gbx2.Controls.Add(this.lblValveType);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.btnSelect);
            this.gbx2.Controls.Add(this.tbOriginY);
            this.gbx2.Controls.Add(this.tbOriginX);
            this.gbx2.Controls.Add(this.label2);
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(7, 35);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.Size = new System.Drawing.Size(255, 242);
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
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(346, 46);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 8;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(265, 46);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 9;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Location = new System.Drawing.Point(185, 47);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(74, 22);
            this.tbOriginY.TabIndex = 6;
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Location = new System.Drawing.Point(105, 47);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(74, 22);
            this.tbOriginX.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Origin:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(346, 201);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(265, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblValveType
            // 
            this.lblValveType.Location = new System.Drawing.Point(46, 93);
            this.lblValveType.Name = "lblValveType";
            this.lblValveType.Size = new System.Drawing.Size(98, 18);
            this.lblValveType.TabIndex = 12;
            this.lblValveType.Text = "Valve Type :";
            // 
            // cbxValveType
            // 
            this.cbxValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxValveType.FormattingEnabled = true;
            this.cbxValveType.Location = new System.Drawing.Point(150, 90);
            this.cbxValveType.Name = "cbxValveType";
            this.cbxValveType.Size = new System.Drawing.Size(121, 22);
            this.cbxValveType.TabIndex = 13;
            // 
            // txtBoardNo
            // 
            this.txtBoardNo.BackColor = System.Drawing.Color.White;
            this.txtBoardNo.Location = new System.Drawing.Point(132, 131);
            this.txtBoardNo.Name = "txtBoardNo";
            this.txtBoardNo.Size = new System.Drawing.Size(100, 22);
            this.txtBoardNo.TabIndex = 17;
            // 
            // lblBoardNo
            // 
            this.lblBoardNo.AutoSize = true;
            this.lblBoardNo.Location = new System.Drawing.Point(49, 134);
            this.lblBoardNo.Name = "lblBoardNo";
            this.lblBoardNo.Size = new System.Drawing.Size(77, 14);
            this.lblBoardNo.TabIndex = 16;
            this.lblBoardNo.Text = "所属穴位号:";
            // 
            // EditDoMultipassForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditDoMultipassForm1";
            this.Text = "EditDoMultipassForm1";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblValveType;
        private System.Windows.Forms.ComboBox cbxValveType;
        private Controls.IntTextBox txtBoardNo;
        private System.Windows.Forms.Label lblBoardNo;
    }
}