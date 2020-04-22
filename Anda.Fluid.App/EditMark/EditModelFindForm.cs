using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Vision.CameraFramework;
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
    public partial class EditModelFindForm : DialogBase, IOptional
    {
        private int flag = 0;
        private Pen penDash, penViolet, penGreen;
        private Rectangle pbxRoiRect = Rectangle.Empty;
        private Rectangle pbxSearchRect = Rectangle.Empty;
        private Camera camera;
        private Light light;
        private bool isManual; //手动指定Mark
        private ModelFindPrm modelFindPrm;
        private Pattern pattern;
        private ModelFindPrm modelFindPrmBackUp;

        public EditModelFindForm(ModelFindPrm modelFindPrm, Pattern pattern, bool isManual = false) 
            : base( pattern==null ? new PointD(0,0): pattern.GetOriginPos())
        {
            InitializeComponent();
            this.Init();
            this.UpdateByFlag();
            this.camera = Machine.Instance.Camera;
            this.light = Machine.Instance.Light;
            if (modelFindPrm == null)
            {
                modelFindPrm = new ModelFindPrm();
            }
            else
            {
                if (modelFindPrm.ExecutePrm.PrmOPT.ListCls.Count == 0)
                {
                    modelFindPrm.ExecutePrm.PrmOPT = this.light.ExecutePrm.PrmOPT;
                }
                this.light.SetLight(modelFindPrm.ExecutePrm);
                this.SetupLight(modelFindPrm.ExecutePrm);
            }
            this.modelFindPrm = modelFindPrm;
            this.modelFindPrm.ExposureTime = this.camera.Prm.Exposure;
            this.modelFindPrm.Gain = this.camera.Prm.Gain;
            //this.modelFindPrm.LightType = this.camera.Prm.LightType;
            this.modelFindPrm.ExecutePrm = this.light.ExecutePrm.Clone() as ExecutePrm;
            this.Setup(this.modelFindPrm);
            this.pattern = pattern;
            this.isManual = isManual;

            this.ckbFrmFile.Checked = modelFindPrm.IsFromFile;
            if(this.isManual)
            {
                this.gbxContent.Enabled = false;
                this.BtnPrev.Enabled = false;
                this.BtnNext.Enabled = false;
                this.BtnTeach.Enabled = false;
                this.BtnDone.Enabled = true;
                this.BtnCancel.Enabled = true;
            }
            this.UpdateByFlag();
            this.ReadLanguageResources();
            if (this.modelFindPrm!=null)
                this.modelFindPrmBackUp = (ModelFindPrm)this.modelFindPrm.Clone();
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditModelFindForm()
        {
            InitializeComponent();
        }

        private void Init()
        {
            this.picModelImage.Hide();

            this.CamCtrl.Pbx.Paint += imageBox_Paint;

            this.penDash = new Pen(Color.Red, 1);
            this.penDash.DashStyle = DashStyle.Custom;
            penDash.DashPattern = new float[] { 3f, 2f };
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

            this.nudTolerance.Minimum = 0.1M;
            this.nudTolerance.Maximum = 5;
            this.nudTolerance.Increment = 0.1M;
            this.nudTolerance.DecimalPlaces = 1;

            this.nudAcceptThreshold.Minimum = 0;
            this.nudAcceptThreshold.Maximum = 1;
            this.nudAcceptThreshold.Increment = 0.1M;
            this.nudAcceptThreshold.DecimalPlaces = 1;

            this.nudSettlingTime.Minimum = 0;
            this.nudSettlingTime.Maximum = 5000;
        }

        private EditModelFindForm Setup(ModelFindPrm modelFindPrm)
        {
            if(TempVisionData.Ins.TempModelFindPrm == null)
            {
                TempVisionData.Ins.TempModelFindPrm = new ModelFindPrm();
                TempVisionData.Ins.TempModelFindPrm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - 100;
                TempVisionData.Ins.TempModelFindPrm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - 100;
                TempVisionData.Ins.TempModelFindPrm.ModelWidth = 200;
                TempVisionData.Ins.TempModelFindPrm.ModelHeight = 200;
                TempVisionData.Ins.TempModelFindPrm.SearchTopLeftX = 100;
                TempVisionData.Ins.TempModelFindPrm.SearchTopLeftY = 100;
                TempVisionData.Ins.TempModelFindPrm.SearchWidth = this.CamCtrl.ImgWidth - 200;
                TempVisionData.Ins.TempModelFindPrm.SearchHeight = this.CamCtrl.ImgHeight - 200;
                TempVisionData.Ins.TempModelFindPrm.AcceptScore = 0.6;
                TempVisionData.Ins.TempModelFindPrm.SettlingTime = 50;
                TempVisionData.Ins.TempModelFindPrm.Tolerance = 2;
            }
            if (modelFindPrm.ModelId == 0)
            {
                TempVisionData.Ins.TempModelFindPrm.CopyTempDataTo(modelFindPrm);
            }
   
            this.CamCtrl.HideROI();
            this.CamCtrl.SetExposure(this.modelFindPrm.ExposureTime).SetGain(this.modelFindPrm.Gain);
            //this.CamCtrl.SelectLight(this.modelFindPrm.LightType);
            this.CamCtrl.SelectLight(this.modelFindPrm.ExecutePrm);
            this.SetupLight(this.modelFindPrm.ExecutePrm);

            this.nudModelSizeWidth.Maximum = this.camera.Executor.ImageWidth;
            this.nudModelSizeHeight.Maximum = this.camera.Executor.ImageHeight;
            this.nudSearchWindowWidth.Maximum = this.camera.Executor.ImageWidth;
            this.nudSearchWindowHeight.Maximum = this.camera.Executor.ImageHeight;

            this.nudSearchWindowWidth.Value = MathUtils.Limit(this.modelFindPrm.SearchWidth, 0, this.camera.Executor.ImageWidth);
            this.nudSearchWindowHeight.Value = MathUtils.Limit(this.modelFindPrm.SearchHeight, 0, this.camera.Executor.ImageHeight);
            this.nudModelSizeWidth.Value = MathUtils.Limit(this.modelFindPrm.ModelWidth, 0, this.camera.Executor.ImageWidth);
            this.nudModelSizeHeight.Value = MathUtils.Limit(this.modelFindPrm.ModelHeight, 0, this.camera.Executor.ImageHeight);
            this.nudAcceptThreshold.Value = (decimal)MathUtils.Limit(this.modelFindPrm.AcceptScore, 0, 1);
            this.nudTolerance.Value = (decimal)MathUtils.Limit(this.modelFindPrm.Tolerance, (double)this.nudTolerance.Minimum, (double)this.nudTolerance.Maximum);
            this.nudSettlingTime.Value = MathUtils.Limit(this.modelFindPrm.SettlingTime, 0, (int)this.nudSettlingTime.Maximum);
            return this;
        }

        public void DoCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void DoDone()
        {
            this.modelFindPrm.AcceptScore = (double)this.nudAcceptThreshold.Value;
            this.modelFindPrm.SettlingTime = (int)this.nudSettlingTime.Value;
            this.modelFindPrm.Tolerance = (double)this.nudTolerance.Value;
            this.modelFindPrm.ExecutePrm = (ExecutePrm)this.light.ExecutePrm.Clone();
            if (this.pattern is Workpiece && this.ckbFrmFile.Checked)
            {
                MessageBox.Show("workPiece的mark点不能来自文件，只能示教");
                return;
            }
            this.modelFindPrm.IsFromFile = this.ckbFrmFile.Checked;
            if (this.isManual)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.modelFindPrm.CopyTempDataTo(TempVisionData.Ins.TempModelFindPrm);
                TempVisionData.Ins.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            CompareObj.CompareProperty(this.modelFindPrm, this.modelFindPrmBackUp, true);
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
            this.CreateModel();
            if (this.pattern != null)
            {
                PointD p = pattern.SystemRel(Machine.Instance.Robot.PosX - pattern.GetOriginPos().X, Machine.Instance.Robot.PosY - pattern.GetOriginPos().Y);
                this.modelFindPrm.PosInPattern.X = p.X;
                this.modelFindPrm.PosInPattern.Y = p.Y;
            }
            //改变Mark曝光需要重新做飞拍校正
            if (cameraControl1.ExposureTime != this.modelFindPrm.ExposureTime)
            {
                this.pattern.Program.RuntimeSettings.FlyOffsetIsValid = false;
            }
            this.modelFindPrm.ExposureTime = this.cameraControl1.ExposureTime;
            this.modelFindPrm.Gain = this.cameraControl1.Gain;
            //this.modelFindPrm.LightType = Machine.Instance.Light.CurrType;
            this.modelFindPrm.ExecutePrm = Machine.Instance.Light.ExecutePrm.Clone() as ExecutePrm;
            this.modelFindPrm.AcceptScore = (double)this.nudAcceptThreshold.Value;
            this.modelFindPrm.SettlingTime = (int)this.nudSettlingTime.Value;
            this.modelFindPrm.Tolerance = (double)this.nudTolerance.Value;
        }

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                pbxRoiRect.Width = (int)(modelFindPrm.ModelWidth / this.CamCtrl.Radio);
                pbxRoiRect.Height = (int)(modelFindPrm.ModelHeight / this.CamCtrl.Radio);

                if (this.flag <= 1)
                {
                    pbxRoiRect.X = this.CamCtrl.PbxCenter.X - (int)(modelFindPrm.ModelWidth / 2 / this.CamCtrl.Radio);
                    pbxRoiRect.Y = this.CamCtrl.PbxCenter.Y - (int)(modelFindPrm.ModelHeight / 2 / this.CamCtrl.Radio);
                    e.Graphics.DrawRectangle(this.penGreen, pbxRoiRect);

                }
                else
                {
                    pbxRoiRect.X = this.CamCtrl.PbxCenter.X - (int)((this.CamCtrl.ImgCenter.X - this.modelFindPrm.MarkInImg.X + this.modelFindPrm.ModelWidth / 2) / this.CamCtrl.Radio);
                    pbxRoiRect.Y = this.CamCtrl.PbxCenter.Y - (int)((this.CamCtrl.ImgCenter.Y - this.modelFindPrm.MarkInImg.Y + this.modelFindPrm.ModelHeight / 2) / this.CamCtrl.Radio);
                    e.Graphics.DrawRectangle(this.penGreen, pbxRoiRect);
                }

                pbxSearchRect.Width = (int)(modelFindPrm.SearchWidth / this.CamCtrl.Radio);
                pbxSearchRect.Height = (int)(modelFindPrm.SearchHeight / this.CamCtrl.Radio);
                pbxSearchRect.X = this.CamCtrl.PbxCenter.X - (int)(modelFindPrm.SearchWidth / 2 / this.CamCtrl.Radio);
                pbxSearchRect.Y = this.CamCtrl.PbxCenter.Y - (int)(modelFindPrm.SearchHeight / 2 / this.CamCtrl.Radio);
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
                this.nudModelSizeWidth.Value = this.modelFindPrm.ModelWidth;
                return;
            }
            modelFindPrm.ModelWidth = (int)this.nudModelSizeWidth.Value;
            modelFindPrm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - modelFindPrm.ModelWidth / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudModelSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudModelSizeHeight.Value > this.nudSearchWindowHeight.Value - 8)
            {
                this.nudModelSizeHeight.Value = modelFindPrm.ModelHeight;
                return;
            }
            modelFindPrm.ModelHeight = (int)this.nudModelSizeHeight.Value;
            modelFindPrm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - modelFindPrm.ModelHeight / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudSearchWindowWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudSearchWindowWidth.Value < this.nudModelSizeWidth.Value + 8)
            {
                this.nudSearchWindowWidth.Value = modelFindPrm.SearchWidth;
                return;
            }
            modelFindPrm.SearchWidth = (int)this.nudSearchWindowWidth.Value;
            modelFindPrm.SearchTopLeftX = this.CamCtrl.ImgCenter.X - modelFindPrm.SearchWidth / 2;
            this.CamCtrl.Pbx.Invalidate();
        }

        private void nudSearchWindowHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudSearchWindowHeight.Value < this.nudModelSizeHeight.Value + 8)
            {
                this.nudSearchWindowHeight.Value = modelFindPrm.SearchHeight;
                return;
            }
            modelFindPrm.SearchHeight = (int)this.nudSearchWindowHeight.Value;
            modelFindPrm.SearchTopLeftY = this.CamCtrl.ImgCenter.Y - modelFindPrm.SearchHeight / 2;
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
                this.MatchTest();
                this.flag = 2;
                if (this.tbStatus.Text == "查找成功")
                {
                    this.CamCtrl.Pbx.Invalidate();
                }
                UpdateByFlag();
            }
            if (this.rbFindAtTaught.Checked)
            {
                Task.Factory.StartNew(() =>
                {
                    //move to mark pos
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //Machine.Instance.Robot.MovePosXYAndReply(pattern.MachineAbs(modelFindPrm.PosInPattern));
                    Machine.Instance.Robot.ManualMovePosXYAndReply(pattern.MachineAbs(modelFindPrm.PosInPattern));
                    Thread.Sleep(100);

                    this.BeginInvoke(new Action(() =>
                    {
                        this.MatchTest();
                        this.flag = 2;
                        if (this.tbStatus.Text == "查找成功")
                        {
                            this.CamCtrl.Pbx.Invalidate();
                        }
                        UpdateByFlag();
                    }));
                });
            }
        }

        private void btnShowModel_Click(object sender, EventArgs e)
        {
            if (this.btnShowModel.FlatStyle == FlatStyle.Standard)
            {
                this.picModelImage.Width = (int)(modelFindPrm.ModelWidth / this.CamCtrl.Radio) * 2;
                this.picModelImage.Height = (int)(modelFindPrm.ModelHeight / this.CamCtrl.Radio) * 2;
                this.picModelImage.Show();
                this.picModelImage.Image = this.BytesToBmp(modelFindPrm.ModelData,
                    modelFindPrm.ModelWidth, modelFindPrm.ModelHeight);
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
                    this.LblTitle.Text = "调节查找参数，点击[示教]。";
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
                    this.tbConfidence.Text = "";
                    this.tbFoundLoc.Text = "";
                    this.tbTime.Text = "";
                    this.tbTaughtLoc.Text = "";
                    this.ReadLanguageResources();
                    break;
                case 1:
                    this.LblTitle.Text = "测试查找结果或者点击[完成]确定查找结果。";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.tab.TabPages.Clear();
                    this.tab.TabPages.Add(this.tbgTest);
                    this.tbStatus.Text = "";
                    this.tbConfidence.Text = "";
                    this.tbFoundLoc.Text = "";
                    this.tbTime.Text = "";
                    this.tbTaughtLoc.Text = "";
                    this.ReadLanguageResources();
                    break;
                case 2:
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.tab.TabPages.Clear();
                    this.tab.TabPages.Add(this.tbgTest);
                    if (this.tbStatus.Text == "查找成功")
                    {
                        this.LblTitle.Text = "查找到结果，点击[完成]。";
                        this.BtnDone.Enabled = true;
                    }
                    else if (this.tbStatus.Text == "查找失败")
                    {
                        this.LblTitle.Text = "没有查找到结果，点击[上一步]重新查找。";
                        this.BtnDone.Enabled = false;
                    }
                    this.ReadLanguageResources();
                    break;
            }
        }

        private int CreateModel()
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

            byte[] roiImageData = new byte[modelFindPrm.ModelWidth * modelFindPrm.ModelHeight];
            int rtn = ModelFindThm.GetRoiImageData(bytes, this.CamCtrl.ImgWidth, this.CamCtrl.ImgHeight, modelFindPrm.ModelTopLeftX,
                modelFindPrm.ModelTopLeftY, modelFindPrm.ModelWidth, modelFindPrm.ModelHeight, roiImageData);

            if (rtn != 0) return rtn;

            int modelId = 0;
            rtn = ModelFindThm.CreateModel(roiImageData, modelFindPrm.ModelWidth, modelFindPrm.ModelHeight,
                modelFindPrm.SearchTopLeftX, modelFindPrm.SearchTopLeftY, modelFindPrm.SearchWidth, modelFindPrm.SearchHeight, ref modelId);
            this.modelFindPrm.ModelId = modelId;

            modelFindPrm.ModelData = roiImageData;

            return rtn;
        }

        private void btnGotoTaught_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            PointD pos = pattern.MachineAbs(modelFindPrm.PosInPattern);
            //Machine.Instance.Robot.MovePosXY(pattern.MachineAbs(modelFindPrm.PosInPattern));
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

        private byte[] CaptureSearchImage(byte[] imgBytes, int imgWidth, int imgHeight)
        {
            byte[] searchImageData = new byte[modelFindPrm.SearchWidth * modelFindPrm.SearchHeight];
            ModelFindThm.GetRoiImageData(imgBytes, imgWidth, imgHeight, modelFindPrm.SearchTopLeftX,
                modelFindPrm.SearchTopLeftY, modelFindPrm.SearchWidth, modelFindPrm.SearchHeight, searchImageData);
            return searchImageData;
        }

        private void MatchTest()
        {
            double matchScore = 0, matchPointX = 0, matchPointY = 0;
            DateTime beforeDT = System.DateTime.Now;
            this.modelFindPrm.PosInMachine = new PointD(Machine.Instance.Robot.PosXY);
            this.modelFindPrm.TargetInMachine = new PointD(this.modelFindPrm.PosInMachine);
            this.tbTaughtLoc.Text = this.modelFindPrm.PosInMachine.ToString();

            //0:found, -1:unfound, 1:found but out of tolerance
            int foundState = -1;
            this.tbConfidence.Text = string.Empty;
            this.tbTime.Text = string.Empty;

            if (ModelFindThm.Match(this.camera.Executor.CurrentBytes, this.CamCtrl.ImgWidth, this.CamCtrl.ImgHeight, this.modelFindPrm.ModelId,
               ref matchScore, ref matchPointX, ref matchPointY) == 0)
            {
                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforeDT);

                this.tbConfidence.Text = matchScore.ToString("f3");
                this.tbTime.Text = string.Format("{0}ms", ts.TotalMilliseconds);

                if (matchScore > (double)this.nudAcceptThreshold.Value)
                {
                    this.modelFindPrm.MarkInImg = new PointD(matchPointX, matchPointY);
                    this.modelFindPrm.TargetInMachine += Machine.Instance.Camera.ToMachine(this.modelFindPrm.MarkInImg);

                    if (this.modelFindPrm.IsOutOfTolerance())
                    {
                        foundState = 1;
                    }
                    else
                    {
                        foundState = 0;
                    }
                }
                else
                {
                    foundState = -1;
                }
            }
            else
            {
                foundState = -1;
            }

            if(foundState < 0)
            {
                this.tbStatus.Text = "UnFound";
                this.tbStatus.BackColor = Color.Red;
                this.tbFoundLoc.Text = string.Empty;
                this.tbDiff.Text = string.Empty;
            }
            else
            {
                if(foundState == 0)
                {
                    this.tbStatus.BackColor = Color.Green;
                }
                else if(foundState == 1)
                {
                    this.tbStatus.BackColor = Color.Yellow;
                }
                this.tbStatus.Text = "Found";
                this.tbFoundLoc.Text = this.modelFindPrm.TargetInMachine.ToString();
                this.tbDiff.Text = (this.modelFindPrm.TargetInMachine - this.modelFindPrm.PosInMachine).ToString();
            }

        }
    }
}
