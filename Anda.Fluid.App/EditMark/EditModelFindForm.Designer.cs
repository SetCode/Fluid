namespace Anda.Fluid.App.EditMark
{
    partial class EditModelFindForm
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
            this.tab = new System.Windows.Forms.TabControl();
            this.tbgModel = new System.Windows.Forms.TabPage();
            this.CameraSetting = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mSec = new System.Windows.Forms.Label();
            this.nudSettlingTime = new System.Windows.Forms.NumericUpDown();
            this.Threshold = new System.Windows.Forms.GroupBox();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.AcceptThreshold = new System.Windows.Forms.Label();
            this.nudAcceptThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.ModelSize = new System.Windows.Forms.GroupBox();
            this.ModelHeight = new System.Windows.Forms.Label();
            this.ModelWidth = new System.Windows.Forms.Label();
            this.nudModelSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.nudModelSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.SearchWindow = new System.Windows.Forms.GroupBox();
            this.DetectHeight = new System.Windows.Forms.Label();
            this.DetectWidth = new System.Windows.Forms.Label();
            this.nudSearchWindowWidth = new System.Windows.Forms.NumericUpDown();
            this.nudSearchWindowHeight = new System.Windows.Forms.NumericUpDown();
            this.tbgTest = new System.Windows.Forms.TabPage();
            this.btnGotoTaught = new System.Windows.Forms.Button();
            this.btnShowModel = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDiff = new System.Windows.Forms.TextBox();
            this.tbTaughtLoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFoundLoc = new System.Windows.Forms.TextBox();
            this.tbConfidence = new System.Windows.Forms.TextBox();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.lbFoundLoc = new System.Windows.Forms.Label();
            this.lbConfidence = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.gbSetup = new System.Windows.Forms.GroupBox();
            this.cbSettle = new System.Windows.Forms.CheckBox();
            this.rbFindInPlace = new System.Windows.Forms.RadioButton();
            this.rbFindAtTaught = new System.Windows.Forms.RadioButton();
            this.picModelImage = new System.Windows.Forms.PictureBox();
            this.ckbFrmFile = new System.Windows.Forms.CheckBox();
            this.gbxCam.SuspendLayout();
            this.gbxContent.SuspendLayout();
            this.tab.SuspendLayout();
            this.tbgModel.SuspendLayout();
            this.CameraSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).BeginInit();
            this.Threshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcceptThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            this.ModelSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModelSizeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudModelSizeHeight)).BeginInit();
            this.SearchWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchWindowWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchWindowHeight)).BeginInit();
            this.tbgTest.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.gbSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picModelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxCam
            // 
            this.gbxCam.Controls.Add(this.picModelImage);
            this.gbxCam.Size = new System.Drawing.Size(609, 443);
            this.gbxCam.Controls.SetChildIndex(this.cameraControl1, 0);
            this.gbxCam.Controls.SetChildIndex(this.picModelImage, 0);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Size = new System.Drawing.Size(598, 422);
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.tab);
            this.gbxContent.Location = new System.Drawing.Point(4, 483);
            this.gbxContent.Size = new System.Drawing.Size(609, 176);
            // 
            // gbxOption
            // 
            this.gbxOption.Location = new System.Drawing.Point(621, 37);
            this.gbxOption.Size = new System.Drawing.Size(242, 171);
            // 
            // gbxJog
            // 
            this.gbxJog.Location = new System.Drawing.Point(630, 483);
            this.gbxJog.Size = new System.Drawing.Size(233, 176);
            // 
            // positionVControl1
            // 
            this.positionVControl1.Location = new System.Drawing.Point(621, 396);
            this.positionVControl1.Size = new System.Drawing.Size(242, 84);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(621, 277);
            // 
            // tab
            // 
            this.tab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tab.Controls.Add(this.tbgModel);
            this.tab.Controls.Add(this.tbgTest);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(3, 18);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(603, 155);
            this.tab.TabIndex = 14;
            // 
            // tbgModel
            // 
            this.tbgModel.BackColor = System.Drawing.Color.Transparent;
            this.tbgModel.Controls.Add(this.ckbFrmFile);
            this.tbgModel.Controls.Add(this.CameraSetting);
            this.tbgModel.Controls.Add(this.Threshold);
            this.tbgModel.Controls.Add(this.ModelSize);
            this.tbgModel.Controls.Add(this.SearchWindow);
            this.tbgModel.Location = new System.Drawing.Point(23, 4);
            this.tbgModel.Name = "tbgModel";
            this.tbgModel.Padding = new System.Windows.Forms.Padding(3);
            this.tbgModel.Size = new System.Drawing.Size(576, 147);
            this.tbgModel.TabIndex = 0;
            this.tbgModel.Text = "Model";
            // 
            // CameraSetting
            // 
            this.CameraSetting.Controls.Add(this.label3);
            this.CameraSetting.Controls.Add(this.mSec);
            this.CameraSetting.Controls.Add(this.nudSettlingTime);
            this.CameraSetting.Location = new System.Drawing.Point(384, 7);
            this.CameraSetting.Name = "CameraSetting";
            this.CameraSetting.Size = new System.Drawing.Size(134, 100);
            this.CameraSetting.TabIndex = 10;
            this.CameraSetting.TabStop = false;
            this.CameraSetting.Text = "CameraSetting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Settling Time";
            // 
            // mSec
            // 
            this.mSec.AutoSize = true;
            this.mSec.Location = new System.Drawing.Point(71, 68);
            this.mSec.Name = "mSec";
            this.mSec.Size = new System.Drawing.Size(22, 12);
            this.mSec.TabIndex = 7;
            this.mSec.Text = "ms";
            // 
            // nudSettlingTime
            // 
            this.nudSettlingTime.Location = new System.Drawing.Point(15, 63);
            this.nudSettlingTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSettlingTime.Name = "nudSettlingTime";
            this.nudSettlingTime.Size = new System.Drawing.Size(50, 20);
            this.nudSettlingTime.TabIndex = 6;
            // 
            // Threshold
            // 
            this.Threshold.Controls.Add(this.lblTolerance);
            this.Threshold.Controls.Add(this.AcceptThreshold);
            this.Threshold.Controls.Add(this.nudAcceptThreshold);
            this.Threshold.Controls.Add(this.nudTolerance);
            this.Threshold.Location = new System.Drawing.Point(248, 7);
            this.Threshold.Name = "Threshold";
            this.Threshold.Size = new System.Drawing.Size(130, 100);
            this.Threshold.TabIndex = 9;
            this.Threshold.TabStop = false;
            this.Threshold.Text = "Threshold";
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(6, 66);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(60, 12);
            this.lblTolerance.TabIndex = 7;
            this.lblTolerance.Text = "Tolerance";
            // 
            // AcceptThreshold
            // 
            this.AcceptThreshold.AutoSize = true;
            this.AcceptThreshold.Location = new System.Drawing.Point(6, 33);
            this.AcceptThreshold.Name = "AcceptThreshold";
            this.AcceptThreshold.Size = new System.Drawing.Size(45, 12);
            this.AcceptThreshold.TabIndex = 6;
            this.AcceptThreshold.Text = "Accept";
            // 
            // nudAcceptThreshold
            // 
            this.nudAcceptThreshold.DecimalPlaces = 1;
            this.nudAcceptThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudAcceptThreshold.Location = new System.Drawing.Point(71, 27);
            this.nudAcceptThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAcceptThreshold.Name = "nudAcceptThreshold";
            this.nudAcceptThreshold.Size = new System.Drawing.Size(50, 20);
            this.nudAcceptThreshold.TabIndex = 4;
            this.nudAcceptThreshold.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            // 
            // nudTolerance
            // 
            this.nudTolerance.DecimalPlaces = 1;
            this.nudTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTolerance.Location = new System.Drawing.Point(71, 64);
            this.nudTolerance.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(50, 20);
            this.nudTolerance.TabIndex = 5;
            // 
            // ModelSize
            // 
            this.ModelSize.Controls.Add(this.ModelHeight);
            this.ModelSize.Controls.Add(this.ModelWidth);
            this.ModelSize.Controls.Add(this.nudModelSizeWidth);
            this.ModelSize.Controls.Add(this.nudModelSizeHeight);
            this.ModelSize.Location = new System.Drawing.Point(126, 7);
            this.ModelSize.Name = "ModelSize";
            this.ModelSize.Size = new System.Drawing.Size(116, 100);
            this.ModelSize.TabIndex = 8;
            this.ModelSize.TabStop = false;
            this.ModelSize.Text = "ModelSize";
            // 
            // ModelHeight
            // 
            this.ModelHeight.AutoSize = true;
            this.ModelHeight.Location = new System.Drawing.Point(9, 65);
            this.ModelHeight.Name = "ModelHeight";
            this.ModelHeight.Size = new System.Drawing.Size(42, 12);
            this.ModelHeight.TabIndex = 5;
            this.ModelHeight.Text = "Height";
            // 
            // ModelWidth
            // 
            this.ModelWidth.AutoSize = true;
            this.ModelWidth.Location = new System.Drawing.Point(9, 32);
            this.ModelWidth.Name = "ModelWidth";
            this.ModelWidth.Size = new System.Drawing.Size(39, 12);
            this.ModelWidth.TabIndex = 4;
            this.ModelWidth.Text = "Width";
            // 
            // nudModelSizeWidth
            // 
            this.nudModelSizeWidth.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudModelSizeWidth.Location = new System.Drawing.Point(57, 26);
            this.nudModelSizeWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudModelSizeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudModelSizeWidth.Name = "nudModelSizeWidth";
            this.nudModelSizeWidth.Size = new System.Drawing.Size(50, 20);
            this.nudModelSizeWidth.TabIndex = 2;
            this.nudModelSizeWidth.Value = new decimal(new int[] {
            144,
            0,
            0,
            0});
            // 
            // nudModelSizeHeight
            // 
            this.nudModelSizeHeight.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudModelSizeHeight.Location = new System.Drawing.Point(57, 63);
            this.nudModelSizeHeight.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudModelSizeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudModelSizeHeight.Name = "nudModelSizeHeight";
            this.nudModelSizeHeight.Size = new System.Drawing.Size(50, 20);
            this.nudModelSizeHeight.TabIndex = 3;
            this.nudModelSizeHeight.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // SearchWindow
            // 
            this.SearchWindow.Controls.Add(this.DetectHeight);
            this.SearchWindow.Controls.Add(this.DetectWidth);
            this.SearchWindow.Controls.Add(this.nudSearchWindowWidth);
            this.SearchWindow.Controls.Add(this.nudSearchWindowHeight);
            this.SearchWindow.Location = new System.Drawing.Point(6, 7);
            this.SearchWindow.Name = "SearchWindow";
            this.SearchWindow.Size = new System.Drawing.Size(114, 100);
            this.SearchWindow.TabIndex = 7;
            this.SearchWindow.TabStop = false;
            this.SearchWindow.Text = "SearchWindow";
            // 
            // DetectHeight
            // 
            this.DetectHeight.AutoSize = true;
            this.DetectHeight.Location = new System.Drawing.Point(6, 66);
            this.DetectHeight.Name = "DetectHeight";
            this.DetectHeight.Size = new System.Drawing.Size(42, 12);
            this.DetectHeight.TabIndex = 3;
            this.DetectHeight.Text = "Height";
            // 
            // DetectWidth
            // 
            this.DetectWidth.AutoSize = true;
            this.DetectWidth.Location = new System.Drawing.Point(6, 33);
            this.DetectWidth.Name = "DetectWidth";
            this.DetectWidth.Size = new System.Drawing.Size(39, 12);
            this.DetectWidth.TabIndex = 2;
            this.DetectWidth.Text = "Width";
            // 
            // nudSearchWindowWidth
            // 
            this.nudSearchWindowWidth.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudSearchWindowWidth.Location = new System.Drawing.Point(56, 27);
            this.nudSearchWindowWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSearchWindowWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSearchWindowWidth.Name = "nudSearchWindowWidth";
            this.nudSearchWindowWidth.Size = new System.Drawing.Size(50, 20);
            this.nudSearchWindowWidth.TabIndex = 0;
            this.nudSearchWindowWidth.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // nudSearchWindowHeight
            // 
            this.nudSearchWindowHeight.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudSearchWindowHeight.Location = new System.Drawing.Point(56, 64);
            this.nudSearchWindowHeight.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudSearchWindowHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSearchWindowHeight.Name = "nudSearchWindowHeight";
            this.nudSearchWindowHeight.Size = new System.Drawing.Size(50, 20);
            this.nudSearchWindowHeight.TabIndex = 1;
            this.nudSearchWindowHeight.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // tbgTest
            // 
            this.tbgTest.Controls.Add(this.btnGotoTaught);
            this.tbgTest.Controls.Add(this.btnShowModel);
            this.tbgTest.Controls.Add(this.btnTest);
            this.tbgTest.Controls.Add(this.gbResult);
            this.tbgTest.Controls.Add(this.gbSetup);
            this.tbgTest.Location = new System.Drawing.Point(23, 4);
            this.tbgTest.Name = "tbgTest";
            this.tbgTest.Size = new System.Drawing.Size(576, 147);
            this.tbgTest.TabIndex = 2;
            this.tbgTest.Text = "Test";
            this.tbgTest.UseVisualStyleBackColor = true;
            // 
            // btnGotoTaught
            // 
            this.btnGotoTaught.Location = new System.Drawing.Point(440, 78);
            this.btnGotoTaught.Name = "btnGotoTaught";
            this.btnGotoTaught.Size = new System.Drawing.Size(87, 28);
            this.btnGotoTaught.TabIndex = 13;
            this.btnGotoTaught.Text = "Go to taught";
            this.btnGotoTaught.UseVisualStyleBackColor = true;
            this.btnGotoTaught.Click += new System.EventHandler(this.btnGotoTaught_Click);
            // 
            // btnShowModel
            // 
            this.btnShowModel.Location = new System.Drawing.Point(440, 44);
            this.btnShowModel.Name = "btnShowModel";
            this.btnShowModel.Size = new System.Drawing.Size(87, 28);
            this.btnShowModel.TabIndex = 12;
            this.btnShowModel.Text = "ShowModel";
            this.btnShowModel.UseVisualStyleBackColor = true;
            this.btnShowModel.Click += new System.EventHandler(this.btnShowModel_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(440, 10);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(87, 28);
            this.btnTest.TabIndex = 11;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.label2);
            this.gbResult.Controls.Add(this.tbDiff);
            this.gbResult.Controls.Add(this.tbTaughtLoc);
            this.gbResult.Controls.Add(this.label1);
            this.gbResult.Controls.Add(this.tbFoundLoc);
            this.gbResult.Controls.Add(this.tbConfidence);
            this.gbResult.Controls.Add(this.tbTime);
            this.gbResult.Controls.Add(this.tbStatus);
            this.gbResult.Controls.Add(this.lbFoundLoc);
            this.gbResult.Controls.Add(this.lbConfidence);
            this.gbResult.Controls.Add(this.lbTime);
            this.gbResult.Controls.Add(this.lbStatus);
            this.gbResult.Location = new System.Drawing.Point(6, 50);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(419, 94);
            this.gbResult.TabIndex = 10;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Diff";
            // 
            // tbDiff
            // 
            this.tbDiff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDiff.Enabled = false;
            this.tbDiff.Location = new System.Drawing.Point(238, 66);
            this.tbDiff.Name = "tbDiff";
            this.tbDiff.Size = new System.Drawing.Size(169, 20);
            this.tbDiff.TabIndex = 10;
            this.tbDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTaughtLoc
            // 
            this.tbTaughtLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTaughtLoc.Enabled = false;
            this.tbTaughtLoc.Location = new System.Drawing.Point(238, 14);
            this.tbTaughtLoc.Name = "tbTaughtLoc";
            this.tbTaughtLoc.Size = new System.Drawing.Size(169, 20);
            this.tbTaughtLoc.TabIndex = 9;
            this.tbTaughtLoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Taught Loc";
            // 
            // tbFoundLoc
            // 
            this.tbFoundLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFoundLoc.Enabled = false;
            this.tbFoundLoc.Location = new System.Drawing.Point(238, 40);
            this.tbFoundLoc.Name = "tbFoundLoc";
            this.tbFoundLoc.Size = new System.Drawing.Size(169, 20);
            this.tbFoundLoc.TabIndex = 7;
            this.tbFoundLoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbConfidence
            // 
            this.tbConfidence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConfidence.Enabled = false;
            this.tbConfidence.Location = new System.Drawing.Point(73, 40);
            this.tbConfidence.Name = "tbConfidence";
            this.tbConfidence.Size = new System.Drawing.Size(70, 20);
            this.tbConfidence.TabIndex = 6;
            this.tbConfidence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTime
            // 
            this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTime.Enabled = false;
            this.tbTime.Location = new System.Drawing.Point(73, 66);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(70, 20);
            this.tbTime.TabIndex = 5;
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbStatus
            // 
            this.tbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStatus.Enabled = false;
            this.tbStatus.Location = new System.Drawing.Point(73, 14);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(70, 20);
            this.tbStatus.TabIndex = 4;
            this.tbStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbFoundLoc
            // 
            this.lbFoundLoc.AutoSize = true;
            this.lbFoundLoc.Location = new System.Drawing.Point(170, 40);
            this.lbFoundLoc.Name = "lbFoundLoc";
            this.lbFoundLoc.Size = new System.Drawing.Size(62, 12);
            this.lbFoundLoc.TabIndex = 3;
            this.lbFoundLoc.Text = "Found Loc";
            // 
            // lbConfidence
            // 
            this.lbConfidence.AutoSize = true;
            this.lbConfidence.Location = new System.Drawing.Point(6, 40);
            this.lbConfidence.Name = "lbConfidence";
            this.lbConfidence.Size = new System.Drawing.Size(68, 12);
            this.lbConfidence.TabIndex = 2;
            this.lbConfidence.Text = "Confidence";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(35, 66);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(32, 12);
            this.lbTime.TabIndex = 1;
            this.lbTime.Text = "Time";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(25, 16);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(42, 12);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "Status";
            // 
            // gbSetup
            // 
            this.gbSetup.Controls.Add(this.cbSettle);
            this.gbSetup.Controls.Add(this.rbFindInPlace);
            this.gbSetup.Controls.Add(this.rbFindAtTaught);
            this.gbSetup.Location = new System.Drawing.Point(6, 2);
            this.gbSetup.Name = "gbSetup";
            this.gbSetup.Size = new System.Drawing.Size(419, 46);
            this.gbSetup.TabIndex = 9;
            this.gbSetup.TabStop = false;
            this.gbSetup.Text = "Setup";
            // 
            // cbSettle
            // 
            this.cbSettle.Checked = true;
            this.cbSettle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSettle.Location = new System.Drawing.Point(329, 19);
            this.cbSettle.Name = "cbSettle";
            this.cbSettle.Size = new System.Drawing.Size(78, 16);
            this.cbSettle.TabIndex = 0;
            this.cbSettle.Text = "Settle";
            this.cbSettle.UseVisualStyleBackColor = true;
            // 
            // rbFindInPlace
            // 
            this.rbFindInPlace.Location = new System.Drawing.Point(131, 19);
            this.rbFindInPlace.Name = "rbFindInPlace";
            this.rbFindInPlace.Size = new System.Drawing.Size(105, 16);
            this.rbFindInPlace.TabIndex = 0;
            this.rbFindInPlace.Text = "Find in-place";
            this.rbFindInPlace.UseVisualStyleBackColor = true;
            // 
            // rbFindAtTaught
            // 
            this.rbFindAtTaught.Checked = true;
            this.rbFindAtTaught.Location = new System.Drawing.Point(6, 19);
            this.rbFindAtTaught.Name = "rbFindAtTaught";
            this.rbFindAtTaught.Size = new System.Drawing.Size(119, 16);
            this.rbFindAtTaught.TabIndex = 0;
            this.rbFindAtTaught.TabStop = true;
            this.rbFindAtTaught.Text = "Find at taught";
            this.rbFindAtTaught.UseVisualStyleBackColor = true;
            // 
            // picModelImage
            // 
            this.picModelImage.Location = new System.Drawing.Point(4, 47);
            this.picModelImage.Name = "picModelImage";
            this.picModelImage.Size = new System.Drawing.Size(133, 110);
            this.picModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picModelImage.TabIndex = 5;
            this.picModelImage.TabStop = false;
            // 
            // ckbFrmFile
            // 
            this.ckbFrmFile.AutoSize = true;
            this.ckbFrmFile.Location = new System.Drawing.Point(34, 122);
            this.ckbFrmFile.Name = "ckbFrmFile";
            this.ckbFrmFile.Size = new System.Drawing.Size(68, 16);
            this.ckbFrmFile.TabIndex = 11;
            this.ckbFrmFile.Text = "来自文件";
            this.ckbFrmFile.UseVisualStyleBackColor = true;
            // 
            // EditModelFindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 662);
            this.Name = "EditModelFindForm";
            this.Text = "DialogMarkModelFind";
            this.gbxCam.ResumeLayout(false);
            this.gbxContent.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.tbgModel.ResumeLayout(false);
            this.tbgModel.PerformLayout();
            this.CameraSetting.ResumeLayout(false);
            this.CameraSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettlingTime)).EndInit();
            this.Threshold.ResumeLayout(false);
            this.Threshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcceptThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            this.ModelSize.ResumeLayout(false);
            this.ModelSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModelSizeWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudModelSizeHeight)).EndInit();
            this.SearchWindow.ResumeLayout(false);
            this.SearchWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchWindowWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchWindowHeight)).EndInit();
            this.tbgTest.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.gbSetup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picModelImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tbgModel;
        private System.Windows.Forms.GroupBox CameraSetting;
        private System.Windows.Forms.Label mSec;
        private System.Windows.Forms.NumericUpDown nudSettlingTime;
        private System.Windows.Forms.GroupBox Threshold;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Label AcceptThreshold;
        public System.Windows.Forms.NumericUpDown nudAcceptThreshold;
        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.GroupBox ModelSize;
        private System.Windows.Forms.Label ModelHeight;
        private System.Windows.Forms.Label ModelWidth;
        public System.Windows.Forms.NumericUpDown nudModelSizeWidth;
        public System.Windows.Forms.NumericUpDown nudModelSizeHeight;
        private System.Windows.Forms.GroupBox SearchWindow;
        private System.Windows.Forms.Label DetectHeight;
        private System.Windows.Forms.Label DetectWidth;
        public System.Windows.Forms.NumericUpDown nudSearchWindowWidth;
        public System.Windows.Forms.NumericUpDown nudSearchWindowHeight;
        private System.Windows.Forms.TabPage tbgTest;
        private System.Windows.Forms.GroupBox gbSetup;
        private System.Windows.Forms.CheckBox cbSettle;
        private System.Windows.Forms.RadioButton rbFindInPlace;
        private System.Windows.Forms.RadioButton rbFindAtTaught;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox tbTaughtLoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFoundLoc;
        private System.Windows.Forms.TextBox tbConfidence;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Label lbFoundLoc;
        private System.Windows.Forms.Label lbConfidence;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnGotoTaught;
        private System.Windows.Forms.Button btnShowModel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox picModelImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDiff;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbFrmFile;
    }
}