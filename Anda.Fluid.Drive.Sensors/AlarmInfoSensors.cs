using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Sensors
{
    public static class AlarmInfoSensors
    {
        public static AlarmInfo SerialPortOpenAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 300, "打开串口", "串口错误", AlarmHandleType.ImmediateHandle);//"open", "serial port fatal"
        public static AlarmInfo ErrorLaserState => AlarmInfo.Create(AlarmLevel.Error, 301, "激光状态", "未连接", AlarmHandleType.ImmediateHandle);//"laser state", "disconnected"
        public static AlarmInfo ErrorLaserRead => AlarmInfo.Create(AlarmLevel.Error, 302, "激光读取", "数据错误", AlarmHandleType.ImmediateHandle);//"laser read", "data error"
        public static AlarmInfo ErrorProporSet => AlarmInfo.Create(AlarmLevel.Error, 303, "比例阀设置", "设置错误值", AlarmHandleType.ImmediateHandle);//"proportioner set", "set value error"
        public static AlarmInfo ErrorBarcodeScannerState => AlarmInfo.Create(AlarmLevel.Error, 304, "扫码枪状态", "未连接", AlarmHandleType.ImmediateHandle);//"BarcodeScanner state", "disconnected"
        public static AlarmInfo ErrorBarcodeScannerRead => AlarmInfo.Create(AlarmLevel.Error, 305, "扫码枪读取", "数据错误", AlarmHandleType.ImmediateHandle);//"BarcodeScanner read", "data error"
        public static AlarmInfo WarnMeasureHeight => AlarmInfo.Create(AlarmLevel.Warn, 350, "测高", "读取测高值失败", AlarmHandleType.ImmediateHandle);//"measure height", "read height value error"
        public static AlarmInfo InfoMeasuredValue => AlarmInfo.Create(AlarmLevel.Warn, 1000, "测高", "数值：", AlarmHandleType.ImmediateHandle);//"measure height", "value:"
       

        public static AlarmInfo ErrorScaleState => AlarmInfo.Create(AlarmLevel.Error, 401, "天平状态", "未连接", AlarmHandleType.ImmediateHandle);//"scale state", "disconnected"
        public static AlarmInfo ErrorScaleReadNull => AlarmInfo.Create(AlarmLevel.Error, 402, "天平读取", "天平读取值为空值", AlarmHandleType.ImmediateHandle);//"scale read", "read scale value is null"
        public static AlarmInfo ErrorScaleReadIncomplete => AlarmInfo.Create(AlarmLevel.Error, 403, "天平读取", "天平读取值不完整", AlarmHandleType.ImmediateHandle);//"scale read", "read scale value incomplete"
        public static AlarmInfo ErrorScaleReadOverrun => AlarmInfo.Create(AlarmLevel.Error, 404, "天平读取", "天平读取超重或失重", AlarmHandleType.ImmediateHandle);//"scale read", "over weight or weightless"

        public static AlarmInfo GageReadOutTime => AlarmInfo.Create(AlarmLevel.Error, 601, "数字千分表读取", "数字千分表读取数值超时", AlarmHandleType.ImmediateHandle);//"gage ReadHeight", "digital gage read value out time "

        public static AlarmInfo ErrorLightConncet => AlarmInfo.Create(AlarmLevel.Error, 701, "OPT光源连接", "串口连接失败", AlarmHandleType.ImmediateHandle);
    }
}
