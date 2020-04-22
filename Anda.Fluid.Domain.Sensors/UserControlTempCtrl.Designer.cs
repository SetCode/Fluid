namespace Anda.Fluid.Domain.Sensors
{
    partial class UserControlTempCtrl
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
            ((System.ComponentModel.ISupportInitialize)(this.nudTempOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSetTemp
            // 
            this.btnSetTemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetTemp.Location = new System.Drawing.Point(212, 36);
            this.btnSetTemp.Name = "btnSetTemp";
            this.btnSetTemp.Size = new System.Drawing.Size(55, 25);
            this.btnSetTemp.TabIndex = 0;
            this.btnSetTemp.Text = "设置";
            this.btnSetTemp.UseVisualStyleBackColor = true;
            this.btnSetTemp.Click += new System.EventHandler(this.btnSetTemp_Click);
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemp.Location = new System.Drawing.Point(31, 41);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(35, 14);
            this.lblTemp.TabIndex = 1;
            this.lblTemp.Text = "温度";
            // 
            // lblUpAlarm
            // 
            this.lblUpAlarm.AutoSize = true;
            this.lblUpAlarm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUpAlarm.Location = new System.Drawing.Point(3, 70);
            this.lblUpAlarm.Name = "lblUpAlarm";
            this.lblUpAlarm.Size = new System.Drawing.Size(63, 14);
            this.lblUpAlarm.TabIndex = 1;
            this.lblUpAlarm.Text = "报警上限";
            // 
            // lblDownAlarm
            // 
            this.lblDownAlarm.AutoSize = true;
            this.lblDownAlarm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDownAlarm.Location = new System.Drawing.Point(3, 99);
            this.lblDownAlarm.Name = "lblDownAlarm";
            this.lblDownAlarm.Size = new System.Drawing.Size(63, 14);
            this.lblDownAlarm.TabIndex = 1;
            this.lblDownAlarm.Text = "报警下限";
            // 
            // lblTempOffset
            // 
            this.lblTempOffset.AutoSize = true;
            this.lblTempOffset.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTempOffset.Location = new System.Drawing.Point(3, 128);
            this.lblTempOffset.Name = "lblTempOffset";
            this.lblTempOffset.Size = new System.Drawing.Size(63, 14);
            this.lblTempOffset.TabIndex = 1;
            this.lblTempOffset.Text = "温度偏移";
            // 
            // nudTempOffset
            // 
            this.nudTempOffset.DecimalPlaces = 1;
            this.nudTempOffset.Location = new System.Drawing.Point(142, 125);
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
            this.nudTempOffset.Size = new System.Drawing.Size(57, 21);
            this.nudTempOffset.TabIndex = 3;
            // 
            // nudTemp
            // 
            this.nudTemp.DecimalPlaces = 1;
            this.nudTemp.Location = new System.Drawing.Point(142, 38);
            this.nudTemp.Name = "nudTemp";
            this.nudTemp.Size = new System.Drawing.Size(57, 21);
            this.nudTemp.TabIndex = 4;
            // 
            // nudUpAlarm
            // 
            this.nudUpAlarm.DecimalPlaces = 1;
            this.nudUpAlarm.Location = new System.Drawing.Point(142, 67);
            this.nudUpAlarm.Name = "nudUpAlarm";
            this.nudUpAlarm.Size = new System.Drawing.Size(57, 21);
            this.nudUpAlarm.TabIndex = 5;
            // 
            // nudDownAlarm
            // 
            this.nudDownAlarm.DecimalPlaces = 1;
            this.nudDownAlarm.Location = new System.Drawing.Point(142, 96);
            this.nudDownAlarm.Name = "nudDownAlarm";
            this.nudDownAlarm.Size = new System.Drawing.Size(57, 21);
            this.nudDownAlarm.TabIndex = 6;
            // 
            // btnSetAlarmUp
            // 
            this.btnSetAlarmUp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetAlarmUp.Location = new System.Drawing.Point(212, 65);
            this.btnSetAlarmUp.Name = "btnSetAlarmUp";
            this.btnSetAlarmUp.Size = new System.Drawing.Size(55, 25);
            this.btnSetAlarmUp.TabIndex = 11;
            this.btnSetAlarmUp.Text = "设置";
            this.btnSetAlarmUp.UseVisualStyleBackColor = true;
            this.btnSetAlarmUp.Click += new System.EventHandler(this.btnSetAlarmUp_Click);
            // 
            // btnSetAlarmLow
            // 
            this.btnSetAlarmLow.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetAlarmLow.Location = new System.Drawing.Point(212, 94);
            this.btnSetAlarmLow.Name = "btnSetAlarmLow";
            this.btnSetAlarmLow.Size = new System.Drawing.Size(55, 25);
            this.btnSetAlarmLow.TabIndex = 12;
            this.btnSetAlarmLow.Text = "设置";
            this.btnSetAlarmLow.UseVisualStyleBackColor = true;
            this.btnSetAlarmLow.Click += new System.EventHandler(this.btnSetAlarmLow_Click);
            // 
            // btnSetTempOffset
            // 
            this.btnSetTempOffset.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetTempOffset.Location = new System.Drawing.Point(212, 123);
            this.btnSetTempOffset.Name = "btnSetTempOffset";
            this.btnSetTempOffset.Size = new System.Drawing.Size(55, 25);
            this.btnSetTempOffset.TabIndex = 13;
            this.btnSetTempOffset.Text = "设置";
            this.btnSetTempOffset.UseVisualStyleBackColor = true;
            this.btnSetTempOffset.Click += new System.EventHandler(this.btnSetTempOffset_Click);
            // 
            // richTxtMsg
            // 
            this.richTxtMsg.Location = new System.Drawing.Point(273, 36);
            this.richTxtMsg.Name = "richTxtMsg";
            this.richTxtMsg.Size = new System.Drawing.Size(154, 83);
            this.richTxtMsg.TabIndex = 15;
            this.richTxtMsg.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(359, 123);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 25);
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
            this.rdoValve1.Location = new System.Drawing.Point(16, 8);
            this.rdoValve1.Name = "rdoValve1";
            this.rdoValve1.Size = new System.Drawing.Size(59, 16);
            this.rdoValve1.TabIndex = 17;
            this.rdoValve1.TabStop = true;
            this.rdoValve1.Text = "Valve1";
            this.rdoValve1.UseVisualStyleBackColor = true;
            this.rdoValve1.CheckedChanged += new System.EventHandler(this.rdoValve1_CheckedChanged);
            // 
            // rdoValve2
            // 
            this.rdoValve2.AutoSize = true;
            this.rdoValve2.Location = new System.Drawing.Point(92, 8);
            this.rdoValve2.Name = "rdoValve2";
            this.rdoValve2.Size = new System.Drawing.Size(59, 16);
            this.rdoValve2.TabIndex = 18;
            this.rdoValve2.Text = "Valve2";
            this.rdoValve2.UseVisualStyleBackColor = true;
            // 
            // UserControlTempCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "UserControlTempCtrl";
            this.Size = new System.Drawing.Size(434, 156);
            ((System.ComponentModel.ISupportInitialize)(this.nudTempOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDownAlarm)).EndInit();
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
    }
}
