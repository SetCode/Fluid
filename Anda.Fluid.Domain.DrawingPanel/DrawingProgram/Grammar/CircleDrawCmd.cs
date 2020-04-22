
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class CircleDrawCmd : DrawCmdLine
    {
        private CircleDraw circle;
        public PointF centerPosition;
        public float radius;
        private bool enable;
        private bool isClick = false;
        /// <summary>
        /// 圆指令（中心点，半径，线宽，颜色）
        /// </summary>
        /// <param name="centerPosition"></param>
        /// <param name="radius"></param>
        /// <param name="lineWidth"></param>
        /// <param name="lineColor"></param>
        public CircleDrawCmd(PointF centerPosition, float radius,bool enable)
        {
            this.centerPosition = DrawingUtils.Instance.CoordinateTrans(centerPosition);
            this.radius = radius;
            this.enable = enable;
            this.SetTrack(new PointF(0, 0), Properties.Settings.Default.TrackNormalColor);
        }
        public override bool IsClick
        {
            get { return this.isClick; }
            set { this.isClick = value; }
        }

        public override RectangleF Rect => this.circle.Rect;

        public override void Setup(PointF origin)
        {
            if (this.IsSelected)
            {
                this.SetTrack(origin, Properties.Settings.Default.TrackSelectedColor);
            }
            else  if(!this.enable)
            {
                this.SetTrack(origin, Properties.Settings.Default.TrackDisableColor);
            }
            else
            {
                if(this.isClick)
                {
                    this.SetTrack(origin, Properties.Settings.Default.TrackClickColor);
                    this.isClick = false;
                }
                else
                {
                    this.SetTrack(origin, Properties.Settings.Default.TrackNormalColor);
                }
               
            }
        }
        public override void SetupDisable(PointF origin)
        {
            this.SetTrack(origin, Properties.Settings.Default.TrackDisableColor);
        }
        public override void DrawingPanel()
        {
            this.circle.Execute();
        }
        public override object Clone()
        {
            CircleDrawCmd circleCmdLine = MemberwiseClone() as CircleDrawCmd;
            circleCmdLine.circle = circle.Clone() as CircleDraw;
            circleCmdLine.centerPosition = centerPosition;
            circleCmdLine.radius = radius;
            return circleCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF canvasOrgion= DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            this.circle = new CircleDraw(new PointF(canvasOrgion.X + centerPosition.X, canvasOrgion.Y + centerPosition.Y),
                    radius, Properties.Settings.Default.TrackWidth, backColor);

            this.centerPosition = new PointF(canvasOrgion.X + centerPosition.X, canvasOrgion.Y + centerPosition.Y);
        }

        public override bool IsHitter(PointF point)
        {
            if (this.circle.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
        public override bool IsContain(RectangleF rect)
        {
            if (this.circle.IsContain(rect))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
    }
}
