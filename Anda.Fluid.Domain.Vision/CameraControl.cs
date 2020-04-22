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
using Anda.Fluid.Drive.Sensors.Lighting.Custom;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Lighting;

namespace Anda.Fluid.Domain.Vision
{
    public partial class CameraControl : UserControlEx
    {
        private Camera camera = null;
        private Pen penRedDash;
        private Pen penRed;
        private Pen penGreen = new Pen(Color.Green);
        private Brush brushGreen = new SolidBrush(Color.Green);
        private Rectangle imgRectRoi;
        private Rectangle pbxRectRoi;
        private int shapeIndex = 0;
        private bool hideROI = false;
        private ContextMenuStrip cms;
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private const string STR_SAVE_IMAGE = "Save Image";
        private const string STR_HIDE_CROSS = "Hide Cross";
        private const string STR_MOUSE_FOLLOW = "Mouse Follow";
        private const string STR_RESTORE = "Restore";

        private PointF mouseCenter = new PointF();


        private string light1 = "None";
        private string light2 = "Coax";
        private string light3 = "Ring";
        private string light4 = "Both";

        /// <summary>
        /// Mark或ASV传过来的需要绘制的图形集合
        /// Author:Shawn
        /// Data:2019/11/20
        /// </summary>
        private List<PointD[]> graphics = new List<PointD[]>();

        private bool IsMouseFollow = false;
        private bool IsHideCross = false;

        private CameraPrm prmBackUp;
        public CameraControl()
        {
            InitializeComponent();

            lngResources.Add(STR_SAVE_IMAGE, "Save Image");
            lngResources.Add(STR_MOUSE_FOLLOW, "Mouse Follow");
            lngResources.Add(STR_HIDE_CROSS, "Hide Cross");
            lngResources.Add(STR_RESTORE, "Restore");

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
            this.cms.Items.Add(STR_SAVE_IMAGE);
            this.cms.Items.Add(STR_MOUSE_FOLLOW);
            this.cms.Items.Add(STR_HIDE_CROSS);
            this.cms.Items.Add(STR_RESTORE);
            this.cms.ItemClicked += Cms_ItemClicked;

            this.DoubleBuffered = true;
            if (Machine.Instance.Camera != null)
            {
                this.SetupCamera(Machine.Instance.Camera);
            }
            if (Machine.Instance.Light != null)
            {
                this.SetupLight(Machine.Instance.Light);
            }
            this.nudRaduis.Value = (decimal)Properties.Settings.Default.roiRaduis;

            //切换光源显示为当前光源
            this.cbxLight.SelectedIndex = Properties.Settings.Default.ligthType;

            //this.tbrChn1.ValueChanged += TbrChn1_ValueChanged;
            //this.tbrChn2.ValueChanged += TbrChn2_ValueChanged;
            //this.tbrChn3.ValueChanged += TbrChn3_ValueChanged;
            //this.tbrChn4.ValueChanged += TbrChn4_ValueChanged;
            this.ReadLanguageResources();
            this.UpdateUI();
            this.Disposed += CameraControl_Disposed;
            this.tbrExposure.KeyDown += GainOrExproOrLight_KeyDown;
            this.tbrGain.KeyDown += GainOrExproOrLight_KeyDown;
            this.cbxLight.KeyDown += GainOrExproOrLight_KeyDown;
            this.nudRaduis.KeyDown += GainOrExproOrLight_KeyDown;
        }

        private void CameraControl_Disposed(object sender, EventArgs e)
        {
            this.Dispose(true);
            this.btnShape.Image.Dispose();
        }

        ~CameraControl()
        {
            int i = 0;
        }

        public Camera Cam => this.camera;

        public int ExposureTime => (int)this.tbrExposure.Value;

        public int Gain => (int)this.tbrGain.Value;

        //public LightType Lighting => (LightType)this.cbxLight.SelectedIndex;

        public ExecutePrm ExecutePrm = null;

        private Light light = null;

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

        public CameraControl SetupCamera(Camera camera)
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
            if (!this.DesignMode)
            {
                if (SensorMgr.Instance.Light.Vendor == Drive.Sensors.Lighting.LightVendor.Anda)
                {
                    this.cbxLight.Enabled = true;
                    //this.gpbChanel.Enabled = false;
                }
                else if (SensorMgr.Instance.Light.Vendor == Drive.Sensors.Lighting.LightVendor.Custom)
                {
                    this.cbxLight.Enabled = false;
                    //this.gpbChanel.Enabled = true;
                }

            }
            if (this.camera.Prm != null)
            {
                this.prmBackUp = (CameraPrm)this.camera.Prm.Clone();
            }
            return this;
        }

