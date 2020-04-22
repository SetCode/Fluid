namespace Anda.Fluid.Domain.AccessControl
{
    partial class NewUserForm
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
            this.rdoOperator = new System.Windows.Forms.RadioButton();
            this.rdoTechnician = new System.Windows.Forms.RadioButton();
            this.rdoSupervisor = new System.Windows.Forms.RadioButton();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblReenterPassword = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRePassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rdoOperator
            // 
            this.rdoOperator.AutoSize = true;
            this.rdoOperator.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoOperator.Location = new System.Drawing.Point(69, 14);
            this.rdoOperator.Name = "rdoOperator";
            this.rdoOperator.Size = new System.Drawing.Size(85, 18);
            this.rdoOperator.TabIndex = 0;
            this.rdoOperator.TabStop = true;
            this.rdoOperator.Text = "Operator";
            this.rdoOperator.UseVisualStyleBackColor = true;
            this.rdoOperator.CheckedChanged += new System.EventHandler(this.RdoOperator_CheckedChanged);
            // 
            // rdoTechnician
            // 
            this.rdoTechnician.AutoSize = true;
            this.rdoTechnician.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoTechnician.Location = new System.Drawing.Point(157, 14);
            this.rdoTechnician.Name = "rdoTechnician";
            this.rdoTechnician.Size = new System.Drawing.Size(95, 18);
            this.rdoTechnician.TabIndex = 1;
            this.rdoTechnician.TabStop = true;
            this.rdoTechnician.Text = "Technician";
            this.rdoTechnician.UseVisualStyleBackColor = true;
            // 
            // rdoSupervisor
            // 
            this.rdoSupervisor.AutoSize = true;
            this.rdoSupervisor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSupervisor.Location = new System.Drawing.Point(258, 14);
            this.rdoSupervisor.Name = "rdoSupervisor";
            this.rdoSupervisor.Size = new System.Drawing.Size(97, 18);
            this.rdoSupervisor.TabIndex = 2;
            this.rdoSupervisor.TabStop = true;
            this.rdoSupervisor.Text = "Supervisor";
            this.rdoSupervisor.UseVisualStyleBackColor = true;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(5, 58);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(138, 14);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "User name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(5, 87);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(138, 14);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReenterPassword
            // 
            this.lblReenterPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReenterPassword.Location = new System.Drawing.Point(5, 116);
            this.lblReenterPassword.Name = "lblReenterPassword";
            this.lblReenterPassword.Size = new System.Drawing.Size(138, 14);
            this.lblReenterPassword.TabIndex = 5;
            this.lblReenterPassword.Text = "Re-enter password:";
            this.lblReenterPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(75, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 27);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(209, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 27);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(17, 16);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(41, 14);
            this.lblRole.TabIndex = 8;
            this.lblRole.Text = "Role:";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(149, 55);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(201, 22);
            this.txtUserName.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(149, 84);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(201, 22);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtRePassword
            // 
            this.txtRePassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRePassword.Location = new System.Drawing.Point(149, 113);
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.Size = new System.Drawing.Size(201, 22);
            this.txtRePassword.TabIndex = 11;
            this.txtRePassword.UseSystemPasswordChar = true;
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 211);
            this.Controls.Add(this.txtRePassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblReenterPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.rdoSupervisor);
            this.Controls.Add(this.rdoTechnician);
            this.Controls.Add(this.rdoOperator);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NewUserForm";
            this.Text = "NewUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoOperator;
        private System.Windows.Forms.RadioButton rdoTechnician;
        private System.Windows.Forms.RadioButton rdoSupervisor;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblReenterPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRePassword;
    }
}