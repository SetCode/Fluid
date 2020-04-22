using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Common
{
    /// <summary>
    /// 线段的起始点和终点坐标值
    /// </summary>
    [Serializable]
    public class LineCoordinate : ICloneable
    {
        [CompareAtt("CMP")]
        public double LookOffset;

        [CompareAtt("CMP")]
        public double LookOffsetRevs;

        [CompareAtt("CMP")]
        public PointD Start = new PointD();

        [CompareAtt("CMP")]
        public PointD End = new PointD();

        [CompareAtt("CMP")]
        public double Weight;

        [CompareAtt("CMP")]
        public LineStyle LineStyle = LineStyle.TYPE_1;

        [NonSerialized]
        [CompareAtt("CMP")]
        public LineParam Param = null;

        [CompareAtt("CMP")]
        public LineRepetition Repetition = new LineRepetition();

        /// <summary>
        /// 胶量模式&&总重模式下的点胶数量
        /// </summary>
        [NonSerialized]
        [CompareAtt("CMP")]
        internal int Dots = 0;
        
        public LineCoordinate(PointD start, PointD end)
        {
            Start.X = start.X;
            Start.Y = start.Y;
            End.X = end.X;
            End.Y = end.Y;
        }

        public LineCoordinate(double startX, double startY, double endX, double endY)
        {
            Start.X = startX;
            Start.Y = startY;
            End.X = endX;
            End.Y = endY;
        }

        
        
        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // 校正前的机械坐标
            PointD pStart = (patternOldOrigin.ToSystem() + Start).ToMachine();
            PointD pEnd = (patternOldOrigin.ToSystem() + End).ToMachine();
            // 校正后的机械坐标
            pStart = coordinateTransformer.Transform(pStart);
            pEnd = coordinateTransformer.Transform(pEnd);
            // 相对系统坐标
            Start.X = (pStart.ToSystem() - patternNewOrigin.ToSystem()).X;
            Start.Y = (pStart.ToSystem() - patternNewOrigin.ToSystem()).Y;
            End.X = (pEnd.ToSystem() - patternNewOrigin.ToSystem()).X;
            End.Y = (pEnd.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public virtual object Clone()
        {
            LineCoordinate lineCoordinate = MemberwiseClone() as LineCoordinate;
            lineCoordinate.Start = Start.Clone() as PointD;
            lineCoordinate.End = End.Clone() as PointD;
            lineCoordinate.LineStyle = LineStyle;
            return lineCoordinate;
        }

        /// <summary>
        /// 计算线段长度
        /// </summary>
        /// <returns></returns>
        public double CalculateDistance()
        {
            double dx = End.X - Start.X;
            double dy = End.Y - Start.Y;
            return Math.Sqrt(dx*dx + dy*dy);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Start).Append(End);
            return sb.ToString();
        }

        public LineCoordinate Reverse()
        {
            PointD tmp = new PointD();
            tmp = this.Start;
            this.Start = this.End;
            this.End = tmp;
            return this;
        }
    }
}
