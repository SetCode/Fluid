namespace Anda.Fluid.App.LoadTrajectory
{
    partial class DialogImport
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
            this.lstHead = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lstHeadSelected = new System.Windows.Forms.ListBox();
            this.gpbHeadSel = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLay = new System.Windows.Forms.Label();
            this.cmbLayout = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnAddHead = new System.Windows.Forms.Button();
            this.gpbSeperator = new System.Windows.Forms.GroupBox();
            this.btnSepeDel = new System.Windows.Forms.Button();
            this.btnSepApply = new System.Windows.Forms.Button();
            this.btnAddSep = new System.Windows.Forms.Button();
            this.txtSeperators = new System.Windows.Forms.TextBox();
            this.cmbSeperators = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lswComponents = new System.Windows.Forms.ListView();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gpbLoad = new System.Windows.Forms.GroupBox();
            this.ckbUnit = new System.Windows.Forms.CheckBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.ckbLoadDefault = new System.Windows.Forms.CheckBox();
            this.lswPath = new System.Windows.Forms.ListView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPathDelete = new System.Windows.Forms.Button();
            this.lstStdHead = new System.Windows.Forms.ListBox();
            this.gpbHeadSel.SuspendLayout();
            this.gpbSeperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpbLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstHead
            // 
            this.lstHead.FormattingEnabled = true;
            this.lstHead.ItemHeight = 14;
            this.lstHead.Location = new System.Drawing.Point(7, 21);
            this.lstHead.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstHead.Name = "lstHead";
            this.lstHead.Size = new System.Drawing.Size(116, 214);
            this.lstHead.TabIndex = 0;
            this.lstHead.SelectedIndexChanged += new System.EventHandler(this.lstHead_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 493);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(332, 493);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstHeadSelected
            // 
            this.lstHeadSelected.FormattingEnabled = true;
            this.lstHeadSelected.ItemHeight = 14;
            this.lstHeadSelected.Location = new System.Drawing.Point(223, 21);
            this.lstHeadSelected.Name = "lstHeadSelected";
            this.lstHeadSelected.Size = new System.Drawing.Size(187, 214);
            this.lstHeadSelected.TabIndex = 3;
            // 
            // gpbHeadSel
            // 
            this.gpbHeadSel.Controls.Add(this.lstStdHead);
            this.gpbHeadSel.Controls.Add(this.btnSave);
            this.gpbHeadSel.Controls.Add(this.lblLay);
            this.gpbHeadSel.Controls.Add(this.cmbLayout);
            this.gpbHeadSel.Controls.Add(this.btnDelete);
            this.gpbHeadSel.Controls.Add(this.btnApply);
            this.gpbHeadSel.Controls.Add(this.btnAddHead);
            this.gpbHeadSel.Controls.Add(this.lstHead);
            this.gpbHeadSel.Controls.Add(this.lstHeadSelected);
            this.gpbHeadSel.Location = new System.Drawing.Point(9, 237);
            this.gpbHeadSel.Name = "gpbHeadSel";
            this.gpbHeadSel.Size = new System.Drawing.Size(529, 244);
            this.gpbHeadSel.TabIndex = 4;
            this.gpbHeadSel.TabStop = false;
            this.gpbHeadSel.Text = "HeadSelect";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(418, 199);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLay
            // 
            this.lblLay.AutoSize = true;
            this.lblLay.Location = new System.Drawing.Point(415, 148);
            this.lblLay.Name = "lblLay";
            this.lblLay.Size = new System.Drawing.Size(60, 14);
            this.lblLay.TabIndex = 9;
            this.lblLay.Text = "LayOut:";
            // 
            // cmbLayout
            // 
            this.cmbLayout.FormattingEnabled = true;
            this.cmbLayout.Location = new System.Drawing.Point(418, 167);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(75, 22);
            this.cmbLayout.TabIndex = 8;
            this.cmbLayout.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(418, 52);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(418, 99);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnAddHead
            // 
            this.btnAddHead.Location = new System.Drawing.Point(130, 180);
            this.btnAddHead.Name = "btnAddHead";
            this.btnAddHead.Size = new System.Drawing.Size(87, 23);
            this.btnAddHead.TabIndex = 4;
            this.btnAddHead.Text = ">>";
            this.btnAddHead.UseVisualStyleBackColor = true;
            this.btnAddHead.Click += new System.EventHandler(this.button1_Click);
            // 
            // gpbSeperator
            // 
            this.gpbSeperator.Controls.Add(this.btnSepeDel);
            this.gpbSeperator.Controls.Add(this.btnSepApply);
            this.gpbSeperator.Controls.Add(this.btnAddSep);
            this.gpbSeperator.Controls.Add(this.txtSeperators);
            this.gpbSeperator.Controls.Add(this.cmbSeperators);
            this.gpbSeperator.Location = new System.Drawing.Point(8, 143);
            this.gpbSeperator.Name = "gpbSeperator";
            this.gpbSeperator.Size = new System.Drawing.Size(530, 88);
            this.gpbSeperator.TabIndex = 5;
            this.gpbSeperator.TabStop = false;
            this.gpbSeperator.Text = "Seperator";
            // 
            // btnSepeDel
            // 
            this.btnSepeDel.Location = new System.Drawing.Point(418, 21);
            this.btnSepeDel.Name = "btnSepeDel";
            this.btnSepeDel.Size = new System.Drawing.Size(75, 23);
            this.btnSepeDel.TabIndex = 8;
            this.btnSepeDel.Text = "Delete";
            this.btnSepeDel.UseVisualStyleBackColor = true;
            this.btnSepeDel.Click += new System.EventHandler(this.btnSepeDel_Click);
            // 
            // btnSepApply
            // 
            this.btnSepApply.Location = new System.Drawing.Point(418, 58);
            this.btnSepApply.Name = "btnSepApply";
            this.btnSepApply.Size = new System.Drawing.Size(75, 23);
            this.btnSepApply.TabIndex = 3;
            this.btnSepApply.Text = "Apply";
            this.btnSepApply.UseVisualStyleBackColor = true;
            this.btnSepApply.Click += new System.EventHandler(this.btnSepApply_Click);
            // 
            // btnAddSep
            // 
            this.btnAddSep.Location = new System.Drawing.Point(130, 20);
            this.btnAddSep.Name = "btnAddSep";
            this.btnAddSep.Size = new System.Drawing.Size(75, 23);
            this.btnAddSep.TabIndex = 2;
            this.btnAddSep.Text = ">>";
            this.btnAddSep.UseVisualStyleBackColor = true;
            this.btnAddSep.Click += new System.EventHandler(this.btnAddSep_Click);
            // 
            // txtSeperators
            // 
            this.txtSeperators.Location = new System.Drawing.Point(211, 21);
            this.txtSeperators.Name = "txtSeperators";
            this.txtSeperators.ReadOnly = true;
            this.txtSeperators.Size = new System.Drawing.Size(187, 22);
            this.txtSeperators.TabIndex = 1;
            // 
            // cmbSeperators
            // 
            this.cmbSeperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeperators.FormattingEnabled = true;
            this.cmbSeperators.Location = new System.Drawing.Point(7, 21);
            this.cmbSeperators.Name = "cmbSeperators";
            this.cmbSeperators.Size = new System.Drawing.Size(116, 22);
            this.cmbSeperators.TabIndex = 0;
            this.cmbSeperators.SelectedIndexChanged += new System.EventHandler(this.cmbSeperators_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lswComponents);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
            this.splitContainer1.Panel2.Controls.Add(this.gpbLoad);
            this.splitContainer1.Panel2.Controls.Add(this.gpbSeperator);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.gpbHeadSel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Size = new System.Drawing.Size(848, 522);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 6;
            // 
            // lswComponents
            // 
            this.lswComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lswComponents.Location = new System.Drawing.Point(0, 0);
            this.lswComponents.Name = "lswComponents";
            this.lswComponents.Size = new System.Drawing.Size(302, 522);
            this.lswComponents.TabIndex = 0;
            this.lswComponents.UseCompatibleStateImageBehavior = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(442, 493);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 18;
            this.btnHelp.Text = "help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // gpbLoad
            // 
            this.gpbLoad.Controls.Add(this.btnPathDelete);
            this.gpbLoad.Controls.Add(this.ckbUnit);
            this.gpbLoad.Controls.Add(this.cmbUnit);
            this.gpbLoad.Controls.Add(this.ckbLoadDefault);
            this.gpbLoad.Controls.Add(this.lswPath);
            this.gpbLoad.Controls.Add(this.btnLoad);
            this.gpbLoad.Location = new System.Drawing.Point(8, 5);
            this.gpbLoad.Name = "gpbLoad";
            this.gpbLoad.Size = new System.Drawing.Size(530, 132);
            this.gpbLoad.TabIndex = 17;
            this.gpbLoad.TabStop = false;
            this.gpbLoad.Text = "Load";
            // 
            // ckbUnit
            // 
            this.ckbUnit.AutoSize = true;
            this.ckbUnit.Location = new System.Drawing.Point(386, 78);
            this.ckbUnit.Name = "ckbUnit";
            this.ckbUnit.Size = new System.Drawing.Size(58, 18);
            this.ckbUnit.TabIndex = 14;
            this.ckbUnit.Text = "Unit:";
            this.ckbUnit.UseVisualStyleBackColor = true;
            // 
            // cmbUnit
            // 
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(387, 99);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(75, 22);
            this.cmbUnit.TabIndex = 12;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // ckbLoadDefault
            // 
            this.ckbLoadDefault.AutoSize = true;
            this.ckbLoadDefault.Location = new System.Drawing.Point(386, 54);
            this.ckbLoadDefault.Name = "ckbLoadDefault";
            this.ckbLoadDefault.Size = new System.Drawing.Size(106, 18);
            this.ckbLoadDefault.TabIndex = 9;
            this.ckbLoadDefault.Text = "LoadDefault";
            this.ckbLoadDefault.UseVisualStyleBackColor = true;
            // 
            // lswPath
            // 
            this.lswPath.Location = new System.Drawing.Point(9, 21);
            this.lswPath.Name = "lswPath";
            this.lswPath.Size = new System.Drawing.Size(364, 105);
            this.lswPath.TabIndex = 7;
            this.lswPath.UseCompatibleStateImageBehavior = false;
            this.lswPath.SelectedIndexChanged += new System.EventHandler(this.lswPath_SelectedIndexChanged);
            this.lswPath.MouseLeave += new System.EventHandler(this.lswPath_MouseLeave);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(380, 21);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(64, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnPathDelete
            // 
            this.btnPathDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPathDelete.Location = new System.Drawing.Point(461, 21);
            this.btnPathDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPathDelete.Name = "btnPathDelete";
            this.btnPathDelete.Size = new System.Drawing.Size(64, 23);
            this.btnPathDelete.TabIndex = 15;
            this.btnPathDelete.Text = "Delete";
            this.btnPathDelete.UseVisualStyleBackColor = true;
            this.btnPathDelete.Click += new System.EventHandler(this.btnPathDelete_Click);
            // 
            // lstStdHead
            // 
            this.lstStdHead.FormattingEnabled = true;
            this.lstStdHead.ItemHeight = 14;
            this.lstStdHead.Location = new System.Drawing.Point(129, 24);
            this.lstStdHead.Name = "lstStdHead";
            this.lstStdHead.Size = new System.Drawing.Size(88, 102);
            this.lstStdHead.TabIndex = 12;
            this.lstStdHead.SelectedIndexChanged += new System.EventHandler(this.lstStdHead_SelectedIndexChanged);
            // 
            // DialogImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 522);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogImport";
            this.Text = "ImportEdit";
            this.Load += new System.EventHandler(this.ImportEdit_Load);
            this.gpbHeadSel.ResumeLayout(false);
            this.gpbHeadSel.PerformLayout();
            this.gpbSeperator.ResumeLayout(false);
            this.gpbSeperator.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpbLoad.ResumeLayout(false);
            this.gpbLoad.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstHead;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstHeadSelected;
        private System.Windows.Forms.GroupBox gpbHeadSel;
        private System.Windows.Forms.Button btnAddHead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gpbSeperator;
        private System.Windows.Forms.Button btnAddSep;
        private System.Windows.Forms.TextBox txtSeperators;
        private System.Windows.Forms.ComboBox cmbSeperators;
        private System.Windows.Forms.Button btnSepApply;
        private System.Windows.Forms.Button btnSepeDel;
        private System.Windows.Forms.ComboBox cmbLayout;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lswComponents;
        private System.Windows.Forms.GroupBox gpbLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox ckbLoadDefault;
        private System.Windows.Forms.ListView lswPath;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblLay;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.CheckBox ckbUnit;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnPathDelete;
        private System.Windows.Forms.ListBox lstStdHead;
    }
}