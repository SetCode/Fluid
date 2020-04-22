namespace Anda.Fluid.App.Metro.EditControls
{
    partial class EditMultipassArrayMetro
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
            this.components = new System.ComponentModel.Container();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.btnOk = new System.Windows.Forms.Button();
            this.listBoxPatterns = new System.Windows.Forms.ListBox();
            this.tbVNums = new Anda.Fluid.Controls.IntTextBox();
            this.lblPatternList = new System.Windows.Forms.Label();
            this.tbHNums = new Anda.Fluid.Controls.IntTextBox();
            this.lblVNums = new System.Windows.Forms.Label();
            this.lblHNums = new System.Windows.Forms.Label();
            this.btnGoToVEnd = new System.Windows.Forms.Button();
            this.btnGoToHEnd = new System.Windows.Forms.Button();
            this.btnGoToOrigin = new System.Windows.Forms.Button();
            this.btnSelectVEnd = new System.Windows.Forms.Button();
            this.btnSelectHEnd = new System.Windows.Forms.Button();
            this.btnSelectOrigin = new System.Windows.Forms.Button();
            this.tbVEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbHEndY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbVEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbHEndX = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbOriginX = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblVEnd = new System.Windows.Forms.Label();
            this.lblHEnd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.lblPatternOriginList = new System.Windows.Forms.Label();
            this.listBoxOrigins = new System.Windows.Forms.ListBox();
            this.cmsValveType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiValve1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiValve2 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.cmsValveType.SuspendLayout();
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
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(366, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 49;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(9, 36);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.Size = new System.Drawing.Size(117, 158);
            this.listBoxPatterns.TabIndex = 33;
            // 
            // tbVNums
            // 
            this.tbVNums.BackColor = System.Drawing.Color.White;
            this.tbVNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVNums.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVNums.Location = new System.Drawing.Point(328, 36);
            this.tbVNums.Name = "tbVNums";
            this.tbVNums.Size = new System.Drawing.Size(75, 22);
            this.tbVNums.TabIndex = 48;
            this.tbVNums.Text = "3";
            this.tbVNums.TextChanged += new System.EventHandler(this.tbVNums_TextChanged);
            // 
            // lblPatternList
            // 
            this.lblPatternList.AutoSize = true;
            this.lblPatternList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPatternList.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatternList.ForeColor = System.Drawing.Color.White;
            this.lblPatternList.Location = new System.Drawing.Point(15, 12);
            this.lblPatternList.Name = "lblPatternList";
            this.lblPatternList.Size = new System.Drawing.Size(89, 14);
            this.lblPatternList.TabIndex = 32;
            this.lblPatternList.Text = "Pattern List:";
            // 
            // tbHNums
            // 
            this.tbHNums.BackColor = System.Drawing.Color.White;
            this.tbHNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHNums.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHNums.Location = new System.Drawing.Point(197, 36);
            this.tbHNums.Name = "tbHNums";
            this.tbHNums.Size = new System.Drawing.Size(74, 22);
            this.tbHNums.TabIndex = 47;
            this.tbHNums.Text = "3";
            this.tbHNums.TextChanged += new System.EventHandler(this.tbHNums_TextChanged);
            // 
            // lblVNums
            // 
            this.lblVNums.AutoSize = true;
            this.lblVNums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblVNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVNums.ForeColor = System.Drawing.Color.White;
            this.lblVNums.Location = new System.Drawing.Point(299, 12);
            this.lblVNums.Name = "lblVNums";
            this.lblVNums.Size = new System.Drawing.Size(104, 14);
            this.lblVNums.TabIndex = 46;
            this.lblVNums.Text = "Vertical Nums:";
            // 
            // lblHNums
            // 
            this.lblHNums.AutoSize = true;
            this.lblHNums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblHNums.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHNums.ForeColor = System.Drawing.Color.White;
            this.lblHNums.Location = new System.Drawing.Point(152, 12);
            this.lblHNums.Name = "lblHNums";
            this.lblHNums.Size = new System.Drawing.Size(121, 14);
            this.lblHNums.TabIndex = 45;
            this.lblHNums.Text = "Horizontal Nums:";
            // 
            // btnGoToVEnd
            // 
            this.btnGoToVEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToVEnd.ForeColor = System.Drawing.Color.Black;
            this.btnGoToVEnd.Location = new System.Drawing.Point(374, 169);
            this.btnGoToVEnd.Name = "btnGoToVEnd";
            this.btnGoToVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToVEnd.TabIndex = 43;
            this.btnGoToVEnd.Text = "Go To";
            this.btnGoToVEnd.UseVisualStyleBackColor = true;
            this.btnGoToVEnd.Click += new System.EventHandler(this.btnGoToVEnd_Click);
            // 
            // btnGoToHEnd
            // 
            this.btnGoToHEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToHEnd.ForeColor = System.Drawing.Color.Black;
            this.btnGoToHEnd.Location = new System.Drawing.Point(374, 126);
            this.btnGoToHEnd.Name = "btnGoToHEnd";
            this.btnGoToHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToHEnd.TabIndex = 42;
            this.btnGoToHEnd.Text = "Go To";
            this.btnGoToHEnd.UseVisualStyleBackColor = true;
            this.btnGoToHEnd.Click += new System.EventHandler(this.btnGoToHEnd_Click);
            // 
            // btnGoToOrigin
            // 
            this.btnGoToOrigin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToOrigin.ForeColor = System.Drawing.Color.Black;
            this.btnGoToOrigin.Location = new System.Drawing.Point(374, 83);
            this.btnGoToOrigin.Name = "btnGoToOrigin";
            this.btnGoToOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOrigin.TabIndex = 41;
            this.btnGoToOrigin.Text = "Go To";
            this.btnGoToOrigin.UseVisualStyleBackColor = true;
            this.btnGoToOrigin.Click += new System.EventHandler(this.btnGoToOrigin_Click);
            // 
            // btnSelectVEnd
            // 
            this.btnSelectVEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectVEnd.ForeColor = System.Drawing.Color.Black;
            this.btnSelectVEnd.Location = new System.Drawing.Point(293, 169);
            this.btnSelectVEnd.Name = "btnSelectVEnd";
            this.btnSelectVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectVEnd.TabIndex = 40;
            this.btnSelectVEnd.Text = "Teach";
            this.btnSelectVEnd.UseVisualStyleBackColor = true;
            this.btnSelectVEnd.Click += new System.EventHandler(this.btnSelectVEnd_Click);
            // 
            // btnSelectHEnd
            // 
            this.btnSelectHEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectHEnd.ForeColor = System.Drawing.Color.Black;
            this.btnSelectHEnd.Location = new System.Drawing.Point(293, 126);
            this.btnSelectHEnd.Name = "btnSelectHEnd";
            this.btnSelectHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectHEnd.TabIndex = 44;
            this.btnSelectHEnd.Text = "Teach";
            this.btnSelectHEnd.UseVisualStyleBackColor = true;
            this.btnSelectHEnd.Click += new System.EventHandler(this.btnSelectHEnd_Click);
            // 
            // btnSelectOrigin
            // 
            this.btnSelectOrigin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectOrigin.ForeColor = System.Drawing.Color.Black;
            this.btnSelectOrigin.Location = new System.Drawing.Point(293, 83);
            this.btnSelectOrigin.Name = "btnSelectOrigin";
            this.btnSelectOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOrigin.TabIndex = 39;
            this.btnSelectOrigin.Text = "Teach";
            this.btnSelectOrigin.UseVisualStyleBackColor = true;
            this.btnSelectOrigin.Click += new System.EventHandler(this.btnSelectOrigin_Click);
            // 
            // tbVEndY
            // 
            this.tbVEndY.BackColor = System.Drawing.Color.White;
            this.tbVEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVEndY.Location = new System.Drawing.Point(213, 169);
            this.tbVEndY.Name = "tbVEndY";
            this.tbVEndY.Size = new System.Drawing.Size(74, 22);
            this.tbVEndY.TabIndex = 38;
            this.tbVEndY.Text = "0.000";
            this.tbVEndY.TextChanged += new System.EventHandler(this.tbVEndY_TextChanged);
            // 
            // tbHEndY
            // 
            this.tbHEndY.BackColor = System.Drawing.Color.White;
            this.tbHEndY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHEndY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHEndY.Location = new System.Drawing.Point(213, 127);
            this.tbHEndY.Name = "tbHEndY";
            this.tbHEndY.Size = new System.Drawing.Size(74, 22);
            this.tbHEndY.TabIndex = 37;
            this.tbHEndY.Text = "0.000";
            this.tbHEndY.TextChanged += new System.EventHandler(this.tbHEndY_TextChanged);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginY.Location = new System.Drawing.Point(213, 85);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(74, 22);
            this.tbOriginY.TabIndex = 36;
            this.tbOriginY.Text = "0.000";
            this.tbOriginY.TextChanged += new System.EventHandler(this.tbOriginY_TextChanged);
            // 
            // tbVEndX
            // 
            this.tbVEndX.BackColor = System.Drawing.Color.White;
            this.tbVEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbVEndX.Location = new System.Drawing.Point(133, 169);
            this.tbVEndX.Name = "tbVEndX";
            this.tbVEndX.Size = new System.Drawing.Size(74, 22);
            this.tbVEndX.TabIndex = 35;
            this.tbVEndX.Text = "0.000";
            this.tbVEndX.TextChanged += new System.EventHandler(this.tbVEndX_TextChanged);
            // 
            // tbHEndX
            // 
            this.tbHEndX.BackColor = System.Drawing.Color.White;
            this.tbHEndX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHEndX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbHEndX.Location = new System.Drawing.Point(133, 127);
            this.tbHEndX.Name = "tbHEndX";
            this.tbHEndX.Size = new System.Drawing.Size(74, 22);
            this.tbHEndX.TabIndex = 34;
            this.tbHEndX.Text = "0.000";
            this.tbHEndX.TextChanged += new System.EventHandler(this.tbHEndX_TextChanged);
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOriginX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOriginX.Location = new System.Drawing.Point(133, 85);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(74, 22);
            this.tbOriginX.TabIndex = 31;
            this.tbOriginX.Text = "0.000";
            this.tbOriginX.TextChanged += new System.EventHandler(this.tbOriginX_TextChanged);
            // 
            // lblVEnd
            // 
            this.lblVEnd.AutoSize = true;
            this.lblVEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblVEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVEnd.ForeColor = System.Drawing.Color.White;
            this.lblVEnd.Location = new System.Drawing.Point(130, 152);
            this.lblVEnd.Name = "lblVEnd";
            this.lblVEnd.Size = new System.Drawing.Size(91, 14);
            this.lblVEnd.TabIndex = 29;
            this.lblVEnd.Text = "Vertical End:";
            // 
            // lblHEnd
            // 
            this.lblHEnd.AutoSize = true;
            this.lblHEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblHEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHEnd.ForeColor = System.Drawing.Color.White;
            this.lblHEnd.Location = new System.Drawing.Point(130, 110);
            this.lblHEnd.Name = "lblHEnd";
            this.lblHEnd.Size = new System.Drawing.Size(108, 14);
            this.lblHEnd.TabIndex = 30;
            this.lblHEnd.Text = "Horizontal End:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(130, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 28;
            this.label1.Text = "Origin:";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(366, 252);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 55;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.Color.Black;
            this.btnPrev.Location = new System.Drawing.Point(366, 223);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 54;
            this.btnPrev.Text = "上一个";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTo.ForeColor = System.Drawing.Color.Black;
            this.btnGoTo.Location = new System.Drawing.Point(366, 293);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 53;
            this.btnGoTo.Text = "移动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // lblPatternOriginList
            // 
            this.lblPatternOriginList.AutoSize = true;
            this.lblPatternOriginList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPatternOriginList.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatternOriginList.Location = new System.Drawing.Point(15, 206);
            this.lblPatternOriginList.Name = "lblPatternOriginList";
            this.lblPatternOriginList.Size = new System.Drawing.Size(134, 14);
            this.lblPatternOriginList.TabIndex = 52;
            this.lblPatternOriginList.Text = "Pattern Origin List:";
            // 
            // listBoxOrigins
            // 
            this.listBoxOrigins.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxOrigins.FormattingEnabled = true;
            this.listBoxOrigins.ItemHeight = 14;
            this.listBoxOrigins.Location = new System.Drawing.Point(9, 223);
            this.listBoxOrigins.Name = "listBoxOrigins";
            this.listBoxOrigins.Size = new System.Drawing.Size(351, 186);
            this.listBoxOrigins.TabIndex = 51;
            this.listBoxOrigins.SelectedIndexChanged += new System.EventHandler(this.listBoxOrigins_SelectedIndexChanged);
            // 
            // cmsValveType
            // 
            this.cmsValveType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiValve1,
            this.tsiValve2});
            this.cmsValveType.Name = "cmsValveType";
            this.cmsValveType.Size = new System.Drawing.Size(120, 48);
            // 
            // tsiValve1
            // 
            this.tsiValve1.Name = "tsiValve1";
            this.tsiValve1.Size = new System.Drawing.Size(119, 22);
            this.tsiValve1.Text = "指定阀1";
            this.tsiValve1.Click += new System.EventHandler(this.tsiValve1_Click);
            // 
            // tsiValve2
            // 
            this.tsiValve2.Name = "tsiValve2";
            this.tsiValve2.Size = new System.Drawing.Size(119, 22);
            this.tsiValve2.Text = "指定阀2";
            this.tsiValve2.Click += new System.EventHandler(this.tsiValve2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(366, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // EditMultipassArrayMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.lblPatternOriginList);
            this.Controls.Add(this.listBoxOrigins);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.listBoxPatterns);
            this.Controls.Add(this.tbVNums);
            this.Controls.Add(this.lblPatternList);
            this.Controls.Add(this.tbHNums);
            this.Controls.Add(this.lblVNums);
            this.Controls.Add(this.lblHNums);
            this.Controls.Add(this.btnGoToVEnd);
            this.Controls.Add(this.btnGoToHEnd);
            this.Controls.Add(this.btnGoToOrigin);
            this.Controls.Add(this.btnSelectVEnd);
            this.Controls.Add(this.btnSelectHEnd);
            this.Controls.Add(this.btnSelectOrigin);
            this.Controls.Add(this.tbVEndY);
            this.Controls.Add(this.tbHEndY);
            this.Controls.Add(this.tbOriginY);
            this.Controls.Add(this.tbVEndX);
            this.Controls.Add(this.tbHEndX);
            this.Controls.Add(this.tbOriginX);
            this.Controls.Add(this.lblVEnd);
            this.Controls.Add(this.lblHEnd);
            this.Controls.Add(this.label1);
            this.Name = "EditMultipassArrayMetro";
            this.Size = new System.Drawing.Size(456, 425);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.cmsValveType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox listBoxPatterns;
        private Controls.IntTextBox tbVNums;
        private System.Windows.Forms.Label lblPatternList;
        private Controls.IntTextBox tbHNums;
        private System.Windows.Forms.Label lblVNums;
        private System.Windows.Forms.Label lblHNums;
        private System.Windows.Forms.Button btnGoToVEnd;
        private System.Windows.Forms.Button btnGoToHEnd;
        private System.Windows.Forms.Button btnGoToOrigin;
        private System.Windows.Forms.Button btnSelectVEnd;
        private System.Windows.Forms.Button btnSelectHEnd;
        private System.Windows.Forms.Button btnSelectOrigin;
        private Controls.DoubleTextBox tbVEndY;
        private Controls.DoubleTextBox tbHEndY;
        private Controls.DoubleTextBox tbOriginY;
        private Controls.DoubleTextBox tbVEndX;
        private Controls.DoubleTextBox tbHEndX;
        private Controls.DoubleTextBox tbOriginX;
        private System.Windows.Forms.Label lblVEnd;
        private System.Windows.Forms.Label lblHEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Label lblPatternOriginList;
        private System.Windows.Forms.ListBox listBoxOrigins;
        private System.Windows.Forms.ContextMenuStrip cmsValveType;
        private System.Windows.Forms.ToolStripMenuItem tsiValve1;
        private System.Windows.Forms.ToolStripMenuItem tsiValve2;
        private System.Windows.Forms.Button button1;
    }
}
