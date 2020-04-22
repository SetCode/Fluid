
using DrawingPanel.DrawingProgram.Grammar;
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
    public class DotDraw : DirectiveDraw
    {
        public PointF centerPosition;
        public float radius;
        public Color backColor, sideColor;

        public DotDraw(PointF centerPosition, float radius, Color backColor, Color sideColor)
        {
            this.centerPosition = centerPosition;
            this.radius = radius;
            this.backColor = backColor;
            this.sideColor = sideColor;
        }

        private PointF TopLeft => new PointF(this.centerPosition.X - Properties.Settings.Default.TrackWidth * 0.5f,
                     this.centerPosition.Y - Properties.Settings.Default.TrackWidth * 0.5f);
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

            //画刷和画笔
            Brush brush = new SolidBrush(this.backColor);

            //执行绘图
            DrawProgram.Instance.Graphics.FillEllipse(brush, rect);
        }
        public override object Clone()
        {
            DotDraw dot = MemberwiseClone() as DotDraw;
            dot.centerPosition = centerPosition;
            dot.radius = radius;
            dot.backColor = backColor;
            dot.sideColor = sideColor;
            return dot;
        }
        public override bool IsHitter(PointF point)
        {
            return new DotHitter().IsHitting(point, this);
        }

        public override bool IsContain(RectangleF rect)
        {
            return new DotContain().IsContain(rect, this);
        }
    }
}
