using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    public static class AlarmInfoScale
    {
        public static AlarmInfo ScalePrmSettingAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 403, "天平参数设置", "天平单次读取时间延时或读取次数或单次读取前延时参数设置有误", AlarmHandleType.ImmediateHandle);//"weight read", "weight reading reaches stability time out"
        public static AlarmInfo StabilityTimeOutAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 400, "重量读取", "重量读取超过稳定时长", AlarmHandleType.ImmediateHandle);//"weight read", "weight reading reaches stability time out"
        public static AlarmInfo ScaleCupOverFlowAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 401, "胶杯", "胶杯超重", AlarmHandleType.ImmediateHandle);//"weight scale cup", "weight scale cup over flow!"


    }
}
