using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MoveAbsXyCmdLine : CmdLine
    {
        /// <summary>
        /// 目标位置是以相机还是喷嘴的中心点为准
        /// </summary>
        [CompareAtt("CMP")]
        public MoveType MoveType = MoveType.CAMERA;

        [CompareAtt("CMP")]
        private PointD position;
        /// <summary>
        /// 移动的目标位置（机械坐标系）
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        public MoveAbsXyCmdLine() : base(true)
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
            // do nothing
        }

        public override object Clone()
        {
            MoveAbsXyCmdLine moveAbsXyCmdLine = MemberwiseClone() as MoveAbsXyCmdLine;
            moveAbsXyCmdLine.position = position.Clone() as PointD;
            return moveAbsXyCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("MOVE ABS XY : ").Append(MoveType).Append(", ")
                .Append(Position).ToString();
        }

    }
}