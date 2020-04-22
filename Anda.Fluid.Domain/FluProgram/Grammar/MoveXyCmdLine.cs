using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MoveXyCmdLine : CmdLine
    {
        //private PointD offset;
        [CompareAtt("CMP")]       
        protected PointD position;
        /// <summary>
        /// 移动的目标相对位置
        /// </summary>
        //public PointD Offset
        //{
        //    get { return offset; }
        //}
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 是否返回安全高度
        /// </summary>
        public bool ToSafeZ { get; set; }

        public MoveXyCmdLine() : this(0, 0)
        {
        }

        public MoveXyCmdLine(double dx, double dy, bool toSafeZ = true) : base(true)
        {
            //offset = new PointD(dx, dy);
            this.position = new PointD(dx,dy);
            this.ToSafeZ = toSafeZ;
        }

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // do nothing.
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
            MoveXyCmdLine moveXyCmdLine = MemberwiseClone() as MoveXyCmdLine;
            //moveXyCmdLine.offset = offset.Clone() as PointD;
            moveXyCmdLine.position = position.Clone() as PointD;
            return moveXyCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("MOVE XY : ");
            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber + ",");
            }
            return sb.Append(position).ToString();
        }

    }
}