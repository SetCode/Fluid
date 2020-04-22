namespace Anda.Fluid.Domain.Dialogs.Soak
{
    partial class DialogSoakSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTeachXY = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocationX = new System.Windows.Forms.TextBox();
            this.txtLocationY = new System.Windows.Forms.TextBox();
            this.txtLocationZ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTeachZ = new System.Windows.Forms.Button();
            this.btnGtoXY = new System.Windows.Forms.Button();
            this.btnGtoZ = new System.Windows.Forms.Button();
            this.rdoValve1 = new System.Windows.Forms.RadioButton();
            this.rdoCamera = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTeachXY
            // 
            this.btnTeachXY.Location = new System.Drawing.Point(270, 21);
            this.btnTeachXY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTeachXY.Name = "btnTeachXY";
            this.btnTeachXY.Size = new System.Drawing.Size(69, 27);
            this.btnTeachXY.TabIndex = 0;
            this.btnTeachXY.Text = "示教";
            this.btnTeachXY.UseVisualStyleBackColor = true;
            this.btnTeachXY.Click += new System.EventHandler(this.btnTeachXY_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "浸泡位置";
            // 
            // txtLocationX
            // 
            this.txtLocationX.Location = new System.Drawing.Point(84, 23);
            this.txtLocationX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLocationX.Name = "txtLocationX";
            this.txtLocationX.Size = new System.Drawing.Size(83, 22);
            this.txtLocationX.TabIndex = 2;
            // 
            // txtLocationY
            // 
            this.txtLocationY.Location = new System.Drawing.Point(173, 23);
            this.txtLocationY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLocationY.Name = "txtLocationY";
            this.txtLocationY.Size = new System.Drawing.Size(83, 22);
            this.txtLocationY.TabIndex = 3;
            // 
            // txtLocationZ
            // 
            this.txtLocationZ.Location = new System.Drawing.Point(84, 55);
            this.txtLocationZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLocationZ.Name = "txtLocationZ";
            this.txtLocationZ.Size = new System.Drawing.Size(83, 22);
            this.txtLocationZ.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "浸泡高度";
            // 
            // btnTeachZ
            // 
            this.btnTeachZ.Location = new System.Drawing.Point(270, 58);
            this.btnTeachZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTeachZ.Name = "btnTeachZ";
            this.btnTeachZ.Size = new System.Drawing.Size(69, 27);
            this.btnTeachZ.TabIndex = 6;
            this.btnTeachZ.Text = "示教";
            this.btnTeachZ.UseVisualStyleBackColor = true;
            this.btnTeachZ.Click += new System.EventHandler(this.btnTeachZ_Click);
            // 
            // btnGtoXY
            // 
            this.btnGtoXY.Location = new System.Drawing.Point(348, 20);
            this.btnGtoXY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGtoXY.Name = "btnGtoXY";
            this.btnGtoXY.Size = new System.Drawing.Size(69, 27);
            this.btnGtoXY.TabIndex = 7;
            this.btnGtoXY.Text = "到位置";
            this.btnGtoXY.UseVisualStyleBackColor = true;
            this.btnGtoXY.Click += new System.EventHandler(this.btnGtoXY_Click);
            // 
            // btnGtoZ
            // 
            this.btnGtoZ.Location = new System.Drawing.Point(348, 58);
            this.btnGtoZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGtoZ.Name = "btnGtoZ";
            this.btnGtoZ.Size = new System.Drawing.Size(69, 27);
            this.btnGtoZ.TabIndex = 8;
            this.btnGtoZ.Text = "到位置";
            this.btnGtoZ.UseVisualStyleBackColor = true;
            this.btnGtoZ.Click += new System.EventHandler(this.btnGtoZ_Click);
            // 
            // rdoValve1
            // 
            this.rdoValve1.AutoSize = true;
            this.rdoValve1.Location = new System.Drawing.Point(6, 41);
            this.rdoValve1.Name = "rdoValve1";
            this.rdoValve1.Size = new System.Drawing.Size(38, 18);
            this.rdoValve1.TabIndex = 9;
            this.rdoValve1.TabStop = true;
            this.rdoValve1.Text = "阀";
            this.rdoValve1.UseVisualStyleBackColor = true;
            // 
            // rdoCamera
            // 
            this.rdoCamera.AutoSize = true;
            this.rdoCamera.Location = new System.Drawing.Point(6, 13);
            this.rdoCamera.Name = "rdoCamera";
            this.rdoCamera.Size = new System.Drawing.Size(51, 18);
            this.rdoCamera.TabIndex = 11;
            this.rdoCamera.TabStop = true;
            this.rdoCamera.Text = "相机";
            this.rdoCamera.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoValve1);
            this.groupBox1.Controls.Add(this.rdoCamera);
            this.groupBox1.Location = new System.Drawing.Point(447, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(73, 67);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(452, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(358, 121);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DialogSoakSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 156);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGtoZ);
            this.Controls.Add(this.btnGtoXY);
            this.Controls.Add(this.btnTeachZ);
            this.Controls.Add(this.txtLocationZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocationY);
            this.Controls.Add(this.txtLocationX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTeachXY);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogSoakSetting";
            this.Text = "浸泡设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeachXY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocationX;
        private System.Windows.Forms.TextBox txtLocationY;
        private System.Windows.Forms.TextBox txtLocationZ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTeachZ;
        private System.Windows.Forms.Button btnGtoXY;
        private System.Windows.Forms.Button btnGtoZ;
        private System.Windows.Forms.RadioButton rdoValve1;
        private System.Windows.Forms.RadioButton rdoCamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}