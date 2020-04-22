namespace Anda.Fluid.Domain.Conveyor.Forms
{
    partial class ConveyorSettingControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabConveyorSetting = new System.Windows.Forms.TabControl();
            this.tabBaseSetting = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoExitStandaloneMode = new System.Windows.Forms.RadioButton();
            this.rdoExitSMEMAMode = new System.Windows.Forms.RadioButton();
            this.grpConveyorSpeed = new System.Windows.Forms.GroupBox();
            this.nudConveyorSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblConveyorSpeed = new System.Windows.Forms.Label();
            this.grpSMEMAMode = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ckbWaitOutInExitSensor = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSingleInteraction = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoContinuousSignal = new System.Windows.Forms.RadioButton();
            this.lblPulse = new System.Windows.Forms.Label();
            this.rdoPulseSignal = new System.Windows.Forms.RadioButton();
            this.nudPulseTime = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoReceiveIsPulse = new System.Windows.Forms.RadioButton();
            this.rdoReceiveIsContinues = new System.Windows.Forms.RadioButton();
            this.nudDownStreamStuckTime = new System.Windows.Forms.NumericUpDown();
            this.DownStreamStuckTime = new System.Windows.Forms.Label();
            this.nudUpStreamStuckTime = new System.Windows.Forms.NumericUpDown();
            this.lblUpStreamStuckTime = new System.Windows.Forms.Label();
            this.grpStandaloneMode = new System.Windows.Forms.GroupBox();
            this.grpExitMode = new System.Windows.Forms.GroupBox();
            this.rdoManualTake = new System.Windows.Forms.RadioButton();
            this.rdoStraightExit = new System.Windows.Forms.RadioButton();
            this.grpAutoMode = new System.Windows.Forms.GroupBox();
            this.rdoEnterStandaloneMode = new System.Windows.Forms.RadioButton();
            this.rdoEnterSMEMAMode = new System.Windows.Forms.RadioButton();
            this.grpConveyorDirection = new System.Windows.Forms.GroupBox();
            this.rdoRight2Right = new System.Windows.Forms.RadioButton();
            this.rdoLeft2Left = new System.Windows.Forms.RadioButton();
            this.rdoRight2Left = new System.Windows.Forms.RadioButton();
            this.rdoLeft2Right = new System.Windows.Forms.RadioButton();
            this.grpSitesCount = new System.Windows.Forms.GroupBox();
            this.rdoDispenseAndInsulation = new System.Windows.Forms.RadioButton();
            this.rdoPreAndDispense = new System.Windows.Forms.RadioButton();
            this.rdoTripleSites = new System.Windows.Forms.RadioButton();
            this.rdoSingleSite = new System.Windows.Forms.RadioButton();
            this.tabSubsitesSetting = new System.Windows.Forms.TabPage();
            this.grpFinishedSite = new System.Windows.Forms.GroupBox();
            this.nudFinishedSiteHeatingTime = new System.Windows.Forms.NumericUpDown();
            this.nudFinishedSiteLongMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblFinishedSiteHeatingTime = new System.Windows.Forms.Label();
            this.lblFinishedSiteLongMoveDistance = new System.Windows.Forms.Label();
            this.nudFinishedSiteShortMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblFinishedSiteShortMoveDistance = new System.Windows.Forms.Label();
            this.grpWorkingSite = new System.Windows.Forms.GroupBox();
            this.nudWorkingHeatingTime = new System.Windows.Forms.NumericUpDown();
            this.lblWorkingHeatingTime = new System.Windows.Forms.Label();
            this.nudWorkingSiteExitDistance = new System.Windows.Forms.NumericUpDown();
            this.lblWorkingSiteExitDistance = new System.Windows.Forms.Label();
            this.nudWorkingSiteLongMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblWorkingSiteLongMoveSistance = new System.Windows.Forms.Label();
            this.nudWorkingSiteShortMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblWorkingSiteShortMoveDistance = new System.Windows.Forms.Label();
            this.grpUnifiedParam = new System.Windows.Forms.GroupBox();
            this.nudAccTime = new System.Windows.Forms.NumericUpDown();
            this.lblAccTime = new System.Windows.Forms.Label();
            this.nudBoardLeftDelay = new System.Windows.Forms.NumericUpDown();
            this.lblBoardLeftDelay = new System.Windows.Forms.Label();
            this.nudStuckCoefficent = new System.Windows.Forms.NumericUpDown();
            this.lblStuckCoefficent = new System.Windows.Forms.Label();
            this.nudLiftDelay = new System.Windows.Forms.NumericUpDown();
            this.nudStopperDelay = new System.Windows.Forms.NumericUpDown();
            this.lblCheckTime = new System.Windows.Forms.Label();
            this.nudCheckTime = new System.Windows.Forms.NumericUpDown();
            this.lblLiftDelay = new System.Windows.Forms.Label();
            this.lblStopperDelay = new System.Windows.Forms.Label();
            this.grpPresite = new System.Windows.Forms.GroupBox();
            this.nudPreHeatingTime = new System.Windows.Forms.NumericUpDown();
            this.lblPreHeatingTIme = new System.Windows.Forms.Label();
            this.nudPreLongMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblLongMoveDistance = new System.Windows.Forms.Label();
            this.nudPreShortMoveDistance = new System.Windows.Forms.NumericUpDown();
            this.lblPreShortMoveDistance = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabRTVSetting = new System.Windows.Forms.TabPage();
            this.txtDownConveyorTime = new Anda.Fluid.Controls.IntTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUpConveyorTIme = new Anda.Fluid.Controls.IntTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIOTime = new Anda.Fluid.Controls.IntTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIOEnable = new System.Windows.Forms.CheckBox();

            this.tabOtherSettings = new System.Windows.Forms.TabPage();
            this.cbxConveyorScan = new System.Windows.Forms.CheckBox();

            this.tabConveyorSetting.SuspendLayout();
            this.tabBaseSetting.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpConveyorSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConveyorSpeed)).BeginInit();
            this.grpSMEMAMode.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulseTime)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownStreamStuckTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpStreamStuckTime)).BeginInit();
            this.grpStandaloneMode.SuspendLayout();
            this.grpExitMode.SuspendLayout();
            this.grpAutoMode.SuspendLayout();
            this.grpConveyorDirection.SuspendLayout();
            this.grpSitesCount.SuspendLayout();
            this.tabSubsitesSetting.SuspendLayout();
            this.grpFinishedSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteHeatingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteLongMoveDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteShortMoveDistance)).BeginInit();
            this.grpWorkingSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingHeatingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteExitDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteLongMoveDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteShortMoveDistance)).BeginInit();
            this.grpUnifiedParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoardLeftDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStuckCoefficent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiftDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopperDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCheckTime)).BeginInit();
            this.grpPresite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreHeatingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreLongMoveDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreShortMoveDistance)).BeginInit();
            this.tabRTVSetting.SuspendLayout();
            this.tabOtherSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(523, 533);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 37);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabConveyorSetting
            // 
            this.tabConveyorSetting.Controls.Add(this.tabBaseSetting);
            this.tabConveyorSetting.Controls.Add(this.tabSubsitesSetting);
            this.tabConveyorSetting.Controls.Add(this.tabRTVSetting);
            this.tabConveyorSetting.Controls.Add(this.tabOtherSettings);
            this.tabConveyorSetting.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabConveyorSetting.Location = new System.Drawing.Point(5, 4);
            this.tabConveyorSetting.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabConveyorSetting.Name = "tabConveyorSetting";
            this.tabConveyorSetting.SelectedIndex = 0;
            this.tabConveyorSetting.Size = new System.Drawing.Size(647, 521);
            this.tabConveyorSetting.TabIndex = 0;
            // 
            // tabBaseSetting
            // 
            this.tabBaseSetting.Controls.Add(this.groupBox4);
            this.tabBaseSetting.Controls.Add(this.grpConveyorSpeed);
            this.tabBaseSetting.Controls.Add(this.grpSMEMAMode);
            this.tabBaseSetting.Controls.Add(this.grpStandaloneMode);
            this.tabBaseSetting.Controls.Add(this.grpAutoMode);
            this.tabBaseSetting.Controls.Add(this.grpConveyorDirection);
            this.tabBaseSetting.Controls.Add(this.grpSitesCount);
            this.tabBaseSetting.Location = new System.Drawing.Point(4, 26);
            this.tabBaseSetting.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabBaseSetting.Name = "tabBaseSetting";
            this.tabBaseSetting.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabBaseSetting.Size = new System.Drawing.Size(639, 491);
            this.tabBaseSetting.TabIndex = 0;
            this.tabBaseSetting.Text = "基础设定";
            this.tabBaseSetting.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoExitStandaloneMode);
            this.groupBox4.Controls.Add(this.rdoExitSMEMAMode);
            this.groupBox4.Location = new System.Drawing.Point(10, 400);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Size = new System.Drawing.Size(272, 79);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "出板运行模式";
            // 
            // rdoExitStandaloneMode
            // 
            this.rdoExitStandaloneMode.AutoSize = true;
            this.rdoExitStandaloneMode.Location = new System.Drawing.Point(153, 38);
            this.rdoExitStandaloneMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoExitStandaloneMode.Name = "rdoExitStandaloneMode";
            this.rdoExitStandaloneMode.Size = new System.Drawing.Size(101, 21);
            this.rdoExitStandaloneMode.TabIndex = 1;
            this.rdoExitStandaloneMode.TabStop = true;
            this.rdoExitStandaloneMode.Text = "无通讯模式";
            this.rdoExitStandaloneMode.UseVisualStyleBackColor = true;
            // 
            // rdoExitSMEMAMode
            // 
            this.rdoExitSMEMAMode.AutoSize = true;
            this.rdoExitSMEMAMode.Location = new System.Drawing.Point(30, 38);
            this.rdoExitSMEMAMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoExitSMEMAMode.Name = "rdoExitSMEMAMode";
            this.rdoExitSMEMAMode.Size = new System.Drawing.Size(113, 21);
            this.rdoExitSMEMAMode.TabIndex = 0;
            this.rdoExitSMEMAMode.TabStop = true;
            this.rdoExitSMEMAMode.Text = "SMEMA模式";
            this.rdoExitSMEMAMode.UseVisualStyleBackColor = true;
            // 
            // grpConveyorSpeed
            // 
            this.grpConveyorSpeed.Controls.Add(this.nudConveyorSpeed);
            this.grpConveyorSpeed.Controls.Add(this.lblConveyorSpeed);
            this.grpConveyorSpeed.Location = new System.Drawing.Point(10, 242);
            this.grpConveyorSpeed.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpConveyorSpeed.Name = "grpConveyorSpeed";
            this.grpConveyorSpeed.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpConveyorSpeed.Size = new System.Drawing.Size(272, 74);
            this.grpConveyorSpeed.TabIndex = 5;
            this.grpConveyorSpeed.TabStop = false;
            this.grpConveyorSpeed.Text = "轨道速度设定";
            // 
            // nudConveyorSpeed
            // 
            this.nudConveyorSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudConveyorSpeed.Location = new System.Drawing.Point(30, 32);
            this.nudConveyorSpeed.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudConveyorSpeed.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudConveyorSpeed.Name = "nudConveyorSpeed";
            this.nudConveyorSpeed.Size = new System.Drawing.Size(108, 25);
            this.nudConveyorSpeed.TabIndex = 4;
            // 
            // lblConveyorSpeed
            // 
            this.lblConveyorSpeed.AutoSize = true;
            this.lblConveyorSpeed.Location = new System.Drawing.Point(148, 35);
            this.lblConveyorSpeed.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblConveyorSpeed.Name = "lblConveyorSpeed";
            this.lblConveyorSpeed.Size = new System.Drawing.Size(70, 17);
            this.lblConveyorSpeed.TabIndex = 0;
            this.lblConveyorSpeed.Text = "(mm/s)";
            // 
            // grpSMEMAMode
            // 
            this.grpSMEMAMode.Controls.Add(this.groupBox5);
            this.grpSMEMAMode.Controls.Add(this.groupBox3);
            this.grpSMEMAMode.Controls.Add(this.groupBox2);
            this.grpSMEMAMode.Controls.Add(this.groupBox1);
            this.grpSMEMAMode.Controls.Add(this.nudDownStreamStuckTime);
            this.grpSMEMAMode.Controls.Add(this.DownStreamStuckTime);
            this.grpSMEMAMode.Controls.Add(this.nudUpStreamStuckTime);
            this.grpSMEMAMode.Controls.Add(this.lblUpStreamStuckTime);
            this.grpSMEMAMode.Location = new System.Drawing.Point(292, 17);
            this.grpSMEMAMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpSMEMAMode.Name = "grpSMEMAMode";
            this.grpSMEMAMode.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpSMEMAMode.Size = new System.Drawing.Size(330, 353);
            this.grpSMEMAMode.TabIndex = 4;
            this.grpSMEMAMode.TabStop = false;
            this.grpSMEMAMode.Text = "SMEMA模式设定";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ckbWaitOutInExitSensor);
            this.groupBox5.Location = new System.Drawing.Point(127, 191);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Size = new System.Drawing.Size(189, 57);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "出板位置";
            // 
            // ckbWaitOutInExitSensor
            // 
            this.ckbWaitOutInExitSensor.AutoSize = true;
            this.ckbWaitOutInExitSensor.Location = new System.Drawing.Point(28, 25);
            this.ckbWaitOutInExitSensor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckbWaitOutInExitSensor.Name = "ckbWaitOutInExitSensor";
            this.ckbWaitOutInExitSensor.Size = new System.Drawing.Size(132, 21);
            this.ckbWaitOutInExitSensor.TabIndex = 0;
            this.ckbWaitOutInExitSensor.Text = "出板感应处等待";
            this.ckbWaitOutInExitSensor.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSingleInteraction);
            this.groupBox3.Location = new System.Drawing.Point(11, 191);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Size = new System.Drawing.Size(98, 57);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "交互方式";
            // 
            // chkSingleInteraction
            // 
            this.chkSingleInteraction.AutoSize = true;
            this.chkSingleInteraction.Location = new System.Drawing.Point(28, 25);
            this.chkSingleInteraction.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.chkSingleInteraction.Name = "chkSingleInteraction";
            this.chkSingleInteraction.Size = new System.Drawing.Size(57, 21);
            this.chkSingleInteraction.TabIndex = 0;
            this.chkSingleInteraction.Text = "单边";
            this.chkSingleInteraction.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoContinuousSignal);
            this.groupBox2.Controls.Add(this.lblPulse);
            this.groupBox2.Controls.Add(this.rdoPulseSignal);
            this.groupBox2.Controls.Add(this.nudPulseTime);
            this.groupBox2.Location = new System.Drawing.Point(13, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Size = new System.Drawing.Size(303, 95);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发出信号";
            // 
            // rdoContinuousSignal
            // 
            this.rdoContinuousSignal.AutoSize = true;
            this.rdoContinuousSignal.Location = new System.Drawing.Point(10, 28);
            this.rdoContinuousSignal.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoContinuousSignal.Name = "rdoContinuousSignal";
            this.rdoContinuousSignal.Size = new System.Drawing.Size(86, 21);
            this.rdoContinuousSignal.TabIndex = 0;
            this.rdoContinuousSignal.TabStop = true;
            this.rdoContinuousSignal.Text = "持续信号";
            this.rdoContinuousSignal.UseVisualStyleBackColor = true;
            // 
            // lblPulse
            // 
            this.lblPulse.AutoSize = true;
            this.lblPulse.Location = new System.Drawing.Point(10, 60);
            this.lblPulse.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPulse.Name = "lblPulse";
            this.lblPulse.Size = new System.Drawing.Size(142, 17);
            this.lblPulse.TabIndex = 2;
            this.lblPulse.Text = "脉冲持续时间(ms):";
            // 
            // rdoPulseSignal
            // 
            this.rdoPulseSignal.AutoSize = true;
            this.rdoPulseSignal.Location = new System.Drawing.Point(163, 28);
            this.rdoPulseSignal.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoPulseSignal.Name = "rdoPulseSignal";
            this.rdoPulseSignal.Size = new System.Drawing.Size(86, 21);
            this.rdoPulseSignal.TabIndex = 1;
            this.rdoPulseSignal.TabStop = true;
            this.rdoPulseSignal.Text = "脉冲信号";
            this.rdoPulseSignal.UseVisualStyleBackColor = true;
            // 
            // nudPulseTime
            // 
            this.nudPulseTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPulseTime.Location = new System.Drawing.Point(163, 57);
            this.nudPulseTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudPulseTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPulseTime.Name = "nudPulseTime";
            this.nudPulseTime.Size = new System.Drawing.Size(125, 25);
            this.nudPulseTime.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoReceiveIsPulse);
            this.groupBox1.Controls.Add(this.rdoReceiveIsContinues);
            this.groupBox1.Location = new System.Drawing.Point(13, 126);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(303, 57);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接受信号";
            // 
            // rdoReceiveIsPulse
            // 
            this.rdoReceiveIsPulse.AutoSize = true;
            this.rdoReceiveIsPulse.Location = new System.Drawing.Point(163, 21);
            this.rdoReceiveIsPulse.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoReceiveIsPulse.Name = "rdoReceiveIsPulse";
            this.rdoReceiveIsPulse.Size = new System.Drawing.Size(86, 21);
            this.rdoReceiveIsPulse.TabIndex = 10;
            this.rdoReceiveIsPulse.TabStop = true;
            this.rdoReceiveIsPulse.Text = "脉冲信号";
            this.rdoReceiveIsPulse.UseVisualStyleBackColor = true;
            // 
            // rdoReceiveIsContinues
            // 
            this.rdoReceiveIsContinues.AutoSize = true;
            this.rdoReceiveIsContinues.Location = new System.Drawing.Point(10, 23);
            this.rdoReceiveIsContinues.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoReceiveIsContinues.Name = "rdoReceiveIsContinues";
            this.rdoReceiveIsContinues.Size = new System.Drawing.Size(86, 21);
            this.rdoReceiveIsContinues.TabIndex = 9;
            this.rdoReceiveIsContinues.TabStop = true;
            this.rdoReceiveIsContinues.Text = "持续信号";
            this.rdoReceiveIsContinues.UseVisualStyleBackColor = true;
            // 
            // nudDownStreamStuckTime
            // 
            this.nudDownStreamStuckTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDownStreamStuckTime.Location = new System.Drawing.Point(231, 310);
            this.nudDownStreamStuckTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudDownStreamStuckTime.Maximum = new decimal(new int[] {
            30000000,
            0,
            0,
            0});
            this.nudDownStreamStuckTime.Name = "nudDownStreamStuckTime";
            this.nudDownStreamStuckTime.Size = new System.Drawing.Size(85, 25);
            this.nudDownStreamStuckTime.TabIndex = 7;
            // 
            // DownStreamStuckTime
            // 
            this.DownStreamStuckTime.AutoSize = true;
            this.DownStreamStuckTime.Location = new System.Drawing.Point(19, 312);
            this.DownStreamStuckTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DownStreamStuckTime.Name = "DownStreamStuckTime";
            this.DownStreamStuckTime.Size = new System.Drawing.Size(202, 17);
            this.DownStreamStuckTime.TabIndex = 6;
            this.DownStreamStuckTime.Text = "下游设备卡板判定时长(ms):";
            // 
            // nudUpStreamStuckTime
            // 
            this.nudUpStreamStuckTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudUpStreamStuckTime.Location = new System.Drawing.Point(231, 262);
            this.nudUpStreamStuckTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudUpStreamStuckTime.Maximum = new decimal(new int[] {
            30000000,
            0,
            0,
            0});
            this.nudUpStreamStuckTime.Name = "nudUpStreamStuckTime";
            this.nudUpStreamStuckTime.Size = new System.Drawing.Size(85, 25);
            this.nudUpStreamStuckTime.TabIndex = 5;
            // 
            // lblUpStreamStuckTime
            // 
            this.lblUpStreamStuckTime.AutoSize = true;
            this.lblUpStreamStuckTime.Location = new System.Drawing.Point(19, 265);
            this.lblUpStreamStuckTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUpStreamStuckTime.Name = "lblUpStreamStuckTime";
            this.lblUpStreamStuckTime.Size = new System.Drawing.Size(202, 17);
            this.lblUpStreamStuckTime.TabIndex = 4;
            this.lblUpStreamStuckTime.Text = "上游设备卡板判定时长(ms):";
            // 
            // grpStandaloneMode
            // 
            this.grpStandaloneMode.Controls.Add(this.grpExitMode);
            this.grpStandaloneMode.Location = new System.Drawing.Point(292, 378);
            this.grpStandaloneMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpStandaloneMode.Name = "grpStandaloneMode";
            this.grpStandaloneMode.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpStandaloneMode.Size = new System.Drawing.Size(330, 105);
            this.grpStandaloneMode.TabIndex = 3;
            this.grpStandaloneMode.TabStop = false;
            this.grpStandaloneMode.Text = "无通讯模式设定";
            // 
            // grpExitMode
            // 
            this.grpExitMode.Controls.Add(this.rdoManualTake);
            this.grpExitMode.Controls.Add(this.rdoStraightExit);
            this.grpExitMode.Location = new System.Drawing.Point(10, 26);
            this.grpExitMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpExitMode.Name = "grpExitMode";
            this.grpExitMode.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpExitMode.Size = new System.Drawing.Size(306, 62);
            this.grpExitMode.TabIndex = 1;
            this.grpExitMode.TabStop = false;
            this.grpExitMode.Text = "出板方式选择";
            // 
            // rdoManualTake
            // 
            this.rdoManualTake.AutoSize = true;
            this.rdoManualTake.Location = new System.Drawing.Point(117, 28);
            this.rdoManualTake.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoManualTake.Name = "rdoManualTake";
            this.rdoManualTake.Size = new System.Drawing.Size(86, 21);
            this.rdoManualTake.TabIndex = 1;
            this.rdoManualTake.TabStop = true;
            this.rdoManualTake.Text = "手动取板";
            this.rdoManualTake.UseVisualStyleBackColor = true;
            // 
            // rdoStraightExit
            // 
            this.rdoStraightExit.AutoSize = true;
            this.rdoStraightExit.Location = new System.Drawing.Point(21, 28);
            this.rdoStraightExit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoStraightExit.Name = "rdoStraightExit";
            this.rdoStraightExit.Size = new System.Drawing.Size(86, 21);
            this.rdoStraightExit.TabIndex = 0;
            this.rdoStraightExit.TabStop = true;
            this.rdoStraightExit.Text = "直接流出";
            this.rdoStraightExit.UseVisualStyleBackColor = true;
            // 
            // grpAutoMode
            // 
            this.grpAutoMode.Controls.Add(this.rdoEnterStandaloneMode);
            this.grpAutoMode.Controls.Add(this.rdoEnterSMEMAMode);
            this.grpAutoMode.Location = new System.Drawing.Point(10, 324);
            this.grpAutoMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpAutoMode.Name = "grpAutoMode";
            this.grpAutoMode.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpAutoMode.Size = new System.Drawing.Size(272, 68);
            this.grpAutoMode.TabIndex = 2;
            this.grpAutoMode.TabStop = false;
            this.grpAutoMode.Text = "进板运行模式";
            // 
            // rdoEnterStandaloneMode
            // 
            this.rdoEnterStandaloneMode.AutoSize = true;
            this.rdoEnterStandaloneMode.Location = new System.Drawing.Point(151, 28);
            this.rdoEnterStandaloneMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoEnterStandaloneMode.Name = "rdoEnterStandaloneMode";
            this.rdoEnterStandaloneMode.Size = new System.Drawing.Size(101, 21);
            this.rdoEnterStandaloneMode.TabIndex = 1;
            this.rdoEnterStandaloneMode.TabStop = true;
            this.rdoEnterStandaloneMode.Text = "无通讯模式";
            this.rdoEnterStandaloneMode.UseVisualStyleBackColor = true;
            this.rdoEnterStandaloneMode.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // rdoEnterSMEMAMode
            // 
            this.rdoEnterSMEMAMode.AutoSize = true;
            this.rdoEnterSMEMAMode.Location = new System.Drawing.Point(30, 28);
            this.rdoEnterSMEMAMode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoEnterSMEMAMode.Name = "rdoEnterSMEMAMode";
            this.rdoEnterSMEMAMode.Size = new System.Drawing.Size(113, 21);
            this.rdoEnterSMEMAMode.TabIndex = 0;
            this.rdoEnterSMEMAMode.TabStop = true;
            this.rdoEnterSMEMAMode.Text = "SMEMA模式";
            this.rdoEnterSMEMAMode.UseVisualStyleBackColor = true;
            this.rdoEnterSMEMAMode.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // grpConveyorDirection
            // 
            this.grpConveyorDirection.Controls.Add(this.rdoRight2Right);
            this.grpConveyorDirection.Controls.Add(this.rdoLeft2Left);
            this.grpConveyorDirection.Controls.Add(this.rdoRight2Left);
            this.grpConveyorDirection.Controls.Add(this.rdoLeft2Right);
            this.grpConveyorDirection.Location = new System.Drawing.Point(10, 131);
            this.grpConveyorDirection.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpConveyorDirection.Name = "grpConveyorDirection";
            this.grpConveyorDirection.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpConveyorDirection.Size = new System.Drawing.Size(272, 93);
            this.grpConveyorDirection.TabIndex = 1;
            this.grpConveyorDirection.TabStop = false;
            this.grpConveyorDirection.Text = "轨道进出方向";
            // 
            // rdoRight2Right
            // 
            this.rdoRight2Right.AutoSize = true;
            this.rdoRight2Right.Location = new System.Drawing.Point(135, 63);
            this.rdoRight2Right.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoRight2Right.Name = "rdoRight2Right";
            this.rdoRight2Right.Size = new System.Drawing.Size(86, 21);
            this.rdoRight2Right.TabIndex = 3;
            this.rdoRight2Right.TabStop = true;
            this.rdoRight2Right.Text = "右进右出";
            this.rdoRight2Right.UseVisualStyleBackColor = true;
            // 
            // rdoLeft2Left
            // 
            this.rdoLeft2Left.AutoSize = true;
            this.rdoLeft2Left.Location = new System.Drawing.Point(30, 63);
            this.rdoLeft2Left.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoLeft2Left.Name = "rdoLeft2Left";
            this.rdoLeft2Left.Size = new System.Drawing.Size(86, 21);
            this.rdoLeft2Left.TabIndex = 2;
            this.rdoLeft2Left.TabStop = true;
            this.rdoLeft2Left.Text = "左进左出";
            this.rdoLeft2Left.UseVisualStyleBackColor = true;
            // 
            // rdoRight2Left
            // 
            this.rdoRight2Left.AutoSize = true;
            this.rdoRight2Left.Location = new System.Drawing.Point(135, 25);
            this.rdoRight2Left.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoRight2Left.Name = "rdoRight2Left";
            this.rdoRight2Left.Size = new System.Drawing.Size(86, 21);
            this.rdoRight2Left.TabIndex = 1;
            this.rdoRight2Left.TabStop = true;
            this.rdoRight2Left.Text = "右进左出";
            this.rdoRight2Left.UseVisualStyleBackColor = true;
            // 
            // rdoLeft2Right
            // 
            this.rdoLeft2Right.AutoSize = true;
            this.rdoLeft2Right.Location = new System.Drawing.Point(30, 25);
            this.rdoLeft2Right.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoLeft2Right.Name = "rdoLeft2Right";
            this.rdoLeft2Right.Size = new System.Drawing.Size(86, 21);
            this.rdoLeft2Right.TabIndex = 0;
            this.rdoLeft2Right.TabStop = true;
            this.rdoLeft2Right.Text = "左进右出";
            this.rdoLeft2Right.UseVisualStyleBackColor = true;
            // 
            // grpSitesCount
            // 
            this.grpSitesCount.Controls.Add(this.rdoDispenseAndInsulation);
            this.grpSitesCount.Controls.Add(this.rdoPreAndDispense);
            this.grpSitesCount.Controls.Add(this.rdoTripleSites);
            this.grpSitesCount.Controls.Add(this.rdoSingleSite);
            this.grpSitesCount.Location = new System.Drawing.Point(10, 17);
            this.grpSitesCount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpSitesCount.Name = "grpSitesCount";
            this.grpSitesCount.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpSitesCount.Size = new System.Drawing.Size(272, 106);
            this.grpSitesCount.TabIndex = 0;
            this.grpSitesCount.TabStop = false;
            this.grpSitesCount.Text = "轨道子站数量选择";
            // 
            // rdoDispenseAndInsulation
            // 
            this.rdoDispenseAndInsulation.AutoSize = true;
            this.rdoDispenseAndInsulation.Location = new System.Drawing.Point(135, 68);
            this.rdoDispenseAndInsulation.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoDispenseAndInsulation.Name = "rdoDispenseAndInsulation";
            this.rdoDispenseAndInsulation.Size = new System.Drawing.Size(98, 21);
            this.rdoDispenseAndInsulation.TabIndex = 3;
            this.rdoDispenseAndInsulation.TabStop = true;
            this.rdoDispenseAndInsulation.Text = "点胶+保温";
            this.rdoDispenseAndInsulation.UseVisualStyleBackColor = true;
            // 
            // rdoPreAndDispense
            // 
            this.rdoPreAndDispense.AutoSize = true;
            this.rdoPreAndDispense.Location = new System.Drawing.Point(30, 68);
            this.rdoPreAndDispense.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoPreAndDispense.Name = "rdoPreAndDispense";
            this.rdoPreAndDispense.Size = new System.Drawing.Size(98, 21);
            this.rdoPreAndDispense.TabIndex = 2;
            this.rdoPreAndDispense.TabStop = true;
            this.rdoPreAndDispense.Text = "预热+点胶";
            this.rdoPreAndDispense.UseVisualStyleBackColor = true;
            // 
            // rdoTripleSites
            // 
            this.rdoTripleSites.AutoSize = true;
            this.rdoTripleSites.Location = new System.Drawing.Point(135, 33);
            this.rdoTripleSites.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoTripleSites.Name = "rdoTripleSites";
            this.rdoTripleSites.Size = new System.Drawing.Size(56, 21);
            this.rdoTripleSites.TabIndex = 1;
            this.rdoTripleSites.TabStop = true;
            this.rdoTripleSites.Text = "三站";
            this.rdoTripleSites.UseVisualStyleBackColor = true;
            this.rdoTripleSites.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // rdoSingleSite
            // 
            this.rdoSingleSite.AutoSize = true;
            this.rdoSingleSite.Location = new System.Drawing.Point(30, 33);
            this.rdoSingleSite.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoSingleSite.Name = "rdoSingleSite";
            this.rdoSingleSite.Size = new System.Drawing.Size(56, 21);
            this.rdoSingleSite.TabIndex = 0;
            this.rdoSingleSite.TabStop = true;
            this.rdoSingleSite.Text = "单站";
            this.rdoSingleSite.UseVisualStyleBackColor = true;
            this.rdoSingleSite.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // tabSubsitesSetting
            // 
            this.tabSubsitesSetting.Controls.Add(this.grpFinishedSite);
            this.tabSubsitesSetting.Controls.Add(this.grpWorkingSite);
            this.tabSubsitesSetting.Controls.Add(this.grpUnifiedParam);
            this.tabSubsitesSetting.Controls.Add(this.grpPresite);
            this.tabSubsitesSetting.Location = new System.Drawing.Point(4, 26);
            this.tabSubsitesSetting.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabSubsitesSetting.Name = "tabSubsitesSetting";
            this.tabSubsitesSetting.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabSubsitesSetting.Size = new System.Drawing.Size(639, 491);
            this.tabSubsitesSetting.TabIndex = 1;
            this.tabSubsitesSetting.Text = "运行参数设定";
            this.tabSubsitesSetting.UseVisualStyleBackColor = true;
            // 
            // grpFinishedSite
            // 
            this.grpFinishedSite.Controls.Add(this.nudFinishedSiteHeatingTime);
            this.grpFinishedSite.Controls.Add(this.nudFinishedSiteLongMoveDistance);
            this.grpFinishedSite.Controls.Add(this.lblFinishedSiteHeatingTime);
            this.grpFinishedSite.Controls.Add(this.lblFinishedSiteLongMoveDistance);
            this.grpFinishedSite.Controls.Add(this.nudFinishedSiteShortMoveDistance);
            this.grpFinishedSite.Controls.Add(this.lblFinishedSiteShortMoveDistance);
            this.grpFinishedSite.Location = new System.Drawing.Point(10, 385);
            this.grpFinishedSite.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpFinishedSite.Name = "grpFinishedSite";
            this.grpFinishedSite.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpFinishedSite.Size = new System.Drawing.Size(614, 103);
            this.grpFinishedSite.TabIndex = 3;
            this.grpFinishedSite.TabStop = false;
            this.grpFinishedSite.Text = "保温站参数";
            // 
            // nudFinishedSiteHeatingTime
            // 
            this.nudFinishedSiteHeatingTime.Location = new System.Drawing.Point(177, 64);
            this.nudFinishedSiteHeatingTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudFinishedSiteHeatingTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudFinishedSiteHeatingTime.Name = "nudFinishedSiteHeatingTime";
            this.nudFinishedSiteHeatingTime.Size = new System.Drawing.Size(100, 25);
            this.nudFinishedSiteHeatingTime.TabIndex = 9;
            // 
            // nudFinishedSiteLongMoveDistance
            // 
            this.nudFinishedSiteLongMoveDistance.Location = new System.Drawing.Point(177, 23);
            this.nudFinishedSiteLongMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudFinishedSiteLongMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudFinishedSiteLongMoveDistance.Name = "nudFinishedSiteLongMoveDistance";
            this.nudFinishedSiteLongMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudFinishedSiteLongMoveDistance.TabIndex = 5;
            // 
            // lblFinishedSiteHeatingTime
            // 
            this.lblFinishedSiteHeatingTime.AutoSize = true;
            this.lblFinishedSiteHeatingTime.Location = new System.Drawing.Point(10, 67);
            this.lblFinishedSiteHeatingTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFinishedSiteHeatingTime.Name = "lblFinishedSiteHeatingTime";
            this.lblFinishedSiteHeatingTime.Size = new System.Drawing.Size(112, 17);
            this.lblFinishedSiteHeatingTime.TabIndex = 8;
            this.lblFinishedSiteHeatingTime.Text = "加热时长(ms):";
            // 
            // lblFinishedSiteLongMoveDistance
            // 
            this.lblFinishedSiteLongMoveDistance.AutoSize = true;
            this.lblFinishedSiteLongMoveDistance.Location = new System.Drawing.Point(10, 25);
            this.lblFinishedSiteLongMoveDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFinishedSiteLongMoveDistance.Name = "lblFinishedSiteLongMoveDistance";
            this.lblFinishedSiteLongMoveDistance.Size = new System.Drawing.Size(163, 17);
            this.lblFinishedSiteLongMoveDistance.TabIndex = 4;
            this.lblFinishedSiteLongMoveDistance.Text = "入口到定位距离(mm):";
            // 
            // nudFinishedSiteShortMoveDistance
            // 
            this.nudFinishedSiteShortMoveDistance.Location = new System.Drawing.Point(500, 29);
            this.nudFinishedSiteShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudFinishedSiteShortMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudFinishedSiteShortMoveDistance.Name = "nudFinishedSiteShortMoveDistance";
            this.nudFinishedSiteShortMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudFinishedSiteShortMoveDistance.TabIndex = 3;
            // 
            // lblFinishedSiteShortMoveDistance
            // 
            this.lblFinishedSiteShortMoveDistance.AutoSize = true;
            this.lblFinishedSiteShortMoveDistance.Location = new System.Drawing.Point(317, 31);
            this.lblFinishedSiteShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFinishedSiteShortMoveDistance.Name = "lblFinishedSiteShortMoveDistance";
            this.lblFinishedSiteShortMoveDistance.Size = new System.Drawing.Size(163, 17);
            this.lblFinishedSiteShortMoveDistance.TabIndex = 2;
            this.lblFinishedSiteShortMoveDistance.Text = "定位到阻挡距离(mm):";
            // 
            // grpWorkingSite
            // 
            this.grpWorkingSite.Controls.Add(this.nudWorkingHeatingTime);
            this.grpWorkingSite.Controls.Add(this.lblWorkingHeatingTime);
            this.grpWorkingSite.Controls.Add(this.nudWorkingSiteExitDistance);
            this.grpWorkingSite.Controls.Add(this.lblWorkingSiteExitDistance);
            this.grpWorkingSite.Controls.Add(this.nudWorkingSiteLongMoveDistance);
            this.grpWorkingSite.Controls.Add(this.lblWorkingSiteLongMoveSistance);
            this.grpWorkingSite.Controls.Add(this.nudWorkingSiteShortMoveDistance);
            this.grpWorkingSite.Controls.Add(this.lblWorkingSiteShortMoveDistance);
            this.grpWorkingSite.Location = new System.Drawing.Point(10, 273);
            this.grpWorkingSite.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpWorkingSite.Name = "grpWorkingSite";
            this.grpWorkingSite.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpWorkingSite.Size = new System.Drawing.Size(614, 103);
            this.grpWorkingSite.TabIndex = 2;
            this.grpWorkingSite.TabStop = false;
            this.grpWorkingSite.Text = "点胶站参数";
            // 
            // nudWorkingHeatingTime
            // 
            this.nudWorkingHeatingTime.Location = new System.Drawing.Point(500, 64);
            this.nudWorkingHeatingTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudWorkingHeatingTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudWorkingHeatingTime.Name = "nudWorkingHeatingTime";
            this.nudWorkingHeatingTime.Size = new System.Drawing.Size(100, 25);
            this.nudWorkingHeatingTime.TabIndex = 9;
            // 
            // lblWorkingHeatingTime
            // 
            this.lblWorkingHeatingTime.AutoSize = true;
            this.lblWorkingHeatingTime.Location = new System.Drawing.Point(317, 68);
            this.lblWorkingHeatingTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblWorkingHeatingTime.Name = "lblWorkingHeatingTime";
            this.lblWorkingHeatingTime.Size = new System.Drawing.Size(112, 17);
            this.lblWorkingHeatingTime.TabIndex = 8;
            this.lblWorkingHeatingTime.Text = "加热时长(ms):";
            // 
            // nudWorkingSiteExitDistance
            // 
            this.nudWorkingSiteExitDistance.Location = new System.Drawing.Point(177, 66);
            this.nudWorkingSiteExitDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudWorkingSiteExitDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudWorkingSiteExitDistance.Name = "nudWorkingSiteExitDistance";
            this.nudWorkingSiteExitDistance.Size = new System.Drawing.Size(100, 25);
            this.nudWorkingSiteExitDistance.TabIndex = 7;
            // 
            // lblWorkingSiteExitDistance
            // 
            this.lblWorkingSiteExitDistance.AutoSize = true;
            this.lblWorkingSiteExitDistance.Location = new System.Drawing.Point(10, 68);
            this.lblWorkingSiteExitDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblWorkingSiteExitDistance.Name = "lblWorkingSiteExitDistance";
            this.lblWorkingSiteExitDistance.Size = new System.Drawing.Size(163, 17);
            this.lblWorkingSiteExitDistance.TabIndex = 6;
            this.lblWorkingSiteExitDistance.Text = "阻挡到出口距离(mm):";
            // 
            // nudWorkingSiteLongMoveDistance
            // 
            this.nudWorkingSiteLongMoveDistance.Location = new System.Drawing.Point(500, 26);
            this.nudWorkingSiteLongMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudWorkingSiteLongMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudWorkingSiteLongMoveDistance.Name = "nudWorkingSiteLongMoveDistance";
            this.nudWorkingSiteLongMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudWorkingSiteLongMoveDistance.TabIndex = 5;
            // 
            // lblWorkingSiteLongMoveSistance
            // 
            this.lblWorkingSiteLongMoveSistance.AutoSize = true;
            this.lblWorkingSiteLongMoveSistance.Location = new System.Drawing.Point(317, 30);
            this.lblWorkingSiteLongMoveSistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblWorkingSiteLongMoveSistance.Name = "lblWorkingSiteLongMoveSistance";
            this.lblWorkingSiteLongMoveSistance.Size = new System.Drawing.Size(163, 17);
            this.lblWorkingSiteLongMoveSistance.TabIndex = 4;
            this.lblWorkingSiteLongMoveSistance.Text = "入口到定位距离(mm):";
            // 
            // nudWorkingSiteShortMoveDistance
            // 
            this.nudWorkingSiteShortMoveDistance.Location = new System.Drawing.Point(177, 28);
            this.nudWorkingSiteShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudWorkingSiteShortMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudWorkingSiteShortMoveDistance.Name = "nudWorkingSiteShortMoveDistance";
            this.nudWorkingSiteShortMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudWorkingSiteShortMoveDistance.TabIndex = 3;
            // 
            // lblWorkingSiteShortMoveDistance
            // 
            this.lblWorkingSiteShortMoveDistance.AutoSize = true;
            this.lblWorkingSiteShortMoveDistance.Location = new System.Drawing.Point(10, 30);
            this.lblWorkingSiteShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblWorkingSiteShortMoveDistance.Name = "lblWorkingSiteShortMoveDistance";
            this.lblWorkingSiteShortMoveDistance.Size = new System.Drawing.Size(163, 17);
            this.lblWorkingSiteShortMoveDistance.TabIndex = 2;
            this.lblWorkingSiteShortMoveDistance.Text = "定位到阻挡距离(mm):";
            // 
            // grpUnifiedParam
            // 
            this.grpUnifiedParam.Controls.Add(this.nudAccTime);
            this.grpUnifiedParam.Controls.Add(this.lblAccTime);
            this.grpUnifiedParam.Controls.Add(this.nudBoardLeftDelay);
            this.grpUnifiedParam.Controls.Add(this.lblBoardLeftDelay);
            this.grpUnifiedParam.Controls.Add(this.nudStuckCoefficent);
            this.grpUnifiedParam.Controls.Add(this.lblStuckCoefficent);
            this.grpUnifiedParam.Controls.Add(this.nudLiftDelay);
            this.grpUnifiedParam.Controls.Add(this.nudStopperDelay);
            this.grpUnifiedParam.Controls.Add(this.lblCheckTime);
            this.grpUnifiedParam.Controls.Add(this.nudCheckTime);
            this.grpUnifiedParam.Controls.Add(this.lblLiftDelay);
            this.grpUnifiedParam.Controls.Add(this.lblStopperDelay);
            this.grpUnifiedParam.Location = new System.Drawing.Point(10, 4);
            this.grpUnifiedParam.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpUnifiedParam.Name = "grpUnifiedParam";
            this.grpUnifiedParam.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpUnifiedParam.Size = new System.Drawing.Size(614, 149);
            this.grpUnifiedParam.TabIndex = 1;
            this.grpUnifiedParam.TabStop = false;
            this.grpUnifiedParam.Text = "统一设定参数";
            // 
            // nudAccTime
            // 
            this.nudAccTime.DecimalPlaces = 3;
            this.nudAccTime.Location = new System.Drawing.Point(500, 109);
            this.nudAccTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudAccTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAccTime.Name = "nudAccTime";
            this.nudAccTime.Size = new System.Drawing.Size(100, 25);
            this.nudAccTime.TabIndex = 10;
            // 
            // lblAccTime
            // 
            this.lblAccTime.AutoSize = true;
            this.lblAccTime.Location = new System.Drawing.Point(317, 110);
            this.lblAccTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAccTime.Name = "lblAccTime";
            this.lblAccTime.Size = new System.Drawing.Size(113, 17);
            this.lblAccTime.TabIndex = 9;
            this.lblAccTime.Text = "加减速时间(s):";
            // 
            // nudBoardLeftDelay
            // 
            this.nudBoardLeftDelay.Location = new System.Drawing.Point(500, 69);
            this.nudBoardLeftDelay.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudBoardLeftDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudBoardLeftDelay.Name = "nudBoardLeftDelay";
            this.nudBoardLeftDelay.Size = new System.Drawing.Size(100, 25);
            this.nudBoardLeftDelay.TabIndex = 8;
            // 
            // lblBoardLeftDelay
            // 
            this.lblBoardLeftDelay.AutoSize = true;
            this.lblBoardLeftDelay.Location = new System.Drawing.Point(317, 71);
            this.lblBoardLeftDelay.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBoardLeftDelay.Name = "lblBoardLeftDelay";
            this.lblBoardLeftDelay.Size = new System.Drawing.Size(142, 17);
            this.lblBoardLeftDelay.TabIndex = 7;
            this.lblBoardLeftDelay.Text = "出板完成延时(ms):";
            // 
            // nudStuckCoefficent
            // 
            this.nudStuckCoefficent.Location = new System.Drawing.Point(177, 109);
            this.nudStuckCoefficent.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudStuckCoefficent.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudStuckCoefficent.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudStuckCoefficent.Name = "nudStuckCoefficent";
            this.nudStuckCoefficent.Size = new System.Drawing.Size(100, 25);
            this.nudStuckCoefficent.TabIndex = 6;
            this.nudStuckCoefficent.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblStuckCoefficent
            // 
            this.lblStuckCoefficent.AutoSize = true;
            this.lblStuckCoefficent.Location = new System.Drawing.Point(10, 110);
            this.lblStuckCoefficent.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStuckCoefficent.Name = "lblStuckCoefficent";
            this.lblStuckCoefficent.Size = new System.Drawing.Size(104, 17);
            this.lblStuckCoefficent.TabIndex = 5;
            this.lblStuckCoefficent.Text = "卡板距离系数:";
            // 
            // nudLiftDelay
            // 
            this.nudLiftDelay.Location = new System.Drawing.Point(177, 69);
            this.nudLiftDelay.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudLiftDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLiftDelay.Name = "nudLiftDelay";
            this.nudLiftDelay.Size = new System.Drawing.Size(100, 25);
            this.nudLiftDelay.TabIndex = 4;
            // 
            // nudStopperDelay
            // 
            this.nudStopperDelay.Location = new System.Drawing.Point(500, 29);
            this.nudStopperDelay.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudStopperDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStopperDelay.Name = "nudStopperDelay";
            this.nudStopperDelay.Size = new System.Drawing.Size(100, 25);
            this.nudStopperDelay.TabIndex = 4;
            // 
            // lblCheckTime
            // 
            this.lblCheckTime.AutoSize = true;
            this.lblCheckTime.Location = new System.Drawing.Point(10, 31);
            this.lblCheckTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCheckTime.Name = "lblCheckTime";
            this.lblCheckTime.Size = new System.Drawing.Size(157, 17);
            this.lblCheckTime.TabIndex = 0;
            this.lblCheckTime.Text = "残留板检测时长(ms):";
            // 
            // nudCheckTime
            // 
            this.nudCheckTime.Location = new System.Drawing.Point(177, 29);
            this.nudCheckTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudCheckTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCheckTime.Name = "nudCheckTime";
            this.nudCheckTime.Size = new System.Drawing.Size(100, 25);
            this.nudCheckTime.TabIndex = 1;
            // 
            // lblLiftDelay
            // 
            this.lblLiftDelay.AutoSize = true;
            this.lblLiftDelay.Location = new System.Drawing.Point(10, 71);
            this.lblLiftDelay.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblLiftDelay.Name = "lblLiftDelay";
            this.lblLiftDelay.Size = new System.Drawing.Size(142, 17);
            this.lblLiftDelay.TabIndex = 3;
            this.lblLiftDelay.Text = "压板到位延时(ms):";
            // 
            // lblStopperDelay
            // 
            this.lblStopperDelay.AutoSize = true;
            this.lblStopperDelay.Location = new System.Drawing.Point(317, 31);
            this.lblStopperDelay.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStopperDelay.Name = "lblStopperDelay";
            this.lblStopperDelay.Size = new System.Drawing.Size(142, 17);
            this.lblStopperDelay.TabIndex = 3;
            this.lblStopperDelay.Text = "阻挡到位延时(ms):";
            // 
            // grpPresite
            // 
            this.grpPresite.Controls.Add(this.nudPreHeatingTime);
            this.grpPresite.Controls.Add(this.lblPreHeatingTIme);
            this.grpPresite.Controls.Add(this.nudPreLongMoveDistance);
            this.grpPresite.Controls.Add(this.lblLongMoveDistance);
            this.grpPresite.Controls.Add(this.nudPreShortMoveDistance);
            this.grpPresite.Controls.Add(this.lblPreShortMoveDistance);
            this.grpPresite.Location = new System.Drawing.Point(10, 161);
            this.grpPresite.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpPresite.Name = "grpPresite";
            this.grpPresite.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpPresite.Size = new System.Drawing.Size(614, 103);
            this.grpPresite.TabIndex = 0;
            this.grpPresite.TabStop = false;
            this.grpPresite.Text = "预热站参数";
            // 
            // nudPreHeatingTime
            // 
            this.nudPreHeatingTime.Location = new System.Drawing.Point(177, 64);
            this.nudPreHeatingTime.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudPreHeatingTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPreHeatingTime.Name = "nudPreHeatingTime";
            this.nudPreHeatingTime.Size = new System.Drawing.Size(100, 25);
            this.nudPreHeatingTime.TabIndex = 7;
            // 
            // lblPreHeatingTIme
            // 
            this.lblPreHeatingTIme.AutoSize = true;
            this.lblPreHeatingTIme.Location = new System.Drawing.Point(10, 67);
            this.lblPreHeatingTIme.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPreHeatingTIme.Name = "lblPreHeatingTIme";
            this.lblPreHeatingTIme.Size = new System.Drawing.Size(112, 17);
            this.lblPreHeatingTIme.TabIndex = 6;
            this.lblPreHeatingTIme.Text = "加热时长(ms):";
            // 
            // nudPreLongMoveDistance
            // 
            this.nudPreLongMoveDistance.Location = new System.Drawing.Point(177, 26);
            this.nudPreLongMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudPreLongMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPreLongMoveDistance.Name = "nudPreLongMoveDistance";
            this.nudPreLongMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudPreLongMoveDistance.TabIndex = 5;
            // 
            // lblLongMoveDistance
            // 
            this.lblLongMoveDistance.AutoSize = true;
            this.lblLongMoveDistance.Location = new System.Drawing.Point(10, 28);
            this.lblLongMoveDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblLongMoveDistance.Name = "lblLongMoveDistance";
            this.lblLongMoveDistance.Size = new System.Drawing.Size(163, 17);
            this.lblLongMoveDistance.TabIndex = 4;
            this.lblLongMoveDistance.Text = "入口到定位距离(mm):";
            // 
            // nudPreShortMoveDistance
            // 
            this.nudPreShortMoveDistance.Location = new System.Drawing.Point(500, 26);
            this.nudPreShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudPreShortMoveDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPreShortMoveDistance.Name = "nudPreShortMoveDistance";
            this.nudPreShortMoveDistance.Size = new System.Drawing.Size(100, 25);
            this.nudPreShortMoveDistance.TabIndex = 3;
            // 
            // lblPreShortMoveDistance
            // 
            this.lblPreShortMoveDistance.AutoSize = true;
            this.lblPreShortMoveDistance.Location = new System.Drawing.Point(317, 34);
            this.lblPreShortMoveDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPreShortMoveDistance.Name = "lblPreShortMoveDistance";
            this.lblPreShortMoveDistance.Size = new System.Drawing.Size(163, 17);
            this.lblPreShortMoveDistance.TabIndex = 2;
            this.lblPreShortMoveDistance.Text = "定位到阻挡距离(mm):";

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "轨道模式：";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ckbWaitOutInExitSensor);
            this.groupBox5.Location = new System.Drawing.Point(127, 191);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Size = new System.Drawing.Size(189, 57);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "出板位置";
            // 
            // ckbWaitOutInExitSensor
            // 
            this.ckbWaitOutInExitSensor.AutoSize = true;
            this.ckbWaitOutInExitSensor.Location = new System.Drawing.Point(28, 25);
            this.ckbWaitOutInExitSensor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckbWaitOutInExitSensor.Name = "ckbWaitOutInExitSensor";
            this.ckbWaitOutInExitSensor.Size = new System.Drawing.Size(132, 21);
            this.ckbWaitOutInExitSensor.TabIndex = 0;
            this.ckbWaitOutInExitSensor.Text = "出板感应处等待";
            this.ckbWaitOutInExitSensor.UseVisualStyleBackColor = true;

            // 
            // tabRTVSetting
            // 
            this.tabRTVSetting.Controls.Add(this.txtDownConveyorTime);
            this.tabRTVSetting.Controls.Add(this.label4);
            this.tabRTVSetting.Controls.Add(this.txtUpConveyorTIme);
            this.tabRTVSetting.Controls.Add(this.label3);
            this.tabRTVSetting.Controls.Add(this.txtIOTime);
            this.tabRTVSetting.Controls.Add(this.label2);
            this.tabRTVSetting.Controls.Add(this.chkIOEnable);
            this.tabRTVSetting.Location = new System.Drawing.Point(4, 26);
            this.tabRTVSetting.Name = "tabRTVSetting";
            this.tabRTVSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabRTVSetting.Size = new System.Drawing.Size(639, 491);
            this.tabRTVSetting.TabIndex = 2;
            this.tabRTVSetting.Text = "RTV设定";
            this.tabRTVSetting.UseVisualStyleBackColor = true;
            // 
            // txtDownConveyorTime
            // 
            this.txtDownConveyorTime.BackColor = System.Drawing.Color.White;
            this.txtDownConveyorTime.Location = new System.Drawing.Point(246, 130);
            this.txtDownConveyorTime.Name = "txtDownConveyorTime";
            this.txtDownConveyorTime.Size = new System.Drawing.Size(100, 25);
            this.txtDownConveyorTime.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "下层轨道出板后转动时长(S)：";
            // 
            // txtUpConveyorTIme
            // 
            this.txtUpConveyorTIme.BackColor = System.Drawing.Color.White;
            this.txtUpConveyorTIme.Location = new System.Drawing.Point(246, 92);
            this.txtUpConveyorTIme.Name = "txtUpConveyorTIme";
            this.txtUpConveyorTIme.Size = new System.Drawing.Size(100, 25);
            this.txtUpConveyorTIme.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "上层轨道出板后转动时长(S)：";
            // 
            // txtIOTime
            // 
            this.txtIOTime.BackColor = System.Drawing.Color.White;
            this.txtIOTime.Location = new System.Drawing.Point(246, 54);
            this.txtIOTime.Name = "txtIOTime";
            this.txtIOTime.Size = new System.Drawing.Size(100, 25);
            this.txtIOTime.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "IO失效判定时长(S)：";

            // 
            // chkIOEnable
            // 
            this.chkIOEnable.AutoSize = true;
            this.chkIOEnable.Location = new System.Drawing.Point(19, 16);
            this.chkIOEnable.Name = "chkIOEnable";
            this.chkIOEnable.Size = new System.Drawing.Size(137, 21);
            this.chkIOEnable.TabIndex = 0;
            this.chkIOEnable.Text = "IO作为气缸依据";
            this.chkIOEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "轨道模式：";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabOtherSettings
            // 
            this.tabOtherSettings.Controls.Add(this.cbxConveyorScan);
            this.tabOtherSettings.Location = new System.Drawing.Point(4, 26);
            this.tabOtherSettings.Name = "tabOtherSettings";
            this.tabOtherSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabOtherSettings.Size = new System.Drawing.Size(639, 491);
            this.tabOtherSettings.TabIndex = 3;
            this.tabOtherSettings.Text = "其他设置";
            this.tabOtherSettings.UseVisualStyleBackColor = true;
            // 
            // cbxConveyorScan
            // 
            this.cbxConveyorScan.AutoSize = true;
            this.cbxConveyorScan.Location = new System.Drawing.Point(19, 19);
            this.cbxConveyorScan.Name = "cbxConveyorScan";
            this.cbxConveyorScan.Size = new System.Drawing.Size(87, 21);
            this.cbxConveyorScan.TabIndex = 0;
            this.cbxConveyorScan.Text = "轨道扫码";
            this.cbxConveyorScan.UseVisualStyleBackColor = true;
            // 
            // ConveyorSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabConveyorSetting);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ConveyorSettingControl";
            this.Size = new System.Drawing.Size(659, 576);
            this.tabConveyorSetting.ResumeLayout(false);
            this.tabBaseSetting.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpConveyorSpeed.ResumeLayout(false);
            this.grpConveyorSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConveyorSpeed)).EndInit();
            this.grpSMEMAMode.ResumeLayout(false);
            this.grpSMEMAMode.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulseTime)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownStreamStuckTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpStreamStuckTime)).EndInit();
            this.grpStandaloneMode.ResumeLayout(false);
            this.grpExitMode.ResumeLayout(false);
            this.grpExitMode.PerformLayout();
            this.grpAutoMode.ResumeLayout(false);
            this.grpAutoMode.PerformLayout();
            this.grpConveyorDirection.ResumeLayout(false);
            this.grpConveyorDirection.PerformLayout();
            this.grpSitesCount.ResumeLayout(false);
            this.grpSitesCount.PerformLayout();
            this.tabSubsitesSetting.ResumeLayout(false);
            this.grpFinishedSite.ResumeLayout(false);
            this.grpFinishedSite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteHeatingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteLongMoveDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishedSiteShortMoveDistance)).EndInit();
            this.grpWorkingSite.ResumeLayout(false);
            this.grpWorkingSite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingHeatingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteExitDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteLongMoveDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkingSiteShortMoveDistance)).EndInit();
            this.grpUnifiedParam.ResumeLayout(false);
            this.grpUnifiedParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoardLeftDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStuckCoefficent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiftDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopperDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCheckTime)).EndInit();
            this.grpPresite.ResumeLayout(false);
            this.grpPresite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreHeatingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreLongMoveDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreShortMoveDistance)).EndInit();
            this.tabRTVSetting.ResumeLayout(false);
            this.tabRTVSetting.PerformLayout();
            this.tabOtherSettings.ResumeLayout(false);
            this.tabOtherSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabConveyorSetting;
        private System.Windows.Forms.TabPage tabBaseSetting;
        private System.Windows.Forms.GroupBox grpConveyorDirection;
        private System.Windows.Forms.RadioButton rdoRight2Right;
        private System.Windows.Forms.RadioButton rdoLeft2Left;
        private System.Windows.Forms.RadioButton rdoRight2Left;
        private System.Windows.Forms.RadioButton rdoLeft2Right;
        private System.Windows.Forms.GroupBox grpSitesCount;
        private System.Windows.Forms.RadioButton rdoTripleSites;
        private System.Windows.Forms.RadioButton rdoSingleSite;
        private System.Windows.Forms.TabPage tabSubsitesSetting;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpConveyorSpeed;
        private System.Windows.Forms.GroupBox grpSMEMAMode;
        private System.Windows.Forms.NumericUpDown nudDownStreamStuckTime;
        private System.Windows.Forms.Label DownStreamStuckTime;
        private System.Windows.Forms.NumericUpDown nudUpStreamStuckTime;
        private System.Windows.Forms.Label lblUpStreamStuckTime;
        private System.Windows.Forms.NumericUpDown nudPulseTime;
        private System.Windows.Forms.Label lblPulse;
        private System.Windows.Forms.RadioButton rdoPulseSignal;
        private System.Windows.Forms.RadioButton rdoContinuousSignal;
        private System.Windows.Forms.GroupBox grpStandaloneMode;
        private System.Windows.Forms.GroupBox grpExitMode;
        private System.Windows.Forms.RadioButton rdoManualTake;
        private System.Windows.Forms.RadioButton rdoStraightExit;
        private System.Windows.Forms.GroupBox grpAutoMode;
        private System.Windows.Forms.RadioButton rdoEnterStandaloneMode;
        private System.Windows.Forms.RadioButton rdoEnterSMEMAMode;
        private System.Windows.Forms.NumericUpDown nudConveyorSpeed;
        private System.Windows.Forms.Label lblConveyorSpeed;
        private System.Windows.Forms.GroupBox grpPresite;
        private System.Windows.Forms.GroupBox grpUnifiedParam;
        private System.Windows.Forms.NumericUpDown nudStuckCoefficent;
        private System.Windows.Forms.Label lblStuckCoefficent;
        private System.Windows.Forms.NumericUpDown nudLiftDelay;
        private System.Windows.Forms.NumericUpDown nudStopperDelay;
        private System.Windows.Forms.Label lblLiftDelay;
        private System.Windows.Forms.Label lblStopperDelay;
        private System.Windows.Forms.Label lblPreShortMoveDistance;
        private System.Windows.Forms.NumericUpDown nudPreLongMoveDistance;
        private System.Windows.Forms.Label lblLongMoveDistance;
        private System.Windows.Forms.NumericUpDown nudPreShortMoveDistance;
        private System.Windows.Forms.GroupBox grpFinishedSite;
        private System.Windows.Forms.NumericUpDown nudFinishedSiteLongMoveDistance;
        private System.Windows.Forms.Label lblFinishedSiteLongMoveDistance;
        private System.Windows.Forms.NumericUpDown nudFinishedSiteShortMoveDistance;
        private System.Windows.Forms.Label lblFinishedSiteShortMoveDistance;
        private System.Windows.Forms.GroupBox grpWorkingSite;
        private System.Windows.Forms.NumericUpDown nudWorkingSiteExitDistance;
        private System.Windows.Forms.Label lblWorkingSiteExitDistance;
        private System.Windows.Forms.NumericUpDown nudWorkingSiteLongMoveDistance;
        private System.Windows.Forms.Label lblWorkingSiteLongMoveSistance;
        private System.Windows.Forms.NumericUpDown nudWorkingSiteShortMoveDistance;
        private System.Windows.Forms.Label lblWorkingSiteShortMoveDistance;
        private System.Windows.Forms.NumericUpDown nudCheckTime;
        private System.Windows.Forms.Label lblCheckTime;
        private System.Windows.Forms.NumericUpDown nudFinishedSiteHeatingTime;
        private System.Windows.Forms.Label lblFinishedSiteHeatingTime;
        private System.Windows.Forms.NumericUpDown nudPreHeatingTime;
        private System.Windows.Forms.Label lblPreHeatingTIme;
        private System.Windows.Forms.NumericUpDown nudBoardLeftDelay;
        private System.Windows.Forms.Label lblBoardLeftDelay;
        private System.Windows.Forms.NumericUpDown nudWorkingHeatingTime;
        private System.Windows.Forms.Label lblWorkingHeatingTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoReceiveIsPulse;
        private System.Windows.Forms.RadioButton rdoReceiveIsContinues;
        private System.Windows.Forms.NumericUpDown nudAccTime;
        private System.Windows.Forms.Label lblAccTime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSingleInteraction;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoExitStandaloneMode;
        private System.Windows.Forms.RadioButton rdoExitSMEMAMode;
        private System.Windows.Forms.RadioButton rdoDispenseAndInsulation;
        private System.Windows.Forms.RadioButton rdoPreAndDispense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox ckbWaitOutInExitSensor;
        private System.Windows.Forms.TabPage tabRTVSetting;
        private Controls.IntTextBox txtDownConveyorTime;
        private System.Windows.Forms.Label label4;
        private Controls.IntTextBox txtUpConveyorTIme;
        private System.Windows.Forms.Label label3;
        private Controls.IntTextBox txtIOTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIOEnable;
        private System.Windows.Forms.TabPage tabOtherSettings;
        private System.Windows.Forms.CheckBox cbxConveyorScan;
    }
}
