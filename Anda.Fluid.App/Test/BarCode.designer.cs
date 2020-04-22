namespace Anda.Fluid.App.Test
{
    partial class BarCode
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.lblContent = new System.Windows.Forms.Label();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(25, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 384);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(380, 381);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(418, 37);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(417, 151);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 2;
            this.btnDecode.Text = "解析";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(418, 100);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(75, 23);
            this.btnGen.TabIndex = 3;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(140, 408);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(41, 12);
            this.lblContent.TabIndex = 4;
            this.lblContent.Text = "label1";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(499, 12);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(374, 384);
            this.hWindowControl1.TabIndex = 5;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(374, 384);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 438);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label lblContent;
        private HalconDotNet.HWindowControl hWindowControl1;
    }
}

