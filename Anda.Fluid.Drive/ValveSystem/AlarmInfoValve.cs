using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem
{
    public static class AlarmInfoValve
    {
        public static AlarmInfo ValvesDotWeightCompareAlarm => AlarmInfo.Create(AlarmLevel.Error, 501, "阀单点重量", "主副阀的单点重量的差值超过了设定范围", AlarmHandleType.ImmediateHandle);//"Valve Single dot Weight", "The Difference  of single dot weight between the two Valves exceed the specify scope"
        public static AlarmInfo ValveSingleDotWeightAlarm => AlarmInfo.Create(AlarmLevel.Error, 502, "进行称重()", "单点重量超过设定范围", AlarmHandleType.ImmediateHandle);//"DoWeight()", "Single dot Weight exceed the specify scope"
        public static AlarmInfo ValveDoWtSettingAlarm => AlarmInfo.Create(AlarmLevel.Warn, 503, "进行称重()", "称重参数中的拼板数量和拼板胶点数量为0 ", AlarmHandleType.ImmediateHandle);// "DoWeight()", "this.weightPrm.Panels or this.weightPrm.ShotDotsEachPanel is zero "

        public static AlarmInfo ScaleStabilityAlarm => AlarmInfo.Create(AlarmLevel.Error, 505, "进行称重()", "称重前后的重量变化小于0", AlarmHandleType.ImmediateHandle);//"DoWeight", "Difference Weight between pre-fluid and pos-fluid is less than zero"

        public static AlarmInfo WeightAutoRunAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 506, "自动称重", "自动称重出错", AlarmHandleType.ImmediateHandle);//"weight auto run", "weight auto run exception!"
    }
}
