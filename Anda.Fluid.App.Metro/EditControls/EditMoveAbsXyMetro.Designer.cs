namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditMoveAbsXyMetro
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
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNeedle2 = new System.Windows.Forms.RadioButton();
            this.rbLaser = new System.Windows.Forms.RadioButton();
            this.rbNeedle1 = new System.Windows.Forms.RadioButton();
            this.rbCamera = new System.Windows.Forms.RadioButton();
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
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(219, 166);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 22;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(219, 138);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 23;
            this.btnSelect.Text = "示教";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.SystemColors.Window;
            this.tbY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbY.Location = new System.Drawing.Point(69, 166);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(144, 22);
            this.tbY.TabIndex = 20;
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.SystemColors.Window;
            this.tbX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbX.Location = new System.Drawing.Point(69, 138);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(145, 22);
            this.tbX.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(42, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "X:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox1.Controls.Add(this.rbNeedle2);
            this.groupBox1.Controls.Add(this.rbLaser);
            this.groupBox1.Controls.Add(this.rbNeedle1);
            this.groupBox1.Controls.Add(this.rbCamera);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(39, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 88);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // rbNeedle2
            // 
            this.rbNeedle2.AutoSize = true;
            this.rbNeedle2.Location = new System.Drawing.Point(152, 54);
            this.rbNeedle2.Name = "rbNeedle2";
            this.rbNeedle2.Size = new System.Drawing.Size(86, 18);
            this.rbNeedle2.TabIndex = 2;
            this.rbNeedle2.Text = "NEEDLE2";
            this.rbNeedle2.UseVisualStyleBackColor = true;
            // 
            // rbLaser
            // 
            this.rbLaser.AutoSize = true;
            this.rbLaser.Location = new System.Drawing.Point(152, 21);
            this.rbLaser.Name = "rbLaser";
            this.rbLaser.Size = new System.Drawing.Size(68, 18);
            this.rbLaser.TabIndex = 1;
            this.rbLaser.TabStop = true;
            this.rbLaser.Text = "LASER";
            this.rbLaser.UseVisualStyleBackColor = true;
            // 
            // rbNeedle1
            // 
            this.rbNeedle1.AutoSize = true;
            this.rbNeedle1.Location = new System.Drawing.Point(19, 54);
            this.rbNeedle1.Name = "rbNeedle1";
            this.rbNeedle1.Size = new System.Drawing.Size(86, 18);
            this.rbNeedle1.TabIndex = 0;
            this.rbNeedle1.Text = "NEEDLE1";
            this.rbNeedle1.UseVisualStyleBackColor = true;
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Checked = true;
            this.rbCamera.Location = new System.Drawing.Point(16, 21);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(80, 18);
            this.rbCamera.TabIndex = 0;
            this.rbCamera.TabStop = true;
            this.rbCamera.Text = "CAMERA";
            this.rbCamera.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(285, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditMoveAbsXyMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbY);
            this.Controls.Add(this.tbX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditMoveAbsXyMetro";
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNeedle2;
        private System.Windows.Forms.RadioButton rbLaser;
        private System.Windows.Forms.RadioButton rbNeedle1;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.Button btnCancel;
    }
}
