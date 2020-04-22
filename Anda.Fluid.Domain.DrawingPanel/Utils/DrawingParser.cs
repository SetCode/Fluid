using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using DrawingPanel.DrawingProgram;
using DrawingPanel.DrawingProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawingPanel.Utils
{
    /// <summary>
    /// 解析器
    /// </summary>
    public class DrawingParser
    {
        private FluidProgram fluidProgram;
        private DrawWorkPiece _drawWorkpiece;
        private List<DrawPattern> _drawPatterns;
        /// <summary>
        /// 将点胶程序解析为绘图程序
        /// </summary>
        /// <param name="fluidProgram"></param>
        /// <param name="workpiece"></param>
        /// <param name="patterns"></param>
        public void ParseFluidProgram(FluidProgram fluidProgram, out DrawWorkPiece drawWorkpiece, out List<DrawPattern> drawPatterns)
        {
            //初始化
            this.fluidProgram = fluidProgram;
            this._drawWorkpiece = null;
            if (this._drawPatterns == null)
            {
                this._drawPatterns = new List<DrawPattern>();
            }
            else
            {
                this._drawPatterns.Clear();
            }          

            #region 首先构建drawPatterns
            for (int i = 0; i < this.fluidProgram.Patterns.Count; i++)
            {
                //为drawPattern赋值原点
                PointF patternOrigin = new PointF(0, 0);
                //patternOrigin.X = (float)this.fluidProgram.Patterns[i].Origin.X;
                //patternOrigin.Y = (float)this.fluidProgram.Patterns[i].Origin.Y;
                DrawPattern drawPattern = new DrawPattern(patternOrigin);

                _drawPatterns.Add(drawPattern);

                //为drawPattern添加绘图指令
                for (int j = 0; j < this.fluidProgram.Patterns[i].CmdLineList.Count; j++)
                {
                    this.Parse(this.fluidProgram.Patterns[i].CmdLineList[j], _drawPatterns[i]);
                }
            }
            #endregion

            #region 构建drawWorkpiece

            //为drawWorkpiece赋原点
            PointF _workpieceOrigin = new PointF(0, 0);
            _drawWorkpiece = new DrawWorkPiece(_workpieceOrigin);

            //为drawWorkpiece添加绘图指令
            for (int i = 0; i < this.fluidProgram.Workpiece.CmdLineList.Count; i++)
            {
                this.Parse(this.fluidProgram.Workpiece.CmdLineList[i], _drawWorkpiece);
            }

            #endregion

            drawWorkpiece = _drawWorkpiece;
            drawPatterns = _drawPatterns;
        }

        /// <summary>
        /// 将程序指令解析为绘图指令，添加到绘图workpiece或者pattern中
        /// </summary>
        /// <param name="cmdLine"></param>
        /// <param name="pattern"></param>
        private void Parse(CmdLine cmdLine, DrawPattern pattern)
        {
            if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arc = cmdLine as ArcCmdLine;
                PointF centerPosition = new PointF((float)arc.Center.X, (float)arc.Center.Y);
                PointF startPosition = new PointF((float)arc.Start.X, (float)arc.Start.Y);
                PointF endPosition = new PointF((float)arc.End.X, (float)arc.End.Y);
                float degree = (float)arc.Degree;
                ArcDrawCmd arcDrawCmd = new ArcDrawCmd(centerPosition, startPosition,endPosition, degree, arc.Enabled);           
                pattern.Add(arcDrawCmd);
            }
            //如果是DoPattern指令
            else if(cmdLine is DoCmdLine)
            {
                if (this._drawPatterns.Count <= 0)
                {
                    pattern.Add(null);
                }
                else if (pattern.GetType().Equals(typeof(DrawWorkPiece)))
                {
                    DoCmdLine doPattern = cmdLine as DoCmdLine;

                    //判断是paternList里的哪一个pattern
                    int index = 0;
                    for (int i = 0; i < fluidProgram.Patterns.Count; i++)
                    {
                        if (fluidProgram.Patterns[i].Name.Equals(doPattern.PatternName))
                        {
                            index = i;
                        }
                    }
                    PointF origin = new PointF((float)doPattern.Origin.X, (float)doPattern.Origin.Y);
                    DoPatternDrawCmd doPatternDrawCmd = new DoPatternDrawCmd(this._drawPatterns[index], origin,doPattern.Enabled);

                    pattern.Add(doPatternDrawCmd);
                }
                else
                {
                    pattern.Add(null);
                }
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                if (pattern.GetType().Equals(typeof(DrawWorkPiece)))
                {
                    StepAndRepeatCmdLine array = cmdLine as StepAndRepeatCmdLine;

                    //判断是paternList里的哪一个pattern
                    int index = 0;
                    for (int i = 0; i < fluidProgram.Patterns.Count; i++)
                    {
                        if (fluidProgram.Patterns[i].Name.Equals(array.PatternName))
                        {
                            index = i;
                        }
                    }
                    PointF[] points = new PointF[array.DoCmdLineList.Count];
                    for (int i = 0; i < array.DoCmdLineList.Count; i++)
                    {
                        points[i] = new PointF((float)array.DoCmdLineList[i].Origin.X, (float)array.DoCmdLineList[i].Origin.Y); 
                    }

                    ArrayDrawCmd arrayDrawCmd = new ArrayDrawCmd(this._drawPatterns[index], points,array.Enabled);

                    pattern.Add(arrayDrawCmd);
                }
                else
                {
                    pattern.Add(null);
                }

            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circle = cmdLine as CircleCmdLine;
                PointF centerPosition = new PointF((float)circle.Center.X, (float)circle.Center.Y);
                float radius = (float)Math.Sqrt(Math.Pow(Math.Abs(circle.Start.X - circle.Center.X), 2) + Math.Pow(Math.Abs(circle.Start.Y - circle.Center.Y), 2));
                CircleDrawCmd circleDrawCmd = new CircleDrawCmd(centerPosition, radius,circle.Enabled);

                pattern.Add(circleDrawCmd);
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                if (pattern.GetType().Equals(typeof(DrawWorkPiece)))
                {
                    DoMultiPassCmdLine doMultiPass = cmdLine as DoMultiPassCmdLine;

                    //判断是paternList里的哪一个pattern
                    int index = 0;
                    for (int i = 0; i < fluidProgram.Patterns.Count; i++)
                    {
                        if (fluidProgram.Patterns[i].Name.Equals(doMultiPass.PatternName))
                        {
                            index = i;
                        }
                    }

                    PointF position = new PointF((float)doMultiPass.Origin.X, (float)doMultiPass.Origin.Y);
                    DoMultiPassDrawCmd doMultiDrawCmd = new DoMultiPassDrawCmd(this._drawPatterns[index], position,doMultiPass.Enabled);

                    pattern.Add(doMultiDrawCmd);
                }
                else
                {
                    pattern.Add(null);
                }
                
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dot = cmdLine as DotCmdLine;
                PointF centerPosition = new PointF((float)dot.Position.X, (float)dot.Position.Y);
                DotDrawCmd dotDrawCmd = new DotDrawCmd(centerPosition,dot.Enabled);

                pattern.Add(dotDrawCmd);
            }
            else if (cmdLine is MeasureHeightCmdLine)
            {
                MeasureHeightCmdLine height = cmdLine as MeasureHeightCmdLine;
                PointF position = new PointF((float)height.Position.X, (float)height.Position.Y);
                HeightDrawCmd heightDrawCmd = new HeightDrawCmd(position, height.Enabled);

                pattern.Add(heightDrawCmd);
            }
            ///包含line、lines和polyline
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine line = cmdLine as LineCmdLine;
                int jointCount = 0;
                if (line.LineCoordinateList.Count == 1)
                {
                    PointF startPoint = new PointF((float)line.LineCoordinateList[0].Start.X, (float)line.LineCoordinateList[0].Start.Y);
                    PointF endPoint = new PointF((float)line.LineCoordinateList[0].End.X, (float)line.LineCoordinateList[0].End.Y);
                    LineDrawCmd lineDrawCmd = new LineDrawCmd(startPoint, endPoint, true,line.Enabled);

                    pattern.Add(lineDrawCmd);
                }
                else if (line.LineCoordinateList.Count > 1)
                {
                    Line2Points[] lines = new Line2Points[line.LineCoordinateList.Count];

                    for (int i = 0; i < line.LineCoordinateList.Count; i++)
                    {
                        PointF startPoint = new PointF((float)line.LineCoordinateList[i].Start.X, (float)line.LineCoordinateList[i].Start.Y);
                        PointF endPoint = new PointF((float)line.LineCoordinateList[i].End.X, (float)line.LineCoordinateList[i].End.Y);
                        lines[i] = new Line2Points(startPoint, endPoint);

                        //判断是不是polyline(如果所有相邻线段首尾点一致，则为polyline)                     
                        if (i > 0 && lines[i].StartPoint == lines[i - 1].EndPoint)
                        {
                            jointCount++;
                        }
                    }

                    //如果是polyline
                    if (jointCount == line.LineCoordinateList.Count - 1)
                    {
                        PointF[] points = new PointF[line.LineCoordinateList.Count + 1];
                        for (int i = 0; i < lines.Length + 1; i++)
                        {
                            if (i == 0)
                            {
                                points[i] = new PointF((float)lines[0].StartPoint.X, (float)lines[0].StartPoint.Y);
                            }
                            else
                            {
                                points[i] = new PointF((float)lines[i - 1].EndPoint.X, (float)lines[i - 1].EndPoint.Y);
                            }
                        }
                        PolyLineDrawCmd polyLineDrawCmd = new PolyLineDrawCmd(points,line.Enabled);
                        pattern.Add(polyLineDrawCmd);
                    }
                    //如果是lines
                    else
                    {
                        LinesDrawCmd linesDrawCmd = new LinesDrawCmd(lines,true,line.Enabled);
                        pattern.Add(linesDrawCmd);
                    }
                }

            }

            else if (cmdLine is MarkCmdLine)
            {
                MarkCmdLine mark = cmdLine as MarkCmdLine;
                PointF position = new PointF((float)mark.PosInPattern.X, (float)mark.PosInPattern.Y);
                MarkDrawCmd markDrawCmd = new MarkDrawCmd(position,mark.Enabled, MarkType.NormalMark);

                pattern.Add(markDrawCmd);
            }

            else if (cmdLine is BadMarkCmdLine)
            {
                BadMarkCmdLine mark = cmdLine as BadMarkCmdLine;
                PointF position = new PointF((float)mark.Position.X, (float)mark.Position.Y);
                MarkDrawCmd markDrawCmd = new MarkDrawCmd(position, mark.Enabled,MarkType.BadMark);

                pattern.Add(markDrawCmd);
            }

            else if (cmdLine is NozzleCheckCmdLine)
            {
                NozzleCheckCmdLine mark = cmdLine as NozzleCheckCmdLine;
                PointF position = new PointF((float)mark.Position.X, (float)mark.Position.Y);
                MarkDrawCmd markDrawCmd = new MarkDrawCmd(position, mark.Enabled, MarkType.CheckDotMark);

                pattern.Add(markDrawCmd);
            }

            else if (cmdLine is SnakeLineCmdLine)
            {
                SnakeLineCmdLine snake = cmdLine as SnakeLineCmdLine;

                Line2Points[] lines = new Line2Points[snake.LineCoordinateList.Count];
                for (int i = 0; i < snake.LineCoordinateList.Count; i++)
                {
                    PointF startPoint = new PointF((float)snake.LineCoordinateList[i].Start.X, (float)snake.LineCoordinateList[i].Start.Y);
                    PointF endPoint = new PointF((float)snake.LineCoordinateList[i].End.X, (float)snake.LineCoordinateList[i].End.Y);
                    lines[i] = new Line2Points(startPoint, endPoint);
                }

                SnakeLineDrawCmd snakeLineDrawCmd = new SnakeLineDrawCmd(lines, true ,snake.Enabled);

                pattern.Add(snakeLineDrawCmd);
            }
            else if(cmdLine is SymbolLinesCmdLine)
            {
                SymbolLinesCmdLine symbolLines = cmdLine as SymbolLinesCmdLine;

                SymbolLinesDrawCmd symbolLinesDrawCmd = new SymbolLinesDrawCmd(symbolLines.Symbols, symbolLines.Enabled);

                pattern.Add(symbolLinesDrawCmd);
            }
            else if(cmdLine is MultiTracesCmdLine)
            {
                MultiTracesCmdLine tracesCmdLine=cmdLine as MultiTracesCmdLine;
                MultiTracesDrawCmd tracesDrawCmd=new MultiTracesDrawCmd(tracesCmdLine.Traces,tracesCmdLine.Enabled);
                pattern.Add(tracesDrawCmd);
            }

            //如果是别的逻辑指令，为了保证和程序指令一致性，添加一个null进去
            else
            {
                pattern.Add(null);
            }
        }

    }
}
