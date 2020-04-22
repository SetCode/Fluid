using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.FluidTrace
{
    /// <summary>
    /// Description：圆弧轨迹
    /// Author：Connor
    /// Date：2019/11/08
    /// </summary>
    [Serializable]
    public class TraceArc : TraceBase
    {
        /// <summary>
        /// 中间点
        /// </summary>
        public PointD Mid = new PointD();

        public TraceArc(PointD start, PointD mid, PointD end)
        {
            this.Start = start;
            this.Mid = mid;
            this.End = end;
        }

        /// <summary>
        /// 圆心
        /// </summary>
        public PointD Center => MathUtils.CalculateCircleCenter(this.Start, this.Mid, this.End);

        /// <summary>
        /// 半径
        /// </summary>
        public double Raduis => this.Center.DistanceTo(this.Start);

        /// <summary>
        /// 逆时针为正，顺时针为负，(-360, 360)
        /// </summary>
        public double Degree => MathUtils.CalculateDegree(this.Start, this.Mid, this.End, this.Center);

        /// <summary>
        /// 弧长
        /// </summary>
        public override double Length => Math.Abs(this.Degree) * Math.PI / 180 * this.Raduis;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Start, this.Mid, this.End);
        }

        public override object Clone()
        {
            TraceArc traceArc = this.MemberwiseClone() as TraceArc;
            traceArc.Start = this.Start.Clone() as PointD;
            traceArc.Mid = this.Mid.Clone() as PointD;
            traceArc.End = this.End.Clone() as PointD;
            return traceArc;
        }

        public override TraceBase NewTraceFromStart(double length)
        {
            double degree = this.Degree * length / this.Length;
            TraceArc newTrace = this.Clone() as TraceArc;
            newTrace.Mid = this.Start.Rotate(this.Center, degree / 2);
            newTrace.End = this.Start.Rotate(this.Center, degree);
            return newTrace;
        }

        public override TraceBase Reverse()
        {
            TraceArc reverse = this.Clone() as TraceArc;
            reverse.Start = this.End.Clone() as PointD;
            reverse.End = this.Start.Clone() as PointD;
            return reverse;
        }

        public override void TranslateBy(double x, double y)
        {
            this.Start.X += x;
            this.Start.Y += y;
            this.Mid.X += x;
            this.Mid.Y += y;
            this.End.X += x;
            this.End.Y += y;
        }
    }
}
