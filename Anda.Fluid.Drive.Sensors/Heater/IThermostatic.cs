using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public interface IThermostatic
    {
        /// <summary>
        /// 控制输出地址
        /// </summary>
        ushort[] ControlSelAddr { get; }
        /// <summary>
        /// 开始加热
        /// </summary>
        ushort StartHeating { get; }
        /// <summary>
        /// 停止加热
        /// </summary>
        ushort StopHeating { get; }
        /// <summary>
        /// 控温功能地址
        /// </summary>
        ushort CtrlTempAddr { get; }
        /// <summary>
        /// 控温输出
        /// </summary>
        ushort[] CtrlTempOutput { get; }
        /// <summary>
        /// 控温开关
        /// </summary>
        ushort[] CtrlTempSwitch { get; }
        /// <summary>
        /// 控温开关地址
        /// </summary>
        ushort CtrlTempSwitchAddr { get; }
        /// <summary>
        /// 从站地址
        /// </summary>
        byte SlaveAddr { get; }
        /// <summary>
        /// 获取从站地址指令
        /// </summary>
        ushort GetSlaveAddrCmd { get; }
        /// <summary>
        /// 测量数据地址
        /// </summary>
        ushort MeasureValueAddr { get; }
        /// <summary>
        /// 
        /// </summary>
        ushort MaxMeasureNum { get; }
        /// <summary>
        /// 各个通道报警起始地址
        /// </summary>
        ushort[] AlarmStartAddr { get; }
        /// <summary>
        /// 各个通道PID参数地址
        /// </summary>
        ushort[] PIDStartAddr { get; }
        /// <summary>
        /// PID参数
        /// </summary>
        PIDParamStruct PIDParams { get; }
        /// <summary>
        /// 下限报警地址
        /// </summary>
        ushort DownAlarmAddr { get; }
        /// <summary>
        /// 上限报警地址
        /// </summary>
        ushort UpAlarmAddr { get; }
        /// <summary>
        /// 加热状态地址
        /// </summary>
        ushort HeatingState { get; }
        /// <summary>
        /// 恒温状态地址
        /// </summary>
        ushort HoldingState { get; }
    }
}
