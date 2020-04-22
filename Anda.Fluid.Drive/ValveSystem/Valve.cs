using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;
using System.Threading;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Utils;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.ValveSystem.Series;
using System.IO;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;

namespace Anda.Fluid.Drive.ValveSystem
{
    public enum ValveType
    {
        Valve1,
        Valve2,
        Both
    }

    public enum ValveRunMode
    {
        Wet,
        Dry,
        Look,
        AdjustLine,
        InspectDot,
        InspectRect,
    }

    public enum InspectionKey
    {
        Dot1,
        Dot2,
        Dot3,
        Dot4,
        Dot5,        

        Line1,
        Line2,
        Line3,
        Line4,
        Line5,

        Mark1,
        Mark2,
        Mark3,
        Mark4,
        Mark5,
        Mark6,
        Mark7,
        Mark8,
        Mark9,
        Mark10,

        Measure1,
        Measure2,
        Measure3,
        Measure4,
        Measure5,
        Measure6,
        Measure7,
        Measure8,
        Measure9,
        Measure10,

        Barcode1,
        Barcode2,
        Barcode3,
        Barcode4,
        Barcode5,
        Barcode6,
        Barcode7,
        Barcode8,
        Barcode9,
        Barcode10
    }

    public enum ValveSeries
    {
        喷射阀,
        螺杆阀,
        齿轮泵阀
    }

    public abstract class Valve : EntityBase<ValveType>, IAlarmSenderable
    {
        /// <summary>
        /// 阀组构造函数
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="proportioner">比例阀</param>
        /// <param name="card">板卡</param>
        /// <param name="chn">通道号:1,2</param>
        /// <param name="prm">阀组参数</param>
        public Valve(ValveType valveType, ValveSeries valveSeries, Proportioner proportioner, Card card, short chn, ValvePrm prm)
            : base(valveType)
        {
            this.ValveType = valveType;
            this.ValveSeries = valveSeries;
            this.Proportioner = proportioner;
            this.Card = card;
            this.Chn = chn;
            this.Prm = prm;
            this.setupManualTimer();
        }

        public ValveType ValveType { get; set; }

        public ValveSeries ValveSeries { get; set; }

        public ValvePrm Prm { get; set; }

        public Proportioner Proportioner { get; set; }

        public Card Card { get; set; }

        public short Chn { get; set; }

        /// <summary>
        /// 胶阀当前倾斜类型
        /// </summary>
        public TiltType CurTilt { get; set; } = TiltType.NoTilt;

        /// <summary>
        /// 称重参数
        /// </summary>
        public ValveWeightPrm weightPrm;

        protected bool canCompare = false;

        public ValveRunMode RunMode { get; set; } = ValveRunMode.Wet;

        public int InspectionKey { get; set; }

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.EntityName;

        /// <summary>
        /// 根据轨迹集合生成插补列表
        /// </summary>
        /// <param name="traces"></param>
        /// <param name="vels"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public static List<ICrdable> GetCrdsBy(List<TraceBase> traces, double[] vels, double acc)
        {
            List<ICrdable> crdList = new List<ICrdable>();
            for (int i = 0; i < traces.Count; i++)
            {
                TraceBase trace = traces[i];
                if (trace is TraceLine)
                {
                    CrdLnXY crd = new CrdLnXY()
                    {
                        EndPosX = trace.End.X,
                        EndPosY = trace.End.Y,
                        Vel = vels[i],
                        Acc = acc,
                        VelEnd = i == (traces.Count - 1) ? 0 : vels[i + 1]
                    };
                    crdList.Add(crd);
                }
                else if (trace is TraceArc)
                {
                    CrdArcXYC crd = new CrdArcXYC()
                    {
                        EndPosX = trace.End.X,
                        EndPosY = trace.End.Y,
                        CenterX = (trace as TraceArc).Center.X - trace.Start.X,
                        CenterY = (trace as TraceArc).Center.Y - trace.Start.Y,
                        Clockwise = (short)((trace as TraceArc).Degree > 0 ? 1 : 0),
                        Vel = vels[i],
                        Acc = acc,
                        VelEnd = i == (traces.Count - 1) ? 0 : vels[i + 1]
                    };
                    crdList.Add(crd);
                }
            }
            return crdList;
        }

        /// <summary>
        /// 根据轨迹集合生成插补列表
        /// </summary>
        /// <param name="traces"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public static List<ICrdable> GetCrdsBy(List<TraceBase> traces, double vel, double acc)
        {
            double[] vels = new double[traces.Count];
            for (int i = 0; i < vels.Length; i++)
            {
                vels[i] = vel;
            }
            return GetCrdsBy(traces, vels, acc);
        }

