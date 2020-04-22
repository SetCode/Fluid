using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.TrackBase;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.SvValveTracker
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
  Suck Back: 执行回吸动作（区别于喷射阀的地方）
*/
    public class SvValveDot : DotTracker
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

                if (!ret.IsOk)
                    return ret;
            }

            //进行回吸
            ret = this.SuckBack(dot);

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
            //TODO 螺杆阀暂时没有该功能
            return Result.OK;
        }

        public override Result WetExecute(Directive directive)
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

            //开始打胶
            this.SvValveDispenseLogic(dot);
            if (!ret.IsOk)
            {
                return ret;
            }

            // 抬高一段距离 Retract Distance
            if (dot.Param.RetractDistance > 0)
            {
                Log.Dprint("move up RetractDistance : " + dot.Param.RetractDistance);
                ret = Machine.Instance.Robot.MoveIncZAndReply(dot.Param.RetractDistance, dot.Param.RetractSpeed, dot.Param.RetractAccel);

                if (!ret.IsOk)
                    return ret;
            }

            //进行回吸
            ret = this.SuckBack(dot);

            return ret;
        }

        /// <summary>
        /// 螺杆阀打胶逻辑
        /// </summary>
        /// <returns></returns>
        private Result SvValveDispenseLogic(Dot dot)
        {
            Result ret = Result.OK;
            // 计算螺杆阀喷胶时间:
            // 1. 如果是重量线， time = 总重量(mg) / 当前速度下的胶水重量(mg/s)
            // 2. 如果是普通线  time = Num. Shots * Valve On Time

            double valveWeight;
            FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(dot.Program.RuntimeSettings.SvOrGearValveCurrSpeed, out valveWeight);
            //Program.VavelSpeedDic.TryGetValue(this.Program.SvValveCurrSpeed, out valveWeight);
            Log.Dprint("isWeightControl=" + dot.IsWeightControl + ", weight=" + dot.Weight + ", valveWeight=" + valveWeight);

            double time = dot.IsWeightControl ? (dot.Weight / valveWeight) : dot.Param.NumShots * dot.Param.ValveOnTime;
            Log.Dprint("time： " + time);

            //如果打胶过程中需要抬高
            if (dot.Param.MultiShotDelta > 0)
            {
                //抬高次数
                int UpNumber = dot.Param.NumShots - 1;
                double UpInterval = time / dot.Param.NumShots;

                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.Spraying();
                    }
                    else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                    {
                        Machine.Instance.Valve2.Spraying();
                    }
                    else
                    {
                        Machine.Instance.Valve1.Spraying();
                    }

                    for (int i = 0; i < UpNumber; i++)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(UpInterval));
                        Machine.Instance.Robot.MoveIncZAndReply(dot.Param.MultiShotDelta, dot.Param.RetractSpeed, dot.Param.RetractAccel);
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(time - UpNumber * UpInterval));

                    if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.SprayOff();
                    }
                    else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                    {
                        Machine.Instance.Valve2.SprayOff();
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayOff();
                    }
                }

                // 等待一段时间 Dwell Time
                Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime);
                Thread.Sleep(TimeSpan.FromSeconds(dot.Param.DwellTime));
            }
            //打胶过程中不需要抬高
            else
            {
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.Spraying();
                    }
                    else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                    {
                        Machine.Instance.Valve2.Spraying();
                    }
                    else
                    {
                        Machine.Instance.Valve1.Spraying();
                    }
                    //一直打time时长的胶水
                    Thread.Sleep(TimeSpan.FromSeconds(time));
                    if (dot.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.SprayOff();
                    }
                    else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
                    {
                        Machine.Instance.Valve2.SprayOff();
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayOff();
                    }
                }

                // 等待一段时间 Dwell Time
                Log.Dprint("DwellTime(s) : " + dot.Param.DwellTime);
                Thread.Sleep(TimeSpan.FromSeconds(dot.Param.DwellTime));
            }

            return ret;
        }

        /// <summary>
        /// 进行回吸动作
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        private Result SuckBack(Dot dot)
        {
            Result ret = Result.OK;

            if (dot.RunnableModule.Mode == ModuleMode.MainMode)
            {
                ret = Machine.Instance.DualValve.SuckBack(dot.Param.SuckBackTime);
            }
            else if (dot.RunnableModule.Mode == ModuleMode.AssignMode2)
            {
                ret = Machine.Instance.Valve2.SuckBack(dot.Param.SuckBackTime);
            }
            else
            {
                ret = Machine.Instance.Valve1.SuckBack(dot.Param.SuckBackTime);
            }

            return ret;
        }
    }
}
