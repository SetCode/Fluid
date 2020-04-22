namespace Anda.Fluid.Domain.AccessControl
{
    partial class SetupPasswordForm
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
            this.lblRole = new System.Windows.Forms.Label();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblReEnterPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtReEnterPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(38, 22);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(97, 14);
            this.lblRole.TabIndex = 7;
            this.lblRole.Text = "Current User:";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldPassword.Location = new System.Drawing.Point(50, 63);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(104, 14);
            this.lblOldPassword.TabIndex = 11;
            this.lblOldPassword.Text = "Old Password:";
            this.lblOldPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPassword.Location = new System.Drawing.Point(45, 94);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(111, 14);
            this.lblNewPassword.TabIndex = 12;
            this.lblNewPassword.Text = "New Password:";
            this.lblNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReEnterPassword
            // 
            this.lblReEnterPassword.AutoSize = true;
            this.lblReEnterPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReEnterPassword.Location = new System.Drawing.Point(14, 126);
            this.lblReEnterPassword.Name = "lblReEnterPassword";
            this.lblReEnterPassword.Size = new System.Drawing.Size(139, 14);
            this.lblReEnterPassword.TabIndex = 13;
            this.lblReEnterPassword.Text = "Re-enter Password:";
            this.lblReEnterPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPassword.Location = new System.Drawing.Point(161, 59);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Size = new System.Drawing.Size(148, 22);
            this.txtOldPassword.TabIndex = 14;
            this.txtOldPassword.UseSystemPasswordChar = true;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.Location = new System.Drawing.Point(161, 91);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(148, 22);
            this.txtNewPassword.TabIndex = 15;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtReEnterPassword
            // 
            this.txtReEnterPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReEnterPassword.Location = new System.Drawing.Point(161, 122);
            this.txtReEnterPassword.Name = "txtReEnterPassword";
            this.txtReEnterPassword.Size = new System.Drawing.Size(148, 22);
            this.txtReEnterPassword.TabIndex = 16;
            this.txtReEnterPassword.UseSystemPasswordChar = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(50, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 27);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(182, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 27);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(168, 22);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 14);
            this.lblUserName.TabIndex = 19;
            // 
            // SetupPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 209);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtReEnterPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblReEnterPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.lblRole);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetupPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblReEnterPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtReEnterPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUserName;
    }
}