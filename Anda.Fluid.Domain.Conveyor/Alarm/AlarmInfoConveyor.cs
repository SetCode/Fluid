using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Alarm
{
    public static class AlarmInfoConveyor
    {
        public static AlarmInfo WarnConveyorStuck => AlarmInfo.Create(AlarmLevel.Warn, 1000, "轨道", "轨道发生卡板", AlarmHandleType.ImmediateHandle);//Conveyor , Conveyor is Stuck.
        public static AlarmInfo WarnLiftStuck => AlarmInfo.Create(AlarmLevel.Warn, 1001, "气缸", "气缸感应超时", AlarmHandleType.ImmediateHandle);
    }
}
