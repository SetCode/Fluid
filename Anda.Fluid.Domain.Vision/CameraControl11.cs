using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.International;
using System.Threading;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting;

namespace Anda.Fluid.Domain.Vision
{
    public partial class CameraControl11 : UserControlEx
    {
        private Camera camera = null;
        private Pen penRedDash;
        private Pen penRed;
        private Rectangle imgRectRoi;
        private Rectangle pbxRectRoi;
        private int shapeIndex = 0;
        private bool hideROI = false;
        private ContextMenuStrip cms;
        private const string STR_SAVE_IMAGE = "Save Image";

        private string light1 = "None";
        private string light2 = "Coax";
        private string light3 = "Ring";
        private string light4 = "Both";

        private CameraPrm prmBackUp;
        public CameraControl11()
        {
            InitializeComponent();

            this.picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            this.tbrExposure.Minimum = 0;
            this.tbrExposure.Maximum = 10000;
            this.tbrExposure.SmallChange = 5;
            this.tbrExposure.TickFrequency = 100;
            this.tbrExposure.ValueChanged += TbrExposure_ValueChanged; 

            this.tbrGain.Minimum = 300;
            this.tbrGain.Maximum = 850;
            this.tbrGain.TickFrequency = 100;
            this.tbrGain.SmallChange = 5;
            this.tbrGain.ValueChanged += TbrGain_ValueChanged;

            this.cbxLight.Items.Add(LightType.None);
            this.cbxLight.Items.Add(LightType.Coax);
            this.cbxLight.Items.Add(LightType.Ring);
            this.cbxLight.Items.Add(LightType.Both);
            this.cbxLight.SelectedIndexChanged += CbxLight_SelectedIndexChanged;

            this.nudRaduis.Minimum = 0.01M;
            this.nudRaduis.Maximum = 100;
            this.nudRaduis.DecimalPlaces = 2;
            this.nudRaduis.Increment = 0.1M;
            this.nudRaduis.ValueChanged += NudRaduis_ValueChanged;
            if (Properties.Settings.Default.roiRaduis == 0)
            {
                Properties.Settings.Default.roiRaduis = 2;
            }

            this.btnShape.Click += BtnShape_Click;
            this.shapeIndex = Properties.Settings.Default.roiType;
            this.UpdateBtnShape();

            this.penRedDash = new Pen(Color.Red);
            this.penRedDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.penRed = new Pen(Color.Red);
            this.imgRectRoi = new Rectangle();
            this.pbxRectRoi = new Rectangle();

            this.picCamera.MouseClick += PicCamera_MouseClick;
            this.picCamera.Paint += PicCamera_Paint;
            this.picCamera.SizeChanged += PicCamera_SizeChanged;

            this.cms = new ContextMenuStrip();
            this.cms.Items.Add("Save Image");
            this.cms.ItemClicked += Cms_ItemClicked;

            this.DoubleBuffered = true;
            if(Machine.Instance.Camera != null)
            {
                this.SetupCamera(Machine.Instance.Camera);
            }
            this.nudRaduis.Value = (decimal)Properties.Settings.Default.roiRaduis;

            //切换光源显示为当前光源
            this.cbxLight.SelectedIndex = Properties.Settings.Default.ligthType;

            this.UpdateUI();
        }

        public Camera Cam => this.camera;

        public int ExposureTime => (int)this.tbrExposure.Value;

        public int Gain => (int)this.tbrGain.Value;

        public LightType Lighting => (LightType)this.cbxLight.SelectedIndex;

        public double Radio { get; private set; } = 1;

        public PictureBox Pbx => this.picCamera;

        public Rectangle ImgRectROI => this.imgRectRoi;

        public Rectangle PbxRectROI => this.pbxRectRoi;

        public Point ImgCenter => new Point(this.camera.Executor.ImageWidth / 2, this.camera.Executor.ImageHeight / 2);

        public Point PbxCenter => new Point(this.picCamera.Width / 2, this.picCamera.Height / 2);

        /// <summary>
        /// 图像实际宽度
        /// </summary>
        public int ImgWidth { get; private set; } = 1;

        /// <summary>
        /// 图像实际高度
        /// </summary>
        public int ImgHeight { get; private set; } = 1;

        /// <summary>
        /// 图像在控件中的宽度
        /// </summary>
        public int PbxImgWidth { get; private set; } = 1;

        /// <summary>
        /// 图像在控件中的高度
        /// </summary>
        public int PbxImgHeight { get; private set; } = 1;

        /// <summary>
        /// 隐藏ROI区域及ROI相关控制
        /// </summary>
        public void HideROI()
        {
            this.hideROI = true;
            this.btnShape.Enabled = false;
            this.nudRaduis.Enabled = false;
        }

