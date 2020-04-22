using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram
{
    [Serializable]
    public class UserPosition : ICloneable
    {
        public UserPosition()
        {

        }

        public UserPosition(string name, PointD p)
        {
            this.Name = name;
            this.Position = p;
        }
        [CompareAtt("CMP")]
        public string Name { get; set; } = string.Empty;
        [CompareAtt("CMP")]
        public PointD Position { get; set; } = new PointD();
        [CompareAtt("CMP")]
        public double PosZ { get; set; }
        [CompareAtt("CMP")]
        public MoveType MoveType { get; set; } = MoveType.CAMERA;
        public object Clone()
        {
            UserPosition up = (UserPosition)this.MemberwiseClone();
            up.Position = (PointD)this.Position.Clone();
            return up;
        }
        public override string ToString()
        {
            return string.Format("{0} [{1}, {2}, {3}]", this.Name, this.Position?.X, this.Position?.Y,this.PosZ);
        }
    }
}
