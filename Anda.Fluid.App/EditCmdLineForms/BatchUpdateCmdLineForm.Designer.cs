namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class BatchUpdateCmdLineForm
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDotType = new System.Windows.Forms.Label();
            this.lblLineType = new System.Windows.Forms.Label();
            this.cbxDotType = new System.Windows.Forms.ComboBox();
            this.cbxLineType = new System.Windows.Forms.ComboBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.cbxUseInsert = new System.Windows.Forms.CheckBox();
            this.lblRotate = new System.Windows.Forms.Label();
            this.cbxRotate = new System.Windows.Forms.ComboBox();
            this.btnTargetGoto = new System.Windows.Forms.Button();
            this.btnRefGoto = new System.Windows.Forms.Button();
            this.btnTeachTarget = new System.Windows.Forms.Button();
            this.btnTeachRef = new System.Windows.Forms.Button();
            this.tbTargetX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbTargetY = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblTargetY = new System.Windows.Forms.Label();
            this.lblTargetX = new System.Windows.Forms.Label();
            this.tbRefX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbRefY = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblRefY = new System.Windows.Forms.Label();
            this.lblRefX = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblTargetPoint = new System.Windows.Forms.Label();
            this.lblRefPoint = new System.Windows.Forms.Label();
            this.tbXOffset = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbYOffset = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblOffsetY = new System.Windows.Forms.Label();
            this.lblOffsetX = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.cbIsWeightControl = new System.Windows.Forms.CheckBox();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.rdoIncrementWeight = new System.Windows.Forms.RadioButton();
            this.rdoConstantWeight = new System.Windows.Forms.RadioButton();
            this.tabParm = new System.Windows.Forms.TabControl();
            this.tabPgNormal = new System.Windows.Forms.TabPage();
            this.tabPgSymbol = new System.Windows.Forms.TabPage();
            this.symbolLine1 = new Anda.Fluid.App.BatchModification.SymbolLine();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.tabParm.SuspendLayout();
            this.tabPgNormal.SuspendLayout();
            this.tabPgSymbol.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.gbx1.Controls.Add(this.tabParm);
            this.gbx1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.gbx1.Location = new System.Drawing.Point(510, 12);
            this.gbx1.Size = new System.Drawing.Size(270, 210);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnTargetGoto);
            this.gbx2.Controls.Add(this.btnRefGoto);
            this.gbx2.Controls.Add(this.btnTeachTarget);
            this.gbx2.Controls.Add(this.btnTeachRef);
            this.gbx2.Controls.Add(this.tbTargetX);
            this.gbx2.Controls.Add(this.cbxUseInsert);
            this.gbx2.Controls.Add(this.btnInsert);
            this.gbx2.Controls.Add(this.tbTargetY);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.cbxRotate);
            this.gbx2.Controls.Add(this.btnUpdate);
            this.gbx2.Controls.Add(this.lblTargetY);
            this.gbx2.Controls.Add(this.lblRotate);
            this.gbx2.Controls.Add(this.lblTargetX);
            this.gbx2.Controls.Add(this.tbRefX);
            this.gbx2.Controls.Add(this.tbRefY);
            this.gbx2.Controls.Add(this.lblRefY);
            this.gbx2.Controls.Add(this.lblRefX);
            this.gbx2.Controls.Add(this.lblOffset);
            this.gbx2.Controls.Add(this.lblTargetPoint);
            this.gbx2.Controls.Add(this.lblRefPoint);
            this.gbx2.Controls.Add(this.tbXOffset);
            this.gbx2.Controls.Add(this.tbYOffset);
            this.gbx2.Controls.Add(this.lblOffsetY);
            this.gbx2.Controls.Add(this.lblOffsetX);
            this.gbx2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(305, 216);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(398, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDotType
            // 
            this.lblDotType.Location = new System.Drawing.Point(9, 73);
            this.lblDotType.Name = "lblDotType";
            this.lblDotType.Size = new System.Drawing.Size(71, 17);
            this.lblDotType.TabIndex = 6;
            this.lblDotType.Text = "Dot Type :";
            // 
            // lblLineType
            // 
            this.lblLineType.Location = new System.Drawing.Point(8, 99);
            this.lblLineType.Name = "lblLineType";
            this.lblLineType.Size = new System.Drawing.Size(71, 17);
            this.lblLineType.TabIndex = 7;
            this.lblLineType.Text = "Line Type :";
            // 
            // cbxDotType
            // 
            this.cbxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDotType.FormattingEnabled = true;
            this.cbxDotType.Location = new System.Drawing.Point(85, 70);
            this.cbxDotType.Name = "cbxDotType";
            this.cbxDotType.Size = new System.Drawing.Size(121, 22);
            this.cbxDotType.TabIndex = 8;
            // 
            // cbxLineType
            // 
            this.cbxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLineType.FormattingEnabled = true;
            this.cbxLineType.Location = new System.Drawing.Point(85, 96);
            this.cbxLineType.Name = "cbxLineType";
            this.cbxLineType.Size = new System.Drawing.Size(121, 22);
            this.cbxLineType.TabIndex = 9;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(214, 216);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(87, 23);
            this.btnInsert.TabIndex = 10;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // cbxUseInsert
            // 
            this.cbxUseInsert.Location = new System.Drawing.Point(101, 219);
            this.cbxUseInsert.Name = "cbxUseInsert";
            this.cbxUseInsert.Size = new System.Drawing.Size(105, 19);
            this.cbxUseInsert.TabIndex = 11;
            this.cbxUseInsert.Text = "Use Insert";
            this.cbxUseInsert.UseVisualStyleBackColor = true;
            this.cbxUseInsert.CheckedChanged += new System.EventHandler(this.cbxUseInsert_CheckedChanged);
            // 
            // lblRotate
            // 
            this.lblRotate.Location = new System.Drawing.Point(284, 163);
            this.lblRotate.Name = "lblRotate";
            this.lblRotate.Size = new System.Drawing.Size(95, 17);
            this.lblRotate.TabIndex = 12;
            this.lblRotate.Text = "Rotate Angle :";
            // 
            // cbxRotate
            // 
            this.cbxRotate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRotate.FormattingEnabled = true;
            this.cbxRotate.Location = new System.Drawing.Point(386, 160);
            this.cbxRotate.Name = "cbxRotate";
            this.cbxRotate.Size = new System.Drawing.Size(78, 22);
            this.cbxRotate.TabIndex = 13;
            // 
            // btnTargetGoto
            // 
            this.btnTargetGoto.Location = new System.Drawing.Point(379, 100);
            this.btnTargetGoto.Name = "btnTargetGoto";
            this.btnTargetGoto.Size = new System.Drawing.Size(75, 23);
            this.btnTargetGoto.TabIndex = 38;
            this.btnTargetGoto.Text = "Go to";
            this.btnTargetGoto.UseVisualStyleBackColor = true;
            this.btnTargetGoto.Click += new System.EventHandler(this.btnTargetGoto_Click);
            // 
            // btnRefGoto
            // 
            this.btnRefGoto.Location = new System.Drawing.Point(379, 40);
            this.btnRefGoto.Name = "btnRefGoto";
            this.btnRefGoto.Size = new System.Drawing.Size(75, 23);
            this.btnRefGoto.TabIndex = 37;
            this.btnRefGoto.Text = "Go to";
            this.btnRefGoto.UseVisualStyleBackColor = true;
            this.btnRefGoto.Click += new System.EventHandler(this.btnRefGoto_Click);
            // 
            // btnTeachTarget
            // 
            this.btnTeachTarget.Location = new System.Drawing.Point(287, 100);
            this.btnTeachTarget.Name = "btnTeachTarget";
            this.btnTeachTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTeachTarget.TabIndex = 36;
            this.btnTeachTarget.Text = "Teach";
            this.btnTeachTarget.UseVisualStyleBackColor = true;
            this.btnTeachTarget.Click += new System.EventHandler(this.btnTeachTarget_Click);
            // 
            // btnTeachRef
            // 
            this.btnTeachRef.Location = new System.Drawing.Point(287, 40);
            this.btnTeachRef.Name = "btnTeachRef";
            this.btnTeachRef.Size = new System.Drawing.Size(75, 23);
            this.btnTeachRef.TabIndex = 35;
            this.btnTeachRef.Text = "Teach";
            this.btnTeachRef.UseVisualStyleBackColor = true;
            this.btnTeachRef.Click += new System.EventHandler(this.btnTeachRef_Click);
            // 
            // tbTargetX
            // 
            this.tbTargetX.BackColor = System.Drawing.Color.White;
            this.tbTargetX.Location = new System.Drawing.Point(71, 102);
            this.tbTargetX.Name = "tbTargetX";
            this.tbTargetX.Size = new System.Drawing.Size(64, 22);
            this.tbTargetX.TabIndex = 33;
            // 
            // tbTargetY
            // 
            this.tbTargetY.BackColor = System.Drawing.Color.White;
            this.tbTargetY.Location = new System.Drawing.Point(200, 102);
            this.tbTargetY.Name = "tbTargetY";
            this.tbTargetY.Size = new System.Drawing.Size(64, 22);
            this.tbTargetY.TabIndex = 34;
            // 
            // lblTargetY
            // 
            this.lblTargetY.AutoSize = true;
            this.lblTargetY.Location = new System.Drawing.Point(162, 105);
            this.lblTargetY.Name = "lblTargetY";
            this.lblTargetY.Size = new System.Drawing.Size(22, 14);
            this.lblTargetY.TabIndex = 32;
            this.lblTargetY.Text = "Y:";
            // 
            // lblTargetX
            // 
            this.lblTargetX.AutoSize = true;
            this.lblTargetX.Location = new System.Drawing.Point(40, 105);
            this.lblTargetX.Name = "lblTargetX";
            this.lblTargetX.Size = new System.Drawing.Size(21, 14);
            this.lblTargetX.TabIndex = 31;
            this.lblTargetX.Text = "X:";
            // 
            // tbRefX
            // 
            this.tbRefX.BackColor = System.Drawing.Color.White;
            this.tbRefX.Location = new System.Drawing.Point(71, 42);
            this.tbRefX.Name = "tbRefX";
            this.tbRefX.Size = new System.Drawing.Size(64, 22);
            this.tbRefX.TabIndex = 29;
            // 
            // tbRefY
            // 
            this.tbRefY.BackColor = System.Drawing.Color.White;
            this.tbRefY.Location = new System.Drawing.Point(200, 42);
            this.tbRefY.Name = "tbRefY";
            this.tbRefY.Size = new System.Drawing.Size(64, 22);
            this.tbRefY.TabIndex = 30;
            // 
            // lblRefY
            // 
            this.lblRefY.AutoSize = true;
            this.lblRefY.Location = new System.Drawing.Point(162, 45);
            this.lblRefY.Name = "lblRefY";
            this.lblRefY.Size = new System.Drawing.Size(22, 14);
            this.lblRefY.TabIndex = 28;
            this.lblRefY.Text = "Y:";
            // 
            // lblRefX
            // 
            this.lblRefX.AutoSize = true;
            this.lblRefX.Location = new System.Drawing.Point(40, 45);
            this.lblRefX.Name = "lblRefX";
            this.lblRefX.Size = new System.Drawing.Size(21, 14);
            this.lblRefX.TabIndex = 27;
            this.lblRefX.Text = "X:";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(15, 136);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(57, 14);
            this.lblOffset.TabIndex = 26;
            this.lblOffset.Text = "Offset :";
            // 
            // lblTargetPoint
            // 
            this.lblTargetPoint.AutoSize = true;
            this.lblTargetPoint.Location = new System.Drawing.Point(15, 80);
            this.lblTargetPoint.Name = "lblTargetPoint";
            this.lblTargetPoint.Size = new System.Drawing.Size(97, 14);
            this.lblTargetPoint.TabIndex = 25;
            this.lblTargetPoint.Text = "Target Point :";
            // 
            // lblRefPoint
            // 
            this.lblRefPoint.AutoSize = true;
            this.lblRefPoint.Location = new System.Drawing.Point(15, 19);
            this.lblRefPoint.Name = "lblRefPoint";
            this.lblRefPoint.Size = new System.Drawing.Size(121, 14);
            this.lblRefPoint.TabIndex = 24;
            this.lblRefPoint.Text = "Reference Point :";
            // 
            // tbXOffset
            // 
            this.tbXOffset.BackColor = System.Drawing.Color.White;
            this.tbXOffset.Location = new System.Drawing.Point(71, 160);
            this.tbXOffset.Name = "tbXOffset";
            this.tbXOffset.Size = new System.Drawing.Size(64, 22);
            this.tbXOffset.TabIndex = 23;
            // 
            // tbYOffset
            // 
            this.tbYOffset.BackColor = System.Drawing.Color.White;
            this.tbYOffset.Location = new System.Drawing.Point(200, 160);
            this.tbYOffset.Name = "tbYOffset";
            this.tbYOffset.Size = new System.Drawing.Size(64, 22);
            this.tbYOffset.TabIndex = 22;
            // 
            // lblOffsetY
            // 
            this.lblOffsetY.AutoSize = true;
            this.lblOffsetY.Location = new System.Drawing.Point(162, 163);
            this.lblOffsetY.Name = "lblOffsetY";
            this.lblOffsetY.Size = new System.Drawing.Size(22, 14);
            this.lblOffsetY.TabIndex = 21;
            this.lblOffsetY.Text = "Y:";
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new System.Drawing.Point(40, 163);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(21, 14);
            this.lblOffsetX.TabIndex = 20;
            this.lblOffsetX.Text = "X:";
            // 
            // lblWeight
            // 
            this.lblWeight.Location = new System.Drawing.Point(9, 47);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(70, 13);
            this.lblWeight.TabIndex = 10;
            this.lblWeight.Text = "Weight :";
            // 
            // cbIsWeightControl
            // 
            this.cbIsWeightControl.AutoSize = true;
            this.cbIsWeightControl.Location = new System.Drawing.Point(6, 6);
            this.cbIsWeightControl.Name = "cbIsWeightControl";
            this.cbIsWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbIsWeightControl.TabIndex = 9;
            this.cbIsWeightControl.Text = "Weight Control";
            this.cbIsWeightControl.ThreeState = true;
            this.cbIsWeightControl.UseVisualStyleBackColor = true;
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(85, 44);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(121, 22);
            this.tbWeight.TabIndex = 8;
            // 
            // rdoIncrementWeight
            // 
            this.rdoIncrementWeight.AutoSize = true;
            this.rdoIncrementWeight.Location = new System.Drawing.Point(125, 28);
            this.rdoIncrementWeight.Name = "rdoIncrementWeight";
            this.rdoIncrementWeight.Size = new System.Drawing.Size(91, 18);
            this.rdoIncrementWeight.TabIndex = 7;
            this.rdoIncrementWeight.TabStop = true;
            this.rdoIncrementWeight.Text = "increment";
            this.rdoIncrementWeight.UseVisualStyleBackColor = true;
            // 
            // rdoConstantWeight
            // 
            this.rdoConstantWeight.AutoSize = true;
            this.rdoConstantWeight.Location = new System.Drawing.Point(6, 28);
            this.rdoConstantWeight.Name = "rdoConstantWeight";
            this.rdoConstantWeight.Size = new System.Drawing.Size(81, 18);
            this.rdoConstantWeight.TabIndex = 6;
            this.rdoConstantWeight.TabStop = true;
            this.rdoConstantWeight.Text = "constant";
            this.rdoConstantWeight.UseVisualStyleBackColor = true;
            // 
            // tabParm
            // 
            this.tabParm.Controls.Add(this.tabPgNormal);
            this.tabParm.Controls.Add(this.tabPgSymbol);
            this.tabParm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabParm.Location = new System.Drawing.Point(4, 18);
            this.tabParm.Name = "tabParm";
            this.tabParm.SelectedIndex = 0;
            this.tabParm.Size = new System.Drawing.Size(262, 189);
            this.tabParm.TabIndex = 10;
            // 
            // tabPgNormal
            // 
            this.tabPgNormal.Controls.Add(this.lblDotType);
            this.tabPgNormal.Controls.Add(this.lblWeight);
            this.tabPgNormal.Controls.Add(this.lblLineType);
            this.tabPgNormal.Controls.Add(this.tbWeight);
            this.tabPgNormal.Controls.Add(this.cbxLineType);
            this.tabPgNormal.Controls.Add(this.cbIsWeightControl);
            this.tabPgNormal.Controls.Add(this.cbxDotType);
            this.tabPgNormal.Controls.Add(this.rdoConstantWeight);
            this.tabPgNormal.Controls.Add(this.rdoIncrementWeight);
            this.tabPgNormal.Location = new System.Drawing.Point(4, 23);
            this.tabPgNormal.Name = "tabPgNormal";
            this.tabPgNormal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgNormal.Size = new System.Drawing.Size(254, 162);
            this.tabPgNormal.TabIndex = 0;
            this.tabPgNormal.Text = "胶量";
            this.tabPgNormal.UseVisualStyleBackColor = true;
            // 
            // tabPgSymbol
            // 
            this.tabPgSymbol.Controls.Add(this.symbolLine1);
            this.tabPgSymbol.Location = new System.Drawing.Point(4, 23);
            this.tabPgSymbol.Name = "tabPgSymbol";
            this.tabPgSymbol.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgSymbol.Size = new System.Drawing.Size(254, 162);
            this.tabPgSymbol.TabIndex = 1;
            this.tabPgSymbol.Text = "多段线";
            this.tabPgSymbol.UseVisualStyleBackColor = true;
            // 
            // symbolLine1
            // 
            this.symbolLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.symbolLine1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symbolLine1.Location = new System.Drawing.Point(3, 3);
            this.symbolLine1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.symbolLine1.Name = "symbolLine1";
            this.symbolLine1.Size = new System.Drawing.Size(248, 156);
            this.symbolLine1.TabIndex = 0;
            // 
            // BatchUpdateCmdLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchUpdateCmdLineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BatchUpdateCmdLineForm";
            this.gbx1.ResumeLayout(false);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.tabParm.ResumeLayout(false);
            this.tabPgNormal.ResumeLayout(false);
            this.tabPgNormal.PerformLayout();
            this.tabPgSymbol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDotType;
        private System.Windows.Forms.Label lblLineType;
        private System.Windows.Forms.ComboBox cbxDotType;
        private System.Windows.Forms.ComboBox cbxLineType;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.CheckBox cbxUseInsert;
        private System.Windows.Forms.Label lblRotate;
        private System.Windows.Forms.ComboBox cbxRotate;
        private System.Windows.Forms.Button btnTargetGoto;
        private System.Windows.Forms.Button btnRefGoto;
        private System.Windows.Forms.Button btnTeachTarget;
        private System.Windows.Forms.Button btnTeachRef;
        private Controls.DoubleTextBox tbTargetX;
        private Controls.DoubleTextBox tbTargetY;
        private System.Windows.Forms.Label lblTargetY;
        private System.Windows.Forms.Label lblTargetX;
        private Controls.DoubleTextBox tbRefX;
        private Controls.DoubleTextBox tbRefY;
        private System.Windows.Forms.Label lblRefY;
        private System.Windows.Forms.Label lblRefX;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblTargetPoint;
        private System.Windows.Forms.Label lblRefPoint;
        private Controls.DoubleTextBox tbXOffset;
        private Controls.DoubleTextBox tbYOffset;
        private System.Windows.Forms.Label lblOffsetY;
        private System.Windows.Forms.Label lblOffsetX;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.CheckBox cbIsWeightControl;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.RadioButton rdoIncrementWeight;
        private System.Windows.Forms.RadioButton rdoConstantWeight;
        private System.Windows.Forms.TabControl tabParm;
        private System.Windows.Forms.TabPage tabPgNormal;
        private System.Windows.Forms.TabPage tabPgSymbol;
        private BatchModification.SymbolLine symbolLine1;
    }
}