        public CameraControl11 SetupCamera(Camera camera)
        {
            this.camera = camera;
            this.camera.Executor.BitmapDisplayed -= Executor_BitmapGrabbed;
            try
            {
                this.ImgWidth = this.camera.Executor.ImageWidth;
                this.ImgHeight = this.camera.Executor.ImageHeight;
                this.tbrGain.Minimum = this.camera.GainMin;
                this.tbrGain.Maximum = this.camera.GainMax;
                this.tbrExposure.Minimum = this.camera.ExposureTimeMin;
                this.tbrExposure.Maximum = this.camera.ExposureTimeMax;

                this.tbrExposure.Value = MathUtils.Limit(camera.Prm.Exposure, tbrExposure.Minimum, tbrExposure.Maximum);
                this.tbrGain.Value = MathUtils.Limit(camera.Prm.Gain, tbrGain.Minimum, tbrGain.Maximum);
                this.lblExpo.Text = this.tbrExposure.Value.ToString();
                this.lblGain.Text = this.tbrGain.Value.ToString();
                //this.cbxLight.SelectedIndex = (int)this.camera.Prm.LightType;

                this.CalcRadio();

                this.imgRectRoi.Width = (int)this.nudRaduis.Value * 2;
                this.imgRectRoi.Height = (int)this.nudRaduis.Value * 2;
                this.imgRectRoi.X = (this.ImgWidth - this.imgRectRoi.Width) / 2;
                this.imgRectRoi.Y = (this.ImgHeight - this.imgRectRoi.Height) / 2;
                this.CalcRoiRectPbx();
            }
            catch
            {

            }
            this.camera.Executor.BitmapDisplayed += Executor_BitmapGrabbed;
            if (this.camera.Prm != null)
            {
                this.prmBackUp = (CameraPrm)this.camera.Prm.Clone();
            }
            return this;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(label1.Name, label1.Text);
            this.SaveKeyValueToResources(label2.Name, label2.Text);
            this.SaveKeyValueToResources("LightType1", light1);
            this.SaveKeyValueToResources("LightType2", light2);
            this.SaveKeyValueToResources("LightType3", light3);
            this.SaveKeyValueToResources("LightType4", light4);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            label1.Text = this.ReadKeyValueFromResources(label1.Name);
            label2.Text = this.ReadKeyValueFromResources(label2.Name);
            this.cbxLight.Items[0] = this.ReadKeyValueFromResources("LightType1");
            this.cbxLight.Items[1] = this.ReadKeyValueFromResources("LightType2");
            this.cbxLight.Items[2] = this.ReadKeyValueFromResources("LightType3");
            this.cbxLight.Items[3] = this.ReadKeyValueFromResources("LightType4");
        }
        public void UpdateUI()
        {
            //切换一次数值，触发主界面相机控件重绘
            decimal temp = this.nudRaduis.Value;
            this.nudRaduis.Value = 0.01M;
            this.nudRaduis.Value = temp;
            this.ReadLanguageResources();
        }
        public CameraControl11 SetExposure(int exposureTime)
        {
            if (this.camera == null)
            {
                return this;
            }
            try
            {
                this.camera.SetExposure(exposureTime);
                this.tbrExposure.Value = exposureTime;
            }
            catch
            {

            }
            return this;
        }

        public CameraControl11 SetGain(int gain)
        {
            if (this.camera == null)
            {
                return this;
            }

            try
            {
                this.camera.SetGain(gain);
                this.tbrGain.Value = gain;
            }
            catch
            {

            }
            return this;
        }
        /// <summary>
        /// Anda 光源
        /// </summary>
        /// <param name="lightType"></param>
        public void SelectLight(LightType lightType)
        {
            //Machine.Instance.Light.SetLight(lightType);
            if (Machine.Instance.Light.Lighting.lightVendor==Drive.Sensors.Lighting.LightVendor.Anda)
            {
                AndaLight light=Machine.Instance.Light.Lighting as AndaLight;
                light.SetLight(lightType);
            }
            this.cbxLight.SelectedIndex = (int)lightType;
        }

