using System;

namespace Anda.Fluid.Domain.FluProgram.Common
{
    /// <summary>
    /// 轨迹参数--点参数
    /// </summary>
    [Serializable]
    public class DotParam : ICloneable
    {
        #region Pre Dispense
        /// <summary>
        /// 当喷射阀运动到点命令指定的坐标位置后，需要等待一定时间，此参数指定等待时间，单位：秒
        /// </summary>
        public double SettlingTime = 0;

        /// <summary>
        /// 执行点命令过程中 Z轴运动时的下降速度
        /// </summary>
        public double DownSpeed = 25;

        /// <summary>
        /// 执行点命令过程中 Z轴运动时的下降加速度
        /// </summary>
        public double DownAccel = 5;

        /// <summary>
        /// 备注说明，无其他用途
        /// </summary>
        public string Notes;
        #endregion

        #region During Dispense
        /// <summary>
        /// 距板高度，点胶时点胶阀距离基板的高度值
        /// </summary>
        public double DispenseGap = 5;

        /// <summary>
        /// 在该点位置点胶时喷胶次数
        /// </summary>
        public int NumShots = 1;

        /// <summary>
        /// 喷一滴胶水后，点胶阀需要抬高的距离值
        /// （注：最后一次喷胶水后不抬高，即如果只喷一次胶水Num. Shots=1，则喷完胶水不执行抬高动作）
        /// </summary>
        public double MultiShotDelta = 0;

        /// <summary>
        /// 螺杆阀点胶时的持续时间
        /// </summary>
        public double ValveOnTime = 1;
        #endregion

        #region Post Dispense
        /// <summary>
        /// 每喷完一滴胶水，需要等待一定时间，该参数指定等待时间长度，单位：秒
        /// （注：每喷完一滴胶水都需要等待，即喷完最后一滴胶水也需要等待）
        /// </summary>
        public double DwellTime = 0;

        /// <summary>
        /// 当喷完所有次数的胶水，且Dwell Time指定的等待时间过完之后，需要将点胶阀抬高，此参数指定点胶阀抬高高度
        /// </summary>
        public double RetractDistance = 0;

        /// <summary>
        /// 执行点命令过程中点胶阀抬升速度
        /// </summary>
        public double RetractSpeed = 25;

        /// <summary>
        /// 执行点命令过程中点胶阀抬升加速度
        /// </summary>
        public double RetractAccel = 5;

        /// <summary>
        /// 螺杆阀的回吸时间,设为0时不进行回吸
        /// </summary>
        public double SuckBackTime = 0.01;
        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
