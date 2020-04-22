namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditDotForm1
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
            this.lblDotLocation = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnEditDotParams = new System.Windows.Forms.Button();
            this.comboBoxDotType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.ckbShotNums = new System.Windows.Forms.CheckBox();
            this.tbShots = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblTiltType = new System.Windows.Forms.Label();
            this.cbTiltType = new System.Windows.Forms.ComboBox();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.cbTiltType);
            this.gbx2.Controls.Add(this.lblTiltType);
            this.gbx2.Controls.Add(this.tbShots);
            this.gbx2.Controls.Add(this.ckbShotNums);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.lblDotLocation);
            this.gbx2.Controls.Add(this.tbLocationX);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.tbLocationY);
            this.gbx2.Controls.Add(this.tbWeight);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.cbWeightControl);
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
            // lblDotLocation
            // 
            this.lblDotLocation.AutoSize = true;
            this.lblDotLocation.Location = new System.Drawing.Point(9, 18);
            this.lblDotLocation.Name = "lblDotLocation";
            this.lblDotLocation.Size = new System.Drawing.Size(95, 14);
            this.lblDotLocation.TabIndex = 8;
            this.lblDotLocation.Text = "Dot Location:";
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
            this.btnEditDotParams.Location = new System.Drawing.Point(190, 88);
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
            this.comboBoxDotType.Location = new System.Drawing.Point(30, 89);
            this.comboBoxDotType.Name = "comboBoxDotType";
            this.comboBoxDotType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxDotType.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 72);
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
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(12, 131);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 19;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(143, 129);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(63, 22);
            this.tbWeight.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "mg";
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
            // ckbShotNums
            // 
            this.ckbShotNums.AutoSize = true;
            this.ckbShotNums.Location = new System.Drawing.Point(12, 166);
            this.ckbShotNums.Name = "ckbShotNums";
            this.ckbShotNums.Size = new System.Drawing.Size(93, 18);
            this.ckbShotNums.TabIndex = 24;
            this.ckbShotNums.Text = "ShotNums";
            this.ckbShotNums.UseVisualStyleBackColor = true;
            this.ckbShotNums.CheckedChanged += new System.EventHandler(this.ckbShotNums_CheckedChanged);
            // 
            // tbShots
            // 
            this.tbShots.BackColor = System.Drawing.Color.White;
            this.tbShots.Location = new System.Drawing.Point(143, 164);
            this.tbShots.Name = "tbShots";
            this.tbShots.Size = new System.Drawing.Size(63, 22);
            this.tbShots.TabIndex = 25;
            // 
            // lblTiltType
            // 
            this.lblTiltType.AutoSize = true;
            this.lblTiltType.Location = new System.Drawing.Point(304, 72);
            this.lblTiltType.Name = "lblTiltType";
            this.lblTiltType.Size = new System.Drawing.Size(64, 14);
            this.lblTiltType.TabIndex = 26;
            this.lblTiltType.Text = "倾斜类型:";
            // 
            // cbTiltType
            // 
            this.cbTiltType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTiltType.FormattingEnabled = true;
            this.cbTiltType.Location = new System.Drawing.Point(346, 89);
            this.cbTiltType.Name = "cbTiltType";
            this.cbTiltType.Size = new System.Drawing.Size(105, 22);
            this.cbTiltType.TabIndex = 27;
            // 
            // EditDotForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditDotForm1";
            this.Text = "EditDotForm1";
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
        private System.Windows.Forms.Label lblDotLocation;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnEditDotParams;
        private System.Windows.Forms.ComboBox comboBoxDotType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Button btnNextCmdLine;
        private Controls.DoubleTextBox tbShots;
        private System.Windows.Forms.CheckBox ckbShotNums;
        private System.Windows.Forms.ComboBox cbTiltType;
        private System.Windows.Forms.Label lblTiltType;
    }
}