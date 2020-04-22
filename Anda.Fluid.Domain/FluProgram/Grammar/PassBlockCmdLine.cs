using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class PassBlockCmdLine : CmdLine
    {
        [CompareAtt("CMP")]
        public int StartIndex { get; set; }
        [CompareAtt("CMP")]
        public int EndIndex { get; set; }

        public PassBlockCmdLine(int startIndex, int endIndex) : base(true)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            //do nothing
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("PASS BLOCK : FROM ").Append(StartIndex).Append(" TO ").Append(EndIndex).ToString();
        }

        
    }
}
