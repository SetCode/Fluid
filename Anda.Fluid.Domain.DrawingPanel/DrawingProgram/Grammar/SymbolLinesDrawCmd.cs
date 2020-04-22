using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Infrastructure.Utils;
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
    public class SymbolLinesDrawCmd : DrawCmdLine
    {
        private List<ArcDraw> arcs = new List<ArcDraw>();
        private List<LineDraw> lines = new List<LineDraw>();
        private List<PointF[]> linesData = new List<PointF[]>();
        private List<Tuple<PointF[], float>> arcsData = new List<Tuple<PointF[], float>>();
        private bool enable;
        private bool isClick = false;
        private bool lastTrackIsLine = false;

        public SymbolLinesDrawCmd(List<SymbolLine> Symbols, bool enable)
        {
            for (int i = 0; i < Symbols.Count; i++)
            {
                if (Symbols[i].symbolType == SymbolType.Line)
                {
                    PointF[] linePoints = new PointF[2];
                    PointF lineStart = DrawingUtils.Instance.CoordinateTrans(new PointF((float)Symbols[i].symbolPoints[0].X, (float)Symbols[i].symbolPoints[0].Y));
                    PointF lineEnd = DrawingUtils.Instance.CoordinateTrans(new PointF((float)Symbols[i].symbolPoints[1].X, (float)Symbols[i].symbolPoints[1].Y));
                    linePoints[0] = lineStart;
                    linePoints[1] = lineEnd;
                    this.linesData.Add(linePoints);
                }
                else if (Symbols[i].symbolType == SymbolType.Arc)
                {
                    PointF[] arcPoints = new PointF[4];
                    PointF arcStart = DrawingUtils.Instance.CoordinateTrans(new PointF((float)Symbols[i].symbolPoints[0].X, (float)Symbols[i].symbolPoints[0].Y));
                    PointF arcCenter = DrawingUtils.Instance.CoordinateTrans(new PointF((float)Symbols[i].symbolPoints[1].X, (float)Symbols[i].symbolPoints[1].Y));
                    PointF arcEnd = DrawingUtils.Instance.CoordinateTrans(new PointF((float)Symbols[i].symbolPoints[2].X, (float)Symbols[i].symbolPoints[2].Y));

                    //求圆心坐标
                    PointF arcPointCenter = MathUtils.CalculateCircleCenter(arcStart, arcCenter, arcEnd);

                    arcPoints[0] = arcStart;
                    arcPoints[1] = arcCenter;
                    arcPoints[2] = arcEnd;
                    arcPoints[3] = arcPointCenter;

                    float degree = -(float)MathUtils.CalculateDegree(new Anda.Fluid.Infrastructure.Common.PointD((double)arcStart.X, (double)arcStart.Y),
                        new Anda.Fluid.Infrastructure.Common.PointD((double)arcCenter.X, (double)arcCenter.Y),
                        new Anda.Fluid.Infrastructure.Common.PointD((double)arcEnd.X, (double)arcEnd.Y),
                        new Anda.Fluid.Infrastructure.Common.PointD((double)arcPointCenter.X, (double)arcPointCenter.Y));

                    //float degree = (float)MathUtils.CalculateDegree(Symbols[i].symbolPoints[0], Symbols[i].symbolPoints[2], Symbols[i].symbolPoints[1], Symbols[i].clockwise);

                    this.arcsData.Add(new Tuple<PointF[], float>(arcPoints, degree));
                }

                if (i == Symbols.Count - 1 && Symbols[i].symbolType == SymbolType.Line)
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

                PointF centerPoint = MathUtils.CalculateCircleCenter(item.Item1[0], item.Item1[1], item.Item1[2]);
                float degree = -(float)MathUtils.CalculateDegree(new Anda.Fluid.Infrastructure.Common.PointD((double)item.Item1[0].X, (double)item.Item1[0].Y),
                       new Anda.Fluid.Infrastructure.Common.PointD((double)item.Item1[1].X, (double)item.Item1[1].Y),
                       new Anda.Fluid.Infrastructure.Common.PointD((double)item.Item1[2].X, (double)item.Item1[2].Y),
                       new Anda.Fluid.Infrastructure.Common.PointD((double)centerPoint.X, (double)centerPoint.Y));

                ArcDraw arc = new ArcDraw(new PointF(canvasOrigion.X + centerPoint.X, canvasOrigion.Y + centerPoint.Y),
                     new PointF(canvasOrigion.X + item.Item1[0].X, canvasOrigion.Y + item.Item1[0].Y),
                     new PointF(canvasOrigion.X + item.Item1[2].X, canvasOrigion.Y + item.Item1[2].Y),
                     Properties.Settings.Default.TrackWidth, backColor, degree);



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
            SymbolLinesDrawCmd symbolLines = MemberwiseClone() as SymbolLinesDrawCmd;
            symbolLines.enable = this.enable;

            symbolLines.linesData = new List<PointF[]>();
            symbolLines.lines = new List<LineDraw>();
            foreach (var item in this.linesData)
            {
                PointF[] linePoints = new PointF[2];
                linePoints[0] = item[0];
                linePoints[1] = item[1];
                symbolLines.linesData.Add(linePoints);
            }
            foreach (var item in this.lines)
            {
                symbolLines.lines.Add(item.Clone() as LineDraw);
            }

            symbolLines.arcsData = new List<Tuple<PointF[], float>>();
            symbolLines.arcs = new List<ArcDraw>();
            foreach (var item in this.arcsData)
            {
                PointF[] arcPoints = new PointF[3];
                arcPoints[0] = item.Item1[0];
                arcPoints[1] = item.Item1[1];
                arcPoints[2] = item.Item1[2];
                float degree = item.Item2;
                symbolLines.arcsData.Add(new Tuple<PointF[], float>(arcPoints, degree));
            }
            foreach (var item in this.arcs)
            {
                symbolLines.arcs.Add(item.Clone() as ArcDraw);
            }

            return symbolLines;
        }
    }
}
