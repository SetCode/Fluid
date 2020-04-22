using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.ConveryorHeater
{
    public class ConveyorHeaterConnecion : IConnectable
    {


        public EasySerialPort EasySerialPort { get; private set; }
        public ConveyorHeaterConnecion(EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
        }

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
