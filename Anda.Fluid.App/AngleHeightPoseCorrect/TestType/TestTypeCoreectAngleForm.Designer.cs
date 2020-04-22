namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    partial class TestTypeCoreectAngleForm
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
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValveCameraOffset = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGap = new System.Windows.Forms.TextBox();
            this.txtDispenseValveOffset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPreStep = new System.Windows.Forms.Button();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtStandardZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPosture = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraControl1
            // 
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(2, 1);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(859, 463);
            this.cameraControl1.TabIndex = 0;
            // 
            // jogControl1
            // 
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(684, 608);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(180, 158);
            this.jogControl1.TabIndex = 1;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionVControl1.Location = new System.Drawing.Point(684, 470);
            this.positionVControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(177, 80);
            this.positionVControl1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "角度:";
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(101, 47);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(100, 22);
            this.txtAngle.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "阀-相机偏移:";
            // 
            // txtValveCameraOffset
            // 
            this.txtValveCameraOffset.Location = new System.Drawing.Point(101, 79);
            this.txtValveCameraOffset.Name = "txtValveCameraOffset";
            this.txtValveCameraOffset.Size = new System.Drawing.Size(100, 22);
            this.txtValveCameraOffset.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "距板高度:";
            // 
            // txtGap
            // 
            this.txtGap.Location = new System.Drawing.Point(101, 145);
            this.txtGap.Name = "txtGap";
            this.txtGap.Size = new System.Drawing.Size(100, 22);
            this.txtGap.TabIndex = 11;
            // 
            // txtDispenseValveOffset
            // 
            this.txtDispenseValveOffset.Location = new System.Drawing.Point(101, 177);
            this.txtDispenseValveOffset.Name = "txtDispenseValveOffset";
            this.txtDispenseValveOffset.Size = new System.Drawing.Size(100, 22);
            this.txtDispenseValveOffset.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 12;
            this.label5.Text = "胶点-阀偏移:";
            // 
            // btnPreStep
            // 
            this.btnPreStep.Location = new System.Drawing.Point(6, 21);
            this.btnPreStep.Name = "btnPreStep";
            this.btnPreStep.Size = new System.Drawing.Size(75, 23);
            this.btnPreStep.TabIndex = 14;
            this.btnPreStep.Text = "上一阶段";
            this.btnPreStep.UseVisualStyleBackColor = true;
            this.btnPreStep.Click += new System.EventHandler(this.btnPreStep_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(125, 21);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(75, 23);
            this.btnNextStep.TabIndex = 15;
            this.btnNextStep.Text = "下一阶段";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(126, 209);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 17;
            this.btnContinue.Text = "继续添加";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(7, 209);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "确认添加";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(2, 470);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 296);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阶段操作区域";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(5, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 272);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPreStep);
            this.groupBox2.Controls.Add(this.btnNextStep);
            this.groupBox2.Location = new System.Drawing.Point(467, 470);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "阶段切换";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPosture);
            this.groupBox3.Controls.Add(this.txtStandardZ);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnOk);
            this.groupBox3.Controls.Add(this.btnContinue);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtDispenseValveOffset);
            this.groupBox3.Controls.Add(this.txtAngle);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtGap);
            this.groupBox3.Controls.Add(this.txtValveCameraOffset);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(466, 526);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 240);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "校准结果和操作";
            // 
            // txtStandardZ
            // 
            this.txtStandardZ.Location = new System.Drawing.Point(101, 112);
            this.txtStandardZ.Name = "txtStandardZ";
            this.txtStandardZ.Size = new System.Drawing.Size(100, 22);
            this.txtStandardZ.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "标准高度:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 14);
            this.label6.TabIndex = 20;
            this.label6.Text = "倾斜姿态:";
            // 
            // txtPosture
            // 
            this.txtPosture.Location = new System.Drawing.Point(101, 16);
            this.txtPosture.Name = "txtPosture";
            this.txtPosture.Size = new System.Drawing.Size(100, 22);
            this.txtPosture.TabIndex = 21;
            // 
            // TestTypeCoreectAngleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 767);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.positionVControl1);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.cameraControl1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TestTypeCoreectAngleForm";
            this.Text = "TestTypeCoreectAngleForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Domain.Vision.CameraControl cameraControl1;
        private Domain.Motion.JogControl jogControl1;
        private Domain.Motion.PositionVControl positionVControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValveCameraOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGap;
        private System.Windows.Forms.TextBox txtDispenseValveOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPreStep;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtStandardZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPosture;
    }
}