using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Sensors.Lighting;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class VisionBar : MetroSetUserControl
    {
        private CameraView cameraView;
        private Image imgLightNone = Properties.Resources.numbers_0_24px;
        private Image imgLigthCoax = Properties.Resources.numbers_1_24px;
        private Image imgLigthRing = Properties.Resources.numbers_2_24px;
        private Image imgLightBoth = Properties.Resources.numbers_3_24px;
        private Image imgShapeRect = Properties.Resources.Rect_24px_1186322;
        private Image imgShapeCircle = Properties.Resources.Circle_24px_1185757;
        private const int SHAPE_RECT = 0;
        private const int SHAPE_CIRCLE = 1;
        private int lastShape = 0;
        //private LightType lastLightType = LightType.None;
        private ExecutePrm executePrm = new ExecutePrm();


        public VisionBar()
        {
            InitializeComponent();
            this.ShowBorder = false;

            this.tbrExposure.Minimum = 0;
            this.tbrExposure.Maximum = 10000;
            this.tbrExposure.SmallChange = 5;
            this.tbrExposure.TickFrequency = 100;
            this.tbrExposure.ValueChanged += TbrExposure_ValueChanged;

            this.tbrGain.Minimum = 0;
            this.tbrGain.Maximum = 1000;
            this.tbrGain.TickFrequency = 100;
            this.tbrGain.SmallChange = 5;
            this.tbrGain.ValueChanged += TbrGain_ValueChanged;

            this.nudRaduis.Minimum = 0.01M;
            this.nudRaduis.Maximum = 100;
            this.nudRaduis.DecimalPlaces = 2;
            this.nudRaduis.Increment = 0.1M;
            this.nudRaduis.ValueChanged += NudRaduis_ValueChanged;
            if (Properties.Settings.Default.roiRaduis == 0)
            {
                Properties.Settings.Default.roiRaduis = 1;
            }
            this.nudRaduis.Value = (decimal)Properties.Settings.Default.roiRaduis;

            this.btnShape.Click += BtnShape_Click;
            this.lastShape = Properties.Settings.Default.roiType;
            this.updateBtnShape(lastShape);

            this.btnLight.Click += BtnLight_Click;
            this.executePrm.LightType= (LightType)Properties.Settings.Default.ligthType;
            //this.lastLightType = (LightType)Properties.Settings.Default.ligthType;
            this.updateBtnLight(this.executePrm.LightType);
        }

        private void TbrGain_ValueChanged(object sender, EventArgs e)
        {
            if (this.cameraView == null) return;
            if (this.cameraView.Camera == null) return;
            this.lblGain.Text = this.tbrGain.Value.ToString();
            this.cameraView.Camera.SetGain((int)this.tbrGain.Value);
            Properties.Settings.Default.gain = (int)this.tbrGain.Value;
            //if (this.camera.Prm != null && this.prmBackUp != null)
            //{
            //    CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            //}
        }

        private void TbrExposure_ValueChanged(object sender, EventArgs e)
        {
            if (this.cameraView == null) return;
            if (this.cameraView.Camera == null) return;
            this.lblExpo.Text = this.tbrExposure.Value.ToString();
            this.cameraView.Camera.SetExposure((int)this.tbrExposure.Value);
            Properties.Settings.Default.exposureTime = (int)this.tbrExposure.Value;
            //if (this.camera.Prm != null && this.prmBackUp != null)
            //{
            //    CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            //}
        }

        private void VisionBar_Load(object sender, EventArgs e)
        {

        }

        //private void BtnLight_Click(object sender, EventArgs e)
        //{
        //    if (this.cameraView == null) return;
        //    if (this.cameraView.Camera == null) return;
        //    LightType lightType = LightType.None;
        //    switch (lastLightType)
        //    {
        //        case LightType.None:
        //            lightType = LightType.Coax;
        //            break;
        //        case LightType.Coax:
        //            lightType = LightType.Ring;
        //            break;
        //        case LightType.Ring:
        //            lightType = LightType.Both;
        //            break;
        //        case LightType.Both:
        //            lightType = LightType.None;
        //            break;
        //    }
        //    Machine.Instance.Light.SetLight(lightType);
        //    this.updateBtnLight(lightType);
        //    Properties.Settings.Default.ligthType = (int)lightType;
        //    this.lastLightType = lightType;
        //}
        private void BtnLight_Click(object sender, EventArgs e)
        {
            if (this.cameraView == null) return;
            if (this.cameraView.Camera == null) return;
            LightType lightType = LightType.None;
            switch (this.executePrm.LightType)
            {
                case LightType.None:
                    lightType = LightType.Coax;
                    break;
                case LightType.Coax:
                    lightType = LightType.Ring;
                    break;
                case LightType.Ring:
                    lightType = LightType.Both;
                    break;
                case LightType.Both:
                    lightType = LightType.None;
                    break;
            }
            Machine.Instance.Light.SetLight(this.executePrm);
            this.updateBtnLight(lightType);
            Properties.Settings.Default.ligthType = (int)lightType;
            //this.lastLightType = lightType;
            this.executePrm.LightType = lightType;
        }

        private void updateBtnLight(LightType lightType)
        {
            switch (lightType)
            {
                case LightType.None:
                    btnLight.Image = imgLightNone;
                    break;
                case LightType.Coax:
                    btnLight.Image = imgLigthCoax;
                    break;
                case LightType.Ring:
                    btnLight.Image = imgLigthRing;
                    break;
                case LightType.Both:
                    btnLight.Image = imgLightBoth;
                    break;
            }
        }

        private void BtnShape_Click(object sender, EventArgs e)
        {
            if (this.cameraView == null) return;
            if (this.cameraView.Camera == null) return;
            int shape = 0;
            switch (lastShape)
            {
                case SHAPE_RECT:
                    shape = SHAPE_CIRCLE;
                    break;
                case SHAPE_CIRCLE:
                    shape = SHAPE_RECT;
                    break;
            }
            this.cameraView.UpdateShape(shape);
            Properties.Settings.Default.roiType = shape;
            this.updateBtnShape(shape);
            this.cameraView.Invalidate();
            this.lastShape = shape;
        }

        private void updateBtnShape(int shape)
        {
            switch (shape)
            {
                case SHAPE_RECT:
                    this.btnShape.Image = imgShapeRect;
                    break;
                case SHAPE_CIRCLE:
                    this.btnShape.Image = imgShapeCircle;
                    break;
            }
        }

        private void NudRaduis_ValueChanged(object sender, EventArgs e)
        {
            if (this.cameraView == null) return;
            if (this.cameraView.Camera == null) return;
            this.cameraView.UpdateROI((double)this.nudRaduis.Value); 
            Properties.Settings.Default.roiRaduis = (double)this.nudRaduis.Value;
        }

        public void ConnectCameraView(CameraView cameraView)
        {
            this.cameraView = cameraView;
            if (this.cameraView.Camera == null) return;
            try
            {
                this.tbrGain.Minimum = this.cameraView.Camera.GainMin;
                this.tbrGain.Maximum = this.cameraView.Camera.GainMax;
                this.tbrExposure.Minimum = this.cameraView.Camera.ExposureTimeMin;
                this.tbrExposure.Maximum = this.cameraView.Camera.ExposureTimeMax;
                this.tbrExposure.Value = MathUtils.Limit(cameraView.Camera.Prm.Exposure, tbrExposure.Minimum, tbrExposure.Maximum);
                this.tbrGain.Value = MathUtils.Limit(cameraView.Camera.Prm.Gain, tbrGain.Minimum, tbrGain.Maximum);
                this.lblExpo.Text = this.tbrExposure.Value.ToString();
                this.lblGain.Text = this.tbrGain.Value.ToString();
                //this.cbxLight.SelectedIndex = (int)this.camera.Prm.LightType;
                this.cameraView.UpdateShape(this.lastShape);
                this.cameraView.UpdateROI((double)this.nudRaduis.Value);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
