using Anda.Fluid.Domain.Vision;

namespace Anda.Fluid.Domain.Dialogs
{
    partial class SetupVisionForm
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
            this.cbxVendor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.cbxReverseX = new System.Windows.Forms.CheckBox();
            this.cbxReverseY = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbxVendor
            // 
            this.cbxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendor.FormattingEnabled = true;
            this.cbxVendor.Location = new System.Drawing.Point(145, 468);
            this.cbxVendor.Margin = new System.Windows.Forms.Padding(2);
            this.cbxVendor.Name = "cbxVendor";
            this.cbxVendor.Size = new System.Drawing.Size(122, 25);
            this.cbxVendor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 470);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "CameraVendor:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(507, 508);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(428, 508);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(12, 12);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(569, 441);
            this.cameraControl1.TabIndex = 4;
            // 
            // cbxReverseX
            // 
            this.cbxReverseX.AutoSize = true;
            this.cbxReverseX.Location = new System.Drawing.Point(309, 472);
            this.cbxReverseX.Name = "cbxReverseX";
            this.cbxReverseX.Size = new System.Drawing.Size(100, 21);
            this.cbxReverseX.TabIndex = 5;
            this.cbxReverseX.Text = "ReverseX";
            this.cbxReverseX.UseVisualStyleBackColor = true;
            this.cbxReverseX.CheckedChanged += new System.EventHandler(this.cbxReverseX_CheckedChanged);
            // 
            // cbxReverseY
            // 
            this.cbxReverseY.AutoSize = true;
            this.cbxReverseY.Location = new System.Drawing.Point(309, 499);
            this.cbxReverseY.Name = "cbxReverseY";
            this.cbxReverseY.Size = new System.Drawing.Size(99, 21);
            this.cbxReverseY.TabIndex = 6;
            this.cbxReverseY.Text = "ReverseY";
            this.cbxReverseY.UseVisualStyleBackColor = true;
            this.cbxReverseY.CheckedChanged += new System.EventHandler(this.cbxReverseY_CheckedChanged);
            // 
            // SetupVisionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 544);
            this.Controls.Add(this.cbxReverseY);
            this.Controls.Add(this.cbxReverseX);
            this.Controls.Add(this.cameraControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxVendor);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "SetupVisionForm";
            this.Text = "SetupVisionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxVendor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private CameraControl cameraControl1;
        private System.Windows.Forms.CheckBox cbxReverseX;
        private System.Windows.Forms.CheckBox cbxReverseY;
    }
}