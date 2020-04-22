using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MeasureHeightCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        private PointD position;
        /// <summary>
        /// 测量高度的位置
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 标准高度
        /// </summary>
        [CompareAtt("CMP")]
        public double StandardHt { get; set; } = 0;

        public double ZPos { get; set; } = -2;
        /// <summary>
        /// 上限
        /// </summary>
        [CompareAtt("CMP")]
        public double ToleranceMax { get; set; } = 4;       

        /// <summary>
        /// 下限
        /// </summary>
        [CompareAtt("CMP")]
        public double ToleranceMin { get; set; } = -4;

        public MeasureHeightCmdLine() : base(true)
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
            MeasureHeightCmdLine measureHeightCmdLine = MemberwiseClone() as MeasureHeightCmdLine;
            measureHeightCmdLine.position = position.Clone() as PointD;
            return measureHeightCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("MEASURE HEIGHT : ").Append(MachineRel(Position)).ToString();
        }

     
    }
}
