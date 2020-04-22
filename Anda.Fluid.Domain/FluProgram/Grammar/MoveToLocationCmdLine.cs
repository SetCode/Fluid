using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MoveToLocationCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        private string positionName = "";
        /// <summary>
        /// 系统预定义的位置信息名字
        /// </summary>
        public string PositionName
        {
            get
            {
                return positionName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("PositionName can not be null or empty.");
                }
                positionName = value;
            }
        }
        [CompareAtt("CMP")]
        public MoveType MoveType = MoveType.CAMERA;

        public MoveToLocationCmdLine() : base(true)
        {

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
            UserPosition up = this.CommandsModule.program.UserPositions.Find(x => x.Name == this.positionName);
            return sb.Append("MOVE TO LOCATION : ").Append(up).ToString();
        }

     
    }
}