        public CameraControl SetupLight(Light light)
        {
            this.light = light;
            this.ExecutePrm = this.light.ExecutePrm;
            this.setLight(this.light.ExecutePrm);
            return this;
        }
        public void setLight(ExecutePrm executePrm)
        {
            this.cbxLight.SelectedIndex = (int)this.light.ExecutePrm.LightType;

            //if (this.light.ExecutePrm.PrmOPT.ChanelDic != null)
            //{
            //    this.ckbChn1.Checked = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel1].On;
            //    this.tbrChn1.Value = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel1].Value;
            //    this.ckbChn2.Checked = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel2].On;
            //    this.tbrChn2.Value = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel2].Value;
            //    this.ckbChn3.Checked = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel3].On;
            //    this.tbrChn3.Value = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel3].Value;
            //    this.ckbChn4.Checked = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel4].On;
            //    this.tbrChn4.Value = this.light.ExecutePrm.PrmOPT.ChanelDic[Chanels.Chanel4].Value;
            //}
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
            if (this.HasLngResources())
            {
                lngResources[STR_SAVE_IMAGE] = this.ReadKeyValueFromResources(STR_SAVE_IMAGE);
                lngResources[STR_MOUSE_FOLLOW] = this.ReadKeyValueFromResources(STR_MOUSE_FOLLOW);
                lngResources[STR_HIDE_CROSS] = this.ReadKeyValueFromResources(STR_HIDE_CROSS);
                lngResources[STR_RESTORE] = this.ReadKeyValueFromResources(STR_RESTORE);
            }
            this.cms.Items[0].Text = lngResources[STR_SAVE_IMAGE];
            this.cms.Items[1].Text = lngResources[STR_MOUSE_FOLLOW];
            this.cms.Items[2].Text = lngResources[STR_HIDE_CROSS];
            this.cms.Items[3].Text = lngResources[STR_RESTORE];
        }
        public void UpdateUI()
        {
            //切换一次数值，触发主界面相机控件重绘
            this.nudRaduis.Value = (decimal)Properties.Settings.Default.roiRaduis;
            this.shapeIndex = Properties.Settings.Default.roiType;
            this.ReadLanguageResources();
        }

