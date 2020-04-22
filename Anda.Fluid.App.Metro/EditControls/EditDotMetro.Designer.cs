namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditDotMetro
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
            this.tbShots = new Anda.Fluid.Controls.DoubleTextBox();
            this.ckbShotNums = new System.Windows.Forms.CheckBox();
            this.lblDotLocation = new System.Windows.Forms.Label();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnEditDotParams = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDotType = new System.Windows.Forms.ComboBox();
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
            // tbShots
            // 
            this.tbShots.BackColor = System.Drawing.SystemColors.Window;
            this.tbShots.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbShots.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbShots.Location = new System.Drawing.Point(169, 161);
            this.tbShots.Name = "tbShots";
            this.tbShots.Size = new System.Drawing.Size(63, 22);
            this.tbShots.TabIndex = 38;
            // 
            // ckbShotNums
            // 
            this.ckbShotNums.AutoSize = true;
            this.ckbShotNums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ckbShotNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbShotNums.ForeColor = System.Drawing.Color.White;
            this.ckbShotNums.Location = new System.Drawing.Point(18, 163);
            this.ckbShotNums.Name = "ckbShotNums";
            this.ckbShotNums.Size = new System.Drawing.Size(93, 18);
            this.ckbShotNums.TabIndex = 37;
            this.ckbShotNums.Text = "ShotNums";
            this.ckbShotNums.UseVisualStyleBackColor = false;
            this.ckbShotNums.CheckedChanged += new System.EventHandler(this.ckbShotNums_CheckedChanged);
            // 
            // lblDotLocation
            // 
            this.lblDotLocation.AutoSize = true;
            this.lblDotLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblDotLocation.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDotLocation.ForeColor = System.Drawing.Color.White;
            this.lblDotLocation.Location = new System.Drawing.Point(15, 12);
            this.lblDotLocation.Name = "lblDotLocation";
            this.lblDotLocation.Size = new System.Drawing.Size(95, 14);
            this.lblDotLocation.TabIndex = 26;
            this.lblDotLocation.Text = "Dot Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.SystemColors.Window;
            this.tbLocationX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocationX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLocationX.Location = new System.Drawing.Point(36, 29);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(238, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 36;
            this.label2.Text = "mg";
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.SystemColors.Window;
            this.tbLocationY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLocationY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLocationY.Location = new System.Drawing.Point(122, 29);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 28;
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.SystemColors.Window;
            this.tbWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbWeight.Location = new System.Drawing.Point(169, 126);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(63, 22);
            this.tbWeight.TabIndex = 35;
            // 
            // btnGoTo
            // 
            this.btnGoTo.BackColor = System.Drawing.SystemColors.Control;
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(289, 28);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 29;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = false;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbWeightControl.Location = new System.Drawing.Point(18, 128);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 34;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = false;
            this.cbWeightControl.CheckedChanged += new System.EventHandler(this.cbWeightControl_CheckedChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(208, 28);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 30;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnEditDotParams
            // 
            this.btnEditDotParams.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditDotParams.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDotParams.ForeColor = System.Drawing.Color.Black;
            this.btnEditDotParams.Location = new System.Drawing.Point(196, 83);
            this.btnEditDotParams.Name = "btnEditDotParams";
            this.btnEditDotParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditDotParams.TabIndex = 33;
            this.btnEditDotParams.Text = "编辑";
            this.btnEditDotParams.UseVisualStyleBackColor = false;
            this.btnEditDotParams.Click += new System.EventHandler(this.btnEditDotParams_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 31;
            this.label1.Text = "Dot Style:";
            // 
            // comboBoxDotType
            // 
            this.comboBoxDotType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDotType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDotType.ForeColor = System.Drawing.Color.White;
            this.comboBoxDotType.FormattingEnabled = true;
            this.comboBoxDotType.Location = new System.Drawing.Point(36, 84);
            this.comboBoxDotType.Name = "comboBoxDotType";
            this.comboBoxDotType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxDotType.TabIndex = 32;
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
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditDotMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbShots);
            this.Controls.Add(this.ckbShotNums);
            this.Controls.Add(this.lblDotLocation);
            this.Controls.Add(this.tbLocationX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbLocationY);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.cbWeightControl);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnEditDotParams);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDotType);
            this.Name = "EditDotMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private Controls.DoubleTextBox tbShots;
        private System.Windows.Forms.CheckBox ckbShotNums;
        private System.Windows.Forms.Label lblDotLocation;
        private Controls.DoubleTextBox tbLocationX;
        private System.Windows.Forms.Label label2;
        private Controls.DoubleTextBox tbLocationY;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnEditDotParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDotType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
