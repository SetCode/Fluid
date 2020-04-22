namespace Anda.Fluid.Infrastructure.Alarming
{
    partial class AlarmForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnStopLightTower = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.BackColor = System.Drawing.Color.Yellow;
            this.richTextBox1.Font = new System.Drawing.Font("Calibri", 18F);
            this.richTextBox1.Location = new System.Drawing.Point(10, 10);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(660, 200);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnStopLightTower
            // 
            this.btnStopLightTower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopLightTower.Location = new System.Drawing.Point(570, 175);
            this.btnStopLightTower.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStopLightTower.Name = "btnStopLightTower";
            this.btnStopLightTower.Size = new System.Drawing.Size(100, 35);
            this.btnStopLightTower.TabIndex = 1;
            this.btnStopLightTower.Text = "STOP VOICE";
            this.btnStopLightTower.UseVisualStyleBackColor = true;
            this.btnStopLightTower.Click += new System.EventHandler(this.btnStopLightTower_Click);
            // 
            // AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 282);
            this.Controls.Add(this.btnStopLightTower);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AlarmForm";
            this.Text = "AlarmForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlarmForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnStopLightTower;
    }
}