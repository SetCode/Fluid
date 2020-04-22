using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Linq;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MoveToLocationCmd : SupportDirectiveCmd
    {
        public MoveType MoveType = MoveType.CAMERA;

        private PointD position;
        /// <summary>
        /// 移动目标位置坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        public double PosZ;
        public MoveToLocationCmd(RunnableModule runnableModule, MoveToLocationCmdLine moveToLocationCmdLine) : base(runnableModule, moveToLocationCmdLine)
        {
            FluidProgram program = runnableModule.CommandsModule.program;
            UserPosition up = program.UserPositions.Find(x => x.Name == moveToLocationCmdLine.PositionName);
            if (up == null)
            { 
                throw new Exception(moveToLocationCmdLine.PositionName + " is not defined in system.");
            }
            position = new PointD(up.Position);
            this.PosZ = up.PosZ;
            MoveType = moveToLocationCmdLine.MoveType;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            //return new MoveAbsXy(this);
            return new MoveToLocation(this);
        }

    }
}