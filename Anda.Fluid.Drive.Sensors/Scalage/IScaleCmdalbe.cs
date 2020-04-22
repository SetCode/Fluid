using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    public interface IScaleCmdalbe
    {
        /// <summary>
        /// 打印指令
        /// </summary>
        string PrintCmd { get; }

        /// <summary>
        /// 归零指令
        /// </summary>
        string ZeroCmd { get; }

        /// <summary>
        /// 去皮指令
        /// </summary>
        string TareCmd { get; }

        /// <summary>
        /// 清零去皮组合
        /// </summary>
        string ZeroTareCombiCmd { get; }

        /// <summary>
        /// 重启
        /// </summary>
        string ResetCmd { get; }

        /// <summary>
        /// 外部校准
        /// </summary>
        string ExternalCaliCmd { get; }

        /// <summary>
        /// 内部校准
        /// </summary>
        string InternalCaliCmd { get; }

    }
}
