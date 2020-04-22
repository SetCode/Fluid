using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;
using Anda.Fluid.Drive.GlueManage;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveTracker
{
    /*
    * 点胶前：
      Settling Time: 当喷射阀运动到点命令指定的坐标位置后，需要等待一定时间，此参数指定等待时间，单位：秒
      Down Speed: 执行点命令过程中 Z轴运动时的下降速度
      Down Accel: 执行点命令过程中 Z轴运动时的下降加速度
      点胶中：
      Dispense Gap: 距板高度，点胶时点胶阀距离基板的高度值
      Num. Shots: 在该点位置点胶时喷胶次数
      Multi-shot Delta: 喷一滴胶水后，点胶阀需要抬高的距离值
                 （注：最后一次喷胶水后不抬高，即如果只喷一次胶水Num. Shots=1，则喷完胶水不执行抬高动作）
      点胶后：
      Dwell Time: 每喷完一滴胶水，需要等待一定时间，该参数指定等待时间长度，单位：秒
                 （注：每喷完一滴胶水都需要等待，即喷完最后一滴胶水也需要等待）
      Retract Distance: 当喷完所有次数的胶水，且Dwell Time指定的等待时间过完之后，需要将点胶阀抬高，此参数指定点胶阀抬高高度
      Retract Speed:  执行点命令过程中点胶阀抬升速度
      Retract Accel: 执行点命令过程中点胶阀抬升加速度
*/

    public class JtValveDot : DotTracker
    {

        public override Result AdjustExecute(Directive directive)
        {
            //TODO 点执行没有该功能
            return Result.OK;
        }

        public override Result DryExecute(Directive directive)
        {
            Dot dot = directive as Dot;
            Log.Dprint("begin to execute Dot-Dry");
            Result ret = Result.OK;

            ret = this.WetAndDryMovelogic(dot);
            if (!ret.IsOk)
            {
                return ret;
            }

            // 等待一段时间 Settling Time
            Log.Dprint("wait Settling Time(s) : " + dot.Param.SettlingTime);
            Thread.Sleep(TimeSpan.FromSeconds(dot.Param.SettlingTime));

            // 抬高一段距离 Retract Distance
            if (dot.Param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + dot.Param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(dot.Param.RetractDistance, dot.Param.RetractSpeed, dot.Param.RetractAccel);
            }

            return ret;
        }

        public override Result InspectDotExecute(Directive directive)
        {
            Dot dot = directive as Dot;
            return this.InspectDotLogic(dot);
        }

        public override Result InspectRectExecute(Directive directive)
        {
            //TODO 点执行没有该功能
            return Result.OK;
        }

        public override Result LookExecute(Directive directive)
        {
            Dot dot = directive as Dot;

            return this.LookLogic(dot);
        }

        public override Result PatternWeightExecute(Directive directive, Valve valve)
        {
            Dot dot = directive as Dot;

            valve = Machine.Instance.Valve1;
            valve.MoveToScaleLoc();
            int shots = dot.IsWeightControl ? (int)((decimal)dot.Weight / (decimal)dot.Program.RuntimeSettings.SingleDropWeight) : dot.Param.NumShots;
            this.MultiShotDelta(dot, valve, shots);
            GlueManagerMgr.Instance.UpdateGlueRemainWeight((int)valve.Key, shots * dot.Program.RuntimeSettings.SingleDropWeight);
            dot.shortNum += shots;
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
        {
            Dot dot = directive as Dot;
            Log.Dprint("begin to execute Dot-Wet");
            Result ret = Result.OK;

            ret = this.WetAndDryMovelogic(dot);
            if (!ret.IsOk)
            {
                return ret;
            }

            // 等待一段时间 Settling Time
            Log.Dprint("wait Settling Time(s) : " + dot.Param.SettlingTime);
            Thread.Sleep(TimeSpan.FromSeconds(dot.Param.SettlingTime));

            //开始打胶
            ret = this.JtValveDispenseLogic(dot);
            if (!ret.IsOk)
            {
                return ret;
            }

            // 抬高一段距离 Retract Distance
            if (dot.Param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + dot.Param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(dot.Param.RetractDistance, dot.Param.RetractSpeed, dot.Param.RetractAccel);
            }

            return ret;
        }


        /// <summary>
        /// 喷射阀打胶逻辑
        /// </summary>
        /// <returns></returns>
        private Result JtValveDispenseLogic(Dot dot)
        {
            Result ret = Result.OK;
            // 计算喷胶次数:
            // 1. 如果是重量线， shots = 总重量 / 单滴胶水的重量
            // 2. 如果是普通线 shots = Num. Shots
            Log.Dprint("isWeightControl=" + dot.IsWeightControl + ", weight=" + dot.Weight + ", singleDropWeight=" + dot.Program.RuntimeSettings.SingleDropWeight);
            int shots;
            if (dot.Program.RuntimeSettings.isHalfAdjust)
            {
                // 四舍五入
                shots = dot.IsWeightControl ? (int)Math.Round(((decimal)dot.Weight / (decimal)dot.Program.RuntimeSettings.SingleDropWeight), MidpointRounding.ToEven) : dot.NumShots;
            }
            else
            {
                shots = dot.IsWeightControl ? (int)((decimal)dot.Weight / (decimal)dot.Program.RuntimeSettings.SingleDropWeight) : dot.NumShots;
            }
            Log.Dprint("shots ： " + shots);

            if (shots == 0)
            {
                if (dot.Weight > dot.Program.RuntimeSettings.SingleDropWeight / 2)
                {
                    shots = 1;
                }
                else
                {
                    return ret;
                }
            }

            if (dot.Param.MultiShotDelta > 0)
            {
                // 开始喷胶
                for (int i = 0; i < shots; i++)
                {
                    if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                    {
                        // 喷射一滴胶水
                        Log.Dprint("spray : " + (i + 1));
                        //Machine.Instance.Valve1.SprayOne();
                        if (dot.RunnableModule.Mode == ModuleMode.MainMode || dot.RunnableModule.Mode == ModuleMode.DualFallow)
                        {
                            Machine.Instance.DualValve.SprayOneAndWait();
                            GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, dot.Program.RuntimeSettings.SingleDropWeight);
                            GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, dot.Program.RuntimeSettings.SingleDropWeight);
                        }
                        else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                        {
                            Machine.Instance.Valve2.SprayOneAndWait();
                            GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, dot.Program.RuntimeSettings.SingleDropWeight);
                        }
                        else
                        {
                            Machine.Instance.Valve1.SprayOneAndWait();
                            GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, dot.Program.RuntimeSettings.SingleDropWeight);
                        }
                    }
                    DateTime sprayEnd = DateTime.Now;

                    // 非最后一滴胶水，抬高一段距离 Multi-shot Delta
                    if (dot.Param.MultiShotDelta > 0 && i != shots - 1)
                    {
                        Log.Dprint("move up Multi-shot Delta : " + dot.Param.MultiShotDelta);
                        ret = Machine.Instance.Robot.MoveIncZAndReply(dot.Param.MultiShotDelta, dot.Param.RetractSpeed, dot.Param.RetractAccel);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }

                    // 等待一段时间 Dwell Time
                    double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                    Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime + ", ellapsed : " + ellapsed);
                    double realDwellTime = dot.Param.DwellTime - ellapsed;
                    if (realDwellTime > 0)
                    {
                        Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                        Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                    }
                }
            }
            else
            {
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (dot.RunnableModule.Mode == ModuleMode.MainMode || dot.RunnableModule.Mode == ModuleMode.DualFallow)
                    {
                        Machine.Instance.DualValve.SprayCycleAndWait((short)shots);
                        GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, dot.Program.RuntimeSettings.SingleDropWeight * shots);
                        GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, dot.Program.RuntimeSettings.SingleDropWeight * shots);
                    }
                    else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                    {
                        Machine.Instance.Valve2.SprayCycleAndWait((short)shots);
                        GlueManagerMgr.Instance.UpdateGlueRemainWeight(1, dot.Program.RuntimeSettings.SingleDropWeight * shots);
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayCycleAndWait((short)shots);
                        GlueManagerMgr.Instance.UpdateGlueRemainWeight(0, dot.Program.RuntimeSettings.SingleDropWeight * shots);
                    }
                }
                DateTime sprayEnd = DateTime.Now;

                // 等待一段时间 Dwell Time
                double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime + ", ellapsed : " + ellapsed);
                double realDwellTime = dot.Param.DwellTime - ellapsed;
                if (realDwellTime > 0)
                {
                    Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                    Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                }
            }

            return ret;
        }

        private Result MultiShotDelta(Dot dot, Valve valve, int shots)
        {
            Result ret = Result.OK;
            if (dot.Param.MultiShotDelta > 0)
            {
                // 开始喷胶
                for (int i = 0; i < shots; i++)
                {
                    Machine.Instance.Valve1.SprayOneAndWait();
                    DateTime sprayEnd = DateTime.Now;
                    // 等待一段时间 Dwell Time
                    double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                    Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime + ", ellapsed : " + ellapsed);
                    double realDwellTime = dot.Param.DwellTime - ellapsed;
                    if (realDwellTime > 0)
                    {
                        Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                        Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                    }
                }
            }
            else
            {
                Machine.Instance.Valve1.SprayCycleAndWait((short)shots);
                DateTime sprayEnd = DateTime.Now;
                // 等待一段时间 Dwell Time
                double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime + ", ellapsed : " + ellapsed);
                double realDwellTime = dot.Param.DwellTime - ellapsed;
                if (realDwellTime > 0)
                {
                    Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                    Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                }
            }
            return Result.OK;
        }


    }
}
