using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogCalibCamera : DialogBase, IOptional
    {
        private int flag = 0;
        private bool err = false;

        private Pen penRed;
        private Pen penGreen;
        private Pen penRedDash;
        private Rectangle pbxRoiRect = Rectangle.Empty;

        private ModelFindPrm prm = new ModelFindPrm();
        private ModelFindPrm prmBackUp = new ModelFindPrm();
        private PointD[] points = new PointD[9];
        private CalibBy9dPrm calibBy9dPrm = new CalibBy9dPrm();
        private double matchScore, matchX, matchY;
        private double incBackUp;

        private string[] lblTip = new string[5]
       {
           "Press [Next] to load a board.",
           "Select a model and [Next].",
           "Press [Next] if model is acceptable.",
           "Score1 = 0.99.",
           "Press [Done] to accept results."
       };

        public DialogCalibCamera()
        {
            InitializeComponent();

            this.CamCtrl.Pbx.Paint += CamCtrl_Paint;

            this.penRed = new Pen(Color.Red, 1);
            this.penGreen = new Pen(Color.Green, 1);
            this.penRedDash = new Pen(Color.Red, 1);
            this.penRedDash.DashStyle = DashStyle.Dash;

            this.nudRoiWidth.Minimum = 0;
            this.nudRoiWidth.Increment = 6;
            this.nudRoiHeight.Minimum = 0;
            this.nudRoiHeight.Increment = 6;
            this.nudRoiWidth.ValueChanged += NudRoiWidth_ValueChanged;
            this.nudRoiHeight.ValueChanged += NudRoiHeight_ValueChanged;

            this.nudInc.Minimum = 1;
            this.nudInc.Maximum = 50;
            this.nudInc.Value = 2;
            this.nudInc.Increment = 0.1M;
            this.nudInc.DecimalPlaces = 1;
            this.incBackUp= (double)this.nudInc.Value;
            this.UpdateByFlag();
            this.ReadLanguageResources();
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            for (int i = 0; i < lblTip.Length; i++)
            {
                string temp = "";
                temp = this.ReadKeyValueFromResources(string.Format("Tip{0}", i));
                //空值不读取
                if (temp != "")
                {
                    lblTip[i] = temp;
                }
            }
        }

        public DialogCalibCamera Setup()
        {
            this.CamCtrl.HideROI();

            this.prm.ModelWidth = 200;
            this.prm.ModelHeight = 200;
            this.prm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - this.prm.ModelWidth / 2;
            this.prm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - this.prm.ModelHeight / 2;
            this.prm.SearchTopLeftX = 0;
            this.prm.SearchTopLeftY = 0;
            this.prm.SearchWidth = this.CamCtrl.ImgWidth;
            this.prm.SearchHeight = this.CamCtrl.ImgHeight;

            this.nudRoiWidth.Maximum = this.CamCtrl.ImgWidth;
            this.nudRoiHeight.Maximum = this.CamCtrl.ImgHeight;
            this.nudRoiWidth.Value = this.prm.ModelWidth;
            this.nudRoiHeight.Value = this.prm.ModelHeight;

            this.lblAngle.Text = Machine.Instance.Robot.CalibBy9dPrm.Angle.ToString();
            this.lblScale.Text = Machine.Instance.Robot.CalibBy9dPrm.Scale.ToString();
            this.lblOffsetX.Text = Machine.Instance.Robot.CalibBy9dPrm.OffsetX.ToString();
            this.lblOffsetY.Text = Machine.Instance.Robot.CalibBy9dPrm.OffsetY.ToString();

            this.prmBackUp = (ModelFindPrm)this.prm?.Clone();
            return this;
        }

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            if (this.flag == 4)
            {
                if (!this.err)
                {
                    //to do accept calibration result
                    Machine.Instance.Robot.CalibBy9dPrm = this.calibBy9dPrm;
                    Machine.Instance.Robot.SaveCalibBy9dPrm();
                    CompareObj.CompareProperty(this.prm, this.prmBackUp, null, this.GetType().Name, true);
                }
            }
        }

        public void DoNext()
        {
            if (this.CamCtrl.Cam == null)
            {
                return;
            }

            switch (this.flag)
            {
                case 1:
                    //create model
                    this.CreateModel();
                    break;
                case 2:
                    double inc = (double)this.nudInc.Value;
                    string msg = string.Format("Inc oldValue: {0} -> newValue: {1}", this.incBackUp, this.nudInc.Value);
                    Logger.DEFAULT.Info(LogCategory.SETTING, this.GetType().Name, msg);
                    PointD pos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    this.points[0] = new PointD(0, 0);
                    this.points[1] = new PointD(0, inc);
                    this.points[2] = new PointD(inc, inc);
                    this.points[3] = new PointD(inc, 0);
                    this.points[4] = new PointD(inc, -inc);
                    this.points[5] = new PointD(0, -inc);
                    this.points[6] = new PointD(-inc, -inc);
                    this.points[7] = new PointD(-inc, 0);
                    this.points[8] = new PointD(-inc, inc);
                    this.calibBy9dPrm.PhyPtSet.Clear();
                    this.calibBy9dPrm.ImgPtSet.Clear();
                    foreach (var item in this.points)
                    {
                        this.calibBy9dPrm.PhyPtSet.Add(item.X);
                        this.calibBy9dPrm.PhyPtSet.Add(item.Y);
                    }

                    int rtn = 0;
                    //find model 9 times
                    Task.Factory.StartNew(() =>
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            //to do move 
                            if (Machine.Instance.Robot.ManualMovePosXYAndReply(pos.X + this.points[i].X, pos.Y + this.points[i].Y) == Result.FAILED)
                            {
                                this.err = true;
                                break;
                            }

                            Thread.Sleep(500);
                            byte[] bytes = this.CamCtrl.Cam.Executor.CurrentBytes;
                            if (bytes == null)
                            {
                                this.err = true;
                                break;
                            }

                            rtn = ModelFindThm.Match(bytes, this.CamCtrl.ImgWidth, this.CamCtrl.ImgHeight, this.prm.ModelId,
                                ref this.matchScore, ref this.matchX, ref this.matchY);
                            if (rtn != 0)
                            {
                                this.err = true;
                                break;
                            }

                            this.calibBy9dPrm.ImgPtSet.Add(this.matchX);
                            this.calibBy9dPrm.ImgPtSet.Add(this.matchY);

                            this.BeginUpdateMatchResult(i + 1, this.matchScore, this.matchX, this.matchY);
                            Thread.Sleep(500);
                        }
                        //Machine.Instance.Robot.MovePosXYAndReply(pos.X, pos.Y);
                        Machine.Instance.Robot.ManualMovePosXYAndReply(pos.X, pos.Y);

                        this.calibBy9dPrm.Num = 9;
                        rtn = this.calibBy9dPrm.Update();
                        if (rtn != 0)
                        {
                            this.err = true;
                        }

                        this.flag++;
                        this.BeginInvoke(new Action(() =>
                        {
                            this.UpdateByFlag();
                        }));
                    });

                    break;
            }

            this.flag++;
            this.UpdateByFlag();
        }

        public void DoPrev()
        {
            if (this.flag == 4)
            {
                this.flag = 2;
            }
            else
            {
                this.flag--;
            }
            this.UpdateByFlag();
        }

        public void DoTeach()
        {
        
        }

        private void CamCtrl_Paint(object sender, PaintEventArgs e)
        {
            if (this.CamCtrl.Cam == null)
            {
                return;
            }

            try
            {
                pbxRoiRect.Width = (int)(this.prm.ModelWidth / this.CamCtrl.Radio);
                pbxRoiRect.Height = (int)(this.prm.ModelHeight / this.CamCtrl.Radio);

                if (this.flag <= 2)
                {
                    pbxRoiRect.X = this.CamCtrl.PbxCenter.X - (int)(this.prm.ModelWidth / 2 / this.CamCtrl.Radio);
                    pbxRoiRect.Y = this.CamCtrl.PbxCenter.Y - (int)(this.prm.ModelHeight / 2 / this.CamCtrl.Radio);
                    e.Graphics.DrawRectangle(this.penRed, pbxRoiRect);
                }
                else if (this.flag == 3)
                {
                    pbxRoiRect.X = this.CamCtrl.PbxCenter.X - (int)((this.CamCtrl.ImgCenter.X - this.matchX + this.prm.ModelWidth / 2) / this.CamCtrl.Radio);
                    pbxRoiRect.Y = this.CamCtrl.PbxCenter.Y - (int)((this.CamCtrl.ImgCenter.Y - this.matchY + this.prm.ModelHeight / 2) / this.CamCtrl.Radio);
                    e.Graphics.DrawRectangle(this.penGreen, pbxRoiRect);
                }
            }
            catch (Exception)
            {

            }
        }

        private int CreateModel()
        {
            if (this.CamCtrl.Cam == null)
            {
                return -1;
            }

            byte[] bytes = this.CamCtrl.Cam.Executor.CurrentBytes;
            if (bytes == null)
            {
                return -1;
            }

            byte[] roiImageData = new byte[this.prm.ModelWidth * this.prm.ModelHeight];
            int rtn = ModelFindThm.GetRoiImageData(bytes, this.CamCtrl.ImgWidth, this.CamCtrl.ImgHeight,
                this.prm.ModelTopLeftX, this.prm.ModelTopLeftY, this.prm.ModelWidth, this.prm.ModelHeight, roiImageData);
            if (rtn != 0) return rtn;

            int modelId = 0;
            rtn = ModelFindThm.CreateModel(roiImageData, this.prm.ModelWidth, this.prm.ModelHeight,
                this.prm.SearchTopLeftX, this.prm.SearchTopLeftY, this.prm.SearchWidth, this.prm.SearchHeight, ref modelId);
            this.prm.ModelId = modelId;

            return rtn;
        }

        private void UpdateByFlag()
        {
            switch (this.flag)
            {
                case 0:
                    this.LblTitle.Text = lblTip[0];
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.gbxROI.Enabled = false;
                    this.nudInc.Enabled = false;
                    break;
                case 1:
                    this.LblTitle.Text =lblTip[1];
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.gbxROI.Enabled = true;
                    this.nudInc.Enabled = true;
                    break;
                case 2:
                    this.LblTitle.Text = lblTip[2];
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.gbxROI.Enabled = false;
                    this.nudInc.Enabled = true;
                    break;
                case 3:
                    this.LblTitle.Text = lblTip[3];
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.gbxROI.Enabled = false;
                    this.nudInc.Enabled = false;
                    break;
                case 4:
                    this.LblTitle.Text = lblTip[4] ;
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.gbxROI.Enabled = false;
                    this.nudInc.Enabled = false;

                    this.lblAngle.Text = this.calibBy9dPrm.Angle.ToString();
                    this.lblScale.Text = this.calibBy9dPrm.Scale.ToString();
                    this.lblOffsetX.Text = this.calibBy9dPrm.OffsetX.ToString();
                    this.lblOffsetY.Text = this.calibBy9dPrm.OffsetY.ToString();
                    break;
            }
        }

        private void BeginUpdateMatchResult(int i, double score, double x, double y)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.LblTitle.Text = string.Format("Score{0} = {1}, X = {2}, Y = {3}", i, score, x, y);
                this.CamCtrl.Pbx.Invalidate();
            }));
        }

        private void NudRoiHeight_ValueChanged(object sender, EventArgs e)
        {
            this.prm.ModelHeight = (int)this.nudRoiHeight.Value;
            this.prm.ModelTopLeftY = this.CamCtrl.ImgCenter.Y - this.prm.ModelHeight / 2;

            this.CamCtrl.Pbx.Invalidate();
        }

        private void NudRoiWidth_ValueChanged(object sender, EventArgs e)
        {
            this.prm.ModelWidth = (int)this.nudRoiWidth.Value;
            this.prm.ModelTopLeftX = this.CamCtrl.ImgCenter.X - this.prm.ModelWidth / 2;

            this.CamCtrl.Pbx.Invalidate();
        }
    }
}
