using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.FluidTrace
{
    /// <summary>
    /// Description：轨迹基类，线段和圆弧
    /// Author：Connor
    /// Date：2019/11/08
    /// </summary>
    [Serializable]
    public abstract class TraceBase : ICloneable
    {
        /// <summary>
        /// 起点
        /// </summary>
        public PointD Start = new PointD();

        /// <summary>
        /// 终点
        /// </summary>
        public PointD End = new PointD();

        /// <summary>
        /// 胶量
        /// </summary>
        public double Weight;

        /// <summary>
        /// 线型，对应领域层的10种LineStyle
        /// </summary>
        public int LineStyle;

        /// <summary>
        /// 提前量微调
        /// </summary>
        public double LookOffset;

        /// <summary>
        /// 反向提前量微调
        /// </summary>
        public double LookOffsetRevs;

        /// <summary>
        /// 长度
        /// </summary>
        public abstract double Length { get; }

        /// <summary>
        /// 轨迹反向
        /// </summary>
        /// <returns></returns>
        public abstract TraceBase Reverse();

        /// <summary>
        /// 根据距起点的长度获取新的截断轨迹
        /// </summary>
        /// <param name="length">距离起点长度</param>
        /// <returns></returns>
        public abstract TraceBase NewTraceFromStart(double length);

        /// <summary>
        /// 轨迹偏移微调
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract void TranslateBy(double x, double y);

        public abstract object Clone();
    }

    public enum TracePointType
    {
        Start,
        Mid,
        End
    }
}
