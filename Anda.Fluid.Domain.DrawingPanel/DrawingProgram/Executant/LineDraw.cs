
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
    public class LineDraw : DirectiveDraw
    {
        public PointF startPoint, endPoint;
        public float lineWidth;
        public Color backColor;
        private bool isArrowLine;
        private bool isRound;
        public LineDraw(PointF startPoint, PointF endPoint,float lineWidth, Color backColor, bool isArrowLine,bool isRound)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.lineWidth = lineWidth;
            this.backColor = backColor;
            this.isArrowLine = isArrowLine;
            this.isRound = isRound;
        }

        private float Top =>  Math.Min(startPoint.Y, endPoint.Y) - this.lineWidth;
        private float Bottom => Math.Max(startPoint.Y, endPoint.Y) + this.lineWidth;
        private float Left => Math.Min(startPoint.X, endPoint.X) - this.lineWidth;
        private float Right => Math.Max(startPoint.X, endPoint.X) + this.lineWidth;
        public RectangleF Rect => new RectangleF(new PointF(Left, Top), new SizeF(this.Right - this.Left, this.Bottom - this.Top));


        public override void Execute()
        {

            PointF startPoint = this.startPoint;

            PointF endPoint = this.endPoint;

            float lineWidth = this.lineWidth;

            Pen pen = new Pen(this.backColor, lineWidth);
            if(this.isArrowLine)
            {
                pen.EndCap = LineCap.ArrowAnchor;
                if (isRound)
                {
                    pen.StartCap = LineCap.Round;
                }
                else
                {
                    pen.StartCap = LineCap.Square ;
                }
            }
            else
            {
                if (isRound)
                {
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Round;
                }
                else
                {
                    pen.EndCap = LineCap.Square;
                    pen.StartCap = LineCap.Square;
                }
            }

            //执行绘图
            DrawProgram.Instance.Graphics.DrawLine(pen, startPoint, endPoint);
        }
        public override object Clone()
        {
            LineDraw line = MemberwiseClone() as LineDraw;
            line.startPoint = startPoint;
            line.endPoint = endPoint;
            line.lineWidth = lineWidth;
            line.backColor = backColor;
            line.isArrowLine = isArrowLine;
            return line;
         }
        public override bool IsHitter(PointF point)
        {
            return new LineHitter().IsHitting(point, this);
        }

        public override bool IsContain(RectangleF rect)
        {
            return new LineContain().IsContain(rect, this);
        }
    }
}
