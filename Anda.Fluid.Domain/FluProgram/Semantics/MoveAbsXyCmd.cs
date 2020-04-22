using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MoveAbsXyCmd : SupportDirectiveCmd
    {
        /// <summary>
        /// 目标位置是以相机还是喷嘴的中心点为准
        /// </summary>
        public MoveType MoveType = MoveType.NEEDLE1;

        private PointD position;
        /// <summary>
        /// 移动的目标位置
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        public MoveAbsXyCmd(RunnableModule runnableModule, MoveAbsXyCmdLine moveAbsXyCmdLine) : base(runnableModule, moveAbsXyCmdLine)
        {
            MoveType = moveAbsXyCmdLine.MoveType;
            position = new PointD(moveAbsXyCmdLine.Position.X, moveAbsXyCmdLine.Position.Y);
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new MoveAbsXy(this);
        }
    }
}