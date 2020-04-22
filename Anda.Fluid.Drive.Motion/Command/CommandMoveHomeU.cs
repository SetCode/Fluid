using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Trace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    /// <summary>
    /// 四方位机台U轴回原点方式（光电开关+方向判断正负限位）
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveHomeU : CommandMotion
    {
        private int HomeStep = 0;
        private bool goZeroPos = false;
        public CommandMoveHomeU(Axis axis, MoveHomePrm moveHomePrm)
            : base(new Axis[] { axis })
        {
            this.HomePrm = moveHomePrm;
        }

        private Stopwatch stopWatch = new Stopwatch();
        [JsonProperty]
        public MoveHomePrm HomePrm { get; private set; }
        public override void Call()
        {
            this.Axes[0].State.SetBusy(AxisMoveMode.MoveHome);
            this.Axes[0].IsHomeError.Update(false);
            this.Axes[0].Servo(true);
            this.State = CommandState.Running;
            this.Axes[0].State.SetBusy(AxisMoveMode.MoveHome);
            this.Axes[0].SetLimit(9999, -9999);
            this.Axes[0].ZeroPos();
            // 先用Jog运行向负方向运动，直到限位光电触发
            int rtn = this.Axes[0].MoveJog(-HomePrm.VelLow);
            if (rtn != 0)
            {
                this.Axes[0].State.SetAlarm(AxisAlarmType.Alarm);
                this.State = CommandState.Failed;
                AlarmServer.Instance.Fire(this.Axes[0], AlarmInfoMotion.FatalMoveHomeError);
            }
            this.HomeStep = 1;
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Guard()
        {
            return true;
        }

        public override void HandleMsg(CmdMsgType msgType)
        {
            if (this.State == CommandState.Failed)
            {
                return;
            }

            switch (msgType)
            {
                case CmdMsgType.Pause:
                    if (this.State == CommandState.Running)
                    {
                        this.State = CommandState.Pause;
                        this.MoveSmoothStop();
                    }
                    break;
                case CmdMsgType.Stop:
                    this.State = CommandState.Failed;
                    this.MoveSmoothStop();
                    break;
                default:
                    break;
            }
        }

        public override void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }

            bool flag = false;
            short error = 0,rtn = 0;
            // 触发了负限位，反向运动

            if (this.Axes[0].IsNLmt.Value && this.HomeStep == 1 && this.Axes[0].Pos < 0)
            {
                this.Axes[0].MoveAbruptStop();
                Thread.Sleep(50);
                this.Axes[0].ZeroPos();
                // 到0点位置
                rtn = this.Axes[0].Card.Executor.SetMovePos(
                    this.Axes[0].CardId,
                    this.Axes[0].AxisId,
                    this.Axes[0].ConvertPos2Card(this.HomePrm.HomeOffset),
                    this.Axes[0].ConvertVel2Card(this.HomePrm.VelHigh),
                    HomePrm.Acc,
                    HomePrm.Acc);
                Log.Dprint("Home U SetMovePos" + rtn.ToString() + "; Pos :" + this.HomePrm.HomeOffset.ToString() + "; pulse :" + this.Axes[0].ConvertPos2Card(this.HomePrm.HomeOffset).ToString());
                if (rtn == 0)
                {
                    rtn = this.Axes[0].Card.Executor.MovePos(this.Axes[0].CardId, this.Axes[0].AxisId);
                    Log.Dprint("Home U MovePos" + rtn);
                    this.HomeStep = 2;
                    if (rtn == 0)
                    {
                        this.goZeroPos = true;
                        Thread.Sleep(500);
                    }
                }
            }

            // 原点偏移设置不对可能回0位置超出限位
            if (this.Axes[0].Pos > this.HomePrm.HomeOffset + 2 && this.HomeStep == 2)
            {
                this.Axes[0].MoveAbruptStop();
                Log.Dprint("stop pos :" + this.Axes[0].Pos.ToString());
                error = 999;
            }

            error = rtn;
            bool isError = (error != 0) ? true : false;
            this.Axes[0].IsHomeError.Update(isError);

            if (this.Axes[0].IsHomeError.Value)
            {
                this.Axes[0].State.SetAlarm(AxisAlarmType.Alarm);
                this.State = CommandState.Failed;
                AlarmServer.Instance.Fire(this.Axes[0], AlarmInfoMotion.FatalMoveHomeError);
                return;
            }

            if (this.Axes[0].IsMoving.Value)
            {
                flag = true;
            }

            if (!this.goZeroPos)
            {
                flag = true;
            }

            if (flag)
            {
                this.State = CommandState.Running;
                return;
            }

            if (!this.stopWatch.IsRunning)
            {
                this.stopWatch.Restart();
            }
            if (this.stopWatch.ElapsedMilliseconds < 200)
            {
                return;
            }
            this.stopWatch.Stop();
            this.Axes[0].ClrSts();
            this.Axes[0].ZeroPos();
            this.State = CommandState.Succeed;
            this.Axes[0].State.SetIdle();
            this.Axes[0].IsHomeError.Update(false);
        }
    }
}
