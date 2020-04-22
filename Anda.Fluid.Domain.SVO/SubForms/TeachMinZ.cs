using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    public partial class TeachMinZ : JogFormBase
    {
        
        #region 语言切换字符串变量

        private string lblTip = "必须小于-5";

        #endregion
        private double minZ;
        public TeachMinZ()
        {
            InitializeComponent();

            Machine.Instance.Robot.AxisZ.SetLimit(200, -200);

            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.txtMinZ.ReadOnly = true;
            this.btnTeach.Click += BtnTeach_Click;

            if (Machine.Instance.Robot != null)
            {
                this.ShowMinZ();
            }
            this.ReadLanguageResources();
            this.minZ = Machine.Instance.Robot.DefaultPrm.MinZ;
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources("Tip1", lblTip);
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            string temp = this.ReadKeyValueFromResources("Tip1");
            if (temp != "")
            {
                this.lblTip = temp;
            }
        }

        private void BtnTeach_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Robot.PosZ >= -5)
            {
                MessageBox.Show(this.lblTip);
                return;
            }
            this.txtMinZ.Text = Machine.Instance.Robot.PosZ.ToString();
        }

        private void ShowMinZ()
        {
            this.txtMinZ.Text = Machine.Instance.Robot.DefaultPrm.MinZ.ToString();
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MovePosZ(Convert.ToDouble(this.txtMinZ.Text));
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.DefaultPrm.MinZ = Machine.Instance.Robot.PosZ;
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.SaveDefaultPrm();

            if (DataSetting.Default.DoneStepCount <= 1)
            {
                DataSetting.Default.DoneStepCount = 1;
            }
            DataSetting.Save();

            StepStateMgr.Instance.FindBy(0).IsDone = true;
            StepStateMgr.Instance.FindBy(0).IsChecked();

            //Machine.Instance.Robot.AxisZ.SetLimit(200, Machine.Instance.Robot.DefaultPrm.MinZ);

            this.DialogResult = DialogResult.OK;
            this.Close();
            string msg = string.Format("MinZ oldValue: {0} -> newValue: {1}", this.minZ, Machine.Instance.Robot.DefaultPrm.MinZ);
            Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
        }

        private void FormMinZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            Machine.Instance.Robot.AxisZ.SetLimit(200, Machine.Instance.Robot.DefaultPrm.MinZ);
        }
    }
}
