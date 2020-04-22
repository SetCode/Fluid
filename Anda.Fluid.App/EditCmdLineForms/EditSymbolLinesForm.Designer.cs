namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditSymbolLinesForm
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
            this.components = new System.ComponentModel.Container();
            this.tab = new System.Windows.Forms.TabControl();
            this.tbgSymbol = new System.Windows.Forms.TabPage();
            this.chkModifyEnable = new System.Windows.Forms.CheckBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tipPrev = new System.Windows.Forms.ToolStripButton();
            this.tipNext = new System.Windows.Forms.ToolStripButton();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnCreateSymbols = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoLine = new System.Windows.Forms.RadioButton();
            this.rdoArc = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.tbPointY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbPointX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.tbgMeasureHeight = new System.Windows.Forms.TabPage();
            this.btnGoMHPos = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMHZPos = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnMHCmdLine = new System.Windows.Forms.Button();
            this.heightControl1 = new Anda.Fluid.App.EditCmdLineForms.HeightControl();
            this.tbgSpecialPrm = new System.Windows.Forms.TabPage();
            this.nudOffsetY = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetX = new System.Windows.Forms.NumericUpDown();
            this.lblOffsetX = new System.Windows.Forms.Label();
            this.tbArcSpeed = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblArcSpeed = new System.Windows.Forms.Label();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMHCount = new Anda.Fluid.Controls.IntTextBox();
            this.tbTransitionR = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnModifyParam = new System.Windows.Forms.Button();
            this.lblLineType = new System.Windows.Forms.Label();
            this.cbxLineType = new System.Windows.Forms.ComboBox();
            this.btnLineStyleEdit = new System.Windows.Forms.Button();
            this.cmsPointUpDown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiPointUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiPointDown = new System.Windows.Forms.ToolStripMenuItem();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.tab.SuspendLayout();
            this.tbgSymbol.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tbgMeasureHeight.SuspendLayout();
            this.tbgSpecialPrm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            this.cmsPointUpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnModifyParam);
            this.gbx1.Controls.Add(this.tbTransitionR);
            this.gbx1.Controls.Add(this.label3);
            this.gbx1.Controls.Add(this.tbMHCount);
            this.gbx1.Controls.Add(this.listBoxLines);
            this.gbx1.Controls.Add(this.label1);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnLineStyleEdit);
            this.gbx2.Controls.Add(this.cbxLineType);
            this.gbx2.Controls.Add(this.lblLineType);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.tab);
            this.gbx2.Size = new System.Drawing.Size(500, 280);
            // 
            // tab
            // 
            this.tab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tab.Controls.Add(this.tbgSymbol);
            this.tab.Controls.Add(this.tbgMeasureHeight);
            this.tab.Controls.Add(this.tbgSpecialPrm);
            this.tab.Location = new System.Drawing.Point(6, 19);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(485, 222);
            this.tab.TabIndex = 65;
            // 
            // tbgSymbol
            // 
            this.tbgSymbol.Controls.Add(this.chkModifyEnable);
            this.tbgSymbol.Controls.Add(this.toolStrip3);
            this.tbgSymbol.Controls.Add(this.btnDeletePoint);
            this.tbgSymbol.Controls.Add(this.btnModify);
            this.tbgSymbol.Controls.Add(this.btnCreateSymbols);
            this.tbgSymbol.Controls.Add(this.label2);
            this.tbgSymbol.Controls.Add(this.rdoLine);
            this.tbgSymbol.Controls.Add(this.rdoArc);
            this.tbgSymbol.Controls.Add(this.btnAdd);
            this.tbgSymbol.Controls.Add(this.btnGotoStart);
            this.tbgSymbol.Controls.Add(this.btnSelectStart);
            this.tbgSymbol.Controls.Add(this.tbPointY);
            this.tbgSymbol.Controls.Add(this.tbPointX);
            this.tbgSymbol.Controls.Add(this.label4);
            this.tbgSymbol.Controls.Add(this.listBoxPoints);
            this.tbgSymbol.Location = new System.Drawing.Point(25, 4);
            this.tbgSymbol.Name = "tbgSymbol";
            this.tbgSymbol.Padding = new System.Windows.Forms.Padding(3);
            this.tbgSymbol.Size = new System.Drawing.Size(456, 214);
            this.tbgSymbol.TabIndex = 0;
            this.tbgSymbol.Text = "轨迹";
            // 
            // chkModifyEnable
            // 
            this.chkModifyEnable.AutoSize = true;
            this.chkModifyEnable.Location = new System.Drawing.Point(10, 48);
            this.chkModifyEnable.Name = "chkModifyEnable";
            this.chkModifyEnable.Size = new System.Drawing.Size(143, 18);
            this.chkModifyEnable.TabIndex = 79;
            this.chkModifyEnable.Text = "运行时进行手动校正";
            this.chkModifyEnable.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tipPrev,
            this.tipNext});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolStrip3.Location = new System.Drawing.Point(428, 112);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(24, 46);
            this.toolStrip3.TabIndex = 78;
            // 
            // tipPrev
            // 
            this.tipPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tipPrev.Image = global::Anda.Fluid.App.Properties.Resources.Prev;
            this.tipPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tipPrev.Name = "tipPrev";
            this.tipPrev.Size = new System.Drawing.Size(23, 20);
            this.tipPrev.Text = "tipPrev";
            this.tipPrev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // tipNext
            // 
            this.tipNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tipNext.Image = global::Anda.Fluid.App.Properties.Resources.Next;
            this.tipNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tipNext.Name = "tipNext";
            this.tipNext.Size = new System.Drawing.Size(23, 20);
            this.tipNext.Text = "tipNext";
            this.tipNext.Click += new System.EventHandler(this.Next_Click);
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Location = new System.Drawing.Point(214, 40);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePoint.TabIndex = 77;
            this.btnDeletePoint.Text = "删除";
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            this.btnDeletePoint.Click += new System.EventHandler(this.btnDeletePoint_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(295, 40);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 76;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnCreateSymbols
            // 
            this.btnCreateSymbols.Location = new System.Drawing.Point(376, 40);
            this.btnCreateSymbols.Name = "btnCreateSymbols";
            this.btnCreateSymbols.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSymbols.TabIndex = 75;
            this.btnCreateSymbols.Text = "生成轨迹";
            this.btnCreateSymbols.UseVisualStyleBackColor = true;
            this.btnCreateSymbols.Click += new System.EventHandler(this.btnCreateSymbols_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 14);
            this.label2.TabIndex = 74;
            this.label2.Text = "点类型:";
            // 
            // rdoLine
            // 
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new System.Drawing.Point(136, 28);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new System.Drawing.Size(64, 18);
            this.rdoLine.TabIndex = 73;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "直线点";
            this.rdoLine.UseVisualStyleBackColor = true;
            // 
            // rdoArc
            // 
            this.rdoArc.AutoSize = true;
            this.rdoArc.Location = new System.Drawing.Point(66, 28);
            this.rdoArc.Name = "rdoArc";
            this.rdoArc.Size = new System.Drawing.Size(64, 18);
            this.rdoArc.TabIndex = 72;
            this.rdoArc.TabStop = true;
            this.rdoArc.Text = "圆弧点";
            this.rdoArc.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(376, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 71;
            this.btnAdd.Text = "示教";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Location = new System.Drawing.Point(295, 5);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 70;
            this.btnGotoStart.Text = "到位置";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Location = new System.Drawing.Point(214, 5);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 69;
            this.btnSelectStart.Text = "修改";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // tbPointY
            // 
            this.tbPointY.BackColor = System.Drawing.Color.White;
            this.tbPointY.Location = new System.Drawing.Point(136, 5);
            this.tbPointY.Name = "tbPointY";
            this.tbPointY.Size = new System.Drawing.Size(72, 22);
            this.tbPointY.TabIndex = 67;
            this.tbPointY.Text = "0.000";
            // 
            // tbPointX
            // 
            this.tbPointX.BackColor = System.Drawing.Color.White;
            this.tbPointX.Location = new System.Drawing.Point(58, 5);
            this.tbPointX.Name = "tbPointX";
            this.tbPointX.Size = new System.Drawing.Size(72, 22);
            this.tbPointX.TabIndex = 68;
            this.tbPointX.Text = "0.000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 14);
            this.label4.TabIndex = 66;
            this.label4.Text = "坐标:";
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPoints.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(6, 70);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(398, 130);
            this.listBoxPoints.TabIndex = 65;
            this.listBoxPoints.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxPoints_DrawItem);
            // 
            // tbgMeasureHeight
            // 
            this.tbgMeasureHeight.Controls.Add(this.btnGoMHPos);
            this.tbgMeasureHeight.Controls.Add(this.label5);
            this.tbgMeasureHeight.Controls.Add(this.tbMHZPos);
            this.tbgMeasureHeight.Controls.Add(this.btnMHCmdLine);
            this.tbgMeasureHeight.Controls.Add(this.heightControl1);
            this.tbgMeasureHeight.Location = new System.Drawing.Point(25, 4);
            this.tbgMeasureHeight.Name = "tbgMeasureHeight";
            this.tbgMeasureHeight.Padding = new System.Windows.Forms.Padding(3);
            this.tbgMeasureHeight.Size = new System.Drawing.Size(456, 214);
            this.tbgMeasureHeight.TabIndex = 1;
            this.tbgMeasureHeight.Text = "测高";
            // 
            // btnGoMHPos
            // 
            this.btnGoMHPos.Location = new System.Drawing.Point(224, 175);
            this.btnGoMHPos.Name = "btnGoMHPos";
            this.btnGoMHPos.Size = new System.Drawing.Size(100, 23);
            this.btnGoMHPos.TabIndex = 15;
            this.btnGoMHPos.Text = "测高到位置";
            this.btnGoMHPos.UseVisualStyleBackColor = true;
            this.btnGoMHPos.Click += new System.EventHandler(this.btnGoMHPos_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(221, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "测高Z轴高度：";
            // 
            // tbMHZPos
            // 
            this.tbMHZPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbMHZPos.Location = new System.Drawing.Point(330, 150);
            this.tbMHZPos.Name = "tbMHZPos";
            this.tbMHZPos.Size = new System.Drawing.Size(100, 22);
            this.tbMHZPos.TabIndex = 13;
            // 
            // btnMHCmdLine
            // 
            this.btnMHCmdLine.Location = new System.Drawing.Point(20, 175);
            this.btnMHCmdLine.Name = "btnMHCmdLine";
            this.btnMHCmdLine.Size = new System.Drawing.Size(75, 23);
            this.btnMHCmdLine.TabIndex = 12;
            this.btnMHCmdLine.Text = "生成测高";
            this.btnMHCmdLine.UseVisualStyleBackColor = true;
            this.btnMHCmdLine.Click += new System.EventHandler(this.btnMHCmdLine_Click);
            // 
            // heightControl1
            // 
            this.heightControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightControl1.Location = new System.Drawing.Point(6, 6);
            this.heightControl1.Name = "heightControl1";
            this.heightControl1.Size = new System.Drawing.Size(445, 138);
            this.heightControl1.TabIndex = 11;
            // 
            // tbgSpecialPrm
            // 
            this.tbgSpecialPrm.BackColor = System.Drawing.SystemColors.Control;
            this.tbgSpecialPrm.Controls.Add(this.nudOffsetY);
            this.tbgSpecialPrm.Controls.Add(this.nudOffsetX);
            this.tbgSpecialPrm.Controls.Add(this.lblOffsetX);
            this.tbgSpecialPrm.Controls.Add(this.tbArcSpeed);
            this.tbgSpecialPrm.Controls.Add(this.lblArcSpeed);
            this.tbgSpecialPrm.Location = new System.Drawing.Point(25, 4);
            this.tbgSpecialPrm.Name = "tbgSpecialPrm";
            this.tbgSpecialPrm.Padding = new System.Windows.Forms.Padding(3);
            this.tbgSpecialPrm.Size = new System.Drawing.Size(456, 214);
            this.tbgSpecialPrm.TabIndex = 2;
            this.tbgSpecialPrm.Text = "附加参数";
            // 
            // nudOffsetY
            // 
            this.nudOffsetY.Location = new System.Drawing.Point(368, 14);
            this.nudOffsetY.Name = "nudOffsetY";
            this.nudOffsetY.Size = new System.Drawing.Size(66, 22);
            this.nudOffsetY.TabIndex = 5;
            // 
            // nudOffsetX
            // 
            this.nudOffsetX.Location = new System.Drawing.Point(291, 14);
            this.nudOffsetX.Name = "nudOffsetX";
            this.nudOffsetX.Size = new System.Drawing.Size(66, 22);
            this.nudOffsetX.TabIndex = 3;
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new System.Drawing.Point(207, 18);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(83, 14);
            this.lblOffsetX.TabIndex = 2;
            this.lblOffsetX.Text = "偏移(X,Y)：";
            // 
            // tbArcSpeed
            // 
            this.tbArcSpeed.BackColor = System.Drawing.Color.White;
            this.tbArcSpeed.Location = new System.Drawing.Point(128, 11);
            this.tbArcSpeed.Name = "tbArcSpeed";
            this.tbArcSpeed.Size = new System.Drawing.Size(66, 22);
            this.tbArcSpeed.TabIndex = 1;
            // 
            // lblArcSpeed
            // 
            this.lblArcSpeed.Location = new System.Drawing.Point(25, 14);
            this.lblArcSpeed.Name = "lblArcSpeed";
            this.lblArcSpeed.Size = new System.Drawing.Size(97, 16);
            this.lblArcSpeed.TabIndex = 0;
            this.lblArcSpeed.Text = "圆弧基准速度:";
            // 
            // listBoxLines
            // 
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.ItemHeight = 14;
            this.listBoxLines.Location = new System.Drawing.Point(7, 76);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxLines.Size = new System.Drawing.Size(255, 200);
            this.listBoxLines.TabIndex = 22;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(407, 251);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 66;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(326, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 67;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 14);
            this.label1.TabIndex = 72;
            this.label1.Text = "轨迹测高数量:";
            // 
            // tbMHCount
            // 
            this.tbMHCount.BackColor = System.Drawing.Color.White;
            this.tbMHCount.Location = new System.Drawing.Point(129, 15);
            this.tbMHCount.Name = "tbMHCount";
            this.tbMHCount.Size = new System.Drawing.Size(72, 22);
            this.tbMHCount.TabIndex = 73;
            this.tbMHCount.Text = "1";
            // 
            // tbTransitionR
            // 
            this.tbTransitionR.BackColor = System.Drawing.Color.White;
            this.tbTransitionR.Location = new System.Drawing.Point(129, 42);
            this.tbTransitionR.Name = "tbTransitionR";
            this.tbTransitionR.Size = new System.Drawing.Size(72, 22);
            this.tbTransitionR.TabIndex = 75;
            this.tbTransitionR.Text = "2.000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 14);
            this.label3.TabIndex = 74;
            this.label3.Text = "过渡半径(mm):";
            // 
            // btnModifyParam
            // 
            this.btnModifyParam.Location = new System.Drawing.Point(207, 41);
            this.btnModifyParam.Name = "btnModifyParam";
            this.btnModifyParam.Size = new System.Drawing.Size(54, 23);
            this.btnModifyParam.TabIndex = 76;
            this.btnModifyParam.Text = "修改";
            this.btnModifyParam.UseVisualStyleBackColor = true;
            this.btnModifyParam.Click += new System.EventHandler(this.btnModifyParam_Click);
            // 
            // lblLineType
            // 
            this.lblLineType.AutoSize = true;
            this.lblLineType.Location = new System.Drawing.Point(28, 255);
            this.lblLineType.Name = "lblLineType";
            this.lblLineType.Size = new System.Drawing.Size(76, 14);
            this.lblLineType.TabIndex = 68;
            this.lblLineType.Text = "Line Type:";
            // 
            // cbxLineType
            // 
            this.cbxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLineType.FormattingEnabled = true;
            this.cbxLineType.Location = new System.Drawing.Point(111, 251);
            this.cbxLineType.Name = "cbxLineType";
            this.cbxLineType.Size = new System.Drawing.Size(97, 22);
            this.cbxLineType.TabIndex = 69;
            // 
            // btnLineStyleEdit
            // 
            this.btnLineStyleEdit.Location = new System.Drawing.Point(214, 250);
            this.btnLineStyleEdit.Name = "btnLineStyleEdit";
            this.btnLineStyleEdit.Size = new System.Drawing.Size(75, 23);
            this.btnLineStyleEdit.TabIndex = 70;
            this.btnLineStyleEdit.Text = "编辑";
            this.btnLineStyleEdit.UseVisualStyleBackColor = true;
            this.btnLineStyleEdit.Click += new System.EventHandler(this.btnLineStyleEdit_Click);
            // 
            // cmsPointUpDown
            // 
            this.cmsPointUpDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiPointUp,
            this.tsiPointDown});
            this.cmsPointUpDown.Name = "cmsPointUpDown";
            this.cmsPointUpDown.Size = new System.Drawing.Size(137, 48);
            // 
            // tsiPointUp
            // 
            this.tsiPointUp.Name = "tsiPointUp";
            this.tsiPointUp.Size = new System.Drawing.Size(136, 22);
            this.tsiPointUp.Text = "上移此点位";
            this.tsiPointUp.Click += new System.EventHandler(this.tsiPointUp_Click);
            // 
            // tsiPointDown
            // 
            this.tsiPointDown.Name = "tsiPointDown";
            this.tsiPointDown.Size = new System.Drawing.Size(136, 22);
            this.tsiPointDown.Text = "下移此点位";
            this.tsiPointDown.Click += new System.EventHandler(this.tsiPointDown_Click);
            // 
            // EditSymbolLinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 692);
            this.Name = "EditSymbolLinesForm";
            this.Text = "EditSymbolLinesForm";
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.tab.ResumeLayout(false);
            this.tbgSymbol.ResumeLayout(false);
            this.tbgSymbol.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tbgMeasureHeight.ResumeLayout(false);
            this.tbgMeasureHeight.PerformLayout();
            this.tbgSpecialPrm.ResumeLayout(false);
            this.tbgSpecialPrm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).EndInit();
            this.cmsPointUpDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tbgSymbol;
        private System.Windows.Forms.TabPage tbgMeasureHeight;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnSelectStart;
        private Controls.DoubleTextBox tbPointY;
        private Controls.DoubleTextBox tbPointX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxLines;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private Controls.IntTextBox tbMHCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoLine;
        private System.Windows.Forms.RadioButton rdoArc;
        private System.Windows.Forms.Button btnCreateSymbols;
        private System.Windows.Forms.Button btnModify;
        private HeightControl heightControl1;
        private System.Windows.Forms.Button btnMHCmdLine;
        private Controls.DoubleTextBox tbTransitionR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnModifyParam;
        private System.Windows.Forms.Label lblLineType;
        private System.Windows.Forms.ComboBox cbxLineType;
        private System.Windows.Forms.Label label5;
        private Controls.DoubleTextBox tbMHZPos;
        private System.Windows.Forms.Button btnDeletePoint;
        private System.Windows.Forms.Button btnLineStyleEdit;
        private System.Windows.Forms.ContextMenuStrip cmsPointUpDown;
        private System.Windows.Forms.ToolStripMenuItem tsiPointUp;
        private System.Windows.Forms.ToolStripMenuItem tsiPointDown;
        private System.Windows.Forms.Button btnGoMHPos;
        private System.Windows.Forms.TabPage tbgSpecialPrm;
        private System.Windows.Forms.Label lblArcSpeed;
        private Controls.DoubleTextBox tbArcSpeed;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tipPrev;
        private System.Windows.Forms.ToolStripButton tipNext;
        private System.Windows.Forms.NumericUpDown nudOffsetY;
        private System.Windows.Forms.NumericUpDown nudOffsetX;
        private System.Windows.Forms.Label lblOffsetX;
        private System.Windows.Forms.CheckBox chkModifyEnable;
    }
}