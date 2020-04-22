using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using System.Drawing;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 移动到指定坐标位置
    /// </summary>
    [Serializable]
    public class MoveAbsXy : Directive
    {
        private MoveType moveType;
        /// <summary>
        /// 目标位置是以相机还是喷嘴的中心点为准
        /// </summary>
        public MoveType MoveType
        {
            get { return moveType; }
        }

        private PointD position;
        /// <summary>
        /// 移动的目标位置（机器坐标值）
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        public MoveAbsXy(MoveAbsXyCmd moveAbsXyCmd)
        {
            moveType = moveAbsXyCmd.MoveType;
            position = moveAbsXyCmd.Position;
            Program = moveAbsXyCmd.RunnableModule.CommandsModule.Program;
        }

        public MoveAbsXy(MoveToLocationCmd moveToLocationCmd)
        {
            moveType = moveToLocationCmd.MoveType;
            position = moveToLocationCmd.Position; 
            this.Program = moveToLocationCmd.RunnableModule.CommandsModule.Program;
        }

        public override Result Execute()
        {
            Log.Dprint("begin to execute MoveAbsXy");
            PointD pos = new PointD(position);
            switch (moveType)
            {
                case MoveType.NEEDLE1:
                    pos = pos.ToNeedle(Drive.ValveSystem.ValveType.Valve1);
                    Log.Dprint("move to position by needle1 : " + pos + ", position by camera:" + position);
                    break;
                case MoveType.NEEDLE2:
                    pos = pos.ToNeedle(Drive.ValveSystem.ValveType.Valve2);
                    Log.Dprint("move to position by needle2 : " + pos + ", position by camera:" + position);
                    break;
                case MoveType.LASER:
                    pos = pos.ToLaser();
                    Log.Dprint("move to position by laser : " + pos + ", position by camera:" + position);
                    break;
                default:
                    Log.Dprint("move to position : " + pos);
                    break;
            }
            Result ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            return Machine.Instance.Robot.MovePosXYAndReply(Position,
                this.Program.MotionSettings.VelXY,
                this.Program.MotionSettings.AccXY);
        }
    }
}