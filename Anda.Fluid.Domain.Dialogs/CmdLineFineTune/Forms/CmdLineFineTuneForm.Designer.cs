namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.Forms
{
    partial class CmdLineFineTuneForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbKeyDis = new Anda.Fluid.Domain.Motion.JogComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRightClickDis = new Anda.Fluid.Domain.Motion.JogComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLeftClickDis = new Anda.Fluid.Domain.Motion.JogComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPointY = new System.Windows.Forms.TextBox();
            this.txtPointX = new System.Windows.Forms.TextBox();
            this.txtCurrY = new System.Windows.Forms.TextBox();
            this.txtCurrX = new System.Windows.Forms.TextBox();
            this.btnAutoMove = new System.Windows.Forms.Button();
            this.btnMoveToNext = new System.Windows.Forms.Button();
            this.btnMoveToPre = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.btnDownRigth = new System.Windows.Forms.Button();
            this.btnUpRight = new System.Windows.Forms.Button();
            this.btnUpLeft = new System.Windows.Forms.Button();
            this.btnDownLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelectedAll = new System.Windows.Forms.Button();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDoMultiPassPattern = new System.Windows.Forms.CheckBox();
            this.chkStepAndRepeat = new System.Windows.Forms.CheckBox();
            this.chkDoPattern = new System.Windows.Forms.CheckBox();
            this.chkSymbolLines = new System.Windows.Forms.CheckBox();
            this.chkSnakeLines = new System.Windows.Forms.CheckBox();
            this.chkMultiLines = new System.Windows.Forms.CheckBox();
            this.chkPolyLine = new System.Windows.Forms.CheckBox();
            this.chkArcEnable = new System.Windows.Forms.CheckBox();
            this.chkSingleLine = new System.Windows.Forms.CheckBox();
            this.chkDot = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CmdLineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdLineTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PointType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Skip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.XValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cameraControl1);
            this.groupBox1.Location = new System.Drawing.Point(0, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 379);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(3, 18);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(587, 358);
            this.cameraControl1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbKeyDis);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbRightClickDis);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbLeftClickDis);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtPointY);
            this.groupBox2.Controls.Add(this.txtPointX);
            this.groupBox2.Controls.Add(this.txtCurrY);
            this.groupBox2.Controls.Add(this.txtCurrX);
            this.groupBox2.Controls.Add(this.btnAutoMove);
            this.groupBox2.Controls.Add(this.btnMoveToNext);
            this.groupBox2.Controls.Add(this.btnMoveToPre);
            this.groupBox2.Controls.Add(this.btnTeach);
            this.groupBox2.Controls.Add(this.btnDownRigth);
            this.groupBox2.Controls.Add(this.btnUpRight);
            this.groupBox2.Controls.Add(this.btnUpLeft);
            this.groupBox2.Controls.Add(this.btnDownLeft);
            this.groupBox2.Controls.Add(this.btnRight);
            this.groupBox2.Controls.Add(this.btnLeft);
            this.groupBox2.Controls.Add(this.btnUp);
            this.groupBox2.Controls.Add(this.btnDown);
            this.groupBox2.Location = new System.Drawing.Point(596, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 376);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 14);
            this.label7.TabIndex = 25;
            this.label7.Text = "键盘方向键寸动距离:";
            // 
            // cmbKeyDis
            // 
            this.cmbKeyDis.FormattingEnabled = true;
            this.cmbKeyDis.Location = new System.Drawing.Point(147, 340);
            this.cmbKeyDis.Name = "cmbKeyDis";
            this.cmbKeyDis.Size = new System.Drawing.Size(102, 22);
            this.cmbKeyDis.TabIndex = 24;
            this.cmbKeyDis.TextChanged += new System.EventHandler(this.cmbKeyDis_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 14);
            this.label6.TabIndex = 23;
            this.label6.Text = "鼠标右键单击寸动距离:";
            // 
            // cmbRightClickDis
            // 
            this.cmbRightClickDis.FormattingEnabled = true;
            this.cmbRightClickDis.Location = new System.Drawing.Point(147, 312);
            this.cmbRightClickDis.Name = "cmbRightClickDis";
            this.cmbRightClickDis.Size = new System.Drawing.Size(100, 22);
            this.cmbRightClickDis.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 14);
            this.label5.TabIndex = 21;
            this.label5.Text = "鼠标左键单击寸动距离:";
            // 
            // cmbLeftClickDis
            // 
            this.cmbLeftClickDis.FormattingEnabled = true;
            this.cmbLeftClickDis.Location = new System.Drawing.Point(147, 284);
            this.cmbLeftClickDis.Name = "cmbLeftClickDis";
            this.cmbLeftClickDis.Size = new System.Drawing.Size(100, 22);
            this.cmbLeftClickDis.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 14);
            this.label4.TabIndex = 19;
            this.label4.Text = "实时:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "选点:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 17;
            this.label2.Text = "Y(mm):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "X(mm):";
            // 
            // txtPointY
            // 
            this.txtPointY.Location = new System.Drawing.Point(147, 35);
            this.txtPointY.Name = "txtPointY";
            this.txtPointY.ReadOnly = true;
            this.txtPointY.Size = new System.Drawing.Size(100, 22);
            this.txtPointY.TabIndex = 15;
            // 
            // txtPointX
            // 
            this.txtPointX.Location = new System.Drawing.Point(41, 35);
            this.txtPointX.Name = "txtPointX";
            this.txtPointX.ReadOnly = true;
            this.txtPointX.Size = new System.Drawing.Size(100, 22);
            this.txtPointX.TabIndex = 14;
            // 
            // txtCurrY
            // 
            this.txtCurrY.Location = new System.Drawing.Point(147, 63);
            this.txtCurrY.Name = "txtCurrY";
            this.txtCurrY.ReadOnly = true;
            this.txtCurrY.Size = new System.Drawing.Size(100, 22);
            this.txtCurrY.TabIndex = 13;
            // 
            // txtCurrX
            // 
            this.txtCurrX.Location = new System.Drawing.Point(41, 63);
            this.txtCurrX.Name = "txtCurrX";
            this.txtCurrX.ReadOnly = true;
            this.txtCurrX.Size = new System.Drawing.Size(100, 22);
            this.txtCurrX.TabIndex = 12;
            // 
            // btnAutoMove
            // 
            this.btnAutoMove.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoMove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAutoMove.Location = new System.Drawing.Point(194, 222);
            this.btnAutoMove.Name = "btnAutoMove";
            this.btnAutoMove.Size = new System.Drawing.Size(56, 56);
            this.btnAutoMove.TabIndex = 11;
            this.btnAutoMove.Text = "自动跟踪";
            this.btnAutoMove.UseVisualStyleBackColor = true;
            this.btnAutoMove.Click += new System.EventHandler(this.btnAutoMove_Click);
            // 
            // btnMoveToNext
            // 
            this.btnMoveToNext.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveToNext.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveToNext.Location = new System.Drawing.Point(194, 160);
            this.btnMoveToNext.Name = "btnMoveToNext";
            this.btnMoveToNext.Size = new System.Drawing.Size(56, 56);
            this.btnMoveToNext.TabIndex = 10;
            this.btnMoveToNext.Text = "向下跟踪";
            this.btnMoveToNext.UseVisualStyleBackColor = true;
            this.btnMoveToNext.Click += new System.EventHandler(this.btnMoveToNext_Click);
            // 
            // btnMoveToPre
            // 
            this.btnMoveToPre.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveToPre.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveToPre.Location = new System.Drawing.Point(194, 98);
            this.btnMoveToPre.Name = "btnMoveToPre";
            this.btnMoveToPre.Size = new System.Drawing.Size(56, 56);
            this.btnMoveToPre.TabIndex = 9;
            this.btnMoveToPre.Text = "向上跟踪";
            this.btnMoveToPre.UseVisualStyleBackColor = true;
            this.btnMoveToPre.Click += new System.EventHandler(this.btnMoveToPre_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeach.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTeach.Location = new System.Drawing.Point(68, 160);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(56, 56);
            this.btnTeach.TabIndex = 8;
            this.btnTeach.Text = "示教";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnDownRigth
            // 
            this.btnDownRigth.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Down_Right_52px;
            this.btnDownRigth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownRigth.Location = new System.Drawing.Point(132, 222);
            this.btnDownRigth.Name = "btnDownRigth";
            this.btnDownRigth.Size = new System.Drawing.Size(56, 56);
            this.btnDownRigth.TabIndex = 7;
            this.btnDownRigth.UseVisualStyleBackColor = true;
            this.btnDownRigth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDownRigth_MouseDown);
            // 
            // btnUpRight
            // 
            this.btnUpRight.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Up_Right_52px;
            this.btnUpRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpRight.Location = new System.Drawing.Point(132, 98);
            this.btnUpRight.Name = "btnUpRight";
            this.btnUpRight.Size = new System.Drawing.Size(56, 56);
            this.btnUpRight.TabIndex = 6;
            this.btnUpRight.UseVisualStyleBackColor = true;
            this.btnUpRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpRight_MouseDown);
            // 
            // btnUpLeft
            // 
            this.btnUpLeft.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Up_Left_52px;
            this.btnUpLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpLeft.Location = new System.Drawing.Point(6, 98);
            this.btnUpLeft.Name = "btnUpLeft";
            this.btnUpLeft.Size = new System.Drawing.Size(56, 56);
            this.btnUpLeft.TabIndex = 5;
            this.btnUpLeft.UseVisualStyleBackColor = true;
            this.btnUpLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpLeft_MouseDown);
            // 
            // btnDownLeft
            // 
            this.btnDownLeft.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Down_Left_52px;
            this.btnDownLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownLeft.Location = new System.Drawing.Point(6, 222);
            this.btnDownLeft.Name = "btnDownLeft";
            this.btnDownLeft.Size = new System.Drawing.Size(56, 56);
            this.btnDownLeft.TabIndex = 4;
            this.btnDownLeft.UseVisualStyleBackColor = true;
            this.btnDownLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDownLeft_MouseDown);
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Right_52px;
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRight.Location = new System.Drawing.Point(132, 160);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(56, 56);
            this.btnRight.TabIndex = 3;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
            // 
            // btnLeft
            // 
            this.btnLeft.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Left_52px;
            this.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLeft.Location = new System.Drawing.Point(6, 160);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(56, 56);
            this.btnLeft.TabIndex = 2;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Up_52px;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Location = new System.Drawing.Point(68, 98);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(56, 56);
            this.btnUp.TabIndex = 1;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Down_Arrow_52px;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Location = new System.Drawing.Point(68, 222);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(56, 56);
            this.btnDown.TabIndex = 0;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.listView1);
            this.groupBox3.Location = new System.Drawing.Point(3, -1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(281, 254);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(6, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(269, 235);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnSelectedAll);
            this.groupBox4.Controls.Add(this.btnMoveTo);
            this.groupBox4.Controls.Add(this.btnNext);
            this.groupBox4.Controls.Add(this.btnPre);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Location = new System.Drawing.Point(290, -1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(569, 254);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // btnSelectedAll
            // 
            this.btnSelectedAll.Location = new System.Drawing.Point(451, 21);
            this.btnSelectedAll.Name = "btnSelectedAll";
            this.btnSelectedAll.Size = new System.Drawing.Size(57, 27);
            this.btnSelectedAll.TabIndex = 12;
            this.btnSelectedAll.Text = "全不选";
            this.btnSelectedAll.UseVisualStyleBackColor = true;
            this.btnSelectedAll.Click += new System.EventHandler(this.btnSelectedAll_Click);
            // 
            // btnMoveTo
            // 
            this.btnMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveTo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveTo.Location = new System.Drawing.Point(514, 150);
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.Size = new System.Drawing.Size(46, 46);
            this.btnMoveTo.TabIndex = 28;
            this.btnMoveTo.Text = "定位";
            this.btnMoveTo.UseVisualStyleBackColor = true;
            this.btnMoveTo.Click += new System.EventHandler(this.btnMoveTo_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Chevron_Down_52px;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNext.Location = new System.Drawing.Point(514, 202);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(46, 46);
            this.btnNext.TabIndex = 27;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPre.BackgroundImage = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Chevron_Up_52px;
            this.btnPre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPre.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPre.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPre.Location = new System.Drawing.Point(514, 98);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(46, 46);
            this.btnPre.TabIndex = 26;
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDoMultiPassPattern);
            this.panel1.Controls.Add(this.chkStepAndRepeat);
            this.panel1.Controls.Add(this.chkDoPattern);
            this.panel1.Controls.Add(this.chkSymbolLines);
            this.panel1.Controls.Add(this.chkSnakeLines);
            this.panel1.Controls.Add(this.chkMultiLines);
            this.panel1.Controls.Add(this.chkPolyLine);
            this.panel1.Controls.Add(this.chkArcEnable);
            this.panel1.Controls.Add(this.chkSingleLine);
            this.panel1.Controls.Add(this.chkDot);
            this.panel1.Location = new System.Drawing.Point(6, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 44);
            this.panel1.TabIndex = 1;
            // 
            // chkDoMultiPassPattern
            // 
            this.chkDoMultiPassPattern.AutoSize = true;
            this.chkDoMultiPassPattern.Location = new System.Drawing.Point(287, 23);
            this.chkDoMultiPassPattern.Name = "chkDoMultiPassPattern";
            this.chkDoMultiPassPattern.Size = new System.Drawing.Size(104, 18);
            this.chkDoMultiPassPattern.TabIndex = 11;
            this.chkDoMultiPassPattern.Text = "执行分组拼板";
            this.chkDoMultiPassPattern.UseVisualStyleBackColor = true;
            this.chkDoMultiPassPattern.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkStepAndRepeat
            // 
            this.chkStepAndRepeat.AutoSize = true;
            this.chkStepAndRepeat.Location = new System.Drawing.Point(188, 23);
            this.chkStepAndRepeat.Name = "chkStepAndRepeat";
            this.chkStepAndRepeat.Size = new System.Drawing.Size(78, 18);
            this.chkStepAndRepeat.TabIndex = 10;
            this.chkStepAndRepeat.Text = "拼板阵列";
            this.chkStepAndRepeat.UseVisualStyleBackColor = true;
            this.chkStepAndRepeat.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkDoPattern
            // 
            this.chkDoPattern.AutoSize = true;
            this.chkDoPattern.Location = new System.Drawing.Point(89, 23);
            this.chkDoPattern.Name = "chkDoPattern";
            this.chkDoPattern.Size = new System.Drawing.Size(78, 18);
            this.chkDoPattern.TabIndex = 9;
            this.chkDoPattern.Text = "执行拼板";
            this.chkDoPattern.UseVisualStyleBackColor = true;
            this.chkDoPattern.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkSymbolLines
            // 
            this.chkSymbolLines.AutoSize = true;
            this.chkSymbolLines.Location = new System.Drawing.Point(3, 23);
            this.chkSymbolLines.Name = "chkSymbolLines";
            this.chkSymbolLines.Size = new System.Drawing.Size(65, 18);
            this.chkSymbolLines.TabIndex = 8;
            this.chkSymbolLines.Text = "复合线";
            this.chkSymbolLines.UseVisualStyleBackColor = true;
            this.chkSymbolLines.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkSnakeLines
            // 
            this.chkSnakeLines.AutoSize = true;
            this.chkSnakeLines.Location = new System.Drawing.Point(235, 3);
            this.chkSnakeLines.Name = "chkSnakeLines";
            this.chkSnakeLines.Size = new System.Drawing.Size(65, 18);
            this.chkSnakeLines.TabIndex = 7;
            this.chkSnakeLines.Text = "蛇形线";
            this.chkSnakeLines.UseVisualStyleBackColor = true;
            this.chkSnakeLines.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkMultiLines
            // 
            this.chkMultiLines.AutoSize = true;
            this.chkMultiLines.Location = new System.Drawing.Point(164, 3);
            this.chkMultiLines.Name = "chkMultiLines";
            this.chkMultiLines.Size = new System.Drawing.Size(65, 18);
            this.chkMultiLines.TabIndex = 6;
            this.chkMultiLines.Text = "多线段";
            this.chkMultiLines.UseVisualStyleBackColor = true;
            this.chkMultiLines.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkPolyLine
            // 
            this.chkPolyLine.AutoSize = true;
            this.chkPolyLine.Location = new System.Drawing.Point(93, 3);
            this.chkPolyLine.Name = "chkPolyLine";
            this.chkPolyLine.Size = new System.Drawing.Size(65, 18);
            this.chkPolyLine.TabIndex = 5;
            this.chkPolyLine.Text = "多段线";
            this.chkPolyLine.UseVisualStyleBackColor = true;
            this.chkPolyLine.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkArcEnable
            // 
            this.chkArcEnable.AutoSize = true;
            this.chkArcEnable.Location = new System.Drawing.Point(306, 3);
            this.chkArcEnable.Name = "chkArcEnable";
            this.chkArcEnable.Size = new System.Drawing.Size(86, 18);
            this.chkArcEnable.TabIndex = 4;
            this.chkArcEnable.Text = "圆弧/圆环";
            this.chkArcEnable.UseVisualStyleBackColor = true;
            this.chkArcEnable.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkSingleLine
            // 
            this.chkSingleLine.AutoSize = true;
            this.chkSingleLine.Location = new System.Drawing.Point(48, 3);
            this.chkSingleLine.Name = "chkSingleLine";
            this.chkSingleLine.Size = new System.Drawing.Size(39, 18);
            this.chkSingleLine.TabIndex = 3;
            this.chkSingleLine.Text = "线";
            this.chkSingleLine.UseVisualStyleBackColor = true;
            this.chkSingleLine.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // chkDot
            // 
            this.chkDot.AutoSize = true;
            this.chkDot.Location = new System.Drawing.Point(3, 3);
            this.chkDot.Name = "chkDot";
            this.chkDot.Size = new System.Drawing.Size(39, 18);
            this.chkDot.TabIndex = 2;
            this.chkDot.Text = "点";
            this.chkDot.UseVisualStyleBackColor = true;
            this.chkDot.CheckedChanged += new System.EventHandler(this.Chk_ValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CmdLineNo,
            this.cmdLineTypeName,
            this.PointType,
            this.Skip,
            this.XValue,
            this.YValue});
            this.dataGridView1.Location = new System.Drawing.Point(6, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(502, 182);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // CmdLineNo
            // 
            this.CmdLineNo.HeaderText = "编号";
            this.CmdLineNo.Name = "CmdLineNo";
            this.CmdLineNo.ReadOnly = true;
            this.CmdLineNo.Width = 40;
            // 
            // cmdLineTypeName
            // 
            this.cmdLineTypeName.HeaderText = "轨迹名称";
            this.cmdLineTypeName.Name = "cmdLineTypeName";
            this.cmdLineTypeName.ReadOnly = true;
            // 
            // PointType
            // 
            this.PointType.HeaderText = "点名称";
            this.PointType.Name = "PointType";
            this.PointType.ReadOnly = true;
            this.PointType.Width = 80;
            // 
            // Skip
            // 
            this.Skip.HeaderText = "跳过";
            this.Skip.Name = "Skip";
            this.Skip.ReadOnly = true;
            this.Skip.Width = 40;
            // 
            // XValue
            // 
            this.XValue.HeaderText = "X(mm)";
            this.XValue.Name = "XValue";
            this.XValue.ReadOnly = true;
            // 
            // YValue
            // 
            this.YValue.HeaderText = "Y(mm)";
            this.YValue.Name = "YValue";
            this.YValue.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CmdLineFineTuneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 631);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CmdLineFineTuneForm";
            this.Text = "轨迹微调功能窗口";
            this.Load += new System.EventHandler(this.CmdLineFineTuneForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Vision.CameraControl cameraControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDownRigth;
        private System.Windows.Forms.Button btnUpRight;
        private System.Windows.Forms.Button btnUpLeft;
        private System.Windows.Forms.Button btnDownLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnAutoMove;
        private System.Windows.Forms.Button btnMoveToNext;
        private System.Windows.Forms.Button btnMoveToPre;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPointY;
        private System.Windows.Forms.TextBox txtPointX;
        private System.Windows.Forms.TextBox txtCurrY;
        private System.Windows.Forms.TextBox txtCurrX;
        private System.Windows.Forms.Label label7;
        private Motion.JogComboBox cmbKeyDis;
        private System.Windows.Forms.Label label6;
        private Motion.JogComboBox cmbRightClickDis;
        private System.Windows.Forms.Label label5;
        private Motion.JogComboBox cmbLeftClickDis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectedAll;
        private System.Windows.Forms.CheckBox chkDoMultiPassPattern;
        private System.Windows.Forms.CheckBox chkStepAndRepeat;
        private System.Windows.Forms.CheckBox chkDoPattern;
        private System.Windows.Forms.CheckBox chkSymbolLines;
        private System.Windows.Forms.CheckBox chkSnakeLines;
        private System.Windows.Forms.CheckBox chkMultiLines;
        private System.Windows.Forms.CheckBox chkPolyLine;
        private System.Windows.Forms.CheckBox chkArcEnable;
        private System.Windows.Forms.CheckBox chkSingleLine;
        private System.Windows.Forms.CheckBox chkDot;
        private System.Windows.Forms.Button btnMoveTo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CmdLineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdLineTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PointType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Skip;
        private System.Windows.Forms.DataGridViewTextBoxColumn XValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn YValue;
    }
}