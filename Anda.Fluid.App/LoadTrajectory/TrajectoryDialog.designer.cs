namespace Anda.Fluid.App.LoadTrajectory
{
    partial class TrajectoryDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblPatternName = new System.Windows.Forms.Label();
            this.txtPatternName = new System.Windows.Forms.TextBox();
            this.btnReLoad = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lswComponents = new System.Windows.Forms.ListView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMark2Y = new System.Windows.Forms.TextBox();
            this.txtMark2X = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOffset = new System.Windows.Forms.Button();
            this.lblmark1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMark1Y = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMark1X = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudMinX = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetY = new System.Windows.Forms.NumericUpDown();
            this.nudMaxX = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetX = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudMinY = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudMaxY = new System.Windows.Forms.NumericUpDown();
            this.lblDistance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDis = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.ckbLoadDefault = new System.Windows.Forms.CheckBox();
            this.lswPath = new System.Windows.Forms.ListView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trajectoryChart1 = new Anda.Fluid.App.LoadTrajectory.TrajectoryChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxY)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(396, 21);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblPatternName
            // 
            this.lblPatternName.AutoSize = true;
            this.lblPatternName.Location = new System.Drawing.Point(8, 313);
            this.lblPatternName.Name = "lblPatternName";
            this.lblPatternName.Size = new System.Drawing.Size(94, 14);
            this.lblPatternName.TabIndex = 4;
            this.lblPatternName.Text = "PatternName";
            // 
            // txtPatternName
            // 
            this.txtPatternName.Location = new System.Drawing.Point(108, 310);
            this.txtPatternName.Name = "txtPatternName";
            this.txtPatternName.Size = new System.Drawing.Size(174, 22);
            this.txtPatternName.TabIndex = 5;
            // 
            // btnReLoad
            // 
            this.btnReLoad.Location = new System.Drawing.Point(395, 45);
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.Size = new System.Drawing.Size(71, 27);
            this.btnReLoad.TabIndex = 8;
            this.btnReLoad.Text = "Selecte";
            this.btnReLoad.UseVisualStyleBackColor = true;
            this.btnReLoad.Click += new System.EventHandler(this.btnReLoad_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(769, 313);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(671, 313);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCreate);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.lblPatternName);
            this.splitContainer1.Panel2.Controls.Add(this.txtPatternName);
            this.splitContainer1.Size = new System.Drawing.Size(859, 706);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.TabIndex = 12;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lswComponents);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.trajectoryChart1);
            this.splitContainer2.Size = new System.Drawing.Size(857, 352);
            this.splitContainer2.SplitterDistance = 278;
            this.splitContainer2.TabIndex = 0;
            // 
            // lswComponents
            // 
            this.lswComponents.BackColor = System.Drawing.SystemColors.Control;
            this.lswComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lswComponents.FullRowSelect = true;
            this.lswComponents.GridLines = true;
            this.lswComponents.Location = new System.Drawing.Point(0, 0);
            this.lswComponents.Name = "lswComponents";
            this.lswComponents.Size = new System.Drawing.Size(278, 352);
            this.lswComponents.TabIndex = 0;
            this.lswComponents.UseCompatibleStateImageBehavior = false;
            this.lswComponents.View = System.Windows.Forms.View.Details;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(310, 310);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 18;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMark2Y);
            this.groupBox2.Controls.Add(this.txtMark2X);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnOffset);
            this.groupBox2.Controls.Add(this.lblmark1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtMark1Y);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtMark1X);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudMinX);
            this.groupBox2.Controls.Add(this.nudOffsetY);
            this.groupBox2.Controls.Add(this.nudMaxX);
            this.groupBox2.Controls.Add(this.nudOffsetX);
            this.groupBox2.Controls.Add(this.btnReLoad);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.nudMinY);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nudMaxY);
            this.groupBox2.Controls.Add(this.lblDistance);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblDis);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnOptimize);
            this.groupBox2.Location = new System.Drawing.Point(4, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(843, 159);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optimize";
            // 
            // txtMark2Y
            // 
            this.txtMark2Y.Location = new System.Drawing.Point(639, 76);
            this.txtMark2Y.Name = "txtMark2Y";
            this.txtMark2Y.ReadOnly = true;
            this.txtMark2Y.Size = new System.Drawing.Size(77, 22);
            this.txtMark2Y.TabIndex = 31;
            // 
            // txtMark2X
            // 
            this.txtMark2X.Location = new System.Drawing.Point(556, 77);
            this.txtMark2X.Name = "txtMark2X";
            this.txtMark2X.ReadOnly = true;
            this.txtMark2X.Size = new System.Drawing.Size(77, 22);
            this.txtMark2X.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(495, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 21;
            this.label1.Text = "mark2:";
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(395, 122);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(71, 27);
            this.btnOffset.TabIndex = 29;
            this.btnOffset.Text = "Offset";
            this.btnOffset.UseVisualStyleBackColor = true;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // lblmark1
            // 
            this.lblmark1.AutoSize = true;
            this.lblmark1.Location = new System.Drawing.Point(495, 45);
            this.lblmark1.Name = "lblmark1";
            this.lblmark1.Size = new System.Drawing.Size(55, 14);
            this.lblmark1.TabIndex = 20;
            this.lblmark1.Text = "mark1:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(240, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 14);
            this.label10.TabIndex = 28;
            this.label10.Text = "Y";
            // 
            // txtMark1Y
            // 
            this.txtMark1Y.Location = new System.Drawing.Point(639, 41);
            this.txtMark1Y.Name = "txtMark1Y";
            this.txtMark1Y.ReadOnly = true;
            this.txtMark1Y.Size = new System.Drawing.Size(77, 22);
            this.txtMark1Y.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Scope";
            // 
            // txtMark1X
            // 
            this.txtMark1X.Location = new System.Drawing.Point(556, 42);
            this.txtMark1X.Name = "txtMark1X";
            this.txtMark1X.ReadOnly = true;
            this.txtMark1X.Size = new System.Drawing.Size(77, 22);
            this.txtMark1X.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 14);
            this.label9.TabIndex = 27;
            this.label9.Text = "X";
            // 
            // nudMinX
            // 
            this.nudMinX.DecimalPlaces = 3;
            this.nudMinX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMinX.Location = new System.Drawing.Point(79, 47);
            this.nudMinX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMinX.Name = "nudMinX";
            this.nudMinX.Size = new System.Drawing.Size(98, 22);
            this.nudMinX.TabIndex = 12;
            // 
            // nudOffsetY
            // 
            this.nudOffsetY.DecimalPlaces = 3;
            this.nudOffsetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffsetY.Location = new System.Drawing.Point(271, 124);
            this.nudOffsetY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudOffsetY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudOffsetY.Name = "nudOffsetY";
            this.nudOffsetY.Size = new System.Drawing.Size(98, 22);
            this.nudOffsetY.TabIndex = 26;
            // 
            // nudMaxX
            // 
            this.nudMaxX.DecimalPlaces = 3;
            this.nudMaxX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMaxX.Location = new System.Drawing.Point(271, 47);
            this.nudMaxX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMaxX.Name = "nudMaxX";
            this.nudMaxX.Size = new System.Drawing.Size(98, 22);
            this.nudMaxX.TabIndex = 13;
            // 
            // nudOffsetX
            // 
            this.nudOffsetX.DecimalPlaces = 3;
            this.nudOffsetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffsetX.Location = new System.Drawing.Point(79, 124);
            this.nudOffsetX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudOffsetX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudOffsetX.Name = "nudOffsetX";
            this.nudOffsetX.Size = new System.Drawing.Size(98, 22);
            this.nudOffsetX.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "Offset";
            // 
            // nudMinY
            // 
            this.nudMinY.DecimalPlaces = 3;
            this.nudMinY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMinY.Location = new System.Drawing.Point(79, 79);
            this.nudMinY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMinY.Name = "nudMinY";
            this.nudMinY.Size = new System.Drawing.Size(98, 22);
            this.nudMinY.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(749, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 14);
            this.label8.TabIndex = 23;
            this.label8.Text = "mm";
            // 
            // nudMaxY
            // 
            this.nudMaxY.DecimalPlaces = 3;
            this.nudMaxY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMaxY.Location = new System.Drawing.Point(271, 79);
            this.nudMaxY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMaxY.Name = "nudMaxY";
            this.nudMaxY.Size = new System.Drawing.Size(98, 22);
            this.nudMaxY.TabIndex = 15;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(674, 132);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(29, 14);
            this.lblDistance.TabIndex = 22;
            this.lblDistance.Text = "0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "MinX";
            // 
            // lblDis
            // 
            this.lblDis.AutoSize = true;
            this.lblDis.Location = new System.Drawing.Point(603, 132);
            this.lblDis.Name = "lblDis";
            this.lblDis.Size = new System.Drawing.Size(69, 14);
            this.lblDis.TabIndex = 21;
            this.lblDis.Text = "Distance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "MaxX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 14);
            this.label6.TabIndex = 20;
            this.label6.Text = "MaxY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "MinY";
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(498, 122);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(71, 27);
            this.btnOptimize.TabIndex = 11;
            this.btnOptimize.Text = "Optimize";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.ckbLoadDefault);
            this.groupBox1.Controls.Add(this.lswPath);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(844, 132);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(396, 92);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 27);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(15, 62);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(126, 27);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // ckbLoadDefault
            // 
            this.ckbLoadDefault.AutoSize = true;
            this.ckbLoadDefault.Location = new System.Drawing.Point(396, 54);
            this.ckbLoadDefault.Name = "ckbLoadDefault";
            this.ckbLoadDefault.Size = new System.Drawing.Size(106, 18);
            this.ckbLoadDefault.TabIndex = 9;
            this.ckbLoadDefault.Text = "LoadDefault";
            this.ckbLoadDefault.UseVisualStyleBackColor = true;
            // 
            // lswPath
            // 
            this.lswPath.Location = new System.Drawing.Point(9, 21);
            this.lswPath.Name = "lswPath";
            this.lswPath.Size = new System.Drawing.Size(361, 105);
            this.lswPath.TabIndex = 7;
            this.lswPath.UseCompatibleStateImageBehavior = false;
            this.lswPath.SelectedIndexChanged += new System.EventHandler(this.lswPath_SelectedIndexChanged);
            this.lswPath.MouseEnter += new System.EventHandler(this.lswPath_MouseEnter);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(494, 21);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 27);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 27);
            this.button2.TabIndex = 12;
            this.button2.Text = "EditComponens";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.btnGenerate);
            this.groupBox3.Location = new System.Drawing.Point(578, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 100);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EditComponents";
            // 
            // trajectoryChart1
            // 
            this.trajectoryChart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trajectoryChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryChart1.Location = new System.Drawing.Point(0, 0);
            this.trajectoryChart1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trajectoryChart1.Name = "trajectoryChart1";
            this.trajectoryChart1.Size = new System.Drawing.Size(575, 352);
            this.trajectoryChart1.TabIndex = 0;
            // 
            // TrajectoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 706);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TrajectoryDialog";
            this.Text = "TrajectoryDialog";
            this.Load += new System.EventHandler(this.TrajectoryDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxY)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblPatternName;
        private System.Windows.Forms.TextBox txtPatternName;
        private System.Windows.Forms.Button btnReLoad;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxY;
        private System.Windows.Forms.NumericUpDown nudMinY;
        private System.Windows.Forms.NumericUpDown nudMaxX;
        private System.Windows.Forms.NumericUpDown nudMinX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblDis;
        private System.Windows.Forms.NumericUpDown nudOffsetX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudOffsetY;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lswComponents;
        private System.Windows.Forms.ListView lswPath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnOffset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox ckbLoadDefault;
        private TrajectoryChart trajectoryChart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblmark1;
        private System.Windows.Forms.TextBox txtMark1Y;
        private System.Windows.Forms.TextBox txtMark1X;
        private System.Windows.Forms.TextBox txtMark2Y;
        private System.Windows.Forms.TextBox txtMark2X;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
    }
}