        #region Spray

        /// <summary>
        /// 将开胶和关胶时间（单位微秒）之和转换为秒（点胶时间）
        /// </summary>
        public abstract double SpraySec { get; }

        /// <summary>
        /// 连续打胶
        /// </summary>
        /// <returns></returns>
        public abstract short Spraying();

        /// <summary>
        /// 打指定时间的胶 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        //public abstract short SprayOnTime(long time); 该方法并没有用上，暂时先注释处理

        /// <summary>
        /// 循环打胶，仅支持喷射阀
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="offTime"></param>
        /// <returns></returns>
        public abstract short SprayCycle(short cycle);

        /// <summary>
        /// 循环打胶，仅支持喷射阀
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="offTime"></param>
        /// <returns></returns>
        public abstract short SprayCycle(short cycle, short offTime);

        /// <summary>
        /// 打一次胶后等待
        /// </summary>
        /// <returns></returns>
        public abstract short SprayOneAndWait();

        public abstract short SprayOneAndWait(int sprayingTime);

        /// <summary>
        /// 打cycle-1次胶后等待
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        public abstract short SprayCycleAndWait(short cycle);

        /// <summary>
        /// 将打胶时间转换为秒
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        //public abstract int GetSprayMills(short cycle); 该方法并没有用上，暂时先注释处理;

        /// <summary>
        /// 停止打胶
        /// </summary>
        /// <returns></returns>
        public abstract short SprayOff();

        /// <summary>
        /// 执行回吸动作，仅支持螺杆阀
        /// </summary>
        /// <param name="suckBackTime"></param>
        /// <returns></returns>
        public abstract Result SuckBack(double suckBackTime);
        #endregion


        #region Fluid

        /// <summary>
        /// 喷射阀直线运动，含加速段、减速段，堵塞等待结果
        /// </summary>
        /// <param name="accStartPos">加速起点</param>
        /// <param name="lineStartPos">直线起点</param>
        /// <param name="lineEndPos">直线终点</param>
        /// <param name="decEndPos">加速终点</param>
        /// <param name="vel">速度</param>
        /// <param name="points">点胶位置</param>
        /// <param name="intervalSec">两点间隔时间</param>
        /// <returns></returns>
        public abstract Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc);

        /// <summary>
        /// 螺杆阀的直线运动
        /// </summary>
        /// <returns></returns>
        public abstract Result FluidLine(SvValveFludLineParam svValveLineParam, double acc);

