namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditCircleForm1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCenter = new System.Windows.Forms.RadioButton();
            this.rbSME = new System.Windows.Forms.RadioButton();
            this.btnCenterGoTo = new System.Windows.Forms.Button();
            this.btnSelectCenter = new System.Windows.Forms.Button();
            this.btnEndGoTo = new System.Windows.Forms.Button();
            this.btnSelectEnd = new System.Windows.Forms.Button();
            this.btnMiddleGoTo = new System.Windows.Forms.Button();
            this.tbCenterY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelectMiddle = new System.Windows.Forms.Button();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnStartGoTo = new System.Windows.Forms.Button();
            this.tbCenterX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbMiddleY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMiddleX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboBoxDegree = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.cbWeightControl);
            this.gbx1.Controls.Add(this.comboBoxLineType);
            this.gbx1.Controls.Add(this.btnEditLineParams);
            this.gbx1.Controls.Add(this.tbWeight);
            this.gbx1.Controls.Add(this.label7);
            this.gbx1.Controls.Add(this.label6);
            this.gbx1.Controls.Add(this.groupBox1);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.comboBoxDegree);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnCenterGoTo);
            this.gbx2.Controls.Add(this.btnSelectCenter);
            this.gbx2.Controls.Add(this.btnEndGoTo);
            this.gbx2.Controls.Add(this.btnSelectEnd);
            this.gbx2.Controls.Add(this.btnMiddleGoTo);
            this.gbx2.Controls.Add(this.tbCenterY);
            this.gbx2.Controls.Add(this.btnSelectMiddle);
            this.gbx2.Controls.Add(this.tbEndY);
            this.gbx2.Controls.Add(this.btnStartGoTo);
            this.gbx2.Controls.Add(this.tbCenterX);
            this.gbx2.Controls.Add(this.tbMiddleY);
            this.gbx2.Controls.Add(this.tbEndX);
            this.gbx2.Controls.Add(this.btnSelectStart);
            this.gbx2.Controls.Add(this.label5);
            this.gbx2.Controls.Add(this.label4);
            this.gbx2.Controls.Add(this.tbMiddleX);
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbStartY);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.tbStartX);
            this.gbx2.Controls.Add(this.label1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCenter);
            this.groupBox1.Controls.Add(this.rbSME);
            this.groupBox1.Location = new System.Drawing.Point(7, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // rbCenter
            // 
            this.rbCenter.AutoSize = true;
            this.rbCenter.Location = new System.Drawing.Point(119, 19);
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.Size = new System.Drawing.Size(69, 18);
            this.rbCenter.TabIndex = 0;
            this.rbCenter.TabStop = true;
            this.rbCenter.Text = "Center";
            this.rbCenter.UseVisualStyleBackColor = true;
            this.rbCenter.CheckedChanged += new System.EventHandler(this.rbCenter_CheckedChanged);
            // 
            // rbSME
            // 
            this.rbSME.AutoSize = true;
            this.rbSME.Location = new System.Drawing.Point(29, 19);
            this.rbSME.Name = "rbSME";
            this.rbSME.Size = new System.Drawing.Size(53, 18);
            this.rbSME.TabIndex = 0;
            this.rbSME.TabStop = true;
            this.rbSME.Text = "SME";
            this.rbSME.UseVisualStyleBackColor = true;
            this.rbSME.CheckedChanged += new System.EventHandler(this.rbSME_CheckedChanged);
            // 
            // btnCenterGoTo
            // 
            this.btnCenterGoTo.Location = new System.Drawing.Point(287, 120);
            this.btnCenterGoTo.Name = "btnCenterGoTo";
            this.btnCenterGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnCenterGoTo.TabIndex = 23;
            this.btnCenterGoTo.Text = "Go To";
            this.btnCenterGoTo.UseVisualStyleBackColor = true;
            this.btnCenterGoTo.Click += new System.EventHandler(this.btnCenterGoTo_Click);
            // 
            // btnSelectCenter
            // 
            this.btnSelectCenter.Location = new System.Drawing.Point(206, 120);
            this.btnSelectCenter.Name = "btnSelectCenter";
            this.btnSelectCenter.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCenter.TabIndex = 21;
            this.btnSelectCenter.Text = "Teach";
            this.btnSelectCenter.UseVisualStyleBackColor = true;
            this.btnSelectCenter.Click += new System.EventHandler(this.btnSelectCenter_Click);
            // 
            // btnEndGoTo
            // 
            this.btnEndGoTo.Location = new System.Drawing.Point(287, 87);
            this.btnEndGoTo.Name = "btnEndGoTo";
            this.btnEndGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnEndGoTo.TabIndex = 20;
            this.btnEndGoTo.Text = "Go To";
            this.btnEndGoTo.UseVisualStyleBackColor = true;
            this.btnEndGoTo.Click += new System.EventHandler(this.btnEndGoTo_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.Location = new System.Drawing.Point(206, 87);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 19;
            this.btnSelectEnd.Text = "Teach";
            this.btnSelectEnd.UseVisualStyleBackColor = true;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnMiddleGoTo
            // 
            this.btnMiddleGoTo.Location = new System.Drawing.Point(287, 53);
            this.btnMiddleGoTo.Name = "btnMiddleGoTo";
            this.btnMiddleGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnMiddleGoTo.TabIndex = 18;
            this.btnMiddleGoTo.Text = "Go To";
            this.btnMiddleGoTo.UseVisualStyleBackColor = true;
            this.btnMiddleGoTo.Click += new System.EventHandler(this.btnMiddleGoTo_Click);
            // 
            // tbCenterY
            // 
            this.tbCenterY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbCenterY.Location = new System.Drawing.Point(134, 122);
            this.tbCenterY.Name = "tbCenterY";
            this.tbCenterY.Size = new System.Drawing.Size(66, 22);
            this.tbCenterY.TabIndex = 15;
            this.tbCenterY.TextChanged += new System.EventHandler(this.tbCenterY_TextChanged);
            // 
            // btnSelectMiddle
            // 
            this.btnSelectMiddle.Location = new System.Drawing.Point(206, 53);
            this.btnSelectMiddle.Name = "btnSelectMiddle";
            this.btnSelectMiddle.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMiddle.TabIndex = 16;
            this.btnSelectMiddle.Text = "Teach";
            this.btnSelectMiddle.UseVisualStyleBackColor = true;
            this.btnSelectMiddle.Click += new System.EventHandler(this.btnSelectMiddle_Click);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbEndY.Location = new System.Drawing.Point(134, 89);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(66, 22);
            this.tbEndY.TabIndex = 14;
            this.tbEndY.TextChanged += new System.EventHandler(this.tbEndY_TextChanged);
            // 
            // btnStartGoTo
            // 
            this.btnStartGoTo.Location = new System.Drawing.Point(287, 19);
            this.btnStartGoTo.Name = "btnStartGoTo";
            this.btnStartGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnStartGoTo.TabIndex = 22;
            this.btnStartGoTo.Text = "Go To";
            this.btnStartGoTo.UseVisualStyleBackColor = true;
            this.btnStartGoTo.Click += new System.EventHandler(this.btnStartGoTo_Click);
            // 
            // tbCenterX
            // 
            this.tbCenterX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbCenterX.Location = new System.Drawing.Point(62, 122);
            this.tbCenterX.Name = "tbCenterX";
            this.tbCenterX.Size = new System.Drawing.Size(66, 22);
            this.tbCenterX.TabIndex = 13;
            this.tbCenterX.TextChanged += new System.EventHandler(this.tbCenterX_TextChanged);
            // 
            // tbMiddleY
            // 
            this.tbMiddleY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbMiddleY.Location = new System.Drawing.Point(134, 55);
            this.tbMiddleY.Name = "tbMiddleY";
            this.tbMiddleY.Size = new System.Drawing.Size(66, 22);
            this.tbMiddleY.TabIndex = 11;
            this.tbMiddleY.TextChanged += new System.EventHandler(this.tbMiddleY_TextChanged);
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbEndX.Location = new System.Drawing.Point(62, 89);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(66, 22);
            this.tbEndX.TabIndex = 10;
            this.tbEndX.TextChanged += new System.EventHandler(this.tbEndX_TextChanged);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(206, 19);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 17;
            this.btnSelectStart.Text = "Teach";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Center:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbMiddleX
            // 
            this.tbMiddleX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbMiddleX.Location = new System.Drawing.Point(62, 55);
            this.tbMiddleX.Name = "tbMiddleX";
            this.tbMiddleX.Size = new System.Drawing.Size(66, 22);
            this.tbMiddleX.TabIndex = 9;
            this.tbMiddleX.TextChanged += new System.EventHandler(this.tbMiddleX_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "End:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbStartY.Location = new System.Drawing.Point(134, 21);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(66, 22);
            this.tbStartY.TabIndex = 8;
            this.tbStartY.TextChanged += new System.EventHandler(this.tbStartY_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Middle:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbStartX.Location = new System.Drawing.Point(62, 21);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(66, 22);
            this.tbStartX.TabIndex = 12;
            this.tbStartX.TextChanged += new System.EventHandler(this.tbStartX_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(16, 121);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(121, 18);
            this.cbWeightControl.TabIndex = 11;
            this.cbWeightControl.Text = "weight control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(95, 75);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(75, 22);
            this.comboBoxLineType.TabIndex = 10;
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.Location = new System.Drawing.Point(203, 74);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(59, 23);
            this.btnEditLineParams.TabIndex = 9;
            this.btnEditLineParams.Text = "Edit";
            this.btnEditLineParams.UseVisualStyleBackColor = true;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbWeight.Location = new System.Drawing.Point(143, 119);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(74, 22);
            this.tbWeight.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "mg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 14);
            this.label6.TabIndex = 7;
            this.label6.Text = "Line Type:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboBoxDegree
            // 
            this.comboBoxDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDegree.FormattingEnabled = true;
            this.comboBoxDegree.Items.AddRange(new object[] {
            "360",
            "-360"});
            this.comboBoxDegree.Location = new System.Drawing.Point(65, 154);
            this.comboBoxDegree.Name = "comboBoxDegree";
            this.comboBoxDegree.Size = new System.Drawing.Size(66, 22);
            this.comboBoxDegree.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "Degree:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(509, 627);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 25;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(509, 598);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 26;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // EditCircleForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditCircleForm1";
            this.Text = "EditCircleForm1";
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbSME;
        private System.Windows.Forms.Button btnCenterGoTo;
        private System.Windows.Forms.Button btnSelectCenter;
        private System.Windows.Forms.Button btnEndGoTo;
        private System.Windows.Forms.Button btnSelectEnd;
        private System.Windows.Forms.Button btnMiddleGoTo;
        private Controls.DoubleTextBox tbCenterY;
        private System.Windows.Forms.Button btnSelectMiddle;
        private Controls.DoubleTextBox tbEndY;
        private System.Windows.Forms.Button btnStartGoTo;
        private Controls.DoubleTextBox tbCenterX;
        private Controls.DoubleTextBox tbMiddleY;
        private Controls.DoubleTextBox tbEndX;
        private System.Windows.Forms.Button btnSelectStart;
        private System.Windows.Forms.Label label4;
        private Controls.DoubleTextBox tbMiddleX;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbStartY;
        private System.Windows.Forms.Label label2;
        private Controls.DoubleTextBox tbStartX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button btnEditLineParams;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboBoxDegree;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Button btnNextCmdLine;
    }
}