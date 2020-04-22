namespace Anda.Fluid.App.Metro.Pages
{
    partial class PageSetupSensors
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
            this.gbxSensor = new System.Windows.Forms.GroupBox();
            this.gbxCom = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.cbxCom = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHeight = new MetroSet_UI.Controls.MetroSetButton();
            this.btnScale = new MetroSet_UI.Controls.MetroSetButton();
            this.btnHeater = new MetroSet_UI.Controls.MetroSetButton();
            this.btnPropor = new MetroSet_UI.Controls.MetroSetButton();
            this.btnGage = new MetroSet_UI.Controls.MetroSetButton();
            this.btnScanner = new MetroSet_UI.Controls.MetroSetButton();
            this.btnSave = new MetroSet_UI.Controls.MetroSetButton();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.gbxCom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSensor
            // 
            this.gbxSensor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxSensor.ForeColor = System.Drawing.Color.Black;
            this.gbxSensor.Location = new System.Drawing.Point(15, 271);
            this.gbxSensor.Name = "gbxSensor";
            this.gbxSensor.Size = new System.Drawing.Size(570, 293);
            this.gbxSensor.TabIndex = 3;
            this.gbxSensor.TabStop = false;
            // 
            // gbxCom
            // 
            this.gbxCom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxCom.Controls.Add(this.btnDisconnect);
            this.gbxCom.Controls.Add(this.btnConnect);
            this.gbxCom.Controls.Add(this.btnSend);
            this.gbxCom.Controls.Add(this.txtReceive);
            this.gbxCom.Controls.Add(this.txtSend);
            this.gbxCom.Controls.Add(this.cbxStopBits);
            this.gbxCom.Controls.Add(this.cbxDataBits);
            this.gbxCom.Controls.Add(this.cbxParity);
            this.gbxCom.Controls.Add(this.cbxBaudRate);
            this.gbxCom.Controls.Add(this.cbxCom);
            this.gbxCom.Controls.Add(this.label5);
            this.gbxCom.Controls.Add(this.label4);
            this.gbxCom.Controls.Add(this.label3);
            this.gbxCom.Controls.Add(this.label2);
            this.gbxCom.Controls.Add(this.label1);
            this.gbxCom.ForeColor = System.Drawing.Color.Black;
            this.gbxCom.Location = new System.Drawing.Point(15, 50);
            this.gbxCom.Name = "gbxCom";
            this.gbxCom.Size = new System.Drawing.Size(570, 215);
            this.gbxCom.TabIndex = 2;
            this.gbxCom.TabStop = false;
            this.gbxCom.Text = "串口设置";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(285, 94);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 29);
            this.btnDisconnect.TabIndex = 14;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(204, 94);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 29);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(489, 94);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 29);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceive.Location = new System.Drawing.Point(204, 129);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(360, 74);
            this.txtReceive.TabIndex = 11;
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(204, 21);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(360, 67);
            this.txtSend.TabIndex = 10;
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(84, 158);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(97, 25);
            this.cbxStopBits.TabIndex = 9;
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(84, 126);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(97, 25);
            this.cbxDataBits.TabIndex = 8;
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(84, 94);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(97, 25);
            this.cbxParity.TabIndex = 7;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(84, 62);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(97, 25);
            this.cbxBaudRate.TabIndex = 6;
            // 
            // cbxCom
            // 
            this.cbxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCom.FormattingEnabled = true;
            this.cbxCom.Location = new System.Drawing.Point(84, 30);
            this.cbxCom.Name = "cbxCom";
            this.cbxCom.Size = new System.Drawing.Size(97, 25);
            this.cbxCom.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "校验位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // btnHeight
            // 
            this.btnHeight.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnHeight.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnHeight.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnHeight.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnHeight.HoverTextColor = System.Drawing.Color.White;
            this.btnHeight.Location = new System.Drawing.Point(15, 15);
            this.btnHeight.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnHeight.Name = "btnHeight";
            this.btnHeight.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnHeight.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnHeight.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnHeight.PressTextColor = System.Drawing.Color.White;
            this.btnHeight.Selected = false;
            this.btnHeight.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeight.SelectedTextColor = System.Drawing.Color.White;
            this.btnHeight.ShowBorder = true;
            this.btnHeight.Size = new System.Drawing.Size(75, 23);
            this.btnHeight.Style = MetroSet_UI.Design.Style.Dark;
            this.btnHeight.StyleManager = this.styleManager1;
            this.btnHeight.TabIndex = 4;
            this.btnHeight.Text = "测高";
            this.btnHeight.ThemeAuthor = "Narwin";
            this.btnHeight.ThemeName = "MetroDark";
            this.btnHeight.Click += new System.EventHandler(this.btnHeight_Click);
            // 
            // btnScale
            // 
            this.btnScale.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnScale.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnScale.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnScale.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnScale.HoverTextColor = System.Drawing.Color.White;
            this.btnScale.Location = new System.Drawing.Point(96, 15);
            this.btnScale.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnScale.Name = "btnScale";
            this.btnScale.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnScale.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnScale.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnScale.PressTextColor = System.Drawing.Color.White;
            this.btnScale.Selected = false;
            this.btnScale.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScale.SelectedTextColor = System.Drawing.Color.White;
            this.btnScale.ShowBorder = true;
            this.btnScale.Size = new System.Drawing.Size(75, 23);
            this.btnScale.Style = MetroSet_UI.Design.Style.Dark;
            this.btnScale.StyleManager = this.styleManager1;
            this.btnScale.TabIndex = 5;
            this.btnScale.Text = "称重";
            this.btnScale.ThemeAuthor = "Narwin";
            this.btnScale.ThemeName = "MetroDark";
            this.btnScale.Click += new System.EventHandler(this.btnScale_Click);
            // 
            // btnHeater
            // 
            this.btnHeater.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnHeater.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnHeater.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnHeater.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnHeater.HoverTextColor = System.Drawing.Color.White;
            this.btnHeater.Location = new System.Drawing.Point(177, 15);
            this.btnHeater.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnHeater.Name = "btnHeater";
            this.btnHeater.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnHeater.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnHeater.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnHeater.PressTextColor = System.Drawing.Color.White;
            this.btnHeater.Selected = false;
            this.btnHeater.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnHeater.SelectedTextColor = System.Drawing.Color.White;
            this.btnHeater.ShowBorder = true;
            this.btnHeater.Size = new System.Drawing.Size(75, 23);
            this.btnHeater.Style = MetroSet_UI.Design.Style.Dark;
            this.btnHeater.StyleManager = this.styleManager1;
            this.btnHeater.TabIndex = 6;
            this.btnHeater.Text = "加热";
            this.btnHeater.ThemeAuthor = "Narwin";
            this.btnHeater.ThemeName = "MetroDark";
            this.btnHeater.Click += new System.EventHandler(this.btnHeater_Click);
            // 
            // btnPropor
            // 
            this.btnPropor.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnPropor.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnPropor.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnPropor.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnPropor.HoverTextColor = System.Drawing.Color.White;
            this.btnPropor.Location = new System.Drawing.Point(258, 15);
            this.btnPropor.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnPropor.Name = "btnPropor";
            this.btnPropor.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnPropor.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnPropor.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnPropor.PressTextColor = System.Drawing.Color.White;
            this.btnPropor.Selected = false;
            this.btnPropor.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnPropor.SelectedTextColor = System.Drawing.Color.White;
            this.btnPropor.ShowBorder = true;
            this.btnPropor.Size = new System.Drawing.Size(75, 23);
            this.btnPropor.Style = MetroSet_UI.Design.Style.Dark;
            this.btnPropor.StyleManager = this.styleManager1;
            this.btnPropor.TabIndex = 7;
            this.btnPropor.Text = "比例阀";
            this.btnPropor.ThemeAuthor = "Narwin";
            this.btnPropor.ThemeName = "MetroDark";
            this.btnPropor.Click += new System.EventHandler(this.btnPropor_Click);
            // 
            // btnGage
            // 
            this.btnGage.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnGage.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnGage.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnGage.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnGage.HoverTextColor = System.Drawing.Color.White;
            this.btnGage.Location = new System.Drawing.Point(339, 15);
            this.btnGage.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnGage.Name = "btnGage";
            this.btnGage.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnGage.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnGage.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnGage.PressTextColor = System.Drawing.Color.White;
            this.btnGage.Selected = false;
            this.btnGage.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnGage.SelectedTextColor = System.Drawing.Color.White;
            this.btnGage.ShowBorder = true;
            this.btnGage.Size = new System.Drawing.Size(75, 23);
            this.btnGage.Style = MetroSet_UI.Design.Style.Dark;
            this.btnGage.StyleManager = this.styleManager1;
            this.btnGage.TabIndex = 8;
            this.btnGage.Text = "高度规";
            this.btnGage.ThemeAuthor = "Narwin";
            this.btnGage.ThemeName = "MetroDark";
            this.btnGage.Click += new System.EventHandler(this.btnGage_Click);
            // 
            // btnScanner
            // 
            this.btnScanner.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnScanner.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnScanner.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnScanner.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnScanner.HoverTextColor = System.Drawing.Color.White;
            this.btnScanner.Location = new System.Drawing.Point(420, 15);
            this.btnScanner.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnScanner.Name = "btnScanner";
            this.btnScanner.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnScanner.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnScanner.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnScanner.PressTextColor = System.Drawing.Color.White;
            this.btnScanner.Selected = false;
            this.btnScanner.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnScanner.SelectedTextColor = System.Drawing.Color.White;
            this.btnScanner.ShowBorder = true;
            this.btnScanner.Size = new System.Drawing.Size(75, 23);
            this.btnScanner.Style = MetroSet_UI.Design.Style.Dark;
            this.btnScanner.StyleManager = this.styleManager1;
            this.btnScanner.TabIndex = 9;
            this.btnScanner.Text = "条码枪";
            this.btnScanner.ThemeAuthor = "Narwin";
            this.btnScanner.ThemeName = "MetroDark";
            this.btnScanner.Click += new System.EventHandler(this.btnScanner_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnSave.HoverTextColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(510, 570);
            this.btnSave.Mode = MetroSet_UI.Enums.ButtonMode.Normal;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.NormalTextColor = System.Drawing.Color.LightGray;
            this.btnSave.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSave.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnSave.PressTextColor = System.Drawing.Color.White;
            this.btnSave.Selected = false;
            this.btnSave.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnSave.SelectedTextColor = System.Drawing.Color.White;
            this.btnSave.ShowBorder = true;
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = MetroSet_UI.Design.Style.Dark;
            this.btnSave.StyleManager = this.styleManager1;
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.ThemeAuthor = "Narwin";
            this.btnSave.ThemeName = "MetroDark";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // PageSetupSensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnScanner);
            this.Controls.Add(this.btnGage);
            this.Controls.Add(this.btnPropor);
            this.Controls.Add(this.btnHeater);
            this.Controls.Add(this.btnScale);
            this.Controls.Add(this.btnHeight);
            this.Controls.Add(this.gbxSensor);
            this.Controls.Add(this.gbxCom);
            this.Name = "PageSetupSensors";
            this.Size = new System.Drawing.Size(600, 608);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.gbxCom.ResumeLayout(false);
            this.gbxCom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxSensor;
        private System.Windows.Forms.GroupBox gbxCom;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.ComboBox cbxDataBits;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ComboBox cbxCom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MetroSet_UI.Controls.MetroSetButton btnGage;
        private MetroSet_UI.Controls.MetroSetButton btnPropor;
        private MetroSet_UI.Controls.MetroSetButton btnHeater;
        private MetroSet_UI.Controls.MetroSetButton btnScale;
        private MetroSet_UI.Controls.MetroSetButton btnHeight;
        private MetroSet_UI.Controls.MetroSetButton btnScanner;
        private MetroSet_UI.Controls.MetroSetButton btnSave;
        private MetroSet_UI.StyleManager styleManager1;
    }
}
