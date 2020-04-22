namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditMultiTracesForm
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
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.rbArc = new System.Windows.Forms.RadioButton();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnTeachStart = new System.Windows.Forms.Button();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblMid = new System.Windows.Forms.Label();
            this.tbMidX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbMidY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGotoMid = new System.Windows.Forms.Button();
            this.btnTeachMid = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGotoEnd = new System.Windows.Forms.Button();
            this.btnTeachEnd = new System.Windows.Forms.Button();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.lblLineStyle = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            this.btnReTeach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOffsetX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOffsetY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnInsert);
            this.gbx1.Controls.Add(this.btnDelete);
            this.gbx1.Controls.Add(this.btnReTeach);
            this.gbx1.Controls.Add(this.listBoxPoints);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.tbOffsetY);
            this.gbx2.Controls.Add(this.tbOffsetX);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.listBoxLines);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnEditLineParams);
            this.gbx2.Controls.Add(this.lblLineStyle);
            this.gbx2.Controls.Add(this.comboBoxLineType);
            this.gbx2.Controls.Add(this.lblEnd);
            this.gbx2.Controls.Add(this.tbEndX);
            this.gbx2.Controls.Add(this.tbEndY);
            this.gbx2.Controls.Add(this.btnGotoEnd);
            this.gbx2.Controls.Add(this.btnTeachEnd);
            this.gbx2.Controls.Add(this.lblMid);
            this.gbx2.Controls.Add(this.tbMidX);
            this.gbx2.Controls.Add(this.tbMidY);
            this.gbx2.Controls.Add(this.btnGotoMid);
            this.gbx2.Controls.Add(this.btnTeachMid);
            this.gbx2.Controls.Add(this.lblStart);
            this.gbx2.Controls.Add(this.tbStartX);
            this.gbx2.Controls.Add(this.tbStartY);
            this.gbx2.Controls.Add(this.btnGotoStart);
            this.gbx2.Controls.Add(this.btnTeachStart);
            this.gbx2.Controls.Add(this.rbArc);
            this.gbx2.Controls.Add(this.rbLine);
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(9, 21);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(51, 18);
            this.rbLine.TabIndex = 0;
            this.rbLine.TabStop = true;
            this.rbLine.Text = "直线";
            this.rbLine.UseVisualStyleBackColor = true;
            this.rbLine.CheckedChanged += new System.EventHandler(this.rbLine_CheckedChanged);
            // 
            // rbArc
            // 
            this.rbArc.AutoSize = true;
            this.rbArc.Location = new System.Drawing.Point(84, 21);
            this.rbArc.Name = "rbArc";
            this.rbArc.Size = new System.Drawing.Size(51, 18);
            this.rbArc.TabIndex = 1;
            this.rbArc.TabStop = true;
            this.rbArc.Text = "圆弧";
            this.rbArc.UseVisualStyleBackColor = true;
            this.rbArc.CheckedChanged += new System.EventHandler(this.rbArc_CheckedChanged);
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.Color.White;
            this.tbStartX.Location = new System.Drawing.Point(55, 45);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(80, 22);
            this.tbStartX.TabIndex = 16;
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.White;
            this.tbStartY.Location = new System.Drawing.Point(141, 45);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(80, 22);
            this.tbStartY.TabIndex = 17;
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(308, 44);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 18;
            this.btnGotoStart.Text = "再现";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnTeachStart
            // 
            this.btnTeachStart.Location = new System.Drawing.Point(227, 44);
            this.btnTeachStart.Name = "btnTeachStart";
            this.btnTeachStart.Size = new System.Drawing.Size(75, 23);
            this.btnTeachStart.TabIndex = 19;
            this.btnTeachStart.Text = "示教";
            this.btnTeachStart.UseVisualStyleBackColor = true;
            this.btnTeachStart.Click += new System.EventHandler(this.btnTeachStart_Click);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(11, 48);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(38, 14);
            this.lblStart.TabIndex = 20;
            this.lblStart.Text = "起点:";
            // 
            // lblMid
            // 
            this.lblMid.AutoSize = true;
            this.lblMid.Location = new System.Drawing.Point(11, 76);
            this.lblMid.Name = "lblMid";
            this.lblMid.Size = new System.Drawing.Size(38, 14);
            this.lblMid.TabIndex = 25;
            this.lblMid.Text = "中点:";
            // 
            // tbMidX
            // 
            this.tbMidX.BackColor = System.Drawing.Color.White;
            this.tbMidX.Location = new System.Drawing.Point(55, 73);
            this.tbMidX.Name = "tbMidX";
            this.tbMidX.Size = new System.Drawing.Size(80, 22);
            this.tbMidX.TabIndex = 21;
            // 
            // tbMidY
            // 
            this.tbMidY.BackColor = System.Drawing.Color.White;
            this.tbMidY.Location = new System.Drawing.Point(141, 73);
            this.tbMidY.Name = "tbMidY";
            this.tbMidY.Size = new System.Drawing.Size(80, 22);
            this.tbMidY.TabIndex = 22;
            // 
            // btnGotoMid
            // 
            this.btnGotoMid.Location = new System.Drawing.Point(308, 72);
            this.btnGotoMid.Name = "btnGotoMid";
            this.btnGotoMid.Size = new System.Drawing.Size(75, 23);
            this.btnGotoMid.TabIndex = 23;
            this.btnGotoMid.Text = "再现";
            this.btnGotoMid.UseVisualStyleBackColor = true;
            this.btnGotoMid.Click += new System.EventHandler(this.btnGotoMid_Click);
            // 
            // btnTeachMid
            // 
            this.btnTeachMid.Location = new System.Drawing.Point(227, 72);
            this.btnTeachMid.Name = "btnTeachMid";
            this.btnTeachMid.Size = new System.Drawing.Size(75, 23);
            this.btnTeachMid.TabIndex = 24;
            this.btnTeachMid.Text = "示教";
            this.btnTeachMid.UseVisualStyleBackColor = true;
            this.btnTeachMid.Click += new System.EventHandler(this.btnTeachMid_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(11, 105);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(38, 14);
            this.lblEnd.TabIndex = 30;
            this.lblEnd.Text = "终点:";
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.White;
            this.tbEndX.Location = new System.Drawing.Point(55, 102);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(80, 22);
            this.tbEndX.TabIndex = 26;
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.White;
            this.tbEndY.Location = new System.Drawing.Point(141, 102);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(80, 22);
            this.tbEndY.TabIndex = 27;
            // 
            // btnGotoEnd
            // 
            this.btnGotoEnd.Location = new System.Drawing.Point(308, 101);
            this.btnGotoEnd.Name = "btnGotoEnd";
            this.btnGotoEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGotoEnd.TabIndex = 28;
            this.btnGotoEnd.Text = "再现";
            this.btnGotoEnd.UseVisualStyleBackColor = true;
            this.btnGotoEnd.Click += new System.EventHandler(this.btnGotoEnd_Click);
            // 
            // btnTeachEnd
            // 
            this.btnTeachEnd.Location = new System.Drawing.Point(227, 101);
            this.btnTeachEnd.Name = "btnTeachEnd";
            this.btnTeachEnd.Size = new System.Drawing.Size(75, 23);
            this.btnTeachEnd.TabIndex = 29;
            this.btnTeachEnd.Text = "示教";
            this.btnTeachEnd.UseVisualStyleBackColor = true;
            this.btnTeachEnd.Click += new System.EventHandler(this.btnTeachEnd_Click);
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.Location = new System.Drawing.Point(141, 223);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditLineParams.TabIndex = 53;
            this.btnEditLineParams.Text = "编辑";
            this.btnEditLineParams.UseVisualStyleBackColor = true;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // lblLineStyle
            // 
            this.lblLineStyle.AutoSize = true;
            this.lblLineStyle.Location = new System.Drawing.Point(5, 227);
            this.lblLineStyle.Name = "lblLineStyle";
            this.lblLineStyle.Size = new System.Drawing.Size(51, 14);
            this.lblLineStyle.TabIndex = 52;
            this.lblLineStyle.Text = "线类型:";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(62, 224);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(73, 22);
            this.comboBoxLineType.TabIndex = 51;
            this.comboBoxLineType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLineType_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 223);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 56;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 57;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(4, 30);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(262, 256);
            this.listBoxPoints.TabIndex = 21;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.listBoxPoints_SelectedIndexChanged);
            this.listBoxPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.prevKeyDown);
            // 
            // listBoxLines
            // 
            this.listBoxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.ItemHeight = 14;
            this.listBoxLines.Location = new System.Drawing.Point(9, 130);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.Size = new System.Drawing.Size(484, 88);
            this.listBoxLines.TabIndex = 59;
            this.listBoxLines.SelectedIndexChanged += new System.EventHandler(this.listBoxLines_SelectedIndexChanged);
            // 
            // btnReTeach
            // 
            this.btnReTeach.Location = new System.Drawing.Point(187, 7);
            this.btnReTeach.Name = "btnReTeach";
            this.btnReTeach.Size = new System.Drawing.Size(75, 23);
            this.btnReTeach.TabIndex = 60;
            this.btnReTeach.Text = "重新示教";
            this.btnReTeach.UseVisualStyleBackColor = true;
            this.btnReTeach.Click += new System.EventHandler(this.btnReTeach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 60;
            this.label1.Text = "XY偏移:";
            // 
            // tbOffsetX
            // 
            this.tbOffsetX.BackColor = System.Drawing.Color.White;
            this.tbOffsetX.Location = new System.Drawing.Point(413, 45);
            this.tbOffsetX.Name = "tbOffsetX";
            this.tbOffsetX.Size = new System.Drawing.Size(80, 22);
            this.tbOffsetX.TabIndex = 61;
            // 
            // tbOffsetY
            // 
            this.tbOffsetY.BackColor = System.Drawing.Color.White;
            this.tbOffsetY.Location = new System.Drawing.Point(413, 73);
            this.tbOffsetY.Name = "tbOffsetY";
            this.tbOffsetY.Size = new System.Drawing.Size(80, 22);
            this.tbOffsetY.TabIndex = 62;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(7, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 61;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(97, 7);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 62;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // EditMultiTracesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditMultiTracesForm";
            this.Text = "EditMultiTracesForm";
            this.gbx1.ResumeLayout(false);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbArc;
        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.Label lblEnd;
        private Controls.DoubleTextBox tbEndX;
        private Controls.DoubleTextBox tbEndY;
        private System.Windows.Forms.Button btnGotoEnd;
        private System.Windows.Forms.Button btnTeachEnd;
        private System.Windows.Forms.Label lblMid;
        private Controls.DoubleTextBox tbMidX;
        private Controls.DoubleTextBox tbMidY;
        private System.Windows.Forms.Button btnGotoMid;
        private System.Windows.Forms.Button btnTeachMid;
        private System.Windows.Forms.Label lblStart;
        private Controls.DoubleTextBox tbStartX;
        private Controls.DoubleTextBox tbStartY;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnTeachStart;
        private System.Windows.Forms.Button btnEditLineParams;
        private System.Windows.Forms.Label lblLineStyle;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.ListBox listBoxLines;
        private System.Windows.Forms.Button btnReTeach;
        private Controls.DoubleTextBox tbOffsetY;
        private Controls.DoubleTextBox tbOffsetX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsert;
    }
}