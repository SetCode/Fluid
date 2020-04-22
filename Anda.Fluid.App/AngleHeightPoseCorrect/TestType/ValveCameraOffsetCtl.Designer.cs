namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    partial class ValveCameraOffsetCtl
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
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnRest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(6, 21);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(211, 104);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = "";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(142, 140);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnRest
            // 
            this.btnRest.Location = new System.Drawing.Point(6, 140);
            this.btnRest.Name = "btnRest";
            this.btnRest.Size = new System.Drawing.Size(75, 23);
            this.btnRest.TabIndex = 2;
            this.btnRest.Text = "重做";
            this.btnRest.UseVisualStyleBackColor = true;
            this.btnRest.Click += new System.EventHandler(this.btnRest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMsg);
            this.groupBox1.Controls.Add(this.btnRest);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 173);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第二阶段";
            // 
            // ValveCametaOffsetCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ValveCametaOffsetCtl";
            this.Size = new System.Drawing.Size(239, 180);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnRest;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
