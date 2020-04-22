namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditSingleLineForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.btnGoToEnd = new System.Windows.Forms.Button();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnSelectEnd = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.btnOffset = new System.Windows.Forms.Button();
            this.btnGotoOffset = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbInspectionKey = new System.Windows.Forms.ComboBox();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.lblTiltType = new System.Windows.Forms.Label();
            this.cbTiltType = new System.Windows.Forms.ComboBox();
            this.gbx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Text = "10";
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.cbTiltType);
            this.gbx2.Controls.Add(this.lblTiltType);
            this.gbx2.Controls.Add(this.cbInspectionKey);
            this.gbx2.Controls.Add(this.label7);
            this.gbx2.Controls.Add(this.btnGotoOffset);
            this.gbx2.Controls.Add(this.btnOffset);
            this.gbx2.Controls.Add(this.nudOffset);
            this.gbx2.Controls.Add(this.label6);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbWeight);
            this.gbx2.Controls.Add(this.cbWeightControl);
            this.gbx2.Controls.Add(this.btnEditLineParams);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.comboBoxLineType);
            this.gbx2.Controls.Add(this.btnGoToEnd);
            this.gbx2.Controls.Add(this.btnGotoStart);
            this.gbx2.Controls.Add(this.btnSelectEnd);
            this.gbx2.Controls.Add(this.btnSelectStart);
            this.gbx2.Controls.Add(this.tbEndY);
            this.gbx2.Controls.Add(this.tbStartY);
            this.gbx2.Controls.Add(this.tbEndX);
            this.gbx2.Controls.Add(this.tbStartX);
            this.gbx2.Controls.Add(this.label5);
            this.gbx2.Controls.Add(this.label4);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(418, 216);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 54;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 14);
            this.label3.TabIndex = 53;
            this.label3.Text = "mg";
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(142, 134);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(76, 22);
            this.tbWeight.TabIndex = 52;
            this.tbWeight.Text = "0.000";
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(11, 136);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 51;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.Location = new System.Drawing.Point(171, 94);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditLineParams.TabIndex = 50;
            this.btnEditLineParams.Text = "Edit";
            this.btnEditLineParams.UseVisualStyleBackColor = true;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 49;
            this.label1.Text = "Line Style:";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(92, 95);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(73, 22);
            this.comboBoxLineType.TabIndex = 48;
            // 
            // btnGoToEnd
            // 
            this.btnGoToEnd.Location = new System.Drawing.Point(296, 44);
            this.btnGoToEnd.Name = "btnGoToEnd";
            this.btnGoToEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToEnd.TabIndex = 46;
            this.btnGoToEnd.Text = "Go To";
            this.btnGoToEnd.UseVisualStyleBackColor = true;
            this.btnGoToEnd.Click += new System.EventHandler(this.btnGoToEnd_Click);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(296, 12);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 47;
            this.btnGotoStart.Text = "Go To";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.Location = new System.Drawing.Point(215, 44);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 44;
            this.btnSelectEnd.Text = "Teach";
            this.btnSelectEnd.UseVisualStyleBackColor = true;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(215, 12);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 45;
            this.btnSelectStart.Text = "Teach";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.White;
            this.tbEndY.Location = new System.Drawing.Point(137, 45);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(72, 22);
            this.tbEndY.TabIndex = 40;
            this.tbEndY.Text = "0.000";
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.White;
            this.tbStartY.Location = new System.Drawing.Point(137, 13);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(72, 22);
            this.tbStartY.TabIndex = 41;
            this.tbStartY.Text = "0.000";
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.White;
            this.tbEndX.Location = new System.Drawing.Point(59, 45);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(72, 22);
            this.tbEndX.TabIndex = 42;
            this.tbEndX.Text = "0.000";
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.Color.White;
            this.tbStartX.Location = new System.Drawing.Point(59, 13);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(72, 22);
            this.tbStartX.TabIndex = 43;
            this.tbStartX.Text = "0.000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 14);
            this.label5.TabIndex = 38;
            this.label5.Text = "End:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 39;
            this.label4.Text = "Start:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 57;
            this.label2.Text = "Offset";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 58;
            this.label6.Text = "mm";
            // 
            // nudOffset
            // 
            this.nudOffset.DecimalPlaces = 3;
            this.nudOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffset.Location = new System.Drawing.Point(59, 182);
            this.nudOffset.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOffset.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudOffset.Name = "nudOffset";
            this.nudOffset.Size = new System.Drawing.Size(106, 22);
            this.nudOffset.TabIndex = 60;
            this.nudOffset.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(204, 183);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(75, 23);
            this.btnOffset.TabIndex = 61;
            this.btnOffset.Text = "Teach";
            this.btnOffset.UseVisualStyleBackColor = true;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // btnGotoOffset
            // 
            this.btnGotoOffset.Location = new System.Drawing.Point(285, 184);
            this.btnGotoOffset.Name = "btnGotoOffset";
            this.btnGotoOffset.Size = new System.Drawing.Size(75, 23);
            this.btnGotoOffset.TabIndex = 62;
            this.btnGotoOffset.Text = "Go To";
            this.btnGotoOffset.UseVisualStyleBackColor = true;
            this.btnGotoOffset.Click += new System.EventHandler(this.btnGotoOffset_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 14);
            this.label7.TabIndex = 63;
            this.label7.Text = "InspectionKey:";
            // 
            // cbInspectionKey
            // 
            this.cbInspectionKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInspectionKey.FormattingEnabled = true;
            this.cbInspectionKey.Location = new System.Drawing.Point(119, 213);
            this.cbInspectionKey.Name = "cbInspectionKey";
            this.cbInspectionKey.Size = new System.Drawing.Size(121, 22);
            this.cbInspectionKey.TabIndex = 64;
            this.cbInspectionKey.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(510, 624);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 9;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(510, 595);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 10;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // lblTiltType
            // 
            this.lblTiltType.AutoSize = true;
            this.lblTiltType.Location = new System.Drawing.Point(268, 98);
            this.lblTiltType.Name = "lblTiltType";
            this.lblTiltType.Size = new System.Drawing.Size(64, 14);
            this.lblTiltType.TabIndex = 65;
            this.lblTiltType.Text = "倾斜方向:";
            // 
            // cbTiltType
            // 
            this.cbTiltType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTiltType.FormattingEnabled = true;
            this.cbTiltType.Location = new System.Drawing.Point(339, 95);
            this.cbTiltType.Name = "cbTiltType";
            this.cbTiltType.Size = new System.Drawing.Size(92, 22);
            this.cbTiltType.TabIndex = 66;
            // 
            // EditSingleLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditSingleLineForm";
            this.Text = "EditSingleLineForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditSingleLineForm_FormClosing);
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnEditLineParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button btnGoToEnd;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnSelectEnd;
        private System.Windows.Forms.Button btnSelectStart;
        private Controls.DoubleTextBox tbEndY;
        private Controls.DoubleTextBox tbStartY;
        private Controls.DoubleTextBox tbEndX;
        private Controls.DoubleTextBox tbStartX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.Button btnOffset;
        private System.Windows.Forms.Button btnGotoOffset;
        private System.Windows.Forms.ComboBox cbInspectionKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNextCmdLine;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Label lblTiltType;
        private System.Windows.Forms.ComboBox cbTiltType;
    }
}