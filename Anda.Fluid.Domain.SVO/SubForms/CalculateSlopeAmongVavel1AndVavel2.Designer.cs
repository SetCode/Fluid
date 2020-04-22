namespace Anda.Fluid.Domain.SVO.SubForms
{
    partial class CalculateSlopeAmongVavel1AndVavel2
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
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.cameraControl = new Anda.Fluid.Domain.Vision.CameraControl();
            this.grpCorrectionMode = new System.Windows.Forms.GroupBox();
            this.rdoDispenseMode = new System.Windows.Forms.RadioButton();
            this.rdoPlasticineMode = new System.Windows.Forms.RadioButton();
            this.grpCorrectionAxis = new System.Windows.Forms.GroupBox();
            this.rdoAxisAandB = new System.Windows.Forms.RadioButton();
            this.rdoAxisB = new System.Windows.Forms.RadioButton();
            this.rdoAxisA = new System.Windows.Forms.RadioButton();
            this.grpVavelParam = new System.Windows.Forms.GroupBox();
            this.valveControl1 = new Anda.Fluid.Domain.Motion.ValveControl();
            this.btnEditDot = new System.Windows.Forms.Button();
            this.cbxDotStyle = new System.Windows.Forms.ComboBox();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.txtAxisBResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAxisAResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSprayTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.grpCorrectionMode.SuspendLayout();
            this.grpCorrectionAxis.SuspendLayout();
            this.grpVavelParam.SuspendLayout();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.grpResult);
            this.grpResultTest.Controls.Add(this.grpVavelParam);
            this.grpResultTest.Controls.Add(this.grpCorrectionAxis);
            this.grpResultTest.Controls.Add(this.grpCorrectionMode);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.cameraControl);
            this.pnlDisplay.Controls.Add(this.picDiagram);
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(1, 0);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(499, 395);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 8;
            this.picDiagram.TabStop = false;
            // 
            // cameraControl
            // 
            this.cameraControl.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = this.pnlDisplay.Size;
            this.cameraControl.TabIndex = 9;
            // 
            // grpCorrectionMode
            // 
            this.grpCorrectionMode.Controls.Add(this.rdoDispenseMode);
            this.grpCorrectionMode.Controls.Add(this.rdoPlasticineMode);
            this.grpCorrectionMode.Location = new System.Drawing.Point(7, 20);
            this.grpCorrectionMode.Name = "grpCorrectionMode";
            this.grpCorrectionMode.Size = new System.Drawing.Size(245, 55);
            this.grpCorrectionMode.TabIndex = 0;
            this.grpCorrectionMode.TabStop = false;
            this.grpCorrectionMode.Text = "Correction Mode";
            // 
            // rdoDispenseMode
            // 
            this.rdoDispenseMode.AutoSize = true;
            this.rdoDispenseMode.Location = new System.Drawing.Point(138, 23);
            this.rdoDispenseMode.Name = "rdoDispenseMode";
            this.rdoDispenseMode.Size = new System.Drawing.Size(101, 16);
            this.rdoDispenseMode.TabIndex = 1;
            this.rdoDispenseMode.TabStop = true;
            this.rdoDispenseMode.Text = "Dispense Mode";
            this.rdoDispenseMode.UseVisualStyleBackColor = true;
            this.rdoDispenseMode.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rdoPlasticineMode
            // 
            this.rdoPlasticineMode.AutoSize = true;
            this.rdoPlasticineMode.Location = new System.Drawing.Point(6, 23);
            this.rdoPlasticineMode.Name = "rdoPlasticineMode";
            this.rdoPlasticineMode.Size = new System.Drawing.Size(113, 16);
            this.rdoPlasticineMode.TabIndex = 0;
            this.rdoPlasticineMode.TabStop = true;
            this.rdoPlasticineMode.Text = "Plasticine Mode";
            this.rdoPlasticineMode.UseVisualStyleBackColor = true;
            this.rdoPlasticineMode.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // grpCorrectionAxis
            // 
            this.grpCorrectionAxis.Controls.Add(this.rdoAxisAandB);
            this.grpCorrectionAxis.Controls.Add(this.rdoAxisB);
            this.grpCorrectionAxis.Controls.Add(this.rdoAxisA);
            this.grpCorrectionAxis.Location = new System.Drawing.Point(7, 81);
            this.grpCorrectionAxis.Name = "grpCorrectionAxis";
            this.grpCorrectionAxis.Size = new System.Drawing.Size(245, 100);
            this.grpCorrectionAxis.TabIndex = 1;
            this.grpCorrectionAxis.TabStop = false;
            this.grpCorrectionAxis.Text = "Correction Axis";
            // 
            // rdoAxisAandB
            // 
            this.rdoAxisAandB.AutoSize = true;
            this.rdoAxisAandB.Location = new System.Drawing.Point(6, 64);
            this.rdoAxisAandB.Name = "rdoAxisAandB";
            this.rdoAxisAandB.Size = new System.Drawing.Size(71, 16);
            this.rdoAxisAandB.TabIndex = 2;
            this.rdoAxisAandB.TabStop = true;
            this.rdoAxisAandB.Text = "Axis A&&B";
            this.rdoAxisAandB.UseVisualStyleBackColor = true;
            this.rdoAxisAandB.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rdoAxisB
            // 
            this.rdoAxisB.AutoSize = true;
            this.rdoAxisB.Location = new System.Drawing.Point(138, 31);
            this.rdoAxisB.Name = "rdoAxisB";
            this.rdoAxisB.Size = new System.Drawing.Size(59, 16);
            this.rdoAxisB.TabIndex = 1;
            this.rdoAxisB.TabStop = true;
            this.rdoAxisB.Text = "Axis B";
            this.rdoAxisB.UseVisualStyleBackColor = true;
            this.rdoAxisB.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rdoAxisA
            // 
            this.rdoAxisA.AutoSize = true;
            this.rdoAxisA.Location = new System.Drawing.Point(6, 31);
            this.rdoAxisA.Name = "rdoAxisA";
            this.rdoAxisA.Size = new System.Drawing.Size(59, 16);
            this.rdoAxisA.TabIndex = 0;
            this.rdoAxisA.TabStop = true;
            this.rdoAxisA.Text = "Axis A";
            this.rdoAxisA.UseVisualStyleBackColor = true;
            this.rdoAxisA.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // grpVavelParam
            // 
            this.grpVavelParam.Controls.Add(this.nudSprayTime);
            this.grpVavelParam.Controls.Add(this.label3);
            this.grpVavelParam.Controls.Add(this.valveControl1);
            this.grpVavelParam.Controls.Add(this.btnEditDot);
            this.grpVavelParam.Controls.Add(this.cbxDotStyle);
            this.grpVavelParam.Location = new System.Drawing.Point(258, 20);
            this.grpVavelParam.Name = "grpVavelParam";
            this.grpVavelParam.Size = new System.Drawing.Size(238, 108);
            this.grpVavelParam.TabIndex = 2;
            this.grpVavelParam.TabStop = false;
            this.grpVavelParam.Text = "Vavel Param";
            // 
            // valveControl1
            // 
            this.valveControl1.Location = new System.Drawing.Point(107, 23);
            this.valveControl1.Name = "valveControl1";
            this.valveControl1.Size = new System.Drawing.Size(123, 54);
            this.valveControl1.TabIndex = 6;
            // 
            // btnEditDot
            // 
            this.btnEditDot.Location = new System.Drawing.Point(11, 54);
            this.btnEditDot.Name = "btnEditDot";
            this.btnEditDot.Size = new System.Drawing.Size(75, 23);
            this.btnEditDot.TabIndex = 5;
            this.btnEditDot.Text = "edit";
            this.btnEditDot.UseVisualStyleBackColor = true;
            this.btnEditDot.Click += new System.EventHandler(this.btnEditDot_Click);
            // 
            // cbxDotStyle
            // 
            this.cbxDotStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDotStyle.FormattingEnabled = true;
            this.cbxDotStyle.Location = new System.Drawing.Point(11, 22);
            this.cbxDotStyle.Name = "cbxDotStyle";
            this.cbxDotStyle.Size = new System.Drawing.Size(70, 20);
            this.cbxDotStyle.TabIndex = 4;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.txtAxisBResult);
            this.grpResult.Controls.Add(this.label2);
            this.grpResult.Controls.Add(this.txtAxisAResult);
            this.grpResult.Controls.Add(this.label1);
            this.grpResult.Location = new System.Drawing.Point(258, 134);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(238, 47);
            this.grpResult.TabIndex = 3;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Result";
            // 
            // txtAxisBResult
            // 
            this.txtAxisBResult.Location = new System.Drawing.Point(173, 16);
            this.txtAxisBResult.Name = "txtAxisBResult";
            this.txtAxisBResult.ReadOnly = true;
            this.txtAxisBResult.Size = new System.Drawing.Size(57, 21);
            this.txtAxisBResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Axis B:";
            // 
            // txtAxisAResult
            // 
            this.txtAxisAResult.Location = new System.Drawing.Point(57, 16);
            this.txtAxisAResult.Name = "txtAxisAResult";
            this.txtAxisAResult.ReadOnly = true;
            this.txtAxisAResult.Size = new System.Drawing.Size(57, 21);
            this.txtAxisAResult.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Axis A:";
            // 
            // nudSprayTime
            // 
            this.nudSprayTime.Location = new System.Drawing.Point(136, 81);
            this.nudSprayTime.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudSprayTime.Name = "nudSprayTime";
            this.nudSprayTime.Size = new System.Drawing.Size(84, 21);
            this.nudSprayTime.TabIndex = 8;
            this.nudSprayTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "非喷射型阀出胶时间:";
            // 
            // CalculateSlopeAmongVavel1AndVavel2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 657);
            this.Name = "CalculateSlopeAmongVavel1AndVavel2";
            this.Text = "Anda Fluid - Calculate slope among Vavel1 and Vavel2";
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.pnlDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.grpCorrectionMode.ResumeLayout(false);
            this.grpCorrectionMode.PerformLayout();
            this.grpCorrectionAxis.ResumeLayout(false);
            this.grpCorrectionAxis.PerformLayout();
            this.grpVavelParam.ResumeLayout(false);
            this.grpVavelParam.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDiagram;
        private Vision.CameraControl cameraControl;
        private System.Windows.Forms.GroupBox grpCorrectionAxis;
        private System.Windows.Forms.RadioButton rdoAxisAandB;
        private System.Windows.Forms.RadioButton rdoAxisB;
        private System.Windows.Forms.RadioButton rdoAxisA;
        private System.Windows.Forms.GroupBox grpCorrectionMode;
        private System.Windows.Forms.RadioButton rdoDispenseMode;
        private System.Windows.Forms.RadioButton rdoPlasticineMode;
        private System.Windows.Forms.GroupBox grpVavelParam;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.TextBox txtAxisBResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAxisAResult;
        private System.Windows.Forms.Label label1;
        private Motion.ValveControl valveControl1;
        private System.Windows.Forms.Button btnEditDot;
        private System.Windows.Forms.ComboBox cbxDotStyle;
        private System.Windows.Forms.NumericUpDown nudSprayTime;
        private System.Windows.Forms.Label label3;
    }
}