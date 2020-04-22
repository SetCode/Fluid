using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Common;
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

namespace Anda.Fluid.App.EditInspection
{
    public partial class EditInspectionForm : EditFormBase
    {
        private Inspection inspection;
        private Inspection inspectionBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditInspectionForm()
        {
            InitializeComponent();
        }
             
        public EditInspectionForm(Inspection inspection)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            foreach (var item in InspectionMgr.Instance.List)
            {
                this.comboBox1.Items.Add(item.Key);
            }

            this.rbnTaught.Checked = true;
            this.rbnPlace.Checked = false;

            this.inspection = inspection;
            this.comboBox1.SelectedItem = this.inspection.Key;
            this.tbLocationX.Text = this.inspection.PosInMachine.X.ToString("0.000");
            this.tbLocationY.Text = this.inspection.PosInMachine.Y.ToString("0.000");
            
            this.cameraControl1.SetExposure(this.inspection.ExposureTime);
            this.cameraControl1.SetGain(this.inspection.Gain);
            //this.cameraControl1.SelectLight(this.inspection.LightType);
            this.cameraControl1.SelectLight(this.inspection.ExecutePrm);
            this.inspectionBackUp = (Inspection)this.inspection.Clone();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(this.inspection == null)
            {
                return;
            }
            this.inspection.SetImage(
                Machine.Instance.Camera.Executor.CurrentBytes,
                Machine.Instance.Camera.Executor.ImageWidth,
                Machine.Instance.Camera.Executor.ImageHeight);
            this.inspection.ShowEditWindow();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }

            this.inspection.ExposureTime = this.cameraControl1.ExposureTime;
            this.inspection.Gain = this.cameraControl1.Gain;
            //this.inspection.LightType = this.cameraControl1.Lighting;
            this.inspection.ExecutePrm = this.cameraControl1.ExecutePrm;
            this.inspection.PosInMachine.X = tbLocationX.Value;
            this.inspection.PosInMachine.Y = tbLocationY.Value;

            Task.Factory.StartNew(() =>
            {
                if (this.rbnTaught.Checked)
                {
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //Machine.Instance.Robot.MovePosXYAndReply(this.inspection.PosInMachine);
                    Machine.Instance.Robot.ManualMovePosXYAndReply(this.inspection.PosInMachine);
                }
                if (this.inspection is InspectionLine)
                {
                    InspectionLine inspectionLine = this.inspection as InspectionLine;
                    this.inspection = inspectionLine;
                }
                //double imgX, imgY, dx, dy;
                Result rtn = Machine.Instance.CaptureAndInspect(this.inspection);
                this.BeginInvoke(new Action(()=> {
                    if(rtn.IsOk)
                    {
                        this.txtResult.BackColor = Color.Green;
                    }
                    else
                    {
                        this.txtResult.BackColor = Color.Red;
                    }
                    this.txtResult.Text = this.inspection.CurrResultStr;
                }));
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedItem == null)
            {
                return;
            }
            int key = (int)this.comboBox1.SelectedItem;
            this.inspection = InspectionMgr.Instance.FindBy(key);
            if (this.inspection == null)
            {
                return;
            }
            this.tbLocationX.Text = this.inspection.PosInMachine.X.ToString("0.000");
            this.tbLocationY.Text = this.inspection.PosInMachine.Y.ToString("0.000");
            this.cameraControl1.SetExposure(this.inspection.ExposureTime);
            this.cameraControl1.SetGain(this.inspection.Gain);
            //this.cameraControl1.SelectLight(this.inspection.LightType);     
            this.cameraControl1.SelectLight(this.inspection.ExecutePrm);
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }
            this.inspection.PosInMachine.X = Machine.Instance.Robot.PosX;
            this.inspection.PosInMachine.Y = Machine.Instance.Robot.PosY;
            tbLocationX.Text = this.inspection.PosInMachine.X.ToString("0.000");
            tbLocationY.Text = this.inspection.PosInMachine.Y.ToString("0.000");
            this.inspection.ExposureTime = this.cameraControl1.ExposureTime;
            this.inspection.Gain = this.cameraControl1.Gain;
            //this.inspection.LightType = this.cameraControl1.Lighting;
            this.inspection.ExecutePrm = this.cameraControl1.ExecutePrm;
            CompareObj.CompareField(this.inspection, this.inspectionBackUp, null, this.GetType().Name, true);
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (this.inspection == null)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(tbLocationX.Value, tbLocationY.Value);
            Machine.Instance.Robot.ManualMovePosXY(tbLocationX.Value, tbLocationY.Value);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.inspection.PosInMachine.X = Machine.Instance.Robot.PosX;
            this.inspection.PosInMachine.Y = Machine.Instance.Robot.PosY;
            this.inspection.ExposureTime = this.cameraControl1.ExposureTime;
            this.inspection.Gain = this.cameraControl1.Gain;
            //this.inspection.LightType = this.cameraControl1.Lighting;
            this.inspection.ExecutePrm = this.cameraControl1.ExecutePrm;
            //Machine.Instance.Valve1.InspectionKey = this.inspection.Key;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
