namespace Anda.Fluid.Drive.Conveyor.LeadShine.Forms.IOForms
{
    partial class IOForm
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
            this.grpDi = new System.Windows.Forms.GroupBox();
            this.grpDo = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // grpDi
            // 
            this.grpDi.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpDi.Location = new System.Drawing.Point(3, 2);
            this.grpDi.Name = "grpDi";
            this.grpDi.Size = new System.Drawing.Size(580, 697);
            this.grpDi.TabIndex = 0;
            this.grpDi.TabStop = false;
            this.grpDi.Text = "DI";
            // 
            // grpDo
            // 
            this.grpDo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpDo.Location = new System.Drawing.Point(589, 2);
            this.grpDo.Name = "grpDo";
            this.grpDo.Size = new System.Drawing.Size(580, 697);
            this.grpDo.TabIndex = 1;
            this.grpDo.TabStop = false;
            this.grpDo.Text = "DO";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // IOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 700);
            this.Controls.Add(this.grpDo);
            this.Controls.Add(this.grpDi);
            this.Name = "IOForm";
            this.Text = "IOForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDi;
        private System.Windows.Forms.GroupBox grpDo;
        private System.Windows.Forms.Timer timer1;
    }
}