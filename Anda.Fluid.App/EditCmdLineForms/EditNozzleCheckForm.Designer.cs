namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditNozzleCheckForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDotLocation = new System.Windows.Forms.Label();
            this.tbLocationX = new Anda.Fluid.Controls.DoubleTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLocationY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.cbWeightControl = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnEditDotParams = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDotType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnModelEdit = new System.Windows.Forms.Button();
            this.cbxIsGlobal = new System.Windows.Forms.CheckBox();
            this.btnAutoDispense = new System.Windows.Forms.Button();
            this.rbnModelFind = new System.Windows.Forms.RadioButton();
            this.rbnGrayScale = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnNgAlarm = new System.Windows.Forms.RadioButton();
            this.rbnOkAlarm = new System.Windows.Forms.RadioButton();
            this.nudSprayTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).BeginInit();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.groupBox2);
            this.gbx1.Controls.Add(this.groupBox1);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.nudSprayTime);
            this.gbx2.Controls.Add(this.label4);
            this.gbx2.Controls.Add(this.btnAutoDispense);
            this.gbx2.Controls.Add(this.cbxIsGlobal);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.lblDotLocation);
            this.gbx2.Controls.Add(this.tbLocationX);
            this.gbx2.Controls.Add(this.label2);
            this.gbx2.Controls.Add(this.tbLocationY);
            this.gbx2.Controls.Add(this.tbWeight);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.cbWeightControl);
            this.gbx2.Controls.Add(this.btnSelect);
            this.gbx2.Controls.Add(this.btnEditDotParams);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Controls.Add(this.comboBoxDotType);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(418, 214);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 36;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDotLocation
            // 
            this.lblDotLocation.AutoSize = true;
            this.lblDotLocation.Location = new System.Drawing.Point(8, 14);
            this.lblDotLocation.Name = "lblDotLocation";
            this.lblDotLocation.Size = new System.Drawing.Size(95, 14);
            this.lblDotLocation.TabIndex = 24;
            this.lblDotLocation.Text = "Dot Location:";
            // 
            // tbLocationX
            // 
            this.tbLocationX.BackColor = System.Drawing.Color.White;
            this.tbLocationX.Location = new System.Drawing.Point(29, 31);
            this.tbLocationX.Name = "tbLocationX";
            this.tbLocationX.Size = new System.Drawing.Size(80, 22);
            this.tbLocationX.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 34;
            this.label2.Text = "mg";
            // 
            // tbLocationY
            // 
            this.tbLocationY.BackColor = System.Drawing.Color.White;
            this.tbLocationY.Location = new System.Drawing.Point(115, 31);
            this.tbLocationY.Name = "tbLocationY";
            this.tbLocationY.Size = new System.Drawing.Size(80, 22);
            this.tbLocationY.TabIndex = 26;
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(142, 125);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(63, 22);
            this.tbWeight.TabIndex = 33;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(282, 30);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 27;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // cbWeightControl
            // 
            this.cbWeightControl.AutoSize = true;
            this.cbWeightControl.Location = new System.Drawing.Point(11, 127);
            this.cbWeightControl.Name = "cbWeightControl";
            this.cbWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbWeightControl.TabIndex = 32;
            this.cbWeightControl.Text = "Weight Control";
            this.cbWeightControl.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(201, 30);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 28;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnEditDotParams
            // 
            this.btnEditDotParams.Location = new System.Drawing.Point(189, 81);
            this.btnEditDotParams.Name = "btnEditDotParams";
            this.btnEditDotParams.Size = new System.Drawing.Size(75, 23);
            this.btnEditDotParams.TabIndex = 31;
            this.btnEditDotParams.Text = "Edit";
            this.btnEditDotParams.UseVisualStyleBackColor = true;
            this.btnEditDotParams.Click += new System.EventHandler(this.btnEditDotParams_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 29;
            this.label1.Text = "Dot Style:";
            // 
            // comboBoxDotType
            // 
            this.comboBoxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDotType.FormattingEnabled = true;
            this.comboBoxDotType.Location = new System.Drawing.Point(29, 82);
            this.comboBoxDotType.Name = "comboBoxDotType";
            this.comboBoxDotType.Size = new System.Drawing.Size(152, 22);
            this.comboBoxDotType.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 14);
            this.label3.TabIndex = 37;
            this.label3.Text = "Image detection：";
            // 
            // btnModelEdit
            // 
            this.btnModelEdit.Location = new System.Drawing.Point(153, 60);
            this.btnModelEdit.Name = "btnModelEdit";
            this.btnModelEdit.Size = new System.Drawing.Size(75, 23);
            this.btnModelEdit.TabIndex = 38;
            this.btnModelEdit.Text = "Edit";
            this.btnModelEdit.UseVisualStyleBackColor = true;
            this.btnModelEdit.Click += new System.EventHandler(this.btnModelEdit_Click);
            // 
            // cbxIsGlobal
            // 
            this.cbxIsGlobal.AutoSize = true;
            this.cbxIsGlobal.Location = new System.Drawing.Point(11, 205);
            this.cbxIsGlobal.Name = "cbxIsGlobal";
            this.cbxIsGlobal.Size = new System.Drawing.Size(133, 18);
            this.cbxIsGlobal.TabIndex = 39;
            this.cbxIsGlobal.Text = "Global detection";
            this.cbxIsGlobal.UseVisualStyleBackColor = true;
            // 
            // btnAutoDispense
            // 
            this.btnAutoDispense.Location = new System.Drawing.Point(363, 30);
            this.btnAutoDispense.Name = "btnAutoDispense";
            this.btnAutoDispense.Size = new System.Drawing.Size(75, 23);
            this.btnAutoDispense.TabIndex = 40;
            this.btnAutoDispense.Text = "Auto";
            this.btnAutoDispense.UseVisualStyleBackColor = true;
            this.btnAutoDispense.Click += new System.EventHandler(this.btnAutoDispense_Click);
            // 
            // rbnModelFind
            // 
            this.rbnModelFind.AutoSize = true;
            this.rbnModelFind.Location = new System.Drawing.Point(145, 29);
            this.rbnModelFind.Name = "rbnModelFind";
            this.rbnModelFind.Size = new System.Drawing.Size(92, 18);
            this.rbnModelFind.TabIndex = 41;
            this.rbnModelFind.TabStop = true;
            this.rbnModelFind.Text = "ModelFind";
            this.rbnModelFind.UseVisualStyleBackColor = true;
            // 
            // rbnGrayScale
            // 
            this.rbnGrayScale.AutoSize = true;
            this.rbnGrayScale.Location = new System.Drawing.Point(22, 29);
            this.rbnGrayScale.Name = "rbnGrayScale";
            this.rbnGrayScale.Size = new System.Drawing.Size(93, 18);
            this.rbnGrayScale.TabIndex = 42;
            this.rbnGrayScale.TabStop = true;
            this.rbnGrayScale.Text = "GrayScale";
            this.rbnGrayScale.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnGrayScale);
            this.groupBox1.Controls.Add(this.rbnModelFind);
            this.groupBox1.Controls.Add(this.btnModelEdit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbnNgAlarm);
            this.groupBox2.Controls.Add(this.rbnOkAlarm);
            this.groupBox2.Location = new System.Drawing.Point(8, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 75);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skip";
            // 
            // rbnNgAlarm
            // 
            this.rbnNgAlarm.AutoSize = true;
            this.rbnNgAlarm.Location = new System.Drawing.Point(22, 33);
            this.rbnNgAlarm.Name = "rbnNgAlarm";
            this.rbnNgAlarm.Size = new System.Drawing.Size(88, 18);
            this.rbnNgAlarm.TabIndex = 42;
            this.rbnNgAlarm.TabStop = true;
            this.rbnNgAlarm.Text = "NG Alarm";
            this.rbnNgAlarm.UseVisualStyleBackColor = true;
            // 
            // rbnOkAlarm
            // 
            this.rbnOkAlarm.AutoSize = true;
            this.rbnOkAlarm.Location = new System.Drawing.Point(145, 33);
            this.rbnOkAlarm.Name = "rbnOkAlarm";
            this.rbnOkAlarm.Size = new System.Drawing.Size(88, 18);
            this.rbnOkAlarm.TabIndex = 41;
            this.rbnOkAlarm.TabStop = true;
            this.rbnOkAlarm.Text = "OK Alarm";
            this.rbnOkAlarm.UseVisualStyleBackColor = true;
            // 
            // nudSprayTime
            // 
            this.nudSprayTime.Location = new System.Drawing.Point(406, 81);
            this.nudSprayTime.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudSprayTime.Name = "nudSprayTime";
            this.nudSprayTime.Size = new System.Drawing.Size(84, 22);
            this.nudSprayTime.TabIndex = 42;
            this.nudSprayTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "非喷射型阀出胶时间:";
            // 
            // EditNozzleCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditNozzleCheckForm";
            this.Text = "EditNozzleCheckForm";
            this.gbx1.ResumeLayout(false);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSprayTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDotLocation;
        private Controls.DoubleTextBox tbLocationX;
        private System.Windows.Forms.Label label2;
        private Controls.DoubleTextBox tbLocationY;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.CheckBox cbWeightControl;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnEditDotParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDotType;
        private System.Windows.Forms.Button btnModelEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxIsGlobal;
        private System.Windows.Forms.Button btnAutoDispense;
        private System.Windows.Forms.RadioButton rbnGrayScale;
        private System.Windows.Forms.RadioButton rbnModelFind;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnNgAlarm;
        private System.Windows.Forms.RadioButton rbnOkAlarm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudSprayTime;
        private System.Windows.Forms.Label label4;
    }
}