namespace Anda.Fluid.App.EditCmdLineForms
{
    partial class EditBoardHeightForm
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
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbY = new Anda.Fluid.Controls.DoubleTextBox();
            this.tbX = new Anda.Fluid.Controls.DoubleTextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.heightControl1 = new Anda.Fluid.App.EditCmdLineForms.HeightControl();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.heightControl1);
            this.gbx2.Controls.Add(this.btnOk);
            this.gbx2.Controls.Add(this.lblPosition);
            this.gbx2.Controls.Add(this.btnCancel);
            this.gbx2.Controls.Add(this.tbX);
            this.gbx2.Controls.Add(this.btnGoTo);
            this.gbx2.Controls.Add(this.tbY);
            this.gbx2.Controls.Add(this.btnSelect);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(322, 19);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 6;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(241, 19);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "Teach";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(163, 20);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(72, 22);
            this.tbY.TabIndex = 4;
            this.tbY.Text = "0.000";
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(85, 20);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(72, 22);
            this.tbX.TabIndex = 5;
            this.tbX.Text = "0.000";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(14, 23);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(65, 14);
            this.lblPosition.TabIndex = 3;
            this.lblPosition.Text = "Position:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(419, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // heightControl1
            // 
            this.heightControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightControl1.Location = new System.Drawing.Point(17, 50);
            this.heightControl1.Name = "heightControl1";
            this.heightControl1.Size = new System.Drawing.Size(446, 136);
            this.heightControl1.TabIndex = 10;
            // 
            // EditBoardHeightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Name = "EditBoardHeightForm";
            this.Text = "EditBoardHeightForm";
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnSelect;
        private Controls.DoubleTextBox tbY;
        private Controls.DoubleTextBox tbX;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private HeightControl heightControl1;
    }
}