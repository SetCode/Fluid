using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    /// <summary>
    /// 天平接口类
    /// </summary>
    public interface IScalable : IScaleCmdalbe, IConnectable, IAlarmSenderable, IUpdatable
    {
        ScalePrm Prm { get; set; }

        ScalePrm PrmBackUp { get; set; }
        ComCommunicationSts CommunicationOK { get; }

        Scale.Vendor Vendor { get; }
        
        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int Print(TimeSpan timeout,int readTimes, out double value);

        /// <summary>
        /// 去皮
        /// </summary>
        /// <returns></returns>
        bool Tare();

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        bool Zero();

        /// <summary>
        /// 去皮清零组合
        /// </summary>
        /// <returns></returns>
        bool ZeroTareCombi();

        /// <summary>
        /// 重启
        /// </summary>
        /// <returns></returns>
        bool Reset();

        /// <summary>
        /// 外部校准
        /// </summary>
        /// <returns></returns>
        bool ExternalCali();

        /// <summary>
        /// 内部校准
        /// </summary>
        /// <returns></returns>
        bool InternalCali();

        /// <summary>
        /// 通讯检测
        /// </summary>
        /// <returns></returns>
        bool CommunicationTest();

        int Print(TimeSpan timeOut,out string value);
        void StopReadWeight();
    }
}
