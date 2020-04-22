using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class LoopPassCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        private int start;
        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        [CompareAtt("CMP")]
        private int end;
        public int End
        {
            get { return end; }
            set { end = value; }
        }

        public LoopPassCmdLine(int start, int end) : base(true)
        {
            this.start = start;
            this.end = end;
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
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("LOOP PASS : FROM ").Append(start).Append(" TO ").Append(end).ToString();
        }

        
    }
}