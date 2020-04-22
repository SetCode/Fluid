namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditLineMetro
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
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.cbInspectionKey = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGotoOffset = new System.Windows.Forms.Button();
            this.btnOffset = new System.Windows.Forms.Button();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.btnGoToEnd = new System.Windows.Forms.Button();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnSelectEnd = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // cbInspectionKey
            // 
            this.cbInspectionKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbInspectionKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInspectionKey.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInspectionKey.ForeColor = System.Drawing.Color.White;
            this.cbInspectionKey.FormattingEnabled = true;
            this.cbInspectionKey.Location = new System.Drawing.Point(122, 228);
            this.cbInspectionKey.Name = "cbInspectionKey";
            this.cbInspectionKey.Size = new System.Drawing.Size(121, 22);
            this.cbInspectionKey.TabIndex = 89;
            this.cbInspectionKey.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(15, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 14);
            this.label7.TabIndex = 88;
            this.label7.Text = "InspectionKey:";
            // 
            // btnGotoOffset
            // 
            this.btnGotoOffset.BackColor = System.Drawing.SystemColors.Control;
            this.btnGotoOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoOffset.ForeColor = System.Drawing.Color.Black;
            this.btnGotoOffset.Location = new System.Drawing.Point(291, 185);
            this.btnGotoOffset.Name = "btnGotoOffset";
            this.btnGotoOffset.Size = new System.Drawing.Size(75, 23);
            this.btnGotoOffset.TabIndex = 87;
            this.btnGotoOffset.Text = "移动";
            this.btnGotoOffset.UseVisualStyleBackColor = false;
            this.btnGotoOffset.Click += new System.EventHandler(this.btnGotoOffset_Click);
            // 
            // btnOffset
            // 
            this.btnOffset.BackColor = System.Drawing.SystemColors.Control;
            this.btnOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffset.ForeColor = System.Drawing.Color.Black;
            this.btnOffset.Location = new System.Drawing.Point(210, 184);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(75, 23);
            this.btnOffset.TabIndex = 86;
            this.btnOffset.Text = "示教";
            this.btnOffset.UseVisualStyleBackColor = false;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // nudOffset
            // 
            this.nudOffset.BackColor = System.Drawing.SystemColors.Window;
            this.nudOffset.DecimalPlaces = 3;
            this.nudOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOffset.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nudOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOffset.Location = new System.Drawing.Point(65, 183);
            this.nudOffset.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOffset.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudOffset.Name = "nudOffset";
            this.nudOffset.Size = new System.Drawing.Size(106, 22);
            this.nudOffset.TabIndex = 85;
            this.nudOffset.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(173, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 84;
            this.label6.Text = "mm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 83;
            this.label2.Text = "Offset";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 81;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(231, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 14);
            this.label3.TabIndex = 80;
            this.label3.Text = "mg";
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbWeight.Location = new System.Drawing.Point(149, 142);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(76, 22);
            this.tbWeight.TabIndex = 79;
            this.tbWeight.Text = "0.000";
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbWeightControl.Location = new System.Drawing.Point(18, 144);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 78;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = false;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditLineParams.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineParams.ForeColor = System.Drawing.Color.Black;
            this.btnEditLineParams.Location = new System.Drawing.Point(178, 102);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditLineParams.TabIndex = 77;
            this.btnEditLineParams.Text = "编辑";
            this.btnEditLineParams.UseVisualStyleBackColor = false;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 76;
            this.label1.Text = "Line Style:";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLineType.ForeColor = System.Drawing.Color.White;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(99, 103);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(73, 22);
            this.comboBoxLineType.TabIndex = 75;
            // 
            // btnGoToEnd
            // 
            this.btnGoToEnd.BackColor = System.Drawing.SystemColors.Control;
            this.btnGoToEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToEnd.ForeColor = System.Drawing.Color.Black;
            this.btnGoToEnd.Location = new System.Drawing.Point(303, 52);
            this.btnGoToEnd.Name = "btnGoToEnd";
            this.btnGoToEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToEnd.TabIndex = 73;
            this.btnGoToEnd.Text = "移动";
            this.btnGoToEnd.UseVisualStyleBackColor = false;
            this.btnGoToEnd.Click += new System.EventHandler(this.btnGoToEnd_Click);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnGotoStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoStart.ForeColor = System.Drawing.Color.Black;
            this.btnGotoStart.Location = new System.Drawing.Point(303, 20);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 74;
            this.btnGotoStart.Text = "移动";
            this.btnGotoStart.UseVisualStyleBackColor = false;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoStart_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelectEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectEnd.ForeColor = System.Drawing.Color.Black;
            this.btnSelectEnd.Location = new System.Drawing.Point(222, 52);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 71;
            this.btnSelectEnd.Text = "示教";
            this.btnSelectEnd.UseVisualStyleBackColor = false;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelectStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectStart.ForeColor = System.Drawing.Color.Black;
            this.btnSelectStart.Location = new System.Drawing.Point(222, 20);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 72;
            this.btnSelectStart.Text = "示教";
            this.btnSelectStart.UseVisualStyleBackColor = false;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.Color.White;
            this.tbEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbEndY.Location = new System.Drawing.Point(144, 53);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(72, 22);
            this.tbEndY.TabIndex = 67;
            this.tbEndY.Text = "0.000";
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.Color.White;
            this.tbStartY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbStartY.Location = new System.Drawing.Point(144, 21);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(72, 22);
            this.tbStartY.TabIndex = 68;
            this.tbStartY.Text = "0.000";
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.Color.White;
            this.tbEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbEndX.Location = new System.Drawing.Point(66, 53);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(72, 22);
            this.tbEndX.TabIndex = 69;
            this.tbEndX.Text = "0.000";
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.Color.White;
            this.tbStartX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbStartX.Location = new System.Drawing.Point(66, 21);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(72, 22);
            this.tbStartX.TabIndex = 70;
            this.tbStartX.Text = "0.000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(15, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 14);
            this.label5.TabIndex = 65;
            this.label5.Text = "End:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 66;
            this.label4.Text = "Start:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 90;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditLineMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbInspectionKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGotoOffset);
            this.Controls.Add(this.btnOffset);
            this.Controls.Add(this.nudOffset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.cbWeightControl);
            this.Controls.Add(this.btnEditLineParams);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxLineType);
            this.Controls.Add(this.btnGoToEnd);
            this.Controls.Add(this.btnGotoStart);
            this.Controls.Add(this.btnSelectEnd);
            this.Controls.Add(this.btnSelectStart);
            this.Controls.Add(this.tbEndY);
            this.Controls.Add(this.tbStartY);
            this.Controls.Add(this.tbEndX);
            this.Controls.Add(this.tbStartX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "EditLineMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.ComboBox cbInspectionKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGotoOffset;
        private System.Windows.Forms.Button btnOffset;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnEditLineParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button btnGoToEnd;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnSelectEnd;
        private System.Windows.Forms.Button btnSelectStart;
        private Controls.DoubleTextBox tbEndY;
        private Controls.DoubleTextBox tbStartY;
        private Controls.DoubleTextBox tbEndX;
        private Controls.DoubleTextBox tbStartX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
    }
}