        public void SwitchSizeMode()
        {
            if (this.picCamera.SizeMode == PictureBoxSizeMode.Zoom)
            {
                this.picCamera.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                this.picCamera.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public CameraControl SetExposure(int exposureTime)
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

        public CameraControl SetGain(int gain)
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
        //public void SelectLight(LightType lightType)
        //{
        //    //Machine.Instance.Light.SetLight(lightType);
        //    if (Machine.Instance.Light.Lighting.lightVendor==Drive.Sensors.Lighting.LightVendor.Anda)
        //    {
        //        AndaLight light=Machine.Instance.Light.Lighting as AndaLight;
        //        light.SetLight(lightType);
        //    }
        //    this.cbxLight.SelectedIndex = (int)lightType;
        //}
        public void SelectLight(ExecutePrm executePrm)
        {
            Machine.Instance.Light.SetLight(executePrm);
            //this.cbxLight.SelectedIndex = (int)executePrm.LightType;
            this.setLight(executePrm);
        }

        /// <summary>
        /// 显示图形,数组长度确定图形样式（长度1-点、2-线、3-圆弧/圆(起点和终点相同)、4-矩形）。
        /// <param name="graph">graph:要绘制的图形组成点数组。</param>
        /// <example>axample:
        /// 圆弧：graph[0]=圆弧起点,graph[1]=圆弧中间点,graph[2]=圆弧终点;
        /// 圆：graph[0]=圆上任一点,graph[1]=圆心,graph[2]=graph[0];
        /// 矩形：graph[0]=左上点,graph[1]=左下点,graph[2]=右上点,graph[3]=右下点。</example>
        /// Author:Shawn
        /// Data:2019/11/20
        /// </summary>
        public void ShowGraph(PointD[] graph)
        {
            this.graphics.Add(graph);
            this.picCamera.Invalidate();
        }

        /// <summary>
        /// 清空相机空间图形显示
        /// Autor:Shawn
        /// Data:2019/11/20
        /// </summary>
        public void ClearGraph()
        {
            this.graphics.Clear();
            this.picCamera.Invalidate();
        }

        /// <summary>
        /// 绘制Mark或者ASV传递过来的图形
        /// Autor:Shawn
        /// Data:2019/11/20
        /// </summary>
        private void DrawGraphics(PaintEventArgs e)
        {
            foreach (var item in this.graphics)
            {
                //点
                if (item.Length == 1)
                {
                    this.DrawPoint(e, item);
                }
                //线
                else if (item.Length == 2)
                {
                    this.DrawLine(e, item);
                }
                //圆弧
                else if (item.Length == 3 && !item[0].Equals(item[2]))
                {
                    this.DrawArc(e, item);
                }
                //圆
                else if (item.Length == 3 && item[0].Equals(item[2]))
                {
                    this.DrawCircle(e, item);
                }
                //矩形
                else if (item.Length == 4)
                {
                    this.DrawRect(e, item);
                }
            }
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
            Properties.Settings.Default.Save();
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
            switch (this.shapeIndex)
            {
                case 0:
                    this.shapeIndex = 1;
                    break;
                case 1:
                    this.shapeIndex = 0;
                    break;
            }
            Properties.Settings.Default.roiType = this.shapeIndex;
            Properties.Settings.Default.Save();
            this.UpdateBtnShape();
            this.picCamera.Invalidate();
        }

        private void PicCamera_Paint(object sender, PaintEventArgs e)
        {
            if (!this.IsHideCross)
            {
                this.drawBaseLines(e);
            }
            if (!this.IsMouseFollow)
            {
                if (this.hideROI)
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

            this.DrawGraphics(e);
        }

        private void drawBaseLines(PaintEventArgs e)
        {
            if (this.IsMouseFollow)
            {
                e.Graphics.DrawLine(penRed, 0, this.mouseCenter.Y, this.picCamera.Width, this.mouseCenter.Y);
                e.Graphics.DrawLine(penRed, this.mouseCenter.X, 0, this.mouseCenter.X, this.picCamera.Height);
                if (Machine.Instance.Robot == null)
                {
                    return;
                }
                double pixesPerUnit = (0.1 / Machine.Instance.Robot.CalibBy9dPrm.Scale);
                if (pixesPerUnit <= 0)
                {
                    return;
                }
                int count_x = (int)((this.ImgWidth - this.mouseCenter.X) / pixesPerUnit);
                int count_y = (int)((this.ImgHeight - this.mouseCenter.Y) / pixesPerUnit);
                for (int i = 1; i <= count_x; i++)
                {
                    int x = (int)(this.mouseCenter.X + pixesPerUnit * i);
                    int y = (int)this.mouseCenter.Y;
                    int h = 5; if (i % 10 == 0) { h = 15; } else if (i % 5 == 0) { h = 10; }
                    e.Graphics.DrawLine(penRed, convertPixToControl(x, y + h), convertPixToControl(x, y - h));
                    x = (int)(this.mouseCenter.X - pixesPerUnit * i);
                    e.Graphics.DrawLine(penRed, convertPixToControl(x, y + h), convertPixToControl(x, y - h));
                }

                for (int i = 1; i <= count_y; i++)
                {
                    int x = (int)this.mouseCenter.X;
                    int y = (int)(this.mouseCenter.Y + pixesPerUnit * i);
                    int h = 5; if (i % 10 == 0) { h = 15; } else if (i % 5 == 0) { h = 10; }
                    e.Graphics.DrawLine(penRed, convertPixToControl(x + h, y), convertPixToControl(x - h, y));
                    y = (int)(this.mouseCenter.Y - pixesPerUnit * i);
                    e.Graphics.DrawLine(penRed, convertPixToControl(x + h, y), convertPixToControl(x - h, y));
                }
            }
            else
            {
                e.Graphics.DrawLine(penRed, 0, this.picCamera.Height / 2, this.picCamera.Width, this.picCamera.Height / 2);
                e.Graphics.DrawLine(penRed, this.picCamera.Width / 2, 0, this.picCamera.Width / 2, this.picCamera.Height);
                if (Machine.Instance.Robot == null)
                {
                    return;
                }
                double pixesPerUnit = (0.1 / Machine.Instance.Robot.CalibBy9dPrm.Scale);
                if (pixesPerUnit <= 0)
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


        }

        private Point convertPixToControl(int xp, int yp)
        {
            if (IsMouseFollow)
            {
                int x = (int)this.mouseCenter.X + (int)((xp - this.mouseCenter.X) / this.Radio);
                int y = (int)this.mouseCenter.Y + (int)((yp - this.mouseCenter.Y) / this.Radio);
                return new Point(x, y);
            }
            else
            {
                int x = this.PbxCenter.X + (int)((xp - this.ImgCenter.X) / this.Radio);
                int y = this.PbxCenter.Y + (int)((yp - this.ImgCenter.Y) / this.Radio);
                return new Point(x, y);
            }

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
                if (Machine.Instance.IsBusy)
                {
                    return;
                }

                double imgX = 0, imgY = 0;
                //double phyX = 0, phyY = 0;

                imgX = this.Radio * (this.PbxImgWidth / 2 - (this.picCamera.Width / 2 - e.X));
                imgY = this.Radio * (this.PbxImgHeight / 2 - (this.picCamera.Height / 2 - e.Y));

                if (imgX < 0 || imgY < 0)
                {
                    return;
                }

                PointD p = Machine.Instance.Camera.ToMachine(imgX, imgY);
                //CalibBy9d.ConvertImg2Phy(ref imgX, ref imgY, ref phyX, ref phyY);
                if (Math.Abs(p.X) > 100 || Math.Abs(p.Y) > 100)
                {
                    return;
                }

                Machine.Instance.Robot.MoveSafeZ();
                //Machine.Instance.Robot.MoveIncXY(p);
                Machine.Instance.Robot.ManulMoveIncXY(p);
            }
            else if (e.Button == MouseButtons.Right)
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
            // 只需更新引用，原引用对象在其他地方释放
            picCamera.Image = arg1;
        }

        private void CbxLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.camera == null)
            {
                return;
            }

            //LightType lightType = (LightType)this.cbxLight.SelectedIndex;
            ////Machine.Instance.Light.SetLight(lightType);
            //if (Machine.Instance.Light.Lighting.lightVendor==Drive.Sensors.Lighting.LightVendor.Anda)
            //{
            //    AndaLight light=Machine.Instance.Light.Lighting as AndaLight;
            //    light.SetLight(lightType);
            //}
            this.ExecutePrm.LightType = (LightType)this.cbxLight.SelectedIndex;
            Machine.Instance.Light.SetLight(this.ExecutePrm);

            Properties.Settings.Default.ligthType = this.cbxLight.SelectedIndex;
            if (this.camera.Prm != null && this.prmBackUp != null)
            {
                CompareObj.CompareProperty(this.camera.Prm, this.prmBackUp, null, this.GetType().Name);
            }
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == lngResources[STR_SAVE_IMAGE])
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "*.jpg|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.camera.Executor.SaveImage(sfd.FileName + ".jpg");
                }
            }
            if (e.ClickedItem.Text == lngResources[STR_HIDE_CROSS])
            {
                this.IsHideCross = true;
                this.picCamera.Invalidate();
            }
            if (e.ClickedItem.Text == lngResources[STR_MOUSE_FOLLOW])
            {
                this.IsHideCross = false;
                this.IsMouseFollow = true;
                this.picCamera.Invalidate();
            }
            if (e.ClickedItem.Text == lngResources[STR_RESTORE])
            {
                this.IsHideCross = false;
                this.IsMouseFollow = false;
                this.picCamera.Invalidate();
            }

        }

        #region  chanleSetting
        //private void ckbChn1_CheckedChanged(object sender, EventArgs e)
        //{            
        //    this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel1, this.ckbChn1.Checked, this.tbrChn1.Value);
        //    //LightCustom light=Machine.Instance.Light.Lighting as LightCustom;
        //    //light.SetLight(this.executePrm);
        //    Machine.Instance.Light.SetLight(this.ExecutePrm);
        //}

        //private void ckbChn2_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel2, this.ckbChn2.Checked, this.tbrChn2.Value);
        //    //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //    //light.SetLight(this.executePrm);
        //    Machine.Instance.Light.SetLight(this.ExecutePrm);
        //}

        //private void ckbChn3_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel3, this.ckbChn3.Checked, this.tbrChn3.Value);
        //    //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //    //light.SetLight(this.executePrm);
        //    Machine.Instance.Light.SetLight(this.ExecutePrm);
        //}

        //private void ckbChn4_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel4, this.ckbChn4.Checked, this.tbrChn4.Value);
        //    //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //    //light.SetLight(this.executePrm);
        //    Machine.Instance.Light.SetLight(this.ExecutePrm);
        //}

        //private void TbrChn4_ValueChanged(object sender, EventArgs e)
        //{
        //    this.lblChn4.Text = this.tbrChn4.Value.ToString();
        //    if (this.ckbChn4.Checked)
        //    {
        //        this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel4, this.ckbChn4.Checked, this.tbrChn4.Value);
        //        //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //        //light.SetLight(this.executePrm);
        //        Machine.Instance.Light.SetLight(this.ExecutePrm);
        //    }

        //}

        //private void TbrChn3_ValueChanged(object sender, EventArgs e)
        //{
        //    this.lblChn3.Text = this.tbrChn3.Value.ToString();
        //    if (this.ckbChn3.Checked)
        //    {
        //        this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel3, this.ckbChn3.Checked, this.tbrChn3.Value);
        //        //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //        //light.SetLight(this.executePrm);
        //        Machine.Instance.Light.SetLight(this.ExecutePrm);
        //    }
        //    this.lblChn1.Text = this.tbrChn1.Value.ToString();
        //}

        //private void TbrChn2_ValueChanged(object sender, EventArgs e)
        //{
        //    this.lblChn2.Text = this.tbrChn2.Value.ToString();
        //    if (this.ckbChn2.Checked)
        //    {
        //        this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel2, this.ckbChn2.Checked, this.tbrChn2.Value);
        //        //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //        //light.SetLight(this.ExecutePrm);
        //        Machine.Instance.Light.SetLight(this.ExecutePrm);
        //    }

        //}

        //private void TbrChn1_ValueChanged(object sender, EventArgs e)
        //{
        //    this.lblChn1.Text = this.tbrChn1.Value.ToString();
        //    if (this.ckbChn1.Checked)
        //    {
        //        this.ExecutePrm.PrmOPT.SetChanel(Chanels.Chanel1, this.ckbChn1.Checked, this.tbrChn1.Value);
        //        //LightCustom light = Machine.Instance.Light.Lighting as LightCustom;
        //        //light.SetLight(this.executePrm);
        //        Machine.Instance.Light.SetLight(this.ExecutePrm);
        //    }


        //}
        #endregion
        private void CameraControl_Load(object sender, EventArgs e)
        {
            //this.gpbChanel.Width = this.Width;

        }

        #region 绘制各式图形
        /// <summary>
        /// 绘制点
        /// </summary>
        private void DrawPoint(PaintEventArgs e, PointD[] point)
        {
            //点的中心坐标
            PointF center = new PointF();
            center.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - point[0].X) / this.Radio);
            center.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - point[0].Y) / this.Radio);

            //点半径
            float radius = this.picCamera.Height / 50;

            //点外接四边形的左上点坐标(当作圆来处理)
            PointF panelPosition = new PointF(center.X - radius, center.Y - radius);

            //点外接四边形尺寸
            SizeF arcSize = new SizeF(radius * 2, radius * 2);
            RectangleF rect = new RectangleF(panelPosition, arcSize);

            //执行绘图
            e.Graphics.FillEllipse(this.brushGreen, rect);
        }

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="e"></param>
        /// <param name="line"></param>
        private void DrawLine(PaintEventArgs e, PointD[] line)
        {
            PointF lineStart = new PointF();
            PointF lineEnd = new PointF();

            lineStart.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - line[0].X) / this.Radio);
            lineStart.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - line[0].Y) / this.Radio);

            lineEnd.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - line[1].X) / this.Radio);
            lineEnd.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - line[1].Y) / this.Radio);

            e.Graphics.DrawLine(penGreen, lineStart, lineEnd);
        }

        /// <summary>
        /// 绘制圆弧
        /// </summary>
        /// <param name="e"></param>
        /// <param name="arc"></param>
        private void DrawArc(PaintEventArgs e, PointD[] arc)
        {
            //未转换前的圆弧中心
            PointD center = MathUtils.CalculateCircleCenter(arc[0], arc[1], arc[2]);
            //圆弧的角度
            double degree = MathUtils.CalculateDegree(arc[0], arc[1], arc[2], center);

            //圆弧的中心坐标
            PointF centerPosition = new PointF();
            centerPosition.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - center.X) / this.Radio);
            centerPosition.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - center.Y) / this.Radio);

            //圆弧的起点坐标
            PointF startPosition = new PointF();
            startPosition.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - arc[0].X) / this.Radio);
            startPosition.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - arc[0].Y) / this.Radio);

            //圆弧半径
            float radius = (float)Math.Sqrt(Math.Pow((double)startPosition.X - (double)centerPosition.X, 2) + Math.Pow((double)startPosition.Y - (double)centerPosition.Y, 2));

            //圆弧外接四边形的左上点坐标
            PointF panelPosition = new PointF(centerPosition.X - radius, centerPosition.Y - radius);

            //圆外接四边形尺寸
            SizeF arcSize = new SizeF(radius * 2, radius * 2);
            RectangleF rect = new RectangleF(panelPosition, arcSize);

            //圆弧的起点度数
            float startDegree = MathUtils.CalculateStartDegreeOnArc(startPosition, centerPosition);

            //圆弧的角度
            float sweepDegree = -(float)degree;

            //执行绘图
            e.Graphics.DrawArc(penGreen, rect, startDegree, sweepDegree);
        }

        /// <summary>
        /// 绘制圆环
        /// </summary>
        /// <param name="e"></param>
        /// <param name="circle"></param>
        private void DrawCircle(PaintEventArgs e, PointD[] circle)
        {
            //圆心坐标
            PointF centerPosition = new PointF();
            centerPosition.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - circle[1].X) / this.Radio);
            centerPosition.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - circle[1].Y) / this.Radio);

            //圆的起点坐标
            PointF startPosition = new PointF();
            startPosition.X = (float)(this.PbxCenter.X - (this.ImgCenter.X - circle[0].X) / this.Radio);
            startPosition.Y = (float)(this.PbxCenter.Y - (this.ImgCenter.Y - circle[0].Y) / this.Radio);

            //圆半径
            float radius = (float)Math.Sqrt(Math.Pow((double)startPosition.X - (double)centerPosition.X, 2)
                + Math.Pow((double)startPosition.Y - (double)centerPosition.Y, 2));

            //圆外接四边形的左上点坐标
            PointF panelPosition = new PointF(centerPosition.X - radius, centerPosition.Y - radius);

            //圆外接四边形尺寸
            SizeF circleSize = new SizeF(radius * 2, radius * 2);
            RectangleF rect = new RectangleF(panelPosition, circleSize);

            //执行绘图
            e.Graphics.DrawArc(this.penGreen, rect, 0, 360);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        private void DrawRect(PaintEventArgs e, PointD[] rect)
        {
            //左上点坐标
            Point upLeft = new Point();
            upLeft.X = (int)(this.PbxCenter.X - (this.ImgCenter.X - rect[0].X) / this.Radio);
            upLeft.Y = (int)(this.PbxCenter.Y - (this.ImgCenter.Y - rect[0].Y) / this.Radio);

            //左下点坐标
            Point downLeft = new Point();
            downLeft.X = (int)(this.PbxCenter.X - (this.ImgCenter.X - rect[1].X) / this.Radio);
            downLeft.Y = (int)(this.PbxCenter.Y - (this.ImgCenter.Y - rect[1].Y) / this.Radio);

            //右上点坐标
            Point upRight = new Point();
            upRight.X = (int)(this.PbxCenter.X - (this.ImgCenter.X - rect[2].X) / this.Radio);
            upRight.Y = (int)(this.PbxCenter.Y - (this.ImgCenter.Y - rect[2].Y) / this.Radio);

            //矩形尺寸
            Size size = new Size();
            size.Width = upRight.X - upLeft.X;
            size.Height = downLeft.Y - upLeft.Y;

            Rectangle rectf = new Rectangle(upLeft, size);

            //绘制图形
            e.Graphics.DrawRectangle(this.penGreen, rectf);
        }

        #endregion

        private void Picamera_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseCenter.X = e.X;
            this.mouseCenter.Y = e.Y;
            this.picCamera.Invalidate();
        }

        private void Picamera_MouseLeave(object sender, EventArgs e)
        {
            this.IsMouseFollow = false;
            this.picCamera.Invalidate();
        }

        private void GainOrExproOrLight_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
