namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditCircleMetro
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
            this.comboBoxDegree = new System.Windows.Forms.ComboBox();
            this.btnCenterGoTo = new System.Windows.Forms.Button();
            this.btnSelectCenter = new System.Windows.Forms.Button();
            this.btnEndGoTo = new System.Windows.Forms.Button();
            this.btnSelectEnd = new System.Windows.Forms.Button();
            this.btnMiddleGoTo = new System.Windows.Forms.Button();
            this.tbCenterY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelectMiddle = new System.Windows.Forms.Button();
            this.tbEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnStartGoTo = new System.Windows.Forms.Button();
            this.tbCenterX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbMiddleY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMiddleX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStartY = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStartX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.btnEditLineParams = new System.Windows.Forms.Button();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCenter = new System.Windows.Forms.RadioButton();
            this.rbSME = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            // comboBoxDegree
            // 
            this.comboBoxDegree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDegree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDegree.ForeColor = System.Drawing.Color.White;
            this.comboBoxDegree.FormattingEnabled = true;
            this.comboBoxDegree.Items.AddRange(new object[] {
            "360",
            "-360"});
            this.comboBoxDegree.Location = new System.Drawing.Point(69, 215);
            this.comboBoxDegree.Name = "comboBoxDegree";
            this.comboBoxDegree.Size = new System.Drawing.Size(66, 22);
            this.comboBoxDegree.TabIndex = 46;
            // 
            // btnCenterGoTo
            // 
            this.btnCenterGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCenterGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnCenterGoTo.Location = new System.Drawing.Point(291, 181);
            this.btnCenterGoTo.Name = "btnCenterGoTo";
            this.btnCenterGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnCenterGoTo.TabIndex = 45;
            this.btnCenterGoTo.Text = "移动";
            this.btnCenterGoTo.UseVisualStyleBackColor = true;
            this.btnCenterGoTo.Click += new System.EventHandler(this.btnCenterGoTo_Click);
            // 
            // btnSelectCenter
            // 
            this.btnSelectCenter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCenter.ForeColor = System.Drawing.Color.Black;
            this.btnSelectCenter.Location = new System.Drawing.Point(210, 181);
            this.btnSelectCenter.Name = "btnSelectCenter";
            this.btnSelectCenter.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCenter.TabIndex = 43;
            this.btnSelectCenter.Text = "示教";
            this.btnSelectCenter.UseVisualStyleBackColor = true;
            this.btnSelectCenter.Click += new System.EventHandler(this.btnSelectCenter_Click);
            // 
            // btnEndGoTo
            // 
            this.btnEndGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnEndGoTo.Location = new System.Drawing.Point(291, 148);
            this.btnEndGoTo.Name = "btnEndGoTo";
            this.btnEndGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnEndGoTo.TabIndex = 42;
            this.btnEndGoTo.Text = "移动";
            this.btnEndGoTo.UseVisualStyleBackColor = true;
            this.btnEndGoTo.Click += new System.EventHandler(this.btnEndGoTo_Click);
            // 
            // btnSelectEnd
            // 
            this.btnSelectEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectEnd.ForeColor = System.Drawing.Color.Black;
            this.btnSelectEnd.Location = new System.Drawing.Point(210, 148);
            this.btnSelectEnd.Name = "btnSelectEnd";
            this.btnSelectEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectEnd.TabIndex = 41;
            this.btnSelectEnd.Text = "示教";
            this.btnSelectEnd.UseVisualStyleBackColor = true;
            this.btnSelectEnd.Click += new System.EventHandler(this.btnSelectEnd_Click);
            // 
            // btnMiddleGoTo
            // 
            this.btnMiddleGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMiddleGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnMiddleGoTo.Location = new System.Drawing.Point(291, 114);
            this.btnMiddleGoTo.Name = "btnMiddleGoTo";
            this.btnMiddleGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnMiddleGoTo.TabIndex = 40;
            this.btnMiddleGoTo.Text = "移动";
            this.btnMiddleGoTo.UseVisualStyleBackColor = true;
            this.btnMiddleGoTo.Click += new System.EventHandler(this.btnMiddleGoTo_Click);
            // 
            // tbCenterY
            // 
            this.tbCenterY.BackColor = System.Drawing.SystemColors.Window;
            this.tbCenterY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCenterY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbCenterY.Location = new System.Drawing.Point(138, 183);
            this.tbCenterY.Name = "tbCenterY";
            this.tbCenterY.Size = new System.Drawing.Size(66, 22);
            this.tbCenterY.TabIndex = 37;
            this.tbCenterY.TextChanged += new System.EventHandler(this.tbCenterY_TextChanged);
            // 
            // btnSelectMiddle
            // 
            this.btnSelectMiddle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectMiddle.ForeColor = System.Drawing.Color.Black;
            this.btnSelectMiddle.Location = new System.Drawing.Point(210, 114);
            this.btnSelectMiddle.Name = "btnSelectMiddle";
            this.btnSelectMiddle.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMiddle.TabIndex = 38;
            this.btnSelectMiddle.Text = "示教";
            this.btnSelectMiddle.UseVisualStyleBackColor = true;
            this.btnSelectMiddle.Click += new System.EventHandler(this.btnSelectMiddle_Click);
            // 
            // tbEndY
            // 
            this.tbEndY.BackColor = System.Drawing.SystemColors.Window;
            this.tbEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbEndY.Location = new System.Drawing.Point(138, 150);
            this.tbEndY.Name = "tbEndY";
            this.tbEndY.Size = new System.Drawing.Size(66, 22);
            this.tbEndY.TabIndex = 36;
            this.tbEndY.TextChanged += new System.EventHandler(this.tbEndY_TextChanged);
            // 
            // btnStartGoTo
            // 
            this.btnStartGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnStartGoTo.Location = new System.Drawing.Point(291, 80);
            this.btnStartGoTo.Name = "btnStartGoTo";
            this.btnStartGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnStartGoTo.TabIndex = 44;
            this.btnStartGoTo.Text = "移动";
            this.btnStartGoTo.UseVisualStyleBackColor = true;
            this.btnStartGoTo.Click += new System.EventHandler(this.btnStartGoTo_Click);
            // 
            // tbCenterX
            // 
            this.tbCenterX.BackColor = System.Drawing.SystemColors.Window;
            this.tbCenterX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCenterX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbCenterX.Location = new System.Drawing.Point(66, 183);
            this.tbCenterX.Name = "tbCenterX";
            this.tbCenterX.Size = new System.Drawing.Size(66, 22);
            this.tbCenterX.TabIndex = 35;
            this.tbCenterX.TextChanged += new System.EventHandler(this.tbCenterX_TextChanged);
            // 
            // tbMiddleY
            // 
            this.tbMiddleY.BackColor = System.Drawing.SystemColors.Window;
            this.tbMiddleY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMiddleY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbMiddleY.Location = new System.Drawing.Point(138, 116);
            this.tbMiddleY.Name = "tbMiddleY";
            this.tbMiddleY.Size = new System.Drawing.Size(66, 22);
            this.tbMiddleY.TabIndex = 33;
            this.tbMiddleY.TextChanged += new System.EventHandler(this.tbMiddleY_TextChanged);
            // 
            // tbEndX
            // 
            this.tbEndX.BackColor = System.Drawing.SystemColors.Window;
            this.tbEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbEndX.Location = new System.Drawing.Point(66, 150);
            this.tbEndX.Name = "tbEndX";
            this.tbEndX.Size = new System.Drawing.Size(66, 22);
            this.tbEndX.TabIndex = 32;
            this.tbEndX.TextChanged += new System.EventHandler(this.tbEndX_TextChanged);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectStart.ForeColor = System.Drawing.Color.Black;
            this.btnSelectStart.Location = new System.Drawing.Point(210, 80);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 39;
            this.btnSelectStart.Text = "示教";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnSelectStart_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 14);
            this.label5.TabIndex = 29;
            this.label5.Text = "Degree:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 28;
            this.label4.Text = "Center:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbMiddleX
            // 
            this.tbMiddleX.BackColor = System.Drawing.SystemColors.Window;
            this.tbMiddleX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMiddleX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbMiddleX.Location = new System.Drawing.Point(66, 116);
            this.tbMiddleX.Name = "tbMiddleX";
            this.tbMiddleX.Size = new System.Drawing.Size(66, 22);
            this.tbMiddleX.TabIndex = 31;
            this.tbMiddleX.TextChanged += new System.EventHandler(this.tbMiddleX_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 27;
            this.label3.Text = "End:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbStartY
            // 
            this.tbStartY.BackColor = System.Drawing.SystemColors.Window;
            this.tbStartY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbStartY.Location = new System.Drawing.Point(138, 82);
            this.tbStartY.Name = "tbStartY";
            this.tbStartY.Size = new System.Drawing.Size(66, 22);
            this.tbStartY.TabIndex = 30;
            this.tbStartY.TextChanged += new System.EventHandler(this.tbStartY_TextChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 26;
            this.label2.Text = "Middle:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbStartX
            // 
            this.tbStartX.BackColor = System.Drawing.SystemColors.Window;
            this.tbStartX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbStartX.Location = new System.Drawing.Point(66, 82);
            this.tbStartX.Name = "tbStartX";
            this.tbStartX.Size = new System.Drawing.Size(66, 22);
            this.tbStartX.TabIndex = 34;
            this.tbStartX.TextChanged += new System.EventHandler(this.tbStartX_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 25;
            this.label1.Text = "Start:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbWeightControl.Location = new System.Drawing.Point(23, 311);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(121, 18);
            this.cbWeightControl.TabIndex = 53;
            this.cbWeightControl.Text = "weight control";
            this.cbWeightControl.UseVisualStyleBackColor = false;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLineType.ForeColor = System.Drawing.Color.White;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(102, 265);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(75, 22);
            this.comboBoxLineType.TabIndex = 52;
            // 
            // btnEditLineParams
            // 
            this.btnEditLineParams.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineParams.ForeColor = System.Drawing.Color.Black;
            this.btnEditLineParams.Location = new System.Drawing.Point(210, 264);
            this.btnEditLineParams.Name = "btnEditLineParams";
            this.btnEditLineParams.Size = new System.Drawing.Size(59, 23);
            this.btnEditLineParams.TabIndex = 51;
            this.btnEditLineParams.Text = "编辑";
            this.btnEditLineParams.UseVisualStyleBackColor = true;
            this.btnEditLineParams.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.SystemColors.Window;
            this.tbWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbWeight.Location = new System.Drawing.Point(150, 309);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(74, 22);
            this.tbWeight.TabIndex = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(230, 312);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 14);
            this.label7.TabIndex = 48;
            this.label7.Text = "mg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(20, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 14);
            this.label6.TabIndex = 49;
            this.label6.Text = "Line Type:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox1.Controls.Add(this.rbCenter);
            this.groupBox1.Controls.Add(this.rbSME);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 47);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // rbCenter
            // 
            this.rbCenter.AutoSize = true;
            this.rbCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rbCenter.Location = new System.Drawing.Point(119, 19);
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.Size = new System.Drawing.Size(69, 18);
            this.rbCenter.TabIndex = 0;
            this.rbCenter.TabStop = true;
            this.rbCenter.Text = "Center";
            this.rbCenter.UseVisualStyleBackColor = false;
            this.rbCenter.CheckedChanged += new System.EventHandler(this.rbCenter_CheckedChanged);
            // 
            // rbSME
            // 
            this.rbSME.AutoSize = true;
            this.rbSME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rbSME.Location = new System.Drawing.Point(29, 19);
            this.rbSME.Name = "rbSME";
            this.rbSME.Size = new System.Drawing.Size(53, 18);
            this.rbSME.TabIndex = 0;
            this.rbSME.TabStop = true;
            this.rbSME.Text = "SME";
            this.rbSME.UseVisualStyleBackColor = false;
            this.rbSME.CheckedChanged += new System.EventHandler(this.rbSME_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 54;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditCircleMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbWeightControl);
            this.Controls.Add(this.comboBoxLineType);
            this.Controls.Add(this.btnEditLineParams);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxDegree);
            this.Controls.Add(this.btnCenterGoTo);
            this.Controls.Add(this.btnSelectCenter);
            this.Controls.Add(this.btnEndGoTo);
            this.Controls.Add(this.btnSelectEnd);
            this.Controls.Add(this.btnMiddleGoTo);
            this.Controls.Add(this.tbCenterY);
            this.Controls.Add(this.btnSelectMiddle);
            this.Controls.Add(this.tbEndY);
            this.Controls.Add(this.btnStartGoTo);
            this.Controls.Add(this.tbCenterX);
            this.Controls.Add(this.tbMiddleY);
            this.Controls.Add(this.tbEndX);
            this.Controls.Add(this.btnSelectStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMiddleX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbStartY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbStartX);
            this.Controls.Add(this.label1);
            this.Name = "EditCircleMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.ComboBox comboBoxDegree;
        private System.Windows.Forms.Button btnCenterGoTo;
        private System.Windows.Forms.Button btnSelectCenter;
        private System.Windows.Forms.Button btnEndGoTo;
        private System.Windows.Forms.Button btnSelectEnd;
        private System.Windows.Forms.Button btnMiddleGoTo;
        private Controls.DoubleTextBox tbCenterY;
        private System.Windows.Forms.Button btnSelectMiddle;
        private Controls.DoubleTextBox tbEndY;
        private System.Windows.Forms.Button btnStartGoTo;
        private Controls.DoubleTextBox tbCenterX;
        private Controls.DoubleTextBox tbMiddleY;
        private Controls.DoubleTextBox tbEndX;
        private System.Windows.Forms.Button btnSelectStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Controls.DoubleTextBox tbMiddleX;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbStartY;
        private System.Windows.Forms.Label label2;
        private Controls.DoubleTextBox tbStartX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button btnEditLineParams;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbSME;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
