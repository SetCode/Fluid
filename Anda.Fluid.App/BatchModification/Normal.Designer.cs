namespace Anda.Fluid.App.BatchModification
{
    partial class Normal
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
            this.lblWeight = new System.Windows.Forms.Label();
            this.cbIsWeightControl = new System.Windows.Forms.CheckBox();
            this.tbWeight = new Anda.Fluid.Controls.DoubleTextBox();
            this.rdoIncrementWeight = new System.Windows.Forms.RadioButton();
            this.rdoConstantWeight = new System.Windows.Forms.RadioButton();
            this.lblDotType = new System.Windows.Forms.Label();
            this.lblLineType = new System.Windows.Forms.Label();
            this.cbxLineType = new System.Windows.Forms.ComboBox();
            this.cbxDotType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblWeight
            // 
            this.lblWeight.Location = new System.Drawing.Point(24, 85);
            this.lblWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(93, 15);
            this.lblWeight.TabIndex = 19;
            this.lblWeight.Text = "Weight :";
            // 
            // cbIsWeightControl
            // 
            this.cbIsWeightControl.AutoSize = true;
            this.cbIsWeightControl.Location = new System.Drawing.Point(19, 10);
            this.cbIsWeightControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbIsWeightControl.Name = "cbIsWeightControl";
            this.cbIsWeightControl.Size = new System.Drawing.Size(125, 18);
            this.cbIsWeightControl.TabIndex = 17;
            this.cbIsWeightControl.Text = "Weight Control";
            this.cbIsWeightControl.ThreeState = true;
            this.cbIsWeightControl.UseVisualStyleBackColor = true;
            // 
            // tbWeight
            // 
            this.tbWeight.BackColor = System.Drawing.Color.White;
            this.tbWeight.Location = new System.Drawing.Point(125, 81);
            this.tbWeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(160, 22);
            this.tbWeight.TabIndex = 15;
            // 
            // rdoIncrementWeight
            // 
            this.rdoIncrementWeight.AutoSize = true;
            this.rdoIncrementWeight.Location = new System.Drawing.Point(179, 47);
            this.rdoIncrementWeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoIncrementWeight.Name = "rdoIncrementWeight";
            this.rdoIncrementWeight.Size = new System.Drawing.Size(91, 18);
            this.rdoIncrementWeight.TabIndex = 13;
            this.rdoIncrementWeight.TabStop = true;
            this.rdoIncrementWeight.Text = "increment";
            this.rdoIncrementWeight.UseVisualStyleBackColor = true;
            // 
            // rdoConstantWeight
            // 
            this.rdoConstantWeight.AutoSize = true;
            this.rdoConstantWeight.Location = new System.Drawing.Point(20, 47);
            this.rdoConstantWeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoConstantWeight.Name = "rdoConstantWeight";
            this.rdoConstantWeight.Size = new System.Drawing.Size(81, 18);
            this.rdoConstantWeight.TabIndex = 11;
            this.rdoConstantWeight.TabStop = true;
            this.rdoConstantWeight.Text = "constant";
            this.rdoConstantWeight.UseVisualStyleBackColor = true;
            // 
            // lblDotType
            // 
            this.lblDotType.Location = new System.Drawing.Point(24, 127);
            this.lblDotType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDotType.Name = "lblDotType";
            this.lblDotType.Size = new System.Drawing.Size(95, 20);
            this.lblDotType.TabIndex = 12;
            this.lblDotType.Text = "Dot Type :";
            // 
            // lblLineType
            // 
            this.lblLineType.Location = new System.Drawing.Point(23, 157);
            this.lblLineType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLineType.Name = "lblLineType";
            this.lblLineType.Size = new System.Drawing.Size(95, 20);
            this.lblLineType.TabIndex = 14;
            this.lblLineType.Text = "Line Type :";
            // 
            // cbxLineType
            // 
            this.cbxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLineType.FormattingEnabled = true;
            this.cbxLineType.Location = new System.Drawing.Point(125, 154);
            this.cbxLineType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxLineType.Name = "cbxLineType";
            this.cbxLineType.Size = new System.Drawing.Size(160, 22);
            this.cbxLineType.TabIndex = 18;
            // 
            // cbxDotType
            // 
            this.cbxDotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDotType.FormattingEnabled = true;
            this.cbxDotType.Location = new System.Drawing.Point(125, 123);
            this.cbxDotType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxDotType.Name = "cbxDotType";
            this.cbxDotType.Size = new System.Drawing.Size(160, 22);
            this.cbxDotType.TabIndex = 16;
            // 
            // Normal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.cbIsWeightControl);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.rdoIncrementWeight);
            this.Controls.Add(this.rdoConstantWeight);
            this.Controls.Add(this.lblDotType);
            this.Controls.Add(this.lblLineType);
            this.Controls.Add(this.cbxLineType);
            this.Controls.Add(this.cbxDotType);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Normal";
            this.Size = new System.Drawing.Size(297, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.CheckBox cbIsWeightControl;
        private Controls.DoubleTextBox tbWeight;
        private System.Windows.Forms.RadioButton rdoIncrementWeight;
        private System.Windows.Forms.RadioButton rdoConstantWeight;
        private System.Windows.Forms.Label lblDotType;
        private System.Windows.Forms.Label lblLineType;
        private System.Windows.Forms.ComboBox cbxLineType;
        private System.Windows.Forms.ComboBox cbxDotType;
    }
}
