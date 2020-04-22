namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditFinishShotMetro
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnEditDotParams = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDotType = new System.Windows.Forms.ComboBox();
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
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 14);
            this.label3.TabIndex = 24;
            this.label3.Text = "Dot Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.SystemColors.Window;
            this.tbLocationX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocationX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLocationX.Location = new System.Drawing.Point(36, 29);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 25;
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.SystemColors.Window;
            this.tbLocationY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocationY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLocationY.Location = new System.Drawing.Point(122, 29);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 26;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(289, 28);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 27;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(208, 28);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 28;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnEditDotParams
            // 
            this.btnEditDotParams.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDotParams.ForeColor = System.Drawing.Color.Black;
            this.btnEditDotParams.Location = new System.Drawing.Point(196, 98);
            this.btnEditDotParams.Name = "btnEditDotParams";
            this.btnEditDotParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditDotParams.TabIndex = 31;
            this.btnEditDotParams.Text = "编辑";
            this.btnEditDotParams.UseVisualStyleBackColor = true;
            this.btnEditDotParams.Click += new System.EventHandler(this.btnEditDotParams_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 29;
            this.label1.Text = "Dot Style:";
            // 
            // comboBoxDotType
            // 
            this.comboBoxDotType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDotType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDotType.ForeColor = System.Drawing.Color.White;
            this.comboBoxDotType.FormattingEnabled = true;
            this.comboBoxDotType.Location = new System.Drawing.Point(36, 99);
            this.comboBoxDotType.Name = "comboBoxDotType";
            this.comboBoxDotType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxDotType.TabIndex = 30;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditFinishShotMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLocationX);
            this.Controls.Add(this.tbLocationY);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnEditDotParams);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDotType);
            this.Name = "EditFinishShotMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbLocationX;
        private Controls.DoubleTextBox tbLocationY;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnEditDotParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDotType;
        private System.Windows.Forms.Button btnCancel;
    }
}