        /// <summary>
        /// 喷射阀圆弧运动，含加速段、减速段，堵塞等待结果
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
        public abstract Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc);

        /// <summary>
        /// 螺杆阀的圆弧运动
        /// </summary>
        /// <param name="svValveArcParam"></param>
        /// <returns></returns>
        public abstract Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc);

        /// <summary>
        /// 执行复合线段运动
        /// </summary>
        /// <param name="SymbolLinesCrdData"></param>
        /// <param name="fluidPrm"></param>
        /// <returns></returns>
        public abstract Result FluidSymbolLines(List<CrdSymbolLine> SymbolLinesCrdData, GearValveFluidSymbolLinesPrm fluidPrm, double acc, double offsetX = 0,double offsetY=0);

        
        /// <summary>
        /// 启动2d比较
        /// </summary>
        /// <param name="cmp2dSrc">比较源，0：规划器，1：编码器</param>
        /// <param name="cmp2dMaxErr">最大误差范围</param>
        /// <param name="points">2d比较点，绝对坐标</param>
        protected abstract void Cmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points);

        /// <summary>
        /// 停止2d比较
        /// </summary>
        protected void Cmp2dStop()
        {
            //stop cmp2d
            if (Machine.Instance.Robot.Cmp2dStop(this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStop);
            }
        }

        /// <summary>
        /// 启动2d比较的一维模式
        /// </summary>
        /// <param name="cmp2dSrc"></param>
        /// <param name="cmp2dMaxErr"></param>
        protected abstract void Cmp2dMode1dStart(short cmp2dSrc, short cmp2dMaxErr);


        /// <summary>
        /// 启动一维比较
        /// </summary>
        /// <param name="source">比较源</param>
        /// <param name="points">触发点数据</param>
        /// <returns></returns>
        protected abstract short CmpStart(short source, PointD[] points);

        /// <summary>
        /// 停止一维比较
        /// </summary>
        /// <returns></returns>
        protected short CmpStop()
        {
            return this.Card.Executor.CmpStop(this.Card.CardId);
        }

        #endregion


        #region BufFulid

        public abstract Result StartBufFluid(InitLook LookAheadPrm);

        public abstract Result BufFluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc);

        public abstract Result BufFluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc);

        public Result BufChangeValveTiltStatus(TiltType tiltType, double velU, double accU)
        {
            Result ret = Result.OK;
            // 倾斜到位(胶阀当前倾斜状态不等于轨迹倾斜状态时执行)
            if (tiltType != Machine.Instance.Valve1.CurTilt && (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV))
            {
                // 回到安全高度再变方向
                if (Machine.Instance.Robot.PosZ < Machine.Instance.Robot.CalibPrm.SafeZ)
                {
                    ret = Machine.Instance.Robot.BufMoveSafeZ();
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                if (Machine.Instance.Robot.RobotIsXYZUV)
                {
                    // todo 四方位旋转气缸动作
                }
                //U轴倾斜
                AngleHeightPosOffset tiltValvePrm = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Find(t => t.TiltType.Equals(tiltType));
                if (tiltValvePrm == null)
                {
                    return Result.FAILED;
                }
                if (velU == 0 || accU == 0)
                {
                    return Result.FAILED;
                }
                ret = Machine.Instance.Robot.BufMoveAxis(Machine.Instance.Robot.AxisU, tiltValvePrm.ValveAngle, velU, accU);
                if (!ret.IsOk)
                {
                    return ret;
                }
                else
                {
                    Machine.Instance.Valve1.CurTilt = tiltType;
                }
            }
            return ret;
        }

        #endregion


        #region　Purge & Prime 

        public abstract Result DoPurgeAndPrime();

        public abstract Result DoPrime();


        public abstract Result DoPurge();


        #endregion


        #region Soak
        public abstract Result DoSoak();

        public abstract Result OutSoak();
        #endregion


        #region Weight
        /// <summary>
        /// 去称重天平位置
        /// </summary>
        /// <returns></returns>
        public Result MoveToScaleLoc()
        {
            Machine.Instance.Robot.MoveSafeZAndReply();
            return Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.ScaleLoc.ToNeedle(this.Key));
        }

        /// <summary>
        /// 模拟实际生产
        /// </summary>
        /// <param name="times">打多少次</param>
        /// <param name="dots">每次打多少点</param>
        /// <param name="interval">每次的时间间隔</param>
        /// <returns></returns>
        public Result Shot(int times, short dots, int interval)//阀
        {
            Result ret = Result.OK;
            for (int i = 0; i < times; i++)
            {
                this.SprayCycleAndWait(dots);
                Thread.Sleep(interval);
            }
            return ret;
        }
        public void Shot()
        {
            this.Shot(this.weightPrm.PatternCount, (short)this.weightPrm.ShotDotsEachPattern, this.weightPrm.Interval);
        }

        /// <summary>
        /// 计算单点重量
        /// </summary>
        protected Result CalSingleDotWeight()
        {
            Result ret = Result.OK;
            int dots = GetTotalDots();
            if (dots <= 0)
            {
                ret = Result.FAILED;
                return ret;
            }
            //计算多点的平均值
            double singleDotWeight = Math.Round(this.weightPrm.DifferWeight / dots, 4);
            this.weightPrm.SetSingleDotWeight(singleDotWeight);
            Log.Print("阀" + (this.Key+1) + "本次称重单点重量：" + singleDotWeight);
            if (Key == 0)
            {
                MsgCenter.Broadcast(MachineMsg.SINGLEDROPWEIGHT_UPDATE, null, this.weightPrm.SingleDotWeight);
            }
            return ret;
        }
        /// <summary>
        ///比较两阀之间单点重量
        ////// </summary>
        /// <returns></returns>
        protected Result checkValveDotsWeight()
        {
            Result ret = Result.OK;

            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                Logger.DEFAULT.Info(LogCategory.MANUAL |LogCategory.RUNNING, this.Key.ToString(), "Only one single valve");
                return ret;
            }

            if (this.Key == ValveType.Valve2 && Machine.Instance.Valve1.canCompare)
            {
                Machine.Instance.Valve1.canCompare = false;
                if (Machine.Instance.Valve1.weightPrm.StandardWeight <= 0)
                {
                    ret = Result.FAILED;
                    Logger.DEFAULT.Warn(LogCategory.MANUAL|LogCategory.RUNNING, this.GetType().Name, "the standard weight is 0");
                    return ret;
                }
                double diffSingle = Math.Abs(Machine.Instance.Valve1.weightPrm.SingleDotWeight - Machine.Instance.Valve2.weightPrm.SingleDotWeight);
                double scopeDisgn = Machine.Instance.Valve1.weightPrm.StandardWeight * (Machine.Instance.Valve1.weightPrm.Percentage / 100);
                //单点重量进行比较
                if (diffSingle > scopeDisgn)
                {
                    ret = Result.FAILED;
                }
                string msg = string.Format("the single dot weight diff of valve1 and valve2 is {0},the scope is {0} ", diffSingle, scopeDisgn);
                Logger.DEFAULT.Info(LogCategory.MANUAL|LogCategory.RUNNING, this.GetType().Name, msg);
                return ret;
            }
            else
            {
                ret = Result.OK;
                return ret;
            }


        }
        public abstract Result AutoRunWeighingWithPurge();

        public abstract Result DoWeight();

        public abstract Result DoWeight(Action sprayAction);
        public abstract Result WeightCpk(int times, short cycles,int interval, out double[] outweights);

        public abstract void WeightCpkStop();

        /// <summary>
        /// 校验重量是否超出标准重量范围
        /// </summary>
        /// <returns>false:超出，true:在范围内</returns>
        public abstract bool CalibWeight();


        /// <summary>
        /// 在模拟生产中，计算总共打点次数
        /// </summary>
        /// <returns></returns>
        protected abstract int GetTotalDots();

        /// <summary>
        /// 增加累计重量
        /// </summary>
        /// <param name="weight"></param>
        protected void AddCumulativeWeight(double weight)
        {
            this.weightPrm.CumulativeWeight += Math.Round(weight, 3);
        }
        public abstract void AddCumulativeWeight();

        #endregion


        #region Manual Spray

        protected System.Timers.Timer timer1;
        protected bool isManualSpraying;

        private void setupManualTimer()
        {
            this.timer1 = new System.Timers.Timer();
            this.timer1.AutoReset = false;
            this.timer1.Elapsed += Timer1_Elapsed;
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.isManualSpraying = false;
            this.timer1.Stop();
        }

        /// <summary>
        /// 开启手动打胶
        /// </summary>
        /// <param name="value"></param>
        public abstract void StartManualSpray(int value);

        /// <summary>
        /// 关闭手动打胶
        /// </summary>
        public abstract void StopManualSpray();

        #endregion


        #region Valve Action

        /// <summary>
        /// 重置倾斜胶阀
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public Result ResetValveTilt(double vel,double acc)
        {
            Result ret = Result.OK;
            if (CurTilt != TiltType.NoTilt || Math.Round(Machine.Instance.Robot.PosU,3) != 0)
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }
                
                if (Machine.Instance.Robot.RobotIsXYZUV)
                {
                    // todo 四方位旋转气缸动作
                }
                if (acc == 0 || vel == 0)
                {
                    return Result.FAILED;
                }
                ret = Machine.Instance.Robot.MovePosUAndReply(0,acc,vel);
                if (ret.IsOk)
                {
                    this.CurTilt = TiltType.NoTilt;
                }
            }
            return ret;
        }

        /// <summary>
        /// 胶阀倾斜到位
        /// </summary>
        /// <param name="tiltType"></param>
        /// <param name="velU"></param>
        /// <param name="accU"></param>
        /// <returns></returns>
        public Result ChangeValveTiltStatus(TiltType tiltType, double velU, double accU)
        {
            Result ret = Result.OK;
            // 倾斜到位(胶阀当前倾斜状态不等于轨迹倾斜状态时执行)
            if (tiltType != Machine.Instance.Valve1.CurTilt && (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV))
            {
                // 回到安全高度再变方向
                if (Machine.Instance.Robot.PosZ < Machine.Instance.Robot.CalibPrm.SafeZ)
                {
                    ret = Machine.Instance.Robot.MoveSafeZAndReply();
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                if (Machine.Instance.Robot.RobotIsXYZUV)
                {
                    // todo 四方位旋转气缸动作
                }
                //U轴倾斜
                AngleHeightPosOffset tiltValvePrm = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Find(t => t.TiltType.Equals(tiltType));
                if (tiltValvePrm == null)
                {
                    return Result.FAILED;
                }
                if (velU == 0 || accU == 0)
                {
                    return Result.FAILED;
                }
                ret = Machine.Instance.Robot.MovePosUAndReply(tiltValvePrm.ValveAngle, velU, accU);
                if (!ret.IsOk)
                {
                    return ret;
                }
                else
                {
                    Machine.Instance.Valve1.CurTilt = tiltType;
                }
            }
            return ret;
        }

        #endregion
    }
}
