using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class SnakeLineCmdLine : LineCmdLine
    {
        [CompareAtt("CMP")]
        public bool IsContinuous { get; set; } = false;
        [CompareAtt("CMP")]
        private PointD point1 = new PointD();
        /// <summary>
        /// 第一个点
        /// </summary>
        public PointD Point1
        {
            get { return point1; }
        }
        [CompareAtt("CMP")]
        private PointD point2 = new PointD();
        /// <summary>
        /// 第二个点
        /// </summary>
        public PointD Point2
        {
            get { return point2; }
        }
        [CompareAtt("CMP")]
        private PointD point3 = new PointD();
        /// <summary>
        /// 第三个点
        /// </summary>
        public PointD Point3
        {
            get { return point3; }
        }

        public override CmdLineType CmdLineName => CmdLineType.蛇形线;

        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                list.Add(new Tuple<PointD, string>(this.point1, "第一个点"));
                list.Add(new Tuple<PointD, string>(this.point2, "第二个点"));
                list.Add(new Tuple<PointD, string>(this.point3, "第三个点"));
                return list;
            }
        }

        [CompareAtt("CMP")]
        private int lineNumbers = 2;
        /// <summary>
        /// 线的个数，最少为2个
        /// </summary>
        public int LineNumbers
        {
            get { return lineNumbers; }
            set { lineNumbers = value < 2 ? 2 : value; }
        }

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // 校正前的机械坐标
            PointD p1 = (patternOldOrigin.ToSystem() + point1).ToMachine();
            PointD p2 = (patternOldOrigin.ToSystem() + point2).ToMachine();
            PointD p3 = (patternOldOrigin.ToSystem() + point3).ToMachine();
            // 校正后的机械坐标
            p1 = coordinateTransformer.Transform(p1);
            p2 = coordinateTransformer.Transform(p2);
            p3 = coordinateTransformer.Transform(p3);
            // 相对系统坐标
            point1.X = (p1.ToSystem() - patternNewOrigin.ToSystem()).X;
            point1.Y = (p1.ToSystem() - patternNewOrigin.ToSystem()).Y;
            point2.X = (p2.ToSystem() - patternNewOrigin.ToSystem()).X;
            point2.Y = (p2.ToSystem() - patternNewOrigin.ToSystem()).Y;
            point3.X = (p3.ToSystem() - patternNewOrigin.ToSystem()).X;
            point3.Y = (p3.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override object Clone()
        {
            SnakeLineCmdLine snakeLineCmdLine = MemberwiseClone() as SnakeLineCmdLine;
            snakeLineCmdLine.point1 = point1.Clone() as PointD;
            snakeLineCmdLine.point2 = point2.Clone() as PointD;
            snakeLineCmdLine.point3 = point3.Clone() as PointD;
            snakeLineCmdLine.lineCoordinateList = new List<LineCoordinate>();
            foreach (LineCoordinate c in lineCoordinateList)
            {
                snakeLineCmdLine.lineCoordinateList.Add(c.Clone() as LineCoordinate);
            }
            snakeLineCmdLine.IdCode = snakeLineCmdLine.GetHashCode();
            return snakeLineCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("SNAKE LINE : ");
            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber + ",");
            }
            sb.Append((int)LineStyle + 1);
            if (IsWeightControl)
            {
                sb.Append(", ").Append(WholeWeight.ToString("0.000"));
            }
            if (LineCoordinateList.Count > 0)
            {
                sb.Append(", Start:")
                    .Append(MachineRel(LineCoordinateList[0].Start))
                    .Append(", End:")
                    .Append(MachineRel(LineCoordinateList[0].End));
            }
            if (LineCoordinateList.Count > 1)
            {
                sb.Append("...");
            }
            return sb.ToString();
        }

     
    }
}
