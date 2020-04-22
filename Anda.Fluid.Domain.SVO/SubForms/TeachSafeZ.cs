using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
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
    internal class TeachSafeZ : TeachFormBase, IClickable
    {
        private int flag = 0;

        //private ICheckedChangable checkedChange= SVOForm.Instance as ICheckedChangable;

        #region Teach Safe Z Controls
        private TextBox txtZ;
        private Button btnGoto;
        private Label lblMessage;
        private PictureBox picDiagram;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Vision.CameraControl cameraControl1;
        private Label lblValue;

        private String strPage2 = "Camera Page";
        private String strPage1 = "Picture Page";
        #endregion

        #region 语言切换字符串变量

        private string[] lblTip = new string[2]
        {
            "Confirm Safe Z height and press [Teach].",
            "Teach compelet,Press [Done];Want again,Press [Prev]."
        };

        #endregion

        public TeachSafeZ()
        {
            this.InitializeComponent();
            this.picDiagram.Image = Properties.Resources.SafeZ;
            UpdateByFlag();
            this.ReadLanguageResources();
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            for (int i = 0; i < lblTip.Length; i++)
            {
                this.SaveKeyValueToResources(string.Format("Tip{0}", i), lblTip[i]);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            for (int i = 0; i < lblTip.Length; i++)
            {
                string temp = "";
                temp = this.ReadKeyValueFromResources(string.Format("Tip{0}", i));
                if (temp != "")
                {
                    this.lblTip[i] = temp;
                }
            }
            if (this.tabPage1 != null && this.tabPage2 != null)
            {
                this.tabPage1.Text = this.ReadKeyValueFromResources(this.strPage1);
                this.tabPage2.Text = this.ReadKeyValueFromResources(this.strPage2);
            }


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
            Machine.Instance.Robot.CalibPrm.SafeZ = Machine.Instance.Robot.PosZ;
            this.txtZ.Text = Machine.Instance.Robot.CalibPrm.SafeZ.ToString();

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
            if (DataSetting.Default.DoneStepCount <= 2)
            {
                DataSetting.Default.DoneStepCount = 2;
            }
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.CalibPrm.SavedItem = 2;
            Machine.Instance.Robot.SaveCalibPrm();
            DataSetting.Save();

            StepStateMgr.Instance.FindBy(1).IsDone = true;
            StepStateMgr.Instance.FindBy(1).IsChecked();
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                new TeachMeasureHeightAndMarkZ().ShowDialog();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            //参数修改记录
            //CompareObj.CompareProperty(Machine.Instance.Robot.CalibPrm, Machine.Instance.Robot.CalibPrmBackUp);
        }
        public void DoHelp()
        {

        }
        private void btnGoto_Click(object sender, EventArgs e)
        {
            //移动到SafeZ
            Machine.Instance.Robot.MoveSafeZ();
        }
        private void TeachSafeZ_Load(object sender, EventArgs e)
        {
            //抬起到最高位置
            Machine.Instance.Robot.MovePosZ(0);
            //
            this.txtZ.Text = Machine.Instance.Robot.CalibPrm.SafeZ.ToString();
        }
        private void UpdateByFlag()
        {
            switch (this.flag)
            {
                case 0:
                    this.lblTitle.Text = this.lblTip[0];
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    break;
                case 1:
                    this.lblTitle.Text = this.lblTip[1];
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = true;
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    break;
            }
        }
        private void TeachSafeZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            SVOForm.Instance.IsRunToEnd = false;
        }
        private void InitializeComponent()
        {
            this.txtZ = new System.Windows.Forms.TextBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cameraControl1 = new Anda.Fluid.Domain.Vision.CameraControl();
            this.grpOperation.SuspendLayout();
            this.grpResultTest.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.pnlDisplay.Controls.Add(this.tabControl1);
            // 
            // txtZ
            // 
            this.txtZ.Enabled = false;
            this.txtZ.Location = new System.Drawing.Point(114, 118);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(100, 21);
            this.txtZ.TabIndex = 24;
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(220, 116);
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
            this.lblValue.Location = new System.Drawing.Point(77, 121);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(41, 12);
            this.lblValue.TabIndex = 23;
            this.lblValue.Text = "Value:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("宋体", 9F);
            this.lblMessage.Location = new System.Drawing.Point(73, 16);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(377, 60);
            this.lblMessage.TabIndex = 25;
            this.lblMessage.Text = "Move the dispense tip to a  height that is clear of obstacles \r\nwhen moving in al" +
    "lX-Y directions.\r\n\r\nIt is recommended the SafeZ height be taught at the focal \r\n" +
    "length if possible.";
            // 
            // picDiagram
            // 
            this.picDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDiagram.Location = new System.Drawing.Point(3, 3);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = this.pnlDisplay.Size;
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 0;
            this.picDiagram.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(505, 393);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picDiagram);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(497, 367);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cameraControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(497, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraControl1.Location = new System.Drawing.Point(3, 3);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(491, 361);
            this.cameraControl1.TabIndex = 0;
            // 
            // TeachSafeZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(764, 657);
            this.Name = "TeachSafeZ";
            this.Text = "Anda Fluidmove-Teach Safe Z ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachSafeZ_FormClosing);
            this.Load += new System.EventHandler(this.TeachSafeZ_Load);
            this.grpOperation.ResumeLayout(false);
            this.grpResultTest.ResumeLayout(false);
            this.grpResultTest.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }

}
