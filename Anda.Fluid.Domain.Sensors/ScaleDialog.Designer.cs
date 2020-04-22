namespace Anda.Fluid.Domain.Sensors
{
    partial class ScaleDialog
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
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.pnlPrm = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(350, 34);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(100, 27);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(347, 357);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(347, 390);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 27);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrate.Location = new System.Drawing.Point(347, 294);
            this.btnCalibrate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(100, 27);
            this.btnCalibrate.TabIndex = 4;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // btnZero
            // 
            this.btnZero.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZero.Location = new System.Drawing.Point(349, 96);
            this.btnZero.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(100, 27);
            this.btnZero.TabIndex = 5;
            this.btnZero.Text = "Zeroing";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // pnlPrm
            // 
            this.pnlPrm.Location = new System.Drawing.Point(3, 14);
            this.pnlPrm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlPrm.Name = "pnlPrm";
            this.pnlPrm.Size = new System.Drawing.Size(337, 406);
            this.pnlPrm.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(347, 158);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "ExtCalibrate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 220);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 27);
            this.button2.TabIndex = 8;
            this.button2.Text = "ReadValue";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ScaleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 429);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlPrm);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRestart);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ScaleDialog";
            this.Text = "ScaleDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScaleDialog_FormClosing);
            this.Load += new System.EventHandler(this.ScaleDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Panel pnlPrm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}