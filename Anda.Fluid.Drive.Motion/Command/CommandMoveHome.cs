using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class MoveHomePrm
    {
        public HomeMode Mode { get; set; }
        public short MoveDir { get; set; }
        public short IndexDir { get; set; }
        public double VelHigh { get; set; }
        public double VelLow { get; set; }
        public double Acc { get; set; }
        public double HomeOffset { get; set; }
        public double SearchHomeDis { get; set; }
        public double SearchIndexDis { get; set; }
        public double EscapeStep { get; set; }
    }

    public struct MoveHomeSts
    {
        /// <summary>
        /// 0:stop|1:running
        /// </summary>
        public short Run { get; set; }
        /// <summary>
        /// 0: no error
        /// </summary>
        public short Error { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommandMoveHome : CommandMotion
    {
        private Stopwatch stopWatch = new Stopwatch();

        public CommandMoveHome(Axis[] axes, MoveHomePrm[] moveHomePrms)
            : base(axes)
        {
            this.HomePrms = moveHomePrms;
            this.HomeStss = new MoveHomeSts[axes.Length];
        }
        public CommandMoveHome(Axis axis, MoveHomePrm moveHomePrm)
            : this(new Axis[] { axis }, new MoveHomePrm[] { moveHomePrm })
        {

        }

        [JsonProperty]
        public MoveHomePrm[] HomePrms { get; private set; }

        public MoveHomeSts[] HomeStss { get; private set; }

        public override void Call()
        {
            this.State = CommandState.Running;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                this.Axes[i].State.SetBusy(AxisMoveMode.MoveHome);
                this.Axes[i].IsHomeError.Update(false);
                this.Axes[i].Servo(true);
                MoveHomePrm moveHomePrm = this.HomePrms[i];
                this.Axes[i].Card.Executor.MoveHome(
                    this.Axes[i].CardId,
                    this.Axes[i].AxisId,
                    moveHomePrm.Mode,
                    moveHomePrm.MoveDir,
                    moveHomePrm.IndexDir,
                    this.Axes[i].ConvertVel2Card(moveHomePrm.VelHigh),
                    this.Axes[i].ConvertVel2Card(moveHomePrm.VelLow),
                    moveHomePrm.Acc,
                    this.Axes[i].ConvertPos2Card(moveHomePrm.HomeOffset),
                    this.Axes[i].ConvertPos2Card(moveHomePrm.SearchHomeDis),
                    this.Axes[i].ConvertPos2Card(moveHomePrm.SearchIndexDis),
                    this.Axes[i].ConvertPos2Card(moveHomePrm.EscapeStep));
            }
        }

        public override bool Guard()
        {
            return true;
        }

        public override void Update()
        {
            if (this.State != CommandState.Running)
            {
                return;
            }

            bool flag = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                short run, error;
                this.Axes[i].Card.Executor.GetMoveHomeStatus(this.Axes[i].CardId, this.Axes[i].AxisId, out run, out error);
                this.HomeStss[i].Run = run;
                this.HomeStss[i].Error = error;
                bool isError = (error != 0) ? true : false;
                this.Axes[i].IsHomeError.Update(isError);

                if (this.Axes[i].IsHomeError.Value)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalMoveHomeError);
                    return;
                }

                if (this.HomeStss[i].Run == 1)
                {
                    flag = true;
                }

                if(this.Axes[i].IsMoving.Value)
                {
                    flag = true;
                }
            }

            if(flag)
            {
                this.State = CommandState.Running;
                return;
            }

            if (!this.stopWatch.IsRunning)
            {
                this.stopWatch.Restart();
            }
            if(this.stopWatch.ElapsedMilliseconds < 200)
            {
                return;
            }
            this.stopWatch.Stop();
            Thread.Sleep(500);
            foreach (var item in this.Axes)
            {
                item.ClrSts();
                item.ZeroPos();
            }
            this.State = CommandState.Succeed;
            foreach (var item in this.Axes)
            {
                item.State.SetIdle();
                item.IsHomeError.Update(false);
            }
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
                case CmdMsgType.Resume:
                    if (this.State == CommandState.Pause)
                    {
                        this.Call();
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

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
