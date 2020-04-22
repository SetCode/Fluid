namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditPatternsArrayMetro
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
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.listBoxPatterns = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGoToVEnd = new System.Windows.Forms.Button();
            this.btnGoToHEnd = new System.Windows.Forms.Button();
            this.btnGoToOrigin = new System.Windows.Forms.Button();
            this.btnTeachVEnd = new System.Windows.Forms.Button();
            this.btnTeachHEnd = new System.Windows.Forms.Button();
            this.btnTeachOrigin = new System.Windows.Forms.Button();
            this.tbVEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbHEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbVEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbHEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbVNums = new Anda.Fluid.Controls.IntTextBox();
            this.tbHNums = new Anda.Fluid.Controls.IntTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.listBoxOrigins = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            // cbxMode
            // 
            this.cbxMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMode.ForeColor = System.Drawing.Color.White;
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Location = new System.Drawing.Point(327, 176);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(121, 22);
            this.cbxMode.TabIndex = 78;
            this.cbxMode.SelectedIndexChanged += new System.EventHandler(this.cbxMode_SelectedIndexChanged);
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(7, 29);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxPatterns.Size = new System.Drawing.Size(107, 172);
            this.listBoxPatterns.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 14);
            this.label1.TabIndex = 76;
            this.label1.Text = "Pattern List:";
            // 
            // btnGoToVEnd
            // 
            this.btnGoToVEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToVEnd.ForeColor = System.Drawing.Color.Black;
            this.btnGoToVEnd.Location = new System.Drawing.Point(373, 136);
            this.btnGoToVEnd.Name = "btnGoToVEnd";
            this.btnGoToVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToVEnd.TabIndex = 75;
            this.btnGoToVEnd.Text = "移动";
            this.btnGoToVEnd.UseVisualStyleBackColor = true;
            this.btnGoToVEnd.Click += new System.EventHandler(this.btnGoToVEnd_Click);
            // 
            // btnGoToHEnd
            // 
            this.btnGoToHEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToHEnd.ForeColor = System.Drawing.Color.Black;
            this.btnGoToHEnd.Location = new System.Drawing.Point(373, 92);
            this.btnGoToHEnd.Name = "btnGoToHEnd";
            this.btnGoToHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToHEnd.TabIndex = 74;
            this.btnGoToHEnd.Text = "移动";
            this.btnGoToHEnd.UseVisualStyleBackColor = true;
            this.btnGoToHEnd.Click += new System.EventHandler(this.btnGoToHEnd_Click);
            // 
            // btnGoToOrigin
            // 
            this.btnGoToOrigin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToOrigin.ForeColor = System.Drawing.Color.Black;
            this.btnGoToOrigin.Location = new System.Drawing.Point(373, 48);
            this.btnGoToOrigin.Name = "btnGoToOrigin";
            this.btnGoToOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOrigin.TabIndex = 73;
            this.btnGoToOrigin.Text = "移动";
            this.btnGoToOrigin.UseVisualStyleBackColor = true;
            this.btnGoToOrigin.Click += new System.EventHandler(this.btnGoToOrigin_Click);
            // 
            // btnTeachVEnd
            // 
            this.btnTeachVEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeachVEnd.ForeColor = System.Drawing.Color.Black;
            this.btnTeachVEnd.Location = new System.Drawing.Point(292, 136);
            this.btnTeachVEnd.Name = "btnTeachVEnd";
            this.btnTeachVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnTeachVEnd.TabIndex = 72;
            this.btnTeachVEnd.Text = "示教";
            this.btnTeachVEnd.UseVisualStyleBackColor = true;
            this.btnTeachVEnd.Click += new System.EventHandler(this.btnTeachVEnd_Click);
            // 
            // btnTeachHEnd
            // 
            this.btnTeachHEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeachHEnd.ForeColor = System.Drawing.Color.Black;
            this.btnTeachHEnd.Location = new System.Drawing.Point(292, 92);
            this.btnTeachHEnd.Name = "btnTeachHEnd";
            this.btnTeachHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnTeachHEnd.TabIndex = 71;
            this.btnTeachHEnd.Text = "示教";
            this.btnTeachHEnd.UseVisualStyleBackColor = true;
            this.btnTeachHEnd.Click += new System.EventHandler(this.btnTeachHEnd_Click);
            // 
            // btnTeachOrigin
            // 
            this.btnTeachOrigin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeachOrigin.ForeColor = System.Drawing.Color.Black;
            this.btnTeachOrigin.Location = new System.Drawing.Point(292, 48);
            this.btnTeachOrigin.Name = "btnTeachOrigin";
            this.btnTeachOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnTeachOrigin.TabIndex = 70;
            this.btnTeachOrigin.Text = "示教";
            this.btnTeachOrigin.UseVisualStyleBackColor = true;
            this.btnTeachOrigin.Click += new System.EventHandler(this.btnTeachOrigin_Click);
            // 
            // tbVEndY
            // 
            this.tbVEndY.BackColor = System.Drawing.Color.White;
            this.tbVEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVEndY.Location = new System.Drawing.Point(210, 136);
            this.tbVEndY.Name = "tbVEndY";
            this.tbVEndY.Size = new System.Drawing.Size(75, 22);
            this.tbVEndY.TabIndex = 68;
            this.tbVEndY.Text = "0.000";
            this.tbVEndY.TextChanged += new System.EventHandler(this.tbVEndY_TextChanged);
            // 
            // tbHEndY
            // 
            this.tbHEndY.BackColor = System.Drawing.Color.White;
            this.tbHEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHEndY.Location = new System.Drawing.Point(210, 92);
            this.tbHEndY.Name = "tbHEndY";
            this.tbHEndY.Size = new System.Drawing.Size(75, 22);
            this.tbHEndY.TabIndex = 67;
            this.tbHEndY.Text = "0.000";
            this.tbHEndY.TextChanged += new System.EventHandler(this.tbHEndY_TextChanged);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginY.Location = new System.Drawing.Point(210, 48);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(75, 22);
            this.tbOriginY.TabIndex = 69;
            this.tbOriginY.Text = "0.000";
            this.tbOriginY.TextChanged += new System.EventHandler(this.tbOriginY_TextChanged);
            // 
            // tbVEndX
            // 
            this.tbVEndX.BackColor = System.Drawing.Color.White;
            this.tbVEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVEndX.Location = new System.Drawing.Point(129, 136);
            this.tbVEndX.Name = "tbVEndX";
            this.tbVEndX.Size = new System.Drawing.Size(75, 22);
            this.tbVEndX.TabIndex = 66;
            this.tbVEndX.Text = "0.000";
            this.tbVEndX.TextChanged += new System.EventHandler(this.tbVEndX_TextChanged);
            // 
            // tbHEndX
            // 
            this.tbHEndX.BackColor = System.Drawing.Color.White;
            this.tbHEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHEndX.Location = new System.Drawing.Point(129, 92);
            this.tbHEndX.Name = "tbHEndX";
            this.tbHEndX.Size = new System.Drawing.Size(75, 22);
            this.tbHEndX.TabIndex = 65;
            this.tbHEndX.Text = "0.000";
            this.tbHEndX.TextChanged += new System.EventHandler(this.tbHEndX_TextChanged);
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginX.Location = new System.Drawing.Point(129, 48);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(75, 22);
            this.tbOriginX.TabIndex = 64;
            this.tbOriginX.Text = "0.000";
            this.tbOriginX.TextChanged += new System.EventHandler(this.tbOriginX_TextChanged);
            // 
            // tbVNums
            // 
            this.tbVNums.BackColor = System.Drawing.Color.White;
            this.tbVNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVNums.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVNums.Location = new System.Drawing.Point(344, 9);
            this.tbVNums.Name = "tbVNums";
            this.tbVNums.Size = new System.Drawing.Size(51, 22);
            this.tbVNums.TabIndex = 63;
            this.tbVNums.Text = "3";
            this.tbVNums.TextChanged += new System.EventHandler(this.tbVNums_TextChanged);
            // 
            // tbHNums
            // 
            this.tbHNums.BackColor = System.Drawing.Color.White;
            this.tbHNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHNums.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHNums.Location = new System.Drawing.Point(203, 9);
            this.tbHNums.Name = "tbHNums";
            this.tbHNums.Size = new System.Drawing.Size(51, 22);
            this.tbHNums.TabIndex = 62;
            this.tbHNums.Text = "3";
            this.tbHNums.TextChanged += new System.EventHandler(this.tbHNums_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(131, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 60;
            this.label7.Text = "Vertical End:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(131, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 14);
            this.label6.TabIndex = 59;
            this.label6.Text = "Horizontal End:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(260, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 14);
            this.label4.TabIndex = 58;
            this.label4.Text = "ColumNum";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(131, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 14);
            this.label5.TabIndex = 61;
            this.label5.Text = "Origin:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(131, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 57;
            this.label3.Text = "RowNum";
            // 
            // btnDisable
            // 
            this.btnDisable.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisable.ForeColor = System.Drawing.Color.Black;
            this.btnDisable.Location = new System.Drawing.Point(366, 218);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 82;
            this.btnDisable.Text = "屏蔽";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(366, 266);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 81;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // listBoxOrigins
            // 
            this.listBoxOrigins.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxOrigins.FormattingEnabled = true;
            this.listBoxOrigins.ItemHeight = 14;
            this.listBoxOrigins.Location = new System.Drawing.Point(7, 207);
            this.listBoxOrigins.Name = "listBoxOrigins";
            this.listBoxOrigins.Size = new System.Drawing.Size(331, 186);
            this.listBoxOrigins.TabIndex = 80;
            this.listBoxOrigins.SelectedIndexChanged += new System.EventHandler(this.ListBoxOrigins_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(126, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 79;
            this.label2.Text = "Pattern Origins:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 83;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(366, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 84;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditPatternsArrayMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.listBoxOrigins);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxMode);
            this.Controls.Add(this.listBoxPatterns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGoToVEnd);
            this.Controls.Add(this.btnGoToHEnd);
            this.Controls.Add(this.btnGoToOrigin);
            this.Controls.Add(this.btnTeachVEnd);
            this.Controls.Add(this.btnTeachHEnd);
            this.Controls.Add(this.btnTeachOrigin);
            this.Controls.Add(this.tbVEndY);
            this.Controls.Add(this.tbHEndY);
            this.Controls.Add(this.tbOriginY);
            this.Controls.Add(this.tbVEndX);
            this.Controls.Add(this.tbHEndX);
            this.Controls.Add(this.tbOriginX);
            this.Controls.Add(this.tbVNums);
            this.Controls.Add(this.tbHNums);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Name = "EditPatternsArrayMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.ComboBox cbxMode;
        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoToVEnd;
        private System.Windows.Forms.Button btnGoToHEnd;
        private System.Windows.Forms.Button btnGoToOrigin;
        private System.Windows.Forms.Button btnTeachVEnd;
        private System.Windows.Forms.Button btnTeachHEnd;
        private System.Windows.Forms.Button btnTeachOrigin;
        private Controls.DoubleTextBox tbVEndY;
        private Controls.DoubleTextBox tbHEndY;
        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbVEndX;
        private Controls.DoubleTextBox tbHEndX;
        private Controls.DoubleTextBox tbOriginX;
        private Controls.IntTextBox tbVNums;
        private Controls.IntTextBox tbHNums;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.ListBox listBoxOrigins;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
