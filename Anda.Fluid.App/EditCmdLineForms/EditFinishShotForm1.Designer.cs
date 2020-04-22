namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditFinishShotForm1
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
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnEditDotParams = new System.Windows.Forms.Button();
            this.comboBoxDotType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbLocationX);
            this.gbx2.Controls.Add(this.tbLocationY);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.btnSelect);
            this.gbx2.Controls.Add(this.btnEditDotParams);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.comboBoxDotType);
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(116, 35);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 10;
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(30, 35);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dot Location:";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(202, 34);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(283, 34);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 14;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnEditDotParams
            // 
            this.btnEditDotParams.Location = new System.Drawing.Point(190, 104);
            this.btnEditDotParams.Name = "btnEditDotParams";
            this.btnEditDotParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditDotParams.TabIndex = 18;
            this.btnEditDotParams.Text = "Edit";
            this.btnEditDotParams.UseVisualStyleBackColor = true;
            this.btnEditDotParams.Click += new System.EventHandler(this.btnEditDotParams_Click);
            // 
            // comboBoxDotType
            // 
            this.comboBoxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDotType.FormattingEnabled = true;
            this.comboBoxDotType.Location = new System.Drawing.Point(30, 105);
            this.comboBoxDotType.Name = "comboBoxDotType";
            this.comboBoxDotType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxDotType.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Dot Style:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(509, 627);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 24;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(509, 598);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 25;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // EditFinishShotForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditFinishShotForm1";
            this.Text = "EditFinishShotForm1";
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DoubleTextBox tbLocationY;
        private Controls.DoubleTextBox tbLocationX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnEditDotParams;
        private System.Windows.Forms.ComboBox comboBoxDotType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Button btnNextCmdLine;
    }
}