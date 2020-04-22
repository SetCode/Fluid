namespace Anda.Fluid.Domain.DataStatistics
{
    partial class StatisticsForm
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
            this.pnlChartContainer = new System.Windows.Forms.Panel();
            this.rdoCapacityChart = new System.Windows.Forms.RadioButton();
            this.rdoNGInfoChart = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxOtherChart = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.rdoCTChart = new System.Windows.Forms.RadioButton();
            this.rdoDownTimeChart = new System.Windows.Forms.RadioButton();
            this.rdoTemperature = new System.Windows.Forms.RadioButton();
            this.rdoLaserData = new System.Windows.Forms.RadioButton();
            this.rdoDotWeight = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDownTime = new System.Windows.Forms.Button();
            this.ckbEnable = new System.Windows.Forms.CheckBox();
            this.btnCapacity = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlChartContainer
            // 
            this.pnlChartContainer.Location = new System.Drawing.Point(0, 2);
            this.pnlChartContainer.Name = "pnlChartContainer";
            this.pnlChartContainer.Size = new System.Drawing.Size(900, 700);
            this.pnlChartContainer.TabIndex = 0;
            // 
            // rdoCapacityChart
            // 
            this.rdoCapacityChart.AutoSize = true;
            this.rdoCapacityChart.Location = new System.Drawing.Point(928, 246);
            this.rdoCapacityChart.Name = "rdoCapacityChart";
            this.rdoCapacityChart.Size = new System.Drawing.Size(71, 16);
            this.rdoCapacityChart.TabIndex = 1;
            this.rdoCapacityChart.TabStop = true;
            this.rdoCapacityChart.Text = "Capacity";
            this.rdoCapacityChart.UseVisualStyleBackColor = true;
            this.rdoCapacityChart.CheckedChanged += new System.EventHandler(this.rdoCapacityChart_CheckedChanged);
            // 
            // rdoNGInfoChart
            // 
            this.rdoNGInfoChart.AutoSize = true;
            this.rdoNGInfoChart.Location = new System.Drawing.Point(928, 290);
            this.rdoNGInfoChart.Name = "rdoNGInfoChart";
            this.rdoNGInfoChart.Size = new System.Drawing.Size(65, 16);
            this.rdoNGInfoChart.TabIndex = 3;
            this.rdoNGInfoChart.TabStop = true;
            this.rdoNGInfoChart.Text = "NG Info";
            this.rdoNGInfoChart.UseVisualStyleBackColor = true;
            this.rdoNGInfoChart.CheckedChanged += new System.EventHandler(this.rdoNGInfoChart_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(928, 596);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Other data:";
            // 
            // cbxOtherChart
            // 
            this.cbxOtherChart.FormattingEnabled = true;
            this.cbxOtherChart.Location = new System.Drawing.Point(930, 615);
            this.cbxOtherChart.Name = "cbxOtherChart";
            this.cbxOtherChart.Size = new System.Drawing.Size(89, 20);
            this.cbxOtherChart.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(930, 678);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // rdoCTChart
            // 
            this.rdoCTChart.AutoSize = true;
            this.rdoCTChart.Location = new System.Drawing.Point(928, 268);
            this.rdoCTChart.Name = "rdoCTChart";
            this.rdoCTChart.Size = new System.Drawing.Size(77, 16);
            this.rdoCTChart.TabIndex = 7;
            this.rdoCTChart.TabStop = true;
            this.rdoCTChart.Text = "CycleTime";
            this.rdoCTChart.UseVisualStyleBackColor = true;
            this.rdoCTChart.CheckedChanged += new System.EventHandler(this.rdoCTChart_CheckedChanged);
            // 
            // rdoDownTimeChart
            // 
            this.rdoDownTimeChart.AutoSize = true;
            this.rdoDownTimeChart.Location = new System.Drawing.Point(928, 312);
            this.rdoDownTimeChart.Name = "rdoDownTimeChart";
            this.rdoDownTimeChart.Size = new System.Drawing.Size(77, 16);
            this.rdoDownTimeChart.TabIndex = 8;
            this.rdoDownTimeChart.TabStop = true;
            this.rdoDownTimeChart.Text = "Down Time";
            this.rdoDownTimeChart.UseVisualStyleBackColor = true;
            this.rdoDownTimeChart.CheckedChanged += new System.EventHandler(this.rdoDownTimeChart_CheckedChanged);
            // 
            // rdoTemperature
            // 
            this.rdoTemperature.AutoSize = true;
            this.rdoTemperature.Location = new System.Drawing.Point(928, 378);
            this.rdoTemperature.Name = "rdoTemperature";
            this.rdoTemperature.Size = new System.Drawing.Size(89, 16);
            this.rdoTemperature.TabIndex = 9;
            this.rdoTemperature.TabStop = true;
            this.rdoTemperature.Text = "Tempareture";
            this.rdoTemperature.UseVisualStyleBackColor = true;
            this.rdoTemperature.CheckedChanged += new System.EventHandler(this.rdoTemperature_CheckedChanged);
            // 
            // rdoLaserData
            // 
            this.rdoLaserData.AutoSize = true;
            this.rdoLaserData.Location = new System.Drawing.Point(928, 356);
            this.rdoLaserData.Name = "rdoLaserData";
            this.rdoLaserData.Size = new System.Drawing.Size(83, 16);
            this.rdoLaserData.TabIndex = 10;
            this.rdoLaserData.TabStop = true;
            this.rdoLaserData.Text = "Laser Data";
            this.rdoLaserData.UseVisualStyleBackColor = true;
            this.rdoLaserData.CheckedChanged += new System.EventHandler(this.rdoLaserData_CheckedChanged);
            // 
            // rdoDotWeight
            // 
            this.rdoDotWeight.AutoSize = true;
            this.rdoDotWeight.Location = new System.Drawing.Point(928, 334);
            this.rdoDotWeight.Name = "rdoDotWeight";
            this.rdoDotWeight.Size = new System.Drawing.Size(83, 16);
            this.rdoDotWeight.TabIndex = 11;
            this.rdoDotWeight.TabStop = true;
            this.rdoDotWeight.Text = "Dot Weight";
            this.rdoDotWeight.UseVisualStyleBackColor = true;
            this.rdoDotWeight.CheckedChanged += new System.EventHandler(this.rdoDotWeight_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(930, 436);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDownTime
            // 
            this.btnDownTime.Location = new System.Drawing.Point(928, 109);
            this.btnDownTime.Name = "btnDownTime";
            this.btnDownTime.Size = new System.Drawing.Size(75, 23);
            this.btnDownTime.TabIndex = 13;
            this.btnDownTime.Text = "DownTime";
            this.btnDownTime.UseVisualStyleBackColor = true;
            this.btnDownTime.Click += new System.EventHandler(this.btnDownTime_Click);
            // 
            // ckbEnable
            // 
            this.ckbEnable.AutoSize = true;
            this.ckbEnable.Location = new System.Drawing.Point(933, 545);
            this.ckbEnable.Name = "ckbEnable";
            this.ckbEnable.Size = new System.Drawing.Size(84, 16);
            this.ckbEnable.TabIndex = 14;
            this.ckbEnable.Text = "EnableData";
            this.ckbEnable.UseVisualStyleBackColor = true;
            this.ckbEnable.CheckedChanged += new System.EventHandler(this.ckbEnable_CheckedChanged);
            // 
            // btnCapacity
            // 
            this.btnCapacity.Location = new System.Drawing.Point(930, 30);
            this.btnCapacity.Name = "btnCapacity";
            this.btnCapacity.Size = new System.Drawing.Size(75, 23);
            this.btnCapacity.TabIndex = 15;
            this.btnCapacity.Text = "Capacity";
            this.btnCapacity.UseVisualStyleBackColor = true;
            this.btnCapacity.Click += new System.EventHandler(this.btnCapacity_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 713);
            this.Controls.Add(this.btnCapacity);
            this.Controls.Add(this.ckbEnable);
            this.Controls.Add(this.btnDownTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rdoDotWeight);
            this.Controls.Add(this.rdoLaserData);
            this.Controls.Add(this.rdoTemperature);
            this.Controls.Add(this.rdoDownTimeChart);
            this.Controls.Add(this.rdoCTChart);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxOtherChart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoNGInfoChart);
            this.Controls.Add(this.rdoCapacityChart);
            this.Controls.Add(this.pnlChartContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatisticsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlChartContainer;
        private System.Windows.Forms.RadioButton rdoCapacityChart;
        private System.Windows.Forms.RadioButton rdoNGInfoChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxOtherChart;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdoCTChart;
        private System.Windows.Forms.RadioButton rdoDownTimeChart;
        private System.Windows.Forms.RadioButton rdoTemperature;
        private System.Windows.Forms.RadioButton rdoLaserData;
        private System.Windows.Forms.RadioButton rdoDotWeight;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDownTime;
        private System.Windows.Forms.CheckBox ckbEnable;
        private System.Windows.Forms.Button btnCapacity;
    }
}