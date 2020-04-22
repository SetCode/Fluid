using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Anda.Fluid.Domain.Sensors
{
    public class ComParams
    {
        /// <summary>
        /// 串口接口名
        /// </summary>
        public string PortName;
        /// <summary>
        /// 波特率，默认9600
        /// </summary>
        public int BaudRate;
        /// <summary>
        /// 数据位，默认8
        /// </summary>
        public int DataBits;
        /// <summary>
        /// 停止位，默认1，其余1.5/2
        /// </summary>
        public StopBits StopBits;
        /// <summary>
        /// 奇偶检验，默认无，对应None，其余奇Odd/偶Even
        /// </summary>
        public Parity Parity;
    }
}
