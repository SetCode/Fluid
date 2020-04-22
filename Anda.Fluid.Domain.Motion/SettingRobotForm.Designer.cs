namespace Anda.Fluid.Domain.Motion
{
    partial class SettingRobotForm
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.gbxContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxContent
            // 
            this.gbxContent.Controls.Add(this.propertyGrid1);
            this.gbxContent.Size = new System.Drawing.Size(524, 575);
            // 
            // tvwList
            // 
            this.tvwList.LineColor = System.Drawing.Color.Black;
            this.tvwList.Size = new System.Drawing.Size(230, 646);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 17);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(520, 556);
            this.propertyGrid1.TabIndex = 0;
            // 
            // SettingRobotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 654);
            this.Name = "SettingRobotForm";
            this.Text = "SettingRobotForm";
            this.gbxContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}