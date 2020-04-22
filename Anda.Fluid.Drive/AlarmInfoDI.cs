using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive
{
    public static class AlarmInfoDI
    {
        public static AlarmInfo ErrorAirPressure => AlarmInfo.Create(AlarmLevel.Error, 700, "输入", "气压错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "AirPressure Error"
        public static AlarmInfo ErrorValve1Temp => AlarmInfo.Create(AlarmLevel.Error, 701, "输入", "阀1温度错误", AlarmHandleType.AutoAndDelayHandle);//"DI", "Valve1Temp Error"
        public static AlarmInfo ErrorValve2Temp => AlarmInfo.Create(AlarmLevel.Error, 702, "输入", "阀2温度错误", AlarmHandleType.AutoAndDelayHandle);//"DI", "Valve2Temp Error"
        public static AlarmInfo ErrorValve1GlueEmpty => AlarmInfo.Create(AlarmLevel.Error, 703, "输入", "阀1胶水为空", AlarmHandleType.AutoAndDelayHandle);//"DI", "Valve1Glue Is Empty"
        public static AlarmInfo ErrorValve2GlueEmpty => AlarmInfo.Create(AlarmLevel.Error, 704, "输入", "阀2胶水为空", AlarmHandleType.AutoAndDelayHandle);//"DI", "Valve2Glue Is Empty"
        public static AlarmInfo ErrorDoorGuard => AlarmInfo.Create(AlarmLevel.Error, 705, "输入", "门禁打开", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Door Is Open"
        public static AlarmInfo ErrorExhaust => AlarmInfo.Create(AlarmLevel.Error, 706, "输入", "排风错误", AlarmHandleType.AutoAndDelayHandle);//"DI", "Exhaust Is Error"
        public static AlarmInfo Conveyor1PreTemp => AlarmInfo.Create(AlarmLevel.Error, 707, "输入", "轨道1预热站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Conveyor1PreTemp Error"
        public static AlarmInfo Conveyor1WorkTemp => AlarmInfo.Create(AlarmLevel.Error, 708, "输入", "轨道1点胶站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);// "DI", "Conveyor1WorkTemp Error"
        public static AlarmInfo Conveyor1FinishTemp => AlarmInfo.Create(AlarmLevel.Error, 709, "输入", "轨道1保温站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Conveyor1FinishTemp Error"
        public static AlarmInfo Conveyor2PreTemp => AlarmInfo.Create(AlarmLevel.Error, 710, "输入", "轨道2预热站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Conveyor2PreTemp Error"
        public static AlarmInfo Conveyor2WorkTemp => AlarmInfo.Create(AlarmLevel.Error, 711, "输入", "轨道2点胶站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Conveyor2WorkTemp Error"
        public static AlarmInfo Conveyor2FinishTemp => AlarmInfo.Create(AlarmLevel.Error, 712, "输入", "轨道2保温站温度错误", AlarmHandleType.AutoAndImmeDiateHandle);//"DI", "Conveyor2FinishTemp Error"
    }
}
