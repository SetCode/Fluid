using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// Z轴上移动到指定位置
    /// </summary>
    [Serializable]
    public class MoveAbsZ : Directive
    {
        /// <summary>
        /// 移动的目标位置（机器坐标值）
        /// </summary>
        public double Z = 0;

        public MoveAbsZ(MoveAbsZCmd moveAbsZCmd)
        {
            Z = moveAbsZCmd.Z;
            Program = moveAbsZCmd.RunnableModule.CommandsModule.Program;
        }

        public override Result Execute()
        {
            Log.Dprint("begin to execute MoveAbsZ, Z = " + Z);
            return Machine.Instance.Robot.MovePosZAndReply(Z,
                this.Program.MotionSettings.VelZ,
                this.Program.MotionSettings.AccZ);
        }
    }
}