using System;

namespace Anda.Fluid.Domain.FluProgram.Common
{
    /// <summary>
    /// 轨迹参数--线参数
    /// </summary>
    [Serializable]
    public class LineParam : ICloneable
    {
        #region Pre Dispense
        /// <summary>
        /// 执行线命令过程中Z轴下降速度
        /// </summary>
        public double DownSpeed = 25;

        /// <summary>
        /// 执行线命令过程中Z轴下降加速度
        /// </summary>
        public double DownAccel = 5;

        /// <summary>
        /// 点胶起点偏移
        /// </summary>
        public double Offset = 0;

        /// <summary>
        /// 螺杆阀在起始位置提前开胶多久后移动
        /// </summary>
        public double PreMoveDelay = 0.5;

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
        /// 执行普通线命令时沿着线轨迹的运动速度。该参数只针对普通线。
        /// </summary>
        public double Speed = 30;

        /// <summary>
        /// WtCtrlSpeed 参数生成方式：由内置公式自动计算 或 用户输入指定
        /// </summary>
        public ValueType WtCtrlSpeedValueType = ValueType.COMPUTE;
        private double wtCtrlSpeed = 30;
        /// <summary>
        /// 执行重量线命令时沿着线轨迹的运动速度。用户可以下拉选择"Compute",由内置公式自动计算运动速度；
        /// 也可以由用户手动指定，但是不能大于内置公式计算出来的值。该参数只针对重量线。
        /// </summary>
        public double WtCtrlSpeed
        {
            get { return wtCtrlSpeed; }
            set { wtCtrlSpeed = value; }
        }

        /// <summary>
        /// AccelDistance 参数生成方式：由内置公式自动计算 或 用户输入指定
        /// </summary>
        public ValueType AccelDistanceValueType = ValueType.COMPUTE;
        private double accelDistance = 0;
        /// <summary>
        /// 线的起始点之前需要增加一段距离作为点胶阀运动的加速区间，以保证线段上运动是匀速的。
        /// 此参数指定了加速区间的长度。
        /// 用户可以选择"Compute"表示由内置公式自动计算加速区间长度，也可以由用户手动编辑指定加速区间长度。
        /// </summary>
        public double AccelDistance
        {
            get { return accelDistance; }
            set { accelDistance = value; }
        }

        /// <summary>
        /// DecelDistance 参数生成方式：由内置公式自动计算 或 用户输入指定
        /// </summary>
        public ValueType DecelDistanceValueType = ValueType.COMPUTE;
        private double decelDistance = 0;
        /// <summary>
        /// 线的末端跟随一段距离作为点胶阀运动的减速区间，以保证线段上运动是匀速的。
        /// 此参数指定了减速区间的长度。
        /// 用户可以选择"Compute"表示由内置公式自动计算减速区间长度，也可以由用户手动编辑指定减速区间长度。
        /// </summary>
        public double DecelDistance
        {
            get { return decelDistance; }
            set { decelDistance = value; }
        }

        /// <summary>
        /// 螺杆阀的提前关胶距离量,单位毫米.
        /// </summary>
        public double ShutOffDistance = 30;

        /// <summary>
        /// 螺杆阀的回吸时间,单位秒.
        /// </summary>
        public double SuckBackTime = 0.01;

        /// <summary>
        /// 在终点延时多久回走,单位秒.
        /// </summary>
        public double Dwell = 0.02;
        #endregion

        #region Post Dispense
        /// <summary>
        /// 在减速区间运动完成后，XY方向上速度为0后，点胶阀需要抬升一段距离，此参数指定点胶阀抬升的相对距离值。
        /// </summary>
        public double RetractDistance = 0;

        /// <summary>
        /// 指定点胶阀抬升速度
        /// </summary>
        public double RetractSpeed = 25;

        /// <summary>
        /// 指定点胶阀抬升加速度
        /// </summary>
        public double RetractAccel = 5;

        /// <summary>
        /// 回走高度,单位毫米.
        /// </summary>
        public double BacktrackGap = 5;

        /// <summary>
        /// 回走距离,单位为百分比(相对于整条线的距离).
        /// </summary>
        public double BacktrackDistance = 0;

        /// <summary>
        /// 回走速度,单位为毫米/秒.
        /// </summary>
        public double BacktrackSpeed = 20;

        /// <summary>
        /// 回走终点高度,单位为毫米.
        /// </summary>
        public double BacktrackEndGap = 5;


        /// <summary>
        /// 回位高度,单位为毫米.
        /// </summary>
        public double BackGap = 5 ;

        /// <summary>
        /// 齿轮泵阀的下压距离，单位毫米
        /// </summary>
        public double PressDistance = 0;

        /// <summary>
        /// 齿轮泵阀的下压速度，单位mm/s
        /// </summary>
        public double PressSpeed = 25;

        /// <summary>
        /// 齿轮泵阀的下压加速度
        /// </summary>
        public double PressAccel = 5;

        /// <summary>
        /// 齿轮泵阀的下压保持时间，单位sec
        /// </summary>
        public double PressTime = 1;

        /// <summary>
        /// 齿轮泵阀的下压后的抬起距离
        /// </summary>
        public double RaiseDistance = 0;

        /// <summary>
        /// 齿轮泵阀的下压后的抬起速度
        /// </summary>
        public double RaiseSpeed = 25;

        /// <summary>
        /// 齿轮泵阀的下压后的抬起加速度
        /// </summary>
        public double RaiseAccel = 5;

        #endregion

        #region Control 该部分参数只针对普通线
        /// <summary>
        /// 控制模式
        /// </summary>
        public CtrlMode ControlMode = CtrlMode.TIME_BASED_SPACING;

        /// <summary>
        /// 该参数只针对Pos-based spacing模式，指定了两滴胶水之间的间距值
        /// </summary>
        public double Spacing = 1;

        /// <summary>
        /// 该参数只针对Time-based spacing模式，指定了相邻两滴胶水喷胶开始时间的最小时间间隔值
        /// </summary>
        public double ShotTimeInterval = 100;

        /// <summary>
        /// 该参数只针对Total # of dots模式，指定了点的数量
        /// </summary>
        public int TotalOfDots = 0;
        #endregion

        public enum ValueType
        {
            /// <summary>
            /// 由内置公式自动计算
            /// </summary>
            COMPUTE,
            /// <summary>
            /// 由用户输入指定
            /// </summary>
            USER_EDIT
        }

        public enum CtrlMode
        {
            /// <summary>
            /// 指定两滴胶水之间的间距长度值
            /// </summary>
            POS_BASED_SPACING,
            /// <summary>
            /// 相邻两滴胶水喷胶开始时间的最小时间间隔，单位：秒
            /// 如果该参数值为s，本次喷胶开始时间距离上次喷胶开始时间为d, 如果d < s，则还需要等待（s-d）秒，否则无需等待。
            /// </summary>
            TIME_BASED_SPACING,
            /// <summary>
            /// 指定一条线上一共有几个点
            /// </summary>
            TOTAL_OF_DOTS,
            /// <summary>
            /// 只运动不喷胶
            /// </summary>
            NO_DISPENSE
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