        private void TbrGain_ValueChanged(object sender, EventArgs e)
        {
            this.lblGain.Text = this.tbrGain.Value.ToString();

            if (this.camera == null)
            {
                return;
            }

            if (this.camera.SetGain((int)this.tbrGain.Value) != 0)
            {
                return;
            }
            Properties.Settings.Default.gain = (int)this.tbrGain.Value;
            if (this.camera.Prm != null && this.prmBackUp != null)
            {
                CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
        }

        private void TbrExposure_ValueChanged(object sender, EventArgs e)
        {
            this.lblExpo.Text = this.tbrExposure.Value.ToString();

            if (this.camera == null)
            {
                return;
            }

            if (this.camera.SetExposure((int)this.tbrExposure.Value) != 0)
            {
                return;
            }
            Properties.Settings.Default.exposureTime = (int)this.tbrExposure.Value;
            if (this.camera.Prm != null && this.prmBackUp != null)
            {
                CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
        }

        private void NudRaduis_ValueChanged(object sender, EventArgs e)
        {
            if (this.camera == null) 
                return;

            if (Machine.Instance.Robot.CalibBy9dPrm.Scale == 0)
                return;

            //单个像素的长度(mm)
            double pixesLength = Machine.Instance.Robot.CalibBy9dPrm.Scale;

            //求得指定长度的像素个数
            int pixesInCamera = (int)((double)this.nudRaduis.Value / pixesLength);

            //求得在实际显示控件中应该显示的像素个数
            int pixesInControlX = (this.ImgWidth / this.camera.Executor.ImageWidth) * pixesInCamera;
            int pixesInControlY = (this.ImgHeight / this.camera.Executor.ImageHeight) * pixesInCamera;

            this.imgRectRoi.Width = pixesInControlX;
            this.imgRectRoi.Height = pixesInControlX;
            this.imgRectRoi.X = (this.ImgWidth - this.imgRectRoi.Width) / 2;
            this.imgRectRoi.Y = (this.ImgHeight - this.imgRectRoi.Height) / 2;
            this.CalcRoiRectPbx();
            this.picCamera.Invalidate();
            Thread.Sleep(3);
            Properties.Settings.Default.roiRaduis = (double)this.nudRaduis.Value;
        }

        private void UpdateBtnShape()
        {
            switch (this.shapeIndex)
            {
                case 0:
                    this.btnShape.Image = Properties.Resources._0_Percents_16px;
                    break;
                case 1:
                    this.btnShape.Image = Properties.Resources.Rectangle_16px;
                    break;
            }
        }

        private void BtnShape_Click(object sender, EventArgs e)
        {
            switch(this.shapeIndex)
            {
                case 0:
                    this.shapeIndex = 1;
                    break;
                case 1:
                    this.shapeIndex = 0;
                    break;
            }
            Properties.Settings.Default.roiType = this.shapeIndex;
            this.UpdateBtnShape();
            this.picCamera.Invalidate();
        }

        private void PicCamera_Paint(object sender, PaintEventArgs e)
        {
            this.drawBaseLines(e);

            if(this.hideROI)
            {
                return;
            }

            switch (this.shapeIndex)
            {
                case 0:
                    e.Graphics.DrawRectangle(this.penRed, this.pbxRectRoi);
                    break;
                case 1:
                    e.Graphics.DrawEllipse(this.penRed, this.pbxRectRoi);
                    break;
            }
        }

        private void drawBaseLines(PaintEventArgs e)
        {
            e.Graphics.DrawLine(penRed, 0, this.picCamera.Height / 2, this.picCamera.Width, this.picCamera.Height / 2);
            e.Graphics.DrawLine(penRed, this.picCamera.Width / 2, 0, this.picCamera.Width / 2, this.picCamera.Height);
            if(Machine.Instance.Robot == null)
            {
                return;
            }
            double pixesPerUnit = (0.1 / Machine.Instance.Robot.CalibBy9dPrm.Scale);
            if(pixesPerUnit <= 0)
            {
                return;
            }
            int count_x = (int)((this.ImgWidth - this.ImgCenter.X) / pixesPerUnit);
            int count_y = (int)((this.ImgHeight - this.ImgCenter.Y) / pixesPerUnit);
            for (int i = 1; i <= count_x; i++)
            {
                int x = (int)(this.ImgCenter.X + pixesPerUnit * i);
                int y = this.ImgCenter.Y;
                int h = 5; if (i % 10 == 0) { h = 15; } else if (i % 5 == 0) { h = 10; }
                e.Graphics.DrawLine(penRed, convertPixToControl(x, y + h), convertPixToControl(x, y - h));
                x = (int)(this.ImgCenter.X - pixesPerUnit * i);
                e.Graphics.DrawLine(penRed, convertPixToControl(x, y + h), convertPixToControl(x, y - h));
            }

            for (int i = 1; i <= count_y; i++)
            {
                int x = this.ImgCenter.X;
                int y = (int)(this.ImgCenter.Y + pixesPerUnit * i);
                int h = 5; if (i % 10 == 0) { h = 15; } else if (i % 5 == 0) { h = 10; }
                e.Graphics.DrawLine(penRed, convertPixToControl(x + h, y), convertPixToControl(x - h, y));
                y = (int)(this.ImgCenter.Y - pixesPerUnit * i);
                e.Graphics.DrawLine(penRed, convertPixToControl(x + h, y), convertPixToControl(x - h, y));
            }
        }

        private Point convertPixToControl(int xp, int yp)
        {
            int x = this.PbxCenter.X + (int)((xp - this.ImgCenter.X) / this.Radio);
            int y = this.PbxCenter.Y + (int)((yp - this.ImgCenter.Y) / this.Radio);
            return new Point(x, y);
        }

        private void CalcRadio()
        {
            double d1 = (double)this.picCamera.Width / this.picCamera.Height;
            double d2 = (double)this.ImgWidth / this.ImgHeight;
            if (d1 > d2)
            {
                this.Radio = (double)this.ImgHeight / this.picCamera.Height;
                this.PbxImgWidth = this.picCamera.Height * this.ImgWidth / this.ImgHeight;
                this.PbxImgHeight = this.picCamera.Height;
            }
            else
            {
                this.Radio = (double)this.ImgWidth / this.picCamera.Width;
                this.PbxImgWidth = this.picCamera.Width;
                this.PbxImgHeight = this.picCamera.Width * this.ImgHeight / this.ImgWidth;
            }
        }

        private void CalcRoiRectPbx()
        {
            this.pbxRectRoi.Width = (int)(this.imgRectRoi.Width / this.Radio);
            this.pbxRectRoi.Height = (int)(this.imgRectRoi.Height / this.Radio);
            this.pbxRectRoi.X = (this.picCamera.Width - this.pbxRectRoi.Width) / 2;
            this.pbxRectRoi.Y = (this.picCamera.Height - this.pbxRectRoi.Height) / 2;
        }

        private void PicCamera_SizeChanged(object sender, EventArgs e)
        {
            this.CalcRadio();
            this.CalcRoiRectPbx();
        }

        private void PicCamera_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(Machine.Instance.IsBusy)
                {
                    return;
                }

                double imgX = 0, imgY = 0;
                //double phyX = 0, phyY = 0;

                imgX = this.Radio * (this.PbxImgWidth / 2 - (this.picCamera.Width / 2 - e.X));
                imgY = this.Radio * (this.PbxImgHeight / 2 - (this.picCamera.Height / 2 - e.Y));

                if(imgX < 0 || imgY < 0)
                {
                    return;
                }

                PointD p = Machine.Instance.Camera.ToMachine(imgX, imgY);
                //CalibBy9d.ConvertImg2Phy(ref imgX, ref imgY, ref phyX, ref phyY);
                if (Math.Abs(p.X) > 10 || Math.Abs(p.Y) > 10)
                {
                    return;
                }

                Machine.Instance.Robot.MoveSafeZ();
                //Machine.Instance.Robot.MoveIncXY(p);
                Machine.Instance.Robot.ManulMoveIncXY(p);
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.cms.Show(this.picCamera, e.Location);
            }
        }

