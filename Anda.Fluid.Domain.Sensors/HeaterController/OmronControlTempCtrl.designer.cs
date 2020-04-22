namespace Anda.Fluid.Domain.Sensors.HeaterController
{
    partial class OmronControlTempCtrl
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
            this.btnSetTemp = new System.Windows.Forms.Button();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblUpAlarm = new System.Windows.Forms.Label();
            this.lblDownAlarm = new System.Windows.Forms.Label();
            this.lblTempOffset = new System.Windows.Forms.Label();
            this.nudTempOffset = new System.Windows.Forms.NumericUpDown();
            this.nudTemp = new System.Windows.Forms.NumericUpDown();
            this.nudUpAlarm = new System.Windows.Forms.NumericUpDown();
            this.nudDownAlarm = new System.Windows.Forms.NumericUpDown();
            this.btnSetAlarmUp = new System.Windows.Forms.Button();
            this.btnSetAlarmLow = new System.Windows.Forms.Button();
            this.btnSetTempOffset = new System.Windows.Forms.Button();
            this.richTxtMsg = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.rdoValve1 = new System.Windows.Forms.RadioButton();
            this.rdoValve2 = new System.Windows.Forms.RadioButton();
            this.rdoContinueHeating = new System.Windows.Forms.RadioButton();
            this.rdoManufactureHeating = new System.Windows.Forms.RadioButton();
            this.chkIdleClosed = new System.Windows.Forms.CheckBox();
            this.intTxtClosedDecideTime = new Anda.Fluid.Controls.IntTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAlarmEnable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownAlarm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSetTemp
            // 
            this.btnSetTemp.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTemp.Location = new System.Drawing.Point(258, 51);
            this.btnSetTemp.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetTemp.Name = "btnSetTemp";
            this.btnSetTemp.Size = new System.Drawing.Size(92, 35);
            this.btnSetTemp.TabIndex = 0;
            this.btnSetTemp.Text = "设置";
            this.btnSetTemp.UseVisualStyleBackColor = true;
            this.btnSetTemp.Click += new System.EventHandler(this.btnSetTemp_Click);
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(24, 59);
            this.lblTemp.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(38, 17);
            this.lblTemp.TabIndex = 1;
            this.lblTemp.Text = "温度";
            // 
            // lblUpAlarm
            // 
            this.lblUpAlarm.AutoSize = true;
            this.lblUpAlarm.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpAlarm.Location = new System.Drawing.Point(5, 99);
            this.lblUpAlarm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUpAlarm.Name = "lblUpAlarm";
            this.lblUpAlarm.Size = new System.Drawing.Size(68, 17);
            this.lblUpAlarm.TabIndex = 1;
            this.lblUpAlarm.Text = "报警上限";
            // 
            // lblDownAlarm
            // 
            this.lblDownAlarm.AutoSize = true;
            this.lblDownAlarm.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownAlarm.Location = new System.Drawing.Point(5, 140);
            this.lblDownAlarm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDownAlarm.Name = "lblDownAlarm";
            this.lblDownAlarm.Size = new System.Drawing.Size(68, 17);
            this.lblDownAlarm.TabIndex = 1;
            this.lblDownAlarm.Text = "报警下限";
            // 
            // lblTempOffset
            // 
            this.lblTempOffset.AutoSize = true;
            this.lblTempOffset.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempOffset.Location = new System.Drawing.Point(5, 181);
            this.lblTempOffset.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTempOffset.Name = "lblTempOffset";
            this.lblTempOffset.Size = new System.Drawing.Size(68, 17);
            this.lblTempOffset.TabIndex = 1;
            this.lblTempOffset.Text = "温度偏移";
            // 
            // nudTempOffset
            // 
            this.nudTempOffset.DecimalPlaces = 1;
            this.nudTempOffset.Location = new System.Drawing.Point(153, 180);
            this.nudTempOffset.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudTempOffset.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTempOffset.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudTempOffset.Name = "nudTempOffset";
            this.nudTempOffset.Size = new System.Drawing.Size(95, 25);
            this.nudTempOffset.TabIndex = 3;
            // 
            // nudTemp
            // 
            this.nudTemp.DecimalPlaces = 1;
            this.nudTemp.Location = new System.Drawing.Point(153, 57);
            this.nudTemp.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudTemp.Name = "nudTemp";
            this.nudTemp.Size = new System.Drawing.Size(95, 25);
            this.nudTemp.TabIndex = 4;
            // 
            // nudUpAlarm
            // 
            this.nudUpAlarm.DecimalPlaces = 1;
            this.nudUpAlarm.Location = new System.Drawing.Point(153, 98);
            this.nudUpAlarm.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudUpAlarm.Name = "nudUpAlarm";
            this.nudUpAlarm.Size = new System.Drawing.Size(95, 25);
            this.nudUpAlarm.TabIndex = 5;
            // 
            // nudDownAlarm
            // 
            this.nudDownAlarm.DecimalPlaces = 1;
            this.nudDownAlarm.Location = new System.Drawing.Point(153, 139);
            this.nudDownAlarm.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudDownAlarm.Name = "nudDownAlarm";
            this.nudDownAlarm.Size = new System.Drawing.Size(95, 25);
            this.nudDownAlarm.TabIndex = 6;
            // 
            // btnSetAlarmUp
            // 
            this.btnSetAlarmUp.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetAlarmUp.Location = new System.Drawing.Point(258, 92);
            this.btnSetAlarmUp.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetAlarmUp.Name = "btnSetAlarmUp";
            this.btnSetAlarmUp.Size = new System.Drawing.Size(92, 35);
            this.btnSetAlarmUp.TabIndex = 11;
            this.btnSetAlarmUp.Text = "设置";
            this.btnSetAlarmUp.UseVisualStyleBackColor = true;
            this.btnSetAlarmUp.Click += new System.EventHandler(this.btnSetAlarmUp_Click);
            // 
            // btnSetAlarmLow
            // 
            this.btnSetAlarmLow.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetAlarmLow.Location = new System.Drawing.Point(258, 133);
            this.btnSetAlarmLow.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetAlarmLow.Name = "btnSetAlarmLow";
            this.btnSetAlarmLow.Size = new System.Drawing.Size(92, 35);
            this.btnSetAlarmLow.TabIndex = 12;
            this.btnSetAlarmLow.Text = "设置";
            this.btnSetAlarmLow.UseVisualStyleBackColor = true;
            this.btnSetAlarmLow.Click += new System.EventHandler(this.btnSetAlarmLow_Click);
            // 
            // btnSetTempOffset
            // 
            this.btnSetTempOffset.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTempOffset.Location = new System.Drawing.Point(258, 174);
            this.btnSetTempOffset.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetTempOffset.Name = "btnSetTempOffset";
            this.btnSetTempOffset.Size = new System.Drawing.Size(92, 35);
            this.btnSetTempOffset.TabIndex = 13;
            this.btnSetTempOffset.Text = "设置";
            this.btnSetTempOffset.UseVisualStyleBackColor = true;
            this.btnSetTempOffset.Click += new System.EventHandler(this.btnSetTempOffset_Click);
            // 
            // richTxtMsg
            // 
            this.richTxtMsg.Location = new System.Drawing.Point(360, 52);
            this.richTxtMsg.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.richTxtMsg.Name = "richTxtMsg";
            this.richTxtMsg.Size = new System.Drawing.Size(254, 157);
            this.richTxtMsg.TabIndex = 15;
            this.richTxtMsg.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(501, 285);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 35);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // rdoValve1
            // 
            this.rdoValve1.AutoSize = true;
            this.rdoValve1.Checked = true;
            this.rdoValve1.Location = new System.Drawing.Point(27, 11);
            this.rdoValve1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoValve1.Name = "rdoValve1";
            this.rdoValve1.Size = new System.Drawing.Size(78, 21);
            this.rdoValve1.TabIndex = 17;
            this.rdoValve1.TabStop = true;
            this.rdoValve1.Text = "Valve1";
            this.rdoValve1.UseVisualStyleBackColor = true;
            this.rdoValve1.CheckedChanged += new System.EventHandler(this.rdoValve1_CheckedChanged);
            // 
            // rdoValve2
            // 
            this.rdoValve2.AutoSize = true;
            this.rdoValve2.Location = new System.Drawing.Point(153, 11);
            this.rdoValve2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoValve2.Name = "rdoValve2";
            this.rdoValve2.Size = new System.Drawing.Size(78, 21);
            this.rdoValve2.TabIndex = 18;
            this.rdoValve2.Text = "Valve2";
            this.rdoValve2.UseVisualStyleBackColor = true;
            // 
            // rdoContinueHeating
            // 
            this.rdoContinueHeating.AutoSize = true;
            this.rdoContinueHeating.Location = new System.Drawing.Point(6, 22);
            this.rdoContinueHeating.Name = "rdoContinueHeating";
            this.rdoContinueHeating.Size = new System.Drawing.Size(86, 21);
            this.rdoContinueHeating.TabIndex = 19;
            this.rdoContinueHeating.TabStop = true;
            this.rdoContinueHeating.Text = "持续加热";
            this.rdoContinueHeating.UseVisualStyleBackColor = true;
            this.rdoContinueHeating.CheckedChanged += new System.EventHandler(this.rdoContinueHeating_CheckedChanged);
            // 
            // rdoManufactureHeating
            // 
            this.rdoManufactureHeating.AutoSize = true;
            this.rdoManufactureHeating.Location = new System.Drawing.Point(177, 22);
            this.rdoManufactureHeating.Name = "rdoManufactureHeating";
            this.rdoManufactureHeating.Size = new System.Drawing.Size(101, 21);
            this.rdoManufactureHeating.TabIndex = 20;
            this.rdoManufactureHeating.TabStop = true;
            this.rdoManufactureHeating.Text = "生产时加热";
            this.rdoManufactureHeating.UseVisualStyleBackColor = true;
            // 
            // chkIdleClosed
            // 
            this.chkIdleClosed.AutoSize = true;
            this.chkIdleClosed.Location = new System.Drawing.Point(6, 50);
            this.chkIdleClosed.Name = "chkIdleClosed";
            this.chkIdleClosed.Size = new System.Drawing.Size(162, 21);
            this.chkIdleClosed.TabIndex = 21;
            this.chkIdleClosed.Text = "生产空闲时关闭加热";
            this.chkIdleClosed.UseVisualStyleBackColor = true;
            // 
            // intTxtClosedDecideTime
            // 
            this.intTxtClosedDecideTime.BackColor = System.Drawing.Color.White;
            this.intTxtClosedDecideTime.Location = new System.Drawing.Point(347, 47);
            this.intTxtClosedDecideTime.Name = "intTxtClosedDecideTime";
            this.intTxtClosedDecideTime.Size = new System.Drawing.Size(100, 25);
            this.intTxtClosedDecideTime.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "生产空闲判定时长(s)：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkIdleClosed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoContinueHeating);
            this.groupBox1.Controls.Add(this.intTxtClosedDecideTime);
            this.groupBox1.Controls.Add(this.rdoManufactureHeating);
            this.groupBox1.Location = new System.Drawing.Point(8, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 104);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "温控器启动设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "注意:开启此功能后,关闭加热再打开时会执行清洗动作!";
            // 
            // chkAlarmEnable
            // 
            this.chkAlarmEnable.AutoSize = true;
            this.chkAlarmEnable.Location = new System.Drawing.Point(501, 238);
            this.chkAlarmEnable.Name = "chkAlarmEnable";
            this.chkAlarmEnable.Size = new System.Drawing.Size(87, 21);
            this.chkAlarmEnable.TabIndex = 25;
            this.chkAlarmEnable.Text = "启用报警";
            this.chkAlarmEnable.UseVisualStyleBackColor = true;
            this.chkAlarmEnable.CheckedChanged += new System.EventHandler(this.chkAlarmEnable_CheckedChanged);
            // 
            // OmronControlTempCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAlarmEnable);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rdoValve2);
            this.Controls.Add(this.rdoValve1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.richTxtMsg);
            this.Controls.Add(this.btnSetTempOffset);
            this.Controls.Add(this.btnSetAlarmLow);
            this.Controls.Add(this.btnSetAlarmUp);
            this.Controls.Add(this.nudDownAlarm);
            this.Controls.Add(this.nudUpAlarm);
            this.Controls.Add(this.nudTemp);
            this.Controls.Add(this.nudTempOffset);
            this.Controls.Add(this.lblTempOffset);
            this.Controls.Add(this.lblDownAlarm);
            this.Controls.Add(this.lblUpAlarm);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.btnSetTemp);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "OmronControlTempCtrl";
            this.Size = new System.Drawing.Size(625, 323);
            ((System.ComponentModel.ISupportInitialize)(this.nudTempOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownAlarm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetTemp;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblUpAlarm;
        private System.Windows.Forms.Label lblDownAlarm;
        private System.Windows.Forms.Label lblTempOffset;
        private System.Windows.Forms.NumericUpDown nudTempOffset;
        private System.Windows.Forms.NumericUpDown nudTemp;
        private System.Windows.Forms.NumericUpDown nudUpAlarm;
        private System.Windows.Forms.NumericUpDown nudDownAlarm;
        private System.Windows.Forms.Button btnSetAlarmUp;
        private System.Windows.Forms.Button btnSetAlarmLow;
        private System.Windows.Forms.Button btnSetTempOffset;
        private System.Windows.Forms.RichTextBox richTxtMsg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.RadioButton rdoValve1;
        private System.Windows.Forms.RadioButton rdoValve2;
        private System.Windows.Forms.RadioButton rdoContinueHeating;
        private System.Windows.Forms.RadioButton rdoManufactureHeating;
        private System.Windows.Forms.CheckBox chkIdleClosed;
        private Controls.IntTextBox intTxtClosedDecideTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAlarmEnable;
    }
}
