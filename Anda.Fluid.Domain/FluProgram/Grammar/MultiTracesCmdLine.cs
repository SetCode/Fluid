using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MultiTracesCmdLine : CmdLine
    {
        private List<TraceBase> traces = new List<TraceBase>();
        /// <summary>
        /// 轨迹集合
        /// </summary>
        public List<TraceBase> Traces => this.traces;

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

        /// <summary>
        /// 是否开启全局重量模式
        /// </summary>
        [CompareAtt("CMP")]
        public bool IsWholeWtMode = true;

        private double wholeWeight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定全局重量值，单位：mg
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
        /// <summary>
        /// 如果开启了重量控制，该参数指定每个重量值，单位：mg
        /// </summary>
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

        public double OffsetX;

        public double OffsetY;

        public MultiTracesCmdLine() : base(true)
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
            foreach (var trace in this.traces)
            {
                this.correctPoint(trace.Start, patternOldOrigin, coordinateTransformer, patternNewOrigin);
                if (trace is TraceArc)
                {
                    this.correctPoint((trace as TraceArc).Mid, patternOldOrigin, coordinateTransformer, patternNewOrigin);
                }
                //else
                //{
                //    this.correctPoint((trace as TraceArc).Start, patternOldOrigin, coordinateTransformer, patternNewOrigin);
                //    this.correctPoint((trace as TraceArc).Mid, patternOldOrigin, coordinateTransformer, patternNewOrigin);
                //    this.correctPoint((trace as TraceArc).End, patternOldOrigin, coordinateTransformer, patternNewOrigin);
                //}
                this.correctPoint(trace.End, patternOldOrigin, coordinateTransformer, patternNewOrigin);
            }
        }

        private void correctPoint(PointD point, PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // 校正前的机械坐标
            PointD pMachine = (patternOldOrigin.ToSystem() + point).ToMachine();
            // 校正后的机械坐标
            pMachine = coordinateTransformer.Transform(pMachine);
            // 相对系统坐标
            point.X = (pMachine.ToSystem() - patternNewOrigin.ToSystem()).X;
            point.Y = (pMachine.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override object Clone()
        {
            MultiTracesCmdLine multiTracesCmdLine = this.MemberwiseClone() as MultiTracesCmdLine;
            multiTracesCmdLine.traces = new List<TraceBase>();
            foreach (var item in this.traces)
            {
                multiTracesCmdLine.traces.Add(item.Clone() as TraceBase);
            }
            multiTracesCmdLine.IdCode = multiTracesCmdLine.GetHashCode();
            return multiTracesCmdLine;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("MULTITRACES : ");
            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber + ",");
            }
            if (IsWeightControl)
            {
                sb.Append(", ").Append(wholeWeight.ToString("0.000"));
            }
            if (this.traces.Count > 0)
            {
                sb.Append(", ").Append(traces[0].LineStyle + 1);
                sb.Append(" ").Append(traces[0].Start.ToString()).Append(" ")
                    .Append(traces[0].End.ToString()).Append("...");
            }
            return sb.ToString();
        }

    }
}