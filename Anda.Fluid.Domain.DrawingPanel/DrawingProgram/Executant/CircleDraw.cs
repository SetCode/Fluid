
using DrawingPanel.EntitySelect.EntityContain;
using DrawingPanel.EntitySelect.EntityHitter;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Executant
{
    public class CircleDraw : DirectiveDraw
    {
        public PointF centerPosition;
        public float radius, lineWidth;
        private Color lineColor;
        public CircleDraw(PointF centerPosition, float radius, float lineWidth, Color lineColor)
        {
            this.centerPosition = centerPosition;
            this.radius = radius;
            this.lineWidth = lineWidth;
            this.lineColor = lineColor;
        }
        private PointF TopLeft => new PointF(this.centerPosition.X - this.radius - Properties.Settings.Default.TrackWidth * 0.5f,
                             this.centerPosition.Y - this.radius - Properties.Settings.Default.TrackWidth * 0.5f);
        public RectangleF Rect => new RectangleF(TopLeft, new SizeF(this.radius * 2, this.radius * 2));
        public override void Execute()
        {
            //圆的中心坐标
            PointF centerPosition = this.centerPosition;

            //圆半径
            float radius = this.radius;

            //圆外接四边形的左上点坐标
            PointF panelPosition = new PointF(centerPosition.X - radius, centerPosition.Y - radius);

            //圆外接四边形尺寸
            SizeF arcSize = new SizeF(radius * 2, radius * 2);
            RectangleF rect = new RectangleF(panelPosition, arcSize);

            //画笔
            Pen pen = new Pen(this.lineColor, this.lineWidth);

            //执行绘图
            DrawProgram.Instance.Graphics.DrawArc(pen, rect, 0, 360);
        }
        public override object Clone()
        {
            CircleDraw circle = MemberwiseClone() as CircleDraw;
            circle.centerPosition = centerPosition;
            circle.radius = radius;
            circle.lineWidth = lineWidth;
            circle.lineColor = lineColor;
            return circle;
         }
        public override bool IsHitter(PointF point)
        {
            return new CircleHitter().IsHitting(point, this);
        }

        public override bool IsContain(RectangleF rect)
        {
            return new CircleContain().IsContain(rect, this);
        }
    }
}
