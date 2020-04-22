namespace Anda.Fluid.App
{
    partial class ConveyorWidthForm
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
            this.btnTeach = new System.Windows.Forms.Button();
            this.txtLiveLocation = new System.Windows.Forms.TextBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnAxisYDown = new System.Windows.Forms.Button();
            this.btnAxisYUp = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(273, 68);
            this.btnTeach.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(105, 37);
            this.btnTeach.TabIndex = 14;
            this.btnTeach.Text = "Teach\r\n\r\n";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // txtLiveLocation
            // 
            this.txtLiveLocation.Location = new System.Drawing.Point(120, 30);
            this.txtLiveLocation.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtLiveLocation.Name = "txtLiveLocation";
            this.txtLiveLocation.ReadOnly = true;
            this.txtLiveLocation.Size = new System.Drawing.Size(191, 25);
            this.txtLiveLocation.TabIndex = 12;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHome.Location = new System.Drawing.Point(120, 68);
            this.btnHome.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(105, 37);
            this.btnHome.TabIndex = 13;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnAxisYDown
            // 
            this.btnAxisYDown.Image = global::Anda.Fluid.App.Properties.Resources.Down;
            this.btnAxisYDown.Location = new System.Drawing.Point(23, 89);
            this.btnAxisYDown.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAxisYDown.Name = "btnAxisYDown";
            this.btnAxisYDown.Size = new System.Drawing.Size(70, 55);
            this.btnAxisYDown.TabIndex = 11;
            this.btnAxisYDown.UseVisualStyleBackColor = true;
            this.btnAxisYDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisYDown_MouseDown);
            this.btnAxisYDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxisYDown_MouseUp);
            // 
            // btnAxisYUp
            // 
            this.btnAxisYUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAxisYUp.Image = global::Anda.Fluid.App.Properties.Resources.Up;
            this.btnAxisYUp.Location = new System.Drawing.Point(23, 16);
            this.btnAxisYUp.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAxisYUp.Name = "btnAxisYUp";
            this.btnAxisYUp.Size = new System.Drawing.Size(70, 55);
            this.btnAxisYUp.TabIndex = 10;
            this.btnAxisYUp.UseVisualStyleBackColor = true;
            this.btnAxisYUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxisYUp_MouseDown);
            this.btnAxisYUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxisYUp_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "mm";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(273, 113);
            this.btnStop.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 35);
            this.btnStop.TabIndex = 16;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnGo
            // 
            this.btnGo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGo.Location = new System.Drawing.Point(120, 112);
            this.btnGo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(105, 35);
            this.btnGo.TabIndex = 17;
            this.btnGo.Text = "To Width";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ConveyorWidthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 156);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTeach);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.txtLiveLocation);
            this.Controls.Add(this.btnAxisYDown);
            this.Controls.Add(this.btnAxisYUp);
            this.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ConveyorWidthForm";
            this.Text = "ConveyorWidthForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.TextBox txtLiveLocation;
        private System.Windows.Forms.Button btnAxisYDown;
        private System.Windows.Forms.Button btnAxisYUp;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnGo;
    }
}