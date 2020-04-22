
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
    public class PolyLineDrawCmd : DrawCmdLine
    {
        private PolyLineDraw polyLine;
        private PointF[] points;
        private bool enable;
        private bool isClick=false;

        /// <summary>
        /// 多段线指令（点集合，线宽，颜色，是否带箭头）
        /// </summary>
        /// <param name="points"></param>
        /// <param name="lineWidth"></param>
        /// <param name="lineColor"></param>
        /// <param name="isArrowLine"></param>
        public PolyLineDrawCmd(PointF[] points,bool enable)
        {
            this.points = points;
            this.enable = enable;
            this.SetTrack(new PointF(0, 0), Properties.Settings.Default.TrackNormalColor);
        }
        public override bool IsClick
        {
            get
            {
                return this.isClick;
            }

            set
            {
                this.isClick = value;
            }
        }

        public override RectangleF Rect => this.polyLine.Rect;

        public override void Setup(PointF origin)
        {
            if (this.IsSelected)
            {
                this.SetTrack(origin, Properties.Settings.Default.TrackSelectedColor);
            }
            else if (!this.enable)
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
            this.polyLine.Execute();
        }
        public override object Clone()
        {
            PolyLineDrawCmd polyLineCmdLine = MemberwiseClone() as PolyLineDrawCmd;
            polyLineCmdLine.polyLine = polyLine.Clone() as PolyLineDraw;
            polyLineCmdLine.points = points.Clone() as PointF[];
            return polyLineCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF canvasOrigin = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            PointF[] points = new PointF[this.points.Length];
            for (int i = 0; i < this.points.Length; i++)
            {
                points[i] = new PointF(this.points[i].X + canvasOrigin.X, this.points[i].Y + canvasOrigin.Y);
            }
            this.polyLine = new PolyLineDraw(points, Properties.Settings.Default.TrackWidth, backColor, true);
        }
        public override bool IsHitter(PointF point)
        {
            if (this.polyLine.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            if (this.polyLine.IsContain(rect))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
    }
}
