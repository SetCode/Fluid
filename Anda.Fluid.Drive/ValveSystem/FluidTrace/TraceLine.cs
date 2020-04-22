using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Drive.ValveSystem.FluidTrace
{
    /// <summary>
    /// Description：线段轨迹
    /// Author：Connor
    /// Date：2019/11/08
    /// </summary>
    [Serializable]
    public class TraceLine : TraceBase
    {
        public TraceLine(PointD start, PointD end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public override double Length => Start.DistanceTo(End);

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Start, this.End);
        }

        public override object Clone()
        {
            TraceLine traceLine = this.MemberwiseClone() as TraceLine;
            traceLine.Start = this.Start.Clone() as PointD;
            traceLine.End = this.End.Clone() as PointD;
            return traceLine;
        }

        public override TraceBase NewTraceFromStart(double length)
        {
            VectorD v = this.End - this.Start;
            PointD newEnd = this.Start + v * length / this.Length;
            TraceLine newTrace = this.Clone() as TraceLine;
            newTrace.End = newEnd;
            return newTrace;
        }

        public override TraceBase Reverse()
        {
            TraceLine reverse = this.Clone() as TraceLine;
            reverse.Start = this.End.Clone() as PointD;
            reverse.End = this.Start.Clone() as PointD;
            return reverse;
        }

        public override void TranslateBy(double x, double y)
        {
            this.Start.X += x;
            this.Start.Y += y;
            this.End.X += x;
            this.End.Y += y;
        }
    }
}
