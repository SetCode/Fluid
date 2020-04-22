namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditDoPatternMetro
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
            this.cbxValveType = new System.Windows.Forms.ComboBox();
            this.lblValveType = new System.Windows.Forms.Label();
            this.ckbReverse = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.listBoxPatterns = new System.Windows.Forms.ListBox();
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
            // cbxValveType
            // 
            this.cbxValveType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbxValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxValveType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxValveType.ForeColor = System.Drawing.Color.White;
            this.cbxValveType.FormattingEnabled = true;
            this.cbxValveType.Location = new System.Drawing.Point(249, 56);
            this.cbxValveType.Name = "cbxValveType";
            this.cbxValveType.Size = new System.Drawing.Size(121, 22);
            this.cbxValveType.TabIndex = 23;
            // 
            // lblValveType
            // 
            this.lblValveType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblValveType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValveType.ForeColor = System.Drawing.Color.White;
            this.lblValveType.Location = new System.Drawing.Point(146, 59);
            this.lblValveType.Name = "lblValveType";
            this.lblValveType.Size = new System.Drawing.Size(97, 19);
            this.lblValveType.TabIndex = 22;
            this.lblValveType.Text = "Valve Type :";
            // 
            // ckbReverse
            // 
            this.ckbReverse.AutoSize = true;
            this.ckbReverse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ckbReverse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbReverse.ForeColor = System.Drawing.Color.White;
            this.ckbReverse.Location = new System.Drawing.Point(20, 58);
            this.ckbReverse.Name = "ckbReverse";
            this.ckbReverse.Size = new System.Drawing.Size(80, 18);
            this.ckbReverse.TabIndex = 21;
            this.ckbReverse.Text = "Reverse";
            this.ckbReverse.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "Origin:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.SystemColors.Window;
            this.tbOriginX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginX.Location = new System.Drawing.Point(76, 16);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(67, 22);
            this.tbOriginX.TabIndex = 18;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(222, 15);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 16;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.SystemColors.Window;
            this.tbOriginY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginY.Location = new System.Drawing.Point(149, 16);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(67, 22);
            this.tbOriginY.TabIndex = 17;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(303, 15);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 15;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(20, 108);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.Size = new System.Drawing.Size(340, 270);
            this.listBoxPatterns.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "Pattern list:";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditDoPatternMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBoxPatterns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxValveType);
            this.Controls.Add(this.lblValveType);
            this.Controls.Add(this.ckbReverse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbOriginX);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbOriginY);
            this.Controls.Add(this.btnGoTo);
            this.Name = "EditDoPatternMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.ComboBox cbxValveType;
        private System.Windows.Forms.Label lblValveType;
        private System.Windows.Forms.CheckBox ckbReverse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbOriginY;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
    }
}
