namespace Anda.Fluid.Domain.Sensors
{
    partial class FormLight
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.richTextBoxState = new System.Windows.Forms.RichTextBox();
            this.labelConnectState = new System.Windows.Forms.Label();
            this.flpSingleChannel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRChannelOpen = new System.Windows.Forms.Button();
            this.hScrollBarChannel_R = new System.Windows.Forms.HScrollBar();
            this.textBoxR_Value = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonGChannelOpen = new System.Windows.Forms.Button();
            this.hScrollBarChannel_G = new System.Windows.Forms.HScrollBar();
            this.textBoxG_Value = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonBChannelOpen = new System.Windows.Forms.Button();
            this.hScrollBarChannel_B = new System.Windows.Forms.HScrollBar();
            this.textBoxB_Value = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRGBChannelOpen = new System.Windows.Forms.Button();
            this.hScrollBarChannel_RGB = new System.Windows.Forms.HScrollBar();
            this.comboBoxRGB = new System.Windows.Forms.ComboBox();
            this.textBoxCombiValue = new System.Windows.Forms.TextBox();
            this.flp5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTrigWidth = new System.Windows.Forms.Button();
            this.txtTrigWidth = new System.Windows.Forms.TextBox();
            this.cmbRGBTrigWidth = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMultiTrigWidth = new System.Windows.Forms.Button();
            this.txtMultiTrigWidth = new System.Windows.Forms.TextBox();
            this.cmbMultiTrigWidth = new System.Windows.Forms.ComboBox();
            this.btnHBTrigWidth = new System.Windows.Forms.Button();
            this.btnHBMultiTrigWidth = new System.Windows.Forms.Button();
            this.chkResponse = new System.Windows.Forms.CheckBox();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtSubnetMask = new System.Windows.Forms.TextBox();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblSubnetMask = new System.Windows.Forms.Label();
            this.lblGateway = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSoftTrig = new System.Windows.Forms.Button();
            this.cmbSoftTrig = new System.Windows.Forms.ComboBox();
            this.btnMultiSoftTrig = new System.Windows.Forms.Button();
            this.cmbMultiSoftTrig = new System.Windows.Forms.ComboBox();
            this.lblMaxCurrent = new System.Windows.Forms.Label();
            this.txtTrigTime = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWorkMode = new System.Windows.Forms.Button();
            this.cmbWorkMode = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActivation = new System.Windows.Forms.Button();
            this.cmbActivation = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbMaxCurrent = new System.Windows.Forms.ComboBox();
            this.btnMaxCurrent = new System.Windows.Forms.Button();
            this.txtMaxCurrent = new System.Windows.Forms.TextBox();
            this.btnMultiMaxCurrent = new System.Windows.Forms.Button();
            this.lblTrigTime = new System.Windows.Forms.Label();
            this.cmbMultiMaxCurrent = new System.Windows.Forms.ComboBox();
            this.flpSingleChannel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flp5.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(83, 6);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(187, 21);
            this.textBoxIP.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(14, 47);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(89, 27);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // richTextBoxState
            // 
            this.richTextBoxState.Location = new System.Drawing.Point(454, 6);
            this.richTextBoxState.Name = "richTextBoxState";
            this.richTextBoxState.Size = new System.Drawing.Size(100, 178);
            this.richTextBoxState.TabIndex = 3;
            this.richTextBoxState.Text = "";
            // 
            // labelConnectState
            // 
            this.labelConnectState.AutoSize = true;
            this.labelConnectState.Location = new System.Drawing.Point(116, 55);
            this.labelConnectState.Name = "labelConnectState";
            this.labelConnectState.Size = new System.Drawing.Size(29, 12);
            this.labelConnectState.TabIndex = 4;
            this.labelConnectState.Text = "----";
            // 
            // flpSingleChannel
            // 
            this.flpSingleChannel.Controls.Add(this.flowLayoutPanel1);
            this.flpSingleChannel.Controls.Add(this.flowLayoutPanel2);
            this.flpSingleChannel.Controls.Add(this.flowLayoutPanel3);
            this.flpSingleChannel.Controls.Add(this.flowLayoutPanel4);
            this.flpSingleChannel.Location = new System.Drawing.Point(14, 191);
            this.flpSingleChannel.Name = "flpSingleChannel";
            this.flpSingleChannel.Size = new System.Drawing.Size(418, 151);
            this.flpSingleChannel.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonRChannelOpen);
            this.flowLayoutPanel1.Controls.Add(this.hScrollBarChannel_R);
            this.flowLayoutPanel1.Controls.Add(this.textBoxR_Value);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonRChannelOpen
            // 
            this.buttonRChannelOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonRChannelOpen.Name = "buttonRChannelOpen";
            this.buttonRChannelOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonRChannelOpen.TabIndex = 0;
            this.buttonRChannelOpen.Text = "R_Open";
            this.buttonRChannelOpen.UseVisualStyleBackColor = true;
            this.buttonRChannelOpen.Click += new System.EventHandler(this.buttonOpenChannel_Click);
            // 
            // hScrollBarChannel_R
            // 
            this.hScrollBarChannel_R.Location = new System.Drawing.Point(84, 3);
            this.hScrollBarChannel_R.Margin = new System.Windows.Forms.Padding(3);
            this.hScrollBarChannel_R.Name = "hScrollBarChannel_R";
            this.hScrollBarChannel_R.Size = new System.Drawing.Size(175, 23);
            this.hScrollBarChannel_R.TabIndex = 1;
            this.hScrollBarChannel_R.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarSetChannelValue_Scroll);
            // 
            // textBoxR_Value
            // 
            this.textBoxR_Value.Location = new System.Drawing.Point(265, 3);
            this.textBoxR_Value.Name = "textBoxR_Value";
            this.textBoxR_Value.Size = new System.Drawing.Size(57, 21);
            this.textBoxR_Value.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonGChannelOpen);
            this.flowLayoutPanel2.Controls.Add(this.hScrollBarChannel_G);
            this.flowLayoutPanel2.Controls.Add(this.textBoxG_Value);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(400, 29);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // buttonGChannelOpen
            // 
            this.buttonGChannelOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonGChannelOpen.Name = "buttonGChannelOpen";
            this.buttonGChannelOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonGChannelOpen.TabIndex = 0;
            this.buttonGChannelOpen.Text = "G_Open";
            this.buttonGChannelOpen.UseVisualStyleBackColor = true;
            this.buttonGChannelOpen.Click += new System.EventHandler(this.buttonOpenChannel_Click);
            // 
            // hScrollBarChannel_G
            // 
            this.hScrollBarChannel_G.Location = new System.Drawing.Point(84, 3);
            this.hScrollBarChannel_G.Margin = new System.Windows.Forms.Padding(3);
            this.hScrollBarChannel_G.Name = "hScrollBarChannel_G";
            this.hScrollBarChannel_G.Size = new System.Drawing.Size(175, 23);
            this.hScrollBarChannel_G.TabIndex = 1;
            this.hScrollBarChannel_G.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarSetChannelValue_Scroll);
            // 
            // textBoxG_Value
            // 
            this.textBoxG_Value.Location = new System.Drawing.Point(265, 3);
            this.textBoxG_Value.Name = "textBoxG_Value";
            this.textBoxG_Value.Size = new System.Drawing.Size(57, 21);
            this.textBoxG_Value.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.buttonBChannelOpen);
            this.flowLayoutPanel3.Controls.Add(this.hScrollBarChannel_B);
            this.flowLayoutPanel3.Controls.Add(this.textBoxB_Value);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 73);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(400, 29);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // buttonBChannelOpen
            // 
            this.buttonBChannelOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonBChannelOpen.Name = "buttonBChannelOpen";
            this.buttonBChannelOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonBChannelOpen.TabIndex = 0;
            this.buttonBChannelOpen.Text = "B_Open";
            this.buttonBChannelOpen.UseVisualStyleBackColor = true;
            this.buttonBChannelOpen.Click += new System.EventHandler(this.buttonOpenChannel_Click);
            // 
            // hScrollBarChannel_B
            // 
            this.hScrollBarChannel_B.Location = new System.Drawing.Point(84, 3);
            this.hScrollBarChannel_B.Margin = new System.Windows.Forms.Padding(3);
            this.hScrollBarChannel_B.Name = "hScrollBarChannel_B";
            this.hScrollBarChannel_B.Size = new System.Drawing.Size(175, 23);
            this.hScrollBarChannel_B.TabIndex = 1;
            this.hScrollBarChannel_B.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarSetChannelValue_Scroll);
            // 
            // textBoxB_Value
            // 
            this.textBoxB_Value.Location = new System.Drawing.Point(265, 3);
            this.textBoxB_Value.Name = "textBoxB_Value";
            this.textBoxB_Value.Size = new System.Drawing.Size(57, 21);
            this.textBoxB_Value.TabIndex = 2;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.buttonRGBChannelOpen);
            this.flowLayoutPanel4.Controls.Add(this.hScrollBarChannel_RGB);
            this.flowLayoutPanel4.Controls.Add(this.comboBoxRGB);
            this.flowLayoutPanel4.Controls.Add(this.textBoxCombiValue);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 108);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(400, 29);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // buttonRGBChannelOpen
            // 
            this.buttonRGBChannelOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonRGBChannelOpen.Name = "buttonRGBChannelOpen";
            this.buttonRGBChannelOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonRGBChannelOpen.TabIndex = 0;
            this.buttonRGBChannelOpen.Text = "RGB_Open";
            this.buttonRGBChannelOpen.UseVisualStyleBackColor = true;
            this.buttonRGBChannelOpen.Click += new System.EventHandler(this.buttonOpenChannel_Click);
            // 
            // hScrollBarChannel_RGB
            // 
            this.hScrollBarChannel_RGB.Location = new System.Drawing.Point(84, 3);
            this.hScrollBarChannel_RGB.Margin = new System.Windows.Forms.Padding(3);
            this.hScrollBarChannel_RGB.Name = "hScrollBarChannel_RGB";
            this.hScrollBarChannel_RGB.Size = new System.Drawing.Size(175, 23);
            this.hScrollBarChannel_RGB.TabIndex = 1;
            this.hScrollBarChannel_RGB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarSetChannelValue_Scroll);
            // 
            // comboBoxRGB
            // 
            this.comboBoxRGB.FormattingEnabled = true;
            this.comboBoxRGB.Items.AddRange(new object[] {
            "R",
            "G",
            "B",
            "RG",
            "RB",
            "GB",
            "RGB"});
            this.comboBoxRGB.Location = new System.Drawing.Point(265, 3);
            this.comboBoxRGB.Name = "comboBoxRGB";
            this.comboBoxRGB.Size = new System.Drawing.Size(57, 20);
            this.comboBoxRGB.TabIndex = 2;
            // 
            // textBoxCombiValue
            // 
            this.textBoxCombiValue.Location = new System.Drawing.Point(328, 3);
            this.textBoxCombiValue.Name = "textBoxCombiValue";
            this.textBoxCombiValue.Size = new System.Drawing.Size(57, 21);
            this.textBoxCombiValue.TabIndex = 2;
            // 
            // flp5
            // 
            this.flp5.Controls.Add(this.btnTrigWidth);
            this.flp5.Controls.Add(this.cmbRGBTrigWidth);
            this.flp5.Controls.Add(this.txtTrigWidth);
            this.flp5.Location = new System.Drawing.Point(14, 357);
            this.flp5.Name = "flp5";
            this.flp5.Size = new System.Drawing.Size(255, 28);
            this.flp5.TabIndex = 6;
            // 
            // btnTrigWidth
            // 
            this.btnTrigWidth.Location = new System.Drawing.Point(3, 3);
            this.btnTrigWidth.Name = "btnTrigWidth";
            this.btnTrigWidth.Size = new System.Drawing.Size(75, 23);
            this.btnTrigWidth.TabIndex = 0;
            this.btnTrigWidth.Text = "TrigWidth";
            this.btnTrigWidth.UseVisualStyleBackColor = true;
            this.btnTrigWidth.Click += new System.EventHandler(this.btnTrigWidth_Click);
            // 
            // txtTrigWidth
            // 
            this.txtTrigWidth.Location = new System.Drawing.Point(147, 3);
            this.txtTrigWidth.Name = "txtTrigWidth";
            this.txtTrigWidth.Size = new System.Drawing.Size(57, 21);
            this.txtTrigWidth.TabIndex = 2;
            // 
            // cmbRGBTrigWidth
            // 
            this.cmbRGBTrigWidth.FormattingEnabled = true;
            this.cmbRGBTrigWidth.Items.AddRange(new object[] {
            "R",
            "G",
            "B"});
            this.cmbRGBTrigWidth.Location = new System.Drawing.Point(84, 3);
            this.cmbRGBTrigWidth.Name = "cmbRGBTrigWidth";
            this.cmbRGBTrigWidth.Size = new System.Drawing.Size(57, 20);
            this.cmbRGBTrigWidth.TabIndex = 2;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.btnMultiTrigWidth);
            this.flowLayoutPanel5.Controls.Add(this.cmbMultiTrigWidth);
            this.flowLayoutPanel5.Controls.Add(this.txtMultiTrigWidth);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(14, 434);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(325, 28);
            this.flowLayoutPanel5.TabIndex = 6;
            // 
            // btnMultiTrigWidth
            // 
            this.btnMultiTrigWidth.Location = new System.Drawing.Point(3, 3);
            this.btnMultiTrigWidth.Name = "btnMultiTrigWidth";
            this.btnMultiTrigWidth.Size = new System.Drawing.Size(126, 23);
            this.btnMultiTrigWidth.TabIndex = 0;
            this.btnMultiTrigWidth.Text = "MultiTrigWidth";
            this.btnMultiTrigWidth.UseVisualStyleBackColor = true;
            this.btnMultiTrigWidth.Click += new System.EventHandler(this.btnMultiTrigWidth_Click);
            // 
            // txtMultiTrigWidth
            // 
            this.txtMultiTrigWidth.Location = new System.Drawing.Point(198, 3);
            this.txtMultiTrigWidth.Name = "txtMultiTrigWidth";
            this.txtMultiTrigWidth.Size = new System.Drawing.Size(57, 21);
            this.txtMultiTrigWidth.TabIndex = 2;
            // 
            // cmbMultiTrigWidth
            // 
            this.cmbMultiTrigWidth.FormattingEnabled = true;
            this.cmbMultiTrigWidth.Items.AddRange(new object[] {
            "R",
            "G",
            "B",
            "RG",
            "RB",
            "GB",
            "RGB"});
            this.cmbMultiTrigWidth.Location = new System.Drawing.Point(135, 3);
            this.cmbMultiTrigWidth.Name = "cmbMultiTrigWidth";
            this.cmbMultiTrigWidth.Size = new System.Drawing.Size(57, 20);
            this.cmbMultiTrigWidth.TabIndex = 2;
            // 
            // btnHBTrigWidth
            // 
            this.btnHBTrigWidth.Location = new System.Drawing.Point(17, 391);
            this.btnHBTrigWidth.Name = "btnHBTrigWidth";
            this.btnHBTrigWidth.Size = new System.Drawing.Size(96, 23);
            this.btnHBTrigWidth.TabIndex = 0;
            this.btnHBTrigWidth.Text = "HBTrigWidth";
            this.btnHBTrigWidth.UseVisualStyleBackColor = true;
            this.btnHBTrigWidth.Click += new System.EventHandler(this.btnHBTrigWidth_Click);
            // 
            // btnHBMultiTrigWidth
            // 
            this.btnHBMultiTrigWidth.Location = new System.Drawing.Point(17, 472);
            this.btnHBMultiTrigWidth.Name = "btnHBMultiTrigWidth";
            this.btnHBMultiTrigWidth.Size = new System.Drawing.Size(126, 23);
            this.btnHBMultiTrigWidth.TabIndex = 0;
            this.btnHBMultiTrigWidth.Text = "HBMultiTrigWidth";
            this.btnHBMultiTrigWidth.UseVisualStyleBackColor = true;
            this.btnHBMultiTrigWidth.Click += new System.EventHandler(this.btnHBMultiTrigWidth_Click);
            // 
            // chkResponse
            // 
            this.chkResponse.AutoSize = true;
            this.chkResponse.Location = new System.Drawing.Point(212, 58);
            this.chkResponse.Name = "chkResponse";
            this.chkResponse.Size = new System.Drawing.Size(72, 16);
            this.chkResponse.TabIndex = 7;
            this.chkResponse.Text = "Response";
            this.chkResponse.UseVisualStyleBackColor = true;
            this.chkResponse.CheckedChanged += new System.EventHandler(this.chkResponse_CheckedChanged);
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Location = new System.Drawing.Point(310, 58);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(60, 16);
            this.chkBackup.TabIndex = 7;
            this.chkBackup.Text = "Backup";
            this.chkBackup.UseVisualStyleBackColor = true;
            this.chkBackup.CheckedChanged += new System.EventHandler(this.chkResponse_CheckedChanged);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(118, 82);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(100, 21);
            this.txtSN.TabIndex = 8;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(118, 109);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 8;
            // 
            // txtSubnetMask
            // 
            this.txtSubnetMask.Location = new System.Drawing.Point(118, 136);
            this.txtSubnetMask.Name = "txtSubnetMask";
            this.txtSubnetMask.Size = new System.Drawing.Size(100, 21);
            this.txtSubnetMask.TabIndex = 8;
            // 
            // txtGateway
            // 
            this.txtGateway.Location = new System.Drawing.Point(118, 163);
            this.txtGateway.Name = "txtGateway";
            this.txtGateway.Size = new System.Drawing.Size(100, 21);
            this.txtGateway.TabIndex = 8;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(36, 85);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(17, 12);
            this.lbl.TabIndex = 9;
            this.lbl.Text = "SN";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(36, 112);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(17, 12);
            this.lblIP.TabIndex = 9;
            this.lblIP.Text = "IP";
            // 
            // lblSubnetMask
            // 
            this.lblSubnetMask.AutoSize = true;
            this.lblSubnetMask.Location = new System.Drawing.Point(36, 136);
            this.lblSubnetMask.Name = "lblSubnetMask";
            this.lblSubnetMask.Size = new System.Drawing.Size(65, 12);
            this.lblSubnetMask.TabIndex = 9;
            this.lblSubnetMask.Text = "SubnetMask";
            // 
            // lblGateway
            // 
            this.lblGateway.AutoSize = true;
            this.lblGateway.Location = new System.Drawing.Point(36, 163);
            this.lblGateway.Name = "lblGateway";
            this.lblGateway.Size = new System.Drawing.Size(47, 12);
            this.lblGateway.TabIndex = 9;
            this.lblGateway.Text = "Gateway";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.btnSoftTrig);
            this.flowLayoutPanel6.Controls.Add(this.cmbSoftTrig);
            this.flowLayoutPanel6.Controls.Add(this.btnMultiSoftTrig);
            this.flowLayoutPanel6.Controls.Add(this.cmbMultiSoftTrig);
            this.flowLayoutPanel6.Controls.Add(this.lblTrigTime);
            this.flowLayoutPanel6.Controls.Add(this.txtTrigTime);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(14, 511);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(458, 28);
            this.flowLayoutPanel6.TabIndex = 10;
            // 
            // btnSoftTrig
            // 
            this.btnSoftTrig.Location = new System.Drawing.Point(3, 3);
            this.btnSoftTrig.Name = "btnSoftTrig";
            this.btnSoftTrig.Size = new System.Drawing.Size(75, 23);
            this.btnSoftTrig.TabIndex = 0;
            this.btnSoftTrig.Text = "SoftTrig";
            this.btnSoftTrig.UseVisualStyleBackColor = true;
            this.btnSoftTrig.Click += new System.EventHandler(this.btnSoftTrig_Click);
            // 
            // cmbSoftTrig
            // 
            this.cmbSoftTrig.FormattingEnabled = true;
            this.cmbSoftTrig.Items.AddRange(new object[] {
            "R",
            "G",
            "B"});
            this.cmbSoftTrig.Location = new System.Drawing.Point(84, 3);
            this.cmbSoftTrig.Name = "cmbSoftTrig";
            this.cmbSoftTrig.Size = new System.Drawing.Size(57, 20);
            this.cmbSoftTrig.TabIndex = 2;
            // 
            // btnMultiSoftTrig
            // 
            this.btnMultiSoftTrig.Location = new System.Drawing.Point(147, 3);
            this.btnMultiSoftTrig.Name = "btnMultiSoftTrig";
            this.btnMultiSoftTrig.Size = new System.Drawing.Size(95, 23);
            this.btnMultiSoftTrig.TabIndex = 0;
            this.btnMultiSoftTrig.Text = "MultiSoftTrig";
            this.btnMultiSoftTrig.UseVisualStyleBackColor = true;
            this.btnMultiSoftTrig.Click += new System.EventHandler(this.btnSoftTrig_Click);
            // 
            // cmbMultiSoftTrig
            // 
            this.cmbMultiSoftTrig.FormattingEnabled = true;
            this.cmbMultiSoftTrig.Items.AddRange(new object[] {
            "R",
            "G",
            "B",
            "RG",
            "RB",
            "GB",
            "RGB"});
            this.cmbMultiSoftTrig.Location = new System.Drawing.Point(248, 3);
            this.cmbMultiSoftTrig.Name = "cmbMultiSoftTrig";
            this.cmbMultiSoftTrig.Size = new System.Drawing.Size(57, 20);
            this.cmbMultiSoftTrig.TabIndex = 2;
            // 
            // lblMaxCurrent
            // 
            this.lblMaxCurrent.Location = new System.Drawing.Point(311, 3);
            this.lblMaxCurrent.Margin = new System.Windows.Forms.Padding(3);
            this.lblMaxCurrent.Name = "lblMaxCurrent";
            this.lblMaxCurrent.Size = new System.Drawing.Size(77, 20);
            this.lblMaxCurrent.TabIndex = 3;
            this.lblMaxCurrent.Text = "MaxCurrent";
            this.lblMaxCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTrigTime
            // 
            this.txtTrigTime.Location = new System.Drawing.Point(394, 3);
            this.txtTrigTime.Name = "txtTrigTime";
            this.txtTrigTime.Size = new System.Drawing.Size(57, 21);
            this.txtTrigTime.TabIndex = 2;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.btnWorkMode);
            this.flowLayoutPanel7.Controls.Add(this.cmbWorkMode);
            this.flowLayoutPanel7.Location = new System.Drawing.Point(14, 554);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(153, 28);
            this.flowLayoutPanel7.TabIndex = 11;
            // 
            // btnWorkMode
            // 
            this.btnWorkMode.Location = new System.Drawing.Point(3, 3);
            this.btnWorkMode.Name = "btnWorkMode";
            this.btnWorkMode.Size = new System.Drawing.Size(75, 23);
            this.btnWorkMode.TabIndex = 0;
            this.btnWorkMode.Text = "WorkMode";
            this.btnWorkMode.UseVisualStyleBackColor = true;
            this.btnWorkMode.Click += new System.EventHandler(this.btnWorkMode_Click);
            // 
            // cmbWorkMode
            // 
            this.cmbWorkMode.FormattingEnabled = true;
            this.cmbWorkMode.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cmbWorkMode.Location = new System.Drawing.Point(84, 3);
            this.cmbWorkMode.Name = "cmbWorkMode";
            this.cmbWorkMode.Size = new System.Drawing.Size(57, 20);
            this.cmbWorkMode.TabIndex = 2;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.btnActivation);
            this.flowLayoutPanel8.Controls.Add(this.cmbActivation);
            this.flowLayoutPanel8.Location = new System.Drawing.Point(195, 554);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(175, 28);
            this.flowLayoutPanel8.TabIndex = 11;
            // 
            // btnActivation
            // 
            this.btnActivation.Location = new System.Drawing.Point(3, 3);
            this.btnActivation.Name = "btnActivation";
            this.btnActivation.Size = new System.Drawing.Size(98, 23);
            this.btnActivation.TabIndex = 0;
            this.btnActivation.Text = "Activation";
            this.btnActivation.UseVisualStyleBackColor = true;
            this.btnActivation.Click += new System.EventHandler(this.btnActivation_Click);
            // 
            // cmbActivation
            // 
            this.cmbActivation.FormattingEnabled = true;
            this.cmbActivation.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cmbActivation.Location = new System.Drawing.Point(107, 3);
            this.cmbActivation.Name = "cmbActivation";
            this.cmbActivation.Size = new System.Drawing.Size(57, 20);
            this.cmbActivation.TabIndex = 2;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Controls.Add(this.btnMaxCurrent);
            this.flowLayoutPanel9.Controls.Add(this.cmbMaxCurrent);
            this.flowLayoutPanel9.Controls.Add(this.btnMultiMaxCurrent);
            this.flowLayoutPanel9.Controls.Add(this.cmbMultiMaxCurrent);
            this.flowLayoutPanel9.Controls.Add(this.lblMaxCurrent);
            this.flowLayoutPanel9.Controls.Add(this.txtMaxCurrent);
            this.flowLayoutPanel9.Location = new System.Drawing.Point(14, 595);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(458, 28);
            this.flowLayoutPanel9.TabIndex = 12;
            // 
            // cmbMaxCurrent
            // 
            this.cmbMaxCurrent.FormattingEnabled = true;
            this.cmbMaxCurrent.Items.AddRange(new object[] {
            "R",
            "G",
            "B"});
            this.cmbMaxCurrent.Location = new System.Drawing.Point(84, 3);
            this.cmbMaxCurrent.Name = "cmbMaxCurrent";
            this.cmbMaxCurrent.Size = new System.Drawing.Size(57, 20);
            this.cmbMaxCurrent.TabIndex = 2;
            // 
            // btnMaxCurrent
            // 
            this.btnMaxCurrent.Location = new System.Drawing.Point(3, 3);
            this.btnMaxCurrent.Name = "btnMaxCurrent";
            this.btnMaxCurrent.Size = new System.Drawing.Size(75, 23);
            this.btnMaxCurrent.TabIndex = 0;
            this.btnMaxCurrent.Text = "MaxCurrent";
            this.btnMaxCurrent.UseVisualStyleBackColor = true;
            this.btnMaxCurrent.Click += new System.EventHandler(this.btnMaxCurrent_Click);
            // 
            // txtMaxCurrent
            // 
            this.txtMaxCurrent.Location = new System.Drawing.Point(394, 3);
            this.txtMaxCurrent.Name = "txtMaxCurrent";
            this.txtMaxCurrent.Size = new System.Drawing.Size(57, 21);
            this.txtMaxCurrent.TabIndex = 2;
            // 
            // btnMultiMaxCurrent
            // 
            this.btnMultiMaxCurrent.Location = new System.Drawing.Point(147, 3);
            this.btnMultiMaxCurrent.Name = "btnMultiMaxCurrent";
            this.btnMultiMaxCurrent.Size = new System.Drawing.Size(95, 23);
            this.btnMultiMaxCurrent.TabIndex = 0;
            this.btnMultiMaxCurrent.Text = "MultiMaxCurrent";
            this.btnMultiMaxCurrent.UseVisualStyleBackColor = true;
            this.btnMultiMaxCurrent.Click += new System.EventHandler(this.btnMaxCurrent_Click);
            // 
            // lblTrigTime
            // 
            this.lblTrigTime.Location = new System.Drawing.Point(311, 3);
            this.lblTrigTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblTrigTime.Name = "lblTrigTime";
            this.lblTrigTime.Size = new System.Drawing.Size(77, 20);
            this.lblTrigTime.TabIndex = 3;
            this.lblTrigTime.Text = "TrigTime(ms)";
            this.lblTrigTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbMultiMaxCurrent
            // 
            this.cmbMultiMaxCurrent.FormattingEnabled = true;
            this.cmbMultiMaxCurrent.Items.AddRange(new object[] {
            "R",
            "G",
            "B",
            "RG",
            "RB",
            "GB",
            "RGB"});
            this.cmbMultiMaxCurrent.Location = new System.Drawing.Point(248, 3);
            this.cmbMultiMaxCurrent.Name = "cmbMultiMaxCurrent";
            this.cmbMultiMaxCurrent.Size = new System.Drawing.Size(57, 20);
            this.cmbMultiMaxCurrent.TabIndex = 2;
            // 
            // FormLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 635);
            this.Controls.Add(this.flowLayoutPanel9);
            this.Controls.Add(this.flowLayoutPanel8);
            this.Controls.Add(this.flowLayoutPanel7);
            this.Controls.Add(this.flowLayoutPanel6);
            this.Controls.Add(this.lblGateway);
            this.Controls.Add(this.lblSubnetMask);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.txtGateway);
            this.Controls.Add(this.txtSubnetMask);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.chkResponse);
            this.Controls.Add(this.btnHBMultiTrigWidth);
            this.Controls.Add(this.btnHBTrigWidth);
            this.Controls.Add(this.flowLayoutPanel5);
            this.Controls.Add(this.flp5);
            this.Controls.Add(this.flpSingleChannel);
            this.Controls.Add(this.labelConnectState);
            this.Controls.Add(this.richTextBoxState);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLight";
            this.Text = "FormLight";
            this.flpSingleChannel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flp5.ResumeLayout(false);
            this.flp5.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.RichTextBox richTextBoxState;
        private System.Windows.Forms.Label labelConnectState;
        private System.Windows.Forms.FlowLayoutPanel flpSingleChannel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonRChannelOpen;
        private System.Windows.Forms.HScrollBar hScrollBarChannel_R;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonGChannelOpen;
        private System.Windows.Forms.HScrollBar hScrollBarChannel_G;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonBChannelOpen;
        private System.Windows.Forms.HScrollBar hScrollBarChannel_B;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button buttonRGBChannelOpen;
        private System.Windows.Forms.HScrollBar hScrollBarChannel_RGB;
        private System.Windows.Forms.ComboBox comboBoxRGB;
        private System.Windows.Forms.TextBox textBoxR_Value;
        private System.Windows.Forms.TextBox textBoxG_Value;
        private System.Windows.Forms.TextBox textBoxB_Value;
        private System.Windows.Forms.TextBox textBoxCombiValue;
        private System.Windows.Forms.FlowLayoutPanel flp5;
        private System.Windows.Forms.Button btnTrigWidth;
        private System.Windows.Forms.TextBox txtTrigWidth;
        private System.Windows.Forms.ComboBox cmbRGBTrigWidth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Button btnMultiTrigWidth;
        private System.Windows.Forms.ComboBox cmbMultiTrigWidth;
        private System.Windows.Forms.TextBox txtMultiTrigWidth;
        private System.Windows.Forms.Button btnHBTrigWidth;
        private System.Windows.Forms.Button btnHBMultiTrigWidth;
        private System.Windows.Forms.CheckBox chkResponse;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtSubnetMask;
        private System.Windows.Forms.TextBox txtGateway;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblSubnetMask;
        private System.Windows.Forms.Label lblGateway;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Button btnSoftTrig;
        private System.Windows.Forms.ComboBox cmbSoftTrig;
        private System.Windows.Forms.Button btnMultiSoftTrig;
        private System.Windows.Forms.ComboBox cmbMultiSoftTrig;
        private System.Windows.Forms.Label lblMaxCurrent;
        private System.Windows.Forms.TextBox txtTrigTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Button btnWorkMode;
        private System.Windows.Forms.ComboBox cmbWorkMode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.Button btnActivation;
        private System.Windows.Forms.ComboBox cmbActivation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.Button btnMaxCurrent;
        private System.Windows.Forms.ComboBox cmbMaxCurrent;
        private System.Windows.Forms.TextBox txtMaxCurrent;
        private System.Windows.Forms.Label lblTrigTime;
        private System.Windows.Forms.Button btnMultiMaxCurrent;
        private System.Windows.Forms.ComboBox cmbMultiMaxCurrent;
    }
}