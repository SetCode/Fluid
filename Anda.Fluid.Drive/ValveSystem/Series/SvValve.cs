using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem.Prm;
using System.Threading;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class SvValveFludLineParam
    {
        /// <summary>
        /// 在起始位置开胶后，延时移动的时间
        /// </summary>
        public double startPosDelay;
        /// <summary>
        /// 线终点位置
        /// </summary>
        public PointD endPos;

        /// <summary>
        /// Poly线的各个过渡点位置(包含终点,不包含起点)
        /// </summary>
        public PointD[] transPoints;

        /// <summary>
        /// 提前关胶位置
        /// </summary>
        public PointD stopSprayPos;

        /// <summary>
        /// 回走时的终点位置
        /// </summary>
        public PointD backTrackPos;

        /// <summary>
        /// Poly回走时的过渡点位置(包含终点,不包含起点)
        /// </summary>
        public PointD[] backTransPoints;

        /// <summary>
        /// 轨迹模式则采用backTraces
        /// </summary>
        public bool IsTraceMode;

        /// <summary>
        /// 正向轨迹集合
        /// </summary>
        public List<TraceBase> transTraces;

        /// <summary>
        /// 回走时的轨迹集合
        /// </summary>
        public List<TraceBase> backTransTraces;

        /// <summary>
        /// 线速度,Poly线为若干个,线段为一个.
        /// </summary>
        public double[] vels;

        /// <summary>
        /// 在终点延时多久回走
        /// </summary>
        public double backTrackDelay;

        /// <summary>
        /// 回走时的回抬高度
        /// </summary>
        public double backTrackGap;

        /// <summary>
        /// 回走时的速度
        /// </summary>
        public double backTrackVel;
    }
    public class SvValve : Valve
    {
        new public SvValvePrm Prm { get; set; }
        public SvValve(ValveType valveType, Proportioner proportioner, Card card, short chn, ValvePrm prm) : base(valveType, ValveSeries.螺杆阀, proportioner, card, chn, prm)
        {
            this.Prm = prm.SvValvePrm;
        }

        public SvValve(Valve valve) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, valve.Prm)
        {

        }
        public SvValve(Valve valve, ValvePrm prm) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, prm)
        {

        }
        public override double SpraySec
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void AddCumulativeWeight()
        {
            throw new NotImplementedException();
        }

        public override Result AutoRunWeighingWithPurge()
        {
            Result ret = Result.OK;

            ret = this.DoPurgeAndPrime();
            if (!ret.IsOk)
            {
                return ret;
            }
            ret = this.DoWeight();
            return ret;
        }

        public override bool CalibWeight()
        {
            throw new NotImplementedException();
        }

        public override Result DoPrime()
        {
            return PrimeFactory.GetIPrimable().DoPrime(this);
        }

        public override Result DoPurge()
        {
            return PurgeFactory.GetIPurgable().DoPurge(this);
        }
        #region 
        public override Result DoSoak()
        {
            return Result.OK;
        }
        public override Result OutSoak()
        {
            return Result.OK;
        }
        #endregion 
        public override Result DoPurgeAndPrime()
        {
            return PurgeAndPrimeFactory.GetIPurgePrimable().DoPurgeAndPrime(this);
        }

        public override Result DoWeight()
        {
            Result ret = Result.OK;

            ret = this.MoveToScaleLoc();
            if (!ret.IsOk)
            {
                return ret;
            }

            try
            {
                Dictionary<int, double> dic = new Dictionary<int, double>();
                foreach (var item in SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic)
                {
                    double beforeWeight = 0.0;
                    double weight = 0.0;

                    Machine.Instance.Scale.Scalable.ReadWeight(out beforeWeight);
                    this.weightPrm.SetWeightBeforeSpray(beforeWeight);//称重前读数

                    //打一秒胶
                    this.Spraying();
                    Thread.Sleep(1000);
                    this.SprayOff();

                    //称重后度数
                    Thread.Sleep(1000);
                    Machine.Instance.Scale.Scalable.ReadWeight(out weight);
                    this.weightPrm.SetCurrentWeight(weight);

                    //差值
                    double value = weight - beforeWeight;
                    dic.Add(item.Key, value);
                }

                SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic = dic;
            }
            catch (Exception)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoValve.WeightAutoRunAlarm);
            }

            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            return ret;
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycle(short cycle)
        {
            return this.Spraying();
        }

        public override short SprayCycle(short cycle, short offTime)
        {
            return this.Spraying();
        }

        public override short SprayCycleAndWait(short cycle)
        {
            return this.Spraying();
        }

        public override short Spraying()
        {
            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.Chn);
            if (rtn != 0) return rtn;

            rtn = this.Card.Executor.CmpPulse(this.Card.CardId, (short)(this.Chn + 1), 1, 0);
            if (rtn != 0) return rtn;

            return rtn;
        }

        public override short SprayOff()
        {
            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.Chn);
            if (rtn != 0) return rtn;

            rtn = this.Card.Executor.CmpPulse(this.Card.CardId, 0, 1, 0);
            if (rtn != 0) return rtn;

            return rtn;
        }


        public override short SprayOneAndWait()
        {
            return this.Spraying();
        }

        public override short SprayOneAndWait(int sprayingTime)
        {
            this.Spraying();

            Thread.Sleep(sprayingTime);

            return this.SprayOff();
        }

        public override Result WeightCpk(int times, short cycles, int interval, out double[] outweights)
        {
            throw new NotImplementedException();
        }
        public override void WeightCpkStop()
        {
            throw new NotImplementedException();
        }
        protected override void Cmp2dMode1dStart(short cmp2dSrc, short cmp2dMaxErr)
        {
            throw new NotImplementedException();
        }

        protected override void Cmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {

        }

        protected override short CmpStart(short source, PointD[] points)
        {
            throw new NotImplementedException();
        }

        protected override int GetTotalDots()
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(SvValveFludLineParam svValveLineParam, double acc)
        {
            Result ret = Result.OK;
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (svValveLineParam.endPos == null)
            {
                return Result.FAILED;
            }

            //TODO 前瞻预处理还没加
            //连续插补
            List<ICrdable> crdList = new List<ICrdable>();
            if (!svValveLineParam.IsTraceMode)
            {
                // 多段线模式
                if (svValveLineParam.transPoints.Length > 0)
                {
                    for (int i = 0; i < svValveLineParam.transPoints.Length; i++)
                    {
                        CrdLnXY crd = new CrdLnXY()
                        {
                            EndPosX = svValveLineParam.transPoints[i].X,
                            EndPosY = svValveLineParam.transPoints[i].Y,
                            Vel = svValveLineParam.vels[i],
                            Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                            VelEnd = svValveLineParam.vels[i]
                        };
                        crdList.Add(crd);
                    }
                }
            }
            else
            {
                // 线+圆弧轨迹模式
                crdList = GetCrdsBy(svValveLineParam.transTraces, svValveLineParam.vels, acc);
            }

            if (this.RunMode == ValveRunMode.Wet)
            {
                //如果线的终点和关胶点不一致，则可以启用二维比较
                if (svValveLineParam.endPos != svValveLineParam.stopSprayPos)
                {
                    this.CmpStop();
                    this.Cmp2dStart(svValveLineParam.stopSprayPos);
                }
                else
                {
                    this.Spraying();
                }

            }

            //在起始位置开胶后，延时移动的时间
            Thread.Sleep(TimeSpan.FromSeconds(svValveLineParam.startPosDelay));

            //开始移动
            if (!svValveLineParam.IsTraceMode)
            {
                if (svValveLineParam.transPoints.Length > 0)
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                          Machine.Instance.Robot.AxisX,
                          Machine.Instance.Robot.AxisY,
                          Machine.Instance.Robot.TrcPrmWeight,
                          crdList, this.Chn, svValveLineParam.transPoints.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}", this.Prm.Cmp2dMaxErr));
                    Machine.Instance.Robot.Fire(command);
                    ret = Machine.Instance.Robot.WaitCommandReply(command);
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(svValveLineParam.endPos, svValveLineParam.vels[0], acc);
                }
            }
            else
            {
                ret = Machine.Instance.Robot.MoveTrcXYReply(crdList);
            }

            //如果提前关胶距离是0，就手动关胶
            if (svValveLineParam.endPos == svValveLineParam.stopSprayPos)
            {
                this.SprayOff();
            }

            //在终点延时
            Thread.Sleep(TimeSpan.FromSeconds(svValveLineParam.backTrackDelay));

            //抬高
            ret = Machine.Instance.Robot.MoveIncZAndReply(svValveLineParam.backTrackGap, svValveLineParam.backTrackVel, acc);
            if (!ret.IsOk)
            {
                return ret;
            }

            //开始回走

            //连续插补
            List<ICrdable> backCrdList = new List<ICrdable>();
            if (!svValveLineParam.IsTraceMode)
            {
                if (svValveLineParam.backTransPoints.Length > 0)
                {
                    for (int i = 0; i < svValveLineParam.backTransPoints.Length; i++)
                    {
                        CrdLnXY crd = new CrdLnXY()
                        {
                            EndPosX = svValveLineParam.backTransPoints[i].X,
                            EndPosY = svValveLineParam.backTransPoints[i].Y,
                            Vel = svValveLineParam.backTrackVel,
                            Acc = acc,
                            VelEnd = svValveLineParam.backTrackVel
                        };
                        backCrdList.Add(crd);
                    }
                }
            }
            else
            {
                // 线+圆弧轨迹模式
                backCrdList = GetCrdsBy(svValveLineParam.backTransTraces, svValveLineParam.backTrackVel, acc);
            }

            if (!svValveLineParam.IsTraceMode)
            {
                if (svValveLineParam.backTransPoints.Length > 0)
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                          Machine.Instance.Robot.AxisX,
                          Machine.Instance.Robot.AxisY,
                          Machine.Instance.Robot.TrcPrmWeight,
                          backCrdList, this.Chn, svValveLineParam.backTransPoints.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Machine.Instance.Robot.Fire(command);
                    ret = Machine.Instance.Robot.WaitCommandReply(command);
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(svValveLineParam.backTrackPos, svValveLineParam.vels[0], acc);
                }
            }
            else
            {
                ret = Machine.Instance.Robot.MoveTrcXYReply(backCrdList);
            }
            return ret;

        }

        public override Result SuckBack(double suckBackTime)
        {
            return Result.OK;
        }

        public override Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc)
        {
            Result ret = Result.OK;
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (svValveArcParam.endPos == null)
            {
                return Result.FAILED;
            }

            if (this.RunMode == ValveRunMode.Wet)
            {
                //如果线的终点和关胶点不一致，则可以启用二维比较
                if (svValveArcParam.endPos != svValveArcParam.stopSprayPos)
                {
                    this.CmpStop();
                    this.Cmp2dStart(svValveArcParam.stopSprayPos);
                }
                else
                {
                    this.Spraying();
                }
            }

            //在起始位置开胶后，延时移动的时间
            Thread.Sleep(TimeSpan.FromSeconds(svValveArcParam.startPosDelay));

            //开始移动
            ret = Machine.Instance.Robot.MoveArcAndReply(svValveArcParam.endPos, center, clockwize, svValveArcParam.vels[0], acc);

            //如果提前关胶距离是0，就手动关胶
            if (svValveArcParam.endPos == svValveArcParam.stopSprayPos)
            {
                this.SprayOff();
            }

            //在终点延时
            Thread.Sleep(TimeSpan.FromSeconds(svValveArcParam.backTrackDelay));

            //抬高
            ret = Machine.Instance.Robot.MoveIncZAndReply(svValveArcParam.backTrackGap, svValveArcParam.backTrackVel, acc);
            if (!ret.IsOk)
            {
                return ret;
            }

            //开始回走
            short backClockWise = 0;
            if (clockwize == 0)
            {
                backClockWise = 1;
            }
            else
            {
                backClockWise = 0;
            }
            ret = Machine.Instance.Robot.MoveArcAndReply(svValveArcParam.backTrackPos, center, backClockWise, svValveArcParam.backTrackVel, acc);

            return ret;
        }

        public override Result FluidSymbolLines(List<CrdSymbolLine> SymbolLinesCrdData, GearValveFluidSymbolLinesPrm fluidPrm, double acc, double offsetX = 0, double offsetY = 0)
        {
            throw new NotImplementedException();
        }

        public override Result BufFluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result BufFluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result StartBufFluid(InitLook LookAheadPrm)
        {
            throw new NotImplementedException();
        }

        private void Cmp2dStart(PointD shutOffPoint)
        {
            PointD cmp2dPoints = new PointD(
                Math.Round(shutOffPoint.X - Machine.Instance.Robot.PosX, 2),
                Math.Round(shutOffPoint.Y - Machine.Instance.Robot.PosY, 2));

            short threshold = (short)this.Prm.Cmp2dThreshold;

            short cmp2dMaxErr = (short)this.Prm.Cmp2dMaxErr;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }

            //初始化二维比较
            if (this.Card.Executor.Cmp2dClear(this.Card.CardId, this.Chn) != 0
                || this.Card.Executor.Cmp2dMode(this.Card.CardId, this.Chn) != 0
                || this.Card.Executor.Cmp2dSetPrm(this.Card.CardId, this.Chn, Machine.Instance.Robot.AxisX.AxisId, Machine.Instance.Robot.AxisY.AxisId,
                    (short)Cmp2dSrcType.编码器, 1, 1, 10, cmp2dMaxErr, 0) != 0) 
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }

            //导入位置比较点
            int[] pulsx = new int[1];
            int[] pulsy = new int[1];
            pulsx[0] = Machine.Instance.Robot.AxisX.ConvertPos2Card(cmp2dPoints.X);
            pulsy[0] = Machine.Instance.Robot.AxisY.ConvertPos2Card(cmp2dPoints.Y);

            if (this.Card.Executor.Cmp2dData(this.Card.CardId, this.Chn, 1, pulsx, pulsy, 0) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            }

            //启动二维比较
            if (this.Card.Executor.Cmp2dStart(this.Card.CardId, this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            }

            ////导入2d比较点数据
            //PointD[] points = new PointD[1];
            //points[0] = cmp2dPoints;
            //if (Machine.Instance.Robot.Cmp2dData(this.Chn, points) != 0)
            //{
            //    AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            //}

            ////启动2d比较
            //if (Machine.Instance.Robot.Cmp2dStart(this.Chn) != 0)
            //{
            //    AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            //}
        }

        public override Result DoWeight(Action sprayAction)
        {
            throw new NotImplementedException();
        }

        #region 手动开关胶
        public override void StartManualSpray(int value)
        {
            this.Spraying();
        }

        public override void StopManualSpray()
        {
            this.SprayOff();
        }
       

        #endregion
    }
}
