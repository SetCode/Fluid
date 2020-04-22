namespace Anda.Fluid.Domain.Sensors
{
    partial class UserControlComParams
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPortName = new System.Windows.Forms.Label();
            this.labelBuadRate = new System.Windows.Forms.Label();
            this.labelDatabits = new System.Windows.Forms.Label();
            this.labelParity = new System.Windows.Forms.Label();
            this.labelStopBit = new System.Windows.Forms.Label();
            this.flpComParams = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxPortName = new System.Windows.Forms.ComboBox();
            this.comboBoxBuadRate = new System.Windows.Forms.ComboBox();
            this.comboBoxDatabits = new System.Windows.Forms.ComboBox();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBit = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpComParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelPortName);
            this.flowLayoutPanel1.Controls.Add(this.labelBuadRate);
            this.flowLayoutPanel1.Controls.Add(this.labelDatabits);
            this.flowLayoutPanel1.Controls.Add(this.labelParity);
            this.flowLayoutPanel1.Controls.Add(this.labelStopBit);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(78, 154);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelPortName
            // 
            this.labelPortName.Location = new System.Drawing.Point(3, 8);
            this.labelPortName.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelPortName.Name = "labelPortName";
            this.labelPortName.Size = new System.Drawing.Size(64, 20);
            this.labelPortName.TabIndex = 0;
            this.labelPortName.Text = "PortName";
            this.labelPortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBuadRate
            // 
            this.labelBuadRate.Location = new System.Drawing.Point(3, 36);
            this.labelBuadRate.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelBuadRate.Name = "labelBuadRate";
            this.labelBuadRate.Size = new System.Drawing.Size(64, 20);
            this.labelBuadRate.TabIndex = 0;
            this.labelBuadRate.Text = "BuadRate";
            this.labelBuadRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDatabits
            // 
            this.labelDatabits.Location = new System.Drawing.Point(3, 64);
            this.labelDatabits.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelDatabits.Name = "labelDatabits";
            this.labelDatabits.Size = new System.Drawing.Size(64, 20);
            this.labelDatabits.TabIndex = 0;
            this.labelDatabits.Text = "Databits";
            this.labelDatabits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelParity
            // 
            this.labelParity.Location = new System.Drawing.Point(3, 92);
            this.labelParity.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(64, 20);
            this.labelParity.TabIndex = 0;
            this.labelParity.Text = "Parity";
            this.labelParity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStopBit
            // 
            this.labelStopBit.Location = new System.Drawing.Point(3, 120);
            this.labelStopBit.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelStopBit.Name = "labelStopBit";
            this.labelStopBit.Size = new System.Drawing.Size(64, 20);
            this.labelStopBit.TabIndex = 0;
            this.labelStopBit.Text = "StopBit";
            this.labelStopBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpComParams
            // 
            this.flpComParams.Controls.Add(this.comboBoxPortName);
            this.flpComParams.Controls.Add(this.comboBoxBuadRate);
            this.flpComParams.Controls.Add(this.comboBoxDatabits);
            this.flpComParams.Controls.Add(this.comboBoxParity);
            this.flpComParams.Controls.Add(this.comboBoxStopBit);
            this.flpComParams.Location = new System.Drawing.Point(87, 3);
            this.flpComParams.Name = "flpComParams";
            this.flpComParams.Size = new System.Drawing.Size(89, 154);
            this.flpComParams.TabIndex = 0;
            // 
            // comboBoxPortName
            // 
            this.comboBoxPortName.FormattingEnabled = true;
            this.comboBoxPortName.Location = new System.Drawing.Point(3, 8);
            this.comboBoxPortName.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.comboBoxPortName.Name = "comboBoxPortName";
            this.comboBoxPortName.Size = new System.Drawing.Size(75, 20);
            this.comboBoxPortName.TabIndex = 0;
            this.comboBoxPortName.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortName_SelectedIndexChanged);
            // 
            // comboBoxBuadRate
            // 
            this.comboBoxBuadRate.FormattingEnabled = true;
            this.comboBoxBuadRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.comboBoxBuadRate.Location = new System.Drawing.Point(3, 36);
            this.comboBoxBuadRate.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.comboBoxBuadRate.Name = "comboBoxBuadRate";
            this.comboBoxBuadRate.Size = new System.Drawing.Size(75, 20);
            this.comboBoxBuadRate.TabIndex = 0;
            this.comboBoxBuadRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortName_SelectedIndexChanged);
            // 
            // comboBoxDatabits
            // 
            this.comboBoxDatabits.FormattingEnabled = true;
            this.comboBoxDatabits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.comboBoxDatabits.Location = new System.Drawing.Point(3, 64);
            this.comboBoxDatabits.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.comboBoxDatabits.Name = "comboBoxDatabits";
            this.comboBoxDatabits.Size = new System.Drawing.Size(75, 20);
            this.comboBoxDatabits.TabIndex = 0;
            this.comboBoxDatabits.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortName_SelectedIndexChanged);
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.comboBoxParity.Location = new System.Drawing.Point(3, 92);
            this.comboBoxParity.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(75, 20);
            this.comboBoxParity.TabIndex = 0;
            this.comboBoxParity.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortName_SelectedIndexChanged);
            // 
            // comboBoxStopBit
            // 
            this.comboBoxStopBit.FormattingEnabled = true;
            this.comboBoxStopBit.Items.AddRange(new object[] {
            "One",
            "Two",
            "OnePointFive"});
            this.comboBoxStopBit.Location = new System.Drawing.Point(3, 120);
            this.comboBoxStopBit.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.comboBoxStopBit.Name = "comboBoxStopBit";
            this.comboBoxStopBit.Size = new System.Drawing.Size(75, 20);
            this.comboBoxStopBit.TabIndex = 0;
            this.comboBoxStopBit.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortName_SelectedIndexChanged);
            // 
            // UserControlComParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.flpComParams);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UserControlComParams";
            this.Size = new System.Drawing.Size(180, 165);
            this.Load += new System.EventHandler(this.UserControlComParams_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flpComParams.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelPortName;
        private System.Windows.Forms.Label labelBuadRate;
        private System.Windows.Forms.Label labelDatabits;
        private System.Windows.Forms.Label labelParity;
        private System.Windows.Forms.Label labelStopBit;
        private System.Windows.Forms.FlowLayoutPanel flpComParams;
        private System.Windows.Forms.ComboBox comboBoxPortName;
        private System.Windows.Forms.ComboBox comboBoxBuadRate;
        private System.Windows.Forms.ComboBox comboBoxDatabits;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxStopBit;
        public System.Windows.Forms.ComboBox ComboBoxPortName { get { return comboBoxPortName; } set { comboBoxPortName = value; } }
        public System.Windows.Forms.ComboBox ComboBoxBuadRate { get { return comboBoxBuadRate; } set { comboBoxBuadRate = value; } }
        public System.Windows.Forms.ComboBox ComboBoxDatabits { get { return comboBoxDatabits; } set { comboBoxDatabits = value; } }
        public System.Windows.Forms.ComboBox ComboBoxParity { get { return comboBoxParity; } set { comboBoxParity = value; } }
        public System.Windows.Forms.ComboBox ComboBoxStopBit { get { return comboBoxStopBit; } set { comboBoxStopBit = value; } }
    }
}
