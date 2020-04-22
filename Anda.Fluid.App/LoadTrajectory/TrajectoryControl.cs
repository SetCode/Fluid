using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class TrajectoryControl : UserControl
    {
        private List<PointD> trajpoints;
        private List<PointD> pointsScaled = new List<PointD>();
        //private Graphics graphics;

        private PointF origin1 = new PointF(0, 0);
        private PointF origin2 = new PointF(0,0);
        private double scale;

        private bool mouseMiddIsClick = false;
        private PointF mouseDownPoint = new PointF(0,0);
        private PointF mouseLastPoint = new PointF(0,0);

        public TrajectoryControl()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(MouseWheeled);
        }
        public TrajectoryControl Setup(int width,int height)
        {
            this.origin2.X = (float)width /2;
            this.origin2.Y = (float)height/2;
            this.Width = width;
            this.Height = height;
            return this;
        }

        
        public void SetTraj(List<PointD> trajpoints)
        {
            this.trajpoints = trajpoints;
            this.Initial(trajpoints);
        }
        private void TransPoint(double xInCoord2,double yInCoord2, out double xInCoord1, out double yInCoord1)
        {
            xInCoord1 = xInCoord2 + origin2.X;
            yInCoord1 = origin2.Y-yInCoord2;
        }
        //  画坐标系
        private void DrawCoordinate(Graphics graphics)
        {
            Pen pen = new Pen(Color.Red, 2);
            graphics.DrawLine(pen, origin2.X,origin2.Y, origin2.X+10, origin2.Y);
            graphics.DrawLine(pen, origin2.X, origin2.Y, origin2.X, origin2.Y-10);

        }
        public void Initial(List<PointD> list)
        {
            List<double> midXList = new List<double>();
            List<double> midYList = new List<double>();

            foreach (PointD point in list)
            {
                midXList.Add(point.X);
                midYList.Add(point.Y);
            }
            
            double radioX = (this.Width*1/2) / Math.Max(Math.Abs(midXList.Max()), Math.Abs(midXList.Min()));
            double radioY = (this.Height*1/2) / Math.Max(Math.Abs(midYList.Max()), Math.Abs(midYList.Min()));
            if (radioX >= radioY)
            {
                scale = radioY;
            }
            else
            {
                scale = radioX;
            }
            this.scalePoints();
        }

        private void MouseWheeled(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                scale += 0.1F;
            }
            else
            {
                scale=Math.Max(scale-0.1F, 0.01F) ;
            }            
            this.scalePoints();
        }

        public void scalePoints()
        {
            if (this.trajpoints == null || this.trajpoints.Count <= 0)
                return;
            pointsScaled.Clear();
            foreach (PointD p in trajpoints)
            {
                pointsScaled.Add(new PointD(p.X * scale, p.Y * scale));
            }
            this.Invalidate();
        }
        private void PaintPoint(Graphics graphics,double x, double y)
        {
            Pen pen = new Pen(Color.Green, 1);
            PointF location = new PointF((float)x, (float)y);
            SizeF size = new SizeF(2, 2);
            RectangleF rect = new RectangleF(location, size);
            Brush brush = new SolidBrush(Color.Red);
            graphics.FillEllipse(brush, rect);
            graphics.DrawArc(pen, rect, 0, 360);
        }

    
        private void TrajectoryControl_Paint(object sender, PaintEventArgs e)
        {            
            Bitmap img = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(img);
            graphics.Clear(Color.Moccasin);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.DrawCoordinate(graphics);
            double xInCoord1, yInCoord1;
            if (this.pointsScaled != null && this.pointsScaled.Count > 0)
            {
                foreach (PointD p in pointsScaled)
                {
                    this.TransPoint(p.X, p.Y, out xInCoord1, out yInCoord1);
                    this.PaintPoint(graphics, xInCoord1, yInCoord1);
                }
            }
            Graphics g = e.Graphics;
            g.Clear(Color.Moccasin);
            g.DrawImage(img, new Point(0, 0));
            graphics.Dispose();
            img.Dispose();
            g.Dispose();
        }

        private void TrajectoryControl_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void TrajectoryControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddIsClick = true;
                this.mouseLastPoint.X = e.X;
                this.mouseLastPoint.Y = e.Y;
            }
        }

        private void TrajectoryControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddIsClick = false;                
            }
        }

        private void TrajectoryControl_MouseMove(object sender, MouseEventArgs e)
        {            
            if (!this.mouseMiddIsClick)
            {
                return;
            }
            double dx = e.X - this.mouseLastPoint.X;
            double dy= e.Y - this.mouseLastPoint.Y;
            origin2.X += (float)dx;
            origin2.Y += (float)dy;
            this.mouseLastPoint.X = e.X;
            this.mouseLastPoint.Y = e.Y;

            this.Invalidate();
        }
    }
}
