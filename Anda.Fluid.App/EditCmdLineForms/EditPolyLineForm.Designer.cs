namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditPolyLineForm
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
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.tbPointY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbPointX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            this.btnEditWeight = new System.Windows.Forms.Button();
            this.btnLineStyle = new System.Windows.Forms.Button();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
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
            this.gbx1.Controls.Add(this.btnDelete);
            this.gbx1.Controls.Add(this.label2);
            this.gbx1.Controls.Add(this.listBoxPoints);
            this.gbx1.Size = new System.Drawing.Size(270, 293);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnGoToOffset);
            this.gbx2.Controls.Add(this.btnSelectOffset);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.nudOffset);
            this.gbx2.Controls.Add(this.btnLineStyle);
            this.gbx2.Controls.Add(this.btnEditWeight);
            this.gbx2.Controls.Add(this.listBoxLines);
            this.gbx2.Controls.Add(this.btnAdd);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.cbWeightControl);
            this.gbx2.Controls.Add(this.btnGotoStart);
            this.gbx2.Controls.Add(this.btnSelectStart);
            this.gbx2.Controls.Add(this.tbPointY);
            this.gbx2.Controls.Add(this.tbPointX);
            this.gbx2.Controls.Add(this.label4);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(418, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 55;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(6, 224);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 52;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(297, 14);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 48;
            this.btnGotoStart.Text = "Go To";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(216, 14);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 46;
            this.btnSelectStart.Text = "Teach";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnTeachPoint_Click);
            // 
            // tbPointY
            // 
            this.tbPointY.BackColor = System.Drawing.Color.White;
            this.tbPointY.Location = new System.Drawing.Point(138, 15);
            this.tbPointY.Name = "tbPointY";
            this.tbPointY.Size = new System.Drawing.Size(72, 22);
            this.tbPointY.TabIndex = 42;
            this.tbPointY.Text = "0.000";
            // 
            // tbPointX
            // 
            this.tbPointX.BackColor = System.Drawing.Color.White;
            this.tbPointX.Location = new System.Drawing.Point(60, 15);
            this.tbPointX.Name = "tbPointX";
            this.tbPointX.Size = new System.Drawing.Size(72, 22);
            this.tbPointX.TabIndex = 44;
            this.tbPointX.Text = "0.000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 14);
            this.label4.TabIndex = 40;
            this.label4.Text = "Point:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(188, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "PolyLine Points:";
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(4, 34);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(262, 256);
            this.listBoxPoints.TabIndex = 20;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.listBoxPoints_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(418, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 57;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listBoxLines
            // 
            this.listBoxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.ItemHeight = 14;
            this.listBoxLines.Location = new System.Drawing.Point(9, 72);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.Size = new System.Drawing.Size(484, 144);
            this.listBoxLines.TabIndex = 58;
            this.listBoxLines.SelectedIndexChanged += new System.EventHandler(this.listBoxLines_SelectedIndexChanged);
            // 
            // btnEditWeight
            // 
            this.btnEditWeight.Location = new System.Drawing.Point(135, 221);
            this.btnEditWeight.Name = "btnEditWeight";
            this.btnEditWeight.Size = new System.Drawing.Size(75, 23);
            this.btnEditWeight.TabIndex = 59;
            this.btnEditWeight.Text = "Edit";
            this.btnEditWeight.UseVisualStyleBackColor = true;
            this.btnEditWeight.Click += new System.EventHandler(this.btnEditWeight_Click);
            // 
            // btnLineStyle
            // 
            this.btnLineStyle.Location = new System.Drawing.Point(216, 221);
            this.btnLineStyle.Name = "btnLineStyle";
            this.btnLineStyle.Size = new System.Drawing.Size(75, 23);
            this.btnLineStyle.TabIndex = 60;
            this.btnLineStyle.Text = "LineType";
            this.btnLineStyle.UseVisualStyleBackColor = true;
            this.btnLineStyle.Click += new System.EventHandler(this.btnLineStyle_Click);
            // 
            // nudOffset
            // 
            this.nudOffset.DecimalPlaces = 3;
            this.nudOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffset.Location = new System.Drawing.Point(63, 43);
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
            this.nudOffset.Size = new System.Drawing.Size(113, 22);
            this.nudOffset.TabIndex = 61;
            this.nudOffset.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 62;
            this.label1.Text = "Offset";
            // 
            // btnSelectOffset
            // 
            this.btnSelectOffset.Location = new System.Drawing.Point(216, 42);
            this.btnSelectOffset.Name = "btnSelectOffset";
            this.btnSelectOffset.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOffset.TabIndex = 63;
            this.btnSelectOffset.Text = "Teach";
            this.btnSelectOffset.UseVisualStyleBackColor = true;
            this.btnSelectOffset.Click += new System.EventHandler(this.btnSelectOffset_Click);
            // 
            // btnGoToOffset
            // 
            this.btnGoToOffset.Location = new System.Drawing.Point(297, 41);
            this.btnGoToOffset.Name = "btnGoToOffset";
            this.btnGoToOffset.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOffset.TabIndex = 64;
            this.btnGoToOffset.Text = "Go To";
            this.btnGoToOffset.UseVisualStyleBackColor = true;
            this.btnGoToOffset.Click += new System.EventHandler(this.btnGoToOffset_Click);
            // 
            // btnNextCmdLine
            // 
            this.btnNextCmdLine.Location = new System.Drawing.Point(509, 630);
            this.btnNextCmdLine.Name = "btnNextCmdLine";
            this.btnNextCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnNextCmdLine.TabIndex = 9;
            this.btnNextCmdLine.Text = "next";
            this.btnNextCmdLine.UseVisualStyleBackColor = true;
            this.btnNextCmdLine.Click += new System.EventHandler(this.btnNextCmdLine_Click);
            // 
            // btnLastCmdLine
            // 
            this.btnLastCmdLine.Location = new System.Drawing.Point(510, 601);
            this.btnLastCmdLine.Name = "btnLastCmdLine";
            this.btnLastCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnLastCmdLine.TabIndex = 10;
            this.btnLastCmdLine.Text = "last";
            this.btnLastCmdLine.UseVisualStyleBackColor = true;
            this.btnLastCmdLine.Click += new System.EventHandler(this.btnLastCmdLine_Click);
            // 
            // EditPolyLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnLastCmdLine);
            this.Controls.Add(this.btnNextCmdLine);
            this.Name = "EditPolyLineForm";
            this.Text = "EditPolyLineForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditPolyLineForm_FormClosing);
            this.Load += new System.EventHandler(this.EditPolyLineForm_Load);
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnSelectStart;
        private Controls.DoubleTextBox tbPointY;
        private Controls.DoubleTextBox tbPointX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listBoxLines;
        private System.Windows.Forms.Button btnEditWeight;
        private System.Windows.Forms.Button btnLineStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.Button btnGoToOffset;
        private System.Windows.Forms.Button btnSelectOffset;
        private System.Windows.Forms.Button btnNextCmdLine;
        private System.Windows.Forms.Button btnLastCmdLine;
    }
}