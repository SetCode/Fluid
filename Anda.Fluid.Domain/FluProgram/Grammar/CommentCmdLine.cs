using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class CommentCmdLine : CmdLine
    {
        // 该指令不允许被禁用
        public override bool Enabled
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// 注释内容
        /// </summary>
        [CompareAtt("CMP")]
        public string Content;

        public CommentCmdLine(string content) : base(true)
        {
            Content = content;
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
            return "COMMENT : " + Content;
        }

       
    }
}