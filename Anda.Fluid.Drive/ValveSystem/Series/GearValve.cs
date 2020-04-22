using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors.Proportionor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem.Prm;
using System.Threading;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class GearValve : Valve
    {
        new public GearValvePrm Prm { get; set; }
        public GearValve(ValveType valveType, Proportioner proportioner, Card card, short chn, ValvePrm prm) : base(valveType, ValveSeries.齿轮泵阀, proportioner, card, chn, prm)
        {
            this.Prm = prm.GearValvePrm;
        }

        public GearValve(Valve valve) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, valve.Prm)
        {

        }
        public GearValve(Valve valve, ValvePrm prm) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, prm)
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

        public override Result DoWeight(Action sprayAction)
        {
            throw new NotImplementedException();
        }
        #region Soak
        public override Result DoSoak()
        {
            return Result.OK;
        }
        public override Result OutSoak()
        {
            return Result.OK;
        }
        #endregion 
        public override Result FluidSymbolLines(List<CrdSymbolLine> symbolLinesCrdData, GearValveFluidSymbolLinesPrm fluidPrm, double acc, double offsetX = 0, double offsetY = 0)
        {
            Result ret = Result.OK;

            //如果不是CV模式，则要把坐标都转换到阀上
            if (this.RunMode != ValveRunMode.Look)
            {
                PointD offset = new PointD(offsetX, offsetY);
                foreach (var item in symbolLinesCrdData)
                {
                    if (item.Type == 0)
                    {
                        item.Points[0] = item.Points[0].ToNeedle(this.ValveType) + offset;
                        item.Points[1] = item.Points[1].ToNeedle(this.ValveType) + offset;
                    }
                    else
                    {
                        item.Points[0] = item.Points[0].ToNeedle(this.ValveType) + offset;
                        item.Points[1] = item.Points[1].ToNeedle(this.ValveType) + offset;
                        item.Points[2] = item.Points[2].ToNeedle(this.ValveType) + offset;
                    }
                }
            }


            //解析复合线段为对应的插补指令
            List<ICrdable> crdList = new List<ICrdable>();
            for (int i = 0; i < symbolLinesCrdData.Count; i++)
            {
                //如果是直线，则先要判断高度是否一致，一致的话添加一个两轴直线插补，不一致的话要先添加一个BufGear插补
                if (symbolLinesCrdData[i].Type == 0)
                {
                    if (Machine.Instance.Robot.AxisZ.Pos != symbolLinesCrdData[i].EndZ)
                    {
                        CrdXYGear crdXYGear = new CrdXYGear()
                        {
                            GearAxis = Machine.Instance.Robot.AxisZ,
                            DeltaPulse = Machine.Instance.Robot.AxisZ.ConvertPos2Card(symbolLinesCrdData[i].EndZ)
                        };
                        crdList.Add(crdXYGear);
                    }

                    CrdLnXY crd = new CrdLnXY()
                    {
                        EndPosX = symbolLinesCrdData[i].Points[1].X,
                        EndPosY = symbolLinesCrdData[i].Points[1].Y,
                        Vel = fluidPrm.Vel,
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = (i != symbolLinesCrdData.Count - 1 ? 0 : fluidPrm.Vel),

                    };
                    crdList.Add(crd);
                }
                //如果是圆弧，则要先添加一个BufGear插补，再添加一个圆弧插补
                else if (symbolLinesCrdData[i].Type == 1)
                {
                    CrdXYGear crdXYGear = new CrdXYGear()
                    {
                        GearAxis = Machine.Instance.Robot.AxisR,
                        DeltaPulse = Machine.Instance.Robot.AxisR.ConvertPos2Card(symbolLinesCrdData[i].TrackSweep)
                    };
                    crdList.Add(crdXYGear);
                    double r = Math.Sqrt(Math.Pow(symbolLinesCrdData[i].Points[0].X - symbolLinesCrdData[i].Points[1].X, 2)
                              + Math.Pow(symbolLinesCrdData[i].Points[0].Y - symbolLinesCrdData[i].Points[1].Y, 2));
                    CrdArcXYR crd = new CrdArcXYR
                    {
                        EndPosX = symbolLinesCrdData[i].Points[2].X,
                        EndPosY = symbolLinesCrdData[i].Points[2].Y,
                        R = r,
                        Clockwise = (short)symbolLinesCrdData[i].Clockwise,
                        Vel = fluidPrm.ArcSpeed,
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = (i != symbolLinesCrdData.Count - 1 ? 0 : fluidPrm.Vel),

                    };
                    if (Math.Abs(symbolLinesCrdData[i].EndAngle) > 180)
                    {
                        crd.R = crd.R * -1;
                    }
                    crdList.Add(crd);
                }
            }
            //计算关胶位置
            PointD stopSprayPos = this.CalculateStopSprayPos(symbolLinesCrdData, fluidPrm.StopSprayDistance);

            //终点位置
            PointD endPos = new PointD();

            //如果是Wet模式
            if (this.RunMode == ValveRunMode.Wet)
            {
                //得到终点位置
                if (symbolLinesCrdData.Last().Type == 0)
                {
                    endPos = symbolLinesCrdData.Last().Points[1];
                }
                else
                {
                    endPos = symbolLinesCrdData.Last().Points[2];
                }
                endPos.X = Math.Round(endPos.X, 3);
                endPos.Y = Math.Round(endPos.Y, 3);
                //如果线的终点和关胶点不一致，则可以启用二维比较
                if (endPos != stopSprayPos)
                {
                    this.CmpStop();
                    this.Cmp2dStart(stopSprayPos);
                }
                else
                {
                    this.Spraying();
                }
            }

            //移动前的出胶延时
            Thread.Sleep(TimeSpan.FromSeconds(fluidPrm.StartSprayDelay));

            //开始移动
            CommandMoveTrc command = new CommandMoveTrc(
                  Machine.Instance.Robot.AxisX,
                  Machine.Instance.Robot.AxisY,
                  Machine.Instance.Robot.TrcPrm,
                  crdList)
            {
                EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
            };
            Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}", this.Prm.Cmp2dMaxErr));
            Machine.Instance.Robot.Fire(command);
            ret = Machine.Instance.Robot.WaitCommandReply(command);
            if (!ret.IsOk)
                return ret;

            if (this.RunMode == ValveRunMode.Wet)
            {
                //如果提前关胶距离是0，就直接关胶
                if (fluidPrm.StopSprayDistance == 0)
                {
                    this.SprayOff();
                }
            }

            //在终点延时
            Thread.Sleep(TimeSpan.FromSeconds(fluidPrm.EndPosDelay));

            //if (this.RunMode != ValveRunMode.Look)
            //{
            //    //下压
            //    ret = Machine.Instance.Robot.MoveIncZAndReply(-fluidPrm.PressDistance, fluidPrm.PressVel, fluidPrm.PressAcc);
            //    if (!ret.IsOk)
            //        return ret;

            //    //下压保持时间
            //    Thread.Sleep(TimeSpan.FromSeconds(fluidPrm.PressTime));

            //    //抬起
            //    ret = Machine.Instance.Robot.MoveIncZAndReply(fluidPrm.RaiseDistance, fluidPrm.RaiseVel, fluidPrm.RaiseAcc);
            //}

            //开始回走
            //如果是Wet模式或者Dry模式
            if (fluidPrm.BacktrackDistance == 0 || fluidPrm.BacktrackDistance.Equals(0))
            {


            }
            else
            {
                if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
                {
                    //回抬
                    ret = Machine.Instance.Robot.MoveIncZAndReply(fluidPrm.BacktrackGap, fluidPrm.BacktrackSpeed, acc);

                    //计算回走终点位置
                    PointD backTrackPoint = CalculateBackTrackPos(symbolLinesCrdData, fluidPrm.BacktrackDistance);

                    //执行回走
                    List<ICrdable> crdXYZList = new List<ICrdable>();
                    if (symbolLinesCrdData.Last().Type == 0) //直线
                    {
                        CrdXYGear crdXYGear = new CrdXYGear()
                        {
                            GearAxis = Machine.Instance.Robot.AxisZ,
                            DeltaPulse = Machine.Instance.Robot.AxisZ.ConvertPos2Card(fluidPrm.BacktrackEndGap - fluidPrm.BacktrackGap)
                        };
                        crdXYZList.Add(crdXYGear);
                        CrdLnXY crd = new CrdLnXY()
                        {
                            EndPosX = backTrackPoint.X,
                            EndPosY = backTrackPoint.Y,
                            Vel = fluidPrm.BacktrackSpeed,
                            Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                            VelEnd = 0,
                        };
                        crdXYZList.Add(crd);

                    }
                    else      //圆弧
                    {
                        CrdXYGear crdXYZGear = new CrdXYGear()
                        {
                            GearAxis = Machine.Instance.Robot.AxisZ,
                            DeltaPulse = Machine.Instance.Robot.AxisZ.ConvertPos2Card(fluidPrm.BacktrackEndGap - fluidPrm.BacktrackGap)
                        };
                        crdXYZList.Add(crdXYZGear);
                        CrdXYGear crdXYRGear = new CrdXYGear()
                        {
                            GearAxis = Machine.Instance.Robot.AxisR,
                            DeltaPulse = Machine.Instance.Robot.AxisR.ConvertPos2Card(-symbolLinesCrdData.Last().TrackSweep * fluidPrm.BacktrackDistance / 100)
                        };
                        crdXYZList.Add(crdXYRGear);
                        double r = Math.Sqrt(Math.Pow(symbolLinesCrdData.Last().Points[0].X - symbolLinesCrdData.Last().Points[1].X, 2)
                                  + Math.Pow(symbolLinesCrdData.Last().Points[0].Y - symbolLinesCrdData.Last().Points[1].Y, 2));
                        CrdArcXYR crd = new CrdArcXYR
                        {
                            EndPosX = backTrackPoint.X,
                            EndPosY = backTrackPoint.Y,
                            R = r,
                            Clockwise = (short)symbolLinesCrdData.Last().Clockwise == (short)0 ? (short)1 : (short)0,
                            Vel = fluidPrm.BacktrackSpeed,
                            Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                            VelEnd = 0,
                        };
                        if (Math.Abs(symbolLinesCrdData.Last().EndAngle * fluidPrm.BacktrackDistance / 100) > 180)
                        {
                            crd.R = crd.R * -1;
                        }
                        crdXYZList.Add(crd);
                    }
                    CommandMoveTrc command2 = new CommandMoveTrc(
                                     Machine.Instance.Robot.AxisX,
                                     Machine.Instance.Robot.AxisY,
                                     Machine.Instance.Robot.TrcPrm,
                                     crdXYZList)
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Machine.Instance.Robot.Fire(command2);
                    ret = Machine.Instance.Robot.WaitCommandReply(command2);
                }
            }
            //结束回走

            if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
            {
                //抬起到回位高度
                ret = Machine.Instance.Robot.MoveIncZAndReply(fluidPrm.BackGap, fluidPrm.Vel, acc);
            }

            return ret;

        }

        public override Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(SvValveFludLineParam svValveLineParam, double acc)
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

        public override void StartManualSpray(int value)
        {
            this.Spraying();
        }

        public override void StopManualSpray()
        {
            this.SprayOff();
        }

        public override Result SuckBack(double suckBackTime)
        {
            return Result.OK;
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
            throw new NotImplementedException();
        }

        protected override short CmpStart(short source, PointD[] points)
        {
            throw new NotImplementedException();
        }

        protected override int GetTotalDots()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 计算提前关胶的位置
        /// </summary>
        /// <param name="symbolLinesCrdData"></param>
        /// <param name="shutOffDistance"></param>
        /// <returns></returns>
        private PointD CalculateStopSprayPos(List<CrdSymbolLine> symbolLinesCrdData, double shutOffDistance)
        {
            //计算复合线的总长度和各线段的长度
            double sumDistance = 0;
            double[] singleDistance = new double[symbolLinesCrdData.Count];
            for (int i = 0; i < symbolLinesCrdData.Count; i++)
            {
                if (symbolLinesCrdData[i].Type == 0)
                {
                    singleDistance[i] = Math.Sqrt(Math.Pow(symbolLinesCrdData[i].Points[1].Y - symbolLinesCrdData[i].Points[0].Y, 2) + Math.Pow(symbolLinesCrdData[i].Points[1].X - symbolLinesCrdData[i].Points[0].X, 2));
                }
                else
                {
                    singleDistance[i] = this.CalculateArcLength(symbolLinesCrdData[i].TrackStartAngle, symbolLinesCrdData[i].TrackEndAngle, symbolLinesCrdData[i].Points[0], symbolLinesCrdData[i].Points[1], symbolLinesCrdData[i].EndAngle);
                }
                sumDistance += singleDistance[i];
            }

            //确定该距离相对于起始点的距离
            double shutOffDisToStart = sumDistance - shutOffDistance;

            //确定该距离在第几个线段内
            int lineIndex = 0;
            for (int i = 0; i < singleDistance.Length; i++)
            {
                double currDistance = 0;
                for (int j = 0; j <= i; j++)
                {
                    currDistance += singleDistance[j];
                }

                if (currDistance >= shutOffDisToStart)
                {
                    lineIndex = i;
                    break;
                }
            }

            //计算该距离的坐标点相对于所处线段上的距离
            double PreDistance = 0;
            double relativeDistance = 0;
            for (int i = 0; i < lineIndex; i++)
            {
                PreDistance += singleDistance[i];
            }
            relativeDistance = shutOffDisToStart - PreDistance;

            //得到结果
            PointD pos = new PointD();
            //如果在直线上
            if (symbolLinesCrdData[lineIndex].Type == 0)
            {
                //计算线段的角度
                double angleOfLine = Math.Atan2(symbolLinesCrdData[lineIndex].Points[1].Y - symbolLinesCrdData[lineIndex].Points[0].Y,
                    symbolLinesCrdData[lineIndex].Points[1].X - symbolLinesCrdData[lineIndex].Points[0].X) * 180 / Math.PI;

                double cos = Math.Cos(angleOfLine * Math.PI / 180);
                double sin = Math.Sin(angleOfLine * Math.PI / 180);
                pos.X = Math.Round(symbolLinesCrdData[lineIndex].Points[0].X + relativeDistance * cos, 3);
                pos.Y = Math.Round(symbolLinesCrdData[lineIndex].Points[0].Y + relativeDistance * sin, 3);
            }
            //如果在圆弧上
            else
            {
                //计算以起点为基准的关胶距离
                double clockwise = symbolLinesCrdData[lineIndex].Clockwise == 0 ? 1 : 1;
                double shutOffRatio = relativeDistance / singleDistance[lineIndex];
                //double shutOffDegree = (symbolLinesCrdData[lineIndex].EndAngle - symbolLinesCrdData[lineIndex].StartAngle) * shutOffRatio * clockwise;
                double degree = -symbolLinesCrdData[lineIndex].TrackSweep * shutOffRatio * clockwise;
                pos = symbolLinesCrdData[lineIndex].Points[0].Rotate(symbolLinesCrdData[lineIndex].Points[1], degree);
            }

            return pos;

        }


        /// <summary>
        /// 计算回走终点的位置
        /// </summary>
        /// <param name="symbolLinesCrdData"></param>
        /// <param name="backTrackDistance"></param>
        /// <returns></returns>
        private PointD CalculateBackTrackPos(List<CrdSymbolLine> symbolLinesCrdData, double backTrackDistance)
        {
            //计算复合线的总长度和各线段的长度
            double sumDistance = 0;
            double[] singleDistance = new double[symbolLinesCrdData.Count];
            for (int i = 0; i < symbolLinesCrdData.Count; i++)
            {
                if (symbolLinesCrdData[i].Type == 0)
                {
                    singleDistance[i] = Math.Sqrt(Math.Pow(symbolLinesCrdData[i].Points[1].Y - symbolLinesCrdData[i].Points[0].Y, 2) + Math.Pow(symbolLinesCrdData[i].Points[1].X - symbolLinesCrdData[i].Points[0].X, 2));
                }
                else
                {
                    singleDistance[i] = this.CalculateArcLength(symbolLinesCrdData[i].StartAngle, symbolLinesCrdData[i].EndAngle, symbolLinesCrdData[i].Points[0], symbolLinesCrdData[i].Points[1], symbolLinesCrdData[i].EndAngle);
                }
                sumDistance += singleDistance[i];
            }


            //确定该距离在第几个线段内
            int lineIndex = symbolLinesCrdData.Count - 1;

            //确定该距离相对于起始点的距离
            //double shutOffDisToStart = sumDistance - (100 - backTrackDistance) * singleDistance[lineIndex] / 100;
            double shutOffDisToStart = sumDistance - backTrackDistance;
            //计算该距离的坐标点相对于所处线段上的距离
            double PreDistance = 0;
            double relativeDistance = 0;
            for (int i = 0; i < lineIndex; i++)
            {
                PreDistance += singleDistance[i];
            }
            relativeDistance = shutOffDisToStart - PreDistance;

            //得到结果
            PointD pos = new PointD();
            //如果在直线上
            if (symbolLinesCrdData[lineIndex].Type == 0)
            {
                //计算线段的角度
                double angleOfLine = Math.Atan2(symbolLinesCrdData[lineIndex].Points[0].Y - symbolLinesCrdData[lineIndex].Points[1].Y,
                    symbolLinesCrdData[lineIndex].Points[0].X - symbolLinesCrdData[lineIndex].Points[1].X) * 180 / Math.PI;

                double cos = Math.Cos(angleOfLine * Math.PI / 180);
                double sin = Math.Sin(angleOfLine * Math.PI / 180);
                pos.X = Math.Round(symbolLinesCrdData[lineIndex].Points[1].X + relativeDistance * cos, 3);
                pos.Y = Math.Round(symbolLinesCrdData[lineIndex].Points[1].Y + relativeDistance * sin, 3);
            }
            //如果在圆弧上
            else
            {
                //计算以终点为基准的回走终点位置
                double clockwise = symbolLinesCrdData[lineIndex].Clockwise == 0 ? -1 : -1;
                double shutOffRatio = relativeDistance / singleDistance[lineIndex];
                //double shutOffDegree = (symbolLinesCrdData[lineIndex].EndAngle - symbolLinesCrdData[lineIndex].StartAngle) * shutOffRatio * clockwise;
                double degree = -symbolLinesCrdData[lineIndex].TrackSweep * shutOffRatio * clockwise;
                pos = symbolLinesCrdData[lineIndex].Points[2].Rotate(symbolLinesCrdData[lineIndex].Points[1], degree);
            }

            return pos;
        }

        private double CalculateArcLength(double startAngle, double endAngle, PointD startPos, PointD centerPos, double trackSweep)
        {
            if (Math.Abs(trackSweep) > 180)
            {
                double r = Math.Sqrt(Math.Pow(startPos.Y - centerPos.Y, 2) + Math.Pow(startPos.X - centerPos.X, 2));
                return (Math.Abs(endAngle - startAngle) + 180) / 360f * 2 * Math.PI * r;
            }
            else
            {
                double r = Math.Sqrt(Math.Pow(startPos.Y - centerPos.Y, 2) + Math.Pow(startPos.X - centerPos.X, 2));
                return Math.Abs(endAngle - startAngle) / 360f * 2 * Math.PI * r;
            }
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

        public override Result BufFluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result StartBufFluid(InitLook LookAheadPrm)
        {
            throw new NotImplementedException();
        }

        public override Result BufFluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

    }

    public class GearValveFluidSymbolLinesPrm
    {
        /// <summary>
        /// 点胶时的速度
        /// </summary>
        public double Vel { get; set; }

        /// <summary>
        /// 提前关胶距离
        /// </summary>
        public double StopSprayDistance { get; set; }

        /// <summary>
        /// 到位开胶延时
        /// </summary>
        public double StartSprayDelay { get; set; }

        /// <summary>
        /// 终点延时
        /// </summary>
        public double EndPosDelay { get; set; }

        /// <summary>
        /// 下压距离
        /// </summary>
        public double PressDistance { get; set; }

        /// <summary>
        /// 下压速度
        /// </summary>
        public double PressVel { get; set; }

        /// <summary>
        /// 下压加速度
        /// </summary>
        public double PressAcc { get; set; }

        /// <summary>
        /// 下压保持时间
        /// </summary>
        public double PressTime { get; set; }

        /// <summary>
        /// 抬起距离
        /// </summary>
        public double RaiseDistance { get; set; }

        /// <summary>
        /// 抬起速度
        /// </summary>
        public double RaiseVel { get; set; }

        /// <summary>
        /// 抬起加速度
        /// </summary>
        public double RaiseAcc { get; set; }
        /// <summary>
        /// 圆弧基准速度(半径1为基准，速度与半径成反比)
        /// </summary>
        public double ArcSpeed { get; set; }


        /// <summary>
        /// 回走终点位置
        /// </summary>
        public double BacktrackEndPos { get; set; }

        /// <summary>
        /// 回走高度
        /// </summary>
        public double BacktrackGap { get; set; }

        /// <summary>
        /// 回走距离
        /// </summary>

        public double BacktrackDistance { get; set; }
        /// <summary>
        /// 回走速度
        /// </summary>
        public double BacktrackSpeed { get; set; }
        /// <summary>
        /// 回走终点高度
        /// </summary>
        public double BacktrackEndGap { get; set; }

        /// <summary>
        /// 回位高度
        /// </summary>
        public double BackGap { get; set; }

    }
}
