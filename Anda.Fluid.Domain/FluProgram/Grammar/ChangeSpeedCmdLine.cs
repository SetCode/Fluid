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
    public class ChangeSpeedCmdLine : CmdLine
    {
        public ChangeSpeedCmdLine():this(0,0)
        {

        }
        public ChangeSpeedCmdLine(int speed,int waitInMills) : base(true)
        {
            this.Speed = speed;
            this.WaitInMills = waitInMills;
        }
        [CompareAtt("CMP")]
        public int WaitInMills { get; set; }
        [CompareAtt("CMP")]
        public int Speed { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("Change Speed To : ").Append(this.Speed).Append("r/s. Wait : ").Append(this.WaitInMills).Append("ms.").ToString();
        }

        
    }
}
