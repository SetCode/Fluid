using Anda.Fluid.Domain.FluProgram.Structure;
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
    public class NormalTimerCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        private int waitInMills;
        public int WaitInMills
        {
            get { return waitInMills; }
            set { waitInMills = value; }
        }

        public NormalTimerCmdLine() : this(0)
        {
        }

        public NormalTimerCmdLine(int waitInMills) : base(true)
        {
            this.waitInMills = waitInMills;
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
            return sb.Append("TIMER : Wait ").Append(WaitInMills).ToString();
        }

      
    }
}
