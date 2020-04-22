using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    public class TeachSafeZ:TeachFormBase, IClickable
    {
        private int flag = 0;

        private ICheckedChangable checkedChange= SVOForm.Instance as ICheckedChangable;

        #region Teach Safe Z Controls
        private TextBox txtZ;
        private Button btnGoto;
        private Label lblMessage;
        private PictureBox picDiagram;
        private Label lblValue;
        #endregion

        public TeachSafeZ()
        {          
            this.InitializeComponent();
            this.picDiagram.Image = Properties.Resources.SafeZ;
            UpdateByFlag();
        }

        public void DoNext()
        {
            Debug.WriteLine("do next");
        }

        public void DoPrev()
        {
            flag--;
            UpdateByFlag();
        }

        public void DoTeach()
        {
            //给设置文件中的SafeZ赋值
            double SafaZ = 199;
            //DataSetting.Default.SafeZ = SafaZ;
            //this.txtZ.Text = DataSetting.Default.SafeZ.ToString();

            flag++;
            UpdateByFlag();
        }
        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            //对设置文件进行保存
            //DataSetting.Default.Save();

            StepStateMgr.Instance.FindBy(0).IsDone = true;
            this.checkedChange.Task1Checked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void DoHelp()
        {
            throw new NotImplementedException();
        }
        private void btnGoto_Click(object sender, EventArgs e)
        {
            //移动到SafeZ
            //     =DataSetting.Default.SafeZ
        }      

        private void UpdateByFlag()
        {
            switch (this.flag)
            {
                case 0:
                    this.lblTitle.Text = "Confirm Safe Z height and press [Teach].";
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    break;
                case 1:
                    this.lblTitle.Text = "Teach compelet,Press [Done];Want again,Press [Prev].";
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    break;
            }
        }
        private void TeachSafeZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            //抬起到SafeZ
            //        = DataSetting.Default.SafeZ;
            //
            SVOForm.Instance.IsRunToEnd = false;
        }
        private void InitializeComponent()
        {
            this.txtZ = new System.Windows.Forms.TextBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResultTest
            // 
            this.grpResultTest.Controls.Add(this.lblMessage);
            this.grpResultTest.Controls.Add(this.txtZ);
            this.grpResultTest.Controls.Add(this.btnGoto);
            this.grpResultTest.Controls.Add(this.lblValue);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.picDiagram);
            // 
            // txtZ
            // 
            this.txtZ.Enabled = false;
            this.txtZ.Location = new System.Drawing.Point(135, 126);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(100, 21);
            this.txtZ.TabIndex = 24;
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(241, 124);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(75, 23);
            this.btnGoto.TabIndex = 22;
            this.btnGoto.Text = "Go to";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(98, 129);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(41, 12);
            this.lblValue.TabIndex = 23;
            this.lblValue.Text = "Value:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("宋体", 9F);
            this.lblMessage.Location = new System.Drawing.Point(37, 27);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(377, 60);
            this.lblMessage.TabIndex = 25;
            this.lblMessage.Text = "Move the dispense tip to a  height that is clear of obstacles \r\nwhen moving in al" +
    "lX-Y directions.\r\n\r\nIt is recommended tha SafeZ height be taught at the focal \r\n" +
    "length if possible.";
            // 
            // picDiagram
            // 
            this.picDiagram.Location = new System.Drawing.Point(0, 0);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = this.pnlDisplay.Size;
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 0;
            this.picDiagram.TabStop = false;
            // 
            // TeachSafeZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(664, 690);
            this.Name = "TeachSafeZ";
            this.Text = "Anda Fluidmove-Teach Safe Z ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachSafeZ_FormClosing);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
