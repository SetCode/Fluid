using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.Crd
{
    /// <summary>
    /// 银宝特殊运动对象结构
    /// </summary>
    public class CrdSymbolLine
    {
        /// <summary>
        /// 轨迹类型：0-线、1-圆弧
        /// </summary>
        public int Type { get; set; } = 0;
        /// <summary>
        /// 轨迹点位
        /// 线模式：1起点、2终点
        /// 圆弧模式：1起点、2圆心、3终点
        /// </summary>
        public List<PointD> Points { get; set; } = new List<PointD>();
        /// <summary>
        /// 轨迹起始角度
        /// </summary>
        public double StartAngle { get; set; } = 0.0;
        /// <summary>
        /// 轨迹终止角度
        /// </summary>
        public double EndAngle { get; set; } = 0.0;


        /// <summary>
        /// 轨迹起始角度
        /// </summary>
        public double TrackStartAngle { get; set; } = 0.0;
        /// <summary>
        /// 轨迹终止角度
        /// </summary>
        public double TrackEndAngle { get; set; } = 0.0;

        /// <summary>
        /// 轨迹弧度
        /// </summary>
        public double TrackSweep { get; set; } = 0.0;

        /// <summary>
        /// 轨迹末尾高度
        /// </summary>
        /// 
        public double EndZ { get; set; } = 0.0;
        /// <summary>
        /// 轨迹旋转方向
        /// 0：顺时针
        /// 1：逆时针
        /// </summary>
        public int Clockwise { get; set; } = 0;

       
    }
}
