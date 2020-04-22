using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors
{
    /// <summary>
    /// 可连接的
    /// </summary>
    public interface IConnectable
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns>连接结果</returns>
        bool Connect(TimeSpan timeout);
        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
    }
}
