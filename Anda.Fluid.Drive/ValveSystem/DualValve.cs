using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem
{
    public enum DualValveMode
    {
        /// <summary>
        /// 跟随模式：双阀固定，没有副阀AB
        /// </summary>
        跟随,
        /// <summary>
        /// 同步模式：只在拍完Mark校正
        /// </summary>
        同步,
        /// <summary>
        /// 异步模式：小八字
        /// </summary>
        异步
    }

    public abstract class DualValve
    {

        public DualValve(Card card, Valve valve1, Valve valve2)
        {
            this.Card = card;
            if (valve1.ValveSeries == ValveSeries.喷射阀)
            {
                this.valve1 = (JtValve)valve1;
            }
            else if (valve1.ValveSeries == ValveSeries.螺杆阀)
            {
                this.valve1 = (SvValve)valve1;
            }
            else if (valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.valve1 = (GearValve)valve1;
            }

            if (valve2.ValveSeries == ValveSeries.喷射阀)
            {
                this.valve2 = (JtValve)valve2;
            }
            else if (valve2.ValveSeries == ValveSeries.螺杆阀)
            {
                this.valve2 = (SvValve)valve2;
            }
            else if (valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.valve2 = (GearValve)valve2;
            }
        }

        public Card Card { get; set; }

        public Valve valve1 { get; set; }

        public Valve valve2 { get; set; }

        public int InspectionKey { get; set; }

        public bool isSameValve
        {
            get
            {
                if (this.valve1.ValveSeries == this.valve2.ValveSeries)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        #region SimulSpray //双阀同步开胶

        public abstract short SprayOne();

        public abstract short Spraying();

        public abstract short SprayCycle(short cycle);

        public abstract short SprayCycle(short cycle, short offTime);

        public abstract short SprayOneAndWait();

        public abstract short SprayCycleAndWait(short cycle);

        public abstract int GetSprayMills(short cycle);

        public abstract short SprayOff();

        public abstract Result SuckBack(double suckBackTime);

        #endregion

        #region Fluid (Dual valve)

        /// <summary>
        /// 计算加速时间
        /// </summary>
        /// <param name="accStartPoint">加速起点</param>
        /// <param name="lineStartPoint">直线起点</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns></returns>
        protected double CalcAccTime(PointD accStartPoint, PointD lineStartPoint, double vel, double acc)
        {
            double distance = accStartPoint.DistanceTo(lineStartPoint);
            double t1 = vel / acc;
            double d = vel * t1 * 0.5;
            double t2 = 0;
            if (distance > d)
            {
                t2 = (distance - d) / vel;
            }
            return t1 + t2;
        }
        protected short CmpStop()
        {
            return this.Card.Executor.CmpStop(this.Card.CardId);
        }
        /// <summary>
        /// 启动XYAB4轴2d比较
        /// </summary>
        /// <param name="cmp2dSrc">比较源，0：规划器，1：编码器</param>
        /// <param name="cmp2dMaxErr">最大误差范围</param>
        /// <param name="points">2d比较点，绝对坐标</param>
        protected abstract void SimulCmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points);

        protected void SimulCmp2dStop()
        {
            //stop cmp2d
            short res = Machine.Instance.Robot.Cmp2dStop(0);
            if (Machine.Instance.Robot.Cmp2dStop(1) != 0 && res != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStop);
            }
        }
        /// <summary>
        /// 双阀一维比较
        /// valve1 and valve2 both use XY position data trigger Spraying
        /// </summary>
        /// <param name="source">比较源</param>
        /// <param name="points">触发点数据</param>
        /// <returns></returns>
        protected abstract short CmpStart(short source, PointD[] points);

        /// <summary>
        /// 双阀异步直线打胶
        /// </summary>
        /// <returns></returns>
        public abstract Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc);

        /// <summary>
        /// 双阀跟随直线打胶
        /// </summary>
        /// <returns></returns>
        public abstract Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc);

        /// <summary>
        /// 螺杆阀异步执行打胶
        /// </summary>
        /// <param name="primaryLineParam"></param>
        /// <param name="simulLineParam"></param>
        /// <returns></returns>
        public abstract Result FluidLine(SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam, double acc);

        /// <summary>
        /// 双阀异步圆弧打胶
        /// </summary>
        /// <param name="accStartPos">加速起点</param>
        /// <param name="arcStartPos">圆弧起点</param>
        /// <param name="arcEndPos">圆弧终点</param>
        /// <param name="decEndPos">减速终点</param>
        /// <param name="center">圆弧中心</param>
        /// <param name="clockwize">0：顺时针，1：逆时针</param>
        /// <param name="vel">速度</param>
        /// <param name="points">点胶位置</param>
        /// <param name="intervalSec">两点间隔时间</param>
        /// <returns></returns>
        public abstract Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc);

        /// <summary>
        /// 双阀同步圆弧打胶
        /// </summary>
        /// <returns></returns>
        public abstract Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc);

        /// <summary>
        /// 螺杆阀的异步圆弧打胶
        /// </summary>
        /// <param name="svValveArcParam"></param>
        /// <returns></returns>
        public abstract Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc);
        #endregion
    }
}
