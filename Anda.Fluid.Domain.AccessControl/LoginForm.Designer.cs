namespace Anda.Fluid.Domain.AccessControl
{
    partial class LoginForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSetupPassword = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUserMgr = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.btnSwitchOperator = new System.Windows.Forms.Button();
            this.cboxNameId = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 62);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Password:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(98, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(137, 22);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnSetupPassword
            // 
            this.btnSetupPassword.Location = new System.Drawing.Point(98, 103);
            this.btnSetupPassword.Name = "btnSetupPassword";
            this.btnSetupPassword.Size = new System.Drawing.Size(137, 27);
            this.btnSetupPassword.TabIndex = 4;
            this.btnSetupPassword.Text = "Setup Password";
            this.btnSetupPassword.UseVisualStyleBackColor = true;
            this.btnSetupPassword.Click += new System.EventHandler(this.btnSetupPassword_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(22, 103);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(67, 27);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUserMgr
            // 
            this.btnUserMgr.Location = new System.Drawing.Point(22, 170);
            this.btnUserMgr.Name = "btnUserMgr";
            this.btnUserMgr.Size = new System.Drawing.Size(214, 27);
            this.btnUserMgr.TabIndex = 6;
            this.btnUserMgr.Text = "User Management";
            this.btnUserMgr.UseVisualStyleBackColor = true;
            this.btnUserMgr.Click += new System.EventHandler(this.btnUserMgr_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Location = new System.Drawing.Point(22, 204);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(214, 27);
            this.btnNewUser.TabIndex = 7;
            this.btnNewUser.Text = "New User";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnSwitchOperator
            // 
            this.btnSwitchOperator.Location = new System.Drawing.Point(22, 136);
            this.btnSwitchOperator.Name = "btnSwitchOperator";
            this.btnSwitchOperator.Size = new System.Drawing.Size(214, 27);
            this.btnSwitchOperator.TabIndex = 5;
            this.btnSwitchOperator.Text = "Switch Operator";
            this.btnSwitchOperator.UseVisualStyleBackColor = true;
            this.btnSwitchOperator.Click += new System.EventHandler(this.btnSwitchOperator_Click);
            // 
            // cboxNameId
            // 
            this.cboxNameId.FormattingEnabled = true;
            this.cboxNameId.Items.AddRange(new object[] {
            "Operator",
            "Technician",
            "Supervisor",
            "Developer"});
            this.cboxNameId.Location = new System.Drawing.Point(98, 20);
            this.cboxNameId.Name = "cboxNameId";
            this.cboxNameId.Size = new System.Drawing.Size(137, 22);
            this.cboxNameId.TabIndex = 12;
            this.cboxNameId.SelectedIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 246);
            this.Controls.Add(this.cboxNameId);
            this.Controls.Add(this.btnSwitchOperator);
            this.Controls.Add(this.btnNewUser);
            this.Controls.Add(this.btnUserMgr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnSetupPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSetupPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUserMgr;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.Button btnSwitchOperator;
        private System.Windows.Forms.ComboBox cboxNameId;
    }
}