namespace Anda.Fluid.App.LoadTrajectory
{
    partial class DialogCreatePattern
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPatternName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPatternName = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lswComponents = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectComp2 = new System.Windows.Forms.Button();
            this.btnSelectComp1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.txtOriginY = new System.Windows.Forms.TextBox();
            this.txtOriginX = new System.Windows.Forms.TextBox();
            this.btnGto2 = new System.Windows.Forms.Button();
            this.btnTeachMark2 = new System.Windows.Forms.Button();
            this.btnGto1 = new System.Windows.Forms.Button();
            this.btnTeachMark1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.txtComponent2 = new System.Windows.Forms.TextBox();
            this.txtComponent1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMark2Y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMark2X = new System.Windows.Forms.TextBox();
            this.txtMark1Y = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMark1X = new System.Windows.Forms.TextBox();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.camera = new Anda.Fluid.Domain.Vision.CameraControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(661, 675);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPatternName
            // 
            this.txtPatternName.Location = new System.Drawing.Point(136, 674);
            this.txtPatternName.Name = "txtPatternName";
            this.txtPatternName.Size = new System.Drawing.Size(200, 22);
            this.txtPatternName.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(758, 675);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPatternName
            // 
            this.lblPatternName.Location = new System.Drawing.Point(21, 677);
            this.lblPatternName.Name = "lblPatternName";
            this.lblPatternName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPatternName.Size = new System.Drawing.Size(110, 14);
            this.lblPatternName.TabIndex = 4;
            this.lblPatternName.Text = "PatternName:";
            this.lblPatternName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(353, 675);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 18;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "patternFile:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(123, 431);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(200, 22);
            this.txtFile.TabIndex = 20;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(345, 431);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.Text = "Load...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(532, 677);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(98, 23);
            this.btnImport.TabIndex = 22;
            this.btnImport.Text = "ImportCAD";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lswComponents);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.camera);
            this.splitContainer1.Size = new System.Drawing.Size(842, 423);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 23;
            // 
            // lswComponents
            // 
            this.lswComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lswComponents.Location = new System.Drawing.Point(0, 0);
            this.lswComponents.Name = "lswComponents";
            this.lswComponents.Size = new System.Drawing.Size(343, 423);
            this.lswComponents.TabIndex = 0;
            this.lswComponents.UseCompatibleStateImageBehavior = false;
            this.lswComponents.SelectedIndexChanged += new System.EventHandler(this.lswComponents_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectComp2);
            this.groupBox1.Controls.Add(this.btnSelectComp1);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPre);
            this.groupBox1.Controls.Add(this.txtOriginY);
            this.groupBox1.Controls.Add(this.txtOriginX);
            this.groupBox1.Controls.Add(this.btnGto2);
            this.groupBox1.Controls.Add(this.btnTeachMark2);
            this.groupBox1.Controls.Add(this.btnGto1);
            this.groupBox1.Controls.Add(this.btnTeachMark1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnCalculate);
            this.groupBox1.Controls.Add(this.btnTeach);
            this.groupBox1.Controls.Add(this.txtComponent2);
            this.groupBox1.Controls.Add(this.txtComponent1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMark2Y);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMark2X);
            this.groupBox1.Controls.Add(this.txtMark1Y);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMark1X);
            this.groupBox1.Location = new System.Drawing.Point(16, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 197);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原点计算";
            // 
            // btnSelectComp2
            // 
            this.btnSelectComp2.Location = new System.Drawing.Point(279, 63);
            this.btnSelectComp2.Name = "btnSelectComp2";
            this.btnSelectComp2.Size = new System.Drawing.Size(75, 23);
            this.btnSelectComp2.TabIndex = 28;
            this.btnSelectComp2.Text = "确定";
            this.btnSelectComp2.UseVisualStyleBackColor = true;
            this.btnSelectComp2.Click += new System.EventHandler(this.btnSelectComp2_Click);
            // 
            // btnSelectComp1
            // 
            this.btnSelectComp1.Location = new System.Drawing.Point(279, 39);
            this.btnSelectComp1.Name = "btnSelectComp1";
            this.btnSelectComp1.Size = new System.Drawing.Size(75, 23);
            this.btnSelectComp1.TabIndex = 27;
            this.btnSelectComp1.Text = "确定";
            this.btnSelectComp1.UseVisualStyleBackColor = true;
            this.btnSelectComp1.Click += new System.EventHandler(this.btnSelectComp1_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(460, 157);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 26;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(460, 125);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(75, 23);
            this.btnPre.TabIndex = 25;
            this.btnPre.Text = "上一个";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // txtOriginY
            // 
            this.txtOriginY.Location = new System.Drawing.Point(193, 155);
            this.txtOriginY.Name = "txtOriginY";
            this.txtOriginY.Size = new System.Drawing.Size(80, 22);
            this.txtOriginY.TabIndex = 24;
            // 
            // txtOriginX
            // 
            this.txtOriginX.Location = new System.Drawing.Point(102, 157);
            this.txtOriginX.Name = "txtOriginX";
            this.txtOriginX.Size = new System.Drawing.Size(80, 22);
            this.txtOriginX.TabIndex = 23;
            // 
            // btnGto2
            // 
            this.btnGto2.Location = new System.Drawing.Point(359, 125);
            this.btnGto2.Name = "btnGto2";
            this.btnGto2.Size = new System.Drawing.Size(75, 23);
            this.btnGto2.TabIndex = 22;
            this.btnGto2.Text = "到位置";
            this.btnGto2.UseVisualStyleBackColor = true;
            this.btnGto2.Click += new System.EventHandler(this.btnGto2_Click);
            // 
            // btnTeachMark2
            // 
            this.btnTeachMark2.Location = new System.Drawing.Point(278, 125);
            this.btnTeachMark2.Name = "btnTeachMark2";
            this.btnTeachMark2.Size = new System.Drawing.Size(75, 23);
            this.btnTeachMark2.TabIndex = 21;
            this.btnTeachMark2.Text = "示教";
            this.btnTeachMark2.UseVisualStyleBackColor = true;
            this.btnTeachMark2.Click += new System.EventHandler(this.btnTeachMark2_Click);
            // 
            // btnGto1
            // 
            this.btnGto1.Location = new System.Drawing.Point(359, 92);
            this.btnGto1.Name = "btnGto1";
            this.btnGto1.Size = new System.Drawing.Size(75, 23);
            this.btnGto1.TabIndex = 20;
            this.btnGto1.Text = "到位置";
            this.btnGto1.UseVisualStyleBackColor = true;
            this.btnGto1.Click += new System.EventHandler(this.btnGto1_Click);
            // 
            // btnTeachMark1
            // 
            this.btnTeachMark1.Location = new System.Drawing.Point(278, 93);
            this.btnTeachMark1.Name = "btnTeachMark1";
            this.btnTeachMark1.Size = new System.Drawing.Size(75, 23);
            this.btnTeachMark1.TabIndex = 19;
            this.btnTeachMark1.Text = "示教";
            this.btnTeachMark1.UseVisualStyleBackColor = true;
            this.btnTeachMark1.Click += new System.EventHandler(this.btnTeachMark1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 18;
            this.label6.Text = "Pattern原点:";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(360, 157);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 17;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(279, 156);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(75, 23);
            this.btnTeach.TabIndex = 14;
            this.btnTeach.Text = "示教";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // txtComponent2
            // 
            this.txtComponent2.Location = new System.Drawing.Point(103, 63);
            this.txtComponent2.Name = "txtComponent2";
            this.txtComponent2.Size = new System.Drawing.Size(170, 22);
            this.txtComponent2.TabIndex = 13;
            // 
            // txtComponent1
            // 
            this.txtComponent1.Location = new System.Drawing.Point(103, 39);
            this.txtComponent1.Name = "txtComponent1";
            this.txtComponent1.Size = new System.Drawing.Size(170, 22);
            this.txtComponent1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "元器件2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "元器件1:";
            // 
            // txtMark2Y
            // 
            this.txtMark2Y.Location = new System.Drawing.Point(193, 126);
            this.txtMark2Y.Name = "txtMark2Y";
            this.txtMark2Y.Size = new System.Drawing.Size(80, 22);
            this.txtMark2Y.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "元器件2位置:";
            // 
            // txtMark2X
            // 
            this.txtMark2X.Location = new System.Drawing.Point(102, 126);
            this.txtMark2X.Name = "txtMark2X";
            this.txtMark2X.Size = new System.Drawing.Size(80, 22);
            this.txtMark2X.TabIndex = 5;
            // 
            // txtMark1Y
            // 
            this.txtMark1Y.Location = new System.Drawing.Point(193, 94);
            this.txtMark1Y.Name = "txtMark1Y";
            this.txtMark1Y.Size = new System.Drawing.Size(80, 22);
            this.txtMark1Y.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "元器件1位置:";
            // 
            // txtMark1X
            // 
            this.txtMark1X.Location = new System.Drawing.Point(102, 94);
            this.txtMark1X.Name = "txtMark1X";
            this.txtMark1X.Size = new System.Drawing.Size(80, 22);
            this.txtMark1X.TabIndex = 0;
            // 
            // jogControl1
            // 
            this.jogControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(651, 467);
            this.jogControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(180, 158);
            this.jogControl1.TabIndex = 25;
            // 
            // camera
            // 
            this.camera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camera.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camera.Location = new System.Drawing.Point(0, 0);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(495, 423);
            this.camera.TabIndex = 0;
            // 
            // DialogCreatePattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 704);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtPatternName);
            this.Controls.Add(this.lblPatternName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogCreatePattern";
            this.Text = "DialogOffLineProgramming";
            this.Load += new System.EventHandler(this.TrajectoryDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPatternName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPatternName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lswComponents;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.TextBox txtComponent2;
        private System.Windows.Forms.TextBox txtComponent1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMark2Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMark2X;
        private System.Windows.Forms.TextBox txtMark1Y;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMark1X;
        private System.Windows.Forms.Button btnGto2;
        private System.Windows.Forms.Button btnTeachMark2;
        private System.Windows.Forms.Button btnGto1;
        private System.Windows.Forms.Button btnTeachMark1;
        private System.Windows.Forms.TextBox txtOriginY;
        private System.Windows.Forms.TextBox txtOriginX;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private Domain.Vision.CameraControl camera;
        private Domain.Motion.JogControl jogControl1;
        private System.Windows.Forms.Button btnSelectComp2;
        private System.Windows.Forms.Button btnSelectComp1;
    }
}