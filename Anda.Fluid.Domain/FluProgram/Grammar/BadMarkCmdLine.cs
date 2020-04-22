using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.GrayFind;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class BadMarkCmdLine : CmdLine
    {
        public enum BadMarkType
        {
            ModelFind,
            GrayScale
        }

        public ModelFindPrm ModelFindPrm = new ModelFindPrm();

        private GrayCheckPrm grayCheckPrm;
        public GrayCheckPrm GrayCheckPrm
        {
            get
            {
                if(grayCheckPrm == null)
                {
                    grayCheckPrm = new GrayCheckPrm();
                }
                return grayCheckPrm;
            }
        }

        [CompareAtt("CMP")]
        public PointD Position { get; private set; } = new PointD();
        [CompareAtt("CMP")]
        public bool IsOkSkip { get; set; }
        [CompareAtt("CMP")]
        public BadMarkType FindType { get; set; }

        public BadMarkCmdLine(BadMarkType findType = BadMarkType.ModelFind, bool IsOkSkip = false) : base(true)
        {
            this.FindType = findType;
            this.IsOkSkip = IsOkSkip;
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
            PointD p = (patternOldOrigin.ToSystem() + Position).ToMachine();
            // 校正后的机械坐标
            p = coordinateTransformer.Transform(p);
            // 相对系统坐标
            Position.X = (p.ToSystem() - patternNewOrigin.ToSystem()).X;
            Position.Y = (p.ToSystem() - patternNewOrigin.ToSystem()).Y;
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
            sb.Append("BADMARK : ").Append(MachineRel(Position));
            string temp;
            if (IsOkSkip)
            {
                temp = " OK Skip";
            }
            else
            {
                temp = " NG Skip";
            }
            return sb.Append(temp).ToString();
        }

       
    }
}
