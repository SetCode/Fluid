namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditPatternOriginMetro
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbPatternName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReverse = new System.Windows.Forms.Button();
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
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.BackColor = System.Drawing.SystemColors.Control;
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(196, 79);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 16;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = false;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(196, 51);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 17;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginY.Location = new System.Drawing.Point(75, 80);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(115, 22);
            this.tbOriginY.TabIndex = 13;
            this.tbOriginY.Text = "0";
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginX.Location = new System.Drawing.Point(75, 52);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(115, 22);
            this.tbOriginX.TabIndex = 14;
            this.tbOriginX.Text = "0";
            // 
            // tbPatternName
            // 
            this.tbPatternName.BackColor = System.Drawing.SystemColors.Window;
            this.tbPatternName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPatternName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbPatternName.Location = new System.Drawing.Point(75, 15);
            this.tbPatternName.Name = "tbPatternName";
            this.tbPatternName.Size = new System.Drawing.Size(196, 22);
            this.tbPatternName.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "Origin:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "Name:";
            // 
            // btnReverse
            // 
            this.btnReverse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.ForeColor = System.Drawing.Color.Black;
            this.btnReverse.Location = new System.Drawing.Point(75, 159);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(75, 23);
            this.btnReverse.TabIndex = 20;
            this.btnReverse.Text = "反向";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // EditPatternMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbOriginY);
            this.Controls.Add(this.tbOriginX);
            this.Controls.Add(this.tbPatternName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditPatternMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.EditPattern_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.TextBox tbPatternName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReverse;
    }
}
