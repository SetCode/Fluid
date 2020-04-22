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

namespace Anda.Fluid.Domain.Vision
{
    public partial class CameraView : UserControlEx
    {
        private Camera camera = null;
        private Pen penRedDash;
        private Pen penRed;
        private Rectangle imgRectRoi;
        private Rectangle pbxRectRoi;
        private bool hideROI = false;
        private ContextMenuStrip cms;
        private const string STR_SAVE_IMAGE = "Saved Images";
        private CameraPrm prmBackUp;

        public CameraView()
        {
            InitializeComponent();

            this.picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            if (Properties.Settings.Default.roiRaduis == 0)
            {
                Properties.Settings.Default.roiRaduis = 2;
            }

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
        }

        public int ShapeType { get; set; }

        public Camera Camera => this.camera;

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
        }

        public CameraView SetupCamera(Camera camera)
        {
            this.camera = camera;
            this.camera.Executor.BitmapDisplayed -= Executor_BitmapGrabbed;
            try
            {
                this.ImgWidth = this.camera.Executor.ImageWidth;
                this.ImgHeight = this.camera.Executor.ImageHeight;
                this.CalcRadio();
                this.imgRectRoi.Width = 0;// this.Raduis * 2;
                this.imgRectRoi.Height = 0;// this.Raduis * 2;
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

        public void UpdateROI(double raduis)
        {
            if (Machine.Instance.Robot.CalibBy9dPrm.Scale == 0) return;
            //单个像素的长度(mm)
            double pixesLength = Machine.Instance.Robot.CalibBy9dPrm.Scale;
            //求得指定长度的像素个数
            int pixesInCamera = (int)(raduis / pixesLength);
            //求得在实际显示控件中应该显示的像素个数
            int pixesInControlX = (this.ImgWidth / this.camera.Executor.ImageWidth) * pixesInCamera;
            int pixesInControlY = (this.ImgHeight / this.camera.Executor.ImageHeight) * pixesInCamera;
            this.imgRectRoi.Width = pixesInControlX;
            this.imgRectRoi.Height = pixesInControlX;
            this.imgRectRoi.X = (this.ImgWidth - this.imgRectRoi.Width) / 2;
            this.imgRectRoi.Y = (this.ImgHeight - this.imgRectRoi.Height) / 2;
            this.CalcRoiRectPbx();
            this.picCamera.Invalidate();
        }

        public void UpdateShape(int shapeType)
        {
            this.ShapeType = shapeType;
            this.picCamera.Invalidate();
        }

        private void PicCamera_Paint(object sender, PaintEventArgs e)
        {
            this.drawBaseLines(e);

            if(this.hideROI)
            {
                return;
            }

            switch (this.ShapeType)
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
                imgX = this.Radio * (this.PbxImgWidth / 2 - (this.picCamera.Width / 2 - e.X));
                imgY = this.Radio * (this.PbxImgHeight / 2 - (this.picCamera.Height / 2 - e.Y));
                if(imgX < 0 || imgY < 0)
                {
                    return;
                }

                PointD p = Machine.Instance.Camera?.ToMachine(imgX, imgY);
                if (Math.Abs(p.X) > 10 || Math.Abs(p.Y) > 10)
                {
                    return;
                }

                Machine.Instance.Robot?.MoveSafeZ();
                Machine.Instance.Robot?.ManulMoveIncXY(p);
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
            // 只需更新引用，原引用对象在其他地方释放
            picCamera.Image = arg1;
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
