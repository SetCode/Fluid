using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public enum AxisStateType
    {
        Idel,
        Busy,
        Alarming
    }

    public enum AxisMoveMode
    {
        None,
        Servo,
        MoveHome,
        MoveJog,
        MovePos,
        MoveTrc,
        EscapeLmt
    }

    public enum AxisAlarmType
    {
        None,
        Alarm,
        Error,
        PLmt,
        NLmt
    }

    public class AxisState
    {
        public AxisStateType StateType { get; private set; } = AxisStateType.Idel;
        public AxisMoveMode MoveMode { get; private set; } = AxisMoveMode.None;
        public AxisAlarmType AlarmType { get; private set; } = AxisAlarmType.None;

        public void SetBusy(AxisMoveMode moveMode)
        {
            this.StateType = AxisStateType.Busy;
            this.MoveMode = moveMode;
            this.AlarmType = AxisAlarmType.None;
        }

        public void SetIdle()
        {
            this.StateType = AxisStateType.Idel;
            this.MoveMode = AxisMoveMode.None;
            this.AlarmType = AxisAlarmType.None;
        }

        public void SetAlarm(AxisAlarmType alarmType)
        {
            this.StateType = AxisStateType.Alarming;
            this.AlarmType = alarmType;
        }
    }
}
