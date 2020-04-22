using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditMeasurementForm : EditFormBase,IMsgSender
    {
        private bool isCreating;
        private Inspection inspection;
        private Pattern pattern;
        private PointD origin;
        private MeasureCmdLine measureCmdLine;
        public EditMeasurementForm()
        {
            InitializeComponent();
        }
        public EditMeasurementForm(Pattern pattern):this(pattern,null)
        {

        }
        public EditMeasurementForm(Pattern pattern,MeasureCmdLine measureCmdLine):base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            for (int i = 20; i < 30; i++)
            {
                this.comboBox1.Items.Add(i);
            }           
            if (measureCmdLine == null)
            {
                this.isCreating = true;
                this.measureCmdLine = new MeasureCmdLine();
                this.measureCmdLine.MeasurePrm.ExposureTime = Machine.Instance.Camera.Prm.Exposure;
                this.measureCmdLine.MeasurePrm.Gain = Machine.Instance.Camera.Prm.Gain;
                this.measureCmdLine.MeasurePrm.ExecutePrm = (ExecutePrm)Machine.Instance.Light.ExecutePrm.Clone();
            }
            else
            {
                this.isCreating = false;
                this.measureCmdLine = measureCmdLine;
            }
            this.measureCmdLine.MeasurePrm.Vendor = MeasureVendor.ASV;

            PointD p=this.pattern.MachineRel(this.measureCmdLine.PosInPattern);
            tbLocationX.Text = p.X.ToString("0.000");
            tbLocationY.Text = p.Y.ToString("0.000");

            this.inspection = InspectionMgr.Instance.FindBy(this.measureCmdLine.MeasurePrm.InspectionKey);
            this.comboBox1.SelectedItem = this.measureCmdLine.MeasurePrm.InspectionKey;
            this.cameraControl1.SetExposure(this.measureCmdLine.MeasurePrm.ExposureTime);
            this.cameraControl1.SetGain(this.measureCmdLine.MeasurePrm.Gain);
            this.cameraControl1.SelectLight(this.measureCmdLine.MeasurePrm.ExecutePrm);
            this.nudSettingTime.Value = this.measureCmdLine.MeasurePrm.SettlingTime;

            this.nudUpper.Increment = (decimal)0.001;
            this.nudUpper.DecimalPlaces = 3;
            this.nudLower.Increment = (decimal)0.001;
            this.nudLower.DecimalPlaces = 3;

            this.nudUpper.Value = (decimal)this.measureCmdLine.MeasurePrm.Upper;
            this.nudLower.Value = (decimal)this.measureCmdLine.MeasurePrm.Lower;

            this.nudHeightUpper.Increment = (decimal)0.001;
            this.nudHeightUpper.DecimalPlaces = 3;
            this.nudHeightLower.Increment = (decimal)0.001;
            this.nudHeightLower.DecimalPlaces = 3;

            this.txtResult.ReadOnly = true;
            this.cmbMeasureType.Items.Add(MeasureType.圆半径);
            this.cmbMeasureType.Items.Add(MeasureType.线宽);
            this.cmbMeasureType.Items.Add(MeasureType.面积);
            this.cmbMeasureType.SelectedItem = MeasureType.线宽;
            this.SetupLight(this.measureCmdLine.MeasurePrm.ExecutePrm);
            
            this.ckbNeedMeasureHeight.Checked = this.measureCmdLine.MeasureContent.HasFlag(MeasureContents.GlueHeight);
            this.ckbMeasureLineWidth.Checked = this.measureCmdLine.MeasureContent.HasFlag(MeasureContents.LineWidth);

            this.heightControl1.LaserControl.MeasureStarting += HeightControl1_MeasureStarting;

            //if (this.measureCmdLine.MeasureContent.HasFlag(MeasureContents.GlueHeight))
            //{
            //}
            this.nudHeightUpper.Value = (decimal)this.measureCmdLine.MeasurePrm.ToleranceMax;
            this.nudHeightLower.Value = (decimal)this.measureCmdLine.MeasurePrm.ToleranceMin;
            if (this.measureCmdLine.MeasureHeightCmdLines != null && this.measureCmdLine.MeasureHeightCmdLines.Count != 0)
            {
                PointD ph = this.pattern.MachineRel(this.measureCmdLine.MeasureHeightCmdLines[0].Position);
                this.tbLocationHeightX.Text = ph.X.ToString("0.000");
                this.tbLocationHeightY.Text = ph.Y.ToString("0.000");
                foreach (var item in this.measureCmdLine.MeasureHeightCmdLines)
                {
                    this.heightControl1.SetupCmdLine(item);
                }
            }
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
            if (this.comboBox1.SelectedItem == null)
                this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            this.inspection = InspectionMgr.Instance.FindBy((int)this.comboBox1.SelectedItem);
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
            if (this.inspection==null)
            {
                return;
            }
            this.measureCmdLine.MeasurePrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.measureCmdLine.MeasurePrm.Gain = this.cameraControl1.Gain;
            this.measureCmdLine.MeasurePrm.ExecutePrm = this.cameraControl1.ExecutePrm;
            //this.measureCmdLine.MeasurePrm.ExecutePrm = this.GetLightExecutePrm();
            this.txtResult.BackColor = Color.Red;
            this.measureCmdLine.MeasurePrm.Upper = (double)this.nudUpper.Value;
            this.measureCmdLine.MeasurePrm.Lower = (double)this.nudLower.Value;

            Result res = Result.OK;
            await Task.Factory.StartNew(()=>
            {
                Bitmap bmp;
                Machine.Instance.Robot.MoveSafeZAndReply();
                Machine.Instance.Robot.ManualMovePosXYAndReply(origin.X+tbLocationX.Value, origin.Y+tbLocationY.Value);
                res= Machine.Instance.CaptureAndMeasure(measureCmdLine.MeasurePrm, out bmp);
            });
          
            this.txtResult.Text = this.measureCmdLine.MeasurePrm.PhyResult.ToString("0.000");
            
            if (!this.measureCmdLine.MeasurePrm.WidthIsOutofTolerance())
            {
                this.txtResult.BackColor = Color.Green;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                MessageBox.Show("请输入正确的参数");
                return;
            }
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            this.measureCmdLine.PosInPattern.X = p.X;
            this.measureCmdLine.PosInPattern.Y = p.Y;
            this.measureCmdLine.MeasurePrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.measureCmdLine.MeasurePrm.Gain = this.cameraControl1.Gain;
            this.measureCmdLine.MeasurePrm.ExecutePrm = cameraControl1.ExecutePrm;
            //this.measureCmdLine.MeasurePrm.ExecutePrm = this.GetLightExecutePrm();
            if (this.comboBox1.SelectedItem == null)
                this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            this.measureCmdLine.MeasurePrm.InspectionKey = (int)this.comboBox1.SelectedItem;
            this.measureCmdLine.MeasurePrm.SettlingTime = (int)nudSettingTime.Value;
            this.measureCmdLine.MeasurePrm.PosInPattern.X = p.X;
            this.measureCmdLine.MeasurePrm.PosInPattern.Y = p.Y;
            this.measureCmdLine.MeasurePrm.Upper = (double)this.nudUpper.Value;
            this.measureCmdLine.MeasurePrm.Lower = (double)this.nudLower.Value;

            this.measureCmdLine.MeasurePrm.measureType = (MeasureType)this.cmbMeasureType.SelectedItem;

            //this.measureCmdLine.NeedMeasureHeight = this.ckbNeedMeasureHeight.Checked;
            this.measureCmdLine.MeasureContent = MeasureContents.None;
            if (this.ckbNeedMeasureHeight.Checked)
            {
                this.measureCmdLine.MeasureContent |= MeasureContents.GlueHeight;
            }
            if (this.ckbMeasureLineWidth.Checked)
            {
                this.measureCmdLine.MeasureContent |= MeasureContents.LineWidth;
            }
           
            if (this.measureCmdLine.MeasureContent.HasFlag(MeasureContents.GlueHeight))
            {
                if (this.measureCmdLine.MeasureHeightCmdLines==null)
                {
                    this.measureCmdLine.MeasureHeightCmdLines = new List<MeasureHeightCmdLine>();
                }
                this.measureCmdLine.MeasurePrm.ToleranceMax = (double)this.nudHeightUpper.Value;
                this.measureCmdLine.MeasurePrm.ToleranceMin = (double)this.nudHeightLower.Value;
                this.measureCmdLine.MeasureHeightCmdLines.Clear();
                PointD pHeight = this.pattern.SystemRel(this.tbLocationHeightX.Value, this.tbLocationHeightY.Value);
                for (int i = 0; i < 2; i++)
                {
                    MeasureHeightCmdLine mhCmdLine = new MeasureHeightCmdLine();
                    mhCmdLine.Position.X = pHeight.X;
                    mhCmdLine.Position.Y = pHeight.Y;
                    mhCmdLine.StandardHt = this.heightControl1.BoardHeight;
                    mhCmdLine.ToleranceMax = this.heightControl1.MaxTolerance;
                    mhCmdLine.ToleranceMin = this.heightControl1.MinTolerance;
                    this.measureCmdLine.MeasureHeightCmdLines.Add(mhCmdLine);
                }
               
            }
            
            if (this.isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, this.measureCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.measureCmdLine);
            }
            this.Close();
        }

        private void HeightControl1_MeasureStarting(PointD obj)
        {
            obj.X = origin.X + tbLocationHeightX.Value;
            obj.Y = origin.Y + tbLocationHeightY.Value;
        }

        private void btnTeachHeight_Click(object sender, EventArgs e)
        {
            this.tbLocationHeightX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            this.tbLocationHeightY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGotoHeight_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbLocationHeightX.Value, origin.Y + tbLocationHeightY.Value);
        }
    }
}
