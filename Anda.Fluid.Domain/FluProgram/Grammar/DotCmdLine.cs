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
    public class DotCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        protected PointD position;
        /// <summary>
        /// 点坐标，相对于当前所在Pattern的原点
        /// </summary>
        public PointD Position
        {
            get { return position; }
            set { this.position = value; }
        }

        public override CmdLineType CmdLineName => CmdLineType.点;

        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                list.Add(new Tuple<PointD, string>(this.position, "点胶点"));
                return list;
            }
        }

        /// <summary>
        /// 所使用的点参数类型
        /// </summary>
        [CompareAtt("CMP")]
        public DotStyle DotStyle = DotStyle.TYPE_1;
        public  int NumShots;
        public  bool IsAssign = false;
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

        /// <summary>
        /// 离线编程生成的点对应的元器件名称
        /// Author : 肖旭
        /// Date: 2019/12/26
        /// </summary>
        public string Comp = null;

        /// <summary>
        /// 离线编程生成的点对应的旋转角度
        /// Author : 肖旭
        /// Date: 2019/12/26
        /// </summary>
        public string Rotation = null;
       
        public DotCmdLine() : base(true)
        {
            position = new PointD();
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
            PointD p = (patternOldOrigin.ToSystem() + position).ToMachine();
            // 校正后的机械坐标
            p = coordinateTransformer.Transform(p);
            // 相对系统坐标
            position.X = (p.ToSystem() - patternNewOrigin.ToSystem()).X;
            position.Y = (p.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override object Clone()
        {
            DotCmdLine dotCmdLine = MemberwiseClone() as DotCmdLine;
            dotCmdLine.position = position.Clone() as PointD;
            dotCmdLine.IdCode = dotCmdLine.GetHashCode();
            return dotCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("DOT : ");
            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber + ",");
            }
            sb.Append((int)DotStyle + 1);
            if (IsWeightControl)
            {
                sb.Append(", ").Append(weight.ToString("0.000"));
            }
            sb.Append(", ").Append(MachineRel(position));
            if (this.Rotation != null)
            {
                sb.Append(", ").Append(this.Rotation);
            }
            if (this.Comp != null)
            {
                sb.Append(", ").Append(this.Comp);
            }
            return sb.ToString();
        }

        
    }
}