namespace Anda.Fluid.App
{
    partial class ModifyCompForm
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
            this.txtCompName = new System.Windows.Forms.TextBox();
            this.btnFindComp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nudRotation = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoComponent = new System.Windows.Forms.RadioButton();
            this.rdoDesign = new System.Windows.Forms.RadioButton();
            this.txtCompDesign = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(91, 41);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Size = new System.Drawing.Size(151, 22);
            this.txtCompName.TabIndex = 0;
            // 
            // btnFindComp
            // 
            this.btnFindComp.Location = new System.Drawing.Point(254, 69);
            this.btnFindComp.Name = "btnFindComp";
            this.btnFindComp.Size = new System.Drawing.Size(75, 23);
            this.btnFindComp.TabIndex = 1;
            this.btnFindComp.Text = "查找";
            this.btnFindComp.UseVisualStyleBackColor = true;
            this.btnFindComp.Click += new System.EventHandler(this.btnFindComp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 143);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(420, 370);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // nudRotation
            // 
            this.nudRotation.DecimalPlaces = 2;
            this.nudRotation.Location = new System.Drawing.Point(95, 110);
            this.nudRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudRotation.Name = "nudRotation";
            this.nudRotation.Size = new System.Drawing.Size(151, 22);
            this.nudRotation.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "元器件名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "旋转角度:";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(357, 76);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 61);
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "确认修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(263, 110);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(75, 23);
            this.btnRotate.TabIndex = 7;
            this.btnRotate.Text = "进行旋转";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoComponent);
            this.groupBox1.Controls.Add(this.rdoDesign);
            this.groupBox1.Controls.Add(this.txtCompDesign);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCompName);
            this.groupBox1.Controls.Add(this.btnFindComp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 102);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找";
            // 
            // rdoComponent
            // 
            this.rdoComponent.AutoSize = true;
            this.rdoComponent.Location = new System.Drawing.Point(173, 17);
            this.rdoComponent.Name = "rdoComponent";
            this.rdoComponent.Size = new System.Drawing.Size(64, 18);
            this.rdoComponent.TabIndex = 8;
            this.rdoComponent.TabStop = true;
            this.rdoComponent.Text = "元器件";
            this.rdoComponent.UseVisualStyleBackColor = true;
            // 
            // rdoDesign
            // 
            this.rdoDesign.AutoSize = true;
            this.rdoDesign.Location = new System.Drawing.Point(91, 17);
            this.rdoDesign.Name = "rdoDesign";
            this.rdoDesign.Size = new System.Drawing.Size(51, 18);
            this.rdoDesign.TabIndex = 7;
            this.rdoDesign.TabStop = true;
            this.rdoDesign.Text = "符号";
            this.rdoDesign.UseVisualStyleBackColor = true;
            // 
            // txtCompDesign
            // 
            this.txtCompDesign.Location = new System.Drawing.Point(91, 69);
            this.txtCompDesign.Name = "txtCompDesign";
            this.txtCompDesign.Size = new System.Drawing.Size(151, 22);
            this.txtCompDesign.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "元器件符号:";
            // 
            // ModifyCompForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 518);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudRotation);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ModifyCompForm";
            this.Text = "修改元器件旋转角度";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompName;
        private System.Windows.Forms.Button btnFindComp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown nudRotation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoComponent;
        private System.Windows.Forms.RadioButton rdoDesign;
        private System.Windows.Forms.TextBox txtCompDesign;
        private System.Windows.Forms.Label label3;
    }
}