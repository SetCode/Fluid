using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using DrawingPanel.DrawingProgram.Executant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DrawingPanel.Utils;
using Anda.Fluid.Infrastructure.Utils;


namespace DrawingPanel.DrawingProgram.Grammar
{
    public class MultiTracesDrawCmd : DrawCmdLine
    {
        private List<ArcDraw> arcs = new List<ArcDraw>();
        private List<LineDraw> lines = new List<LineDraw>();
        private List<PointF[]> linesData = new List<PointF[]>();
        private List<Tuple<PointF[], float>> arcsData = new List<Tuple<PointF[], float>>();
        private bool enable;
        private bool isClick = false;
        private bool lastTrackIsLine = false;
        public MultiTracesDrawCmd(List<TraceBase> traces,bool enable)
        {
            for (int i = 0; i < traces.Count; i++)
            {
                if (traces[i] is TraceLine)
                {
                    TraceLine line = (TraceLine)traces[i];
                    PointF[] linePoints = new PointF[2];
                    PointF lineStart = DrawingUtils.Instance.CoordinateTrans(new PointF((float)line.Start.X, (float)line.Start.Y));
                    PointF lineEnd = DrawingUtils.Instance.CoordinateTrans(new PointF((float)line.End.X, (float)line.End.Y));
                    linePoints[0] = lineStart;
                    linePoints[1] = lineEnd;
                    this.linesData.Add(linePoints);
                }
                else if (traces[i] is TraceArc)
                {
                    TraceArc traceArc = (TraceArc)traces[i];
                    PointF[] arcPoints = new PointF[3];
                    PointF arcStart = DrawingUtils.Instance.CoordinateTrans(new PointF((float)traceArc.Start.X, (float)traceArc.Start.Y));
                    PointF arcCenter = DrawingUtils.Instance.CoordinateTrans(new PointF((float)traceArc.Center.X, (float)traceArc.Center.Y));
                    PointF arcEnd = DrawingUtils.Instance.CoordinateTrans(new PointF((float)traceArc.End.X, (float)traceArc.End.Y));
                    arcPoints[0] = arcStart;
                    arcPoints[1] = arcCenter;
                    arcPoints[2] = arcEnd;

                    float degree = (float)traceArc.Degree;
                    this.arcsData.Add(new Tuple<PointF[], float>(arcPoints, degree));
                }

                if (i == traces.Count - 1 && traces[i] is TraceLine)
                {
                    this.lastTrackIsLine = true;
                }
            }

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
                float[] LinesTop = new float[this.lines.Count + this.arcs.Count];
                float[] LinesLeft = new float[this.lines.Count + this.arcs.Count];
                float[] LinesBottom = new float[this.lines.Count + this.arcs.Count];
                float[] LinesRight = new float[this.lines.Count + this.arcs.Count];
                for (int i = 0; i < this.lines.Count; i++)
                {
                    LinesTop[i] = this.lines[i].Rect.Top;
                    LinesLeft[i] = this.lines[i].Rect.Left;
                    LinesBottom[i] = this.lines[i].Rect.Bottom;
                    LinesRight[i] = this.lines[i].Rect.Right;
                }
                for (int i = 0; i < this.arcs.Count; i++)
                {
                    LinesTop[i + this.lines.Count] = this.arcs[i].Rect.Top;
                    LinesLeft[i + this.lines.Count] = this.arcs[i].Rect.Left;
                    LinesBottom[i + this.lines.Count] = this.arcs[i].Rect.Bottom;
                    LinesRight[i + this.lines.Count] = this.arcs[i].Rect.Right;
                }
                return new RectangleF(new PointF(LinesLeft.Min(), LinesTop.Min()),
                    new SizeF(LinesRight.Max() - LinesLeft.Min(), LinesBottom.Max() - LinesTop.Min()));
            }
        }

        public override void DrawingPanel()
        {
            foreach (var item in lines)
            {
                item.Execute();
            }
            foreach (var item in arcs)
            {
                item.Execute();
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

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            //构造直线轨迹
            lines.Clear();
            for (int i = 0; i < linesData.Count; i++)
            {
                PointF CanvasOrgion = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);
                PointF startPosition = new PointF(CanvasOrgion.X + linesData[i][0].X, CanvasOrgion.Y + linesData[i][0].Y);
                PointF endPosition = new PointF(CanvasOrgion.X + linesData[i][1].X, CanvasOrgion.Y + linesData[i][1].Y);

                LineDraw line;
                if (i == linesData.Count - 1 && this.lastTrackIsLine)
                {
                    line = new LineDraw(startPosition, endPosition, Properties.Settings.Default.TrackWidth, backColor, true, true);

                }
                else
                {
                    line = new LineDraw(startPosition, endPosition, Properties.Settings.Default.TrackWidth, backColor, false, true);

                }
                this.lines.Add(line);
            }

            //构造圆弧轨迹
            arcs.Clear();
            foreach (var item in arcsData)
            {
                PointF canvasOrigion = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);
                ArcDraw arc = new ArcDraw(new PointF(canvasOrigion.X + item.Item1[1].X, canvasOrigion.Y + item.Item1[1].Y),
                     new PointF(canvasOrigion.X + item.Item1[0].X, canvasOrigion.Y + item.Item1[0].Y),
                     new PointF(canvasOrigion.X + item.Item1[2].X, canvasOrigion.Y + item.Item1[2].Y),
                     Properties.Settings.Default.TrackWidth, backColor, item.Item2);
                this.arcs.Add(arc);
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
            foreach (var item in this.arcs)
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
                    return false;
                }
            }
            foreach (var item in arcs)
            {
                if (!item.IsContain(rect))
                {
                    return false;
                }
            }
            this.IsSelected = !this.IsSelected;
            return true;
        }


        public override object Clone()
        {
            MultiTracesDrawCmd traces = MemberwiseClone() as MultiTracesDrawCmd;
            traces.enable = this.enable;

            traces.linesData = new List<PointF[]>();
            traces.lines = new List<LineDraw>();
            foreach (var item in this.linesData)
            {
                PointF[] linePoints = new PointF[2];
                linePoints[0] = item[0];
                linePoints[1] = item[1];
                traces.linesData.Add(linePoints);
            }
            foreach (var item in this.lines)
            {
                traces.lines.Add(item.Clone() as LineDraw);
            }

            traces.arcsData = new List<Tuple<PointF[], float>>();
            traces.arcs = new List<ArcDraw>();
            foreach (var item in this.arcsData)
            {
                PointF[] arcPoints = new PointF[3];
                arcPoints[0] = item.Item1[0];
                arcPoints[1] = item.Item1[1];
                arcPoints[2] = item.Item1[2];
                float degree = item.Item2;
                traces.arcsData.Add(new Tuple<PointF[], float>(arcPoints, degree));
            }
            foreach (var item in this.arcs)
            {
                traces.arcs.Add(item.Clone() as ArcDraw);
            }

            return traces;
        }
    }
}
