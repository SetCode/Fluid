namespace Anda.Fluid.Domain.Conveyor.Forms
{
    partial class ConveyorSettingForm
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
            this.rdoConveyor1 = new System.Windows.Forms.RadioButton();
            this.rdoConveyor2 = new System.Windows.Forms.RadioButton();
            this.txtConnectCheck = new System.Windows.Forms.TextBox();
            this.btnConnectCheck = new System.Windows.Forms.Button();
            this.btnRTV = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdoConveyor1
            // 
            this.rdoConveyor1.AutoSize = true;
            this.rdoConveyor1.Location = new System.Drawing.Point(20, 13);
            this.rdoConveyor1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoConveyor1.Name = "rdoConveyor1";
            this.rdoConveyor1.Size = new System.Drawing.Size(111, 21);
            this.rdoConveyor1.TabIndex = 1;
            this.rdoConveyor1.TabStop = true;
            this.rdoConveyor1.Text = "Conveyor1";
            this.rdoConveyor1.UseVisualStyleBackColor = true;
            this.rdoConveyor1.CheckedChanged += new System.EventHandler(this.UpdateChecked);
            // 
            // rdoConveyor2
            // 
            this.rdoConveyor2.AutoSize = true;
            this.rdoConveyor2.Location = new System.Drawing.Point(155, 13);
            this.rdoConveyor2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdoConveyor2.Name = "rdoConveyor2";
            this.rdoConveyor2.Size = new System.Drawing.Size(111, 21);
            this.rdoConveyor2.TabIndex = 2;
            this.rdoConveyor2.TabStop = true;
            this.rdoConveyor2.Text = "Conveyor2";
            this.rdoConveyor2.UseVisualStyleBackColor = true;
            this.rdoConveyor2.CheckedChanged += new System.EventHandler(this.UpdateChecked);
            // 
            // txtConnectCheck
            // 
            this.txtConnectCheck.Location = new System.Drawing.Point(414, 11);
            this.txtConnectCheck.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtConnectCheck.Name = "txtConnectCheck";
            this.txtConnectCheck.ReadOnly = true;
            this.txtConnectCheck.Size = new System.Drawing.Size(104, 25);
            this.txtConnectCheck.TabIndex = 3;
            // 
            // btnConnectCheck
            // 
            this.btnConnectCheck.Location = new System.Drawing.Point(528, 6);
            this.btnConnectCheck.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnConnectCheck.Name = "btnConnectCheck";
            this.btnConnectCheck.Size = new System.Drawing.Size(153, 33);
            this.btnConnectCheck.TabIndex = 4;
            this.btnConnectCheck.Text = "Connect Check";
            this.btnConnectCheck.UseVisualStyleBackColor = true;
            this.btnConnectCheck.Click += new System.EventHandler(this.btnConnectCheck_Click);
            // 
            // btnRTV
            // 
            this.btnRTV.Location = new System.Drawing.Point(278, 6);
            this.btnRTV.Name = "btnRTV";
            this.btnRTV.Size = new System.Drawing.Size(98, 33);
            this.btnRTV.TabIndex = 5;
            this.btnRTV.Text = "轨道调宽";
            this.btnRTV.UseVisualStyleBackColor = true;
            this.btnRTV.Click += new System.EventHandler(this.btnRTV_Click);
            // 
            // ConveyorSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 662);
            this.Controls.Add(this.btnRTV);
            this.Controls.Add(this.btnConnectCheck);
            this.Controls.Add(this.txtConnectCheck);
            this.Controls.Add(this.rdoConveyor2);
            this.Controls.Add(this.rdoConveyor1);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ConveyorSettingForm";
            this.Text = "ConveyorSettingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rdoConveyor1;
        private System.Windows.Forms.RadioButton rdoConveyor2;
        private System.Windows.Forms.TextBox txtConnectCheck;
        private System.Windows.Forms.Button btnConnectCheck;
        private System.Windows.Forms.Button btnRTV;
    }
}