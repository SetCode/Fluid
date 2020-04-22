namespace Anda.Fluid.Domain.SVO.SubForms
{
    partial class FindCircle
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
            this.grpSwitch = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.rdoFindCircleThreePoint = new System.Windows.Forms.RadioButton();
            this.rdoFindCircleOnePoint = new System.Windows.Forms.RadioButton();
            this.grpSwitch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSwitch
            // 
            this.grpSwitch.Controls.Add(this.lblMessage);
            this.grpSwitch.Controls.Add(this.rdoFindCircleThreePoint);
            this.grpSwitch.Controls.Add(this.rdoFindCircleOnePoint);
            this.grpSwitch.Location = new System.Drawing.Point(3, 3);
            this.grpSwitch.Name = "grpSwitch";
            this.grpSwitch.Size = new System.Drawing.Size(444, 80);
            this.grpSwitch.TabIndex = 0;
            this.grpSwitch.TabStop = false;
            this.grpSwitch.Text = "Teach Center Method";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(19, 17);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(41, 12);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "label1";
            this.lblMessage.Visible = false;
            // 
            // rdoFindCircleThreePoint
            // 
            this.rdoFindCircleThreePoint.AutoSize = true;
            this.rdoFindCircleThreePoint.Location = new System.Drawing.Point(217, 38);
            this.rdoFindCircleThreePoint.Name = "rdoFindCircleThreePoint";
            this.rdoFindCircleThreePoint.Size = new System.Drawing.Size(191, 16);
            this.rdoFindCircleThreePoint.TabIndex = 1;
            this.rdoFindCircleThreePoint.Text = "Teach 3 Circumference Points";
            this.rdoFindCircleThreePoint.UseVisualStyleBackColor = true;
            // 
            // rdoFindCircleOnePoint
            // 
            this.rdoFindCircleOnePoint.AutoSize = true;
            this.rdoFindCircleOnePoint.Checked = true;
            this.rdoFindCircleOnePoint.Location = new System.Drawing.Point(42, 38);
            this.rdoFindCircleOnePoint.Name = "rdoFindCircleOnePoint";
            this.rdoFindCircleOnePoint.Size = new System.Drawing.Size(143, 16);
            this.rdoFindCircleOnePoint.TabIndex = 0;
            this.rdoFindCircleOnePoint.TabStop = true;
            this.rdoFindCircleOnePoint.Text = "Teach 1 Center Point";
            this.rdoFindCircleOnePoint.UseVisualStyleBackColor = true;
            // 
            // FindCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSwitch);
            this.Name = "FindCircle";
            this.Size = new System.Drawing.Size(450, 90);
            this.grpSwitch.ResumeLayout(false);
            this.grpSwitch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.RadioButton rdoFindCircleThreePoint;
        internal System.Windows.Forms.RadioButton rdoFindCircleOnePoint;
        public System.Windows.Forms.GroupBox grpSwitch;
    }
}
