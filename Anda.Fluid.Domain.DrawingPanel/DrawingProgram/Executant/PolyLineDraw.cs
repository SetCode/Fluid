
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
    public class PolyLineDraw : DirectiveDraw
    {
        public PointF[] points;
        public float lineWidth;
        private Color lineColor;
        private bool isArrowLine;
        public float[] pointsX, pointsY;
        public PolyLineDraw(PointF[] points, float lineWidth, Color lineColor, bool isArrowLine)
        {
            this.points = points;
            this.lineWidth = lineWidth;
            this.lineColor = lineColor;
            this.isArrowLine = isArrowLine;
            this.pointsX = new float[points.Length];
            this.pointsY = new float[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.pointsX[i] = points[i].X;
                this.pointsY[i] = points[i].Y;
            }
        }
        private PointF TopLeft => new PointF(this.pointsX.Min() - this.lineWidth * 0.5f, this.pointsY.Min() - this.lineWidth * 0.5f);
        private PointF BottomRight => new PointF(this.pointsX.Max() + this.lineWidth * 0.5f, this.pointsY.Max() + this.lineWidth * 0.5f);
        public RectangleF Rect => new RectangleF(TopLeft, new SizeF(this.BottomRight.X - this.TopLeft.X, this.BottomRight.Y - this.TopLeft.Y));
        public override void Execute()
        {
            //对每个点进行坐标转换
            PointF[] points = new PointF[this.points.Length];
            for (int i = 0; i < this.points.Length; i++)
            {
                points[i] = this.points[i];
            }

            //直线的宽度
            float lineWidth = this.lineWidth;

            //画笔
            Pen pen = new Pen(this.lineColor, lineWidth);

            //如果直线不带箭头
            if (!this.isArrowLine)
            {
                //绘图
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
            }
            //直线带箭头
            else
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.ArrowAnchor;
            }
            DrawProgram.Instance.Graphics.DrawLines(pen, points);
        }
        public override object Clone()
        {
            PolyLineDraw polyLine = MemberwiseClone() as PolyLineDraw;
            polyLine.points = this.points;
            polyLine.lineWidth = lineWidth;
            polyLine.lineColor = lineColor;
            polyLine.isArrowLine = isArrowLine;
            return polyLine;
         }
        public override bool IsHitter(PointF point)
        {
            return new PolyLineHitter().IsHitting(point, this);
        }

        public override bool IsContain(RectangleF rect)
        {
            return new PolyLineContain().IsContain(rect, this);
        }
    }
}
