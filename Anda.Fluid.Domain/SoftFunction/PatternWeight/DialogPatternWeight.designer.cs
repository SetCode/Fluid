namespace Anda.Fluid.Domain.SoftFunction.PatternWeight
{
    partial class DialogPatternWeight
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
            this.txtDotPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDotSave = new System.Windows.Forms.Button();
            this.fdgPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnWeight = new System.Windows.Forms.Button();
            this.cmbValves = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstPattern = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDotPath
            // 
            this.txtDotPath.Location = new System.Drawing.Point(134, 47);
            this.txtDotPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDotPath.Name = "txtDotPath";
            this.txtDotPath.Size = new System.Drawing.Size(261, 22);
            this.txtDotPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory:";
            // 
            // btnDotSave
            // 
            this.btnDotSave.Location = new System.Drawing.Point(427, 44);
            this.btnDotSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDotSave.Name = "btnDotSave";
            this.btnDotSave.Size = new System.Drawing.Size(65, 27);
            this.btnDotSave.TabIndex = 2;
            this.btnDotSave.Text = ". . .";
            this.btnDotSave.UseVisualStyleBackColor = true;
            this.btnDotSave.Click += new System.EventHandler(this.btnDotSave_Click);
            // 
            // btnWeight
            // 
            this.btnWeight.Location = new System.Drawing.Point(491, 63);
            this.btnWeight.Name = "btnWeight";
            this.btnWeight.Size = new System.Drawing.Size(75, 23);
            this.btnWeight.TabIndex = 6;
            this.btnWeight.Text = "Weight";
            this.btnWeight.UseVisualStyleBackColor = true;
            this.btnWeight.Click += new System.EventHandler(this.btnWeight_Click);
            // 
            // cmbValves
            // 
            this.cmbValves.FormattingEnabled = true;
            this.cmbValves.Location = new System.Drawing.Point(390, 63);
            this.cmbValves.Name = "cmbValves";
            this.cmbValves.Size = new System.Drawing.Size(95, 22);
            this.cmbValves.TabIndex = 8;
            this.cmbValves.SelectedIndexChanged += new System.EventHandler(this.cmbValves_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstPattern);
            this.groupBox2.Controls.Add(this.cmbValves);
            this.groupBox2.Controls.Add(this.btnWeight);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 246);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Weight";
            // 
            // lstPattern
            // 
            this.lstPattern.FormattingEnabled = true;
            this.lstPattern.ItemHeight = 14;
            this.lstPattern.Location = new System.Drawing.Point(17, 21);
            this.lstPattern.Name = "lstPattern";
            this.lstPattern.Size = new System.Drawing.Size(195, 214);
            this.lstPattern.TabIndex = 9;
            this.lstPattern.SelectedIndexChanged += new System.EventHandler(this.lstPattern_SelectedIndexChanged);
            // 
            // DialogPatternWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 376);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDotPath);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDotSave);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DialogPatternWeight";
            this.Text = "DialogMESGlue";
            this.Load += new System.EventHandler(this.DialogMESGlue_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDotPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDotSave;
        private System.Windows.Forms.FolderBrowserDialog fdgPath;
        private System.Windows.Forms.Button btnWeight;
        private System.Windows.Forms.ComboBox cmbValves;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstPattern;
    }
}