namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditSnakelineMetro
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
            this.chkContinuous = new System.Windows.Forms.CheckBox();
            this.btnLineType = new System.Windows.Forms.Button();
            this.btnEditWt = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.tbLineNumbers = new Anda.Fluid.Controls.IntTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPoint3GoTo = new System.Windows.Forms.Button();
            this.btnPoint2GoTo = new System.Windows.Forms.Button();
            this.btnPoint1GoTo = new System.Windows.Forms.Button();
            this.btnPoint3Select = new System.Windows.Forms.Button();
            this.btnPoint2Select = new System.Windows.Forms.Button();
            this.btnPoint1Select = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbP2Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP3Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP2X = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP1Y = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP3X = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbP1X = new Anda.Fluid.Controls.DoubleTextBox();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // chkContinuous
            // 
            this.chkContinuous.AutoSize = true;
            this.chkContinuous.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.chkContinuous.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContinuous.ForeColor = System.Drawing.Color.White;
            this.chkContinuous.Location = new System.Drawing.Point(262, 14);
            this.chkContinuous.Name = "chkContinuous";
            this.chkContinuous.Size = new System.Drawing.Size(99, 18);
            this.chkContinuous.TabIndex = 62;
            this.chkContinuous.Text = "Continuous";
            this.chkContinuous.UseVisualStyleBackColor = false;
            this.chkContinuous.CheckedChanged += new System.EventHandler(this.chkContinuous_CheckedChanged);
            // 
            // btnLineType
            // 
            this.btnLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLineType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineType.ForeColor = System.Drawing.Color.Black;
            this.btnLineType.Location = new System.Drawing.Point(227, 387);
            this.btnLineType.Name = "btnLineType";
            this.btnLineType.Size = new System.Drawing.Size(75, 23);
            this.btnLineType.TabIndex = 61;
            this.btnLineType.Text = "线型";
            this.btnLineType.UseVisualStyleBackColor = true;
            this.btnLineType.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnEditWt
            // 
            this.btnEditWt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditWt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditWt.ForeColor = System.Drawing.Color.Black;
            this.btnEditWt.Location = new System.Drawing.Point(146, 387);
            this.btnEditWt.Name = "btnEditWt";
            this.btnEditWt.Size = new System.Drawing.Size(75, 23);
            this.btnEditWt.TabIndex = 60;
            this.btnEditWt.Text = "编辑";
            this.btnEditWt.UseVisualStyleBackColor = true;
            this.btnEditWt.Click += new System.EventHandler(this.btnEditLineParams_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 59;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbWeightControl.Location = new System.Drawing.Point(15, 390);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 57;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = false;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // tbLineNumbers
            // 
            this.tbLineNumbers.BackColor = System.Drawing.Color.White;
            this.tbLineNumbers.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLineNumbers.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLineNumbers.Location = new System.Drawing.Point(171, 12);
            this.tbLineNumbers.Name = "tbLineNumbers";
            this.tbLineNumbers.Size = new System.Drawing.Size(84, 22);
            this.tbLineNumbers.TabIndex = 56;
            this.tbLineNumbers.Text = "2";
            this.tbLineNumbers.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 55;
            this.label2.Text = "line numbers:";
            // 
            // btnPoint3GoTo
            // 
            this.btnPoint3GoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint3GoTo.ForeColor = System.Drawing.Color.Black;
            this.btnPoint3GoTo.Location = new System.Drawing.Point(343, 99);
            this.btnPoint3GoTo.Name = "btnPoint3GoTo";
            this.btnPoint3GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint3GoTo.TabIndex = 53;
            this.btnPoint3GoTo.Text = "移动";
            this.btnPoint3GoTo.UseVisualStyleBackColor = true;
            this.btnPoint3GoTo.Click += new System.EventHandler(this.btnPoint3GoTo_Click);
            // 
            // btnPoint2GoTo
            // 
            this.btnPoint2GoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint2GoTo.ForeColor = System.Drawing.Color.Black;
            this.btnPoint2GoTo.Location = new System.Drawing.Point(343, 69);
            this.btnPoint2GoTo.Name = "btnPoint2GoTo";
            this.btnPoint2GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint2GoTo.TabIndex = 52;
            this.btnPoint2GoTo.Text = "移动";
            this.btnPoint2GoTo.UseVisualStyleBackColor = true;
            this.btnPoint2GoTo.Click += new System.EventHandler(this.btnPoint2GoTo_Click);
            // 
            // btnPoint1GoTo
            // 
            this.btnPoint1GoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint1GoTo.ForeColor = System.Drawing.Color.Black;
            this.btnPoint1GoTo.Location = new System.Drawing.Point(343, 40);
            this.btnPoint1GoTo.Name = "btnPoint1GoTo";
            this.btnPoint1GoTo.Size = new System.Drawing.Size(75, 23);
            this.btnPoint1GoTo.TabIndex = 51;
            this.btnPoint1GoTo.Text = "移动";
            this.btnPoint1GoTo.UseVisualStyleBackColor = true;
            this.btnPoint1GoTo.Click += new System.EventHandler(this.btnPoint1GoTo_Click);
            // 
            // btnPoint3Select
            // 
            this.btnPoint3Select.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint3Select.ForeColor = System.Drawing.Color.Black;
            this.btnPoint3Select.Location = new System.Drawing.Point(262, 99);
            this.btnPoint3Select.Name = "btnPoint3Select";
            this.btnPoint3Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint3Select.TabIndex = 50;
            this.btnPoint3Select.Text = "示教";
            this.btnPoint3Select.UseVisualStyleBackColor = true;
            this.btnPoint3Select.Click += new System.EventHandler(this.btnPoint3Select_Click);
            // 
            // btnPoint2Select
            // 
            this.btnPoint2Select.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint2Select.ForeColor = System.Drawing.Color.Black;
            this.btnPoint2Select.Location = new System.Drawing.Point(262, 69);
            this.btnPoint2Select.Name = "btnPoint2Select";
            this.btnPoint2Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint2Select.TabIndex = 49;
            this.btnPoint2Select.Text = "示教";
            this.btnPoint2Select.UseVisualStyleBackColor = true;
            this.btnPoint2Select.Click += new System.EventHandler(this.btnPoint2Select_Click);
            // 
            // btnPoint1Select
            // 
            this.btnPoint1Select.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoint1Select.ForeColor = System.Drawing.Color.Black;
            this.btnPoint1Select.Location = new System.Drawing.Point(262, 40);
            this.btnPoint1Select.Name = "btnPoint1Select";
            this.btnPoint1Select.Size = new System.Drawing.Size(75, 23);
            this.btnPoint1Select.TabIndex = 54;
            this.btnPoint1Select.Text = "示教";
            this.btnPoint1Select.UseVisualStyleBackColor = true;
            this.btnPoint1Select.Click += new System.EventHandler(this.btnPoint1Select_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(14, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 14);
            this.label7.TabIndex = 48;
            this.label7.Text = "Point 2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(14, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 47;
            this.label5.Text = "Point 3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 46;
            this.label3.Text = "Point 1:";
            // 
            // tbP2Y
            // 
            this.tbP2Y.BackColor = System.Drawing.Color.White;
            this.tbP2Y.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP2Y.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP2Y.Location = new System.Drawing.Point(171, 71);
            this.tbP2Y.Name = "tbP2Y";
            this.tbP2Y.Size = new System.Drawing.Size(84, 22);
            this.tbP2Y.TabIndex = 44;
            this.tbP2Y.Text = "0.000";
            // 
            // tbP3Y
            // 
            this.tbP3Y.BackColor = System.Drawing.Color.White;
            this.tbP3Y.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP3Y.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP3Y.Location = new System.Drawing.Point(171, 101);
            this.tbP3Y.Name = "tbP3Y";
            this.tbP3Y.Size = new System.Drawing.Size(84, 22);
            this.tbP3Y.TabIndex = 43;
            this.tbP3Y.Text = "0.000";
            // 
            // tbP2X
            // 
            this.tbP2X.BackColor = System.Drawing.Color.White;
            this.tbP2X.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP2X.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP2X.Location = new System.Drawing.Point(79, 71);
            this.tbP2X.Name = "tbP2X";
            this.tbP2X.Size = new System.Drawing.Size(84, 22);
            this.tbP2X.TabIndex = 42;
            this.tbP2X.Text = "0.000";
            // 
            // tbP1Y
            // 
            this.tbP1Y.BackColor = System.Drawing.Color.White;
            this.tbP1Y.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP1Y.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP1Y.Location = new System.Drawing.Point(172, 40);
            this.tbP1Y.Name = "tbP1Y";
            this.tbP1Y.Size = new System.Drawing.Size(84, 22);
            this.tbP1Y.TabIndex = 41;
            this.tbP1Y.Text = "0.000";
            // 
            // tbP3X
            // 
            this.tbP3X.BackColor = System.Drawing.Color.White;
            this.tbP3X.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP3X.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP3X.Location = new System.Drawing.Point(79, 101);
            this.tbP3X.Name = "tbP3X";
            this.tbP3X.Size = new System.Drawing.Size(84, 22);
            this.tbP3X.TabIndex = 45;
            this.tbP3X.Text = "0.000";
            // 
            // tbP1X
            // 
            this.tbP1X.BackColor = System.Drawing.Color.White;
            this.tbP1X.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbP1X.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbP1X.Location = new System.Drawing.Point(80, 40);
            this.tbP1X.Name = "tbP1X";
            this.tbP1X.Size = new System.Drawing.Size(84, 22);
            this.tbP1X.TabIndex = 40;
            this.tbP1X.Text = "0.000";
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(15, 159);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(346, 214);
            this.listBoxPoints.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 63;
            this.label1.Text = "Line Points:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(366, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditSnakelineMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBoxPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkContinuous);
            this.Controls.Add(this.btnLineType);
            this.Controls.Add(this.btnEditWt);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbWeightControl);
            this.Controls.Add(this.tbLineNumbers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPoint3GoTo);
            this.Controls.Add(this.btnPoint2GoTo);
            this.Controls.Add(this.btnPoint1GoTo);
            this.Controls.Add(this.btnPoint3Select);
            this.Controls.Add(this.btnPoint2Select);
            this.Controls.Add(this.btnPoint1Select);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbP2Y);
            this.Controls.Add(this.tbP3Y);
            this.Controls.Add(this.tbP2X);
            this.Controls.Add(this.tbP1Y);
            this.Controls.Add(this.tbP3X);
            this.Controls.Add(this.tbP1X);
            this.Name = "EditSnakelineMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.CheckBox chkContinuous;
        private System.Windows.Forms.Button btnLineType;
        private System.Windows.Forms.Button btnEditWt;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private Controls.IntTextBox tbLineNumbers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPoint3GoTo;
        private System.Windows.Forms.Button btnPoint2GoTo;
        private System.Windows.Forms.Button btnPoint1GoTo;
        private System.Windows.Forms.Button btnPoint3Select;
        private System.Windows.Forms.Button btnPoint2Select;
        private System.Windows.Forms.Button btnPoint1Select;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbP2Y;
        private Controls.DoubleTextBox tbP3Y;
        private Controls.DoubleTextBox tbP2X;
        private Controls.DoubleTextBox tbP1Y;
        private Controls.DoubleTextBox tbP3X;
        private Controls.DoubleTextBox tbP1X;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
    }
}
