
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
    public class LineDrawCmd : DrawCmdLine
    {
        private LineDraw line;

        public PointF startPoint, endPoint;
        private bool isArrowLine;
        private bool enable;
        private bool isClick = false;
        /// <summary>
        /// 线段命令（起始点，终点，线宽，颜色，是否带箭头）
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="lineWidth"></param>
        /// <param name="backColor"></param>
        /// <param name="isArrowLine"></param>
        public LineDrawCmd(PointF startPoint, PointF endPoint,bool isArrowLine,bool enable)
        {
            this.startPoint = DrawingUtils.Instance.CoordinateTrans(startPoint);
            this.endPoint = DrawingUtils.Instance.CoordinateTrans(endPoint);
            this.isArrowLine = isArrowLine;
            this.enable = enable;
            this.SetTrack(new PointF(0,0), Properties.Settings.Default.TrackNormalColor);
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


        public override RectangleF Rect => this.line.Rect;


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
            this.line.Execute();
        }
        public override object Clone()
        {
            LineDrawCmd lineCmdLine = MemberwiseClone() as LineDrawCmd;
            lineCmdLine.line = line.Clone() as LineDraw;
            lineCmdLine.startPoint = startPoint;
            lineCmdLine.endPoint = endPoint;
            lineCmdLine.isArrowLine = isArrowLine;
            return lineCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF CanvasOrgion = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            PointF startPosition = new PointF(CanvasOrgion.X + startPoint.X, CanvasOrgion.Y + startPoint.Y);
            PointF endPosition = new PointF(CanvasOrgion.X + endPoint.X, CanvasOrgion.Y + endPoint.Y);
            this.line = new LineDraw(startPosition, endPosition, Properties.Settings.Default.TrackWidth, backColor, isArrowLine,true);
        }

        public override bool IsHitter(PointF point)
        {
            if (this.line.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            if(this.line.IsContain(rect))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
    }
}
