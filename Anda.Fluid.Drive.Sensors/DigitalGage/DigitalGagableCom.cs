using Anda.Fluid.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.DigitalGage
{
    public class DigitalGagableCom:IConnectable
    {
        public DigitalGagableCom(EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
        }

        public EasySerialPort EasySerialPort { get; private set; }

        public bool Connect(TimeSpan timeout)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Open();
        }

        public void Disconnect()
        {
            this.EasySerialPort?.Close();
        }
    }
}
