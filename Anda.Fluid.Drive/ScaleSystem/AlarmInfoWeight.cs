using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ScaleSystem
{
    public static class AlarmInfoWeight
    {
        public static AlarmInfo StabilityTimeOutAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 400, "weight read", "weight reading reaches stability time out");
        public static AlarmInfo ScaleCupOverFlowAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 401, "weight scale cup", "weight scale cup over flow!");

        public static AlarmInfo WeightAutoRunAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 402, "weight auto run", "weight auto run exception!");
    }
}
