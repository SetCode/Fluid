namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditBatchMetro
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.btnTargetGoto = new System.Windows.Forms.Button();
            this.btnRefGoto = new System.Windows.Forms.Button();
            this.btnTeachTarget = new System.Windows.Forms.Button();
            this.btnTeachRef = new System.Windows.Forms.Button();
            this.tbTargetX = new Anda.Fluid.Controls.DoubleTextBox();
            this.cbxUseInsert = new System.Windows.Forms.CheckBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.tbTargetY = new Anda.Fluid.Controls.DoubleTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxRotate = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblTargetY = new System.Windows.Forms.Label();
            this.lblRotate = new System.Windows.Forms.Label();
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
            this.lblDotType = new System.Windows.Forms.Label();
            this.lblLineType = new System.Windows.Forms.Label();
            this.cbxLineType = new System.Windows.Forms.ComboBox();
            this.cbxDotType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Administrator\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // btnTargetGoto
            // 
            this.btnTargetGoto.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTargetGoto.ForeColor = System.Drawing.Color.Black;
            this.btnTargetGoto.Location = new System.Drawing.Point(352, 97);
            this.btnTargetGoto.Name = "btnTargetGoto";
            this.btnTargetGoto.Size = new System.Drawing.Size(75, 23);
            this.btnTargetGoto.TabIndex = 63;
            this.btnTargetGoto.Text = "移动";
            this.btnTargetGoto.UseVisualStyleBackColor = true;
            this.btnTargetGoto.Click += new System.EventHandler(this.btnTargetGoto_Click);
            // 
            // btnRefGoto
            // 
            this.btnRefGoto.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefGoto.ForeColor = System.Drawing.Color.Black;
            this.btnRefGoto.Location = new System.Drawing.Point(352, 37);
            this.btnRefGoto.Name = "btnRefGoto";
            this.btnRefGoto.Size = new System.Drawing.Size(75, 23);
            this.btnRefGoto.TabIndex = 62;
            this.btnRefGoto.Text = "移动";
            this.btnRefGoto.UseVisualStyleBackColor = true;
            this.btnRefGoto.Click += new System.EventHandler(this.btnRefGoto_Click);
            // 
            // btnTeachTarget
            // 
            this.btnTeachTarget.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeachTarget.ForeColor = System.Drawing.Color.Black;
            this.btnTeachTarget.Location = new System.Drawing.Point(260, 97);
            this.btnTeachTarget.Name = "btnTeachTarget";
            this.btnTeachTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTeachTarget.TabIndex = 61;
            this.btnTeachTarget.Text = "示教";
            this.btnTeachTarget.UseVisualStyleBackColor = true;
            this.btnTeachTarget.Click += new System.EventHandler(this.btnTeachTarget_Click);
            // 
            // btnTeachRef
            // 
            this.btnTeachRef.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeachRef.ForeColor = System.Drawing.Color.Black;
            this.btnTeachRef.Location = new System.Drawing.Point(260, 37);
            this.btnTeachRef.Name = "btnTeachRef";
            this.btnTeachRef.Size = new System.Drawing.Size(75, 23);
            this.btnTeachRef.TabIndex = 60;
            this.btnTeachRef.Text = "示教";
            this.btnTeachRef.UseVisualStyleBackColor = true;
            this.btnTeachRef.Click += new System.EventHandler(this.btnTeachRef_Click);
            // 
            // tbTargetX
            // 
            this.tbTargetX.BackColor = System.Drawing.Color.White;
            this.tbTargetX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTargetX.ForeColor = System.Drawing.Color.Black;
            this.tbTargetX.Location = new System.Drawing.Point(65, 97);
            this.tbTargetX.Name = "tbTargetX";
            this.tbTargetX.Size = new System.Drawing.Size(64, 22);
            this.tbTargetX.TabIndex = 58;
            this.tbTargetX.TextChanged += new System.EventHandler(this.TbTargetX_TextChanged);
            // 
            // cbxUseInsert
            // 
            this.cbxUseInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbxUseInsert.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUseInsert.ForeColor = System.Drawing.Color.White;
            this.cbxUseInsert.Location = new System.Drawing.Point(18, 213);
            this.cbxUseInsert.Name = "cbxUseInsert";
            this.cbxUseInsert.Size = new System.Drawing.Size(105, 19);
            this.cbxUseInsert.TabIndex = 42;
            this.cbxUseInsert.Text = "Use Insert";
            this.cbxUseInsert.UseVisualStyleBackColor = false;
            this.cbxUseInsert.Click += new System.EventHandler(this.cbxUseInsert_CheckedChanged);
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.ForeColor = System.Drawing.Color.Black;
            this.btnInsert.Location = new System.Drawing.Point(131, 210);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(87, 23);
            this.btnInsert.TabIndex = 41;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // tbTargetY
            // 
            this.tbTargetY.BackColor = System.Drawing.Color.White;
            this.tbTargetY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTargetY.ForeColor = System.Drawing.Color.Black;
            this.tbTargetY.Location = new System.Drawing.Point(173, 97);
            this.tbTargetY.Name = "tbTargetY";
            this.tbTargetY.Size = new System.Drawing.Size(64, 22);
            this.tbTargetY.TabIndex = 59;
            this.tbTargetY.TextChanged += new System.EventHandler(this.TbTargetY_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(315, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxRotate
            // 
            this.cbxRotate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRotate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRotate.ForeColor = System.Drawing.Color.Black;
            this.cbxRotate.FormattingEnabled = true;
            this.cbxRotate.Location = new System.Drawing.Point(352, 158);
            this.cbxRotate.Name = "cbxRotate";
            this.cbxRotate.Size = new System.Drawing.Size(78, 22);
            this.cbxRotate.TabIndex = 44;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(222, 210);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 23);
            this.btnUpdate.TabIndex = 39;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblTargetY
            // 
            this.lblTargetY.AutoSize = true;
            this.lblTargetY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTargetY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetY.ForeColor = System.Drawing.Color.White;
            this.lblTargetY.Location = new System.Drawing.Point(145, 100);
            this.lblTargetY.Name = "lblTargetY";
            this.lblTargetY.Size = new System.Drawing.Size(22, 14);
            this.lblTargetY.TabIndex = 57;
            this.lblTargetY.Text = "Y:";
            // 
            // lblRotate
            // 
            this.lblRotate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRotate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotate.ForeColor = System.Drawing.Color.White;
            this.lblRotate.Location = new System.Drawing.Point(250, 161);
            this.lblRotate.Name = "lblRotate";
            this.lblRotate.Size = new System.Drawing.Size(95, 17);
            this.lblRotate.TabIndex = 43;
            this.lblRotate.Text = "Rotate Angle :";
            // 
            // lblTargetX
            // 
            this.lblTargetX.AutoSize = true;
            this.lblTargetX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTargetX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetX.ForeColor = System.Drawing.Color.White;
            this.lblTargetX.Location = new System.Drawing.Point(34, 100);
            this.lblTargetX.Name = "lblTargetX";
            this.lblTargetX.Size = new System.Drawing.Size(21, 14);
            this.lblTargetX.TabIndex = 56;
            this.lblTargetX.Text = "X:";
            // 
            // tbRefX
            // 
            this.tbRefX.BackColor = System.Drawing.Color.White;
            this.tbRefX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRefX.ForeColor = System.Drawing.Color.Black;
            this.tbRefX.Location = new System.Drawing.Point(65, 37);
            this.tbRefX.Name = "tbRefX";
            this.tbRefX.Size = new System.Drawing.Size(64, 22);
            this.tbRefX.TabIndex = 54;
            this.tbRefX.TextChanged += new System.EventHandler(this.TbRefX_TextChanged);
            // 
            // tbRefY
            // 
            this.tbRefY.BackColor = System.Drawing.Color.White;
            this.tbRefY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRefY.ForeColor = System.Drawing.Color.Black;
            this.tbRefY.Location = new System.Drawing.Point(173, 37);
            this.tbRefY.Name = "tbRefY";
            this.tbRefY.Size = new System.Drawing.Size(64, 22);
            this.tbRefY.TabIndex = 55;
            this.tbRefY.TextChanged += new System.EventHandler(this.TbRefY_TextChanged);
            // 
            // lblRefY
            // 
            this.lblRefY.AutoSize = true;
            this.lblRefY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRefY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefY.ForeColor = System.Drawing.Color.White;
            this.lblRefY.Location = new System.Drawing.Point(145, 40);
            this.lblRefY.Name = "lblRefY";
            this.lblRefY.Size = new System.Drawing.Size(22, 14);
            this.lblRefY.TabIndex = 53;
            this.lblRefY.Text = "Y:";
            // 
            // lblRefX
            // 
            this.lblRefX.AutoSize = true;
            this.lblRefX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRefX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefX.ForeColor = System.Drawing.Color.White;
            this.lblRefX.Location = new System.Drawing.Point(34, 40);
            this.lblRefX.Name = "lblRefX";
            this.lblRefX.Size = new System.Drawing.Size(21, 14);
            this.lblRefX.TabIndex = 52;
            this.lblRefX.Text = "X:";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffset.ForeColor = System.Drawing.Color.White;
            this.lblOffset.Location = new System.Drawing.Point(9, 131);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(57, 14);
            this.lblOffset.TabIndex = 51;
            this.lblOffset.Text = "Offset :";
            // 
            // lblTargetPoint
            // 
            this.lblTargetPoint.AutoSize = true;
            this.lblTargetPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTargetPoint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetPoint.ForeColor = System.Drawing.Color.White;
            this.lblTargetPoint.Location = new System.Drawing.Point(9, 75);
            this.lblTargetPoint.Name = "lblTargetPoint";
            this.lblTargetPoint.Size = new System.Drawing.Size(97, 14);
            this.lblTargetPoint.TabIndex = 50;
            this.lblTargetPoint.Text = "Target Point :";
            // 
            // lblRefPoint
            // 
            this.lblRefPoint.AutoSize = true;
            this.lblRefPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRefPoint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefPoint.ForeColor = System.Drawing.Color.White;
            this.lblRefPoint.Location = new System.Drawing.Point(9, 14);
            this.lblRefPoint.Name = "lblRefPoint";
            this.lblRefPoint.Size = new System.Drawing.Size(121, 14);
            this.lblRefPoint.TabIndex = 49;
            this.lblRefPoint.Text = "Reference Point :";
            // 
            // tbXOffset
            // 
            this.tbXOffset.BackColor = System.Drawing.Color.White;
            this.tbXOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbXOffset.ForeColor = System.Drawing.Color.Black;
            this.tbXOffset.Location = new System.Drawing.Point(65, 155);
            this.tbXOffset.Name = "tbXOffset";
            this.tbXOffset.Size = new System.Drawing.Size(64, 22);
            this.tbXOffset.TabIndex = 48;
            this.tbXOffset.TextChanged += new System.EventHandler(this.TbXOffset_TextChanged);
            // 
            // tbYOffset
            // 
            this.tbYOffset.BackColor = System.Drawing.Color.White;
            this.tbYOffset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbYOffset.ForeColor = System.Drawing.Color.Black;
            this.tbYOffset.Location = new System.Drawing.Point(175, 158);
            this.tbYOffset.Name = "tbYOffset";
            this.tbYOffset.Size = new System.Drawing.Size(64, 22);
            this.tbYOffset.TabIndex = 47;
            this.tbYOffset.TextChanged += new System.EventHandler(this.TbYOffset_TextChanged);
            // 
            // lblOffsetY
            // 
            this.lblOffsetY.AutoSize = true;
            this.lblOffsetY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblOffsetY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffsetY.ForeColor = System.Drawing.Color.White;
            this.lblOffsetY.Location = new System.Drawing.Point(147, 161);
            this.lblOffsetY.Name = "lblOffsetY";
            this.lblOffsetY.Size = new System.Drawing.Size(22, 14);
            this.lblOffsetY.TabIndex = 46;
            this.lblOffsetY.Text = "Y:";
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblOffsetX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffsetX.ForeColor = System.Drawing.Color.White;
            this.lblOffsetX.Location = new System.Drawing.Point(34, 158);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(21, 14);
            this.lblOffsetX.TabIndex = 45;
            this.lblOffsetX.Text = "X:";
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.White;
            this.lblWeight.Location = new System.Drawing.Point(152, 303);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(70, 13);
            this.lblWeight.TabIndex = 72;
            this.lblWeight.Text = "Weight :";
            // 
            // cbIsWeightControl
            // 
            this.cbIsWeightControl.AutoSize = true;
            this.cbIsWeightControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbIsWeightControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsWeightControl.ForeColor = System.Drawing.Color.White;
            this.cbIsWeightControl.Location = new System.Drawing.Point(15, 300);
            this.cbIsWeightControl.Name = "cbIsWeightControl";
            this.cbIsWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbIsWeightControl.TabIndex = 70;
            this.cbIsWeightControl.Text = "Weight Control";
            this.cbIsWeightControl.ThreeState = true;
            this.cbIsWeightControl.UseVisualStyleBackColor = false;
            this.cbIsWeightControl.CheckedChanged += new System.EventHandler(this.CbIsWeightControl_Click);
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeight.ForeColor = System.Drawing.Color.Black;
            this.tbWeight.Location = new System.Drawing.Point(228, 300);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(121, 22);
            this.tbWeight.TabIndex = 68;
            // 
            // rdoIncrementWeight
            // 
            this.rdoIncrementWeight.AutoSize = true;
            this.rdoIncrementWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rdoIncrementWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIncrementWeight.ForeColor = System.Drawing.Color.White;
            this.rdoIncrementWeight.Location = new System.Drawing.Point(137, 260);
            this.rdoIncrementWeight.Name = "rdoIncrementWeight";
            this.rdoIncrementWeight.Size = new System.Drawing.Size(91, 18);
            this.rdoIncrementWeight.TabIndex = 66;
            this.rdoIncrementWeight.TabStop = true;
            this.rdoIncrementWeight.Text = "increment";
            this.rdoIncrementWeight.UseVisualStyleBackColor = false;
            // 
            // rdoConstantWeight
            // 
            this.rdoConstantWeight.AutoSize = true;
            this.rdoConstantWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rdoConstantWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoConstantWeight.ForeColor = System.Drawing.Color.White;
            this.rdoConstantWeight.Location = new System.Drawing.Point(18, 260);
            this.rdoConstantWeight.Name = "rdoConstantWeight";
            this.rdoConstantWeight.Size = new System.Drawing.Size(81, 18);
            this.rdoConstantWeight.TabIndex = 64;
            this.rdoConstantWeight.TabStop = true;
            this.rdoConstantWeight.Text = "constant";
            this.rdoConstantWeight.UseVisualStyleBackColor = false;
            // 
            // lblDotType
            // 
            this.lblDotType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblDotType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDotType.ForeColor = System.Drawing.Color.White;
            this.lblDotType.Location = new System.Drawing.Point(15, 342);
            this.lblDotType.Name = "lblDotType";
            this.lblDotType.Size = new System.Drawing.Size(71, 17);
            this.lblDotType.TabIndex = 65;
            this.lblDotType.Text = "Dot Type :";
            // 
            // lblLineType
            // 
            this.lblLineType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblLineType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineType.ForeColor = System.Drawing.Color.White;
            this.lblLineType.Location = new System.Drawing.Point(14, 368);
            this.lblLineType.Name = "lblLineType";
            this.lblLineType.Size = new System.Drawing.Size(71, 17);
            this.lblLineType.TabIndex = 67;
            this.lblLineType.Text = "Line Type :";
            // 
            // cbxLineType
            // 
            this.cbxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLineType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxLineType.ForeColor = System.Drawing.Color.Black;
            this.cbxLineType.FormattingEnabled = true;
            this.cbxLineType.Location = new System.Drawing.Point(91, 365);
            this.cbxLineType.Name = "cbxLineType";
            this.cbxLineType.Size = new System.Drawing.Size(121, 22);
            this.cbxLineType.TabIndex = 71;
            this.cbxLineType.SelectedIndexChanged += new System.EventHandler(this.CbxLineType_SelectedIndexChanged);
            // 
            // cbxDotType
            // 
            this.cbxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDotType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDotType.ForeColor = System.Drawing.Color.Black;
            this.cbxDotType.FormattingEnabled = true;
            this.cbxDotType.Location = new System.Drawing.Point(91, 339);
            this.cbxDotType.Name = "cbxDotType";
            this.cbxDotType.Size = new System.Drawing.Size(121, 22);
            this.cbxDotType.TabIndex = 69;
            this.cbxDotType.SelectedIndexChanged += new System.EventHandler(this.CbxDotType_SelectedIndexChanged);
            // 
            // EditBatchMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.cbIsWeightControl);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.rdoIncrementWeight);
            this.Controls.Add(this.rdoConstantWeight);
            this.Controls.Add(this.lblDotType);
            this.Controls.Add(this.lblLineType);
            this.Controls.Add(this.cbxLineType);
            this.Controls.Add(this.cbxDotType);
            this.Controls.Add(this.btnTargetGoto);
            this.Controls.Add(this.btnRefGoto);
            this.Controls.Add(this.btnTeachTarget);
            this.Controls.Add(this.btnTeachRef);
            this.Controls.Add(this.tbTargetX);
            this.Controls.Add(this.cbxUseInsert);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.tbTargetY);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbxRotate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblTargetY);
            this.Controls.Add(this.lblRotate);
            this.Controls.Add(this.lblTargetX);
            this.Controls.Add(this.tbRefX);
            this.Controls.Add(this.tbRefY);
            this.Controls.Add(this.lblRefY);
            this.Controls.Add(this.lblRefX);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.lblTargetPoint);
            this.Controls.Add(this.lblRefPoint);
            this.Controls.Add(this.tbXOffset);
            this.Controls.Add(this.tbYOffset);
            this.Controls.Add(this.lblOffsetY);
            this.Controls.Add(this.lblOffsetX);
            this.Name = "EditBatchMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.BatchUpdateCmdLineForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.Button btnTargetGoto;
        private System.Windows.Forms.Button btnRefGoto;
        private System.Windows.Forms.Button btnTeachTarget;
        private System.Windows.Forms.Button btnTeachRef;
        private Controls.DoubleTextBox tbTargetX;
        private System.Windows.Forms.CheckBox cbxUseInsert;
        private System.Windows.Forms.Button btnInsert;
        private Controls.DoubleTextBox tbTargetY;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxRotate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblTargetY;
        private System.Windows.Forms.Label lblRotate;
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
        private System.Windows.Forms.Label lblDotType;
        private System.Windows.Forms.Label lblLineType;
        private System.Windows.Forms.ComboBox cbxLineType;
        private System.Windows.Forms.ComboBox cbxDotType;
    }
}
