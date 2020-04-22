namespace Anda.Fluid.App.Settings
{
    partial class SystemPositionDefinations1
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNeedle2 = new System.Windows.Forms.RadioButton();
            this.rbCamera = new System.Windows.Forms.RadioButton();
            this.rbLaser = new System.Windows.Forms.RadioButton();
            this.rbNeedle1 = new System.Windows.Forms.RadioButton();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listBoxDefs = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.tbZ = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblZ = new System.Windows.Forms.Label();
            this.btnTeachZ = new System.Windows.Forms.Button();
            this.btnGotoZ = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnGotoZ);
            this.gbx1.Controls.Add(this.btnTeachZ);
            this.gbx1.Controls.Add(this.tbZ);
            this.gbx1.Controls.Add(this.lblZ);
            this.gbx1.Controls.Add(this.btnChange);
            this.gbx1.Controls.Add(this.btnDelete);
            this.gbx1.Controls.Add(this.btnAdd);
            this.gbx1.Controls.Add(this.btnGoTo);
            this.gbx1.Controls.Add(this.btnSelect);
            this.gbx1.Controls.Add(this.groupBox1);
            this.gbx1.Controls.Add(this.tbY);
            this.gbx1.Controls.Add(this.tbX);
            this.gbx1.Controls.Add(this.lblPosition);
            this.gbx1.Controls.Add(this.tbName);
            this.gbx1.Controls.Add(this.label3);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.listBoxDefs);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.btnOk);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(66, 15);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(199, 22);
            this.tbName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name:";
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(132, 42);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(63, 22);
            this.tbY.TabIndex = 11;
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(66, 43);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(63, 22);
            this.tbX.TabIndex = 12;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(-1, 46);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(65, 14);
            this.lblPosition.TabIndex = 10;
            this.lblPosition.Text = "Position:";
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(202, 71);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(61, 23);
            this.btnGoTo.TabIndex = 13;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(202, 42);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(61, 23);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNeedle2);
            this.groupBox1.Controls.Add(this.rbCamera);
            this.groupBox1.Controls.Add(this.rbLaser);
            this.groupBox1.Controls.Add(this.rbNeedle1);
            this.groupBox1.Location = new System.Drawing.Point(10, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 83);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // rbNeedle2
            // 
            this.rbNeedle2.AutoSize = true;
            this.rbNeedle2.Location = new System.Drawing.Point(146, 54);
            this.rbNeedle2.Name = "rbNeedle2";
            this.rbNeedle2.Size = new System.Drawing.Size(78, 18);
            this.rbNeedle2.TabIndex = 11;
            this.rbNeedle2.Text = "needle2";
            this.rbNeedle2.UseVisualStyleBackColor = true;
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Checked = true;
            this.rbCamera.Location = new System.Drawing.Point(19, 21);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(74, 18);
            this.rbCamera.TabIndex = 10;
            this.rbCamera.TabStop = true;
            this.rbCamera.Text = "camera";
            this.rbCamera.UseVisualStyleBackColor = true;
            // 
            // rbLaser
            // 
            this.rbLaser.AutoSize = true;
            this.rbLaser.Location = new System.Drawing.Point(146, 21);
            this.rbLaser.Name = "rbLaser";
            this.rbLaser.Size = new System.Drawing.Size(58, 18);
            this.rbLaser.TabIndex = 10;
            this.rbLaser.Text = "laser";
            this.rbLaser.UseVisualStyleBackColor = true;
            // 
            // rbNeedle1
            // 
            this.rbNeedle1.AutoSize = true;
            this.rbNeedle1.Location = new System.Drawing.Point(19, 54);
            this.rbNeedle1.Name = "rbNeedle1";
            this.rbNeedle1.Size = new System.Drawing.Size(78, 18);
            this.rbNeedle1.TabIndex = 10;
            this.rbNeedle1.Text = "needle1";
            this.rbNeedle1.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(25, 254);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(187, 254);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listBoxDefs
            // 
            this.listBoxDefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDefs.HorizontalScrollbar = true;
            this.listBoxDefs.ItemHeight = 14;
            this.listBoxDefs.Location = new System.Drawing.Point(3, 18);
            this.listBoxDefs.Name = "listBoxDefs";
            this.listBoxDefs.Size = new System.Drawing.Size(410, 228);
            this.listBoxDefs.TabIndex = 8;
            this.listBoxDefs.SelectedIndexChanged += new System.EventHandler(this.listBoxDefs_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(419, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(106, 254);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 15;
            this.btnChange.Text = "change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tbZ
            // 
            this.tbZ.BackColor = System.Drawing.Color.White;
            this.tbZ.Location = new System.Drawing.Point(66, 72);
            this.tbZ.Name = "tbZ";
            this.tbZ.Size = new System.Drawing.Size(63, 22);
            this.tbZ.TabIndex = 17;
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(44, 75);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(20, 14);
            this.lblZ.TabIndex = 16;
            this.lblZ.Text = "Z:";
            // 
            // btnTeachZ
            // 
            this.btnTeachZ.Location = new System.Drawing.Point(66, 100);
            this.btnTeachZ.Name = "btnTeachZ";
            this.btnTeachZ.Size = new System.Drawing.Size(61, 23);
            this.btnTeachZ.TabIndex = 18;
            this.btnTeachZ.Text = "Teach";
            this.btnTeachZ.UseVisualStyleBackColor = true;
            this.btnTeachZ.Click += new System.EventHandler(this.btnTeachZ_Click);
            // 
            // btnGotoZ
            // 
            this.btnGotoZ.Location = new System.Drawing.Point(132, 100);
            this.btnGotoZ.Name = "btnGotoZ";
            this.btnGotoZ.Size = new System.Drawing.Size(61, 23);
            this.btnGotoZ.TabIndex = 19;
            this.btnGotoZ.Text = "Go To";
            this.btnGotoZ.UseVisualStyleBackColor = true;
            this.btnGotoZ.Click += new System.EventHandler(this.btnGotoZ_Click);
            // 
            // SystemPositionDefinations1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "SystemPositionDefinations1";
            this.Text = "SystemPositionDefinations1";
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.RadioButton rbLaser;
        private System.Windows.Forms.RadioButton rbNeedle1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listBoxDefs;
        private System.Windows.Forms.RadioButton rbNeedle2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnChange;
        private Controls.DoubleTextBox tbZ;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Button btnGotoZ;
        private System.Windows.Forms.Button btnTeachZ;
    }
}