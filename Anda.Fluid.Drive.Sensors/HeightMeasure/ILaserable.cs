using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.HeightMeasure
{
    /// <summary>
    /// 激光测量接口
    /// </summary>
    public interface Ilaserable : IConnectable, IUpdatable
    {
        Laser.Vendor Vendor { get; }

        ComCommunicationSts CommunicationOK { get; }

        double ErrorValue { get; }

        string CmdReadValue { get; }

        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="value">测量值</param>
        /// <returns>小于0表示通信失败</returns>
        int ReadValue(TimeSpan timeout, out double value);
        
        /// <summary>
        /// 读取通讯模块与探测器的连接状态
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <param name="isConnected">是否连接</param>
        /// <returns></returns>
        //bool ReadConnectState(TimeSpan timeout);
        /// <summary>
        /// 零点偏移操作
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <returns>是否执行成功</returns>
        //bool ZeroOffset(TimeSpan timeout);
        /// <summary>
        /// 取消零点偏移
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <returns>是否执行成功</returns>
        //bool CancelZeroOffset(TimeSpan timeout);
    }
}
