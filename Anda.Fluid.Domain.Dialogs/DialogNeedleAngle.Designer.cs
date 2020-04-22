using Anda.Fluid.Domain.Motion;

namespace Anda.Fluid.Domain.Dialogs
{
    partial class DialogNeedleAngle
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
            this.grpOperation = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.grpMotion = new System.Windows.Forms.GroupBox();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.grpResultTest = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNeedleOffset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRotated = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.txtInter1X = new System.Windows.Forms.TextBox();
            this.txtInter2Y = new System.Windows.Forms.TextBox();
            this.txtInter1Y = new System.Windows.Forms.TextBox();
            this.txtInter2X = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPosZDown = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPosZUp = new System.Windows.Forms.TextBox();
            this.txtP1X = new System.Windows.Forms.TextBox();
            this.txtP2Y = new System.Windows.Forms.TextBox();
            this.txtP1Y = new System.Windows.Forms.TextBox();
            this.txtP2X = new System.Windows.Forms.TextBox();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpDisplay = new System.Windows.Forms.GroupBox();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.grpOperation.SuspendLayout();
            this.grpMotion.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            this.pnlDisplay.SuspendLayout();
            this.grpDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOperation
            // 
            this.grpOperation.Controls.Add(this.btnHelp);
            this.grpOperation.Controls.Add(this.btnNext);
            this.grpOperation.Controls.Add(this.btnDone);
            this.grpOperation.Controls.Add(this.btnCancel);
            this.grpOperation.Controls.Add(this.btnTeach);
            this.grpOperation.Controls.Add(this.btnPrev);
            this.grpOperation.Location = new System.Drawing.Point(573, 33);
            this.grpOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpOperation.Name = "grpOperation";
            this.grpOperation.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpOperation.Size = new System.Drawing.Size(180, 198);
            this.grpOperation.TabIndex = 15;
            this.grpOperation.TabStop = false;
            this.grpOperation.Text = "Operation";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(56, 128);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(74, 23);
            this.btnHelp.TabIndex = 20;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(104, 23);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(65, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(56, 92);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(74, 23);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(56, 164);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(56, 56);
            this.btnTeach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(74, 23);
            this.btnTeach.TabIndex = 4;
            this.btnTeach.Text = "Teach";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(12, 23);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(65, 23);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // grpMotion
            // 
            this.grpMotion.Controls.Add(this.jogControl1);
            this.grpMotion.Location = new System.Drawing.Point(566, 374);
            this.grpMotion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpMotion.Name = "grpMotion";
            this.grpMotion.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpMotion.Size = new System.Drawing.Size(196, 169);
            this.grpMotion.TabIndex = 17;
            this.grpMotion.TabStop = false;
            this.grpMotion.Text = "Motion";
            // 
            // jogControl1
            // 
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(8, 15);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(179, 155);
            this.jogControl1.TabIndex = 0;
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.groupBox2);
            this.grpResultTest.Controls.Add(this.groupBox1);
            this.grpResultTest.Location = new System.Drawing.Point(7, 332);
            this.grpResultTest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpResultTest.Name = "grpResultTest";
            this.grpResultTest.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpResultTest.Size = new System.Drawing.Size(552, 211);
            this.grpResultTest.TabIndex = 20;
            this.grpResultTest.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtNeedleOffset);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRotated);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtAngle);
            this.groupBox2.Controls.Add(this.txtInter1X);
            this.groupBox2.Controls.Add(this.txtInter2Y);
            this.groupBox2.Controls.Add(this.txtInter1Y);
            this.groupBox2.Controls.Add(this.txtInter2X);
            this.groupBox2.Location = new System.Drawing.Point(254, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 186);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // label11
            // 
            this.label11.CausesValidation = false;
            this.label11.Location = new System.Drawing.Point(10, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 14);
            this.label11.TabIndex = 52;
            this.label11.Text = "NeedleOffset:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNeedleOffset
            // 
            this.txtNeedleOffset.Location = new System.Drawing.Point(112, 139);
            this.txtNeedleOffset.Name = "txtNeedleOffset";
            this.txtNeedleOffset.Size = new System.Drawing.Size(67, 22);
            this.txtNeedleOffset.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.CausesValidation = false;
            this.label6.Location = new System.Drawing.Point(10, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 14);
            this.label6.TabIndex = 50;
            this.label6.Text = "NeedleRotated:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRotated
            // 
            this.txtRotated.Location = new System.Drawing.Point(112, 111);
            this.txtRotated.Name = "txtRotated";
            this.txtRotated.Size = new System.Drawing.Size(67, 22);
            this.txtRotated.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.CausesValidation = false;
            this.label2.Location = new System.Drawing.Point(10, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 14);
            this.label2.TabIndex = 43;
            this.label2.Text = "Intersection1:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.CausesValidation = false;
            this.label5.Location = new System.Drawing.Point(10, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 14);
            this.label5.TabIndex = 42;
            this.label5.Text = "LightAngle:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.CausesValidation = false;
            this.label7.Location = new System.Drawing.Point(10, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 14);
            this.label7.TabIndex = 44;
            this.label7.Text = "Intersection2:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(112, 83);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(67, 22);
            this.txtAngle.TabIndex = 49;
            // 
            // txtInter1X
            // 
            this.txtInter1X.Location = new System.Drawing.Point(112, 25);
            this.txtInter1X.Name = "txtInter1X";
            this.txtInter1X.Size = new System.Drawing.Size(67, 22);
            this.txtInter1X.TabIndex = 45;
            // 
            // txtInter2Y
            // 
            this.txtInter2Y.Location = new System.Drawing.Point(188, 52);
            this.txtInter2Y.Name = "txtInter2Y";
            this.txtInter2Y.Size = new System.Drawing.Size(67, 22);
            this.txtInter2Y.TabIndex = 48;
            // 
            // txtInter1Y
            // 
            this.txtInter1Y.Location = new System.Drawing.Point(188, 25);
            this.txtInter1Y.Name = "txtInter1Y";
            this.txtInter1Y.Size = new System.Drawing.Size(67, 22);
            this.txtInter1Y.TabIndex = 46;
            // 
            // txtInter2X
            // 
            this.txtInter2X.Location = new System.Drawing.Point(112, 52);
            this.txtInter2X.Name = "txtInter2X";
            this.txtInter2X.Size = new System.Drawing.Size(67, 22);
            this.txtInter2X.TabIndex = 47;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudTolerance);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtPosZDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPosZUp);
            this.groupBox1.Controls.Add(this.txtP1X);
            this.groupBox1.Controls.Add(this.txtP2Y);
            this.groupBox1.Controls.Add(this.txtP1Y);
            this.groupBox1.Controls.Add(this.txtP2X);
            this.groupBox1.Location = new System.Drawing.Point(8, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 187);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Teach";
            // 
            // nudTolerance
            // 
            this.nudTolerance.Location = new System.Drawing.Point(82, 143);
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(64, 22);
            this.nudTolerance.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(2, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 18);
            this.label10.TabIndex = 39;
            this.label10.Text = "Tolerance:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(-1, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 18);
            this.label9.TabIndex = 37;
            this.label9.Text = "PosZDown:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPosZDown
            // 
            this.txtPosZDown.Location = new System.Drawing.Point(82, 108);
            this.txtPosZDown.Name = "txtPosZDown";
            this.txtPosZDown.Size = new System.Drawing.Size(67, 22);
            this.txtPosZDown.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 29;
            this.label3.Text = "StartPos:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "PosZUp:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 31;
            this.label4.Text = "EndPos:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPosZUp
            // 
            this.txtPosZUp.Location = new System.Drawing.Point(82, 81);
            this.txtPosZUp.Name = "txtPosZUp";
            this.txtPosZUp.Size = new System.Drawing.Size(67, 22);
            this.txtPosZUp.TabIndex = 36;
            // 
            // txtP1X
            // 
            this.txtP1X.Location = new System.Drawing.Point(82, 22);
            this.txtP1X.Name = "txtP1X";
            this.txtP1X.Size = new System.Drawing.Size(67, 22);
            this.txtP1X.TabIndex = 32;
            // 
            // txtP2Y
            // 
            this.txtP2Y.Location = new System.Drawing.Point(158, 49);
            this.txtP2Y.Name = "txtP2Y";
            this.txtP2Y.Size = new System.Drawing.Size(67, 22);
            this.txtP2Y.TabIndex = 35;
            // 
            // txtP1Y
            // 
            this.txtP1Y.Location = new System.Drawing.Point(158, 22);
            this.txtP1Y.Name = "txtP1Y";
            this.txtP1Y.Size = new System.Drawing.Size(67, 22);
            this.txtP1Y.TabIndex = 33;
            // 
            // txtP2X
            // 
            this.txtP2X.Location = new System.Drawing.Point(82, 49);
            this.txtP2X.Name = "txtP2X";
            this.txtP2X.Size = new System.Drawing.Size(67, 22);
            this.txtP2X.TabIndex = 34;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.label8);
            this.pnlDisplay.Location = new System.Drawing.Point(8, 14);
            this.pnlDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(534, 252);
            this.pnlDisplay.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(26, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(362, 185);
            this.label8.TabIndex = 0;
            this.label8.Text = "需要示教两个点\r\n起始点和结束点及Z轴的高度\r\n\r\n具体步骤：\r\n1、首先在光纤的一边示教第一个点位和Z轴高度\r\n\r\n2、然后在光纤的另一边示教第二个点\r\n\r\n3" +
    "、示教完成后点击下一步，执行校准过程\r\n\r\n注意：\r\n示教Z轴的高度时，请确保光纤的光束可以穿过针嘴的缺口\r\n";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(11, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 16);
            this.lblTitle.TabIndex = 22;
            this.lblTitle.Text = "Title";
            // 
            // grpDisplay
            // 
            this.grpDisplay.Controls.Add(this.pnlDisplay);
            this.grpDisplay.Location = new System.Drawing.Point(7, 30);
            this.grpDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpDisplay.Size = new System.Drawing.Size(552, 274);
            this.grpDisplay.TabIndex = 23;
            this.grpDisplay.TabStop = false;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionVControl1.Location = new System.Drawing.Point(566, 255);
            this.positionVControl1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(196, 108);
            this.positionVControl1.TabIndex = 24;
            // 
            // DialogNeedleAngle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 551);
            this.Controls.Add(this.positionVControl1);
            this.Controls.Add(this.grpResultTest);
            this.Controls.Add(this.grpDisplay);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpMotion);
            this.Controls.Add(this.grpOperation);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogNeedleAngle";
            this.Text = "TeachFormBase";
            this.Load += new System.EventHandler(this.DialogNeedleAngle_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpMotion.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            this.pnlDisplay.ResumeLayout(false);
            this.grpDisplay.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox grpOperation;
        public System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpMotion;
        private System.Windows.Forms.GroupBox grpDisplay;
        public System.Windows.Forms.Button btnHelp;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnTeach;
        public System.Windows.Forms.Button btnPrev;
        public System.Windows.Forms.GroupBox grpResultTest;
        public System.Windows.Forms.Panel pnlDisplay;
        public System.Windows.Forms.Label lblTitle;
        private JogControl jogControl1;
        private PositionVControl positionVControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.TextBox txtInter1X;
        private System.Windows.Forms.TextBox txtInter2Y;
        private System.Windows.Forms.TextBox txtInter1Y;
        private System.Windows.Forms.TextBox txtInter2X;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPosZUp;
        private System.Windows.Forms.TextBox txtP1X;
        private System.Windows.Forms.TextBox txtP2Y;
        private System.Windows.Forms.TextBox txtP1Y;
        private System.Windows.Forms.TextBox txtP2X;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRotated;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPosZDown;
        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNeedleOffset;
    }
}