using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    public class MoveToLocation:Directive
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
        public double PosZ { get; }
        public MoveToLocation(MoveToLocationCmd moveToLocationCmd)
        {
            this.moveType = moveToLocationCmd.MoveType;
            this.position = moveToLocationCmd.Position;
            this.PosZ = moveToLocationCmd.PosZ;
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
            ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
            this.Program.MotionSettings.VelXY,
            this.Program.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }
            return Machine.Instance.Robot.MovePosZAndReply(this.PosZ);

        }
    }
}
