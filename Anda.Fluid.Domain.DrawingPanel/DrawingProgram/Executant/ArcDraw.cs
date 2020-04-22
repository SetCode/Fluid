
using DrawingPanel.EntitySelect.EntityContain;
using DrawingPanel.EntitySelect.EntityHitter;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Executant
{
    public class ArcDraw : DirectiveDraw
    {
        public PointF centerPosition, startPosition, endPosition;
        public float lineWidth, degree, radius;
        private Color lineColor;

        public ArcDraw(PointF centerPosition, PointF startPosition, PointF endPosition, float lineWidth, 
            Color lineColor, float degree)
        {
            this.centerPosition = centerPosition;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.lineWidth = lineWidth;
            this.lineColor = lineColor;

            //圆弧半径
            this.radius = (float)Math.Sqrt(Math.Pow((double)startPosition.X - (double)centerPosition.X, 2) + Math.Pow((double)startPosition.Y - (double)centerPosition.Y, 2));

            if (degree >= 360)
            {
                this.degree = 360;
            }
            else if (degree <= -360)
            {
                this.degree = -360;
            }
            else
            {
                this.degree = degree;
            }
        }

        private PointF TopLeft => new PointF(this.centerPosition.X - this.radius - Properties.Settings.Default.TrackWidth * 0.5f,
                             this.centerPosition.Y - this.radius - Properties.Settings.Default.TrackWidth * 0.5f);
        public RectangleF Rect => new RectangleF(TopLeft, new SizeF(this.radius * 2, this.radius * 2));
        public override void Execute()
        {
            //圆弧的中心坐标
            PointF centerPosition = this.centerPosition;

            //圆弧的起点坐标
            PointF startPosition= this.startPosition;

            //圆弧外接四边形的左上点坐标
            PointF panelPosition = new PointF(centerPosition.X - radius, centerPosition.Y - radius);

            //圆外接四边形尺寸
            SizeF arcSize = new SizeF(radius * 2, radius * 2);
            RectangleF rect = new RectangleF(panelPosition, arcSize);

            //圆弧的起点度数
            float startDegree = 0;
            //先判断是不是中心坐标系坐标轴上的点
            if (startPosition.Y == centerPosition.Y)
            {
                if (startPosition.X > centerPosition.X)
                {
                    startDegree = 0;
                }
                else
                {
                    startDegree = 180;
                }
            }
            else if (startPosition.X == centerPosition.X) 
            {
                if (startPosition.Y < centerPosition.Y)
                {
                    startDegree = 270;
                }
                else
                {
                    startDegree = 90;
                }
            }

            //再判断处于四个象限时的情况
            else
            {
                //对边长度
                double oppositeSide = Math.Abs(startPosition.Y - centerPosition.Y);
                //斜边长度
                double hypotenuse = Math.Sqrt(Math.Pow(startPosition.X - centerPosition.X, 2) + Math.Pow(startPosition.Y - centerPosition.Y, 2));
                //初始角度
                startDegree = (float)(Math.Asin(oppositeSide / hypotenuse)*180/Math.PI);
                //在第一象限时
                if (startPosition.X > centerPosition.X && startPosition.Y < centerPosition.Y)
                {
                    startDegree = 360 - startDegree;
                }
                //在第二象限时
                else if(startPosition.X < centerPosition.X && startPosition.Y < centerPosition.Y)
                {
                    startDegree = 180 + startDegree;
                }
                //在第三象限时
                else if(startPosition.X < centerPosition.X && startPosition.Y > centerPosition.Y)
                {
                    startDegree = 180 - startDegree;
                }
                //在第四象限时
                else if (startPosition.X > centerPosition.X && startPosition.Y > centerPosition.Y)
                {
                    
                }
            }


            //圆弧的角度
            float sweepDegree = -this.degree;

            //画笔
            Pen pen = new Pen(this.lineColor, this.lineWidth);

            pen.DashCap = DashCap.Round;
            //执行绘图
            DrawProgram.Instance.Graphics.DrawArc(pen, rect, startDegree, sweepDegree);
        }

        public override object Clone()
        {
            ArcDraw arc = MemberwiseClone() as ArcDraw;
            arc.centerPosition = centerPosition;
            arc.startPosition = startPosition;
            arc.lineWidth = lineWidth;
            arc.degree = degree;
            arc.lineColor = lineColor;
            return arc;
        }

        public override bool IsHitter(PointF point)
        {
            return new ArcHitter().IsHitting(point, this);
        }

        public override bool IsContain(RectangleF rect)
        {
            return new ArcContain().IsContain(rect, this);
        }
    }
}
