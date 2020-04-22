namespace Anda.Fluid.Drive.Conveyor.LeadShine.Forms.MotionForms
{
    partial class MotionCtl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.grpAxisY = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.txtLiveLocation = new System.Windows.Forms.TextBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnAxisYDown = new System.Windows.Forms.Button();
            this.btnAxisYUp = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.grpAxisY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAxisY
            // 
            this.grpAxisY.Controls.Add(this.btnTeach);
            this.grpAxisY.Controls.Add(this.btnHome);
            this.grpAxisY.Controls.Add(this.numericUpDown1);
            this.grpAxisY.Controls.Add(this.txtLiveLocation);
            this.grpAxisY.Controls.Add(this.btnGoTo);
            this.grpAxisY.Controls.Add(this.btnAxisYDown);
            this.grpAxisY.Controls.Add(this.btnAxisYUp);
            this.grpAxisY.Location = new System.Drawing.Point(3, 3);
            this.grpAxisY.Name = "grpAxisY";
            this.grpAxisY.Size = new System.Drawing.Size(255, 112);
            this.grpAxisY.TabIndex = 2;
            this.grpAxisY.TabStop = false;
            this.grpAxisY.Text = "AxisY";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(64, 74);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(76, 21);
            this.numericUpDown1.TabIndex = 7;
            // 
            // txtLiveLocation
            // 
            this.txtLiveLocation.Location = new System.Drawing.Point(64, 28);
            this.txtLiveLocation.Name = "txtLiveLocation";
            this.txtLiveLocation.ReadOnly = true;
            this.txtLiveLocation.Size = new System.Drawing.Size(76, 21);
            this.txtLiveLocation.TabIndex = 5;
            // 
            // btnGoTo
            // 
            this.btnGoTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGoTo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnGoTo.Image = global::Anda.Fluid.Drive.Conveyor.Properties.Resources.Go;
            this.btnGoTo.Location = new System.Drawing.Point(146, 65);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(48, 35);
            this.btnGoTo.TabIndex = 4;
            this.btnGoTo.UseVisualStyleBackColor = true;
            // 
            // btnAxisYDown
            // 
            this.btnAxisYDown.Image = global::Anda.Fluid.Drive.Conveyor.Properties.Resources.Down;
            this.btnAxisYDown.Location = new System.Drawing.Point(6, 63);
            this.btnAxisYDown.Name = "btnAxisYDown";
            this.btnAxisYDown.Size = new System.Drawing.Size(42, 39);
            this.btnAxisYDown.TabIndex = 3;
            this.btnAxisYDown.UseVisualStyleBackColor = true;
            // 
            // btnAxisYUp
            // 
            this.btnAxisYUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAxisYUp.Image = global::Anda.Fluid.Drive.Conveyor.Properties.Resources.Up;
            this.btnAxisYUp.Location = new System.Drawing.Point(6, 18);
            this.btnAxisYUp.Name = "btnAxisYUp";
            this.btnAxisYUp.Size = new System.Drawing.Size(42, 39);
            this.btnAxisYUp.TabIndex = 2;
            this.btnAxisYUp.UseVisualStyleBackColor = true;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImage = global::Anda.Fluid.Drive.Conveyor.Properties.Resources.Return;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHome.Location = new System.Drawing.Point(146, 18);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(48, 36);
            this.btnHome.TabIndex = 8;
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(200, 35);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(49, 47);
            this.btnTeach.TabIndex = 9;
            this.btnTeach.Text = "Teach\r\n\r\nWidth";
            this.btnTeach.UseVisualStyleBackColor = true;
            // 
            // MotionCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAxisY);
            this.Name = "MotionCtl";
            this.Size = new System.Drawing.Size(264, 121);
            this.grpAxisY.ResumeLayout(false);
            this.grpAxisY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpAxisY;
        private System.Windows.Forms.Button btnAxisYDown;
        private System.Windows.Forms.Button btnAxisYUp;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox txtLiveLocation;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnTeach;
    }
}
