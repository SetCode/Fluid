namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditHeightMetro
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
            this.heightControl1 = new Anda.Fluid.App.EditCmdLineForms.HeightControl();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnSelect = new System.Windows.Forms.Button();
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
            // heightControl1
            // 
            this.heightControl1.BackColor = System.Drawing.Color.White;
            this.heightControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightControl1.ForeColor = System.Drawing.Color.Black;
            this.heightControl1.Location = new System.Drawing.Point(8, 53);
            this.heightControl1.Name = "heightControl1";
            this.heightControl1.Size = new System.Drawing.Size(442, 138);
            this.heightControl1.TabIndex = 18;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "Position:";
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbX.Location = new System.Drawing.Point(76, 23);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(72, 22);
            this.tbX.TabIndex = 13;
            this.tbX.Text = "0.000";
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(313, 22);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 14;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbY.Location = new System.Drawing.Point(154, 23);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(72, 22);
            this.tbY.TabIndex = 12;
            this.tbY.Text = "0.000";
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(232, 22);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditHeightMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.heightControl1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbX);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.tbY);
            this.Controls.Add(this.btnSelect);
            this.Name = "EditHeightMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private EditCmdLineForms.HeightControl heightControl1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Button btnGoTo;
        private Controls.DoubleTextBox tbY;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
    }
}
