namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class AddMatrixForm
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
            this.components = new System.ComponentModel.Container();
            this.tbVNums = new Anda.Fluid.Controls.IntTextBox();
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
            this.listBoxPatterns = new System.Windows.Forms.ListBox();
            this.lblPatternList = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPatternOriginList = new System.Windows.Forms.Label();
            this.listBoxOrigins = new System.Windows.Forms.ListBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.cmsValveType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiValve1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiValve2 = new System.Windows.Forms.ToolStripMenuItem();
            this.gbx1.SuspendLayout();
            this.gbx2.SuspendLayout();
            this.cmsValveType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btnNext);
            this.gbx1.Controls.Add(this.btnLast);
            this.gbx1.Controls.Add(this.btnGoTo);
            this.gbx1.Controls.Add(this.lblPatternOriginList);
            this.gbx1.Controls.Add(this.listBoxOrigins);
            this.gbx1.Size = new System.Drawing.Size(270, 293);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.listBoxPatterns);
            this.gbx2.Controls.Add(this.tbVNums);
            this.gbx2.Controls.Add(this.lblPatternList);
            this.gbx2.Controls.Add(this.tbHNums);
            this.gbx2.Controls.Add(this.lblVNums);
            this.gbx2.Controls.Add(this.lblHNums);
            this.gbx2.Controls.Add(this.btnGoToVEnd);
            this.gbx2.Controls.Add(this.btnGoToHEnd);
            this.gbx2.Controls.Add(this.btnGoToOrigin);
            this.gbx2.Controls.Add(this.btnSelectVEnd);
            this.gbx2.Controls.Add(this.btnSelectHEnd);
            this.gbx2.Controls.Add(this.btnSelectOrigin);
            this.gbx2.Controls.Add(this.tbVEndY);
            this.gbx2.Controls.Add(this.tbHEndY);
            this.gbx2.Controls.Add(this.tbOriginY);
            this.gbx2.Controls.Add(this.tbVEndX);
            this.gbx2.Controls.Add(this.tbHEndX);
            this.gbx2.Controls.Add(this.tbOriginX);
            this.gbx2.Controls.Add(this.lblVEnd);
            this.gbx2.Controls.Add(this.lblHEnd);
            this.gbx2.Controls.Add(this.label1);
            // 
            // tbVNums
            // 
            this.tbVNums.BackColor = System.Drawing.Color.White;
            this.tbVNums.Location = new System.Drawing.Point(417, 15);
            this.tbVNums.Name = "tbVNums";
            this.tbVNums.Size = new System.Drawing.Size(75, 22);
            this.tbVNums.TabIndex = 25;
            this.tbVNums.Text = "3";
            this.tbVNums.TextChanged += new System.EventHandler(this.tbVNums_TextChanged);
            // 
            // tbHNums
            // 
            this.tbHNums.BackColor = System.Drawing.Color.White;
            this.tbHNums.Location = new System.Drawing.Point(236, 15);
            this.tbHNums.Name = "tbHNums";
            this.tbHNums.Size = new System.Drawing.Size(74, 22);
            this.tbHNums.TabIndex = 24;
            this.tbHNums.Text = "3";
            this.tbHNums.TextChanged += new System.EventHandler(this.tbHNums_TextChanged);
            // 
            // lblVNums
            // 
            this.lblVNums.AutoSize = true;
            this.lblVNums.Location = new System.Drawing.Point(314, 18);
            this.lblVNums.Name = "lblVNums";
            this.lblVNums.Size = new System.Drawing.Size(104, 14);
            this.lblVNums.TabIndex = 23;
            this.lblVNums.Text = "Vertical Nums:";
            // 
            // lblHNums
            // 
            this.lblHNums.AutoSize = true;
            this.lblHNums.Location = new System.Drawing.Point(118, 18);
            this.lblHNums.Name = "lblHNums";
            this.lblHNums.Size = new System.Drawing.Size(121, 14);
            this.lblHNums.TabIndex = 22;
            this.lblHNums.Text = "Horizontal Nums:";
            // 
            // btnGoToVEnd
            // 
            this.btnGoToVEnd.Location = new System.Drawing.Point(417, 151);
            this.btnGoToVEnd.Name = "btnGoToVEnd";
            this.btnGoToVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToVEnd.TabIndex = 20;
            this.btnGoToVEnd.Text = "Go To";
            this.btnGoToVEnd.UseVisualStyleBackColor = true;
            this.btnGoToVEnd.Click += new System.EventHandler(this.btnGoToVEnd_Click);
            // 
            // btnGoToHEnd
            // 
            this.btnGoToHEnd.Location = new System.Drawing.Point(417, 108);
            this.btnGoToHEnd.Name = "btnGoToHEnd";
            this.btnGoToHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnGoToHEnd.TabIndex = 19;
            this.btnGoToHEnd.Text = "Go To";
            this.btnGoToHEnd.UseVisualStyleBackColor = true;
            this.btnGoToHEnd.Click += new System.EventHandler(this.btnGoToHEnd_Click);
            // 
            // btnGoToOrigin
            // 
            this.btnGoToOrigin.Location = new System.Drawing.Point(417, 65);
            this.btnGoToOrigin.Name = "btnGoToOrigin";
            this.btnGoToOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnGoToOrigin.TabIndex = 18;
            this.btnGoToOrigin.Text = "Go To";
            this.btnGoToOrigin.UseVisualStyleBackColor = true;
            this.btnGoToOrigin.Click += new System.EventHandler(this.btnGoToOrigin_Click);
            // 
            // btnSelectVEnd
            // 
            this.btnSelectVEnd.Location = new System.Drawing.Point(336, 151);
            this.btnSelectVEnd.Name = "btnSelectVEnd";
            this.btnSelectVEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectVEnd.TabIndex = 17;
            this.btnSelectVEnd.Text = "Teach";
            this.btnSelectVEnd.UseVisualStyleBackColor = true;
            this.btnSelectVEnd.Click += new System.EventHandler(this.btnSelectVEnd_Click);
            // 
            // btnSelectHEnd
            // 
            this.btnSelectHEnd.Location = new System.Drawing.Point(336, 108);
            this.btnSelectHEnd.Name = "btnSelectHEnd";
            this.btnSelectHEnd.Size = new System.Drawing.Size(75, 23);
            this.btnSelectHEnd.TabIndex = 21;
            this.btnSelectHEnd.Text = "Teach";
            this.btnSelectHEnd.UseVisualStyleBackColor = true;
            this.btnSelectHEnd.Click += new System.EventHandler(this.btnSelectHEnd_Click);
            // 
            // btnSelectOrigin
            // 
            this.btnSelectOrigin.Location = new System.Drawing.Point(336, 65);
            this.btnSelectOrigin.Name = "btnSelectOrigin";
            this.btnSelectOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOrigin.TabIndex = 16;
            this.btnSelectOrigin.Text = "Teach";
            this.btnSelectOrigin.UseVisualStyleBackColor = true;
            this.btnSelectOrigin.Click += new System.EventHandler(this.btnSelectOrigin_Click);
            // 
            // tbVEndY
            // 
            this.tbVEndY.BackColor = System.Drawing.Color.White;
            this.tbVEndY.Location = new System.Drawing.Point(256, 151);
            this.tbVEndY.Name = "tbVEndY";
            this.tbVEndY.Size = new System.Drawing.Size(74, 22);
            this.tbVEndY.TabIndex = 15;
            this.tbVEndY.Text = "0.000";
            this.tbVEndY.TextChanged += new System.EventHandler(this.tbVEndY_TextChanged);
            // 
            // tbHEndY
            // 
            this.tbHEndY.BackColor = System.Drawing.Color.White;
            this.tbHEndY.Location = new System.Drawing.Point(256, 109);
            this.tbHEndY.Name = "tbHEndY";
            this.tbHEndY.Size = new System.Drawing.Size(74, 22);
            this.tbHEndY.TabIndex = 14;
            this.tbHEndY.Text = "0.000";
            this.tbHEndY.TextChanged += new System.EventHandler(this.tbHEndY_TextChanged);
            // 
            // tbOriginY
            // 
            this.tbOriginY.BackColor = System.Drawing.Color.White;
            this.tbOriginY.Location = new System.Drawing.Point(256, 67);
            this.tbOriginY.Name = "tbOriginY";
            this.tbOriginY.Size = new System.Drawing.Size(74, 22);
            this.tbOriginY.TabIndex = 13;
            this.tbOriginY.Text = "0.000";
            this.tbOriginY.TextChanged += new System.EventHandler(this.tbOriginY_TextChanged);
            // 
            // tbVEndX
            // 
            this.tbVEndX.BackColor = System.Drawing.Color.White;
            this.tbVEndX.Location = new System.Drawing.Point(176, 151);
            this.tbVEndX.Name = "tbVEndX";
            this.tbVEndX.Size = new System.Drawing.Size(74, 22);
            this.tbVEndX.TabIndex = 12;
            this.tbVEndX.Text = "0.000";
            this.tbVEndX.TextChanged += new System.EventHandler(this.tbVEndX_TextChanged);
            // 
            // tbHEndX
            // 
            this.tbHEndX.BackColor = System.Drawing.Color.White;
            this.tbHEndX.Location = new System.Drawing.Point(176, 109);
            this.tbHEndX.Name = "tbHEndX";
            this.tbHEndX.Size = new System.Drawing.Size(74, 22);
            this.tbHEndX.TabIndex = 11;
            this.tbHEndX.Text = "0.000";
            this.tbHEndX.TextChanged += new System.EventHandler(this.tbHEndX_TextChanged);
            // 
            // tbOriginX
            // 
            this.tbOriginX.BackColor = System.Drawing.Color.White;
            this.tbOriginX.Location = new System.Drawing.Point(176, 67);
            this.tbOriginX.Name = "tbOriginX";
            this.tbOriginX.Size = new System.Drawing.Size(74, 22);
            this.tbOriginX.TabIndex = 10;
            this.tbOriginX.Text = "0.000";
            this.tbOriginX.TextChanged += new System.EventHandler(this.tbOriginX_TextChanged);
            // 
            // lblVEnd
            // 
            this.lblVEnd.AutoSize = true;
            this.lblVEnd.Location = new System.Drawing.Point(173, 134);
            this.lblVEnd.Name = "lblVEnd";
            this.lblVEnd.Size = new System.Drawing.Size(91, 14);
            this.lblVEnd.TabIndex = 8;
            this.lblVEnd.Text = "Vertical End:";
            // 
            // lblHEnd
            // 
            this.lblHEnd.AutoSize = true;
            this.lblHEnd.Location = new System.Drawing.Point(173, 92);
            this.lblHEnd.Name = "lblHEnd";
            this.lblHEnd.Size = new System.Drawing.Size(108, 14);
            this.lblHEnd.TabIndex = 9;
            this.lblHEnd.Text = "Horizontal End:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Origin:";
            // 
            // listBoxPatterns
            // 
            this.listBoxPatterns.FormattingEnabled = true;
            this.listBoxPatterns.ItemHeight = 14;
            this.listBoxPatterns.Location = new System.Drawing.Point(6, 35);
            this.listBoxPatterns.Name = "listBoxPatterns";
            this.listBoxPatterns.Size = new System.Drawing.Size(161, 214);
            this.listBoxPatterns.TabIndex = 11;
            // 
            // lblPatternList
            // 
            this.lblPatternList.AutoSize = true;
            this.lblPatternList.Location = new System.Drawing.Point(9, 18);
            this.lblPatternList.Name = "lblPatternList";
            this.lblPatternList.Size = new System.Drawing.Size(89, 14);
            this.lblPatternList.TabIndex = 10;
            this.lblPatternList.Text = "Pattern List:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(417, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 26;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(336, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPatternOriginList
            // 
            this.lblPatternOriginList.AutoSize = true;
            this.lblPatternOriginList.Location = new System.Drawing.Point(7, 18);
            this.lblPatternOriginList.Name = "lblPatternOriginList";
            this.lblPatternOriginList.Size = new System.Drawing.Size(134, 14);
            this.lblPatternOriginList.TabIndex = 6;
            this.lblPatternOriginList.Text = "Pattern Origin List:";
            // 
            // listBoxOrigins
            // 
            this.listBoxOrigins.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxOrigins.FormattingEnabled = true;
            this.listBoxOrigins.ItemHeight = 14;
            this.listBoxOrigins.Location = new System.Drawing.Point(4, 62);
            this.listBoxOrigins.Name = "listBoxOrigins";
            this.listBoxOrigins.Size = new System.Drawing.Size(262, 228);
            this.listBoxOrigins.TabIndex = 5;
            this.listBoxOrigins.SelectedIndexChanged += new System.EventHandler(this.listBoxOrigins_SelectedIndexChanged);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(187, 37);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 7;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(99, 37);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 16;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(11, 37);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 15;
            this.btnLast.Text = "Last";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
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
            // AddMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "AddMatrixForm";
            this.Text = "AddMatrixForm1";
            this.Load += new System.EventHandler(this.AddMatrixForm_Load);
            this.Controls.SetChildIndex(this.gbx1, 0);
            this.Controls.SetChildIndex(this.gbx2, 0);
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.cmsValveType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.IntTextBox tbVNums;
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
        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.Label lblPatternList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPatternOriginList;
        private System.Windows.Forms.ListBox listBoxOrigins;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.ContextMenuStrip cmsValveType;
        private System.Windows.Forms.ToolStripMenuItem tsiValve1;
        private System.Windows.Forms.ToolStripMenuItem tsiValve2;
    }
}