        private void Executor_BitmapGrabbed(Bitmap arg1)
        {
            if (arg1 == null)
            {
                return;
            }
            //Thread.Sleep(3);
            // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
            Bitmap bitmapOld = picCamera.Image as Bitmap;
            // Provide the display control with the new bitmap. This action automatically updates the display.
            picCamera.Image = arg1;
            if (bitmapOld != null)
            {
                // Dispose the bitmap.
                bitmapOld.Dispose();
                bitmapOld = null;
            }
        }

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if (Machine.Instance.Camera.Executor == null)
        //    {
        //        return;
        //    }
        //    lock (Machine.Instance.Camera)
        //    {
        //        Bitmap bitmapNew = Machine.Instance.Camera.Executor.CurrentBmp;
        //        Bitmap bitmapOld = picCamera.Image as Bitmap;
        //        if (bitmapNew != null && bitmapNew != bitmapOld)
        //        {
        //            picCamera.Image = bitmapNew;
        //            bitmapOld?.Dispose();
        //        }
        //    }
        //}

        private void CbxLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.camera == null)
            {
                return;
            }

            LightType lightType = (LightType)this.cbxLight.SelectedIndex;
            //Machine.Instance.Light.SetLight(lightType);
            if (Machine.Instance.Light.Lighting.lightVendor==Drive.Sensors.Lighting.LightVendor.Anda)
            {
                AndaLight light=Machine.Instance.Light.Lighting as AndaLight;
                light.SetLight(lightType);
            }
            
            Properties.Settings.Default.ligthType = this.cbxLight.SelectedIndex;
            if (this.camera.Prm != null && this.prmBackUp != null)
            {
                CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == STR_SAVE_IMAGE)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "*.jpg|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.camera.Executor.SaveImage(sfd.FileName + ".jpg");
                } 
            }
        }
    }
}
