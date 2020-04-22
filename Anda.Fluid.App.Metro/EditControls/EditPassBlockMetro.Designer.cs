namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditPassBlockMetro
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
            this.tbEndIndex = new Anda.Fluid.Controls.IntTextBox();
            this.tbStartIndex = new Anda.Fluid.Controls.IntTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIndexFrom = new System.Windows.Forms.Label();
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
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(340, 383);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbEndIndex
            // 
            this.tbEndIndex.BackColor = System.Drawing.SystemColors.Window;
            this.tbEndIndex.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEndIndex.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbEndIndex.Location = new System.Drawing.Point(262, 34);
            this.tbEndIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbEndIndex.Name = "tbEndIndex";
            this.tbEndIndex.Size = new System.Drawing.Size(100, 22);
            this.tbEndIndex.TabIndex = 6;
            // 
            // tbStartIndex
            // 
            this.tbStartIndex.BackColor = System.Drawing.SystemColors.Window;
            this.tbStartIndex.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStartIndex.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbStartIndex.Location = new System.Drawing.Point(127, 34);
            this.tbStartIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbStartIndex.Name = "tbStartIndex";
            this.tbStartIndex.Size = new System.Drawing.Size(100, 22);
            this.tbStartIndex.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(235, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // lblIndexFrom
            // 
            this.lblIndexFrom.AutoSize = true;
            this.lblIndexFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblIndexFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndexFrom.ForeColor = System.Drawing.Color.White;
            this.lblIndexFrom.Location = new System.Drawing.Point(32, 38);
            this.lblIndexFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIndexFrom.Name = "lblIndexFrom";
            this.lblIndexFrom.Size = new System.Drawing.Size(80, 14);
            this.lblIndexFrom.TabIndex = 5;
            this.lblIndexFrom.Text = "Index from";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(232, 383);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditPassBlockMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbEndIndex);
            this.Controls.Add(this.tbStartIndex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIndexFrom);
            this.Name = "EditPassBlockMetro";
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
        private Controls.IntTextBox tbEndIndex;
        private Controls.IntTextBox tbStartIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIndexFrom;
        private System.Windows.Forms.Button btnCancel;
    }
}
