namespace Anda.Fluid.App.LoadTrajectory
{
    partial class ComponentEditForm
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
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstUserComps = new System.Windows.Forms.ListBox();
            this.lstStandComps = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(163, 70);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(160, 22);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(160, 140);
            this.cmbModel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(160, 22);
            this.cmbModel.TabIndex = 1;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 298);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(436, 298);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(76, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "UserComponents";
            // 
            // lstUserComps
            // 
            this.lstUserComps.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstUserComps.FormattingEnabled = true;
            this.lstUserComps.HorizontalScrollbar = true;
            this.lstUserComps.Location = new System.Drawing.Point(18, 48);
            this.lstUserComps.Name = "lstUserComps";
            this.lstUserComps.Size = new System.Drawing.Size(120, 238);
            this.lstUserComps.TabIndex = 8;
            this.lstUserComps.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstUserComps_DrawItem);
            this.lstUserComps.SelectedIndexChanged += new System.EventHandler(this.lstUserComps_SelectedIndexChanged);
            // 
            // lstStandComps
            // 
            this.lstStandComps.FormattingEnabled = true;
            this.lstStandComps.HorizontalScrollbar = true;
            this.lstStandComps.ItemHeight = 14;
            this.lstStandComps.Location = new System.Drawing.Point(339, 48);
            this.lstStandComps.Name = "lstStandComps";
            this.lstStandComps.Size = new System.Drawing.Size(174, 242);
            this.lstStandComps.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "StandComponents";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(160, 198);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 198);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Location = new System.Drawing.Point(160, 245);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(75, 23);
            this.btnLoadMap.TabIndex = 13;
            this.btnLoadMap.Text = "Load";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(258, 245);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ComponentEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 328);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoadMap);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstStandComps);
            this.Controls.Add(this.lstUserComps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.cmbType);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ComponentEditForm";
            this.Text = "ComponentEditForm";
            this.Load += new System.EventHandler(this.ComponentEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstUserComps;
        private System.Windows.Forms.ListBox lstStandComps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.Button btnSave;
    }
}