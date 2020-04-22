using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using System.Threading;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Infrastructure.Trace;
using System.Diagnostics;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Infrastructure.Utils;
using System.IO;
using System.Windows.Forms;
using Anda.Fluid.Drive.GlueManage;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Prime;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.PurgeWithPrime;
using Anda.Fluid.Drive.Motion.Locations;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class JtValve : Valve
    {
        private const int SPRAY_CYCLE_MAX = 32760;
        new public JtValvePrm Prm { get; set; }

        public JtValve(ValveType valveType, Proportioner proportioner, Card card, short chn, ValvePrm prm) : base(valveType, ValveSeries.喷射阀, proportioner, card, chn, prm)
        {
            this.Prm = prm.JtValvePrm;
        }

        public JtValve(Valve valve) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, valve.Prm)
        {

        }

        public JtValve(Valve valve, ValvePrm prm) : this(valve.ValveType, valve.Proportioner, valve.Card, valve.Chn, prm)
        {

        }

        #region Spray

        public override double SpraySec => (this.Prm.OnTime + this.Prm.OffTime) / 1000000.0;

        /// <summary>
        /// 打一次胶
        /// </summary>
        /// <returns></returns>
        public short SprayOne()
        {
            return this.SprayCycle(1);
        }

        public override short Spraying()
        {
            if (Machine.Instance.Setting.CardSelect == CardSelection.ADMC4)
            {
                return this.SprayCycle(0);
            }
            else
            {
                return this.SprayCycle(SPRAY_CYCLE_MAX);
            }
        }

        public override short SprayCycle(short cycle)
        {
            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.Chn);
            if (rtn != 0) return rtn;

            short outputType;
            //排胶，切换成电平模式
            if (this.Prm.OnTime > 32760)
            {
                outputType = 1;
                //脉冲模式关胶 
                rtn = this.Card.Executor.CmpStop(this.Card.CardId);
                if (rtn != 0) return rtn;

                rtn = this.Card.Executor.CmpContiPulseMode(this.Card.CardId, 0, cycle, (short)this.Prm.OffTime);
                if (rtn != 0) return rtn;
                rtn = this.Card.Executor.CmpPulse(this.Card.CardId, (short)(this.Chn + 1), outputType, 0);
                if (rtn != 0) return rtn;
            }
            else//循环打胶                                    
            {
                rtn = this.SprayCycle(cycle, (short)this.Prm.OffTime);
                if (rtn != 0) return rtn;
            }

            return 0;
        }

        public override short SprayCycle(short cycle, short offTime)
        {
            if (cycle < 0)
            {
                return 0;
            }

            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.Chn);
            if (rtn != 0) return rtn;
            //脉冲模式关胶 
            short outputType = 0;
            rtn = this.Card.Executor.CmpStop(this.Card.CardId);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.CmpContiPulseMode(this.Card.CardId, 1, cycle, offTime);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.CmpPulse(this.Card.CardId, (short)(this.Chn + 1), outputType, (short)this.Prm.OnTime);
            if (rtn != 0) return rtn;
            return 0;
        }

        public override short SprayOneAndWait()
        {
            this.SprayOne();
            int milliSec = (int)((this.Prm.OnTime + this.Prm.OffTime) / 1000.0 * 1.05);
            Thread.Sleep(milliSec);
            return 0;
        }
        public override short SprayOneAndWait(int sprayingTime)
        {
            return this.SprayOneAndWait();
        }

        public override short SprayCycleAndWait(short cycle)
        {
            if (cycle > 1)
            {
                this.SprayCycle((short)(cycle - 1));
                int milliSec = (int)((this.Prm.OnTime + this.Prm.OffTime) * (cycle - 1) / 1000.0 * 1.05);
                Thread.Sleep(milliSec);
            }
            return this.SprayOneAndWait();
        }

        //没有用上，注释处理
        //public override int GetSprayMills(short cycle)
        //{
        //    if (cycle == 0)
        //    {
        //        cycle = SPRAY_CYCLE_MAX;
        //    }
        //    return (int)((this.Prm.OnTime + this.Prm.OffTime) * cycle / 1000.0 * 1.05);
        //}

        public override short SprayOff()
        {
            if (this.Card == null)
            {
                return -10;
            }

            short rtn;
            //先停止二维比较
            rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.Chn);
            if (rtn != 0) return rtn;

            //脉冲模式关胶 
            rtn = this.Card.Executor.CmpStop(this.Card.CardId);
            if (rtn != 0) return rtn;

            ////电平方式关胶
            //short level = 0;
            ////输出类型  0：脉冲方式  1：电平方式      
            //short outputType = 0;
            //rtn = this.Card.Executor.CmpPulse(this.Card.CardId, level, outputType, 0);
            //if (rtn != 0) return rtn;
            return this.SprayOne();
        }
        #endregion

        #region Fluid
        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.Key, this.CurTilt);
                lineStartPos = lineStartPos.ToNeedle(this.Key, this.CurTilt);
                lineEndPos = lineEndPos.ToNeedle(this.Key, this.CurTilt);
                decEndPos = decEndPos.ToNeedle(this.Key, this.CurTilt);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.Key, this.CurTilt);
                }
            }

            double interval = 0;
            if (points.Length >= 2)
            {
                interval = points[0].DistanceTo(points[1]);
            }

            //计算加速段时间，用于启动时间控制打胶
            double accTime = this.CalcAccTime(accStartPos, lineStartPos, vel, acc * 1000);

            //连续插补
            List<ICrdable> crdList = new List<ICrdable>();
            //加速段
            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = lineStartPos.X,
                EndPosY = lineStartPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段
            for (int i = 1; i < points.Length; i++)
            {
                CrdLnXY crdLn = new CrdLnXY()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    Vel = vel,
                    Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                    VelEnd = vel
                };
                crdList.Add(crdLn);
            }
            //减速段
            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = 0
            };
            crdList.Add(crdDec);

            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now;
            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);
            //时间控制函数
            Action timeCtrlAction = () =>
            {
                ICommandable command = null;
                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {//单次插补
                    CrdLnXY crd = new CrdLnXY()
                    {
                        EndPosX = decEndPos.X,
                        EndPosY = decEndPos.Y,
                        Vel = vel,
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = vel
                    };
                    command = new CommandMoveTrc(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.TrcPrmWeight,
                        crd,
                        () =>
                        {
                            t1 = DateTime.Now;
                            ValveSprayServer.Instance.StartSprayEvent.Set();
                        })
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }
                else
                {//多次插补
                    command = new CommandMoveTrc(
                       Machine.Instance.Robot.AxisX,
                       Machine.Instance.Robot.AxisY,
                       Machine.Instance.Robot.TrcPrmWeight,
                       crdList,
                       () =>
                       {
                           t1 = DateTime.Now;
                           ValveSprayServer.Instance.StartSprayEvent.Set();
                       })
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }

                //设置打胶线程启动时间
                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                if (this.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        t2 = DateTime.Now;
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.Prm.OnTime, this.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);

                Debug.WriteLine(t1.ToString("HH::mm::ss::fff"));
                Debug.WriteLine(t2.ToString("HH::mm::ss::fff"));
            };

            //2d比较控制函数
            Action cmp2dAction = () =>
            {
                if (this.RunMode == ValveRunMode.Wet)
                {
                    CmpStop();
                    Cmp2dStart((short)Cmp2dSrcType.编码器, (short)this.Prm.Cmp2dMaxErr, points);
                }

                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                       Machine.Instance.Robot.AxisX,
                       Machine.Instance.Robot.AxisY,
                       Machine.Instance.Robot.TrcPrmWeight,
                       crdList, this.Chn, points.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (this.RunMode == ValveRunMode.Wet)
                {
                    //Cmp2dStop();
                }
            };

            //一维比较控制函数
            Action cmp1dAction = () =>
            {
                if (this.RunMode == ValveRunMode.Wet)
                {
                    Cmp2dStop();
                    CmpStart((short)Cmp2dSrcType.编码器, points);
                }

                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                      Machine.Instance.Robot.AxisX,
                      Machine.Instance.Robot.AxisY,
                      Machine.Instance.Robot.TrcPrmWeight,
                      crdList, this.Chn, points.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (this.RunMode == ValveRunMode.Wet)
                {
                    //CmpStop();
                }
            };

            switch (this.Prm.FluidMode)
            {
                case ValveFluidMode.一维比较优先:
                    cmp1dAction();
                    break;
                case ValveFluidMode.二维比较优先:
                    if (interval * 1000 > this.Prm.Cmp2dMaxErr || points.Length < 2)
                    {
                        cmp2dAction();
                    }
                    else
                    {
                        //timeCtrlAction();
                        cmp1dAction();
                    }
                    break;
                case ValveFluidMode.时间控制优先:
                    if (intervalTime - this.Prm.OnTime < short.MaxValue)
                    {//判断脉宽是否short类型溢出，
                        timeCtrlAction();
                    }
                    else
                    {//溢出：位置比较模式
                        //cmp2dAction();
                        cmp1dAction();
                    }
                    break;
            }


            return rtn;
        }

        /// <summary>
        /// 手动打线，供四方位角度校准时使用
        /// </summary>
        /// <param name="accStartPos"></param>
        /// <param name="lineStartPos"></param>
        /// <param name="lineEndPos"></param>
        /// <param name="decEndPos"></param>
        /// <param name="vel"></param>
        /// <param name="points"></param>
        /// <param name="intervalSec"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public Result FluidManualLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            if (points == null)
            {
                return Result.FAILED;
            }

            double interval = 0;
            if (points.Length >= 2)
            {
                interval = points[0].DistanceTo(points[1]);
            }

            //计算加速段时间，用于启动时间控制打胶
            double accTime = this.CalcAccTime(accStartPos, lineStartPos, vel, acc * 1000);

            //连续插补
            List<ICrdable> crdList = new List<ICrdable>();
            //加速段
            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = lineStartPos.X,
                EndPosY = lineStartPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段
            for (int i = 1; i < points.Length; i++)
            {
                CrdLnXY crdLn = new CrdLnXY()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    Vel = vel,
                    Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                    VelEnd = vel
                };
                crdList.Add(crdLn);
            }
            //减速段
            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdDec);

            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now;
            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);
            //时间控制函数
            Action timeCtrlAction = () =>
            {
                ICommandable command = null;
                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {//单次插补
                    CrdLnXY crd = new CrdLnXY()
                    {
                        EndPosX = decEndPos.X,
                        EndPosY = decEndPos.Y,
                        Vel = vel,
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = vel
                    };
                    command = new CommandMoveTrc(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.TrcPrmWeight,
                        crd,
                        () =>
                        {
                            t1 = DateTime.Now;
                            ValveSprayServer.Instance.StartSprayEvent.Set();
                        })
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }
                else
                {//多次插补
                    command = new CommandMoveTrc(
                       Machine.Instance.Robot.AxisX,
                       Machine.Instance.Robot.AxisY,
                       Machine.Instance.Robot.TrcPrmWeight,
                       crdList,
                       () =>
                       {
                           t1 = DateTime.Now;
                           ValveSprayServer.Instance.StartSprayEvent.Set();
                       })
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }

                //设置打胶线程启动时间
                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                ValveSprayServer.Instance.SprayAction = () =>
                {
                    t2 = DateTime.Now;
                    this.SprayCycle((short)points.Count(), (short)(intervalTime - this.Prm.OnTime));
                };

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.Prm.OnTime, this.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);

                Debug.WriteLine(t1.ToString("HH::mm::ss::fff"));
                Debug.WriteLine(t2.ToString("HH::mm::ss::fff"));
            };

            //2d比较控制函数
            Action cmp2dAction = () =>
            {
                CmpStop();
                Cmp2dStart((short)Cmp2dSrcType.编码器, (short)this.Prm.Cmp2dMaxErr, points);

                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                       Machine.Instance.Robot.AxisX,
                       Machine.Instance.Robot.AxisY,
                       Machine.Instance.Robot.TrcPrmWeight,
                       crdList, this.Chn, points.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                //Cmp2dStop();
            };

            //一维比较控制函数
            Action cmp1dAction = () =>
            {
                Cmp2dStop();
                CmpStart((short)Cmp2dSrcType.编码器, points);

                if (this.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(
                      Machine.Instance.Robot.AxisX,
                      Machine.Instance.Robot.AxisY,
                      Machine.Instance.Robot.TrcPrmWeight,
                      crdList, this.Chn, points.ToList())
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                //CmpStop();
            };

            switch (this.Prm.FluidMode)
            {
                case ValveFluidMode.一维比较优先:
                    cmp1dAction();
                    break;
                case ValveFluidMode.二维比较优先:
                    if (interval * 1000 > this.Prm.Cmp2dMaxErr || points.Length < 2)
                    {
                        cmp2dAction();
                    }
                    else
                    {
                        //timeCtrlAction();
                        cmp1dAction();
                    }
                    break;
                case ValveFluidMode.时间控制优先:
                    if (intervalTime - this.Prm.OnTime < short.MaxValue)
                    {//判断脉宽是否short类型溢出，
                        timeCtrlAction();
                    }
                    else
                    {//溢出：位置比较模式
                        //cmp2dAction();
                        cmp1dAction();
                    }
                    break;
            }


            return rtn;
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.Key);
                arcStartPos = arcStartPos.ToNeedle(this.Key);
                arcEndPos = arcEndPos.ToNeedle(this.Key);
                decEndPos = decEndPos.ToNeedle(this.Key);
                center = center.ToNeedle(this.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.Key);
                }
            }

            double interval = 0;
            if (points.Length >= 2)
            {
                interval = points[0].DistanceTo(points[1]);
            }
            double accTime = this.CalcAccTime(accStartPos, arcStartPos, vel, acc * 1000);

            //连续差补
            List<ICrdable> crdList = new List<ICrdable>();
            //加速段-直线
            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = arcStartPos.X,
                EndPosY = arcStartPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段-圆
            for (int i = 1; i < points.Length; i++)
            {
                CrdArcXYC crdArc = new CrdArcXYC()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    CenterX = center.X - points[i - 1].X,
                    CenterY = center.Y - points[i - 1].Y,
                    Clockwise = clockwize,
                    Vel = vel,
                    Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                    VelEnd = vel
                };
                crdList.Add(crdArc);
            }
            //减速段-直线
            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = 0
            };
            crdList.Add(crdDec);

            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);

            Action timeCtrlAction = () =>
            {
                CommandMoveTrc command = new CommandMoveTrc(
                    Machine.Instance.Robot.AxisX,
                    Machine.Instance.Robot.AxisY,
                    Machine.Instance.Robot.TrcPrmWeight,
                    crdList);
                command.Starting += () =>
                {
                    ValveSprayServer.Instance.StartSprayEvent.Set();
                };

                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                if (this.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.Prm.OnTime, this.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
            };

            Action cmp2dAction = () =>
            {
                if (this.RunMode == ValveRunMode.Wet)
                {
                    Cmp2dStart((short)Cmp2dSrcType.编码器, (short)this.Prm.Cmp2dMaxErr, points);
                }
                CommandMoveTrcCmp2d command = new CommandMoveTrcCmp2d(Machine.Instance.Robot.AxisX, Machine.Instance.Robot.AxisY,
                  Machine.Instance.Robot.TrcPrmWeight, crdList, this.Chn, points.ToList());
                Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
                //if (this.RunMode == ValveRunMode.Wet)
                //{
                //    Cmp2dStop();
                //}
            };
            Action cmp2dMode1dAction = () =>
            {
                if (this.RunMode == ValveRunMode.Wet)
                {
                    Cmp2dMode1dStart((short)Cmp2dSrcType.编码器, (short)this.Prm.Cmp2dMaxErr);
                }
                PointD[] cmp2dPoints = new PointD[points.Length];
                //获取2d比较点数据，转换为相对启动点的相对坐标
                for (int i = 0; i < points.Length; i++)
                {
                    cmp2dPoints[i] = new PointD(
                        points[i].X - Machine.Instance.Robot.PosX,
                        points[i].Y - Machine.Instance.Robot.PosY);
                }
                CommandMoveTrcCmp2dMode1d command = new CommandMoveTrcCmp2dMode1d(Machine.Instance.Robot.AxisX, Machine.Instance.Robot.AxisY,
                  Machine.Instance.Robot.TrcPrmWeight, crdList, this.Chn, cmp2dPoints.ToList());
                Log.Dprint(string.Format("Fluid Arc[2d mode 1d ctrl] -> maxErr:{0},interval:{1}", this.Prm.Cmp2dMaxErr, interval));
                Machine.Instance.Robot.Fire(command); ;
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
            };

            if (this.Prm.FluidMode == ValveFluidMode.时间控制优先)
            {
                if (intervalTime - this.Prm.OnTime < short.MaxValue)
                {//判断脉宽是否short类型溢出，
                    timeCtrlAction();
                }
                else
                {//溢出：位置比较模式
                    cmp2dAction();
                }
            }
            else
            {
                //if (this.Prm.Cmp2dThreshold != 10 && this.Prm.Cmp2dMaxErr < 100)
                //{
                //    cmp2dMode1dAction();
                //}
                //else 
                if (interval * 1000 > this.Prm.Cmp2dMaxErr || points.Length < 2)
                {
                    cmp2dAction();
                }
                else
                {
                    timeCtrlAction();
                }
            }

            return rtn;
        }

        protected override void Cmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {
            PointD[] cmp2dPoints = new PointD[points.Length];
            //获取2d比较点数据，转换为相对启动点的相对坐标
            for (int i = 0; i < points.Length; i++)
            {
                cmp2dPoints[i] = new PointD(
                    points[i].X - Machine.Instance.Robot.PosX,
                    points[i].Y - Machine.Instance.Robot.PosY);
            }
            short threshold = (short)this.Prm.Cmp2dThreshold;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }
            //初始化2d比较
            if (Machine.Instance.Robot.Cmp2dInit(this.Chn, cmp2dSrc, (short)this.Prm.OnTime, cmp2dMaxErr, threshold) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }
            //导入2d比较点数据
            if (Machine.Instance.Robot.Cmp2dData(this.Chn, cmp2dPoints) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            }
            //启动2d比较
            if (Machine.Instance.Robot.Cmp2dStart(this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            }
        }

        protected override void Cmp2dMode1dStart(short cmp2dSrc, short cmp2dMaxErr)
        {
            short threshold = (short)this.Prm.Cmp2dThreshold;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }
            //初始化2d比较(一维模式)
            if (Machine.Instance.Robot.Cmp2dMode1dInit(this.Chn, cmp2dSrc, (short)this.Prm.OnTime, cmp2dMaxErr, threshold) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }
            //启动2d比较
            if (Machine.Instance.Robot.Cmp2dStart(this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            }
        }

        protected override short CmpStart(short source, PointD[] points)
        {

            if (points == null || points.Length == 0)
            {
                return -10;
            }

            Axis axis = null;
            int[] buf1 = new int[points.Length];
            int[] buf2 = new int[1];
            int startPos, interval;

            Action updateBuf2 = () =>
            {
                if (buf1[0] == 0)
                {
                    buf1[0] += 1;
                    buf2[0] = 1000000;
                }
                else if (buf1[0] > 0)
                {
                    buf2[0] = 1000000;
                }
                else
                {
                    buf2[0] = -1000000;
                }
            };

            if (points.Length == 1)
            {
                axis = Machine.Instance.Robot.AxisX;
                buf1[0] = axis.ConvertPos2Card(points[0].X - Machine.Instance.Robot.PosX);
                updateBuf2();
                startPos = buf1[0];
                interval = 0;
            }
            else
            {
                double lx = points[points.Length - 1].X - points[0].X;
                double ly = points[points.Length - 1].Y - points[0].Y;
                if (Math.Abs(lx) >= Math.Abs(ly))
                {
                    axis = Machine.Instance.Robot.AxisX;
                    for (int i = 0; i < points.Length; i++)
                    {
                        buf1[i] = axis.ConvertPos2Card(points[i].X - Machine.Instance.Robot.PosX);
                        if (lx > 0 && buf1[i] <= 0)
                        {
                            buf1[i] = 1;
                        }
                        else if (lx < 0 && buf1[i] >= 0)
                        {
                            buf1[i] = -1;
                        }
                    }
                    updateBuf2();
                    startPos = buf1[0];
                    interval = axis.ConvertPos2Card(lx / (points.Length - 1));
                }
                else
                {
                    axis = Machine.Instance.Robot.AxisY;
                    for (int i = 0; i < points.Length; i++)
                    {
                        buf1[i] = axis.ConvertPos2Card(points[i].Y - Machine.Instance.Robot.PosY);
                        if (ly > 0 && buf1[i] <= 0)
                        {
                            buf1[i] = 1;
                        }
                        else if (ly < 0 && buf1[i] >= 0)
                        {
                            buf1[i] = -1;
                        }
                    }
                    updateBuf2();
                    startPos = buf1[0];
                    interval = axis.ConvertPos2Card(ly / points.Length);
                }
            }

            switch (this.Chn)
            {
                case 0:
                    short m;
                    m = this.Card.Executor.CmpData(axis.CardId, axis.AxisId, source, 0, 0, (short)this.Prm.OnTime, ref buf1, (short)buf1.Length, ref buf2, (short)buf2.Length);
                    //m = this.Card.Executor.CmpLinear(axis.CardId, axis.AxisId, (short)(this.Chn + 1), startPos, points.Length, interval, (short)this.Prm.OnTime, source);
                    return m;
                case 1:
                    return this.Card.Executor.CmpData(axis.CardId, axis.AxisId, source, 0, 0, (short)this.Prm.OnTime, ref buf2, (short)buf2.Length, ref buf1, (short)buf1.Length);
                    //return this.Card.Executor.CmpLinear(axis.CardId, axis.AxisId, (short)(this.Chn + 1), startPos, points.Length, interval, (short)this.Prm.OnTime, source);
            }
            return -10;

        }

        private void Cmp2dData(PointD[] points)
        {
            PointD[] cmp2dPoints = new PointD[points.Length];
            //获取2d比较点数据，转换为相对启动点的相对坐标
            for (int i = 0; i < points.Length; i++)
            {
                cmp2dPoints[i] = new PointD(
                    points[i].X - Machine.Instance.Robot.PosX,
                    points[i].Y - Machine.Instance.Robot.PosY);
            } 
            //导入2d比较点数据
            if (Machine.Instance.Robot.Cmp2dData(this.Chn, cmp2dPoints) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            }
        }

        #endregion

        #region Purge & Prime

        public override Result DoPurgeAndPrime()
        {
            return PurgeAndPrimeFactory.GetIPurgePrimable().DoPurgeAndPrime(this);
        }

        public override Result DoPrime()
        {
            return PrimeFactory.GetIPrimable().DoPrime(this);
        }

        public override Result DoPurge()
        {
            return PurgeFactory.GetIPurgable().DoPurge(this);
        }
        #endregion

        #region Soak
        public override Result DoSoak()
        {
            Result res = Result.OK;
            Location soakNeedle = Machine.Instance.Robot.SystemLocations.SoakLoc.ToNeedle(ValveType);
            res =Machine.Instance.Robot.MoveSafeZAndReply();
            if (!res.IsOk)
            {
                return res;
            }            
            res=Machine.Instance.Robot.MovePosXYAndReply(soakNeedle.X, soakNeedle.Y);
            if (!res.IsOk)
            {
                return res;
            }
            res=Machine.Instance.Robot.MovePosZAndReply(soakNeedle.Z);
            if (!res.IsOk)
            {
                return res;
            }
            return Result.OK;
        }
        public override Result OutSoak()
        {
            Result res = Result.OK;
            res = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!res.IsOk)
            {
                return res;
            }
            return res;
        }

        #endregion

        #region Weight
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
        public override Result DoWeight()
        {
            Result ret = Result.OK;
            try
            {
                Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do weight begin");
                ret = this.DoWeight(this.Shot);
                if (!ret.IsOk)
                {
                    return ret;
                }
                this.canCompare = true;
                ret = this.checkValveDotsWeight();

                if (ret == Result.FAILED)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoValve.ValvesDotWeightCompareAlarm);
                    return ret;
                }
                Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, this.GetType().Name, "do weight end");
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
            }
            catch (Exception)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoValve.WeightAutoRunAlarm);
            }
            return ret;
        }
        public override Result DoWeight(Action sprayAction)
        {
            Result ret = Result.OK;
            if (sprayAction == null)
            {
                return Result.FAILED;
            }
            try
            {
                double beforeWeight = 0.0;
                double weight = 0.0;
                // 手动称重需要重置胶阀状态
                if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
                {
                    ret = this.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                Machine.Instance.Robot.MoveSafeZAndReply();
                ret = this.MoveToScaleLoc();
                if (!ret.IsOk)
                {
                    return ret;
                }
                Machine.Instance.Scale.Scalable.ReadWeight(out beforeWeight);
                this.weightPrm.SetWeightBeforeSpray(beforeWeight);//称重前读数
                if (this.weightPrm.PatternCount == 0 || this.weightPrm.ShotDotsEachPattern == 0)
                {
                    ret = Result.FAILED;
                    AlarmServer.Instance.Fire(this, AlarmInfoValve.ValveDoWtSettingAlarm);
                    return ret;
                }
                //点胶
                sprayAction?.Invoke();
                Machine.Instance.Scale.Scalable.ReadWeight(out weight);
                this.weightPrm.SetCurrentWeight(weight);

                ret = this.weightPrm.SetDiffWeight();//计算点胶前后的天平拼版重量差，
                if (ret == Result.FAILED)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoValve.ScaleStabilityAlarm);
                    return ret;
                }
                //累计重量
                this.AddCumulativeWeight();
                //计算单点重量
                ret = this.CalSingleDotWeight();
                if (ret == Result.FAILED)
                {
                    return ret;
                }
                if (!this.CalibWeight())
                {
                    ret = Result.FAILED;
                    AlarmServer.Instance.Fire(this, AlarmInfoValve.ValveSingleDotWeightAlarm);
                    return ret;
                }
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
            }
            catch (Exception)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoValve.WeightAutoRunAlarm);
            }
            return ret;

        }

        private bool isStop = false;
        /// <summary>
        /// 单点CPK
        /// </summary>
        /// <param name="times"></param>
        /// <param name="cycles"></param>
        /// <param name="interval"></param>
        /// <param name="outweights"></param>
        /// <returns></returns>
        public override Result WeightCpk(int times, short cycles, int interval, out double[] outweights)
        {
            Result ret = Result.OK;
            double curWeight = 0;
            double lastWeight = 0;
            double[] weights = new double[times];
            outweights = weights;
            this.isStop = false;
            ret = this.DoPurgeAndPrime();
            if (ret == Result.FAILED)
            {
                return ret;
            }
            //到点胶位
            this.MoveToScaleLoc();
            if (ret == Result.FAILED)
            {
                return ret;
            }
            //for (int i = 0; i < 3; i++)
            //{
            //    int result =Machine.Instance.Scale.Scalable.ReadWeight(out curWeight);
            //    if (result==4)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(100);
            //}   
            Machine.Instance.Scale.Scalable.ReadWeight(out curWeight);
            this.weightPrm.SetWeightBeforeSpray(curWeight);
            lastWeight = curWeight;
            int timesCout = 0;
            for (int i = 0; i < times; i++)
            {
                //if (this.isStop)
                //{
                //    this.isStop = false;
                //    break;
                //}
                if (timesCout >= interval)
                {
                    Logger.DEFAULT.Debug("timesCount is:  " + timesCout.ToString());
                    ret = this.DoPurge();
                    if (ret != Result.OK)
                        return ret;
                    ret = this.MoveToScaleLoc();
                    if (ret != Result.OK)
                        return ret;
                    timesCout = 0;
                }
                Thread.Sleep(Machine.Instance.Scale.Scalable.Prm.ReadDelay);
                //点一次胶                
                this.SprayCycleAndWait(cycles);
                timesCout++;
                int result = Machine.Instance.Scale.Scalable.ReadWeight(out curWeight);
                if (result == 4)//称重停止
                {
                    break;
                }
                //ret = Machine.Instance.Scale.Scalable.IsScaleCupOverFlow(curWeight);//胶杯超重
                //if (ret == Result.FAILED)
                //{
                //    return ret;
                //}
                Log.Print(curWeight.ToString());
                this.weightPrm.SetCurrentWeight(curWeight);
                weights[i] = curWeight - lastWeight;
                this.AddCumulativeWeight(curWeight - lastWeight);
                lastWeight = curWeight;
                this.weightPrm.SetWeightBeforeSpray(lastWeight);
                Thread.Sleep(100);
            }

            Machine.Instance.Robot.MoveSafeZAndReply();
            outweights = weights;
            return ret;
        }

        public override void WeightCpkStop()
        {
            this.isStop = true;
            Machine.Instance.Scale.Scalable.StopReadWeight();

        }
        /// <summary>
        /// 单点重和标准单点重的比较
        /// </summary>
        /// <returns></returns>
        public override bool CalibWeight()
        {
            //表示客户没有输入校验标准重量和偏差
            if (this.weightPrm.StandardWeight == 0 || this.weightPrm.WeightOffset == 0)
            {
                return false;
            }

            if (this.weightPrm.SingleDotWeight < this.weightPrm.StandardWeight * (1 - this.weightPrm.WeightOffset / 100.0d) || this.weightPrm.SingleDotWeight > this.weightPrm.StandardWeight * (1 + this.weightPrm.WeightOffset / 100.0d))
            {
                return false;
            }
            return true;

        }


        protected override int GetTotalDots()
        {
            return this.weightPrm.ShotDotsEachPattern * this.weightPrm.PatternCount;
        }
        public override void AddCumulativeWeight()
        {
            AddCumulativeWeight(this.weightPrm.DifferWeight);
        }


        #endregion

        public override Result FluidLine(SvValveFludLineParam svValveLineParam, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result SuckBack(double suckBackTime)
        {
            throw new NotImplementedException();
        }

        public override Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc)
        {
            throw new NotImplementedException();
        }

        #region 手动开关胶
        public override void StartManualSpray(int value)
        {
            if (value == 0)
            {
                this.Spraying();
                isManualSpraying = true;
            }
            else
            {
                this.SprayCycle((short)value);
                isManualSpraying = true;
                timer1.Interval = (short)value * (this.Prm.OnTime + this.Prm.OffTime) / 1000;
                timer1.Start();
            }
        }

        public override void StopManualSpray()
        {
            if (this.isManualSpraying)
            {
                this.SprayOff();
                this.timer1.Stop();
                this.isManualSpraying = false;
            }
        }
        #endregion

        /// <summary>
        /// 计算加速时间
        /// </summary>
        /// <param name="accStartPoint">加速起点</param>
        /// <param name="lineStartPoint">直线起点</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns></returns>
        private double CalcAccTime(PointD accStartPoint, PointD lineStartPoint, double vel, double acc)
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

        public override Result FluidSymbolLines(List<CrdSymbolLine> SymbolLinesCrdData, GearValveFluidSymbolLinesPrm fluidPrm, double acc, double offsetX = 0, double offsetY = 0)
        {
            return Result.OK;
        }

        public override Result BufFluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.Key, this.CurTilt);
                lineStartPos = lineStartPos.ToNeedle(this.Key, this.CurTilt);
                lineEndPos = lineEndPos.ToNeedle(this.Key, this.CurTilt);
                decEndPos = decEndPos.ToNeedle(this.Key, this.CurTilt);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.Key, this.CurTilt);
                }
            }

            double interval = 0;
            if (points.Length >= 2)
            {
                interval = points[0].DistanceTo(points[1]);
            }
            //计算加速段时间，用于启动时间控制打胶
            double accTime = this.CalcAccTime(accStartPos, lineStartPos, vel, acc * 1000);

            List<ICrdable> crdlist = new List<ICrdable>();
            //加速段
            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = lineStartPos.X,
                EndPosY = lineStartPos.Y,
                Vel = vel,
                Acc = acc, 
                VelEnd = vel
            };
            crdlist.Add(crdAcc);
            //匀速段
            for (int i = 1; i < points.Length; i++)
            {
                CrdLnXY crdLn = new CrdLnXY()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    Vel = vel,
                    Acc = acc, 
                    VelEnd = vel
                };
                crdlist.Add(crdLn);
            }
            //减速段
            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc, 
                VelEnd = vel
            };
            crdlist.Add(crdDec);

            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now;
            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);

            if (this.RunMode == ValveRunMode.Wet)
            {
                rtn = Machine.Instance.Robot.BufFluidLnXY(decEndPos, vel, acc, points.ToList());
            }
            else if (this.RunMode == ValveRunMode.Dry)
            {
                rtn = Machine.Instance.Robot.BufMoveLnXY(decEndPos, vel, acc);
            }
            return rtn;
        }

        public override Result BufFluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (this.RunMode == ValveRunMode.Wet || this.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.Key);
                arcStartPos = arcStartPos.ToNeedle(this.Key);
                arcEndPos = arcEndPos.ToNeedle(this.Key);
                decEndPos = decEndPos.ToNeedle(this.Key);
                center = center.ToNeedle(this.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.Key);
                }
            }

            double interval = 0;
            if (points.Length >= 2)
            {
                interval = points[0].DistanceTo(points[1]);
            }
            double accTime = this.CalcAccTime(accStartPos, arcStartPos, vel, acc * 1000);

            //连续差补
            List<ICrdable> crdList = new List<ICrdable>();
            //加速段-直线
            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = arcStartPos.X,
                EndPosY = arcStartPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段-圆
            for (int i = 1; i < points.Length; i++)
            {
                CrdArcXYC crdArc = new CrdArcXYC()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    CenterX = center.X - points[i - 1].X,
                    CenterY = center.Y - points[i - 1].Y,
                    Clockwise = clockwize,
                    Vel = vel,
                    Acc = acc,
                    VelEnd = vel
                };
                crdList.Add(crdArc);
            }
            //减速段-直线
            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };
            crdList.Add(crdDec);

            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);

            if (this.RunMode == ValveRunMode.Wet)
            {
                rtn = Machine.Instance.Robot.BufFluidArcXY(decEndPos, center, clockwize, vel, acc, points.ToList());
            }
            else if (this.RunMode == ValveRunMode.Dry)
            {
                rtn = Machine.Instance.Robot.BufMoveArcXY(decEndPos, center, clockwize, vel, acc);
            }
            return rtn;
        }

        public override Result StartBufFluid(InitLook LookAheadPrm)
        {
            Cmp2dPrm cmp2dPrm = new Cmp2dPrm()
            {
                Chn = this.Chn,
                Src = (short)Cmp2dSrcType.编码器,
                PulseWidth = (short)this.Prm.OnTime,
                Maxerr = (short)this.Prm.Cmp2dMaxErr,
                Threshold = (short)this.Prm.Cmp2dThreshold,
                Start = new PointD(Machine.Instance.Robot.PosXY)
            };
            CommandMoveTrcBufFluid command = new CommandMoveTrcBufFluid(
                Machine.Instance.Robot.AxisX, 
                Machine.Instance.Robot.AxisY, 
                Machine.Instance.Robot.TrcPrm, 
                cmp2dPrm,
                LookAheadPrm,
                Machine.Instance.Robot.BufFluidItems);
            Machine.Instance.Robot.Fire(command);
            return Machine.Instance.Robot.WaitCommandReply(command);
        }

        
    }
}
