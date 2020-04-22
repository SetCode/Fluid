using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class ArcCmdLine : CmdLine
    {

        private PointD start;
        /// <summary>
        /// 弧起始点的坐标
        /// </summary>
        [CompareAtt("CMP")]
        public PointD Start
        {
            get { return start; }
        }

        private PointD middle;
        /// <summary>
        /// 弧上位于起始点和末尾点之间的一个坐标
        /// </summary>

        [CompareAtt("CMP")]
        public PointD Middle
        {
            get { return middle; }
        }

        private PointD end;
        /// <summary>
        /// 弧的末尾点的坐标
        /// </summary>
        [CompareAtt("CMP")]
        public PointD End
        {
            get { return end; }
        }

        private PointD center;
        /// <summary>
        /// 弧所在的圆心
        /// </summary>
        [CompareAtt("CMP")]
        public PointD Center
        {
            get { return center; }
            set { this.center = value; }
        }

        public override CmdLineType CmdLineName
        {
            get
            {
                return CmdLineType.圆弧或圆环;
            }
        }
        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                list.Add(new Tuple<PointD, string>(this.start, "起点"));
                list.Add(new Tuple<PointD, string>(this.middle, "中间点"));
                list.Add(new Tuple<PointD, string>(this.end, "结束点"));
                return list;
            }
        }

        /// <summary>
        /// 弧度
        /// </summary>
        [CompareAtt("CMP")]
        public double Degree;

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

        private double weight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        [CompareAtt("CMP")]
        public double Weight
        {
            set
            {
                weight = value < 0 ? 0 : value;
            }
            get
            {
                return weight;
            }
        }

        public ArcCmdLine() : base(true)
        {
            start = new PointD(2, 1);
            middle = new PointD(1, 2);
            end = new PointD(0, 1);
            center = new PointD(1, 1);
            Degree = 180f;
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
            PointD pStart = (patternOldOrigin.ToSystem() + start).ToMachine();
            PointD pMiddle = (patternOldOrigin.ToSystem() + middle).ToMachine();
            PointD pEnd = (patternOldOrigin.ToSystem() + end).ToMachine();
            PointD pCenter = (patternOldOrigin.ToSystem() + center).ToMachine();
            // 校正后的机械坐标
            pStart = coordinateTransformer.Transform(pStart);
            pMiddle = coordinateTransformer.Transform(pMiddle);
            pEnd = coordinateTransformer.Transform(pEnd);
            pCenter = coordinateTransformer.Transform(pCenter);
            // 相对系统坐标
            start.X = (pStart.ToSystem() - patternNewOrigin.ToSystem()).X;
            start.Y = (pStart.ToSystem() - patternNewOrigin.ToSystem()).Y;
            middle.X = (pMiddle.ToSystem() - patternNewOrigin.ToSystem()).X;
            middle.Y = (pMiddle.ToSystem() - patternNewOrigin.ToSystem()).Y;
            end.X = (pEnd.ToSystem() - patternNewOrigin.ToSystem()).X;
            end.Y = (pEnd.ToSystem() - patternNewOrigin.ToSystem()).Y;
            center.X = (pCenter.ToSystem() - patternNewOrigin.ToSystem()).X;
            center.Y = (pCenter.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override object Clone()
        {
            ArcCmdLine arcCmdLine = MemberwiseClone() as ArcCmdLine;
            arcCmdLine.start = start.Clone() as PointD;
            arcCmdLine.middle = middle.Clone() as PointD;
            arcCmdLine.end = end.Clone() as PointD;
            arcCmdLine.center = center.Clone() as PointD;
            arcCmdLine.IdCode = arcCmdLine.GetHashCode();
            return arcCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("ARC : ");

            if (this.TrackNumber != null) 
            {
                sb.Append(this.TrackNumber + ",");
            }
            sb.Append((int)LineStyle + 1);

            if (IsWeightControl)
            {
                sb.Append(", ").Append(Weight.ToString("0.000"));
            }
            sb.Append(", ")
                .Append(MachineRel(Start)).Append(", ")
                .Append(MachineRel(Middle)).Append(", ")
                .Append(MachineRel(End)).Append(", ")
                .Append(MachineRel(Center)).Append(", ")
                .Append(Degree.ToString("0.000"));
            return sb.ToString();
        }

        //public override void ReverseCmd()
        //{
        //    PointD tmp = new PointD();
        //    tmp = this.start;
        //    this.start = this.end;
        //    this.end = tmp;
        //    this.Degree = -this.Degree;
        //}
    }
}