using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class LineCmd : SupportDirectiveCmd,IPatternWeightable
    {
        public  LineCmdLine lineCmdLine;
        private List<LineCoordinate> lineCoordinateList = new List<LineCoordinate>();
        /// <summary>
        /// 所有线段的起始点和终点信息
        /// </summary>
        public List<LineCoordinate> LineCoordinateList
        {
            get { return lineCoordinateList; }
        }

        /// <summary>
        /// 所使用的线参数类型
        /// </summary>
        public LineStyle LineStyle = LineStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl = false;

        private double wholeWeight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        public double WholeWeight
        {
            set
            {
                wholeWeight = value < 0 ? 0 : value;
            }
            get
            {
                return wholeWeight;
            }
        }
        [NonSerialized]
        private MeasureHeightCmd associatedMeasureHeightCmd = null;

        public MeasureHeightCmd AssociatedMeasureHeightCmd
        {
            get { return associatedMeasureHeightCmd; }
        }


        public LineCmd(RunnableModule runnableModule, LineCmdLine lineCmdLine, MeasureHeightCmd mhCmd) : base(runnableModule, lineCmdLine)
        {
            this.Valve = lineCmdLine.Valve;
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            PointD start, end;
            this.lineCmdLine = lineCmdLine;
            if (!runnableModule.CommandsModule.IsReversePattern)
            {
                foreach (LineCoordinate line in lineCmdLine.LineCoordinateList)
                {
                    start = structure.ToMachine(runnableModule, line.Start);
                    end = structure.ToMachine(runnableModule, line.End);
                    LineCoordinate newLine = new LineCoordinate(start, end);
                    newLine.Weight = line.Weight;
                    newLine.LineStyle = line.LineStyle;
                    newLine.LookOffset = line.LookOffset;//偏移量
                    lineCoordinateList.Add(newLine);
                }
            }
            else
            {
                List<LineCoordinate> lineCoordinateRevs = new List<LineCoordinate>();
                lineCoordinateRevs.AddRange(lineCmdLine.LineCoordinateList);
                lineCoordinateRevs.Reverse();
                foreach (LineCoordinate line in lineCoordinateRevs)
                {                    
                    start = structure.ToMachine(runnableModule, line.End);
                    end = structure.ToMachine(runnableModule, line.Start);
                    LineCoordinate newLine = new LineCoordinate(start, end);
                    newLine.Weight = line.Weight;
                    newLine.LineStyle = line.LineStyle;
                    newLine.LookOffsetRevs = line.LookOffsetRevs;//偏移量
                    lineCoordinateList.Add(newLine);
                }
            }
            
            LineStyle = lineCmdLine.LineStyle;
            IsWeightControl = lineCmdLine.IsWeightControl;
            wholeWeight = lineCmdLine.WholeWeight;
            this.associatedMeasureHeightCmd = mhCmd;

            this.CheckRepeat();

        }
        

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Line(this, coordinateCorrector);
        }
        public Directive ToDirectiveISpray()
        {
            return new Line(this);
        }

        /// <summary>
        /// 进行多条线段的首尾重合比较，适用于所有线段类型
        /// </summary>
        private void CheckRepeat()
        {
            //获得所有线段的首尾点
            List<PointD> allPoints = new List<PointD>();
            for (int i = 0; i < lineCoordinateList.Count; i++)
            {
                allPoints.Add(lineCoordinateList[i].Start);
                allPoints.Add(lineCoordinateList[i].End);
            }

            //获得所有线段的首尾点比较结果
            List<LineRepetition> lineRepetition = MathUtils.CheckLinesPepeat(allPoints);

            //将结果赋给线段，理论上结果和线段是一一对应的关系
            for (int i = 0; i < lineCoordinateList.Count; i++)
            {
                lineCoordinateList[i].Repetition = lineRepetition[i];
            }
        }
    }
}