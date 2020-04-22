using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class LineCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        public InspectionKey inspectionKey;

        protected List<LineCoordinate> lineCoordinateList = new List<LineCoordinate>();
        /// <summary>
        /// 所有线段的起始点和终点信息
        /// </summary>
        public List<LineCoordinate> LineCoordinateList
        {
            get { return lineCoordinateList; }
        }

        public override CmdLineType CmdLineName
        {
            get
            {
                if (this.LineMethod == LineMethod.Single)
                    return CmdLineType.直线;
                else if (this.LineMethod == LineMethod.Poly)
                    return CmdLineType.多段线;
                else
                    return CmdLineType.多线段;
            }
        }
        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                return this.GetPointsList();
            }
        }

        [CompareAtt("CMP")]
        public LineMethod LineMethod = LineMethod.Multi;

        /// <summary>
        /// 所使用的线参数类型
        /// </summary>
        [CompareAtt("CMP")]
        public LineStyle LineStyle = LineStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>

        [CompareAtt("CMP")]
        public bool IsWeightControl = false;
        [CompareAtt("CMP")]
        public bool IsWholeWtMode = true;

        private double wholeWeight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        [CompareAtt("CMP")]
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

       
        private double eachWt = 0;

        [CompareAtt("CMP")]
        public double EachWeight
        {
            set
            {
                eachWt = value < 0 ? 0 : value;
            }
            get
            {
                return eachWt;
            }
        }

        public LineCmdLine() : base(true)
        {
        }

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            foreach (LineCoordinate c in lineCoordinateList)
            {
                c.Correct(patternOldOrigin, coordinateTransformer, patternNewOrigin);
            }
        }

        public override object Clone()
        {
            LineCmdLine lineCmdLine = MemberwiseClone() as LineCmdLine;
            lineCmdLine.lineCoordinateList = new List<LineCoordinate>();
            foreach (LineCoordinate c in lineCoordinateList)
            {
                lineCmdLine.lineCoordinateList.Add(c.Clone() as LineCoordinate);
            }
            lineCmdLine.IdCode = lineCmdLine.GetHashCode();
            return lineCmdLine;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            switch (this.LineMethod)
            {
                case LineMethod.Multi:
                    sb.Append("LINES : ");
                    if (this.TrackNumber != null)
                    {
                        sb.Append(this.TrackNumber + ",");
                    }
                    sb.Append((int)LineStyle + 1);
                    break;
                case LineMethod.Single:
                    sb.Append("LINE : ");
                    if (this.TrackNumber != null)
                    {
                        sb.Append(this.TrackNumber + ",");
                    }
                    sb.Append((int)LineStyle + 1);
                    break;
                case LineMethod.Poly:
                    sb.Append("POLYLINE : ");
                    if (this.TrackNumber != null)
                    {
                        sb.Append(this.TrackNumber + ",");
                    }
                    sb.Append((int)LineStyle + 1);
                    break;
            }
            if (IsWeightControl)
            {
                sb.Append(", ").Append(wholeWeight.ToString("0.000"));
            }
            if (lineCoordinateList.Count > 0)
            {
                sb.Append(", Start:")
                    .Append(MachineRel(lineCoordinateList[0].Start))
                    .Append(", End:")
                    .Append(MachineRel(lineCoordinateList[0].End));
            }
            if (lineCoordinateList.Count > 1)
            {
                sb.Append("...");
            }
            return sb.ToString();
        }

        private List<Tuple<PointD, string>> GetPointsList()
        {
            List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
            if (this.LineMethod == LineMethod.Single)
            {
                list.Add(new Tuple<PointD, string>(this.lineCoordinateList[0].Start, "起点"));
                list.Add(new Tuple<PointD, string>(this.lineCoordinateList[0].End, "终点"));
            }
            else if (this.LineMethod== LineMethod.Multi)
            {
                foreach (var item in this.lineCoordinateList)
                {
                    list.Add(new Tuple<PointD, string>(item.Start, "起点"));
                    list.Add(new Tuple<PointD, string>(item.End, "终点"));
                }
            }
            else if (this.LineMethod== LineMethod.Poly)
            {
                for (int i = 0; i < this.lineCoordinateList.Count; i++)
                {
                    //第一条线段是起点和过渡点
                    if (i == 0)
                    {
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].Start, "起点"));
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].End, "过渡点"));
                    }
                    //如果是最后一条线段则是过渡点和终点
                    else if (i == this.lineCoordinateList.Count - 1)
                    {
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].Start, "过渡点"));
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].End, "终点"));
                    }
                    else
                    {
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].Start, "过渡点"));
                        list.Add(new Tuple<PointD, string>(this.lineCoordinateList[i].End, "过渡点"));
                    }
                }
            }
            return list;
        }

    }
}