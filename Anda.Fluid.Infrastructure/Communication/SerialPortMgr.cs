using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Infrastructure.Communication
{
    public sealed class SerialPortMgr : EntityMgr<EasySerialPort, int>
    {
        private readonly static SerialPortMgr instance = new SerialPortMgr();
        private SerialPortMgr()
        {

        }
        public static SerialPortMgr Instance => instance;

        /// <summary>
        /// 获取当前计算机中可用串口
        /// </summary>
        /// <returns></returns>
        public static string[] GetComPortList()
        {
            string[] portNames = SerialPort.GetPortNames();
            //对串口号进行排序
            try
            {
                for (int i = 0; i < portNames.Length; i++)
                {
                    for (int j = 0; j < portNames.Length - 1 - i; j++)
                    {
                        if (int.Parse(portNames[j].Substring(3, 1)) > int.Parse(portNames[j + 1].Substring(3, 1)))
                        {
                            string temp = portNames[j];
                            portNames[j] = portNames[j + 1];
                            portNames[j + 1] = temp;
                        }
                    }
                }

                return portNames;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
