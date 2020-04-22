using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive.Vision.ASV;
using System.Threading;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase
{
    public abstract class DotTracker : ITrackable
    {
        public abstract Result AdjustExecute(Directive directive);
        public abstract Result DryExecute(Directive directive);
        public abstract Result InspectDotExecute(Directive directive);
        public abstract Result InspectRectExecute(Directive directive);
        public abstract Result LookExecute(Directive directive);
        public abstract Result PatternWeightExecute(Directive directive, Valve valve);
        public abstract Result WetExecute(Directive directive);

        /// <summary>
        /// Wet模式和Dry模式下的到位运动逻辑
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="simulPos"></param>
        /// <param name="currZ"></param>
        /// <param name="targZ"></param>
        /// <returns></returns>
        protected Result WetAndDryMovelogic(Dot dot)
        {
            Result ret = Result.OK;

            PointD pos = this.GetXYPos(dot.Position, dot);
            if (pos == null)
            {
                Log.Dprint(dot.Tilt.ToString() + "姿态的胶枪校准未做!!");
                return Result.FAILED;
            }

            PointD simulPos = GetSimulPos(pos, dot);/*-胶阀原点间距？*/

            //添加拼版补偿到轨迹 --- 暂时不启用，待线和圆弧轨迹重构之后启用
            //pos.X += this.RunnableModule.ModuleOffsetX;
            //pos.Y += this.RunnableModule.ModuleOffsetY;
            ////同步模式下，副阀拼版的补偿在此处添加
            //simulPos.X += this.RunnableModule.SimulModuleOffsetX;
            //simulPos.Y += this.RunnableModule.SimulModuleOffsetY;

            // 倾斜到位
            Log.Dprint("change tilt status : " + dot.Tilt.ToString());
            ret = Machine.Instance.Valve1.ChangeValveTiltStatus(dot.Tilt, FluidProgram.Current.MotionSettings.VelU, FluidProgram.Current.MotionSettings.AccU);
            if (!ret.IsOk)
            {
                return ret;
            }

            double currZ = Machine.Instance.Robot.PosZ;
            double targZ = 0;

            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = dot.Program.RuntimeSettings.BoardZValue + dot.Param.DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(dot.Param.DispenseGap, dot.CurMeasureHeightValue, dot.Tilt);
            }
            double z = 0;
            z = targZ;

            if (currZ > targZ)
            {
                // 移动到指定位置
                Log.Dprint("move to position XY : " + pos);
                if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    //ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, (int)Machine.Instance.Setting.CardSelect);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                        FluidProgram.Current.MotionSettings.VelXYAB,
                        FluidProgram.Current.MotionSettings.AccXYAB,
                        (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    //ret = Machine.Instance.Robot.MovePosXYAndReply(pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                        FluidProgram.Current.MotionSettings.VelXY,
                        FluidProgram.Current.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }

                Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + dot.Param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, dot.Param.DownSpeed, dot.Param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            else
            {

                Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + dot.Param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, dot.Param.DownSpeed, dot.Param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }

                Log.Dprint("move to position XY : " + pos);
                if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    //ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, (int)Machine.Instance.Setting.CardSelect);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                      FluidProgram.Current.MotionSettings.VelXYAB,
                      FluidProgram.Current.MotionSettings.AccXYAB,
                      (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    //ret = Machine.Instance.Robot.MovePosXYAndReply(pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                       FluidProgram.Current.MotionSettings.VelXY,
                       FluidProgram.Current.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// InspectDot模式下的逻辑
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        protected Result InspectDotLogic(Dot dot)
        {
            Log.Dprint("begin to execute Dot-InspectDot");
            Result ret = Result.OK;

            //抬起到安全高度
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            // 移动到指定位置
            Log.Dprint("move to position XY : " + dot.Position);
            //ret = Machine.Instance.Robot.MovePosXYAndReply(dot.Position);
            ret = Machine.Instance.Robot.MovePosXYAndReply(dot.Position,
                FluidProgram.Current.MotionSettings.VelXY,
                FluidProgram.Current.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }

            InspectionDot inspectionDot = InspectionMgr.Instance.FindBy((int)InspectionKey.Dot1) as InspectionDot;
            if (inspectionDot != null)
            {
                Thread.Sleep(inspectionDot.SettlingTime);
                double dx, dy;
                Machine.Instance.CaptureAndInspect(inspectionDot);
                dx = inspectionDot.PhyResultX;
                dy = inspectionDot.PhyResultY;
                string line = string.Format("{0},{1},{2},{3}", Math.Round(dot.Position.X, 3), Math.Round(dot.Position.Y, 3), Math.Round(dx, 3), Math.Round(dy, 3));
                CsvUtil.WriteLine(dot.Program.RuntimeSettings.FilePathInspectDot, line);
                Thread.Sleep(inspectionDot.DwellTime);
            }


            // 等待一段时间 Settling Time
            Log.Dprint("wait Settling Time(s) : " + dot.Param.SettlingTime);
            Thread.Sleep(TimeSpan.FromSeconds(dot.Param.SettlingTime));

            return ret;
        }

        /// <summary>
        /// Look模式下的逻辑
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        protected Result LookLogic(Dot dot)
        {
            Log.Dprint("begin to execute Dot-Look");
            Result ret = Result.OK;

            //抬起到安全高度
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            // 移动到指定位置
            Log.Dprint("move to position XY : " + dot.Position);
            //ret = Machine.Instance.Robot.MovePosXYAndReply(dot.Position);
            ret = Machine.Instance.Robot.MovePosXYAndReply(dot.Position,
                FluidProgram.Current.MotionSettings.VelXY,
                FluidProgram.Current.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }

            // 等待一段时间 Settling Time
            Log.Dprint("wait Settling Time(s) : " + dot.Param.SettlingTime);
            Thread.Sleep(TimeSpan.FromSeconds(dot.Param.SettlingTime));

            return ret;
        }

        /// <summary>
        /// 获取各种模式下XY轴的移动目标位置
        /// </summary>
        /// <returns></returns>
        protected PointD GetXYPos(PointD position, Dot dot)
        {
            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
            {
                return new PointD(position);
            }
            else
            {
                // 以相机为中心的坐标转换成以喷嘴为中心
                return position.ToNeedle(dot.Valve,dot.Tilt);
            }
        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        protected PointD GetSimulPos(PointD pos, Dot dot)
        {
            PointD simulPos = new PointD();

            ///生成副阀相关参数(起点、插补点位)
            if (dot.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - dot.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -simulPos.Y / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(dot.Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(dot.Program.RuntimeSettings.SimulOffsetX, dot.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }
    }
}
