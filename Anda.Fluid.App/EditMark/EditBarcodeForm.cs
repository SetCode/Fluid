using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.Barcode;

namespace Anda.Fluid.App.EditMark
{
    public partial class EditBarcodeForm : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private BarcodeCmdLine barcodeCmdLine;
        private Inspection inspection;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        public EditBarcodeForm()
        {
            InitializeComponent();
        }
        public EditBarcodeForm(Pattern pattern) : this(pattern, null)
        {
        }
        public EditBarcodeForm(Pattern pattern, BarcodeCmdLine barcodeCmdLine) 
            : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            for (int i = 30; i < 40; i++)
            {
                this.comboBox1.Items.Add(i);
            }
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (barcodeCmdLine == null)
            {
                isCreating = true;
                this.barcodeCmdLine = new BarcodeCmdLine();
                this.barcodeCmdLine.BarcodePrm.ExposureTime = Machine.Instance.Camera.Prm.Exposure;
                this.barcodeCmdLine.BarcodePrm.Gain = Machine.Instance.Camera.Prm.Gain;
                this.barcodeCmdLine.BarcodePrm.ExecutePrm = (ExecutePrm)Machine.Instance.Light.ExecutePrm.Clone();
            }
            else
            {
                isCreating = false;
                this.barcodeCmdLine = barcodeCmdLine;
            }
            //系统坐标->机械坐标
            PointD p = this.pattern.MachineRel(this.barcodeCmdLine.PosInPattern);
            tbLocationX.Text = p.X.ToString("0.000");
            tbLocationY.Text = p.Y.ToString("0.000");

            this.nudSettingTime.Minimum = 0;
            this.nudSettingTime.Maximum = 5000;
            this.nudSettingTime.Value = this.barcodeCmdLine.BarcodePrm.SettlingTime;

            this.inspection = InspectionMgr.Instance.FindBy(this.barcodeCmdLine.BarcodePrm.InspectionKey);
            this.comboBox1.SelectedItem = this.barcodeCmdLine.BarcodePrm.InspectionKey;

            this.cameraControl1.SetExposure(this.barcodeCmdLine.BarcodePrm.ExposureTime);
            this.cameraControl1.SetGain(this.barcodeCmdLine.BarcodePrm.Gain);
            this.cameraControl1.SelectLight(this.barcodeCmdLine.BarcodePrm.ExecutePrm);
            this.SetupLight(this.barcodeCmdLine.BarcodePrm.ExecutePrm);

            this.nudMinLength.Value = this.barcodeCmdLine.BarcodePrm.MinLength;
            this.nudMaxLength.Value = this.barcodeCmdLine.BarcodePrm.MaxLength;
            this.rbnTaught.Checked = true;

            this.cmbFindCodeMethod.Items.Add(FindBarCodeType.ASV);
            this.cmbFindCodeMethod.Items.Add(FindBarCodeType.AFM);
            this.cmbFindCodeMethod.SelectedItem = this.barcodeCmdLine.BarcodePrm.FindBarCodeType;
            
            this.ReadLanguageResources();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值");
                return;
            }
            if (nudMinLength.Value > nudMaxLength.Value)
            {
                MessageBox.Show("请输入正确的长度范围");
                return;
            }
            //机械坐标->系统坐标
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            barcodeCmdLine.BarcodePrm.PosInPattern.X = p.X;
            barcodeCmdLine.BarcodePrm.PosInPattern.Y = p.Y;
            barcodeCmdLine.BarcodePrm.ExposureTime = cameraControl1.ExposureTime;
            barcodeCmdLine.BarcodePrm.Gain = cameraControl1.Gain;
            //barcodeCmdLine.BarcodePrm.ExecutePrm = cameraControl1.ExecutePrm;
            barcodeCmdLine.BarcodePrm.ExecutePrm = this.GetLightExecutePrm();
            if (comboBox1.SelectedItem == null)
                this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            barcodeCmdLine.BarcodePrm.InspectionKey = (int)comboBox1.SelectedItem;
            barcodeCmdLine.BarcodePrm.SettlingTime = (int)nudSettingTime.Value;
            barcodeCmdLine.BarcodePrm.MinLength = (int)nudMinLength.Value;
            barcodeCmdLine.BarcodePrm.MaxLength = (int)nudMaxLength.Value;
            barcodeCmdLine.PosInPattern.X = p.X;
            barcodeCmdLine.PosInPattern.Y = p.Y;
            barcodeCmdLine.BarcodePrm.FindBarCodeType = (FindBarCodeType)this.cmbFindCodeMethod.SelectedItem;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, barcodeCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, barcodeCmdLine);
            }
            //if (!this.isCreating)
            //{
                Close();
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }

            this.barcodeCmdLine.BarcodePrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.barcodeCmdLine.BarcodePrm.Gain = this.cameraControl1.Gain;
            //this.barcodeCmdLine.BarcodePrm.ExecutePrm = this.cameraControl1.ExecutePrm;
            this.barcodeCmdLine.BarcodePrm.ExecutePrm = this.GetLightExecutePrm();
            this.textBox1.BackColor = Color.Red;
            this.textBox1.Text = string.Empty;
            Result result = Result.FAILED;
            this.barcodeCmdLine.BarcodePrm.FindBarCodeType = (FindBarCodeType)this.cmbFindCodeMethod.SelectedItem;

            await Task.Factory.StartNew(() =>
            {
                if (this.rbnTaught.Checked)
                {
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
                }
                this.barcodeCmdLine.BarcodePrm.PosInMachine = new PointD(Machine.Instance.Robot.PosXY);
                Bitmap bmp;
                result = Machine.Instance.CaptureAndBarcode(this.barcodeCmdLine.BarcodePrm, out bmp);
            });

            if(result.IsOk)
            {
                this.textBox1.BackColor = Color.Green;
                this.textBox1.Text = this.barcodeCmdLine.BarcodePrm.Text;
            }
            else
            {
                this.textBox1.BackColor = Color.Red;
                this.textBox1.Text = "NG";
            }
        }
    }
}
