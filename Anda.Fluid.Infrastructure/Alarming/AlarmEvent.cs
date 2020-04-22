using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Alarming
{
    public class AlarmEvent
    {
        public IAlarmSenderable Sender { get; private set; }
        public AlarmInfo Info { get; private set; }

        public static AlarmEvent Create(IAlarmSenderable sender, AlarmInfo info)
        {
            AlarmEvent alarmEvent = new AlarmEvent()
            {
                Sender = sender,
                Info = info
            };
            return alarmEvent;
        }
    }

    public enum AlarmLevel
    {
        Warn,
        Error,
        Fatal
    }

    public enum AlarmHandleType
    {
        OnlyRecord,
        ImmediateHandle,
        DelayHandle,
        AutoHandle,
        AutoAndImmeDiateHandle,
        AutoAndDelayHandle,
    }

    public class AlarmInfo
    {
        public DateTime DateTime { get; private set; } = DateTime.Now;
        public AlarmLevel Level { get; private set; } = AlarmLevel.Warn;
        public bool CanIgnorled { get; set; } = false;
        public bool IsIgnorled { get; set; } = false;
        public int ErrorCode { get; private set; } = 0;
        public string Where { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public AlarmHandleType HandleType { get; private set; } = AlarmHandleType.OnlyRecord;

        public static AlarmInfo Create(AlarmLevel level, int errorCode, string where, string message, AlarmHandleType handleType)
        {
            AlarmInfo alarmInfo = new AlarmInfo()
            {
                DateTime = DateTime.Now,
                Level = level,
                ErrorCode = errorCode,
                Where = where,
                Message = message,
                HandleType = handleType
            };
            return alarmInfo;
        } 

        public AlarmInfo AppendMsg(string msg)
        {
            Message += string.Format(",{0}", msg);
            return this;
        }
    }
}
