using Anda.Fluid.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public class LightingCom : IConnectable
    {
        public LightingCom() { }
        public LightingCom(EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
        }
        public EasySerialPort EasySerialPort { get; private set; }
        public virtual bool Connect(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public virtual  void Disconnect()
        {
            throw new NotImplementedException();
        }
    }

}
