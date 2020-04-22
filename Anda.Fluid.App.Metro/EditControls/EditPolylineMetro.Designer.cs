namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditPolylineMetro
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
            this.btnGoToOffset = new System.Windows.Forms.Button();
            this.btnSelectOffset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudOffset = new System.Windows.Forms.NumericUpDown();
            this.btnLineStyle = new System.Windows.Forms.Button();
            this.btnEditWeight = new System.Windows.Forms.Button();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnGotoStart = new System.Windows.Forms.Button();
            this.btnSelectStart = new System.Windows.Forms.Button();
            this.tbPointY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbPointX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            // btnGoToOffset
            // 
            this.btnGoToOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToOffset.ForeColor = System.Drawing.Color.Black;
            this.btnGoToOffset.Location = new System.Drawing.Point(259, 352);
            this.btnGoToOffset.Name = "btnGoToOffset";
            this.btnGoToOffset.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOffset.TabIndex = 80;
            this.btnGoToOffset.Text = "移动";
            this.btnGoToOffset.UseVisualStyleBackColor = true;
            this.btnGoToOffset.Click += new System.EventHandler(this.btnGoToOffset_Click);
            // 
            // btnSelectOffset
            // 
            this.btnSelectOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectOffset.ForeColor = System.Drawing.Color.Black;
            this.btnSelectOffset.Location = new System.Drawing.Point(178, 352);
            this.btnSelectOffset.Name = "btnSelectOffset";
            this.btnSelectOffset.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOffset.TabIndex = 79;
            this.btnSelectOffset.Text = "示教";
            this.btnSelectOffset.UseVisualStyleBackColor = true;
            this.btnSelectOffset.Click += new System.EventHandler(this.btnSelectOffset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 78;
            this.label1.Text = "Offset";
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
            this.nudOffset.Location = new System.Drawing.Point(59, 353);
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
            this.nudOffset.Size = new System.Drawing.Size(113, 22);
            this.nudOffset.TabIndex = 77;
            this.nudOffset.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
            // 
            // btnLineStyle
            // 
            this.btnLineStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineStyle.ForeColor = System.Drawing.Color.Black;
            this.btnLineStyle.Location = new System.Drawing.Point(259, 317);
            this.btnLineStyle.Name = "btnLineStyle";
            this.btnLineStyle.Size = new System.Drawing.Size(75, 23);
            this.btnLineStyle.TabIndex = 76;
            this.btnLineStyle.Text = "线型";
            this.btnLineStyle.UseVisualStyleBackColor = true;
            this.btnLineStyle.Click += new System.EventHandler(this.btnLineStyle_Click);
            // 
            // btnEditWeight
            // 
            this.btnEditWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditWeight.ForeColor = System.Drawing.Color.Black;
            this.btnEditWeight.Location = new System.Drawing.Point(178, 317);
            this.btnEditWeight.Name = "btnEditWeight";
            this.btnEditWeight.Size = new System.Drawing.Size(75, 23);
            this.btnEditWeight.TabIndex = 75;
            this.btnEditWeight.Text = "编辑";
            this.btnEditWeight.UseVisualStyleBackColor = true;
            this.btnEditWeight.Click += new System.EventHandler(this.btnEditWeight_Click);
            // 
            // listBoxLines
            // 
            this.listBoxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLines.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.ItemHeight = 14;
            this.listBoxLines.Location = new System.Drawing.Point(8, 181);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.Size = new System.Drawing.Size(444, 130);
            this.listBoxLines.TabIndex = 74;
            this.listBoxLines.SelectedIndexChanged += new System.EventHandler(this.listBoxLines_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(374, 118);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 73;
            this.btnAdd.Text = "示教";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 71;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbWeightControl.Location = new System.Drawing.Point(8, 320);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 70;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = false;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnGotoStart
            // 
            this.btnGotoStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoStart.ForeColor = System.Drawing.Color.Black;
            this.btnGotoStart.Location = new System.Drawing.Point(374, 78);
            this.btnGotoStart.Name = "btnGotoStart";
            this.btnGotoStart.Size = new System.Drawing.Size(75, 23);
            this.btnGotoStart.TabIndex = 69;
            this.btnGotoStart.Text = "移动";
            this.btnGotoStart.UseVisualStyleBackColor = true;
            this.btnGotoStart.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // btnSelectStart
            // 
            this.btnSelectStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectStart.ForeColor = System.Drawing.Color.Black;
            this.btnSelectStart.Location = new System.Drawing.Point(293, 78);
            this.btnSelectStart.Name = "btnSelectStart";
            this.btnSelectStart.Size = new System.Drawing.Size(75, 23);
            this.btnSelectStart.TabIndex = 68;
            this.btnSelectStart.Text = "修改";
            this.btnSelectStart.UseVisualStyleBackColor = true;
            this.btnSelectStart.Click += new System.EventHandler(this.btnTeachPoint_Click);
            // 
            // tbPointY
            // 
            this.tbPointY.BackColor = System.Drawing.Color.White;
            this.tbPointY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPointY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbPointY.Location = new System.Drawing.Point(374, 35);
            this.tbPointY.Name = "tbPointY";
            this.tbPointY.Size = new System.Drawing.Size(72, 22);
            this.tbPointY.TabIndex = 66;
            this.tbPointY.Text = "0.000";
            // 
            // tbPointX
            // 
            this.tbPointX.BackColor = System.Drawing.Color.White;
            this.tbPointX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPointX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbPointX.Location = new System.Drawing.Point(296, 35);
            this.tbPointX.Name = "tbPointX";
            this.tbPointX.Size = new System.Drawing.Size(72, 22);
            this.tbPointX.TabIndex = 67;
            this.tbPointX.Text = "0.000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(290, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 14);
            this.label4.TabIndex = 65;
            this.label4.Text = "Point:";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(293, 118);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 81;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 14;
            this.listBoxPoints.Location = new System.Drawing.Point(8, 29);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(279, 144);
            this.listBoxPoints.TabIndex = 82;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.listBoxPoints_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 83;
            this.label2.Text = "Point list:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(293, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 14);
            this.label3.TabIndex = 84;
            this.label3.Text = "Line list:";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 85;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // EditPolylineMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxPoints);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnGoToOffset);
            this.Controls.Add(this.btnSelectOffset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudOffset);
            this.Controls.Add(this.btnLineStyle);
            this.Controls.Add(this.btnEditWeight);
            this.Controls.Add(this.listBoxLines);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbWeightControl);
            this.Controls.Add(this.btnGotoStart);
            this.Controls.Add(this.btnSelectStart);
            this.Controls.Add(this.tbPointY);
            this.Controls.Add(this.tbPointX);
            this.Controls.Add(this.label4);
            this.Name = "EditPolylineMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.EditPolyLineForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.Button btnGoToOffset;
        private System.Windows.Forms.Button btnSelectOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudOffset;
        private System.Windows.Forms.Button btnLineStyle;
        private System.Windows.Forms.Button btnEditWeight;
        private System.Windows.Forms.ListBox listBoxLines;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnGotoStart;
        private System.Windows.Forms.Button btnSelectStart;
        private Controls.DoubleTextBox tbPointY;
        private Controls.DoubleTextBox tbPointX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
    }
}
