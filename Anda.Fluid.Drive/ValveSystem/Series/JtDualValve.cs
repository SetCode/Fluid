using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;
using System.Threading;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using System.Diagnostics;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class JtDualValve : DualValve
    {
        private const int SPRAY_CYCLE_MAX = 32760;
        private JtValve jtValve1, jtValve2;
        public JtDualValve(Card card, Valve valve1, Valve valve2) : base(card, valve1, valve2)
        {
            this.jtValve1 = (JtValve)valve1;
            this.jtValve2 = (JtValve)valve2;
        }

        #region SimulSpray //双阀同步开胶
        public override short SprayOne()
        {
            return this.SprayCycle(1);
        }

        public override short Spraying()
        {
            return this.SprayCycle(SPRAY_CYCLE_MAX);
        }

        public override short SprayCycle(short cycle)
        {
            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve1.Chn);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve2.Chn);
            if (rtn != 0) return rtn;

            short outputType;
            //排胶，切换成电平模式
            if (jtValve1.Prm.OnTime > 32760)
            {
                outputType = 1;
                //脉冲模式关胶 
                rtn = this.Card.Executor.CmpStop(this.Card.CardId);
                if (rtn != 0) return rtn;

                rtn = this.Card.Executor.CmpContiPulseMode(this.Card.CardId, 0, cycle, (short)jtValve1.Prm.OffTime);
                if (rtn != 0) return rtn;
                rtn = this.Card.Executor.CmpPulse(this.Card.CardId, 3, outputType, 0);
                if (rtn != 0) return rtn;
            }
            else//循环打胶                                    
            {
                rtn = this.SprayCycle(cycle, (short)jtValve1.Prm.OffTime);
                if (rtn != 0) return rtn;
            }

            return 0;
        }

        public override short SprayCycle(short cycle, short offTime)
        {
            if (cycle <= 0)
            {
                return 0;
            }

            //先停止二维比较
            short rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve1.Chn);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve2.Chn);
            if (rtn != 0) return rtn;

            short outputType = 0;
            //脉冲模式关胶 
            rtn = this.Card.Executor.CmpStop(this.Card.CardId);
            if (rtn != 0) return rtn;

            rtn = this.Card.Executor.CmpContiPulseMode(this.Card.CardId, 1, cycle, offTime);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.CmpPulse(this.Card.CardId, 3, outputType, (short)jtValve1.Prm.OnTime);
            if (rtn != 0) return rtn;

            return 0;
        }

        public override short SprayOneAndWait()
        {
            this.SprayOne();
            int milliSec = (int)((jtValve1.Prm.OnTime + jtValve1.Prm.OffTime) / 1000.0 * 1.05);
            Thread.Sleep(milliSec);
            return 0;
        }

        public override short SprayCycleAndWait(short cycle)
        {
            this.SprayCycle((short)(cycle - 1));
            int milliSec = (int)((this.jtValve1.Prm.OnTime + this.jtValve1.Prm.OffTime) * (cycle - 1) / 1000.0 * 1.05);
            Thread.Sleep(milliSec);
            return this.SprayOneAndWait();
        }

        public override int GetSprayMills(short cycle)
        {
            if (cycle == 0)
            {
                cycle = SPRAY_CYCLE_MAX;
            }
            return (int)((this.jtValve1.Prm.OnTime + this.jtValve1.Prm.OffTime) * cycle / 1000.0 * 1.05);
        }

        public override short SprayOff()
        {
            if (this.Card == null)
            {
                return -10;
            }

            short rtn;
            //先停止二维比较
            rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve1.Chn);
            if (rtn != 0) return rtn;
            rtn = this.Card.Executor.Cmp2dStop(this.Card.CardId, this.valve2.Chn);
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
        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet || Machine.Instance.Valve1.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.valve1.Key);
                lineStartPos = lineStartPos.ToNeedle(this.valve1.Key);
                lineEndPos = lineEndPos.ToNeedle(this.valve1.Key);
                decEndPos = decEndPos.ToNeedle(this.valve1.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.valve1.Key);
                }
            }
            PointD simulDecEndPos = simulPoints[simulPoints.Length - 1];
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
            CrdLnXYAB crdAcc = new CrdLnXYAB()
            {
                EndPosX = lineStartPos.X,
                EndPosY = lineStartPos.Y,
                EndPosA = simulStartPos.X,
                EndPosB = simulStartPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段
            for (int i = 1; i < points.Length; i++)
            {
                CrdLnXYAB crdLn = new CrdLnXYAB()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    EndPosA = simulPoints[i].X,
                    EndPosB = simulPoints[i].Y,
                    Vel = vel,
                    Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                    VelEnd = vel
                };
                crdList.Add(crdLn);
            }
            //减速段
            CrdLnXYAB crdDec = new CrdLnXYAB()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                EndPosA = simulPoints[simulPoints.Length - 1].X,
                EndPosB = simulPoints[simulPoints.Length - 1].Y,
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
                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {//单次插补
                    CrdLnXYAB crd = new CrdLnXYAB()
                    {
                        EndPosX = decEndPos.X,
                        EndPosY = decEndPos.Y,
                        EndPosA = simulPoints[simulPoints.Length - 1].X,
                        EndPosB = simulPoints[simulPoints.Length - 1].Y,
                        Vel = vel,
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = vel
                    };
                    command = new CommandMoveTrc4Axis(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.AxisA,
                        Machine.Instance.Robot.AxisB,
                        Machine.Instance.Robot.TrcPrm4Axis,
                        crd,
                        () =>
                        {
                            t1 = DateTime.Now;
                            ValveSprayServer.Instance.StartSprayEvent.Set();
                        },
                        (int)Machine.Instance.Setting.CardSelect
                        )
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }
                else
                {//多次插补
                    command = new CommandMoveTrc4Axis(
                       Machine.Instance.Robot.AxisX,
                       Machine.Instance.Robot.AxisY,
                       Machine.Instance.Robot.AxisA,
                       Machine.Instance.Robot.AxisB,
                       Machine.Instance.Robot.TrcPrm4Axis,
                       crdList,
                       () =>
                       {
                           t1 = DateTime.Now;
                           ValveSprayServer.Instance.StartSprayEvent.Set();
                       },
                       (int)Machine.Instance.Setting.CardSelect
                       )
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                }

                //设置打胶线程启动时间
                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        t2 = DateTime.Now;
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.jtValve1.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.jtValve1.Prm.OnTime, this.jtValve1.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);

                Debug.WriteLine(t1.ToString("HH::mm::ss::fff"));
                Debug.WriteLine(t2.ToString("HH::mm::ss::fff"));
            };

            //2d比较控制函数
            Action cmp2dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    CmpStop();
                    SimulCmp2dStart((short)Cmp2dSrcType.编码器, (short)this.jtValve1.Prm.Cmp2dMaxErr, points);
                }

                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYABAndReply(decEndPos, simulDecEndPos, vel, acc,(int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.AxisA,
                        Machine.Instance.Robot.AxisB,
                        Machine.Instance.Robot.TrcPrm4Axis,
                        crdList,
                        (int)Machine.Instance.Setting.CardSelect)
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    //Cmp2dStop();
                }
            };

            //一维比较控制函数
            Action cmp1dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    SimulCmp2dStop();
                    CmpStart((short)Cmp2dSrcType.编码器, points);
                }

                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYABAndReply(decEndPos, simulDecEndPos, vel, acc, (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(
                      Machine.Instance.Robot.AxisX,
                      Machine.Instance.Robot.AxisY,
                      Machine.Instance.Robot.AxisA,
                      Machine.Instance.Robot.AxisB,
                      Machine.Instance.Robot.TrcPrm4Axis,
                      crdList,
                      (int)Machine.Instance.Setting.CardSelect)
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    //CmpStop();
                }
            };

            switch (this.jtValve1.Prm.FluidMode)
            {
                case ValveFluidMode.一维比较优先:
                    cmp1dAction();
                    break;
                case ValveFluidMode.二维比较优先:
                    if (interval * 1000 > this.jtValve1.Prm.Cmp2dMaxErr || points.Length < 2)
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
                    if (intervalTime - this.jtValve1.Prm.OnTime < short.MaxValue)
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

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (points == null)
            {
                return Result.FAILED;
            }

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet || Machine.Instance.Valve1.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.valve1.Key);
                arcStartPos = arcStartPos.ToNeedle(this.valve1.Key);
                arcEndPos = arcEndPos.ToNeedle(this.valve1.Key);
                decEndPos = decEndPos.ToNeedle(this.valve1.Key);
                center = center.ToNeedle(this.valve1.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.valve1.Key);
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
            CrdLnXYAB crdAcc = new CrdLnXYAB()
            {
                EndPosX = arcStartPos.X,
                EndPosY = arcStartPos.Y,
                EndPosA = simulStartPos.X,
                EndPosB = simulStartPos.Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = vel
            };
            crdList.Add(crdAcc);
            //匀速段-圆
            for (int i = 1; i < points.Length; i++)
            {
                CrdArcXYABC crdArc = new CrdArcXYABC()
                {
                    EndPosX = points[i].X,
                    EndPosY = points[i].Y,
                    EndPosA = simulPoints[i].X,
                    EndPosB = simulPoints[i].Y,
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
            CrdLnXYAB crdDec = new CrdLnXYAB()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                EndPosA = simulPoints[simulPoints.Length - 1].X,
                EndPosB = simulPoints[simulPoints.Length - 1].Y,
                Vel = vel,
                Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                VelEnd = 0
            };
            crdList.Add(crdDec);
            Result rtn = Result.OK;
            int intervalTime = (int)(intervalSec * 1000000);

            Action timeCtrlAction = () =>
            {
                CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(
                    Machine.Instance.Robot.AxisX,
                    Machine.Instance.Robot.AxisY,
                    Machine.Instance.Robot.AxisA,
                    Machine.Instance.Robot.AxisB,
                    Machine.Instance.Robot.TrcPrm4Axis,
                    crdList,
                    (int)Machine.Instance.Setting.CardSelect);
                command.Starting += () =>
                {
                    ValveSprayServer.Instance.StartSprayEvent.Set();
                };

                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.jtValve1.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.jtValve1.Prm.OnTime, this.jtValve1.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
            };

            Action cmp2dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    SimulCmp2dStart((short)Cmp2dSrcType.编码器, (short)this.jtValve1.Prm.Cmp2dMaxErr, points);
                }
                CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(
                    Machine.Instance.Robot.AxisX,
                    Machine.Instance.Robot.AxisY,
                    Machine.Instance.Robot.AxisA,
                    Machine.Instance.Robot.AxisB,
                    Machine.Instance.Robot.TrcPrm4Axis, crdList,
                    (int)Machine.Instance.Setting.CardSelect);
                Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
                //if (this.RunMode == ValveRunMode.Wet)
                //{
                //    Cmp2dStop();
                //}
            };

            if (this.jtValve1.Prm.FluidMode == ValveFluidMode.时间控制优先)
            {
                if (intervalTime - this.jtValve1.Prm.OnTime < short.MaxValue)
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
                if (interval * 1000 > this.jtValve1.Prm.Cmp2dMaxErr || points.Length < 2)
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

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet || Machine.Instance.Valve1.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.valve1.Key);
                lineStartPos = lineStartPos.ToNeedle(this.valve1.Key);
                lineEndPos = lineEndPos.ToNeedle(this.valve1.Key);
                decEndPos = decEndPos.ToNeedle(this.valve1.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.valve1.Key);
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
                Acc = acc, 
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
                    Acc = acc,
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
                Acc = acc,
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
                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {//单次插补
                    CrdLnXY crd = new CrdLnXY()
                    {
                        EndPosX = decEndPos.X,
                        EndPosY = decEndPos.Y,
                        Vel = vel,
                        Acc = acc, 
                        VelEnd = vel
                    };
                    command = new CommandMoveTrc(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.TrcPrm,
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
                       Machine.Instance.Robot.TrcPrm,
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
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        t2 = DateTime.Now;
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.jtValve1.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.jtValve1.Prm.OnTime, this.jtValve1.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);

                Debug.WriteLine(t1.ToString("HH::mm::ss::fff"));
                Debug.WriteLine(t2.ToString("HH::mm::ss::fff"));
            };

            //2d比较控制函数
            Action cmp2dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    CmpStop();
                    SimulCmp2dStart((short)Cmp2dSrcType.编码器, (short)this.jtValve1.Prm.Cmp2dMaxErr, points);
                }

                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrc command = new CommandMoveTrc(
                        Machine.Instance.Robot.AxisX,
                        Machine.Instance.Robot.AxisY,
                        Machine.Instance.Robot.TrcPrm,
                        crdList)
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    //Cmp2dStop();
                }
            };

            //一维比较控制函数
            Action cmp1dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    SimulCmp2dStop();
                    CmpStart((short)Cmp2dSrcType.编码器, points);
                }

                if (this.jtValve1.Prm.MoveMode == ValveMoveMode.单次插补)
                {
                    rtn = Machine.Instance.Robot.MovePosXYAndReply(decEndPos, vel, acc);
                }
                else
                {
                    CommandMoveTrc command = new CommandMoveTrc(
                      Machine.Instance.Robot.AxisX,
                      Machine.Instance.Robot.AxisY,
                      Machine.Instance.Robot.TrcPrm,
                      crdList)
                    {
                        EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                    };
                    Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                    Machine.Instance.Robot.Fire(command);
                    rtn = Machine.Instance.Robot.WaitCommandReply(command);
                }

                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    //CmpStop();
                }
            };

            switch (this.jtValve1.Prm.FluidMode)
            {
                case ValveFluidMode.一维比较优先:
                    cmp1dAction();
                    break;
                case ValveFluidMode.二维比较优先:
                    if (interval * 1000 > this.jtValve1.Prm.Cmp2dMaxErr || points.Length < 2)
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
                    if (intervalTime - this.jtValve1.Prm.OnTime < short.MaxValue)
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

            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet || Machine.Instance.Valve1.RunMode == ValveRunMode.Dry)
            {
                accStartPos = accStartPos.ToNeedle(this.valve1.Key);
                arcStartPos = arcStartPos.ToNeedle(this.valve1.Key);
                arcEndPos = arcEndPos.ToNeedle(this.valve1.Key);
                decEndPos = decEndPos.ToNeedle(this.valve1.Key);
                center = center.ToNeedle(this.valve1.Key);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = points[i].ToNeedle(this.valve1.Key);
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
                    Machine.Instance.Robot.TrcPrm,
                    crdList);
                command.Starting += () =>
                {
                    ValveSprayServer.Instance.StartSprayEvent.Set();
                };

                ValveSprayServer.Instance.SleepSpan = TimeSpan.FromSeconds(accTime);
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    ValveSprayServer.Instance.SprayAction = () =>
                    {
                        this.SprayCycle((short)points.Count(), (short)(intervalTime - this.jtValve1.Prm.OnTime));
                    };
                }
                else
                {
                    ValveSprayServer.Instance.SprayAction = null;
                }

                Log.Dprint(string.Format("Fluid Line[time ctrl]-> accTime:{0}, intervalTime:{1}, onTime:{2}, offTime{3}.",
                    accTime, intervalTime, this.jtValve1.Prm.OnTime, this.jtValve1.Prm.OffTime));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
            };

            Action cmp2dAction = () =>
            {
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Wet)
                {
                    SimulCmp2dStart((short)Cmp2dSrcType.编码器, (short)this.jtValve1.Prm.Cmp2dMaxErr, points);
                }
                CommandMoveTrc command = new CommandMoveTrc(
                    Machine.Instance.Robot.AxisX,
                    Machine.Instance.Robot.AxisY,
                    Machine.Instance.Robot.TrcPrm,
                    crdList);
                Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}, interval:{1}", this.jtValve1.Prm.Cmp2dMaxErr, interval));
                Machine.Instance.Robot.Fire(command);
                rtn = Machine.Instance.Robot.WaitCommandReply(command);
                //if (this.RunMode == ValveRunMode.Wet)
                //{
                //    Cmp2dStop();
                //}
            };

            if (this.jtValve1.Prm.FluidMode == ValveFluidMode.时间控制优先)
            {
                if (intervalTime - this.jtValve1.Prm.OnTime < short.MaxValue)
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
                if (interval * 1000 > this.jtValve1.Prm.Cmp2dMaxErr || points.Length < 2)
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

        protected override void SimulCmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {
            PointD[] cmp2dPoints = new PointD[points.Length];
            //获取2d比较点数据，转换为相对启动点的相对坐标
            for (int i = 0; i < points.Length; i++)
            {
                cmp2dPoints[i] = new PointD(
                    points[i].X - Machine.Instance.Robot.PosX,
                    points[i].Y - Machine.Instance.Robot.PosY);
            }
            short threshold = (short)this.jtValve1.Prm.Cmp2dThreshold;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }
            //初始化2d比较
            short res = Machine.Instance.Robot.Cmp2dInit(this.valve1.Chn, cmp2dSrc, (short)this.jtValve1.Prm.OnTime, cmp2dMaxErr, threshold);
            if (Machine.Instance.Robot.Cmp2dInit(this.valve2.Chn, cmp2dSrc, (short)this.jtValve1.Prm.OnTime, cmp2dMaxErr, threshold) != 0 && res != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }
            //导入2d比较点数据
            res = Machine.Instance.Robot.Cmp2dData(this.valve1.Chn, cmp2dPoints);
            if (Machine.Instance.Robot.Cmp2dData(this.valve2.Chn, cmp2dPoints) != 0 && res != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            }
            //启动2d比较
            res = Machine.Instance.Robot.Cmp2dStart(this.valve1.Chn);
            if (Machine.Instance.Robot.Cmp2dStart(this.valve2.Chn) != 0)
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
            //双阀主阀使用相同数据，主阀触发副阀也触发，都使用buf1
            short m = this.Card.Executor.CmpData(axis.CardId, axis.AxisId, source, 0, 0, (short)this.jtValve1.Prm.OnTime, ref buf1, (short)buf1.Length, ref buf1, (short)buf1.Length);
            return m;
        }

        public override Result FluidLine(SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam, double acc)
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

        #endregion
    }
}
