namespace Anda.Fluid.App.Metro.Pages
{
    partial class PageJobsProduction
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
            this.panelDraw = new MetroSet_UI.Controls.MetroSetPanel();
            this.lblSetupInfo = new MetroSet_UI.Controls.MetroSetLabel();
            this.cmbRunMode = new MetroSet_UI.Controls.MetroSetComboBox();
            this.txtCycleTime = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel8 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtStartTime = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel7 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtFailedCount = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel6 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtPassCount = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel5 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtBoardCount = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel4 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel3 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel2 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtProgramName = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtSetNum = new MetroSet_UI.Controls.MetroSetTextBox();
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
            // panelDraw
            // 
            this.panelDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDraw.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelDraw.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.panelDraw.BorderThickness = 1;
            this.panelDraw.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.panelDraw.Location = new System.Drawing.Point(0, 265);
            this.panelDraw.Name = "panelDraw";
            this.panelDraw.Size = new System.Drawing.Size(768, 565);
            this.panelDraw.Style = MetroSet_UI.Design.Style.Dark;
            this.panelDraw.StyleManager = this.styleManager1;
            this.panelDraw.TabIndex = 0;
            this.panelDraw.ThemeAuthor = "Narwin";
            this.panelDraw.ThemeName = "MetroDark";
            // 
            // lblSetupInfo
            // 
            this.lblSetupInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSetupInfo.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSetupInfo.Location = new System.Drawing.Point(556, 15);
            this.lblSetupInfo.Name = "lblSetupInfo";
            this.lblSetupInfo.Size = new System.Drawing.Size(198, 244);
            this.lblSetupInfo.Style = MetroSet_UI.Design.Style.Dark;
            this.lblSetupInfo.StyleManager = this.styleManager1;
            this.lblSetupInfo.TabIndex = 18;
            this.lblSetupInfo.Text = "setup Info";
            this.lblSetupInfo.ThemeAuthor = "Narwin";
            this.lblSetupInfo.ThemeName = "MetroDark";
            // 
            // cmbRunMode
            // 
            this.cmbRunMode.AllowDrop = true;
            this.cmbRunMode.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.cmbRunMode.BackColor = System.Drawing.Color.Transparent;
            this.cmbRunMode.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.cmbRunMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.cmbRunMode.CausesValidation = false;
            this.cmbRunMode.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmbRunMode.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cmbRunMode.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cmbRunMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRunMode.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.cmbRunMode.FormattingEnabled = true;
            this.cmbRunMode.ItemHeight = 20;
            this.cmbRunMode.Location = new System.Drawing.Point(117, 46);
            this.cmbRunMode.Name = "cmbRunMode";
            this.cmbRunMode.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cmbRunMode.SelectedItemForeColor = System.Drawing.Color.White;
            this.cmbRunMode.Size = new System.Drawing.Size(350, 26);
            this.cmbRunMode.Style = MetroSet_UI.Design.Style.Dark;
            this.cmbRunMode.StyleManager = this.styleManager1;
            this.cmbRunMode.TabIndex = 16;
            this.cmbRunMode.ThemeAuthor = "Narwin";
            this.cmbRunMode.ThemeName = "MetroDark";
            // 
            // txtCycleTime
            // 
            this.txtCycleTime.AutoCompleteCustomSource = null;
            this.txtCycleTime.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCycleTime.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCycleTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtCycleTime.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtCycleTime.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCycleTime.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCycleTime.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtCycleTime.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtCycleTime.Image = null;
            this.txtCycleTime.Lines = null;
            this.txtCycleTime.Location = new System.Drawing.Point(117, 234);
            this.txtCycleTime.MaxLength = 32767;
            this.txtCycleTime.Multiline = false;
            this.txtCycleTime.Name = "txtCycleTime";
            this.txtCycleTime.ReadOnly = false;
            this.txtCycleTime.Size = new System.Drawing.Size(350, 25);
            this.txtCycleTime.Style = MetroSet_UI.Design.Style.Dark;
            this.txtCycleTime.StyleManager = this.styleManager1;
            this.txtCycleTime.TabIndex = 15;
            this.txtCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCycleTime.ThemeAuthor = "Narwin";
            this.txtCycleTime.ThemeName = "MetroDark";
            this.txtCycleTime.UseSystemPasswordChar = false;
            this.txtCycleTime.WatermarkText = "";
            // 
            // metroSetLabel8
            // 
            this.metroSetLabel8.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel8.Location = new System.Drawing.Point(16, 236);
            this.metroSetLabel8.Name = "metroSetLabel8";
            this.metroSetLabel8.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel8.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel8.StyleManager = this.styleManager1;
            this.metroSetLabel8.TabIndex = 14;
            this.metroSetLabel8.Text = "CycleTime:";
            this.metroSetLabel8.ThemeAuthor = "Narwin";
            this.metroSetLabel8.ThemeName = "MetroDark";
            // 
            // txtStartTime
            // 
            this.txtStartTime.AutoCompleteCustomSource = null;
            this.txtStartTime.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtStartTime.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtStartTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtStartTime.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtStartTime.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStartTime.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStartTime.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtStartTime.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtStartTime.Image = null;
            this.txtStartTime.Lines = null;
            this.txtStartTime.Location = new System.Drawing.Point(117, 203);
            this.txtStartTime.MaxLength = 32767;
            this.txtStartTime.Multiline = false;
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = false;
            this.txtStartTime.Size = new System.Drawing.Size(350, 25);
            this.txtStartTime.Style = MetroSet_UI.Design.Style.Dark;
            this.txtStartTime.StyleManager = this.styleManager1;
            this.txtStartTime.TabIndex = 13;
            this.txtStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtStartTime.ThemeAuthor = "Narwin";
            this.txtStartTime.ThemeName = "MetroDark";
            this.txtStartTime.UseSystemPasswordChar = false;
            this.txtStartTime.WatermarkText = "";
            // 
            // metroSetLabel7
            // 
            this.metroSetLabel7.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel7.Location = new System.Drawing.Point(16, 205);
            this.metroSetLabel7.Name = "metroSetLabel7";
            this.metroSetLabel7.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel7.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel7.StyleManager = this.styleManager1;
            this.metroSetLabel7.TabIndex = 12;
            this.metroSetLabel7.Text = "开始时间：";
            this.metroSetLabel7.ThemeAuthor = "Narwin";
            this.metroSetLabel7.ThemeName = "MetroDark";
            // 
            // txtFailedCount
            // 
            this.txtFailedCount.AutoCompleteCustomSource = null;
            this.txtFailedCount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtFailedCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtFailedCount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtFailedCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtFailedCount.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFailedCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFailedCount.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtFailedCount.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtFailedCount.Image = null;
            this.txtFailedCount.Lines = null;
            this.txtFailedCount.Location = new System.Drawing.Point(117, 172);
            this.txtFailedCount.MaxLength = 32767;
            this.txtFailedCount.Multiline = false;
            this.txtFailedCount.Name = "txtFailedCount";
            this.txtFailedCount.ReadOnly = false;
            this.txtFailedCount.Size = new System.Drawing.Size(350, 25);
            this.txtFailedCount.Style = MetroSet_UI.Design.Style.Dark;
            this.txtFailedCount.StyleManager = this.styleManager1;
            this.txtFailedCount.TabIndex = 11;
            this.txtFailedCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFailedCount.ThemeAuthor = "Narwin";
            this.txtFailedCount.ThemeName = "MetroDark";
            this.txtFailedCount.UseSystemPasswordChar = false;
            this.txtFailedCount.WatermarkText = "";
            // 
            // metroSetLabel6
            // 
            this.metroSetLabel6.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel6.Location = new System.Drawing.Point(16, 174);
            this.metroSetLabel6.Name = "metroSetLabel6";
            this.metroSetLabel6.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel6.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel6.StyleManager = this.styleManager1;
            this.metroSetLabel6.TabIndex = 10;
            this.metroSetLabel6.Text = "失败板数：";
            this.metroSetLabel6.ThemeAuthor = "Narwin";
            this.metroSetLabel6.ThemeName = "MetroDark";
            // 
            // txtPassCount
            // 
            this.txtPassCount.AutoCompleteCustomSource = null;
            this.txtPassCount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPassCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPassCount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtPassCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtPassCount.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassCount.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtPassCount.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtPassCount.Image = null;
            this.txtPassCount.Lines = null;
            this.txtPassCount.Location = new System.Drawing.Point(117, 141);
            this.txtPassCount.MaxLength = 32767;
            this.txtPassCount.Multiline = false;
            this.txtPassCount.Name = "txtPassCount";
            this.txtPassCount.ReadOnly = false;
            this.txtPassCount.Size = new System.Drawing.Size(350, 25);
            this.txtPassCount.Style = MetroSet_UI.Design.Style.Dark;
            this.txtPassCount.StyleManager = this.styleManager1;
            this.txtPassCount.TabIndex = 9;
            this.txtPassCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPassCount.ThemeAuthor = "Narwin";
            this.txtPassCount.ThemeName = "MetroDark";
            this.txtPassCount.UseSystemPasswordChar = false;
            this.txtPassCount.WatermarkText = "";
            // 
            // metroSetLabel5
            // 
            this.metroSetLabel5.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel5.Location = new System.Drawing.Point(16, 143);
            this.metroSetLabel5.Name = "metroSetLabel5";
            this.metroSetLabel5.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel5.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel5.StyleManager = this.styleManager1;
            this.metroSetLabel5.TabIndex = 8;
            this.metroSetLabel5.Text = "成功板数：";
            this.metroSetLabel5.ThemeAuthor = "Narwin";
            this.metroSetLabel5.ThemeName = "MetroDark";
            // 
            // txtBoardCount
            // 
            this.txtBoardCount.AutoCompleteCustomSource = null;
            this.txtBoardCount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBoardCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBoardCount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtBoardCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtBoardCount.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBoardCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBoardCount.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtBoardCount.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtBoardCount.Image = null;
            this.txtBoardCount.Lines = null;
            this.txtBoardCount.Location = new System.Drawing.Point(117, 110);
            this.txtBoardCount.MaxLength = 32767;
            this.txtBoardCount.Multiline = false;
            this.txtBoardCount.Name = "txtBoardCount";
            this.txtBoardCount.ReadOnly = false;
            this.txtBoardCount.Size = new System.Drawing.Size(350, 25);
            this.txtBoardCount.Style = MetroSet_UI.Design.Style.Dark;
            this.txtBoardCount.StyleManager = this.styleManager1;
            this.txtBoardCount.TabIndex = 7;
            this.txtBoardCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBoardCount.ThemeAuthor = "Narwin";
            this.txtBoardCount.ThemeName = "MetroDark";
            this.txtBoardCount.UseSystemPasswordChar = false;
            this.txtBoardCount.WatermarkText = "";
            // 
            // metroSetLabel4
            // 
            this.metroSetLabel4.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel4.Location = new System.Drawing.Point(16, 112);
            this.metroSetLabel4.Name = "metroSetLabel4";
            this.metroSetLabel4.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel4.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel4.StyleManager = this.styleManager1;
            this.metroSetLabel4.TabIndex = 6;
            this.metroSetLabel4.Text = "总板数：";
            this.metroSetLabel4.ThemeAuthor = "Narwin";
            this.metroSetLabel4.ThemeName = "MetroDark";
            // 
            // metroSetLabel3
            // 
            this.metroSetLabel3.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel3.Location = new System.Drawing.Point(16, 80);
            this.metroSetLabel3.Name = "metroSetLabel3";
            this.metroSetLabel3.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel3.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel3.StyleManager = this.styleManager1;
            this.metroSetLabel3.TabIndex = 4;
            this.metroSetLabel3.Text = "设置板数：";
            this.metroSetLabel3.ThemeAuthor = "Narwin";
            this.metroSetLabel3.ThemeName = "MetroDark";
            // 
            // metroSetLabel2
            // 
            this.metroSetLabel2.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel2.Location = new System.Drawing.Point(16, 48);
            this.metroSetLabel2.Name = "metroSetLabel2";
            this.metroSetLabel2.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel2.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel2.StyleManager = this.styleManager1;
            this.metroSetLabel2.TabIndex = 2;
            this.metroSetLabel2.Text = "运行模式：";
            this.metroSetLabel2.ThemeAuthor = "Narwin";
            this.metroSetLabel2.ThemeName = "MetroDark";
            // 
            // txtProgramName
            // 
            this.txtProgramName.AutoCompleteCustomSource = null;
            this.txtProgramName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProgramName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProgramName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtProgramName.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtProgramName.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProgramName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProgramName.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtProgramName.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtProgramName.Image = null;
            this.txtProgramName.Lines = null;
            this.txtProgramName.Location = new System.Drawing.Point(117, 15);
            this.txtProgramName.MaxLength = 32767;
            this.txtProgramName.Multiline = false;
            this.txtProgramName.Name = "txtProgramName";
            this.txtProgramName.ReadOnly = false;
            this.txtProgramName.Size = new System.Drawing.Size(350, 25);
            this.txtProgramName.Style = MetroSet_UI.Design.Style.Dark;
            this.txtProgramName.StyleManager = this.styleManager1;
            this.txtProgramName.TabIndex = 1;
            this.txtProgramName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProgramName.ThemeAuthor = "Narwin";
            this.txtProgramName.ThemeName = "MetroDark";
            this.txtProgramName.UseSystemPasswordChar = false;
            this.txtProgramName.WatermarkText = "";
            // 
            // metroSetLabel1
            // 
            this.metroSetLabel1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.metroSetLabel1.Location = new System.Drawing.Point(16, 17);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(95, 23);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetLabel1.StyleManager = this.styleManager1;
            this.metroSetLabel1.TabIndex = 0;
            this.metroSetLabel1.Text = "程序名称：";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "MetroDark";
            // 
            // txtSetNum
            // 
            this.txtSetNum.AutoCompleteCustomSource = null;
            this.txtSetNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSetNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSetNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.txtSetNum.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txtSetNum.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSetNum.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSetNum.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtSetNum.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.txtSetNum.Image = null;
            this.txtSetNum.Lines = null;
            this.txtSetNum.Location = new System.Drawing.Point(117, 78);
            this.txtSetNum.MaxLength = 32767;
            this.txtSetNum.Multiline = false;
            this.txtSetNum.Name = "txtSetNum";
            this.txtSetNum.ReadOnly = false;
            this.txtSetNum.Size = new System.Drawing.Size(350, 25);
            this.txtSetNum.Style = MetroSet_UI.Design.Style.Dark;
            this.txtSetNum.StyleManager = this.styleManager1;
            this.txtSetNum.TabIndex = 19;
            this.txtSetNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSetNum.ThemeAuthor = "Narwin";
            this.txtSetNum.ThemeName = "MetroDark";
            this.txtSetNum.UseSystemPasswordChar = false;
            this.txtSetNum.WatermarkText = "";
            // 
            // PageJobsProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.txtSetNum);
            this.Controls.Add(this.lblSetupInfo);
            this.Controls.Add(this.panelDraw);
            this.Controls.Add(this.txtProgramName);
            this.Controls.Add(this.cmbRunMode);
            this.Controls.Add(this.metroSetLabel1);
            this.Controls.Add(this.txtCycleTime);
            this.Controls.Add(this.metroSetLabel2);
            this.Controls.Add(this.metroSetLabel8);
            this.Controls.Add(this.metroSetLabel3);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.metroSetLabel4);
            this.Controls.Add(this.metroSetLabel7);
            this.Controls.Add(this.txtBoardCount);
            this.Controls.Add(this.txtFailedCount);
            this.Controls.Add(this.metroSetLabel5);
            this.Controls.Add(this.metroSetLabel6);
            this.Controls.Add(this.txtPassCount);
            this.Name = "PageJobsProduction";
            this.Size = new System.Drawing.Size(768, 830);
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetPanel panelDraw;
        private MetroSet_UI.Controls.MetroSetTextBox txtFailedCount;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel6;
        private MetroSet_UI.Controls.MetroSetTextBox txtPassCount;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel5;
        private MetroSet_UI.Controls.MetroSetTextBox txtBoardCount;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel4;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel3;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel2;
        private MetroSet_UI.Controls.MetroSetTextBox txtProgramName;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private MetroSet_UI.Controls.MetroSetTextBox txtCycleTime;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel8;
        private MetroSet_UI.Controls.MetroSetTextBox txtStartTime;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel7;
        private MetroSet_UI.Controls.MetroSetComboBox cmbRunMode;
        private MetroSet_UI.Controls.MetroSetLabel lblSetupInfo;
        private MetroSet_UI.Controls.MetroSetTextBox txtSetNum;
    }
}
