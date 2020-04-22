using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;
using System.Threading;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Motion.Command;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class SvDualValve : DualValve
    {
        private SvValve svValve1, svValve2;
        public SvDualValve(Card card, Valve valve1, Valve valve2) : base(card, valve1, valve2)
        {
            this.svValve1 = (SvValve)valve1;
            this.svValve2 = (SvValve)valve2;
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

            if (this.valve1.RunMode == ValveRunMode.Wet)
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
            ret = Machine.Instance.Robot.MoveArcAndReply(svValveArcParam.endPos, center, clockwize, svValveArcParam.vels[0]);

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
            ret = Machine.Instance.Robot.MoveArcAndReply(svValveArcParam.backTrackPos, center, backClockWise, svValveArcParam.backTrackVel);

            return ret;
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam, double acc)
        {
            Result ret = Result.OK;
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            if (primaryLineParam.endPos == null)
            {
                return Result.FAILED;
            }

            //TODO 前瞻预处理还没加
            //连续插补
            List<ICrdable> crdList = new List<ICrdable>();

            if (primaryLineParam.transPoints.Length > 0)
            {
                for (int i = 0; i < primaryLineParam.transPoints.Length; i++)
                {
                    CrdLnXYAB crd = new CrdLnXYAB()
                    {
                        EndPosX = primaryLineParam.transPoints[i].X,
                        EndPosY = primaryLineParam.transPoints[i].Y,
                        EndPosA = simulLineParam.transPoints[i].X,
                        EndPosB = simulLineParam.transPoints[i].Y,
                        Vel = primaryLineParam.vels[i],
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = primaryLineParam.vels[i]
                    };
                    crdList.Add(crd);
                }
            }

            if (this.valve1.RunMode == ValveRunMode.Wet)
            {
                //如果线的终点和关胶点不一致，则可以启用二维比较
                if (primaryLineParam.endPos != primaryLineParam.stopSprayPos)
                {
                    this.CmpStop();
                    this.Cmp2dStart(primaryLineParam.stopSprayPos);
                }
                else
                {
                    this.Spraying();
                }
            }

            //在起始位置开胶后，延时移动的时间
            Thread.Sleep(TimeSpan.FromSeconds(primaryLineParam.startPosDelay));

            //开始移动
            if (primaryLineParam.transPoints.Length > 0)
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
                Log.Dprint(string.Format("Fluid Line[2d ctrl]-> maxErr:{0}", this.svValve1.Prm.Cmp2dMaxErr));
                Machine.Instance.Robot.Fire(command);
                ret = Machine.Instance.Robot.WaitCommandReply(command);
            }
            else
            {
                ret = Machine.Instance.Robot.MovePosXYABAndReply(primaryLineParam.endPos, simulLineParam.endPos, primaryLineParam.vels[0], acc, (int)Machine.Instance.Setting.CardSelect);
            }

            //如果提前关胶距离是0，就手动关胶
            if (primaryLineParam.endPos == primaryLineParam.stopSprayPos)
            {
                this.SprayOff();
            }

            //在终点延时
            Thread.Sleep(TimeSpan.FromSeconds(primaryLineParam.backTrackDelay));

            //抬高
            ret = Machine.Instance.Robot.MoveIncZAndReply(primaryLineParam.backTrackGap, primaryLineParam.backTrackVel, acc);
            if (!ret.IsOk)
            {
                return ret;
            }

            //开始回走

            List<ICrdable> backCrdList = new List<ICrdable>();

            if (primaryLineParam.backTransPoints.Length > 0)
            {
                for (int i = 0; i < primaryLineParam.backTransPoints.Length; i++)
                {
                    CrdLnXYAB crd = new CrdLnXYAB()
                    {
                        EndPosX = primaryLineParam.backTransPoints[i].X,
                        EndPosY = primaryLineParam.backTransPoints[i].Y,
                        EndPosA = simulLineParam.transPoints[i].X,
                        EndPosB = simulLineParam.transPoints[i].Y,
                        Vel = primaryLineParam.vels[i],
                        Acc = acc, //Machine.Instance.Robot.DefaultPrm.WeightAcc,
                        VelEnd = primaryLineParam.vels[i]
                    };
                    backCrdList.Add(crd);
                }
            }

            if (primaryLineParam.backTransPoints.Length > 0)
            {
                CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(
                      Machine.Instance.Robot.AxisX,
                      Machine.Instance.Robot.AxisY,
                      Machine.Instance.Robot.AxisA,
                      Machine.Instance.Robot.AxisB,
                      Machine.Instance.Robot.TrcPrm4Axis,
                      backCrdList,
                      (int)Machine.Instance.Setting.CardSelect)
                {
                    EnableINP = Machine.Instance.Robot.DefaultPrm.EnableINP
                };
                Machine.Instance.Robot.Fire(command);
                ret = Machine.Instance.Robot.WaitCommandReply(command);
            }
            else
            {
                ret = Machine.Instance.Robot.MovePosXYAndReply(primaryLineParam.backTrackPos, primaryLineParam.vels[0], acc);
            }

            return ret;
        }

        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            throw new NotImplementedException();
        }

        public override int GetSprayMills(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycle(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycle(short cycle, short offTime)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycleAndWait(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short Spraying()
        {
            short ret;
            ret = this.svValve1.Spraying();
            ret = this.svValve2.Spraying();

            return ret;
        }

        public override short SprayOff()
        {
            short ret;
            ret = this.svValve1.SprayOff();
            ret = this.svValve2.SprayOff();

            return ret;
        }

        public override short SprayOne()
        {
            throw new NotImplementedException();
        }

        public override short SprayOneAndWait()
        {
            throw new NotImplementedException();
        }

        public override Result SuckBack(double suckBackTime)
        {
            throw new NotImplementedException();
        }

        protected override short CmpStart(short source, PointD[] points)
        {
            throw new NotImplementedException();
        }

        protected override void SimulCmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {
            throw new NotImplementedException();
        }
        private void Cmp2dStart(PointD shutOffPoint)
        {
            PointD cmp2dPoints = new PointD(
                Math.Round(shutOffPoint.X - Machine.Instance.Robot.PosX, 3),
                Math.Round(shutOffPoint.Y - Machine.Instance.Robot.PosY, 3));

            short threshold = (short)this.svValve1.Prm.Cmp2dThreshold;

            short cmp2dMaxErr = (short)this.svValve1.Prm.Cmp2dMaxErr;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }

            //初始化二维比较
            if (this.Card.Executor.Cmp2dClear(this.svValve1.Card.CardId, this.svValve1.Chn) != 0
                || this.Card.Executor.Cmp2dMode(this.Card.CardId, this.svValve1.Chn) != 0
                || this.Card.Executor.Cmp2dSetPrm(this.Card.CardId, this.svValve1.Chn, Machine.Instance.Robot.AxisX.AxisId, Machine.Instance.Robot.AxisY.AxisId,
                    (short)Cmp2dSrcType.编码器, 1, 1, 10, cmp2dMaxErr, threshold) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }

            //导入2d比较点数据
            PointD[] points = new PointD[1];
            points[0] = cmp2dPoints;
            if (Machine.Instance.Robot.Cmp2dData(this.svValve1.Chn, points) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
            }
            //启动2d比较
            if (Machine.Instance.Robot.Cmp2dStart(this.svValve1.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            }
        }
    }
}
