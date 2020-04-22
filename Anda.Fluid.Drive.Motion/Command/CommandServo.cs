using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Alarming;
using System.Diagnostics;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandServo : CommandMotion
    {
        private Stopwatch stopWatch = new Stopwatch();
        private bool isServoOn;
        public CommandServo(Axis[] axes, bool isServoOn)
            : base(axes)
        {
            this.isServoOn = isServoOn;
        }

        public CommandServo(Axis axis, bool isServoOn)
            : this(new Axis[] { axis }, isServoOn)
        {

        }

        public override void Call()
        {
            this.State = CommandState.Running;
            foreach (var item in this.Axes)
            {
                item.ClrSts();
                item.State.SetBusy(AxisMoveMode.Servo);
                item.Servo(this.isServoOn);
            }
            this.stopWatch.Start();
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

            if (this.stopWatch.ElapsedMilliseconds > 1000)
            {
                this.State = CommandState.Failed;
                foreach (var item in this.Axes)
                {
                    item.State.SetIdle();
                }
                return;
            }

            bool flag = false;
            for (int i = 0; i < this.Axes.Length; i++)
            {
                if (this.Axes[i].IsAlarm.IsRising)
                {
                    this.Axes[i].State.SetAlarm(AxisAlarmType.Alarm);
                    this.State = CommandState.Failed;
                    AlarmServer.Instance.Fire(this.Axes[i], AlarmInfoMotion.FatalServoOn);
                    return;
                }
                if (this.Axes[i].IsServoOn.Value != this.isServoOn)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                this.State = CommandState.Running;
                return;
            }
            this.State = CommandState.Succeed;
            foreach (var item in this.Axes)
            {
                item.State.SetIdle();
            }
        }

        public override void HandleMsg(CmdMsgType msgType)
        {

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
