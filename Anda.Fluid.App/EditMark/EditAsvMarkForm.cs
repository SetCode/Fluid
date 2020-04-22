using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditMark
{
    public partial class EditAsvMarkForm : EditFormBase, IMsgSender
    {
        private bool isCreating;
        private Inspection inspection;
        private Pattern pattern;
        private PointD origin;
        private MarkCmdLine markCmdLine;
        public EditAsvMarkForm(Pattern pattern) : this(pattern, null)
        {

        }

        public EditAsvMarkForm(Pattern pattern, MarkCmdLine markCmdLine) : base(pattern == null ? new PointD(0, 0) : pattern.GetOriginPos())
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            for (int i = 10; i < 20; i++)
            {
                this.comboBox1.Items.Add(i);
            }
            this.cbResultType.Items.Add("1点+角度");
            this.cbResultType.Items.Add("2点");

            this.rbnTaught.Checked = true;
            this.rbnPlace.Checked = false;
            if (markCmdLine == null)
            {
                this.isCreating = true;
                this.markCmdLine = new MarkCmdLine();
                this.markCmdLine.ModelFindPrm.ExposureTime = Machine.Instance.Camera.Prm.Exposure;
                this.markCmdLine.ModelFindPrm.Gain = Machine.Instance.Camera.Prm.Gain;
                //this.markCmdLine.ModelFindPrm.LightType = Machine.Instance.Camera.Prm.LightType;
                this.markCmdLine.ModelFindPrm.ExecutePrm = (ExecutePrm)Machine.Instance.Light.ExecutePrm.Clone();
                this.cbResultType.SelectedIndex = 0;
                this.markCmdLine.ModelFindPrm.UnStandardType = 0;
            }
            else
            {
                this.isCreating = false;
                this.markCmdLine = markCmdLine;
                this.cbResultType.SelectedIndex = markCmdLine.ModelFindPrm.UnStandardType;
            }
            this.markCmdLine.ModelFindPrm.IsUnStandard = true;

            PointD p = this.pattern.MachineRel(this.markCmdLine.PosInPattern);
            tbLocationX.Text = p.X.ToString("0.000");
            tbLocationY.Text = p.Y.ToString("0.000");
            txtStandardX1.Text = this.markCmdLine.ModelFindPrm.ReferenceX.ToString();
            txtStandardY1.Text = this.markCmdLine.ModelFindPrm.ReferenceY.ToString();
            txtStandardX2.Text = this.markCmdLine.ModelFindPrm.ReferenceX2.ToString();
            txtStandardY2.Text = this.markCmdLine.ModelFindPrm.ReferenceY2.ToString();
            txtStandardA.Text = this.markCmdLine.ModelFindPrm.ReferenceA.ToString();

            this.nudSettingTime.Minimum = 0;
            this.nudSettingTime.Maximum = 5000;
            this.nudSettingTime.Value = this.markCmdLine.ModelFindPrm.SettlingTime;
            this.inspection = InspectionMgr.Instance.FindBy(this.markCmdLine.ModelFindPrm.InspectionKey);
            this.comboBox1.SelectedItem = this.markCmdLine.ModelFindPrm.InspectionKey;
            this.cameraControl1.SetExposure(this.markCmdLine.ModelFindPrm.ExposureTime);
            this.cameraControl1.SetGain(this.markCmdLine.ModelFindPrm.Gain);
            //this.cameraControl1.SelectLight(this.markCmdLine.ModelFindPrm.LightType);
            this.cameraControl1.SelectLight(this.markCmdLine.ModelFindPrm.ExecutePrm);

            this.SetupLight(this.markCmdLine.ModelFindPrm.ExecutePrm);
            this.ReadLanguageResources();
        }
        /// <summary>
        /// 仅用于语言切换生成语言文件
        /// </summary>
        private EditAsvMarkForm()
        {
            InitializeComponent();
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            tbLocationX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbLocationY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }
            this.inspection.SetImage(
                Machine.Instance.Camera.Executor.CurrentBytes,
                Machine.Instance.Camera.Executor.ImageWidth,
                Machine.Instance.Camera.Executor.ImageHeight);
            this.inspection.ShowEditWindow();
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }

            this.markCmdLine.ModelFindPrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.markCmdLine.ModelFindPrm.Gain = this.cameraControl1.Gain;
            //this.markCmdLine.ModelFindPrm.LightType = this.cameraControl1.Lighting;
            this.markCmdLine.ModelFindPrm.ExecutePrm =(ExecutePrm) this.cameraControl1.ExecutePrm.Clone();
            this.txtResultX1.BackColor = Color.Red;
            this.txtResultY1.BackColor = Color.Red;
            this.txtResultX2.BackColor = Color.Red;
            this.txtResultY2.BackColor = Color.Red;
            this.txtResultA.BackColor = Color.Red;
            this.txtResultX1.Text = string.Empty;
            this.txtResultY1.Text = string.Empty;
            this.txtResultX2.Text = string.Empty;
            this.txtResultY2.Text = string.Empty;
            this.txtResultA.Text = string.Empty;

            await Task.Factory.StartNew(() =>
            {
                if (this.rbnTaught.Checked)
                {
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
                }
                markCmdLine.ModelFindPrm.PosInMachine = new PointD(Machine.Instance.Robot.PosXY);
                Machine.Instance.CaptureMark(markCmdLine.ModelFindPrm);
            });

            if (markCmdLine.ModelFindPrm.TargetInMachine == null)
            {
                return;
            }

            this.txtResultX1.BackColor = Color.Green;
            this.txtResultY1.BackColor = Color.Green;
            this.txtResultX2.BackColor = Color.Green;
            this.txtResultY2.BackColor = Color.Green;
            this.txtResultA.BackColor = Color.Green;
            this.txtResultX1.Text = markCmdLine.ModelFindPrm.TargetInMachine.X.ToString("0.000");
            this.txtResultY1.Text = markCmdLine.ModelFindPrm.TargetInMachine.Y.ToString("0.000");
            if (markCmdLine.ModelFindPrm.UnStandardType == 0)
            {
                this.txtResultA.Text = markCmdLine.ModelFindPrm.Angle.ToString("0.000");
                this.txtResultX2.Text = 0.00.ToString("0.000");
                this.txtResultY2.Text = 0.00.ToString("0.000");
            }
            else
            {
                this.txtResultA.Text = 0.00.ToString("0.000");
                this.txtResultX2.Text = markCmdLine.ModelFindPrm.TargetInMachine2.X.ToString("0.000");
                this.txtResultY2.Text = markCmdLine.ModelFindPrm.TargetInMachine2.Y.ToString("0.000");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (markCmdLine.ModelFindPrm.TargetInMachine == null)
            {
                return;
            }
            VectorD v = markCmdLine.ModelFindPrm.TargetInMachine.ToSystem() - pattern.GetOriginSys();
            this.txtStandardX1.Text = (markCmdLine.ModelFindPrm.TargetInMachine.X - origin.X).ToString();
            this.txtStandardY1.Text = (markCmdLine.ModelFindPrm.TargetInMachine.Y - origin.Y).ToString();
            markCmdLine.ModelFindPrm.ReferenceX = v.X;
            markCmdLine.ModelFindPrm.ReferenceY = v.Y;
            if (markCmdLine.ModelFindPrm.UnStandardType == 0)
            {
                this.txtStandardA.Text = markCmdLine.ModelFindPrm.Angle.ToString();
                markCmdLine.ModelFindPrm.ReferenceA = markCmdLine.ModelFindPrm.Angle;
                this.txtStandardX2.Text = 0.00.ToString("0.000");
                this.txtStandardY2.Text = 0.00.ToString("0.000");
                markCmdLine.ModelFindPrm.ReferenceX2 = 0;
                markCmdLine.ModelFindPrm.ReferenceY2 = 0;
            }
            else
            {
                VectorD v2 = markCmdLine.ModelFindPrm.TargetInMachine2.ToSystem() - pattern.GetOriginSys();
                this.txtStandardA.Text = 0.00.ToString("0.000");
                markCmdLine.ModelFindPrm.ReferenceA = 0;
                this.txtStandardX2.Text = (markCmdLine.ModelFindPrm.TargetInMachine2.X - origin.X).ToString();
                this.txtStandardY2.Text = (markCmdLine.ModelFindPrm.TargetInMachine2.Y - origin.Y).ToString();
                markCmdLine.ModelFindPrm.ReferenceX2 = v2.X;
                markCmdLine.ModelFindPrm.ReferenceY2 = v2.Y;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的参数.");
                return;
            }
            //改变Mark曝光需要重新做飞拍校正
            if (cameraControl1.ExposureTime != markCmdLine.ModelFindPrm.ExposureTime)
            {
                this.pattern.Program.RuntimeSettings.FlyOffsetIsValid = false;
            }
            //机械坐标->系统坐标
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            markCmdLine.ModelFindPrm.PosInPattern.X = p.X;
            markCmdLine.ModelFindPrm.PosInPattern.Y = p.Y;
            markCmdLine.ModelFindPrm.ExposureTime = cameraControl1.ExposureTime;
            markCmdLine.ModelFindPrm.Gain = cameraControl1.Gain;
            //markCmdLine.ModelFindPrm.LightType = cameraControl1.Lighting;
            markCmdLine.ModelFindPrm.ExecutePrm = cameraControl1.ExecutePrm ;
            markCmdLine.ModelFindPrm.ExecutePrm = this.GetLightExecutePrm();
            if (comboBox1.SelectedItem == null)
                this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            markCmdLine.ModelFindPrm.InspectionKey = (int)comboBox1.SelectedItem;
            markCmdLine.ModelFindPrm.SettlingTime = (int)nudSettingTime.Value;
            markCmdLine.ModelFindPrm.UnStandardType = this.cbResultType.SelectedIndex;
            markCmdLine.PosInPattern.X = p.X;
            markCmdLine.PosInPattern.Y = p.Y;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, markCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, markCmdLine);
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbResultType_SelectedIndexChanged(object sender, EventArgs e)
        {
            markCmdLine.ModelFindPrm.UnStandardType = this.cbResultType.SelectedIndex;
        }
    }
}
