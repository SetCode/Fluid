namespace Anda.Fluid.Domain.Sensors
{
    partial class SettingSensorsForm
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
            this.gbxSensor = new System.Windows.Forms.GroupBox();
            this.gbxContent.SuspendLayout();
            this.gbxCom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.gbxSensor);
            this.gbxContent.Controls.Add(this.gbxCom);
            // 
            // tvwList
            // 
            this.tvwList.LineColor = System.Drawing.Color.Black;
            // 
            // gbxCom
            // 
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
            this.gbxCom.Location = new System.Drawing.Point(5, 20);
            this.gbxCom.Name = "gbxCom";
            this.gbxCom.Size = new System.Drawing.Size(514, 236);
            this.gbxCom.TabIndex = 0;
            this.gbxCom.TabStop = false;
            this.gbxCom.Text = "串口设置";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(285, 94);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 14;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(204, 94);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(433, 94);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceive.Location = new System.Drawing.Point(204, 123);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(304, 74);
            this.txtReceive.TabIndex = 11;
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(204, 21);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(304, 67);
            this.txtSend.TabIndex = 10;
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(84, 158);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(97, 22);
            this.cbxStopBits.TabIndex = 9;
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(84, 126);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(97, 22);
            this.cbxDataBits.TabIndex = 8;
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(84, 94);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(97, 22);
            this.cbxParity.TabIndex = 7;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(84, 62);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(97, 22);
            this.cbxBaudRate.TabIndex = 6;
            // 
            // cbxCom
            // 
            this.cbxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCom.FormattingEnabled = true;
            this.cbxCom.Location = new System.Drawing.Point(84, 30);
            this.cbxCom.Name = "cbxCom";
            this.cbxCom.Size = new System.Drawing.Size(97, 22);
            this.cbxCom.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "校验位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // gbxSensor
            // 
            this.gbxSensor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxSensor.Location = new System.Drawing.Point(5, 262);
            this.gbxSensor.Name = "gbxSensor";
            this.gbxSensor.Size = new System.Drawing.Size(514, 246);
            this.gbxSensor.TabIndex = 1;
            this.gbxSensor.TabStop = false;
            // 
            // SettingSensorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Name = "SettingSensorsForm";
            this.Text = "SettingSensorsForm";
            this.gbxContent.ResumeLayout(false);
            this.gbxCom.ResumeLayout(false);
            this.gbxCom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxCom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.ComboBox cbxDataBits;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ComboBox cbxCom;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbxSensor;
    }
}