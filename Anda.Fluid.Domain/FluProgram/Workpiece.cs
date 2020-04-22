using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram
{
    [Serializable]
    public class Workpiece : Pattern
    {
        public const string WORKPIECE_NAME = "Workpiece";
        public Workpiece(FluidProgram program, double originX, double originY) 
            : base(program, WORKPIECE_NAME, originX, originY)
        {
        }

        /// <summary>
        /// 原点机械坐标
        /// </summary>
        public PointD OriginPos { get; set; } = new PointD();


        public override PointD GetOriginPos()
        {
            return this.OriginPos;
        }
    }
}