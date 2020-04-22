
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
    public class LinesDrawCmd : DrawCmdLine
    {
        protected LineDraw[] lines;
        private Line2Points[] linePoints;
        private bool isArrowLines;
        private bool enable;
        private bool isClick=false;
        /// <summary>
        /// 多线段指令（线段集合，线宽，颜色，是否带箭头）
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="linesWidth"></param>
        /// <param name="linesColor"></param>
        /// <param name="isArrowlines"></param>
        public LinesDrawCmd(Line2Points[] lines, bool isArrowLines,bool enable)
        {
            this.lines = new LineDraw[lines.Length];
            this.linePoints = lines;
            this.isArrowLines = isArrowLines;
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

        public override RectangleF Rect
        {
            get
            {
                float[] LinesTop = new float[this.lines.Length];
                float[] LinesLeft = new float[this.lines.Length];
                float[] LinesBottom = new float[this.lines.Length];
                float[] LinesRight = new float[this.lines.Length];
                for (int i = 0; i < this.lines.Length; i++)
                {
                    LinesTop[i] = this.lines[i].Rect.Top;
                    LinesLeft[i] = this.lines[i].Rect.Left;
                    LinesBottom[i] = this.lines[i].Rect.Bottom;
                    LinesRight[i] = this.lines[i].Rect.Right;
                }
                return new RectangleF(new PointF(LinesLeft.Min(), LinesTop.Min()), 
                    new SizeF(LinesRight.Max() - LinesLeft.Min(), LinesBottom.Max() - LinesTop.Min()));
            }
        }

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
                if(isClick)
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

        }
        public override void DrawingPanel()
        {
            foreach (var item in this.lines)
            {
                item.Execute();
            }
        }
        public override object Clone()
        {
            LinesDrawCmd linesCmdLine = MemberwiseClone() as LinesDrawCmd;
            linesCmdLine.linePoints = linePoints.Clone() as Line2Points[];
            linesCmdLine.isArrowLines = isArrowLines;
            linesCmdLine.lines = new LineDraw[this.lines.Length];
            for (int i = 0; i < this.lines.Length; i++)
            {
                linesCmdLine.lines[i] = this.lines[i].Clone() as LineDraw;
            }
            return linesCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF canvavsOrigin = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);
            for (int i = 0; i < lines.Length; i++)
            {
                this.lines[i] = new LineDraw(new PointF(linePoints[i].StartPoint.X + canvavsOrigin.X, linePoints[i].StartPoint.Y + canvavsOrigin.Y),
                    new PointF(linePoints[i].EndPoint.X + canvavsOrigin.X, linePoints[i].EndPoint.Y + canvavsOrigin.Y), Properties.Settings.Default.TrackWidth, backColor, isArrowLines,true);
            }
        }

        public override bool IsHitter(PointF point)
        {
            foreach (var item in this.lines)
            {
                if (item.IsHitter(point))
                {
                    this.IsSelected = !this.IsSelected;
                    return true; 
                }
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            foreach (var item in this.lines)
            {
                if (!item.IsContain(rect))
                {
                    return false ;
                }
            }
            this.IsSelected = !this.IsSelected;
            return true;
        }
    }
    /// <summary>
    /// 包含起点和终点的线段
    /// </summary>
    public class Line2Points:ICloneable
    {
        private PointF startPoint, endPoint;
        public Line2Points(PointF startPoint, PointF endPoint)
        {
            this.startPoint = DrawingUtils.Instance.CoordinateTrans(startPoint);
            this.endPoint = DrawingUtils.Instance.CoordinateTrans(endPoint);
        }
        public PointF StartPoint => this.startPoint;
        public PointF EndPoint => this.endPoint;

        public object Clone()
        {
            Line2Points line2Points = MemberwiseClone() as Line2Points;
            line2Points.startPoint = startPoint;
            line2Points.endPoint = endPoint;
            return line2Points;
        }
    }
}
