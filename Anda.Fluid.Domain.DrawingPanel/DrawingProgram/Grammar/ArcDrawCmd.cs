
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.EntitySelect.EntityHitter;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class ArcDrawCmd : DrawCmdLine
    {
        public ArcDraw arc;
        private DotDraw startPoint, endPoint;
        private PointF centerPosition, startPosition,endPosition;
        private float degree;
        private bool enable;
        private bool isClick = false;

        /// <summary>
        /// 圆弧指令（中心点，起始点，颜色，度数）
        /// </summary>
        public ArcDrawCmd(PointF centerPosition, PointF startPosition,PointF endPosition,float degree,bool enable)
        {
            this.centerPosition = DrawingUtils.Instance.CoordinateTrans(centerPosition);
            this.startPosition = DrawingUtils.Instance.CoordinateTrans(startPosition);
            this.endPosition = DrawingUtils.Instance.CoordinateTrans(endPosition);
            this.degree = degree;
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
                this.isClick = value; ;
            }
        }

        public override RectangleF Rect => this.arc.Rect;


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
                if (this.isClick)
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
            this.arc.Execute();
            //this.startPoint.Execute();
            //this.endPoint.Execute();
        }

        public override object Clone()
        {
            ArcDrawCmd arcCmdLine = MemberwiseClone() as ArcDrawCmd;
            arcCmdLine.arc = arc.Clone() as ArcDraw;
            arcCmdLine.centerPosition = centerPosition;
            arcCmdLine.startPosition = startPosition;
            arcCmdLine.degree = degree;
            return arcCmdLine;
        }

        protected override void SetTrack(PointF modelOrigin,Color backColor)
        {
            PointF canvasOrigion = DrawingUtils.Instance.CoordinateTrans(modelOrigin);

            this.startPoint = new DotDraw(new PointF(canvasOrigion.X + startPosition.X, canvasOrigion.Y + startPosition.Y), Properties.Settings.Default.TrackWidth * 0.5f,
                    backColor, Color.Black);
            this.endPoint = new DotDraw(new PointF(canvasOrigion.X + endPosition.X, canvasOrigion.Y + endPosition.Y), Properties.Settings.Default.TrackWidth * 0.5f,
                backColor, Color.Black);

            this.arc = new ArcDraw(new PointF(canvasOrigion.X + centerPosition.X, canvasOrigion.Y + centerPosition.Y),
                 new PointF(canvasOrigion.X + startPosition.X, canvasOrigion.Y + startPosition.Y), 
                 new PointF(canvasOrigion.X + endPosition.X, canvasOrigion.Y + endPosition.Y),
                 Properties.Settings.Default.TrackWidth, backColor, degree);
        }

        public override bool IsHitter(PointF point)
        {
            if (this.arc.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            if (this.arc.IsContain(rect))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
    }
}
