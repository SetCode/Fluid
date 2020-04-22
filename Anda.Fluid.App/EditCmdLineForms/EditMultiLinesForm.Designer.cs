namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditMultiLinesForm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
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
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditWt = new System.Windows.Forms.Button();
            this.btnLineType = new System.Windows.Forms.Button();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectOffset = new System.Windows.Forms.Button();
            this.btnGoToOffset = new System.Windows.Forms.Button();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnGoToOffset);
            this.gbx1.Controls.Add(this.btnSelectOffset);
            this.gbx1.Controls.Add(this.label3);
            this.gbx1.Controls.Add(this.label1);
            this.gbx1.Controls.Add(this.nudOffset);
            this.gbx1.Controls.Add(this.tbStartX);
            this.gbx1.Controls.Add(this.label4);
            this.gbx1.Controls.Add(this.tbStartY);
            this.gbx1.Controls.Add(this.btnGotoStart);
            this.gbx1.Controls.Add(this.btnSelectStart);
            this.gbx1.Controls.Add(this.btnAdd);
            this.gbx1.Controls.Add(this.label5);
            this.gbx1.Controls.Add(this.tbEndX);
            this.gbx1.Controls.Add(this.tbEndY);
            this.gbx1.Controls.Add(this.btnSelectEnd);
            this.gbx1.Controls.Add(this.btnGoToEnd);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnDelete);
            this.gbx2.Controls.Add(this.btnLineType);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.btnEditWt);
            this.gbx2.Controls.Add(this.listBoxPoints);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.cbWeightControl);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(187, 248);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 35);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(419, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "Line Points:";
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(6, 35);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(488, 172);
            this.listBoxPoints.TabIndex = 17;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.listBoxPoints_SelectedIndexChanged);
            // 
            // btnGoToEnd
            // 
            this.btnGoToEnd.Location = new System.Drawing.Point(187, 121);
            this.btnGoToEnd.Name = "btnGoToEnd";
            this.btnGoToEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToEnd.TabIndex = 28;
            this.btnGoToEnd.Text = "Go To";
            this.btnGoToEnd.UseVisualStyleBackColor = true;
            this.btnGoToEnd.Click += new System.EventHandler(this.btnGoToEnd_Click);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(187, 42);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 29;
            this.btnGotoStart.Text = "Go To";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.Location = new System.Drawing.Point(187, 93);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 26;
            this.btnSelectEnd.Text = "Teach";
            this.btnSelectEnd.UseVisualStyleBackColor = true;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(187, 14);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 27;
            this.btnSelectStart.Text = "Teach";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.White;
            this.tbEndY.Location = new System.Drawing.Point(58, 122);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(72, 22);
            this.tbEndY.TabIndex = 22;
            this.tbEndY.Text = "0.000";
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.White;
            this.tbStartY.Location = new System.Drawing.Point(58, 43);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(72, 22);
            this.tbStartY.TabIndex = 23;
            this.tbStartY.Text = "0.000";
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.White;
            this.tbEndX.Location = new System.Drawing.Point(58, 94);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(72, 22);
            this.tbEndX.TabIndex = 24;
            this.tbEndX.Text = "0.000";
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.Color.White;
            this.tbStartX.Location = new System.Drawing.Point(58, 15);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(72, 22);
            this.tbStartX.TabIndex = 25;
            this.tbStartX.Text = "0.000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 14);
            this.label5.TabIndex = 20;
            this.label5.Text = "End:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 21;
            this.label4.Text = "Start:";
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(9, 221);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 33;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 36;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditWt
            // 
            this.btnEditWt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditWt.Location = new System.Drawing.Point(140, 218);
            this.btnEditWt.Name = "btnEditWt";
            this.btnEditWt.Size = new System.Drawing.Size(75, 23);
            this.btnEditWt.TabIndex = 38;
            this.btnEditWt.Text = "Edit";
            this.btnEditWt.UseVisualStyleBackColor = true;
            this.btnEditWt.Click += new System.EventHandler(this.btnEditWt_Click);
            // 
            // btnLineType
            // 
            this.btnLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLineType.Location = new System.Drawing.Point(221, 218);
            this.btnLineType.Name = "btnLineType";
            this.btnLineType.Size = new System.Drawing.Size(75, 23);
            this.btnLineType.TabIndex = 39;
            this.btnLineType.Text = "LineType";
            this.btnLineType.UseVisualStyleBackColor = true;
            this.btnLineType.Click += new System.EventHandler(this.btnLineType_Click);
            // 
            // nudOffset
            // 
            this.nudOffset.DecimalPlaces = 3;
            this.nudOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffset.Location = new System.Drawing.Point(58, 175);
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
            this.nudOffset.Size = new System.Drawing.Size(72, 22);
            this.nudOffset.TabIndex = 30;
            this.nudOffset.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 31;
            this.label1.Text = "mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 32;
            this.label3.Text = "Offset";
            // 
            // btnSelectOffset
            // 
            this.btnSelectOffset.Location = new System.Drawing.Point(187, 173);
            this.btnSelectOffset.Name = "btnSelectOffset";
            this.btnSelectOffset.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOffset.TabIndex = 34;
            this.btnSelectOffset.Text = "Teach";
            this.btnSelectOffset.UseVisualStyleBackColor = true;
            this.btnSelectOffset.Click += new System.EventHandler(this.btnSelectOffset_Click);
            // 
            // btnGoToOffset
            // 
            this.btnGoToOffset.Location = new System.Drawing.Point(187, 202);
            this.btnGoToOffset.Name = "btnGoToOffset";
            this.btnGoToOffset.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOffset.TabIndex = 35;
            this.btnGoToOffset.Text = "Go To";
            this.btnGoToOffset.UseVisualStyleBackColor = true;
            this.btnGoToOffset.Click += new System.EventHandler(this.btnGoToOffset_Click);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(510, 626);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 9;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(510, 597);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 10;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // EditMultiLinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditMultiLinesForm";
            this.Text = "EditMultiLinesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditMultiLinesForm_FormClosing);
            this.Load += new System.EventHandler(this.EditMultiLinesForm_Load);
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxPoints;
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
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEditWt;
        private System.Windows.Forms.Button btnLineType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.Button btnGoToOffset;
        private System.Windows.Forms.Button btnSelectOffset;
        private System.Windows.Forms.Button btnNextCmdLine;
        private System.Windows.Forms.Button btnLastCmdLine;
    }
}