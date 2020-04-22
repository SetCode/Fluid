namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditSnakeLineForm1
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
            this.tbLineNumbers = new Anda.Fluid.Controls.IntTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPoint3GoTo = new System.Windows.Forms.Button();
            this.btnPoint2GoTo = new System.Windows.Forms.Button();
            this.btnPoint1GoTo = new System.Windows.Forms.Button();
            this.btnPoint3Select = new System.Windows.Forms.Button();
            this.btnPoint2Select = new System.Windows.Forms.Button();
            this.btnPoint1Select = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbP2Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP3Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP2X = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP1Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP3X = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP1X = new Anda.Fluid.Controls.DoubleTextBox();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditWt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLastCmdLine = new System.Windows.Forms.Button();
            this.btnNextCmdLine = new System.Windows.Forms.Button();
            this.chkContinuous = new System.Windows.Forms.CheckBox();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.listBoxPoints);
            this.gbx1.Controls.Add(this.label1);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.chkContinuous);
            this.gbx2.Controls.Add(this.button1);
            this.gbx2.Controls.Add(this.btnEditWt);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.cbWeightControl);
            this.gbx2.Controls.Add(this.tbLineNumbers);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.btnPoint3GoTo);
            this.gbx2.Controls.Add(this.btnPoint2GoTo);
            this.gbx2.Controls.Add(this.btnPoint1GoTo);
            this.gbx2.Controls.Add(this.btnPoint3Select);
            this.gbx2.Controls.Add(this.btnPoint2Select);
            this.gbx2.Controls.Add(this.btnPoint1Select);
            this.gbx2.Controls.Add(this.label7);
            this.gbx2.Controls.Add(this.label5);
            this.gbx2.Controls.Add(this.label3);
            this.gbx2.Controls.Add(this.tbP2Y);
            this.gbx2.Controls.Add(this.tbP3Y);
            this.gbx2.Controls.Add(this.tbP2X);
            this.gbx2.Controls.Add(this.tbP1Y);
            this.gbx2.Controls.Add(this.tbP3X);
            this.gbx2.Controls.Add(this.tbP1X);
            // 
            // tbLineNumbers
            // 
            this.tbLineNumbers.BackColor = System.Drawing.Color.White;
            this.tbLineNumbers.Location = new System.Drawing.Point(179, 27);
            this.tbLineNumbers.Name = "tbLineNumbers";
            this.tbLineNumbers.Size = new System.Drawing.Size(84, 22);
            this.tbLineNumbers.TabIndex = 28;
            this.tbLineNumbers.Text = "2";
            this.tbLineNumbers.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 27;
            this.label2.Text = "line numbers:";
            // 
            // btnPoint3GoTo
            // 
            this.btnPoint3GoTo.Location = new System.Drawing.Point(351, 114);
            this.btnPoint3GoTo.Name = "btnPoint3GoTo";
            this.btnPoint3GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint3GoTo.TabIndex = 25;
            this.btnPoint3GoTo.Text = "Go To";
            this.btnPoint3GoTo.UseVisualStyleBackColor = true;
            this.btnPoint3GoTo.Click += new System.EventHandler(this.btnPoint3GoTo_Click);
            // 
            // btnPoint2GoTo
            // 
            this.btnPoint2GoTo.Location = new System.Drawing.Point(351, 84);
            this.btnPoint2GoTo.Name = "btnPoint2GoTo";
            this.btnPoint2GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint2GoTo.TabIndex = 24;
            this.btnPoint2GoTo.Text = "Go To";
            this.btnPoint2GoTo.UseVisualStyleBackColor = true;
            this.btnPoint2GoTo.Click += new System.EventHandler(this.btnPoint2GoTo_Click);
            // 
            // btnPoint1GoTo
            // 
            this.btnPoint1GoTo.Location = new System.Drawing.Point(351, 55);
            this.btnPoint1GoTo.Name = "btnPoint1GoTo";
            this.btnPoint1GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint1GoTo.TabIndex = 23;
            this.btnPoint1GoTo.Text = "Go To";
            this.btnPoint1GoTo.UseVisualStyleBackColor = true;
            this.btnPoint1GoTo.Click += new System.EventHandler(this.btnPoint1GoTo_Click);
            // 
            // btnPoint3Select
            // 
            this.btnPoint3Select.Location = new System.Drawing.Point(270, 114);
            this.btnPoint3Select.Name = "btnPoint3Select";
            this.btnPoint3Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint3Select.TabIndex = 22;
            this.btnPoint3Select.Text = "Teach";
            this.btnPoint3Select.UseVisualStyleBackColor = true;
            this.btnPoint3Select.Click += new System.EventHandler(this.btnPoint3Select_Click);
            // 
            // btnPoint2Select
            // 
            this.btnPoint2Select.Location = new System.Drawing.Point(270, 84);
            this.btnPoint2Select.Name = "btnPoint2Select";
            this.btnPoint2Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint2Select.TabIndex = 21;
            this.btnPoint2Select.Text = "Teach";
            this.btnPoint2Select.UseVisualStyleBackColor = true;
            this.btnPoint2Select.Click += new System.EventHandler(this.btnPoint2Select_Click);
            // 
            // btnPoint1Select
            // 
            this.btnPoint1Select.Location = new System.Drawing.Point(270, 55);
            this.btnPoint1Select.Name = "btnPoint1Select";
            this.btnPoint1Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint1Select.TabIndex = 26;
            this.btnPoint1Select.Text = "Teach";
            this.btnPoint1Select.UseVisualStyleBackColor = true;
            this.btnPoint1Select.Click += new System.EventHandler(this.btnPoint1Select_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 14);
            this.label7.TabIndex = 20;
            this.label7.Text = "Point 2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "Point 3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "Point 1:";
            // 
            // tbP2Y
            // 
            this.tbP2Y.BackColor = System.Drawing.Color.White;
            this.tbP2Y.Location = new System.Drawing.Point(179, 86);
            this.tbP2Y.Name = "tbP2Y";
            this.tbP2Y.Size = new System.Drawing.Size(84, 22);
            this.tbP2Y.TabIndex = 16;
            this.tbP2Y.Text = "0.000";
            // 
            // tbP3Y
            // 
            this.tbP3Y.BackColor = System.Drawing.Color.White;
            this.tbP3Y.Location = new System.Drawing.Point(179, 116);
            this.tbP3Y.Name = "tbP3Y";
            this.tbP3Y.Size = new System.Drawing.Size(84, 22);
            this.tbP3Y.TabIndex = 15;
            this.tbP3Y.Text = "0.000";
            // 
            // tbP2X
            // 
            this.tbP2X.BackColor = System.Drawing.Color.White;
            this.tbP2X.Location = new System.Drawing.Point(87, 86);
            this.tbP2X.Name = "tbP2X";
            this.tbP2X.Size = new System.Drawing.Size(84, 22);
            this.tbP2X.TabIndex = 14;
            this.tbP2X.Text = "0.000";
            // 
            // tbP1Y
            // 
            this.tbP1Y.BackColor = System.Drawing.Color.White;
            this.tbP1Y.Location = new System.Drawing.Point(180, 55);
            this.tbP1Y.Name = "tbP1Y";
            this.tbP1Y.Size = new System.Drawing.Size(84, 22);
            this.tbP1Y.TabIndex = 13;
            this.tbP1Y.Text = "0.000";
            // 
            // tbP3X
            // 
            this.tbP3X.BackColor = System.Drawing.Color.White;
            this.tbP3X.Location = new System.Drawing.Point(87, 116);
            this.tbP3X.Name = "tbP3X";
            this.tbP3X.Size = new System.Drawing.Size(84, 22);
            this.tbP3X.TabIndex = 17;
            this.tbP3X.Text = "0.000";
            // 
            // tbP1X
            // 
            this.tbP1X.BackColor = System.Drawing.Color.White;
            this.tbP1X.Location = new System.Drawing.Point(88, 55);
            this.tbP1X.Name = "tbP1X";
            this.tbP1X.Size = new System.Drawing.Size(84, 22);
            this.tbP1X.TabIndex = 12;
            this.tbP1X.Text = "0.000";
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(9, 221);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 34;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // listBoxPoints
            // 
            this.listBoxPoints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(4, 37);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(262, 270);
            this.listBoxPoints.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Line Points:";
            // 
            // btnEditWt
            // 
            this.btnEditWt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditWt.Location = new System.Drawing.Point(140, 218);
            this.btnEditWt.Name = "btnEditWt";
            this.btnEditWt.Size = new System.Drawing.Size(75, 23);
            this.btnEditWt.TabIndex = 37;
            this.btnEditWt.Text = "Edit";
            this.btnEditWt.UseVisualStyleBackColor = true;
            this.btnEditWt.Click += new System.EventHandler(this.btnEditWt_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(221, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "LineType";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(509, 598);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 9;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(509, 627);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 10;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // chkContinuous
            // 
            this.chkContinuous.AutoSize = true;
            this.chkContinuous.Location = new System.Drawing.Point(270, 29);
            this.chkContinuous.Name = "chkContinuous";
            this.chkContinuous.Size = new System.Drawing.Size(99, 18);
            this.chkContinuous.TabIndex = 39;
            this.chkContinuous.Text = "Continuous";
            this.chkContinuous.UseVisualStyleBackColor = true;
            this.chkContinuous.CheckedChanged += new System.EventHandler(this.chkContinuous_CheckedChanged);
            // 
            // EditSnakeLineForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnNextCmdLine);
            this.Controls.Add(this.btnLastCmdLine);
            this.Name = "EditSnakeLineForm1";
            this.Text = "EditSnakeLineForm1";
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.Controls.SetChildIndex(this.btnLastCmdLine, 0);
            this.Controls.SetChildIndex(this.btnNextCmdLine, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.IntTextBox tbLineNumbers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPoint3GoTo;
        private System.Windows.Forms.Button btnPoint2GoTo;
        private System.Windows.Forms.Button btnPoint1GoTo;
        private System.Windows.Forms.Button btnPoint3Select;
        private System.Windows.Forms.Button btnPoint2Select;
        private System.Windows.Forms.Button btnPoint1Select;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbP2Y;
        private Controls.DoubleTextBox tbP3Y;
        private Controls.DoubleTextBox tbP2X;
        private Controls.DoubleTextBox tbP1Y;
        private Controls.DoubleTextBox tbP3X;
        private Controls.DoubleTextBox tbP1X;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditWt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLastCmdLine;
        private System.Windows.Forms.Button btnNextCmdLine;
        private System.Windows.Forms.CheckBox chkContinuous;
    }
}