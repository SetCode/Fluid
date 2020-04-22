using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MoveAbsZCmd : SupportDirectiveCmd
    {
        /// <summary>
        /// 移动的目标位置
        /// </summary>
        public double Z = 0;

        public MoveAbsZCmd(RunnableModule runnableModule, MoveAbsZCmdLine moveAbsZCmdLine) : base(runnableModule, moveAbsZCmdLine)
        {
            Z = moveAbsZCmdLine.Z;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new MoveAbsZ(this);
        }
    }
}