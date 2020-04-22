using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive.Vision.GrayFind;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditMark
{
    public partial class EditGrayCheckForm : DialogBase, IOptional
    {
        private int flag = 0;
        private Pen penDash, penViolet, penGreen;
        private Rectangle pbxRoiRect = Rectangle.Empty;
        private Rectangle pbxSearchRect = Rectangle.Empty;
        private Camera camera;
        private Light light;
        private Pattern pattern;
        private GrayCheckPrm grayCheckPrm;
        private GrayCheckPrm grayCheckPrmBackUP;

        public EditGrayCheckForm(GrayCheckPrm grayCheckPrm, Pattern pattern) : base(pattern == null ? new PointD(0, 0) : pattern.GetOriginPos())
        {
            InitializeComponent();
            this.Init();
            this.UpdateByFlag();
            this.camera = Machine.Instance.Camera;
            this.light = Machine.Instance.Light;
            if (grayCheckPrm == null)
            {
                grayCheckPrm = new GrayCheckPrm();
            }
            this.grayCheckPrm = grayCheckPrm;
            this.grayCheckPrm.ExposureTime = this.camera.Prm.Exposure;
            this.grayCheckPrm.Gain = this.camera.Prm.Gain;
            //this.grayCheckPrm.LightType = this.camera.Prm.LightType;
            this.grayCheckPrm.ExecutePrm = (ExecutePrm)this.light.ExecutePrm.Clone();
            //this.grayCheckPrm.ImgWidth = this.camera.Executor.ImageWidth;
            //this.grayCheckPrm.ImgHeight = this.camera.Executor.ImageHeight;
            this.Setup(this.grayCheckPrm);
            this.pattern = pattern;
            if(this.grayCheckPrm!=null)
            this.grayCheckPrmBackUP = (GrayCheckPrm)this.grayCheckPrm.Clone();
          
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditGrayCheckForm()
        {
            InitializeComponent();
        }

        private void Init()
        {
            this.picModelImage.Hide();
            this.CamCtrl.Pbx.Paint += imageBox_Paint;

            this.penDash = new Pen(Color.Red, 1);
            this.penDash.DashStyle = DashStyle.Custom;
            this.penDash.DashPattern = new float[] { 3f, 2f };
            this.penViolet = new Pen(Color.Violet, 1);
            this.penGreen = new Pen(Color.LightGreen, 1);

            this.nudModelSizeWidth.Minimum = 0;
            this.nudModelSizeWidth.Increment = 6;
            this.nudModelSizeWidth.ValueChanged += nudModelSizeWidth_ValueChanged;

            this.nudModelSizeHeight.Minimum = 0;
            this.nudModelSizeHeight.Increment = 6;
            this.nudModelSizeHeight.ValueChanged += nudModelSizeHeight_ValueChanged;

            this.nudSearchWindowWidth.Minimum = 0;
            this.nudSearchWindowWidth.Increment = 6;
            this.nudSearchWindowWidth.ValueChanged += nudSearchWindowWidth_ValueChanged;

            this.nudSearchWindowHeight.Minimum = 0;
            this.nudSearchWindowHeight.Increment = 6;
            this.nudSearchWindowHeight.ValueChanged += nudSearchWindowHeight_ValueChanged;

            this.nudGrayTolerance.Minimum = 0;
            this.nudGrayTolerance.Maximum = 255;

            this.nudSettlingTime.Minimum = 0;
            this.nudSettlingTime.Maximum = 5000;
        }

        private void Setup(GrayCheckPrm grayCheckPrm)
        {
            if (!grayCheckPrm.IsCreated)
            {
                grayCheckPrm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - 50;
                grayCheckPrm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - 50;
                grayCheckPrm.ModelWidth = 100;
                grayCheckPrm.ModelHeight = 100;
                grayCheckPrm.SearchTopLeftX = 100;
                grayCheckPrm.SearchTopLeftY = 100;
                grayCheckPrm.SearchWidth = this.CamCtrl.ImgWidth - 200;
                grayCheckPrm.SearchHeight = this.CamCtrl.ImgHeight - 200;
                grayCheckPrm.SettlingTime = 50;
                grayCheckPrm.AcceptTolerance = 20;
            }

            this.CamCtrl.HideROI();
            this.CamCtrl.SetExposure(this.grayCheckPrm.ExposureTime).SetGain(this.grayCheckPrm.Gain);
            //this.CamCtrl.SelectLight(this.grayCheckPrm.LightType);
            this.CamCtrl.SelectLight(this.grayCheckPrm.ExecutePrm);
            this.SetupLight(this.grayCheckPrm.ExecutePrm);

            this.nudModelSizeWidth.Maximum = this.camera.Executor.ImageWidth;
            this.nudModelSizeHeight.Maximum = this.camera.Executor.ImageHeight;
            this.nudSearchWindowWidth.Maximum = this.camera.Executor.ImageWidth;
            this.nudSearchWindowHeight.Maximum = this.camera.Executor.ImageHeight;

            this.nudSearchWindowWidth.Value = MathUtils.Limit(this.grayCheckPrm.SearchWidth, 0, this.camera.Executor.ImageWidth);
            this.nudSearchWindowHeight.Value = MathUtils.Limit(this.grayCheckPrm.SearchHeight, 0, this.camera.Executor.ImageHeight);
            this.nudModelSizeWidth.Value = MathUtils.Limit(this.grayCheckPrm.ModelWidth, 0, this.camera.Executor.ImageWidth);
            this.nudModelSizeHeight.Value = MathUtils.Limit(this.grayCheckPrm.ModelHeight, 0, this.camera.Executor.ImageHeight);
            this.nudSettlingTime.Value = MathUtils.Limit(this.grayCheckPrm.SettlingTime, 0, (int)this.nudSettlingTime.Maximum);
            this.nudGrayTolerance.Value = MathUtils.Limit(this.grayCheckPrm.AcceptTolerance, 0, (int)this.nudGrayTolerance.Maximum);

            
        }

        public void DoCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void DoDone()
        {
            this.grayCheckPrm.SettlingTime = (int)this.nudSettlingTime.Value;
            this.grayCheckPrm.AcceptTolerance = (int)this.nudGrayTolerance.Value;
            this.grayCheckPrm.IsCreated = true;
            CompareObj.CompareProperty(this.grayCheckPrm, this.grayCheckPrmBackUP, true);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoNext()
        {
            this.flag++;
            this.UpdateByFlag();
        }

        public void DoPrev()
        {
            this.flag--;
            this.UpdateByFlag();
            this.CamCtrl.Pbx.Invalidate();
        }

        public void DoTeach()
        {
            CreateGrayModel();
            this.grayCheckPrm.PosInPattern = pattern.SystemRel(Machine.Instance.Robot.PosX - pattern.GetOriginPos().X, Machine.Instance.Robot.PosY - pattern.GetOriginPos().Y);
            this.grayCheckPrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.grayCheckPrm.Gain = this.cameraControl1.Gain;
            //this.grayCheckPrm.LightType = Machine.Instance.Light.CurrType;
            this.grayCheckPrm.ExecutePrm = (ExecutePrm)Machine.Instance.Light.ExecutePrm.Clone();
            this.grayCheckPrm.SettlingTime = (int)this.nudSettlingTime.Value;
            this.grayCheckPrm.AcceptTolerance = (int)this.nudGrayTolerance.Value;
        }

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                pbxRoiRect.Width = (int)(grayCheckPrm.ModelWidth / this.CamCtrl.Radio);
                pbxRoiRect.Height = (int)(grayCheckPrm.ModelHeight / this.CamCtrl.Radio);

                pbxRoiRect.X = this.CamCtrl.PbxCenter.X - (int)(grayCheckPrm.ModelWidth / 2 / this.CamCtrl.Radio);
                pbxRoiRect.Y = this.CamCtrl.PbxCenter.Y - (int)(grayCheckPrm.ModelHeight / 2 / this.CamCtrl.Radio);
                e.Graphics.DrawRectangle(this.penGreen, pbxRoiRect);

                pbxSearchRect.Width = (int)(grayCheckPrm.SearchWidth / this.CamCtrl.Radio);
                pbxSearchRect.Height = (int)(grayCheckPrm.SearchHeight / this.CamCtrl.Radio);
                pbxSearchRect.X = this.CamCtrl.PbxCenter.X - (int)(grayCheckPrm.SearchWidth / 2 / this.CamCtrl.Radio);
                pbxSearchRect.Y = this.CamCtrl.PbxCenter.Y - (int)(grayCheckPrm.SearchHeight / 2 / this.CamCtrl.Radio);
                e.Graphics.DrawRectangle(this.penViolet, pbxSearchRect);
            }
            catch (Exception)
            {

            }
        }

        private void nudModelSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudModelSizeWidth.Value > this.nudSearchWindowWidth.Value - 8)
            {
                this.nudModelSizeWidth.Value = this.grayCheckPrm.ModelWidth;
                return;
            }
            grayCheckPrm.ModelWidth = (int)this.nudModelSizeWidth.Value;
            grayCheckPrm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - grayCheckPrm.ModelWidth / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudModelSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudModelSizeHeight.Value > this.nudSearchWindowHeight.Value - 8)
            {
                this.nudModelSizeHeight.Value = grayCheckPrm.ModelHeight;
                return;
            }
            grayCheckPrm.ModelHeight = (int)this.nudModelSizeHeight.Value;
            grayCheckPrm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - grayCheckPrm.ModelHeight / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudSearchWindowWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudSearchWindowWidth.Value < this.nudModelSizeWidth.Value + 8)
            {
                this.nudSearchWindowWidth.Value = grayCheckPrm.SearchWidth;
                return;
            }
            grayCheckPrm.SearchWidth = (int)this.nudSearchWindowWidth.Value;
            grayCheckPrm.SearchTopLeftX = this.CamCtrl.ImgCenter.X - grayCheckPrm.SearchWidth / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudSearchWindowHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudSearchWindowHeight.Value < this.nudModelSizeHeight.Value + 8)
            {
                this.nudSearchWindowHeight.Value = grayCheckPrm.SearchHeight;
                return;
            }
            grayCheckPrm.SearchHeight = (int)this.nudSearchWindowHeight.Value;
            grayCheckPrm.SearchTopLeftY = this.CamCtrl.ImgCenter.Y - grayCheckPrm.SearchHeight / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (this.camera == null)
            {
                return;
            }
            if (this.camera.Executor.CurrentBytes == null)
            {
                return;
            }
            if (this.rbFindInPlace.Checked)
            {
                this.CheckTest();
                this.flag = 2;
                UpdateByFlag();
            }
            if (this.rbFindAtTaught.Checked)
            {
                Task.Factory.StartNew(() =>
                {
                    //move to mark pos
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(pattern.MachineAbs(grayCheckPrm.PosInPattern));
                    Thread.Sleep(100);

                    this.BeginInvoke(new Action(() =>
                    {
                        this.CheckTest();
                        this.flag = 2;
                        UpdateByFlag();
                    }));
                });
            }
        }

        private void btnShowModel_Click(object sender, EventArgs e)
        {
            if (this.btnShowModel.FlatStyle == FlatStyle.Standard)
            {
                this.picModelImage.Width = (int)(grayCheckPrm.ModelWidth / this.CamCtrl.Radio) * 2;
                this.picModelImage.Height = (int)(grayCheckPrm.ModelHeight / this.CamCtrl.Radio) * 2;
                this.picModelImage.Show();
                this.picModelImage.Image = this.BytesToBmp(grayCheckPrm.ModelData,
                    grayCheckPrm.ModelWidth, grayCheckPrm.ModelHeight);
                this.btnShowModel.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                this.picModelImage.Hide();
                this.btnShowModel.FlatStyle = FlatStyle.Standard;
            }
        }

        private void UpdateByFlag()
        {
            switch (this.flag)
            {
                case 0:
                    this.LblTitle.Text = "调节灰度检测参数,点击[示教]。";
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = true;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.tab.TabPages.Clear();
                    this.tab.TabPages.Add(this.tbgModel);
                    this.picModelImage.Hide();
                    this.btnShowModel.FlatStyle = FlatStyle.Standard;
                    this.tbStatus.Text = "";
                    this.tbTime.Text = "";
                    this.ReadLanguageResources();
                    break;
                case 1:
                    this.LblTitle.Text = "检测结果或者点击[完成]。";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.tab.TabPages.Clear();
                    this.tab.TabPages.Add(this.tbgTest);
                    this.tbStatus.Text = "";
                    this.tbTime.Text = "";
                    this.ReadLanguageResources();
                    break;
                case 2:
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.tab.TabPages.Clear();
                    this.tab.TabPages.Add(this.tbgTest);
                    this.ReadLanguageResources();
                    break;
            }
        }

        private int CreateGrayModel()
        {
            if (this.camera == null)
            {
                return -1;
            }

            byte[] bytes = this.camera.Executor.CurrentBytes;
            if (bytes == null)
            {
                return -1;
            }

            byte[] roiImageData = new byte[grayCheckPrm.ModelWidth * grayCheckPrm.ModelHeight];
            int rtn = ModelFindThm.GetRoiImageData(bytes, CamCtrl.ImgWidth, CamCtrl.ImgHeight, 
                grayCheckPrm.ModelTopLeftX,grayCheckPrm.ModelTopLeftY, grayCheckPrm.ModelWidth, grayCheckPrm.ModelHeight, roiImageData);
            if (rtn != 0) return rtn;
            grayCheckPrm.ModelData = roiImageData;
            return rtn;
        }

        private void btnGotoTaught_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            PointD pos = pattern.MachineAbs(grayCheckPrm.PosInPattern);
            Machine.Instance.Robot.ManualMovePosXY(pos.X, pos.Y);
        }

        private Bitmap BytesToBmp(byte[] data, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpdata = bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int stride = bmpdata.Stride;
            int offset = stride - width;
            IntPtr iptr = bmpdata.Scan0;
            int scanBytes = stride * height;

            int posScan = 0, posReal = 0;
            byte[] pixelValues = new byte[scanBytes];
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = data[posReal++];
                }
                posScan += offset;
            }

            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpdata);

            ColorPalette tempPalette;
            using (Bitmap tempbmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempbmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = tempPalette;

            return bmp;
        }

        private void CheckTest()
        {
            DateTime beforeDT = System.DateTime.Now;
            this.grayCheckPrm.PosInMachine = new PointD(Machine.Instance.Robot.PosXY);
            bool foundState = false;
            this.tbTime.Text = string.Empty;
            //获取当前检测ROI
            byte[] bytes = this.camera.Executor.CurrentBytes;
            if (bytes == null)
            {
                return;
            }
            byte[] roiImageData = grayCheckPrm.GetROI(bytes, this.camera.Executor.ImageWidth, this.camera.Executor.ImageHeight);

            grayCheckPrm.CheckData = roiImageData;
            grayCheckPrm.CheckWidth = grayCheckPrm.ModelWidth;
            grayCheckPrm.CheckHeight = grayCheckPrm.ModelHeight;
            
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            if (grayCheckPrm.Execute())
            {
                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforeDT);
                this.tbTime.Text = string.Format("{0}ms", ts.TotalMilliseconds);
                foundState = true;
            }
            else
            {
                foundState = false;
            }

            if (!foundState)
            {
                this.tbStatus.Text = "UnFound";
                this.tbStatus.BackColor = Color.Red;
            }
            else
            {
                this.tbStatus.BackColor = Color.Green;
                this.tbStatus.Text = "Found";
            }

        }
    }
}
