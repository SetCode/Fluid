using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Alarming
{
    public interface IAlarmObservable
    {
        void HandleAlarmEvent(AlarmEvent e);
    }

    public interface IRealTimeAlarmObservable
    {
        void HandleRealTimeAlarm(ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>> dic);
    }

    public interface IAlarmDiObservable
    {
        void HnadleAlarmDi();
    }

    public interface IAlarmLightable
    {
        void HandleAlarmEvent(AlarmHandleType alarmType);

        void StopLightTower();
    }
}
