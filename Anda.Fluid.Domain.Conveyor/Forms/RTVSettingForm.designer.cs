namespace Anda.Fluid.Domain.Conveyor.Forms
{
    partial class RTVSettingForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpConveyorBack = new System.Windows.Forms.Button();
            this.btnUpConveyorForward = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDownConveyorBack = new System.Windows.Forms.Button();
            this.btnDownConveyorForward = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpeed = new Anda.Fluid.Controls.DoubleTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpConveyorBack);
            this.groupBox1.Controls.Add(this.btnUpConveyorForward);
            this.groupBox1.Location = new System.Drawing.Point(9, 44);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(251, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上层轨道";
            // 
            // btnUpConveyorBack
            // 
            this.btnUpConveyorBack.Location = new System.Drawing.Point(156, 31);
            this.btnUpConveyorBack.Name = "btnUpConveyorBack";
            this.btnUpConveyorBack.Size = new System.Drawing.Size(75, 23);
            this.btnUpConveyorBack.TabIndex = 1;
            this.btnUpConveyorBack.Text = "宽度-";
            this.btnUpConveyorBack.UseVisualStyleBackColor = true;
            this.btnUpConveyorBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseDown);
            this.btnUpConveyorBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseUp);
            // 
            // btnUpConveyorForward
            // 
            this.btnUpConveyorForward.Location = new System.Drawing.Point(18, 31);
            this.btnUpConveyorForward.Name = "btnUpConveyorForward";
            this.btnUpConveyorForward.Size = new System.Drawing.Size(75, 23);
            this.btnUpConveyorForward.TabIndex = 0;
            this.btnUpConveyorForward.Text = "宽度+";
            this.btnUpConveyorForward.UseVisualStyleBackColor = true;
            this.btnUpConveyorForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseDown);
            this.btnUpConveyorForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDownConveyorBack);
            this.groupBox2.Controls.Add(this.btnDownConveyorForward);
            this.groupBox2.Location = new System.Drawing.Point(9, 117);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(251, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下层轨道";
            // 
            // btnDownConveyorBack
            // 
            this.btnDownConveyorBack.Location = new System.Drawing.Point(156, 37);
            this.btnDownConveyorBack.Name = "btnDownConveyorBack";
            this.btnDownConveyorBack.Size = new System.Drawing.Size(75, 23);
            this.btnDownConveyorBack.TabIndex = 1;
            this.btnDownConveyorBack.Text = "宽度-";
            this.btnDownConveyorBack.UseVisualStyleBackColor = true;
            this.btnDownConveyorBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseDown);
            this.btnDownConveyorBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseUp);
            // 
            // btnDownConveyorForward
            // 
            this.btnDownConveyorForward.Location = new System.Drawing.Point(18, 37);
            this.btnDownConveyorForward.Name = "btnDownConveyorForward";
            this.btnDownConveyorForward.Size = new System.Drawing.Size(75, 23);
            this.btnDownConveyorForward.TabIndex = 0;
            this.btnDownConveyorForward.Text = "宽度+";
            this.btnDownConveyorForward.UseVisualStyleBackColor = true;
            this.btnDownConveyorForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseDown);
            this.btnDownConveyorForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUpConveyorForward_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "调宽速度(mm/s):";
            // 
            // txtSpeed
            // 
            this.txtSpeed.BackColor = System.Drawing.Color.White;
            this.txtSpeed.Location = new System.Drawing.Point(160, 15);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(100, 22);
            this.txtSpeed.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtSpeed);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(8, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 192);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轨道调宽";
            // 
            // RTVSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 203);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RTVSettingForm";
            this.Text = "RTV轨道设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RTVSettingForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpConveyorBack;
        private System.Windows.Forms.Button btnUpConveyorForward;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDownConveyorBack;
        private System.Windows.Forms.Button btnDownConveyorForward;
        private System.Windows.Forms.Label label1;
        private Controls.DoubleTextBox txtSpeed;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}