namespace Anda.Fluid.App.LoadTrajectory
{
    partial class ImportEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLayout = new System.Windows.Forms.ComboBox();
            this.cmbHead = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnAddHead = new System.Windows.Forms.Button();
            this.gpbSeperator = new System.Windows.Forms.GroupBox();
            this.btnSepeDel = new System.Windows.Forms.Button();
            this.btnSepApply = new System.Windows.Forms.Button();
            this.btnAddSep = new System.Windows.Forms.Button();
            this.txtSeperators = new System.Windows.Forms.TextBox();
            this.cmbSeperators = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.gpbSeperator.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstHead
            // 
            this.lstHead.FormattingEnabled = true;
            this.lstHead.ItemHeight = 14;
            this.lstHead.Location = new System.Drawing.Point(7, 21);
            this.lstHead.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstHead.Name = "lstHead";
            this.lstHead.Size = new System.Drawing.Size(116, 144);
            this.lstHead.TabIndex = 0;
            this.lstHead.SelectedIndexChanged += new System.EventHandler(this.lstHead_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(295, 359);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(425, 359);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstHeadSelected
            // 
            this.lstHeadSelected.FormattingEnabled = true;
            this.lstHeadSelected.ItemHeight = 14;
            this.lstHeadSelected.Location = new System.Drawing.Point(211, 21);
            this.lstHeadSelected.Name = "lstHeadSelected";
            this.lstHeadSelected.Size = new System.Drawing.Size(187, 144);
            this.lstHeadSelected.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbLayout);
            this.groupBox1.Controls.Add(this.cmbHead);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.btnAddHead);
            this.groupBox1.Controls.Add(this.lstHead);
            this.groupBox1.Controls.Add(this.lstHeadSelected);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 182);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HeadSelect";
            // 
            // cmbLayout
            // 
            this.cmbLayout.FormattingEnabled = true;
            this.cmbLayout.Location = new System.Drawing.Point(413, 143);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(75, 22);
            this.cmbLayout.TabIndex = 8;
            this.cmbLayout.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cmbHead
            // 
            this.cmbHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHead.FormattingEnabled = true;
            this.cmbHead.Location = new System.Drawing.Point(130, 51);
            this.cmbHead.Name = "cmbHead";
            this.cmbHead.Size = new System.Drawing.Size(75, 22);
            this.cmbHead.TabIndex = 7;
            this.cmbHead.SelectedIndexChanged += new System.EventHandler(this.cmbHead_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(413, 39);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(413, 106);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnAddHead
            // 
            this.btnAddHead.Location = new System.Drawing.Point(130, 106);
            this.btnAddHead.Name = "btnAddHead";
            this.btnAddHead.Size = new System.Drawing.Size(75, 23);
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
            this.gpbSeperator.Location = new System.Drawing.Point(12, 12);
            this.gpbSeperator.Name = "gpbSeperator";
            this.gpbSeperator.Size = new System.Drawing.Size(513, 88);
            this.gpbSeperator.TabIndex = 5;
            this.gpbSeperator.TabStop = false;
            this.gpbSeperator.Text = "Seperator";
            // 
            // btnSepeDel
            // 
            this.btnSepeDel.Location = new System.Drawing.Point(422, 21);
            this.btnSepeDel.Name = "btnSepeDel";
            this.btnSepeDel.Size = new System.Drawing.Size(75, 23);
            this.btnSepeDel.TabIndex = 8;
            this.btnSepeDel.Text = "Delete";
            this.btnSepeDel.UseVisualStyleBackColor = true;
            this.btnSepeDel.Click += new System.EventHandler(this.btnSepeDel_Click);
            // 
            // btnSepApply
            // 
            this.btnSepApply.Location = new System.Drawing.Point(422, 59);
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
            this.btnAddSep.Size = new System.Drawing.Size(57, 23);
            this.btnAddSep.TabIndex = 2;
            this.btnAddSep.Text = ">>";
            this.btnAddSep.UseVisualStyleBackColor = true;
            this.btnAddSep.Click += new System.EventHandler(this.btnAddSep_Click);
            // 
            // txtSeperators
            // 
            this.txtSeperators.Location = new System.Drawing.Point(193, 21);
            this.txtSeperators.Name = "txtSeperators";
            this.txtSeperators.ReadOnly = true;
            this.txtSeperators.Size = new System.Drawing.Size(205, 22);
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
            // ImportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 394);
            this.Controls.Add(this.gpbSeperator);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ImportEdit";
            this.Text = "ImportEdit";
            this.Load += new System.EventHandler(this.ImportEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.gpbSeperator.ResumeLayout(false);
            this.gpbSeperator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstHead;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstHeadSelected;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddHead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cmbHead;
        private System.Windows.Forms.GroupBox gpbSeperator;
        private System.Windows.Forms.Button btnAddSep;
        private System.Windows.Forms.TextBox txtSeperators;
        private System.Windows.Forms.ComboBox cmbSeperators;
        private System.Windows.Forms.Button btnSepApply;
        private System.Windows.Forms.Button btnSepeDel;
        private System.Windows.Forms.ComboBox cmbLayout;
    }
}