using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Barcode
{
    public interface IBarcodeSannable : IConnectable, IUpdatable
    {
        BarcodeScanner.Vendor Vendor { get; }

        ComCommunicationSts CommunicationOK { get; }

        string CmdReadValue { get; }
        //变长返回结果结束符
        char Delimiter { get; set; }

        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="str">读取字符串</param>
        /// <returns>小于0表示通信失败</returns>
        int ReadValue(TimeSpan timeout, out string str);
    }
}
