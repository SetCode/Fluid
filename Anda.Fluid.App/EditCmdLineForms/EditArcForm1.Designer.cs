namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditArcForm1
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
            this.gbxArcMethod = new System.Windows.Forms.GroupBox();
            this.rbCenter = new System.Windows.Forms.RadioButton();
            this.rbSME = new System.Windows.Forms.RadioButton();
            this.btnCenterGoTo = new System.Windows.Forms.Button();
            this.btnEndGoTo = new System.Windows.Forms.Button();
            this.btnMiddleGoTo = new System.Windows.Forms.Button();
            this.btnStartGoTo = new System.Windows.Forms.Button();
            this.btnSelectCenter = new System.Windows.Forms.Button();
            this.btnSelectMiddle = new System.Windows.Forms.Button();
            this.btnSelectEnd = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCenterY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbCenterX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbMiddleY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbMiddleX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblCenter = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblMiddle = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.lblLineStyle = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.tbDegree = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblDegree = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.gbxArcMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnEditLineParams);
            this.gbx1.Controls.Add(this.comboBoxLineType);
            this.gbx1.Controls.Add(this.lblLineStyle);
            this.gbx1.Controls.Add(this.label10);
            this.gbx1.Controls.Add(this.tbWeight);
            this.gbx1.Controls.Add(this.cbWeightControl);
            this.gbx1.Controls.Add(this.gbxArcMethod);
            this.gbx1.Size = new System.Drawing.Size(270, 258);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnCenterGoTo);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnEndGoTo);
            this.gbx2.Controls.Add(this.btnMiddleGoTo);
            this.gbx2.Controls.Add(this.btnStartGoTo);
            this.gbx2.Controls.Add(this.btnSelectCenter);
            this.gbx2.Controls.Add(this.btnSelectMiddle);
            this.gbx2.Controls.Add(this.tbDegree);
            this.gbx2.Controls.Add(this.btnSelectEnd);
            this.gbx2.Controls.Add(this.lblDegree);
            this.gbx2.Controls.Add(this.btnSelectStart);
            this.gbx2.Controls.Add(this.label9);
            this.gbx2.Controls.Add(this.label8);
            this.gbx2.Controls.Add(this.label7);
            this.gbx2.Controls.Add(this.label6);
            this.gbx2.Controls.Add(this.tbCenterY);
            this.gbx2.Controls.Add(this.tbCenterX);
            this.gbx2.Controls.Add(this.tbEndY);
            this.gbx2.Controls.Add(this.tbEndX);
            this.gbx2.Controls.Add(this.tbMiddleY);
            this.gbx2.Controls.Add(this.tbMiddleX);
            this.gbx2.Controls.Add(this.tbStartY);
            this.gbx2.Controls.Add(this.tbStartX);
            this.gbx2.Controls.Add(this.lblCenter);
            this.gbx2.Controls.Add(this.lblEnd);
            this.gbx2.Controls.Add(this.lblMiddle);
            this.gbx2.Controls.Add(this.lblStart);
            // 
            // gbxArcMethod
            // 
            this.gbxArcMethod.Controls.Add(this.rbCenter);
            this.gbxArcMethod.Controls.Add(this.rbSME);
            this.gbxArcMethod.Location = new System.Drawing.Point(7, 21);
            this.gbxArcMethod.Name = "gbxArcMethod";
            this.gbxArcMethod.Size = new System.Drawing.Size(255, 50);
            this.gbxArcMethod.TabIndex = 5;
            this.gbxArcMethod.TabStop = false;
            this.gbxArcMethod.Text = "Method";
            // 
            // rbCenter
            // 
            this.rbCenter.AutoSize = true;
            this.rbCenter.Location = new System.Drawing.Point(119, 22);
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.Size = new System.Drawing.Size(69, 18);
            this.rbCenter.TabIndex = 0;
            this.rbCenter.Text = "Center";
            this.rbCenter.UseVisualStyleBackColor = true;
            this.rbCenter.CheckedChanged += new System.EventHandler(this.rbCenter_CheckedChanged);
            // 
            // rbSME
            // 
            this.rbSME.AutoSize = true;
            this.rbSME.Location = new System.Drawing.Point(26, 22);
            this.rbSME.Name = "rbSME";
            this.rbSME.Size = new System.Drawing.Size(53, 18);
            this.rbSME.TabIndex = 0;
            this.rbSME.Text = "SME";
            this.rbSME.UseVisualStyleBackColor = true;
            this.rbSME.CheckedChanged += new System.EventHandler(this.rbSME_CheckedChanged);
            // 
            // btnCenterGoTo
            // 
            this.btnCenterGoTo.Location = new System.Drawing.Point(284, 132);
            this.btnCenterGoTo.Name = "btnCenterGoTo";
            this.btnCenterGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnCenterGoTo.TabIndex = 32;
            this.btnCenterGoTo.Text = "Go To";
            this.btnCenterGoTo.UseVisualStyleBackColor = true;
            this.btnCenterGoTo.Click += new System.EventHandler(this.btnCenterGoTo_Click);
            // 
            // btnEndGoTo
            // 
            this.btnEndGoTo.Location = new System.Drawing.Point(284, 95);
            this.btnEndGoTo.Name = "btnEndGoTo";
            this.btnEndGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnEndGoTo.TabIndex = 31;
            this.btnEndGoTo.Text = "Go To";
            this.btnEndGoTo.UseVisualStyleBackColor = true;
            this.btnEndGoTo.Click += new System.EventHandler(this.btnEndGoTo_Click);
            // 
            // btnMiddleGoTo
            // 
            this.btnMiddleGoTo.Location = new System.Drawing.Point(284, 58);
            this.btnMiddleGoTo.Name = "btnMiddleGoTo";
            this.btnMiddleGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnMiddleGoTo.TabIndex = 30;
            this.btnMiddleGoTo.Text = "Go To";
            this.btnMiddleGoTo.UseVisualStyleBackColor = true;
            this.btnMiddleGoTo.Click += new System.EventHandler(this.btnMiddleGoTo_Click);
            // 
            // btnStartGoTo
            // 
            this.btnStartGoTo.Location = new System.Drawing.Point(284, 25);
            this.btnStartGoTo.Name = "btnStartGoTo";
            this.btnStartGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnStartGoTo.TabIndex = 29;
            this.btnStartGoTo.Text = "Go To";
            this.btnStartGoTo.UseVisualStyleBackColor = true;
            this.btnStartGoTo.Click += new System.EventHandler(this.btnStartGoTo_Click);
            // 
            // btnSelectCenter
            // 
            this.btnSelectCenter.Location = new System.Drawing.Point(203, 132);
            this.btnSelectCenter.Name = "btnSelectCenter";
            this.btnSelectCenter.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCenter.TabIndex = 28;
            this.btnSelectCenter.Text = "Teach";
            this.btnSelectCenter.UseVisualStyleBackColor = true;
            this.btnSelectCenter.Click += new System.EventHandler(this.btnSelectCenter_Click);
            // 
            // btnSelectMiddle
            // 
            this.btnSelectMiddle.Location = new System.Drawing.Point(203, 57);
            this.btnSelectMiddle.Name = "btnSelectMiddle";
            this.btnSelectMiddle.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMiddle.TabIndex = 27;
            this.btnSelectMiddle.Text = "Teach";
            this.btnSelectMiddle.UseVisualStyleBackColor = true;
            this.btnSelectMiddle.Click += new System.EventHandler(this.btnSelectMiddle_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.Location = new System.Drawing.Point(203, 95);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 26;
            this.btnSelectEnd.Text = "Teach";
            this.btnSelectEnd.UseVisualStyleBackColor = true;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(203, 24);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 25;
            this.btnSelectStart.Text = "Teach";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 14);
            this.label9.TabIndex = 23;
            this.label9.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 14);
            this.label8.TabIndex = 22;
            this.label8.Text = ":";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 14);
            this.label6.TabIndex = 21;
            this.label6.Text = ":";
            // 
            // tbCenterY
            // 
            this.tbCenterY.BackColor = System.Drawing.Color.White;
            this.tbCenterY.Location = new System.Drawing.Point(143, 134);
            this.tbCenterY.Name = "tbCenterY";
            this.tbCenterY.Size = new System.Drawing.Size(54, 22);
            this.tbCenterY.TabIndex = 20;
            this.tbCenterY.TextChanged += new System.EventHandler(this.tbCenterY_TextChanged);
            // 
            // tbCenterX
            // 
            this.tbCenterX.BackColor = System.Drawing.Color.White;
            this.tbCenterX.Location = new System.Drawing.Point(73, 134);
            this.tbCenterX.Name = "tbCenterX";
            this.tbCenterX.Size = new System.Drawing.Size(54, 22);
            this.tbCenterX.TabIndex = 19;
            this.tbCenterX.TextChanged += new System.EventHandler(this.tbCenterX_TextChanged);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.White;
            this.tbEndY.Location = new System.Drawing.Point(143, 97);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(54, 22);
            this.tbEndY.TabIndex = 18;
            this.tbEndY.TextChanged += new System.EventHandler(this.tbEndY_TextChanged);
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.White;
            this.tbEndX.Location = new System.Drawing.Point(73, 97);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(54, 22);
            this.tbEndX.TabIndex = 17;
            this.tbEndX.TextChanged += new System.EventHandler(this.tbEndX_TextChanged);
            // 
            // tbMiddleY
            // 
            this.tbMiddleY.BackColor = System.Drawing.Color.White;
            this.tbMiddleY.Location = new System.Drawing.Point(143, 59);
            this.tbMiddleY.Name = "tbMiddleY";
            this.tbMiddleY.Size = new System.Drawing.Size(54, 22);
            this.tbMiddleY.TabIndex = 16;
            this.tbMiddleY.TextChanged += new System.EventHandler(this.tbMiddleY_TextChanged);
            // 
            // tbMiddleX
            // 
            this.tbMiddleX.BackColor = System.Drawing.Color.White;
            this.tbMiddleX.Location = new System.Drawing.Point(73, 59);
            this.tbMiddleX.Name = "tbMiddleX";
            this.tbMiddleX.Size = new System.Drawing.Size(54, 22);
            this.tbMiddleX.TabIndex = 15;
            this.tbMiddleX.TextChanged += new System.EventHandler(this.tbMiddleX_TextChanged);
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.White;
            this.tbStartY.Location = new System.Drawing.Point(143, 26);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(54, 22);
            this.tbStartY.TabIndex = 14;
            this.tbStartY.TextChanged += new System.EventHandler(this.tbStartY_TextChanged);
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.SystemColors.Window;
            this.tbStartX.Location = new System.Drawing.Point(73, 26);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(54, 22);
            this.tbStartX.TabIndex = 13;
            this.tbStartX.TextChanged += new System.EventHandler(this.tbStartX_TextChanged);
            // 
            // lblCenter
            // 
            this.lblCenter.Location = new System.Drawing.Point(12, 137);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(60, 14);
            this.lblCenter.TabIndex = 11;
            this.lblCenter.Text = "Center:";
            this.lblCenter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(12, 100);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(60, 14);
            this.lblEnd.TabIndex = 10;
            this.lblEnd.Text = "End:";
            this.lblEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMiddle
            // 
            this.lblMiddle.Location = new System.Drawing.Point(12, 62);
            this.lblMiddle.Name = "lblMiddle";
            this.lblMiddle.Size = new System.Drawing.Size(60, 14);
            this.lblMiddle.TabIndex = 12;
            this.lblMiddle.Text = "Middle:";
            this.lblMiddle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(12, 30);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(60, 14);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "Start:";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.Location = new System.Drawing.Point(200, 83);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(62, 23);
            this.btnEditLineParams.TabIndex = 24;
            this.btnEditLineParams.Text = "Edit";
            this.btnEditLineParams.UseVisualStyleBackColor = true;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(92, 84);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(75, 22);
            this.comboBoxLineType.TabIndex = 23;
            // 
            // lblLineStyle
            // 
            this.lblLineStyle.AutoSize = true;
            this.lblLineStyle.Location = new System.Drawing.Point(8, 87);
            this.lblLineStyle.Name = "lblLineStyle";
            this.lblLineStyle.Size = new System.Drawing.Size(78, 14);
            this.lblLineStyle.TabIndex = 22;
            this.lblLineStyle.Text = "Line Style:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(216, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 14);
            this.label10.TabIndex = 21;
            this.label10.Text = "mg";
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(138, 124);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(72, 22);
            this.tbWeight.TabIndex = 20;
            this.tbWeight.Text = "0";
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(11, 126);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(121, 18);
            this.cbWeightControl.TabIndex = 19;
            this.cbWeightControl.Text = "weight control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // tbDegree
            // 
            this.tbDegree.BackColor = System.Drawing.Color.White;
            this.tbDegree.Location = new System.Drawing.Point(73, 172);
            this.tbDegree.Name = "tbDegree";
            this.tbDegree.Size = new System.Drawing.Size(54, 22);
            this.tbDegree.TabIndex = 18;
            this.tbDegree.TextChanged += new System.EventHandler(this.tbDegree_TextChanged);
            // 
            // lblDegree
            // 
            this.lblDegree.Location = new System.Drawing.Point(12, 175);
            this.lblDegree.Name = "lblDegree";
            this.lblDegree.Size = new System.Drawing.Size(60, 14);
            this.lblDegree.TabIndex = 17;
            this.lblDegree.Text = "Degree:";
            this.lblDegree.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(509, 627);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 35;
            this.btnNextCmdLine.Text = "Next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(509, 598);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 36;
            this.btnLastCmdLine.Text = "Last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // EditArcForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditArcForm1";
            this.Text = "EditArcForm1";
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.gbxArcMethod.ResumeLayout(false);
            this.gbxArcMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxArcMethod;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbSME;
        private System.Windows.Forms.Button btnCenterGoTo;
        private System.Windows.Forms.Button btnEndGoTo;
        private System.Windows.Forms.Button btnMiddleGoTo;
        private System.Windows.Forms.Button btnStartGoTo;
        private System.Windows.Forms.Button btnSelectCenter;
        private System.Windows.Forms.Button btnSelectMiddle;
        private System.Windows.Forms.Button btnSelectEnd;
        private System.Windows.Forms.Button btnSelectStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private Controls.DoubleTextBox tbCenterY;
        private Controls.DoubleTextBox tbCenterX;
        private Controls.DoubleTextBox tbEndY;
        private Controls.DoubleTextBox tbEndX;
        private Controls.DoubleTextBox tbMiddleY;
        private Controls.DoubleTextBox tbMiddleX;
        private Controls.DoubleTextBox tbStartY;
        private Controls.DoubleTextBox tbStartX;
        private System.Windows.Forms.Label lblCenter;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblMiddle;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Button btnEditLineParams;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Label lblLineStyle;
        private System.Windows.Forms.Label label10;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private Controls.DoubleTextBox tbDegree;
        private System.Windows.Forms.Label lblDegree;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Button btnNextCmdLine;
    }
}