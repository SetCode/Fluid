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
    public partial class ComponentShape : UserControl
    {
        private Graphics graphics;
        private PointF origin = new PointF(0, 0);
        private double scale=2;

        private List<GlueDot> points;
        private GlueDot seletecPoint; 
        private float width = 0;
        private float height = 0;
        public ComponentShape()
        {
            InitializeComponent();

        }
        public void Setup(int width, int height)
        {
            this.origin.X = (float)width / 2;
            this.origin.Y = (float)height / 2;
            this.Width = width;
            this.Height = height;
        }
        private void ComponentShape_Paint(object sender, PaintEventArgs e)
        {
            Bitmap img = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(img);
            graphics.Clear(Color.Black);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.drawCoordinate();
            this.paintRect();
            this.paintPoints();
            if(this.seletecPoint!=null)
                this.PaintPoint(Color.Red, (float)this.seletecPoint.point.X, (float)this.seletecPoint.point.Y);
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.DrawImage(img, new Point(0, 0));
            graphics.Dispose();
            img.Dispose();
            g.Dispose();
        }
        private void PaintPoint(Color color,float x, float y)
        {
            Pen pen = new Pen(color, 2);
            graphics.DrawLine(pen, (float)(x * this.scale + origin.X) - (float)(length*this.scale), (float)(origin.Y - y * this.scale), (float)(x * this.scale + origin.X) + (float)(length * this.scale), (float)(origin.Y - y * this.scale));
            graphics.DrawLine(pen, (float)(x * this.scale + origin.X), (float)(origin.Y - y * this.scale) + (float)(length * this.scale), (float)(x * this.scale + origin.X), (float)(origin.Y - y * this.scale) - (float)(length * this.scale));


        }
        
        private void paintPoints()
        {
            if (this.points == null)
                return;
            if (this.points.Count<=0)
                return;
            foreach (var item in this.points)
            {
                this.PaintPoint(Color.Blue,(float)item.point.X, (float) item.point.Y);
                this.drawPoint(Color.Red, (float)item.point.X, (float)item.point.Y, (float)item.Radius);               
            }
        }

        
        private void paintRect()
        {
            Pen pen = new Pen(Color.White, 2);            
            graphics.DrawRectangle(pen, this.origin.X-this.width/2, this.origin.Y-this.height/2, this.width, this.height);
        }
        private float length = 0;
        public void AddRect(float width, float height)
        {
            this.width = width;
            this.height = height;
            double scaleW= this.Width / width;
            double scaleH = this.Height / height;
            //取小
            if (scaleW < scaleH)
            {
                this.scale = scaleW*2/3;
                this.length = height / 15;
            }
            else
            {
                this.scale = scaleH*2/3;
                this.length = width / 15;
            }
            this.width = width*(float)this.scale;
            this.height = height*(float)this.scale;
            
        }
        public void AddPoints(List<GlueDot> points)
        {
            this.points = points;
           
        }
        public void AddOnePoint(GlueDot point)
        {
            this.seletecPoint = point;

        }
        public void BeginPaint()
        {
            this.Invalidate();
        }

        //  画坐标系
        private void drawCoordinate()
        {
            Pen pen = new Pen(Color.White, 2);
            graphics.DrawLine(pen, origin.X-15, origin.Y, origin.X + 15, origin.Y);
            graphics.DrawLine(pen, origin.X, origin.Y+ 15, origin.X, origin.Y - 15);

        }

        private void drawPoint(Color color, float x, float y,float radius)
        {
            if (radius <= 0)
                return;
            //圆的中心坐标
            PointF centerPosition = new PointF((float)(x * this.scale + origin.X), (float)(origin.Y - y * this.scale));
            //圆半径
            float radiusScaled = (float)(radius * this.scale);

            //圆外接四边形的左上点坐标
            PointF panelPosition = new PointF(centerPosition.X - radiusScaled, centerPosition.Y - radiusScaled);

            //圆外接四边形尺寸
            SizeF arcSize = new SizeF(radiusScaled * 2, radiusScaled * 2);
            RectangleF rect = new RectangleF(panelPosition, arcSize);

            //画刷和画笔
            Brush brush = new SolidBrush(color);
            Pen pen = new Pen(color,2);

            //执行绘图
            //graphics.FillEllipse(brush, rect);
            graphics.DrawArc(pen, rect, 0, 360);
        }

    }
}
